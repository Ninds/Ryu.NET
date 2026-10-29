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
        string[] StringArray;

        [GlobalSetup]
        public void GlobalSetup()
        {
            TestArray = SmallTestSet.TestAsciiArray;
            StringArray = new string[TestArray.Length];
            for (int i = 0; i < TestArray.Length; ++i)
            {
                StringArray[i] = Encoding.ASCII.GetString(TestArray[i]);
            }
        }

        [Benchmark(Baseline = true)]
        public void CSDoubleParseString()
        {
            for (int i = 0; i < StringArray.Length; ++i)
            {
                double x;
                for (int u = 0; u < 5; ++u) x = double.Parse(StringArray[i], culture);

            }
        }

        [Benchmark]
        public void CSDoubleParseAsciiEndToEnd()
        {
            for (int i = 0; i < TestArray.Length; ++i)
            {
                double x;
                for (int u = 0; u < 5; ++u) x = double.Parse(Encoding.ASCII.GetString(TestArray[i]), culture);
            }
        }

        [Benchmark]
        public void RyuDoubleParseAscii()
        {
            for (int i = 0; i < TestArray.Length; ++i)
            {
                double x;
                for (int u = 0; u < 5; ++u) RyuDotNet.Internal.Ryu.s2d_n(TestArray[i], out x);

            }
        }
    }
}
