using Ryu.Net.Internal;
using Ryu.Net.UnitTests.s2d_data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ryu.Net.UnitTests
{
    unsafe public class Test_s2d_n_small 
    {
        
        [Theory]
        [InlineData("1.2999999999999999E+154")]
        [ClassData(typeof(SmallDataGenerator))]
        public void TestWithSmall(string str)
        {

            double ryuVale;
            var eq1 = Ryu.Net.Internal.Ryu.s2d_n(str.AsSpan(), out ryuVale);
            Assert.Equal(Status.SUCCESS, eq1);
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));

        }

        [Theory]
        
        [ClassData(typeof(SmallAsciiDataGenerator))]
        public void TestWithSmall_Ascii(byte[] ascii)
        {

            double ryuVale;
            var eq1 = Ryu.Net.Internal.Ryu.s2d_n(ascii, out ryuVale);
            Assert.Equal(Status.SUCCESS, eq1);
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(Encoding.ASCII.GetString(ascii)).ToString("G18"));

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
            foreach (var str in SmallTestSet.TestAsciiArray)
            {

                yield return new object[] { str };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }


}
