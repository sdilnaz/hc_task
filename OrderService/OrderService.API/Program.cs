using OrderService.Application.Interfaces;
using OrderService.Application.Services;
using OrderService.Infrastructure.Extensions;
using OrderService.Infrastructure.Persistence;
using OrderService.API.Middlewares;
using OrderService.Application.Mappers;
using OrderService.Core.Interfaces;
using Hangfire;
using OutboxLibrary.Services;
using OutboxLibrary.Repositories;
using OutboxLibrary.Interfaces;
using OrderService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDBContext(builder.Configuration);
builder.Services.AddMassTransitWithRabbitMQ(builder.Configuration);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderProcessingService>();
builder.Services.AddAutoMapper(typeof(OrderMappingProfile));
builder.Services.AddAutoMapper(typeof(OutboxProfile));
builder.Services.AddHangfireServices(builder.Configuration);
builder.Services.AddScoped<IOutboxService, OutboxService>();
builder.Services.AddScoped<IOutboxRepository, OutboxRepository>();
builder.Services.AddScoped<IOutboxDbContext>(provider => provider.GetRequiredService<ApplicationDBContext>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/hangfire");
app.UseBackgroundJobs();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
