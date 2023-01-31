using Billing.Api.Infrastructure;
using Billing.Api.Services.DocumentGenerationService;
using Billing.Api.Services.OrderService;
using Billing.Api.Services.PaymentService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BillingDbContext>(
    options => {
        options.UseInMemoryDatabase("BillingDb");
    }
);

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDocumentGenerationService, DocumentGenerationService>();

builder.Services.AddScoped<IPaymentService, AmazonPayService>();
builder.Services.AddScoped<IPaymentService, AdyenService>();
builder.Services.AddScoped<IPaymentService, KlarnaService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    // TODO finish security configuration
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
});

builder.Services.AddAuthentication().AddJwtBearer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

