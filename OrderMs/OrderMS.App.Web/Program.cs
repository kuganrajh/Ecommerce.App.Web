using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using OrderMS.App.BLL;
using OrderMS.App.Data.Context;
using OrderMS.App.Data.Repository;
using OrderMS.App.Domain;
using OrderMS.App.Infrastructure.Interface;
using OrderMS.App.Web.AutoMapper;
using OrderMS.App.Web.GraphQL.Mutation;
using OrderMS.App.Web.GraphQL.Query;
using OrderMS.App.Web.QueueHandler;
using Serilog;
using HotChocolate.Data;
using OrderMS.App.Web.GraphQL.Types;
using Azure.Messaging.ServiceBus;
using OrderMS.App.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Read settings from appsettings.json
    .Enrich.FromLogContext()                       // Add contextual information to logs
    .WriteTo.Console()                             // Write logs to the console
    .CreateLogger();

// Use Serilog as the logging provider
builder.Host.UseSerilog(Log.Logger);


//builder.Services.AddDbContext<OrderDbContext>(options =>
//    options.UseCosmos(
//        builder.Configuration["CosmosDbConnection:AccountEndpoint"],
//        builder.Configuration["CosmosDbConnection:AccountKey"],
//        builder.Configuration["CosmosDbConnection:DatabaseName"]
//    ));

builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .AddMutationType<OrderMutation>()
    .AddType<OrderType>()
    .AddType<OrderItemType>()
    .AddFiltering();

// Register services and repositories for Dependency Injection
builder.Services.AddScoped<IService<Order>, OrderService>();
builder.Services.AddScoped<IService<ProductItem>, ProductItemService>();
builder.Services.AddSingleton<IRepository<Order>, InMemoryOrderRepository>();
builder.Services.AddSingleton<IRepository<ProductItem>, InMemoryProductItemRepository>();

// need to enable once we enable the cosmos connection 
//builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
//builder.Services.AddScoped<IRepository<ProductItem>, ProductItemRepository>();


builder.Services.AddSingleton<ServiceBusClient>(sp =>
    new ServiceBusClient(builder.Configuration["AzureServiceBus:ConnectionString"])
);
builder.Services.AddSingleton<OrderMS.App.Infrastructure.Interface.IEventBus, AzureEventBus>();


// Register EasyNetQ IBus as a singleton
builder.Services.AddSingleton<IBus>(sp =>
    RabbitHutch.CreateBus(builder.Configuration.GetConnectionString("RabbitMQConnection")));

// Register AutoMapper and scan for profiles automatically
builder.Services.AddAutoMapper(typeof(ProductProfile));

builder.Services.AddHostedService<ProductUpdatedMessageHandlerService>();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
//    await dbContext.Database.EnsureCreatedAsync();  // This will create the database & container if they don't exist    
//}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapGraphQL();
});

app.UseHttpsRedirection();
 

await app.RunAsync();
