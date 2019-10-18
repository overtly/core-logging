#if !ASP_NET_CORE
namespace Overt.Core.Logging
{
    /// <summary>
    /// 扩展属性
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            get
            {
                return LoggingUtility.GetInternalIp();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg"></param>
        public LogMessage(string msg)
        {
            Message = msg;
        }
    }
}
#endif