using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }

        //Type override and hover over OnModelCreating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //These are some manualy derived Guids for role access using C# Interactive, Guid.NewGuid()
            var readerRoleId = "9bebba3a-070c-4c10-97bb-e0a25437a3d2";
            var writerRoleId = "a14597d6-534b-4154-bcee-f1f727f3100b";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
            {
              Id = readerRoleId,
              ConcurrencyStamp = readerRoleId,
              Name = "Reader",
              NormalizedName = "Reader".ToUpper()
            },
                new IdentityRole
                {
               Id = writerRoleId,
               ConcurrencyStamp = writerRoleId,
               Name = "Writer",
               NormalizedName = "Writer".ToUpper()
            }
        };

            //Seeding some data
            //We are injecting the two roles below.
            builder.Entity<IdentityRole>().HasData(roles);

        }


    }
}

//Comparison: This Auth DB vs DV
//This uses IdentityDbContext rather than DbContext
//We generated a contsructor ctrl + . with options parameter
//As we did for the non auth DB, we set its type
//We are seeding again with nuget package manager console, which I like access via search 
// Two Commands
// Add-Migration "Creating Auth Database" --> WILL FAIL, neext --context
// Add-Migration "Creating Auth Database" -Context "NZWalksAuthDbContext"
//Next Command
//Update-Database -Context "NZWalksAuthDbContext"
//In SQL success
//Set up in Program.cs, Setting up Identity, inject in solution
