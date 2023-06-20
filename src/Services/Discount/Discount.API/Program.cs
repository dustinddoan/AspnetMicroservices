using Discount.API.Extensions;
using Discount.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var configuration = builder.Configuration;
    var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    var logger = loggerFactory.CreateLogger<PostgresMigration>();

    using var scope = builder.Services.BuildServiceProvider().CreateScope();
    var migration = new PostgresMigration(configuration, logger);

    if (!migration.TableExists("coupon", migration.GetConnection()))
    {
        migration.MigrateDatabase();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
