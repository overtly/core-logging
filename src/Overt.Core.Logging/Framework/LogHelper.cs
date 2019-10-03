#if !ASP_NET_CORE
using System;
using System.IO;

namespace Overt.Core.Logging
{
    /// <summary>
    /// LogHelper
    /// Default log4net
    /// </summary>
    public class LogHelper
    {
        static readonly EnumLogType logType = EnumLogType.unknown;
        static LogHelper()
        {
            var baseDirectory = LoggingUtility.GetBaseDirectory();
            var nlogFilePath = Path.Combine(baseDirectory, "nlog.dll");

            if (File.Exists(nlogFilePath))
            {
                logType = EnumLogType.nlog;
                return;
            }

            throw new Exception("未应用Nlog组件");
        }

        /// <summary>
        /// Write default info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="level"></param>
        public static void Write(string msg, Exception ex = null, EnumLogLevel level = EnumLogLevel.Info)
        {
            switch (logType)
            {
                case EnumLogType.nlog:
                    LogHelper<NLogger>.Instance.Write(msg, ex, level);
                    break;
                default:
                    throw new InvalidOperationException("未引用任何Log组件");
            }
        }

        /// <summary>
        /// LogDebug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Debug);
        }

        /// <summary>
        /// LogInfo
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Info(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Info);
        }

        /// <summary>
        /// LogWarn
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Warn(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Warn);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Error);
        }

        /// <summary>
        /// LogFatal
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Fatal(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Fatal);
        }
    }

    /// <summary>
    /// LogHelper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LogHelper<T> where T : class, ILogger, new()
    {
        private volatile static T _instance = default(T);
        private static readonly object lockHelper = new object();
        /// <summary>
        /// 单例实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockHelper)
                    {
                        try
                        {
                            _instance = _instance ?? new T();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"{ex.InnerException?.InnerException?.Message}");
                        }
                    }
                }
                return _instance;
            }
        }


        /// <summary>
        /// Write default info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="level"></param>
        public static void Write(string msg, Exception ex = null, EnumLogLevel level = EnumLogLevel.Info)
        {
            Instance.Write(msg, ex, level);
        }

        /// <summary>
        /// LogDebug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Debug);
        }

        /// <summary>
        /// LogInfo
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Info(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Info);
        }

        /// <summary>
        /// LogWarn
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Warn(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Warn);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Error);
        }

        /// <summary>
        /// LogFatal
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Fatal(string msg, Exception ex = null)
        {
            Write(msg, ex, EnumLogLevel.Fatal);
        }
    }
}
#endif