using BenchmarkDotNet.Running;
using System;
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
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
