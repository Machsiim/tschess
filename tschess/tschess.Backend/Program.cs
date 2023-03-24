using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using Tschess.Application.Infrastructure;
using Tschess.Backend.Hubs;

var builder = WebApplication.CreateBuilder(args);
// JWT Authentication ******************************************************************************
byte[] secret = Convert.FromBase64String(builder.Configuration["JwtSecret"]);
builder.Services
    .AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secret),
            ValidateAudience = false,
            ValidateIssuer = false
        };
        // See https://learn.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-6.0#built-in-jwt-authentication
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Extract Token from WS Request /chessHub?token=......
                var accessToken = context.Request.Query["token"];
                if (string.IsNullOrEmpty(accessToken)) { return Task.CompletedTask; }
                var path = context.HttpContext.Request.Path;
                if (context.HttpContext.Request.Path.StartsWithSegments("/chessHub"))
                    context.Token = accessToken;
                return Task.CompletedTask;
            }
        };
    });
// *************************************************************************************************

builder.Services.AddSignalR();
builder.Services.AddControllers();
//builder.Services.AddAutoMapper(typeof(Tschess.Application.Dto.MappingProfile));
builder.Services.AddDbContext<TschessContext>(opt =>
{
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer"),
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        // Allow vue devserver on port 5173
        options.AddDefaultPolicy(
            builder =>
            {
                builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
                .WithOrigins("http://127.0.0.1:5173", "https://127.0.0.1:5173");
            });
    });
}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
       using (var db = scope.ServiceProvider.GetRequiredService<TschessContext>())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();
        }
    }
    app.UseCors();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChessHub>("/chessHub", options =>
{
    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
});

app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.Run();
