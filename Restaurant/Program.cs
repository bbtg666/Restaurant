using Data.AutoMapper;
using Data.DBContext;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Helper.DI;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default") ?? string.Empty;

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration).Assembly);

builder.Services.RegisterDbContext(connectionString);

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<AccountDBContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "UserIdentity";
    options.AccessDeniedPath = "/Home/AccessDenied";
    options.LogoutPath = "/Account/Logout";
    options.LoginPath = "/Account/Login";
    options.ExpireTimeSpan = new TimeSpan(0, 15, 0);
    options.SlidingExpiration = true;
});

builder.Services.RegisterServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapAreaControllerRoute(
            name: "Admin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
