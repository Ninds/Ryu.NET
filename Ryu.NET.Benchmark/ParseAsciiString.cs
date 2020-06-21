using BenchmarkDotNet.Attributes;
using RyuDotNet.UnitTests.s2d_data;
using System.Globalization;
using System.Text;

namespace RyuDotNet.Benchmark
{
    [Config(typeof(RyuBenchMarkConfig))]
    public class ParseAsciiString
    {
        static CultureInfo culture = System.Globalization.CultureInfo.InvariantCulture;
        byte[][] TestArray;

        [GlobalSetup]
        public void GlobalSetup()
         => TestArray = SmallTestSet.TestAsciiArray;

        [Benchmark(Baseline = true)]
        public void CSDoubleParseAscii()
        {
            for (int i = 0; i < TestArray.Length; ++i)
            {
                double x = double.Parse(Encoding.ASCII.GetString(TestArray[i]),culture);
                x = x + 1.0;
            }
        }

        [Benchmark]
        unsafe public void RyuDoubleParseAscii()
        {
            for (int i = 0; i < TestArray.Length; ++i)
            {
                double x;
                RyuDotNet.Internal.Ryu.s2d_n(TestArray[i], out x);
                x = x + 1.0;
            }
        }
    }
}
