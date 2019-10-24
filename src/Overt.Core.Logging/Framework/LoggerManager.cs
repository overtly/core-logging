#if !ASP_NET_CORE
using NLog;
using NLog.Config;
using System;
using System.IO;

namespace Overt.Core.Logging
{
    public class LoggerManager
    {
        public static void Configure(string configFile = "")
        {
            if (string.IsNullOrEmpty(configFile))
            {
                var baseDirectory = LoggingUtility.GetBaseDirectory();
                configFile = Path.Combine(baseDirectory, $"dllconfigs\\Overt.Core.Logging.dll.config");
                if (!File.Exists(configFile))
                    throw new Exception($"not exit the config file [{baseDirectory}]");
            }

            LogManager.Configuration = new XmlLoggingConfiguration(configFile);
        }
    }
}
#endif