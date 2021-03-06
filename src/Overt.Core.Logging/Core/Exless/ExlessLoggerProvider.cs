﻿#if ASP_NET_CORE
using Microsoft.Extensions.Logging;

namespace Overt.Core.Logging
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
#endif
