#if !ASP_NET_CORE
using System;

namespace Overt.Core.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="level"></param>
        void Write(string msg, Exception ex = null, EnumLogLevel level = EnumLogLevel.Debug);

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Debug(string msg, Exception ex = null);

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Info(string msg, Exception ex = null);

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Warn(string msg, Exception ex = null);

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Error(string msg, Exception ex = null);

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Fatal(string msg, Exception ex = null);
    }
}
#endif