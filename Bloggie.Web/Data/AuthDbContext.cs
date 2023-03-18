using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles (user,admin,super admin)

            var adminRoleId = "290daef8-99eb-453d-b662-2e46e8b0990d";
            var superAdminRoleId = "4e647abb-820e-4a97-8820-e878c25544a3";
            var userRoleId = "1d38d25c-75bb-4293-b000-17212e4868ef";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id= adminRoleId,
                      ConcurrencyStamp = adminRoleId

                },

                 new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId

                },

                  new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id=userRoleId
                    ConcurrencyStamp = userRoleId,

                },
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed Super Admin User

            var superAdminId = "af5fae33-eb3c-483e-b82c-4c6f01c8c835";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all rokes to Super Admin

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                 new IdentityUserRole<string>
                 {
                     RoleId= adminRoleId,
                      UserId= superAdminId,
                 },

                 new IdentityUserRole<string>
                 {
                     RoleId= superAdminRoleId,
                      UserId= superAdminId,
                 },

                   new IdentityUserRole<string>
                 {
                     RoleId= userRoleId,
                      UserId= superAdminId,
                 }
            };
        }
    }
}
