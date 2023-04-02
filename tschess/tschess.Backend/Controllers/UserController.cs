using Tschess.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using htldesk.Application.Dto;
using Tschess.Application.Model;
using Microsoft.EntityFrameworkCore;

[Route("api/users")]
[ApiController]
[AllowAnonymous]
public class UserController : ControllerBase
{
    // DTO class for the JSON body of the login request
    public record CredentialsDto(string username, string password);

    private readonly IConfiguration _config;
    private readonly bool _isDevelopment;
    private readonly TschessContext _db;
    public UserController(IHostEnvironment _env, IConfiguration config, TschessContext context)
    {
        _config = config;
        _isDevelopment = _env.IsDevelopment();
        _db = context;
    }

    /// <summary>
    /// POST /api/user/login
    /// </summary>
    [HttpPost("login")]
    public IActionResult Login([FromBody] CredentialsDto credentials)
    {
        var lifetime = TimeSpan.FromHours(3);
        var searchuser = _config["Searchuser"];
        var searchpass = _config["Searchpass"];
        var secret = Convert.FromBase64String(_config["JwtSecret"]);
        var localAdmins = _config["LocalAdmins"].Split(",");

        using var service = _isDevelopment && !string.IsNullOrEmpty(searchuser)
            ? AdService.Login(searchuser, searchpass, credentials.username)
            : AdService.Login(credentials.username, credentials.password);
        var currentUser = service.CurrentUser;
        if (currentUser is null) { return Unauthorized(); }
        var role = localAdmins.Contains(currentUser.Cn)
                        ? AdUserRole.Management.ToString() : currentUser.Role.ToString();
        var group = (currentUser.Role, currentUser.Classes.Length > 0) switch
        {
            (AdUserRole.Pupil, true) => currentUser.Classes[0],
            (AdUserRole.Pupil, false) => "Unknown class",
            (AdUserRole.Teacher, _) => AdUserRole.Teacher.ToString(),
            (AdUserRole.Management, _) => AdUserRole.Teacher.ToString(),
            (_, _) => AdUserRole.Administration.ToString()
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Payload for our JWT.
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, currentUser.Cn),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                new Claim("Group", group)
            }),
            Expires = DateTime.UtcNow + lifetime,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new
        {
            Username = currentUser.Cn,
            Role = role,
            Group = group,
            Token = tokenHandler.WriteToken(token)
        });
    }

    [HttpPost("registerDev")]
    public IActionResult RegisterDev(UserDto dto)
    {
        _db.Add(
            new User(
                name: dto.Username,
                email: dto.Email,
                password: dto.Password
            ));
        try
        {
            _db.SaveChanges();
        }

        catch(DbUpdateException e)
        {
            return BadRequest();
        }
        
        return Ok();
    }
    

    /// <summary>
    /// GET /api/user/me
    /// Gets information about the current (authenticated) user.
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    public IActionResult GetUserdata()
    {
        var user = HttpContext.User.Identity?.Name;
        return Ok(new
        {
            Username = HttpContext.User.Identity?.Name,
            Group = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Group")?.Value
        });
    }
}
