using EcoMeal.Components;
using EcoMeal.Controllers;
using EcoMeal.Database;
using EcoMeal.Repositories;
using EcoMeal.Repositories.Interfaces;
using EcoMeal.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EcoMealDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderEntryRepository>();
builder.Services.AddScoped<OrderStatusRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<BusinessRepository>();
builder.Services.AddScoped<PackageRepository>();
builder.Services.AddScoped<PackageTypeRepository>();
builder.Services.AddScoped<BusinessTypeRepository>();
builder.Services.AddScoped<BusinessTypeService>();
builder.Services.AddScoped<UtilityService>();

builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IBusinessService, BusinessService>();

builder.Services.AddScoped<IBusinessTypeRepository, BusinessTypeRepository>();
builder.Services.AddScoped<IBusinessTypeService, BusinessTypeService>();

builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IPackageService, PackageService>();

builder.Services.AddScoped<IPackageTypeRepository,  PackageTypeRepository>();

builder.Services.AddControllers();
builder.Services.AddScoped<BusinessController>();
builder.Services.AddScoped<PackageController>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
