using Core.InterfacesRepositories;
using Core.InterfacesServices;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
       .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<INutritionService, NutritionService>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddSingleton<Random>();
builder.Services.AddScoped<IMenuGeneratorService, MenuGeneratorService>();
builder.Services.AddScoped<IDailyLogRepository, DailyLogRepository>();
builder.Services.AddScoped<IDailyLogService, DailyLogService>();
builder.Services.AddScoped<IReportService, ReportService>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
                   .AllowCredentials()
                   .WithOrigins("https://localhost:4200", "http://localhost:4200"));


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}

app.Run();
