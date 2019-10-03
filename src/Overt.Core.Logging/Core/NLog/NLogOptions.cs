#if ASP_NET_CORE
using System;
using System.Collections.Generic;
using System.Text;

namespace Overt.Core.Logging
{
    public class NLogOptions
    {
        private Dictionary<string, Func<object>> _renders = new Dictionary<string, Func<object>>();

        public NLogOptions()
        {
            CaptureMessageProperties = true;
            CaptureMessageTemplates = true;

            _renders.Add(Constants.CustomPropertyKey.HostIP, LoggingUtility.GetInternalIp);
            _renders.Add(Constants.CustomPropertyKey.ServiceName, LoggingUtility.GetServiceName);
        }

        /// <summary>
        /// xml config file path
        /// </summary>
        public string ConfigFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether NLog should throw exceptions. By default
        /// exceptions are not thrown under any circumstances.
        /// </summary>
        public bool? ThrowExceptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether NLog should throw exceptions.By default
        /// exceptions are not thrown under any circumstances.
        /// </summary>
        public bool? ThrowConfigExceptions { get; set; }

        /// <summary>
        /// Enable structured logging by capturing message template parameters and inject into the <see cref="LogEventInfo.Properties" />-dictionary
        /// Default is true
        /// </summary>
        public bool CaptureMessageTemplates { get; set; }

        /// <summary>
        /// Enable capture of properties from the ILogger-State-object, both in <see cref="Microsoft.Extensions.Logging.ILogger.Log"/> and <see cref="Microsoft.Extensions.Logging.ILogger.BeginScope"/>
        /// Default is true
        /// </summary>
        public bool CaptureMessageProperties { get; set; }

        /// <summary>
        /// Custom variables
        /// </summary>
        public IReadOnlyDictionary<string, Func<object>> CustomVarialbes
        {
            get { return _renders; }
        }

        /// <summary>
        /// Register custom variable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="func"></param>
        public void RegisterVariable(string name, Func<object> func)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            _renders.Add(name, func);
        }
    }
}
#endif
