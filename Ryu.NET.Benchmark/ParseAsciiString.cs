using BenchmarkDotNet.Attributes;
using Ryu.Net.UnitTests.s2d_data;
using System.Text;

namespace Ryu.NET.Benchmark
{
    [Config(typeof(RyuBenchMarkConfig))]
    public class ParseAsciiString
    {
        byte[][] TestArray;

        [GlobalSetup]
        public void GlobalSetup()
         => TestArray = SmallTestSet.TestAsciiArray;

        [Benchmark(Baseline = true)]
        public void CSDoubleParse()
        {
            for (int i = 0; i < TestArray.Length; ++i)
            {
                double x = double.Parse(Encoding.ASCII.GetString(TestArray[i]));
                x = x + 1.0;
            }
        }

        [Benchmark]
        unsafe public void RyuDoubleParse()
        {
            for (int i = 0; i < TestArray.Length; ++i)
            {
                double x;
                Ryu.Net.Internal.Ryu.s2d_n(TestArray[i], out x);
                x = x + 1.0;
            }
        }
    }
}
