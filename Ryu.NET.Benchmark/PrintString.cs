using BenchmarkDotNet.Attributes;
using RyuDotNet.UnitTests.s2d_data;
using System;
using System.Globalization;

namespace RyuDotNet.Benchmark
{
    [Config(typeof(RyuBenchMarkConfig))]
    public class PrintString
    {
        static char[] span = new char[100];
        static CultureInfo culture = System.Globalization.CultureInfo.InvariantCulture;
        [Benchmark(Baseline = true)]
        public void CSDoublePrint()
        {
            for (int i = 0; i < SmallD2STestSet.TestArray.Length; ++i)
            {
                string x;
                for (int u = 0; u < 5; ++u) x = SmallD2STestSet.TestArray[i].ToString();
            }
        }

        [Benchmark]
        public void RyuDoublePrint()
        {
            var sp = span.AsSpan();
            for (int i = 0; i < SmallD2STestSet.TestArray.Length; ++i)
            {
                for (int u = 0; u < 5; ++u)
                {
                    int n = RyuDotNet.Internal.Ryu.d2s_buffered_n(SmallD2STestSet.TestArray[i], new Internal.AlphaSpan(span));
                    // var x = new string(sp.Slice(0, n));

                }
            }
        }
    }
}
