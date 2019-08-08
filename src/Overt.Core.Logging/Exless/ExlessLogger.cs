using Exceptionless;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Overt.Core.Logging
{
    /// <summary>
    /// Logger实现
    /// </summary>
    public class ExlessLogger : ILogger
    {
        private static string _addressIps = string.Empty;
        private readonly string _categoryName;
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="categoryName"></param>
        public ExlessLogger(string categoryName)
        {
            _categoryName = categoryName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                var tags = Environment.GetEnvironmentVariable(Constants.EnvKeys.ExceptionlessTags);
                var message = formatter(state, exception);
                var source = $"{_categoryName}{(eventId != 0 ? ":" + eventId : "")}";
                var eventBuilder = default(EventBuilder);
                if (exception != null)
                {
                    eventBuilder = ExceptionlessClient
                                   .Default
                                   .CreateException(exception)
                                   .SetSource(source)
                                   .SetMessage(message);
                }
                else
                {
                    var exlessLogLevel = Exceptionless.Logging.LogLevel.Trace;
                    switch (logLevel)
                    {
                        case LogLevel.Trace:
                            exlessLogLevel = Exceptionless.Logging.LogLevel.Trace;
                            break;
                        case LogLevel.Information:
                            exlessLogLevel = Exceptionless.Logging.LogLevel.Info;
                            break;
                        case LogLevel.Warning:
                            exlessLogLevel = Exceptionless.Logging.LogLevel.Warn;
                            break;
                        case LogLevel.Error:
                            exlessLogLevel = Exceptionless.Logging.LogLevel.Error;
                            break;
                        case LogLevel.Critical:
                            exlessLogLevel = Exceptionless.Logging.LogLevel.Fatal;
                            break;
                        default:
                            exlessLogLevel = Exceptionless.Logging.LogLevel.Debug;
                            break;
                    }
                    eventBuilder = ExceptionlessClient
                                   .Default
                                   .CreateLog(message, exlessLogLevel)
                                   .SetSource(source)
                                   .SetException(exception);
                }
                if (!string.IsNullOrEmpty(tags))
                    eventBuilder.AddTags(tags.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                eventBuilder.SetProperty("ServerEndPoint", GetAddressIP());
                eventBuilder.Submit();
            }
            catch { }
        }

        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        private string GetAddressIP()
        {
            try
            {
                if (!string.IsNullOrEmpty(_addressIps))
                    return _addressIps;

                var addressIps = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                    .Select(p => p.GetIPProperties())
                    .SelectMany(p => p.UnicastAddresses)
                    .Select(oo => oo.Address.ToString());
                _addressIps = string.Join(", ", addressIps);
            }
            catch (Exception ex)
            {
                _addressIps = $"can't get: {ex.Message}";
            }
            return _addressIps;
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
