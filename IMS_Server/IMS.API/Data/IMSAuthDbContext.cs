using IMS.API.Models.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IMS.API.Data
{
    public class IMSAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public IMSAuthDbContext(DbContextOptions<IMSAuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var adminRoleId = "50f2e492-9b92-403e-9dcf-d8ac7b6d7100";
            var customerRoleId = "12fcb3da-d86a-4dd0-a5f3-1d448cda0a1f";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    ConcurrencyStamp = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "Customer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

        public DbSet<ApplicationUser> ApplicationUsers;
    }
}
