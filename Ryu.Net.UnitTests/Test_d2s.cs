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
    unsafe public class Test_d2s
    {

        //[Theory]
        [InlineData(1.0)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(-0.000000000000001E-100)]
        [InlineData(5737722933969577e-231)]
        [ClassData(typeof(D2SDataGenerator))]
        public void Test_d2exp_buffered_n(double f)
        {
            Span<char> readOnlySpan = stackalloc char[100];
            int index = 0;

            index = RyuDotNet.Internal.Ryu.d2exp_buffered_n(f, 19, readOnlySpan);

            var strString = new string(readOnlySpan.Slice(0, index));
            var parts = strString.Split('E');
            var expInt = int.Parse(parts[1]);
            var exp = expInt.ToString("000");
            if (expInt >= 0) exp = "+" + exp;
            strString = parts[0] + 'E' + exp;
            Assert.Equal(f.ToString("E19"), strString);

        }


        //[Theory]
        [InlineData(1.0)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(-0.000000000000001E-100)]
        [InlineData(5737722933969577e-231)]
        [ClassData(typeof(D2SDataGenerator))]
        public void Test_d2s_buffered_n(double f)
        {
            Span<char> readOnlySpan = stackalloc char[100];
            int index = 0;
            index = RyuDotNet.Internal.Ryu.d2s_buffered_n(f, readOnlySpan);
            var strString = new string(readOnlySpan.Slice(0, index));
            Assert.Equal(f.ToString("E19"), double.Parse(strString).ToString("E19"));

        }


        [Theory]
        [InlineData(1.0)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(-0.000000000000001E-100)]
        [InlineData(5737722933969577e-231)]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void TestWithSmall_d2exp_buffered_n(double f)
        {
            Span<char> readOnlySpan = stackalloc char[100];
            int index = 0;

            index = RyuDotNet.Internal.Ryu.d2exp_buffered_n(f, 19, readOnlySpan);

            var strString = new string(readOnlySpan.Slice(0, index));
            var parts = strString.Split('E');
            var expInt = int.Parse(parts[1]);
            var exp = expInt.ToString("000");
            if (expInt >= 0) exp = "+" + exp;
            strString = parts[0] + 'E' + exp;
            Assert.Equal(f.ToString("E19"), strString);

        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(-0.000000000000001E-100)]
        [InlineData(5737722933969577e-231)]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void TestWithSmall_d2s_buffered_n(double f)
        {
            Span<char> readOnlySpan = stackalloc char[100];
            int index = 0;
            index = RyuDotNet.Internal.Ryu.d2s_buffered_n(f, readOnlySpan);
            var strString = new string(readOnlySpan.Slice(0, index));
            Assert.Equal(f.ToString("E19"), double.Parse(strString).ToString("E19"));

        }


        [Theory]
        [InlineData(1.0)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(-0.000000000000001E-100)]
        [InlineData(5737722933969577e-231)]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void TestWithSmall_d2exp_buffered_n_ascii(double f)
        {
            Span<byte> readOnlySpan = stackalloc byte[100];
            int index = 0;

            index = RyuDotNet.Internal.Ryu.d2exp_buffered_n(f, 19, readOnlySpan);
            var strString = Encoding.ASCII.GetString(readOnlySpan.Slice(0, index));
            var parts = strString.Split('E');
            var expInt = int.Parse(parts[1]);
            var exp = expInt.ToString("000");
            if (expInt >= 0) exp = "+" + exp;
            strString = parts[0] + 'E' + exp;
            Assert.Equal(f.ToString("E19"), strString);

        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(-0.000000000000001E-100)]
        [InlineData(5737722933969577e-231)]
        [ClassData(typeof(D2SSmallDataGenerator))]
        public void TestWithSmall_d2s_buffered_n_ascii(double f)
        {
            Span<byte> readOnlySpan = stackalloc byte[100];
            int index = 0;
            index = RyuDotNet.Internal.Ryu.d2s_buffered_n(f, readOnlySpan);
            var strString = Encoding.ASCII.GetString(readOnlySpan.Slice(0, index));
            Assert.Equal(f.ToString("E19"), double.Parse(strString).ToString("E19"));

        }



    }

    public class D2SDataGenerator : IEnumerable<object[]>
    {
        IEnumerable<IFPTestData_D2S> _allFPTestDatas;
        public D2SDataGenerator()
        {
            _allFPTestDatas = typeof(IFPTestData_D2S).Assembly.
                GetTypes().
                Where(t => typeof(IFPTestData_D2S).IsAssignableFrom(t) && !t.IsInterface).
                Select(t => (IFPTestData_D2S)Activator.CreateInstance(t));
        }


        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var klass in _allFPTestDatas)
            {
                foreach (var v in klass.TestArray)
                {
                    yield return new object[] { v};
                }

            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    public class D2SSmallDataGenerator : IEnumerable<object[]>
    {
        
        public IEnumerator<object[]> GetEnumerator()
        {
          
                foreach (var v in SmallD2STestSet.TestArray)
                {
                    yield return new object[] { v };
                }

           
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }


}

