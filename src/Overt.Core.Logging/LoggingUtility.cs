using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Overt.Core.Logging
{
    static class LoggingUtility
    {
        private static readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();
        private static string _addressIps = string.Empty;

        public static string GetInternalIp()
        {
            var ips = new List<string>();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    IPInterfaceProperties ipxx = adapter.GetIPProperties();
                    UnicastIPAddressInformationCollection ipCollection = ipxx.UnicastAddresses;
                    foreach (UnicastIPAddressInformation ipadd in ipCollection)
                    {
                        if (ipadd.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ips.Add(ipadd.Address.ToString());
                        }
                    }
                }
            }

            var result = ips.FirstOrDefault(o => o.StartsWith("10."))
                ?? ips.FirstOrDefault(o => o.StartsWith("172."))
                ?? ips.FirstOrDefault(o => o.StartsWith("192."))
                ?? ips.FirstOrDefault();
            return result;
        }

        public static string GetServiceName()
        {
            var result = Assembly.GetEntryAssembly().GetName().Name.ToLower().Replace(".", "_");
            return result;
        }

        public static object GetCachedValue(string name, Func<object> func)
        {
            return _cache.GetOrAdd(name, key => func());
        }

        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public static string GetAddressIP()
        {
            try
            {
                if (!string.IsNullOrEmpty(_addressIps))
                    return _addressIps;

                var addressIps = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                    .Select(p => p.GetIPProperties())
                    .SelectMany(p => p.UnicastAddresses)
                    .Select(oo => oo.Address.ToString());
                _addressIps = string.Join(", ", addressIps);
            }
            catch (Exception ex)
            {
                _addressIps = $"can't get: {ex.Message}";
            }
            return _addressIps;
        }
    }
}
