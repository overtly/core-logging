using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Overt.Core.Logging.Exless
{
    public static class ExlessLoggerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggingBuilder"></param>
        /// <param name="configFile"></param>
        public static void AddExlessLogging(this IServiceProvider provider)
        {
            var configuration = provider.GetService<IConfiguration>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            var client = ExceptionlessClient.Default;
            client.Configuration.ReadFromConfiguration(configuration);
            client.Configuration.ReadFromEnvironmentalVariables();
            client.Configuration.UseInMemoryStorage();
            client.Startup();

            loggerFactory.AddProvider(new ExlessLoggerProvider());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggingBuilder"></param>
        /// <param name="configFile"></param>
        public static void AddExlessLogging(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices;
            var configuration = provider.GetService<IConfiguration>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            app.UseExceptionless(configuration);

            var client = ExceptionlessClient.Default;
            client.Configuration.UseInMemoryStorage();

            loggerFactory.AddProvider(new ExlessLoggerProvider());
        }
    }
}
