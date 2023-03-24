using Bogus;
using Microsoft.EntityFrameworkCore;
using Novell.Directory.Ldap.Utilclass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Tschess.Application.Infrastructure
{
    public class TschessContext : DbContext
    {
       
        public DbSet<AdUser> Users => Set<AdUser>();

        public TschessContext(DbContextOptions<TschessContext> opt) : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Additional config

            // Generic config for all entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // ON DELETE RESTRICT instead of ON DELETE CASCADE
                foreach (var key in entityType.GetForeignKeys())
                    key.DeleteBehavior = DeleteBehavior.Restrict;

                foreach (var prop in entityType.GetDeclaredProperties())
                {
                    // Define Guid as alternate key. The database can create a guid fou you.
                    if (prop.Name == "Guid")
                    {
                        modelBuilder.Entity(entityType.ClrType).HasAlternateKey("Guid");
                        prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                    }
                    // Default MaxLength of string Properties is 255.
                    if (prop.ClrType == typeof(string) && prop.GetMaxLength() is null) prop.SetMaxLength(255);
                    // Seconds with 3 fractional digits.
                    if (prop.ClrType == typeof(DateTime)) prop.SetPrecision(3);
                    if (prop.ClrType == typeof(DateTime?)) prop.SetPrecision(3);
                }
            }
        }

        private void Initialize()
{
            var users = new AdUser[]
            {
                new AdUser(
            firstname: "Maxim",
            lastname : "Romanenko",
            email: "ichhabeka@spengergasse.at",
            cn: "ROM22162",
            dn: "Klasse 3CHIF",
            groupMemberhips: ["Schüler", "Lehrer"],
            pupilId: null,
            teacherId: null

                    )
            };
            Users.AddRange(users);
            SaveChanges();
        }
        public void Seed()
        {
            Randomizer.Seed = new Random(1000);
            var faker = new Faker("de");

            var users = new Faker<AdUser>("de").CustomInstantiator(f =>
            {
                var lastname = f.Name.LastName();
                return new AdUser(
                    firstname: f.Name.FirstName(),
                    lastname: lastname,
                    email: $"{lastname.ToLower()}@spengergasse.at",
                    cn: lastname.ToLower(),
                    dn: "Klasse 3CHIF",
                    groupMemberhips: ["Schüler", "Lehrer"],
                    pupilId: null,
                    teacherId: null)


                { Guid = f.Random.Guid() };
            })
            .Generate(10)
            .GroupBy(a => a.Email).Select(g => g.First())
            .ToList();
            Users.AddRange(users);
            SaveChanges();

        }

        public void CreateDatabase(bool isDevelopment)
        {
            if (isDevelopment) { Database.EnsureDeleted(); }
            // EnsureCreated only creates the model if the database does not exist or it has no
            // tables. Returns true if the schema was created.  Returns false if there are
            // existing tables in the database. This avoids initializing multiple times.
            if (Database.EnsureCreated()) { Initialize(); }
            if (isDevelopment) Seed();
        }

    }

}
