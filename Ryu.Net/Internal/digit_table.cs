using System;
using System.Runtime.CompilerServices;

namespace RyuDotNet.Internal
{
    unsafe partial class Ryu
    {
        static ReadOnlySpan<byte> DIGIT_TABLE => new byte[200] {
          (byte)'0',(byte)'0',(byte)'0',(byte)'1',(byte)'0',(byte)'2',(byte)'0',(byte)'3',(byte)'0',(byte)'4',
          (byte)'0',(byte)'5',(byte)'0',(byte)'6',(byte)'0',(byte)'7',(byte)'0',(byte)'8',(byte)'0',(byte)'9',
          (byte)'1',(byte)'0',(byte)'1',(byte)'1',(byte)'1',(byte)'2',(byte)'1',(byte)'3',(byte)'1',(byte)'4',
          (byte)'1',(byte)'5',(byte)'1',(byte)'6',(byte)'1',(byte)'7',(byte)'1',(byte)'8',(byte)'1',(byte)'9',
          (byte)'2',(byte)'0',(byte)'2',(byte)'1',(byte)'2',(byte)'2',(byte)'2',(byte)'3',(byte)'2',(byte)'4',
          (byte)'2',(byte)'5',(byte)'2',(byte)'6',(byte)'2',(byte)'7',(byte)'2',(byte)'8',(byte)'2',(byte)'9',
          (byte)'3',(byte)'0',(byte)'3',(byte)'1',(byte)'3',(byte)'2',(byte)'3',(byte)'3',(byte)'3',(byte)'4',
          (byte)'3',(byte)'5',(byte)'3',(byte)'6',(byte)'3',(byte)'7',(byte)'3',(byte)'8',(byte)'3',(byte)'9',
          (byte)'4',(byte)'0',(byte)'4',(byte)'1',(byte)'4',(byte)'2',(byte)'4',(byte)'3',(byte)'4',(byte)'4',
          (byte)'4',(byte)'5',(byte)'4',(byte)'6',(byte)'4',(byte)'7',(byte)'4',(byte)'8',(byte)'4',(byte)'9',
          (byte)'5',(byte)'0',(byte)'5',(byte)'1',(byte)'5',(byte)'2',(byte)'5',(byte)'3',(byte)'5',(byte)'4',
          (byte)'5',(byte)'5',(byte)'5',(byte)'6',(byte)'5',(byte)'7',(byte)'5',(byte)'8',(byte)'5',(byte)'9',
          (byte)'6',(byte)'0',(byte)'6',(byte)'1',(byte)'6',(byte)'2',(byte)'6',(byte)'3',(byte)'6',(byte)'4',
          (byte)'6',(byte)'5',(byte)'6',(byte)'6',(byte)'6',(byte)'7',(byte)'6',(byte)'8',(byte)'6',(byte)'9',
          (byte)'7',(byte)'0',(byte)'7',(byte)'1',(byte)'7',(byte)'2',(byte)'7',(byte)'3',(byte)'7',(byte)'4',
          (byte)'7',(byte)'5',(byte)'7',(byte)'6',(byte)'7',(byte)'7',(byte)'7',(byte)'8',(byte)'7',(byte)'9',
          (byte)'8',(byte)'0',(byte)'8',(byte)'1',(byte)'8',(byte)'2',(byte)'8',(byte)'3',(byte)'8',(byte)'4',
          (byte)'8',(byte)'5',(byte)'8',(byte)'6',(byte)'8',(byte)'7',(byte)'8',(byte)'8',(byte)'8',(byte)'9',
          (byte)'9',(byte)'0',(byte)'9',(byte)'1',(byte)'9',(byte)'2',(byte)'9',(byte)'3',(byte)'9',(byte)'4',
          (byte)'9',(byte)'5',(byte)'9',(byte)'6',(byte)'9',(byte)'7',(byte)'9',(byte)'8',(byte)'9',(byte)'9'
        };
        // memcpy(result.Slice((int)(index + olength - i - 1)), DIGIT_TABLE.Slice((int)c0, 2));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Copy2BytesFromDigitTable(AlphaSpan result, long startDest, uint StartSrc)
        {
            result[(int)startDest++] = DIGIT_TABLE[(int)StartSrc++];
            result[(int)startDest]   = DIGIT_TABLE[(int)StartSrc];
        }
    }
}