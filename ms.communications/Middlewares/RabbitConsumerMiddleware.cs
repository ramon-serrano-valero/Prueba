using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ms.communications.Consumers;

namespace ms.communications.Middlewares
{
    public static class RabbitConsumerMiddleware
    {
        private static IRabbitMqConsumer _consumer { get; set; }

        public static IApplicationBuilder UseRabbitConsumer(this IApplicationBuilder app, IRabbitMqConsumer consumer)
        {
            _consumer = consumer;

            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            //IHostApplicationLifetime lifetime = app.ApplicationServices.GetService(typeof(IHostApplicationLifetime))as IHostApplicationLifetime;
            lifetime.ApplicationStarted.Register(OnStarted);

            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted() => _consumer.Subscribe();


        private static void OnStopping() => _consumer.Unsubscribe();
    }
}
