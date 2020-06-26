using RyuDotNet.Internal;
using System;
using System.Runtime.CompilerServices;

namespace RyuDotNet
{
    public static class Ryu
    {
       
        public static bool TryParse(ReadOnlySpan<char> span, out double res) =>
            RyuDotNet.Internal.Ryu.s2d_n(span, out res) == RyuDotNet.Internal.Status.SUCCESS;

        public static bool TryParse(string str, out double res) =>
            TryParse(str.AsSpan(), out res);

        public static bool TryParse(ReadOnlySpan<byte> span, out double res) =>
           RyuDotNet.Internal.Ryu.s2d_n(span, out res) == RyuDotNet.Internal.Status.SUCCESS;

        public static double Parse(ReadOnlySpan<char> span)
        {
            double res;
            Status status;
            if ((status = RyuDotNet.Internal.Ryu.s2d_n(span, out res)) == RyuDotNet.Internal.Status.SUCCESS) return res;
            throw new FormatException(status.ToString());

        }


        public static double Parse(string str) =>
            Parse(str.AsSpan());

        public static double Parse(ReadOnlySpan<byte> span)
        {
            double res = 0.0;
            Status status;
            if ((status = RyuDotNet.Internal.Ryu.s2d_n(span, out res)) == RyuDotNet.Internal.Status.SUCCESS) return res;
            throw new FormatException(status.ToString());
        }

        unsafe public static string ToStringRyu(this double d)
        {
            Span<char> charSpan = stackalloc char[40];
            int index = RyuDotNet.Internal.Ryu.d2s_buffered_n(d, charSpan);
            fixed (char* data = &charSpan.GetPinnableReference())
            {
                return new string(data, 0, index);
            }
        }
        unsafe public static string ToStringRyu(this double d,int Precision)
        {
            Span<char> charSpan = stackalloc char[40];
            int index = RyuDotNet.Internal.Ryu.d2exp_buffered_n(d,(uint)Precision, charSpan);
            fixed (char* data = &charSpan.GetPinnableReference())
            {
                return new string(data, 0, index);
            }
        }

        public static Span<byte> WriteTo(this double d, Span<byte> buffer)
        {
            int index = RyuDotNet.Internal.Ryu.d2s_buffered_n(d, buffer);
            return buffer.Slice(0, index);
        }

        public static Span<char> WriteTo(this double d, Span<char> buffer)
        {
            int index = RyuDotNet.Internal.Ryu.d2s_buffered_n(d, buffer);
            return buffer.Slice(0, index);
        }

        public static Span<byte> WriteTo(this double d, int Precision,  Span<byte> buffer)
        {
            int index = RyuDotNet.Internal.Ryu.d2exp_buffered_n(d,(uint)Precision, buffer);
            return buffer.Slice(0, index);
        }

        public static Span<char> WriteTo(this double d, int Precision, Span<char> buffer)
        {
            int index = RyuDotNet.Internal.Ryu.d2exp_buffered_n(d, (uint)Precision, buffer);
            return buffer.Slice(0, index);
        }
    }
}
