using Data.Entities;
using Data.Initialize;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.DBContext
{
    public class RestaurantDBContext : DbContext
    {

        public RestaurantDBContext(DbContextOptions<RestaurantDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        DbSet<Meal> Meal{ get; set; }
        DbSet<MealCategory> MealCategory { get; set; }        
        DbSet<Table> Table { get; set; }
        DbSet<OrderMeal> OrderMeal { get; set; }
        DbSet<OrderTable> OrderTable { get; set; }
        DbSet<UserMealOrder> UserMealOrder { get; set; }
        DbSet<UserTableOrder> UserTableOrder { get; set; }
    }
}
