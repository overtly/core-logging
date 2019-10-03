using Overt.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.Error("你好啊");
            LogHelper.Error("你好啊111111");
            Console.ReadLine();
        }
    }
}
