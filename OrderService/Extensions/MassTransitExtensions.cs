using MassTransit;
using OrderService.Configurations;

namespace OrderService.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddMassTransitWithRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitConfiguration = configuration.GetSection("RabbitConfiguration").Get<RabbitConfiguration>();

            if (rabbitConfiguration == null)
            {
                throw new InvalidOperationException("RabbitConfiguration section is not configured properly.");
            }


            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitConfiguration.Host, rabbitConfiguration.VirtualHost, h =>
                    {
                        h.Username(rabbitConfiguration.Username);
                        h.Password(rabbitConfiguration.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
