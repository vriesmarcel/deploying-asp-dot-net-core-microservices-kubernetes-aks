using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Services.Order.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GloboTicket.Services.Order.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static AzServiceBusConsumer Consumer { get; set; }

        public static IApplicationBuilder UseAzServiceBusConsumer(this IApplicationBuilder app)
        {
            Consumer = app.ApplicationServices.GetService<AzServiceBusConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            Consumer.Start();
        }

        private static void OnStopping()
        {
            Consumer.Stop();
        }
    }
}
