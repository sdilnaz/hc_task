using OrderService.Application.Interfaces;
using OrderService.Application.Services;
using OrderService.Infrastructure.Extensions;
using OrderService.Infrastructure.Presistence;
using OrderService.API.Middlewares;
using OrderService.Application.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDBContext(builder.Configuration);
builder.Services.AddMassTransitWithRabbitMQ(builder.Configuration);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderProcessingService>();
builder.Services.AddAutoMapper(typeof(OrderMappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
