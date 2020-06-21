using RyuDotNet.Internal;
using System;
using Xunit;

namespace RyuDotNet.UnitTests
{
    unsafe public class Test_s2d_test_cc
    {

        // Tests taken from  https://github.com/ulfjack/ryu/blob/master/ryu/tests/s2d_test.cc


        // Basic
        [InlineData(0.0, "0")]
        [InlineData(-0.0, "-0")]
        [InlineData(1.0, "1")]
        [InlineData(2.0, "2")]
        [InlineData(123456789.0, "123456789")]
        [InlineData(123.456, "123.456")]
        [InlineData(123.456, "123456e-3")]
        [InlineData(123.456, "1234.56e-1")]
        [InlineData(1.453, "1.453")]
        [InlineData(1453.0, "1.453e+3")]
        [InlineData(0.0, ".0")]
        [InlineData(1.0, "1e0")]
        [InlineData(1.0, "1E0")]
        [InlineData(1.0, "000001.000000")]
        [InlineData(0.2316419, "0.2316419")]

        //MinMax
        [InlineData(1.7976931348623157e308, "1.7976931348623157e308")]
        [InlineData(5E-324, "5E-324")]


        //MantissaRoundingOverflow
        [InlineData(1.0, "0.99999999999999999")]
      

        //Underflow
        [InlineData(0.0, "2.4e-324")]
        [InlineData(0.0, "1e-324")]
        [InlineData(0.0, "9.99999e-325")]
        // These are just about halfway between 0 and the smallest float.
        // The first is just below the halfway point, the second just above.
        [InlineData(0.0, "2.4703282292062327e-324")]
        [InlineData(5e-324, "2.4703282292062328e-324")]


        //Issue157
        [InlineData(1.2999999999999999E+154, "1.2999999999999999E+154")]

        [Theory]
        public void TestRyu(double value,string str)
        {
            double ryuVale;
            var eq1 = RyuDotNet.Internal.Ryu.s2d_n(str.AsSpan(), out ryuVale);
            Assert.Equal(Status.SUCCESS, eq1);
            Assert.Equal(double.Parse(str).ToString("G18"),ryuVale.ToString("G18"));
        }

      
    }

  

}
