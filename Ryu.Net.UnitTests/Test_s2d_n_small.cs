using RyuDotNet.Internal;
using RyuDotNet.UnitTests.s2d_data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RyuDotNet.UnitTests
{
    unsafe public class Test_s2d_n_small 
    {
        
        [Theory]
        [InlineData("1.2999999999999999E+154")]
        [ClassData(typeof(SmallDataGenerator))]
        public void TestWithSmall(string str)
        {

            double ryuVale;
            var eq1 = RyuDotNet.Internal.Ryu.s2d_n(str.AsSpan(), out ryuVale);
            Assert.Equal(Status.SUCCESS, eq1);
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));

        }

        [Theory]
        [ClassData(typeof(SmallAsciiDataGenerator))]
        public void TestWithSmall_Ascii(byte[] ascii, string utf16)
        {

            double ryuVale;
            var eq1 = RyuDotNet.Internal.Ryu.s2d_n(ascii, out ryuVale);
            Assert.Equal(Status.SUCCESS, eq1);
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(utf16).ToString("G18"));

        }


        [Fact]
        public void TestUTF16()
        {
            double x = Math.PI;
            double ryuVale;

            for (int i = 0; i < 50; ++i)
            {
                var str = new StringBuilder(x.ToString());
                var eq1 = RyuDotNet.Internal.Ryu.s2d_n(str.ToString(), out ryuVale);
                Assert.Equal(Status.SUCCESS, eq1);

                for(int j = 0; j < str.Length; ++j)
                {
                    var str2 = new StringBuilder(str.ToString());
                    var r = str2[j] | (1 << (8+ (j%8)));
                    str2[j] = (char)r;
                    var eq2 = RyuDotNet.Internal.Ryu.s2d_n(str2.ToString(), out ryuVale);
                    Assert.Equal(Status.MALFORMED_INPUT, eq2);
                }
                x *= -Math.PI;
            }

        }

        // https://github.com/ulfjack/ryu/issues/173
        [Fact]
        public void TestDenormalBoundaryInParse()
        {
            double result = 0;
            var testCase = "2.2250738585072013e-308";
            double refValue = 2.2250738585072013e-308;

            var st = RyuDotNet.Internal.Ryu.s2d_n(testCase, out result);

            Assert.Equal(Status.SUCCESS, st);
            Assert.Equal(refValue, result);

        }




        }

    public class SmallDataGenerator : IEnumerable<object[]>
    { 
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var str in SmallTestSet.TestArray)
            {
                yield return new object[] { str };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    public class SmallAsciiDataGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var PairsEnumberable  =  SmallTestSet.TestAsciiArray.Zip(SmallTestSet.TestArray);
            foreach (var pair in PairsEnumberable)
            {

                yield return new object[] {pair.First,pair.Second };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }


}
