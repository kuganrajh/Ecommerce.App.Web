using Shared.Grpc.Contract.V1;
using WarehouseMS.App.Infrastructure.Interface;
using WarehouseMS.App.Service.grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ensure Grpc.Net.ClientFactory is referenced in your project file (.csproj)
// <PackageReference Include="Grpc.Net.ClientFactory" Version="2.56.0" /> or later

builder.Services.AddGrpcClient<PaymentService.PaymentServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["GrpcEndpoints:PaymentService"]
                        ?? "https://localhost:5001");
});

builder.Services.AddScoped<IPaymentChecker, PaymentChecker>();

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
