using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Novell.Directory.Ldap.Utilclass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tschess.Application.Model;

namespace Tschess.Application.Infrastructure
{
    public class TschessContext : DbContext
    {
       
        public DbSet<User> Users => Set<User>();
        public DbSet<Game> Games => Set<Game>();

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
            var users = new User[]
            {
                new User(

                    name: "rom22634", 
                    email: "rom22634@spengergasse.at", 
                    password: "1111"

                    )
            };
            Users.AddRange(users);
            SaveChanges();
        }
        public void Seed()
        {
            Randomizer.Seed = new Random(1000);
            var faker = new Faker("de");

            var users = new Faker<User>("de").CustomInstantiator(f =>
            {
                var lastname = f.Name.LastName();
                return new User(
                    name: f.Name.FirstName(),
                    email: $"{lastname.ToLower()}@spengergasse.at",
                    password: "1111"
                    )


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
