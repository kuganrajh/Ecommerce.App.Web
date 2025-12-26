using AccountMS.App.Payment.grpc;

var builder = WebApplication.CreateBuilder(args);

// Add gRPC services
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<PaymentGrpcService>();
app.MapGet("/", () => "gRPC server for AccountMS Payment is running.");

app.Run();
