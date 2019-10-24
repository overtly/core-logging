#if !ASP_NET_CORE
namespace Overt.Core.Logging
{
    /// <summary>
    /// Log等级
    /// </summary>
    public enum EnumLogLevel
    {
        /// <summary>
        /// Trace
        /// </summary>
        Trace,
        /// <summary>
        /// Debug
        /// </summary>
        Debug,
        /// <summary>
        /// Info
        /// </summary>
        Info,
        /// <summary>
        /// Warn
        /// </summary>
        Warn,
        /// <summary>
        /// Error
        /// </summary>
        Error,
        /// <summary>
        /// Fatal
        /// </summary>
        Fatal,
    }
}
#endif