using BenchmarkDotNet.Attributes;
using RyuDotNet.UnitTests.s2d_data;
using System.Globalization;
using System.Text;

namespace RyuDotNet.Benchmark
{
    [Config(typeof(RyuBenchMarkConfig))]
    public class PrintStringAsciii
    {
        static byte[] span = new byte[100];
        static CultureInfo culture = System.Globalization.CultureInfo.InvariantCulture;
        [Benchmark(Baseline = true)]
        public void CSDoublePrintAscii()
        {
            for (int i = 0; i < SmallD2STestSet.TestArray.Length; ++i)
            {
                for (int u = 0; u < 5; ++u)
                {

                    var byteArray =  Encoding.ASCII.GetBytes(SmallD2STestSet.TestArray[i].ToString(), span);
                    int n = byteArray * 2;
                }
            }
        }

        [Benchmark]
        public void RyuDoublePrintAscii()
        {
            for (int i = 0; i < SmallD2STestSet.TestArray.Length; ++i)
            {
                for (int u = 0; u < 5; ++u)
                {

                    int n =  RyuDotNet.Internal.Ryu.d2s_buffered_n(SmallD2STestSet.TestArray[i], span);
                    int m = n * 2;
                }
            }
        }
    }
}
