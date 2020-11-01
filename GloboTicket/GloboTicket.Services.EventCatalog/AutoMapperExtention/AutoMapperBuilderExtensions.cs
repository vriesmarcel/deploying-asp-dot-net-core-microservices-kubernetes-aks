using System;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapperBuilder.Extensions.DependencyInjection
{
    public static class AutoMapperBuilderExtensions
    {
        /// <summary>
        /// Adds AutoMapperBuilder to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        public static IServiceCollection AddAutoMapperBuilder(this IServiceCollection services, Action<AutoMapperBuilderConfiguration> builder)
        {
            //Configure IOptions<AutoMapperBuilderConfiguration>
            services.Configure(builder);

            //Register IAutoMapperBuilder in the service container
            services.AddSingleton<IAutoMapperBuilder, AutoMapperBuilder>();

            //Build IMapper and add to the service container
            services.AddSingleton(services.BuildServiceProvider().GetRequiredService<IAutoMapperBuilder>().Build());

            return services;
        }
    }
}