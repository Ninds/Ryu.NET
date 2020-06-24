using BenchmarkDotNet.Running;
using System;
using System.Reflection;

namespace RyuDotNet.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);
                var debugInstance = new ParseAsciiString();
                //debugInstance.GlobalSetup();
                //debugInstance.CSDoubleParse();
                //debugInstance.RyuDoubleParse();

                //var debugInstance = new PrintStringAsciii();
                //debugInstance.RyuDoublePrintAscii();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
