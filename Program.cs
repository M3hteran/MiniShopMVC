using Microsoft.EntityFrameworkCore;
using MiniShopMVC.Data;
using MiniShopMVC.Repositories.Interfaces;
using MiniShopMVC.Repositories.Concrete;
using MiniShopMVC.Services.Interfaces;
using MiniShopMVC.Services.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(option =>
{
	option.LoginPath = "/Account/Login";
	option.AccessDeniedPath = "/Account/AccessDenied";
	option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});
builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IAppUserService, AppUserService>();

builder.Services.AddScoped<ICartItemService, CartItemService>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IOrderItemServices, OrderItemService>();

builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
