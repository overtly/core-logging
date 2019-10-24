#if !ASP_NET_CORE
using NLog;
using System;

namespace Overt.Core.Logging
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Debug 调试 一般用于console
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void LogTrace(this Logger logger, string msg, Exception ex = null)
        {
            logger.LogWrite(msg, ex, EnumLogLevel.Trace);
        }

        /// <summary>
        /// Debug 调试 一般用于console
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void LogDebug(this Logger logger, string msg, Exception ex = null)
        {
            logger.LogWrite(msg, ex, EnumLogLevel.Debug);
        }

        /// <summary>
        /// Info 信息 一般用于一些信息打印
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void LogInformation(this Logger logger, string msg, Exception ex = null)
        {
            logger.LogWrite(msg, ex, EnumLogLevel.Info);
        }

        /// <summary>
        /// Warn 警告 一般用于非紧急Bug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void LogWarn(this Logger logger, string msg, Exception ex = null)
        {
            logger.LogWrite(msg, ex, EnumLogLevel.Warn);
        }

        /// <summary>
        /// Error 错误Bug 一般用于 Bug的通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void LogError(this Logger logger, string msg, Exception ex = null)
        {
            logger.LogWrite(msg, ex, EnumLogLevel.Error);
        }

        /// <summary>
        /// Fatal 严重Bug 最好直接通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void LogFatal(this Logger logger, string msg, Exception ex = null)
        {
            logger.LogWrite(msg, ex, EnumLogLevel.Fatal);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="level"></param>
        private static void LogWrite(this Logger logger, string msg, Exception ex = null, EnumLogLevel level = EnumLogLevel.Debug)
        {
            if (LogManager.Configuration == null)
                LoggerManager.Configure();

            var logMsg = new LogMessage(msg);
            var logLevel = LogLevel.Info;
            switch (level)
            {
                case EnumLogLevel.Trace:
                    logLevel = LogLevel.Trace;
                    break;
                case EnumLogLevel.Debug:
                    logLevel = LogLevel.Debug;
                    break;
                case EnumLogLevel.Info:
                    logLevel = LogLevel.Info;
                    break;
                case EnumLogLevel.Warn:
                    logLevel = LogLevel.Warn;
                    break;
                case EnumLogLevel.Error:
                    logLevel = LogLevel.Error;
                    break;
                case EnumLogLevel.Fatal:
                    logLevel = LogLevel.Fatal;
                    break;
                default:
                    throw new Exception($"not exist this level: [{level.ToString()}]");
            }

            var ei = new LogEventInfo(logLevel, logger.Name, logMsg.Message)
            {
                Exception = ex
            };
            ei.Properties[Constants.CustomPropertyKey.HostIP] = logMsg.IP;
            logger.Log(ei);
        }
    }
}
#endif