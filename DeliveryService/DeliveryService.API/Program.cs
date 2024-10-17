using DeliveryService.API.Extensions;
using DeliveryService.API.Middlewares;
using DeliveryService.Application.Handlers;
using DeliveryService.Application.Mappers;
using DeliveryService.Core.Interfaces;
using DeliveryService.Infrastructure.Extensions;
using DeliveryService.Infrastructure.Presistence;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDBContext(builder.Configuration);
builder.Services.AddMassTransitWithRabbitMQ(builder.Configuration);
builder.Services.AddMediatR(typeof(GetAllDeliveryRequestsQueryHandler).Assembly);
builder.Services.AddScoped<IDeliveryRequestRepository, DeliveryRequestRepository>();
builder.Services.AddScoped<IDeliveryCreateRepository, DeliveryCreateRepository>();
builder.Services.AddAutoMapper(typeof(DeliveryRequestProfile));


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
