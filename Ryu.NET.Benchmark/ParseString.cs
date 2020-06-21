﻿using BenchmarkDotNet.Attributes;
using Ryu.Net.UnitTests.s2d_data;
using System;
using System.Globalization;

namespace Ryu.NET.Benchmark
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
                double x = double.Parse(SmallTestSet.TestArray[i],culture);
                x = x + 1.0;
            }
        }

        [Benchmark]
        unsafe public void RyuDoubleParse()
        {
            for (int i = 0; i < SmallTestSet.TestArray.Length; ++i)
            {
                double x;
                Ryu.Net.Internal.Ryu.s2d_n(SmallTestSet.TestArray[i].AsSpan(), out x);
                x = x + 1.0;
            }
        }
    }
}
