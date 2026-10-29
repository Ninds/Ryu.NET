
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

        [Theory]
        [InlineData(".")]
        [InlineData("-.")]
        [InlineData("+")]
        [InlineData("1e")]
        [InlineData("1e+")]
        [InlineData("1e-")]
        public void TryParseRejectsMissingDigits(string input)
        {
            Assert.False(Ryu.TryParse(input.AsSpan(), out _));
            Assert.False(Ryu.TryParse(Encoding.ASCII.GetBytes(input), out _));
        }

        [Fact]
        public void TryParseHandlesLargeExponentAndFractionTogether()
        {
            var input = "0." + new string('0', 20000) + "1e20001";

            Assert.True(Ryu.TryParse(input.AsSpan(), out var charResult));
            Assert.True(Ryu.TryParse(Encoding.ASCII.GetBytes(input), out var byteResult));
            Assert.Equal(1.0, charResult);
            Assert.Equal(1.0, byteResult);
        }

        [Theory]
        [InlineData("NaN")]
        [InlineData("Infinity")]
        [InlineData("-Infinity")]
        public void TryParseAcceptsFormattedSpecialValues(string input)
        {
            Assert.True(Ryu.TryParse(input.AsSpan(), out var charResult));
            Assert.True(Ryu.TryParse(Encoding.ASCII.GetBytes(input), out var byteResult));

            if (input == "NaN")
            {
                Assert.True(double.IsNaN(charResult));
                Assert.True(double.IsNaN(byteResult));
            }
            else if (input == "-Infinity")
            {
                Assert.Equal(double.NegativeInfinity, charResult);
                Assert.Equal(double.NegativeInfinity, byteResult);
            }
            else
            {
                Assert.Equal(double.PositiveInfinity, charResult);
                Assert.Equal(double.PositiveInfinity, byteResult);
            }
        }

        [Fact]
        public void SpecialValuesRoundTripThroughStringAndSpanFormatters()
        {
            double[] values = { double.NaN, double.PositiveInfinity, double.NegativeInfinity };
            Span<byte> byteBuffer = stackalloc byte[32];
            Span<char> charBuffer = stackalloc char[32];

            foreach (var value in values)
            {
                Assert.True(Ryu.TryParse(value.ToStringRyu(), out var parsed));
                AssertEquivalent(value, parsed);

                Assert.True(Ryu.TryParse(value.ToStringRyu(4), out parsed));
                AssertEquivalent(value, parsed);

                Assert.True(Ryu.TryParse(value.WriteTo(4, byteBuffer), out parsed));
                AssertEquivalent(value, parsed);

                Assert.True(Ryu.TryParse(value.WriteTo(4, charBuffer), out parsed));
                AssertEquivalent(value, parsed);
            }
        }

        [Fact]
        public void ToStringRyuSupportsPrecisionBeyondStackBuffer()
        {
            var formatted = 1.25.ToStringRyu(64);

            Assert.True(formatted.Length > 40);
            Assert.StartsWith("1.25", formatted);
            Assert.EndsWith("E+00", formatted);
        }

        [Fact]
        public void PrecisionOverloadsRejectNegativePrecision()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 1.25.ToStringRyu(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => 1.25.WriteTo(-1, new byte[32]));
            Assert.Throws<ArgumentOutOfRangeException>(() => 1.25.WriteTo(-1, new char[32]));
        }

        [Fact]
        public void PrecisionWriteToRejectsInsufficientBuffer()
        {
            Assert.Throws<ArgumentException>(() => 1.25.WriteTo(10, new byte[17]));
            Assert.Throws<ArgumentException>(() => 1.25.WriteTo(10, new char[17]));
        }

        static void AssertEquivalent(double expected, double actual)
        {
            if (double.IsNaN(expected))
            {
                Assert.True(double.IsNaN(actual));
            }
            else
            {
                Assert.Equal(expected, actual);
            }
        }

    }

}
