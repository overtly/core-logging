using Microsoft.Extensions.Logging;

namespace Overt.Core.Logging.Exless
{
    /// <summary>
    /// LoggerProvider
    /// </summary>
    public class ExlessLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new ExlessLogger(categoryName);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }
    }
}
