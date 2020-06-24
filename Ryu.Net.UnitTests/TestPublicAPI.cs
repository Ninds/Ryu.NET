
using System;
using Xunit;

namespace RyuDotNet.UnitTests
{
    public class TestPublicApi
    {
        //[Theory]
        [ClassData(typeof(SmallDataGenerator))]
        public void Test_TryParse_char(string str)
        {

            double ryuVale;
            Assert.True(RyuDotNet.Ryu.TryParse(str.AsSpan(), out ryuVale));
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));

        }


        //[Theory]
        [ClassData(typeof(SmallDataGenerator))]
        public void Test_TryParse_string(string str)
        {

            double ryuVale;
            Assert.True(RyuDotNet.Ryu.TryParse(str, out ryuVale));
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));

        }


        //[Theory]
        [ClassData(typeof(SmallAsciiDataGenerator))]
        public void Test_TryParse_byte(byte[] byteArray, string str)
        {
           
            double ryuVale;
            Assert.True(RyuDotNet.Ryu.TryParse(byteArray, out ryuVale));
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));

        }


        //[Theory]
        [ClassData(typeof(SmallDataGenerator))]
        public void Test_Parse_char(string str)
        {
            double ryuVale = RyuDotNet.Ryu.Parse(str.AsSpan());
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));
        }

        //[Theory]
        [ClassData(typeof(SmallDataGenerator))]
        public void Test_Parse_string(string str)
        {
            double ryuVale = RyuDotNet.Ryu.Parse(str);
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));
        }

        //[Theory]
        [ClassData(typeof(SmallAsciiDataGenerator))]
        public void Test_Parse_byte(byte[] byteArray, string str)
        {
            double ryuVale = RyuDotNet.Ryu.Parse(byteArray);
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));
        }


    }

}
