using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Overt.Core.Logging
{
    static class NLogUtility
    {
        private static readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

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
    }
}
