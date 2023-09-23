using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Initialize
{
    public static class ModelBuilderExtensions
    {
        public static void CreateAdminUser(this ModelBuilder modelBuilder)
        {
            List<IdentityRole> roleList = new List<IdentityRole>()
            {
                new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole{ Name = "Member", NormalizedName = "MEMBER"}
            };

            modelBuilder.Entity<IdentityRole>().HasData(roleList);

            var identityUser = new IdentityUser
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GIANGNT75.COM.VN"
            };

            var identityUsers = new List<IdentityUser>() { identityUser };
            modelBuilder.Entity<IdentityUser>().HasData(identityUsers);
            var passwordhHasher = new PasswordHasher<IdentityUser>();
            identityUser.PasswordHash = passwordhHasher.HashPassword(identityUser, "admin");

            var adminRole = new IdentityUserRole<string>
            {
                UserId = identityUser.Id,
                RoleId = roleList.First(r => r.Name == "Admin").Id
            };

            var userRoles = new List<IdentityUserRole<string>>() { adminRole };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}
