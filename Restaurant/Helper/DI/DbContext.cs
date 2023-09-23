using Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Helper.DI
{
    public static class DbContext
    {
        public static void RegisterDbContext(this IServiceCollection service, string connectionString)
        {
            var options = (DbContextOptionsBuilder options) =>
            {
                options.UseSqlServer(connectionString);
            };

            service.AddDbContext<RestaurantDBContext>(options);
            service.AddDbContext<AccountDBContext>(options);

        }
    }
}
