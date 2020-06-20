using BenchmarkDotNet.Running;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Ryu.NET.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                  BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);
                //var debugInstance = new ParseAsciiString();
                //debugInstance.GlobalSetup();
                //debugInstance.CSDoubleParse();
                //debugInstance.RyuDoubleParse();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
