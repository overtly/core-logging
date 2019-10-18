#if !ASP_NET_CORE
namespace Overt.Core.Logging
{
    /// <summary>
    /// log类型
    /// </summary>
    public enum EnumLogType
    {
        /// <summary>
        /// 未知
        /// </summary>
        unknown,
        /// <summary>
        /// Nlog
        /// </summary>
        nlog,
        /// <summary>
        /// log4net
        /// </summary>
        log4net
    }
}
#endif