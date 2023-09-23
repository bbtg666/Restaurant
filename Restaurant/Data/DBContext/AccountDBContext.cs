using Data.Entities;
using Data.Initialize;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DBContext
{
    public class AccountDBContext : IdentityDbContext<IdentityUser>
    {
        public AccountDBContext(DbContextOptions<AccountDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.CreateAdminUser();
            base.OnModelCreating(builder);
        }
    }
}
