using Basket.API.Repository;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Access the configuration
// var configuration = new ConfigurationBuilder()
//     .SetBasePath(builder.Environment.ContentRootPath)
//     .AddJsonFile("appsettings.json", optional: true)
//     .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{

    options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
    // options.Configuration = "localhost:6379";

});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
