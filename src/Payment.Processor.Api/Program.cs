using Payment.Processor.Api.Services;
using Payment.Processor.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Web API setup
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register service
builder.Services.AddScoped<IPaymentProcessorService, PaymentProcessorService>();

var app = builder.Build();

// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Global exception handling
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }
