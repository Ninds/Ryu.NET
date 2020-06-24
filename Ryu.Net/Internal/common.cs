using System;
using int32_t = System.Int32;
using uint32_t = System.UInt32;
using uint64_t = System.UInt64;

namespace RyuDotNet.Internal
{
    internal static unsafe partial class Ryu
    {
        const int DOUBLE_MANTISSA_BITS = 52;
        const int DOUBLE_EXPONENT_BITS = 11;
        const int DOUBLE_BIAS = 1023;

        // Returns the number of decimal digits in v, which must not contain more than 9 digits.
        static uint32_t decimalLength9(uint32_t v)
        {
            // Function precondition: v is not a 10-digit number.
            // (f2s: 9 digits are sufficient for round-tripping.)
            // (d2fixed: We print 9-digit blocks.)
            assert(v < 1000000000);
            if (v >= 100000000) { return 9; }
            if (v >= 10000000) { return 8; }
            if (v >= 1000000) { return 7; }
            if (v >= 100000) { return 6; }
            if (v >= 10000) { return 5; }
            if (v >= 1000) { return 4; }
            if (v >= 100) { return 3; }
            if (v >= 10) { return 2; }
            return 1;
        }

        // Returns e == 0 ? 1 : [log_2(5^e)]; requires 0 <= e <= 3528.
        static int32_t log2pow5(int32_t e)
        {
            // This approximation works up to the point that the multiplication overflows at e = 3529.
            // If the multiplication were done in 64 bits, it would fail at 5^4004 which is just greater
            // than 2^9297.
            assert(e >= 0);
            assert(e <= 3528);
            return (int32_t)((((uint32_t)e) * 1217359) >> 19);
        }

        // Returns e == 0 ? 1 : ceil(log_2(5^e)); requires 0 <= e <= 3528.
        static int32_t pow5bits(int32_t e)
        {
            // This approximation works up to the point that the multiplication overflows at e = 3529.
            // If the multiplication were done in 64 bits, it would fail at 5^4004 which is just greater
            // than 2^9297.
            assert(e >= 0);
            assert(e <= 3528);
            return (int32_t)(((((uint32_t)e) * 1217359) >> 19) + 1);
        }

        // Returns e == 0 ? 1 : ceil(log_2(5^e)); requires 0 <= e <= 3528.
        static int32_t ceil_log2pow5(int32_t e)
        {
            return log2pow5(e) + 1;
        }

        // Returns floor(log_10(2^e)); requires 0 <= e <= 1650.
        static uint32_t log10Pow2(int32_t e)
        {
            // The first value this approximation fails for is 2^1651 which is just greater than 10^297.
            assert(e >= 0);
            assert(e <= 1650);
            return (((uint32_t)e) * 78913) >> 18;
        }

        // Returns floor(log_10(5^e)); requires 0 <= e <= 2620.
        static uint32_t log10Pow5(int32_t e)
        {
            // The first value this approximation fails for is 5^2621 which is just greater than 10^1832.
            assert(e >= 0);
            assert(e <= 2620);
            return (((uint32_t)e) * 732923) >> 20;
        }

        static int copy_special_str(AlphaSpan result, bool sign, bool exponent, bool mantissa)
        {
            int offset = 0;

            if (mantissa)
            {
                result[offset++] = (byte)'N';
                result[offset++] = (byte)'a';
                result[offset++] = (byte)'N';

                return offset;
            }

            if (sign)
            {
                result[offset++] = (byte)'-';
            }

            if (exponent)
            {
                result[offset++] = (byte)'I';
                result[offset++] = (byte)'n';
                result[offset++] = (byte)'f';
                result[offset++] = (byte)'i';
                result[offset++] = (byte)'n';
                result[offset++] = (byte)'i';
                result[offset++] = (byte)'t';
                result[offset++] = (byte)'y';

                return offset;
            }

            result[offset++] = (byte)'0';
            result[offset++] = (byte)'E';
            result[offset++] = (byte)'0';

            return offset;
        }

        static uint32_t float_to_bits(float f)
        {
            return *(uint32_t*)&f;
        }

        internal static uint64_t double_to_bits(double d)
        {
            return *(uint64_t*)&d;
        }

        static void memcpy(AlphaSpan _Dst, string _Src)
        {
            for (int i = 0; i < _Src.Length; ++i) _Dst[i] = (byte)_Src[i];
        }

        static void memset(AlphaSpan _Dst, int start,byte _Val, uint32_t _Size )
        {
            for (int i = 0; i < _Size; i++)
            {
                _Dst[start++] = _Val;
            }
        }

        static uint32_t __builtin_clzll(uint64_t value)
        {
            uint32_t r = 0;

            if ((value & 0xffffffff00000000UL) == 0)
            {
                r += 32;
                value <<= 32;
            }

            if ((value & 0xffff000000000000UL) == 0)
            {
                r += 16;
                value <<= 16;
            }

            if ((value & 0xff00000000000000UL) == 0)
            {
                r += 8;
                value <<= 8;
            }

            if ((value & 0xf000000000000000UL) == 0)
            {
                r += 4;
                value <<= 4;
            }

            if ((value & 0xC000000000000000UL) == 0)
            {
                r += 2;
                value <<= 2;
            }

            if ((value & 0x8000000000000000UL) == 0)
            {
                r += 1;
                value <<= 1;
            }

            return r;
        }

#if NDEBUG
        static void assert(bool expression) { }
#else
        static void assert(bool expression)
        {
            if (!expression)
            {
                throw new AssertException();
            }
        }
#endif
    }
}