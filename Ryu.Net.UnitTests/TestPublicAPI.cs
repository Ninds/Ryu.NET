
using System;
using System.Text;
using Xunit;

namespace RyuDotNet.UnitTests
{
    public class TestPublicApi
    {
        [Theory]
        [ClassData(typeof(SmallDataGenerator))]
        public void Test_TryParse_char(string str)
        {

            double ryuVale;
            Assert.True(RyuDotNet.Ryu.TryParse(str.AsSpan(), out ryuVale));
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));

        }


        [Theory]
        [ClassData(typeof(SmallDataGenerator))]
        public void Test_TryParse_string(string str)
        {

            double ryuVale;
            Assert.True(RyuDotNet.Ryu.TryParse(str, out ryuVale));
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));

        }


        [Theory]
        [ClassData(typeof(SmallAsciiDataGenerator))]
        public void Test_TryParse_byte(byte[] byteArray, string str)
        {
           
            double ryuVale;
            Assert.True(RyuDotNet.Ryu.TryParse(byteArray, out ryuVale));
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));

        }


        [Theory]
        [ClassData(typeof(SmallDataGenerator))]
        public void Test_Parse_char(string str)
        {
            double ryuVale = RyuDotNet.Ryu.Parse(str.AsSpan());
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));
        }

        [Theory]
        [ClassData(typeof(SmallDataGenerator))]
        public void Test_Parse_string(string str)
        {
            double ryuVale = RyuDotNet.Ryu.Parse(str);
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));
        }

        [Theory]
        [ClassData(typeof(SmallAsciiDataGenerator))]
        public void Test_Parse_byte(byte[] byteArray, string str)
        {
            double ryuVale = RyuDotNet.Ryu.Parse(byteArray);
            Assert.Equal(ryuVale.ToString("G18"), double.Parse(str).ToString("G18"));
        }


        [Theory]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void Test_ToStringRyu(double d)
        {
            var ryuString = d.ToStringRyu();
            Assert.Equal(d.ToString(), double.Parse(ryuString).ToString());
        }


        [Theory]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void Test_ToStringRyuN(double d)
        {
            var strString = d.ToStringRyu(19);
            var parts = strString.Split('E');
            var expInt = int.Parse(parts[1]);
            var exp = expInt.ToString("000");
            if (expInt >= 0) exp = "+" + exp;
            strString = parts[0] + 'E' + exp;
            Assert.Equal(d.ToString("E19"), strString);
        }

        [Theory]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void Test_WritToByte(double d)
        {
            Span<byte> span = stackalloc byte[50];
            var ryuString = Encoding.ASCII.GetString(d.WriteTo(span));
            Assert.Equal(d.ToString(), double.Parse(ryuString).ToString());
        }
        [Theory]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void Test_WritToChar(double d)
        {
            Span<char> span = stackalloc char[50];
            var ryuString = new string(d.WriteTo(span));
            Assert.Equal(d.ToString(), double.Parse(ryuString).ToString());
        }

        [Theory]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void Test_WritToByteN(double d)
        {
            Span<byte> span = stackalloc byte[50];
            var strString = Encoding.ASCII.GetString(d.WriteTo(19,span));
            var parts = strString.Split('E');
            var expInt = int.Parse(parts[1]);
            var exp = expInt.ToString("000");
            if (expInt >= 0) exp = "+" + exp;
            strString = parts[0] + 'E' + exp;
            Assert.Equal(d.ToString("E19"), strString);

        }
        [Theory]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void Test_WritToCharN(double d)
        {
            Span<char> span = stackalloc char[50];
            var strString = new string(d.WriteTo(19,span));
            var parts = strString.Split('E');
            var expInt = int.Parse(parts[1]);
            var exp = expInt.ToString("000");
            if (expInt >= 0) exp = "+" + exp;
            strString = parts[0] + 'E' + exp;
            Assert.Equal(d.ToString("E19"), strString);
        }

    }

}
