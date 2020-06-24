using BenchmarkDotNet.Attributes;
using RyuDotNet.UnitTests.s2d_data;
using System;
using System.Globalization;

namespace RyuDotNet.Benchmark
{
    [Config(typeof(RyuBenchMarkConfig))]
    public class ParseString
    {
        static CultureInfo culture = System.Globalization.CultureInfo.InvariantCulture;
        [Benchmark(Baseline = true)]
        public void CSDoubleParse()
        {
            for (int i = 0; i < SmallTestSet.TestArray.Length; ++i)
            {
                double x;
                for (int u = 0; u < 5; ++u) x = double.Parse(SmallTestSet.TestArray[i], culture);

            }
        }

        [Benchmark]
        public void RyuDoubleParse()
        {
            for (int i = 0; i < SmallTestSet.TestArray.Length; ++i)
            {
                double x;
                for (int u = 0; u < 5; ++u) RyuDotNet.Internal.Ryu.s2d_n(SmallTestSet.TestArray[i].AsSpan(), out x);

            }
        }
    }
}
