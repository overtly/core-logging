using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Overt.Core.Logging;
using System;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        #region NLog
        /// <summary>
        /// 对AddNLog的一个简单封装，注册NLog，并设置配置文件，默认dllconfigs/Overt.Core.Logging.dll.config
        /// </summary>
        /// <param name="loggingBuilder"></param>
        /// <param name="configFile"></param>
        public static ILoggingBuilder AddNLogLogging(this ILoggingBuilder loggingBuilder, Action<NLogOptions> config = null)
        {
            ConfigureExtensions.AddNLog(loggingBuilder);

            var options = new NLogOptions();
            config?.Invoke(options);

            if (string.IsNullOrEmpty(options.ConfigFile))
                options.ConfigFile = Path.Combine(AppContext.BaseDirectory, "dllconfigs/Overt.Core.Logging.dll.config");

            if (!File.Exists(options.ConfigFile))
                throw new FileNotFoundException(options.ConfigFile);

            if (options.ThrowExceptions != null)
                NLog.LogManager.ThrowExceptions = options.ThrowExceptions.Value;
            if (options.ThrowConfigExceptions != null)
                NLog.LogManager.ThrowConfigExceptions = options.ThrowConfigExceptions.Value;

            if (options.CustomVarialbes.Count > 0)
            {
                foreach (var item in options.CustomVarialbes)
                {
                    NLog.LayoutRenderers.LayoutRenderer.Register(item.Key, le => NLogUtility.GetCachedValue(item.Key, item.Value));
                }
            }

            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(options.ConfigFile, false);

            return loggingBuilder;
        }

        /// <summary>
        /// services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddNLogLogging(this IServiceCollection services, Action<NLogOptions> config = null)
        {
            services.AddLogging(builder => builder.AddNLogLogging());
            return services;
        }
        #endregion

        #region ExLess
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
        #endregion
    }
}
