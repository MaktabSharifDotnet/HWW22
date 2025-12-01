using App.Domain.AppServices.CategoryAgg;
using App.Domain.AppServices.ProductAgg;
using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Contract.ProductAgg.AppService;
using App.Domain.Core.Contract.ProductAgg.Repository;
using App.Domain.Core.Contract.ProductAgg.Service;
using App.Domain.Services.CategoryAgg;
using App.Domain.Services.ProductAgg;
using App.Infra.Data.Repos.Ef.CategoryAgg;
using App.Infra.Data.Repos.Ef.ProductAgg;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer("Server=DESKTOP-M2BLLND\\SQLEXPRESS;Database=HWW22;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductAppService, ProductAppService>();


builder.Services.AddScoped<ICategoryRepository , CategoryRepository>();
builder.Services.AddScoped<ICategoryService , CategoryService>();
builder.Services.AddScoped<ICategoryAppService , CategoryAppService>();

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
