using Ryu.Net.Internal;
using Ryu.Net.UnitTests.s2d_data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var buffer = new char[str.Length];
            str.CopyTo(0, buffer, 0, str.Length);

            double ryuVale;
            fixed (char* buffStart = &buffer[0])
            {
                var eq1 = Ryu.Net.Internal.Ryu.s2d_n(buffStart, str.Length, &ryuVale);
                Assert.Equal(Status.SUCCESS, eq1);
            }
           
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));

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

}
