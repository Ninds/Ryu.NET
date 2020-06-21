using RyuDotNet.Internal;
using System;

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
            double res = 0.0;
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

    }
}
