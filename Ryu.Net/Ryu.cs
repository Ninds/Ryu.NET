using RyuDotNet.Internal;
using System;
using System.Runtime.CompilerServices;

namespace RyuDotNet
{
    public static class Ryu
    {
        /// <summary>
        /// Converts the span representation of a number to its double-precision floating-point number equivalent using
        /// the Ryu algorithm.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="span">A chacater span  that contains the string representation of the number to convert.</param>
        /// <param name="result">When this method returns, contains the double-precision floating-point number equivalent of the numeric value 
        /// or symbol contained in span parameter,  if the conversion succeeded, or zero if the conversion failed. </param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParse(ReadOnlySpan<char> s, out double result) =>
            RyuDotNet.Internal.Ryu.s2d_n(s, out result) == RyuDotNet.Internal.Status.SUCCESS;

        /// <summary>
        /// Converts the string representation of a number to its double-precision floating-point number equivalent using
        /// the Ryu algorithm.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="s">A string containing a number to convert.</param>
        /// <param name="result">When this method returns, contains the double-precision floating-point number equivalent of the numeric value 
        /// or symbol contained in span parameter,  if the conversion succeeded, or zero if the conversion failed. </param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string s, out double result) =>
            TryParse(s.AsSpan(), out result);

        /// <summary>
        /// Converts the byte (ascii) span containing the string representation of a number to its double-precision floating-point number equivalent using
        /// the Ryu algorithm.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="span">An byte span of the ascii string that contains the string representation of the number to convert.</param>
        /// <param name="result">When this method returns, contains the double-precision floating-point number equivalent of the numeric value 
        /// or symbol contained in span parameter,  if the conversion succeeded, or zero if the conversion failed. </param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParse(ReadOnlySpan<byte> s, out double result) =>
           RyuDotNet.Internal.Ryu.s2d_n(s, out result) == RyuDotNet.Internal.Status.SUCCESS;


        /// <summary>
        /// Converts a character span that contains the string representation of a number  to its double-precision floating-point number equivalent  using
        /// the Ryu algorithm.
        /// </summary>
        /// <param name="s">A character span that contains the number to convert.</param>
        /// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in s.</returns>
        public static double Parse(ReadOnlySpan<char> s)
        {
            double res;
            Status status;
            if ((status = RyuDotNet.Internal.Ryu.s2d_n(s, out res)) == RyuDotNet.Internal.Status.SUCCESS) return res;
            throw new FormatException(status.ToString());

        }

        /// <summary>
        /// Converts the string representation of a number  to its double-precision floating-point number equivalent using
        /// the Ryu algorithm.
        /// </summary>
        /// <param name="s">A string containing a number to convert.</param>
        /// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in s.</returns>
        public static double Parse(string s) =>
            Parse(s.AsSpan());


        /// <summary>
        /// Converts a byte (ascii) span that contains representation of a number  to its double-precision floating-point number equivalent using
        /// the Ryu algorithm.
        /// </summary>
        /// <param name="s">A byte (ascii) span containing a number to convert.</param>
        /// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in s.</returns>
        public static double Parse(ReadOnlySpan<byte> s)
        {
            double res = 0.0;
            Status status;
            if ((status = RyuDotNet.Internal.Ryu.s2d_n(s, out res)) == RyuDotNet.Internal.Status.SUCCESS) return res;
            throw new FormatException(status.ToString());
        }


        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using
        /// the Ryu algorithm.
        /// </summary>
        /// <param name="d">The double to convert to a string</param>
        /// <returns>The string representation of the double value</returns>
        unsafe public static string ToStringRyu(this double d)
        {
            Span<char> charSpan = stackalloc char[40];
            int index = RyuDotNet.Internal.Ryu.d2s_buffered_n(d, charSpan);
            fixed (char* data = &charSpan.GetPinnableReference())
            {
                return new string(data, 0, index);
            }
        }


        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using
        /// the Ryu algorithm.
        /// </summary>
        /// <param name="d">The double to convert to a string</param>
        /// <param name="Precision">The precision in the  exponential (scientific)notation </param>
        /// <returns>The string representation of the double value</returns>
        unsafe public static string ToStringRyu(this double d,int Precision)
        {
            Span<char> charSpan = stackalloc char[40];
            int index = RyuDotNet.Internal.Ryu.d2exp_buffered_n(d,(uint)Precision, charSpan);
            fixed (char* data = &charSpan.GetPinnableReference())
            {
                return new string(data, 0, index);
            }
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using
        /// the Ryu algorithm.
        /// </summary>
        /// <param name="d">The double to convert to a string</param>
        /// <param name="buffer"> A span of bytes in which to place the ascii of the string representation of the double value</param>
        /// <returns>The ascii (byte) span containing the string representation of the double value which will be a slice of buffer</returns>
        public static Span<byte> WriteTo(this double d, Span<byte> buffer)
        {
            int index = RyuDotNet.Internal.Ryu.d2s_buffered_n(d, buffer);
            return buffer.Slice(0, index);
        }


        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using
        /// the Ryu algorithm.
        /// </summary>
        /// <param name="d">The double to convert to a string</param>
        /// <param name="buffer"> A span of characters in which to place the ascii of the string representation of the double value </param>
        /// <returns>The characters containing the string representation of the double value which will be a slice of buffer</returns>
        public static Span<char> WriteTo(this double d, Span<char> buffer)
        {
            int index = RyuDotNet.Internal.Ryu.d2s_buffered_n(d, buffer);
            return buffer.Slice(0, index);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using
        /// the Ryu algorithm.
        /// </summary>
        /// <param name="d">The double to convert to a string</param>
        /// <param name="Precision">The precision in the  exponential (scientific)notation </param>
        /// <param name="buffer">The ascii (byte) span in which to place the ascii of the string representation of the double value</param>
        /// <returns>The ascii (byte) span containing the string representation of the double value which will be a slice of buffer</returns>
        public static Span<byte> WriteTo(this double d, int Precision,  Span<byte> buffer)
        {
            int index = RyuDotNet.Internal.Ryu.d2exp_buffered_n(d,(uint)Precision, buffer);
            return buffer.Slice(0, index);
        }


        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using
        /// the Ryu algorithm.
        /// </summary>
        /// <param name="d">The double to convert to a string</param>
        /// <param name="Precision">The precision in the  exponential (scientific)notation </param>
        /// <param name="buffer">The characters in which to place the ascii of the string representation of the double value</param>
        /// <returns>The characters containing the string representation of the double value which will be a slice of buffer</returns>
        public static Span<char> WriteTo(this double d, int Precision, Span<char> buffer)
        {
            int index = RyuDotNet.Internal.Ryu.d2exp_buffered_n(d, (uint)Precision, buffer);
            return buffer.Slice(0, index);
        }
    }
}
