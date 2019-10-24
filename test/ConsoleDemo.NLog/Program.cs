using NLog;
using Overt.Core.Logging;
using System;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.LogInformation("你好啊");
            logger.LogError("你好啊111111");
            Console.ReadLine();
        }
    }
}
