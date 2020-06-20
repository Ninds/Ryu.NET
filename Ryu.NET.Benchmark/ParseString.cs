using BenchmarkDotNet.Attributes;
using Ryu.Net.UnitTests.s2d_data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ryu.NET.Benchmark
{
    [Config(typeof(RyuBenchMarkConfig))]
    public class ParseString
    {

        [Benchmark(Baseline = true)]
        public void CSDoubleParse()
        {
            for (int i = 0; i < SmallTestSet.TestArray.Length; ++i)
            {
                double x = double.Parse(SmallTestSet.TestArray[i]);
                x = x + 1.0;
            }
        }

        [Benchmark]
        unsafe public void RyuDoubleParse()
        {
            for (int i = 0; i < SmallTestSet.TestArray.Length; ++i)
            {
                double x;
                var str = SmallTestSet.TestArray[i];
                var span = str.AsSpan();
                fixed (char* buffStart = &span[0])
                {
                    var eq1 = Ryu.Net.Internal.Ryu.s2d_n(buffStart, str.Length, &x);
                }
                x = x + 1.0;
            }
        }
    }
}
