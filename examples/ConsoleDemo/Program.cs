using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new HostBuilder()
                .UseConsoleLifetime() //使用控制台生命周期
                .ConfigureHostConfiguration(configurationBinder =>
                {
                    configurationBinder
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    var env = context.HostingEnvironment;

                    configurationBuilder
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureLogging(ConfigureLogging) //注入日志组件
                .ConfigureServices(ConfigureServices)//提供通用注入配置
                .Build();

            host.Services.AddExlessLogging();
            host.Run();
        }

        /// <summary>
        /// 添加ILoggerProvider
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loggingBuilder"></param>
        private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
#if DEBUG
            loggingBuilder.AddDebug();
            loggingBuilder.AddConsole();
#endif
            loggingBuilder.AddNLogLogging();
        }

        /// <summary>
        /// 通用DI注入
        /// </summary>
        /// <param name="context"></param>
        /// <param name="services"></param>
        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<IHostedService, HostedService>();
        }
    }
}
