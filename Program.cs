using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using BackendAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Genshin Food API",
        Version = "v1"
    });
});

builder.Services.AddScoped<IFoodService, FoodService>();

builder.Services.AddHttpClient("genshin", configureClient: client =>
{
    client.BaseAddress = new Uri("https://api.genshin.dev/");
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<FoodDb>(options => options.UseInMemoryDatabase("FoodList"));
} else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<FoodDb>(options => options.UseSqlite(builder.Configuration["GenshinAPIConnection"]));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Genshin Food API Development");
    });
} else if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Genshin Food API Production");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
