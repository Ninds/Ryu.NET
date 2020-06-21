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


    }

    public class SmallDataGenerator : IEnumerable<object[]>
    {
        IEnumerable<IFPTestData> _allFPTestDatas;
       
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach(var str in SmallTestSet.TestArray)
            {
                
                    yield return new object[] { str };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    public class SmallAsciiDataGenerator : IEnumerable<object[]>
    {
        IEnumerable<IFPTestData> _allFPTestDatas;

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
