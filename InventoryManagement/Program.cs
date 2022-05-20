using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<DataContext>(options =>
//                options.UseSqlite(
//                        builder.Configuration.GetConnectionString("DbConn")));
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Inmemory"));

builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IInventoryRepo, InventoryRepo>();   
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
