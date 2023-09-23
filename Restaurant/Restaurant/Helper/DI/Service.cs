using Business.Account;
using Business.Restaurant;
using Core.Helper;
using Data.Services;
using Data.UnitOfWorks;

namespace Restaurant.Helper.DI
{
    public static class Service
    {
        public static void RegisterServices(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            // Business
            service.AddScoped<IMealBusiness, MealBusiness>();
            service.AddScoped<IAccountBusiness, AccountBusiness>();
            service.AddScoped<IRoleBusiness, RoleBusiness>();
            service.AddScoped<ICartBusiness, CartBusiness>();

            // Service
            service.AddScoped<IMealService, MealService>();
            service.AddScoped<IUserService, UserService>(); 
            service.AddScoped<IOrderService, OrderService>();

            // Helper
            service.AddScoped<ISessionHelper, SessionHelper>(); 
        }
    }
}
