using Microsoft.EntityFrameworkCore;
using Product.App.BLL;
using Product.App.Data.Context;
using Product.App.Data.Repositories;
using Product.App.Domain;
using Product.App.Infrastructure.Interface;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Read settings from appsettings.json
    .Enrich.FromLogContext()                       // Add contextual information to logs
    .WriteTo.Console()                             // Write logs to the console
    .CreateLogger();

// Use Serilog as the logging provider
builder.Host.UseSerilog(Log.Logger);

// Set the JSON serializer to handle circular references by ignoring cycles
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

    });

//DI

builder.Services.AddDbContext<ProductDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLProductDBConnection")));


// Register services and repositories for Dependency Injection
builder.Services.AddScoped<IRepository<ProductItem>, ProductRepository>();
builder.Services.AddScoped<IService<ProductItem>, ProductService>();


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
