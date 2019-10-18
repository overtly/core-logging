#if !ASP_NET_CORE
using NLog;
using NLog.Config;
using System;
using System.IO;

namespace Overt.Core.Logging
{
    /// <summary>
    /// base nlog
    /// </summary>
    public class NLogger : ILogger
    {
        #region Inner
        /// <summary>
        /// Logger
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static NLogger()
        {
            var baseDirectory = LoggingUtility.GetBaseDirectory();
            var filePath = Path.Combine(baseDirectory, $"dllconfigs\\Overt.Core.Logging.nlog.dll.config");
            if (!File.Exists(filePath))
                throw new Exception($"not exit the config file [{baseDirectory}]");

            LogManager.Configuration = new XmlLoggingConfiguration(filePath);
        }
        #endregion

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="level"></param>
        public void Write(string msg, Exception ex = null, EnumLogLevel level = EnumLogLevel.Debug)
        {
            var logMsg = new LogMessage(msg);
            var logLevel = LogLevel.Info;
            switch (level)
            {
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

            var ei = new LogEventInfo(logLevel, Logger.Name, logMsg.Message)
            {
                Exception = ex
            };
            ei.Properties[Constants.CustomPropertyKey.HostIP] = logMsg.IP;
            Logger.Log(ei);
        }

        /// <summary>
        /// Debug 调试 一般用于console
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Debug(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Debug);
        }

        /// <summary>
        /// Error 错误Bug 一般用于 Bug的通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Error(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Error);
        }

        /// <summary>
        /// Fatal 严重Bug 最好直接通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Fatal(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Fatal);
        }

        /// <summary>
        /// Info 信息 一般用于一些信息打印
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Info(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Info);
        }

        /// <summary>
        /// Warn 警告 一般用于非紧急Bug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Warn(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Warn);
        }
    }
}
#endif