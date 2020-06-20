﻿using static Ryu.Net.Status;
using int32_t = System.Int32;
using uint32_t = System.UInt32;
using uint64_t = System.UInt64;


namespace Ryu.Net
{
    unsafe partial class Ryu
    {
        const int DOUBLE_EXPONENT_BIAS = 1023;


        static uint32_t floor_log2(uint64_t value)
        {
            return 63 - __builtin_clzll(value);
        }



        // The max function is already defined on Windows.
        static int32_t max32(int32_t a, int32_t b)
        {
            return a < b ? b : a;
        }

        static double int64Bits2Double(uint64_t bits)
        {
            return *(double*)&bits;
        }

        public static Status s2d_n(char* buffer, int len, double* result)
        {
            if (len == 0)
            {
                return INPUT_TOO_SHORT;
            }
            int m10digits = 0;
            int e10digits = 0;
            int dotIndex = len;
            int eIndex = len;
            uint64_t m10 = 0;
            int32_t e10 = 0;
            bool signedM = false;
            bool signedE = false;
            int i = 0;
            if (buffer[i] == '-')
            {
                signedM = true;
                i++;
            }
            for (; i < len; i++)
            {
                char c = buffer[i];
                if (c == '.')
                {
                    if (dotIndex != len)
                    {
                        return MALFORMED_INPUT;
                    }
                    dotIndex = i;
                    continue;
                }
                if ((c < '0') || (c > '9'))
                {
                    break;
                }
                if (m10digits >= 17)
                {
                    return INPUT_TOO_LONG;
                }
                m10 = 10 * m10 + (char)(c - '0');
                if (m10 != 0)
                {
                    m10digits++;
                }
            }
            if (i < len && ((buffer[i] == 'e') || (buffer[i] == 'E')))
            {
                eIndex = i;
                i++;
                if (i < len && ((buffer[i] == '-') || (buffer[i] == '+')))
                {
                    signedE = buffer[i] == '-';
                    i++;
                }
                for (; i < len; i++)
                {
                    char c = buffer[i];
                    if ((c < '0') || (c > '9'))
                    {
                        return MALFORMED_INPUT;
                    }
                    if (e10digits > 3)
                    {
                        // TODO: Be more lenient. Return +/-Infinity or +/-0 instead.
                        return INPUT_TOO_LONG;
                    }
                    e10 = 10 * e10 + (c - '0');
                    if (e10 != 0)
                    {
                        e10digits++;
                    }
                }
            }
            if (i < len)
            {
                return MALFORMED_INPUT;
            }
            if (signedE)
            {
                e10 = -e10;
            }
            e10 -= dotIndex < eIndex ? eIndex - dotIndex - 1 : 0;
            if (m10 == 0)
            {
                *result = signedM ? -0.0 : 0.0;
                return SUCCESS;
            }


            if ((m10digits + e10 <= -324) || (m10 == 0))
            {
                // Number is less than 1e-324, which should be rounded down to 0; return +/-0.0.
                uint64_t ieee = ((uint64_t)(signedM ? 1 : 0)) << (DOUBLE_EXPONENT_BITS + DOUBLE_MANTISSA_BITS);
                *result = int64Bits2Double(ieee);
                return SUCCESS;
            }
            if (m10digits + e10 >= 310)
            {
                // Number is larger than 1e+309, which should be rounded down to 0; return +/-Infinity.
                uint64_t ieee = (((uint64_t)(signedM ? 1 : 0)) << (DOUBLE_EXPONENT_BITS + DOUBLE_MANTISSA_BITS)) | (0x7fful << DOUBLE_MANTISSA_BITS);
                *result = int64Bits2Double(ieee);
                return SUCCESS;
            }

            // Convert to binary float m2 * 2^e2, while retaining information about whether the conversion
            // was exact (trailingZeros).
            int32_t e2;
            uint64_t m2;
            bool trailingZeros;
            if (e10 >= 0)
            {
                // The length of m * 10^e in bits is:
                //   log2(m10 * 10^e10) = log2(m10) + e10 log2(10) = log2(m10) + e10 + e10 * log2(5)
                //
                // We want to compute the DOUBLE_MANTISSA_BITS + 1 top-most bits (+1 for the implicit leading
                // one in IEEE format). We therefore choose a binary output exponent of
                //   log2(m10 * 10^e10) - (DOUBLE_MANTISSA_BITS + 1).
                //
                // We use floor(log2(5^e10)) so that we get at least this many bits; better to
                // have an additional bit than to not have enough bits.
                e2 = (int32_t)(floor_log2(m10) + e10 + log2pow5(e10) - (DOUBLE_MANTISSA_BITS + 1));

                // We now compute [m10 * 10^e10 / 2^e2] = [m10 * 5^e10 / 2^(e2-e10)].
                // To that end, we use the DOUBLE_POW5_SPLIT table.
                int j = e2 - e10 - ceil_log2pow5(e10) + DOUBLE_POW5_BITCOUNT;
                assert(j >= 0);
                assert(e10 < DOUBLE_POW5_TABLE_SIZE);
                m2 = mulShift64(m10, DOUBLE_POW5_SPLIT[(uint)e10], j);

                // We also compute if the result is exact, i.e.,
                //   [m10 * 10^e10 / 2^e2] == m10 * 10^e10 / 2^e2.
                // This can only be the case if 2^e2 divides m10 * 10^e10, which in turn requires that the
                // largest power of 2 that divides m10 + e10 is greater than e2. If e2 is less than e10, then
                // the result must be exact. Otherwise we use the existing multipleOfPowerOf2 function.
                trailingZeros = e2 < e10 || (e2 - e10 < 64 && multipleOfPowerOf2(m10, (uint32_t)(e2 - e10)));
            }
            else
            {
                e2 = (int32_t)(floor_log2(m10) + e10 - ceil_log2pow5(-e10) - (DOUBLE_MANTISSA_BITS + 1));
                int j = e2 - e10 + ceil_log2pow5(-e10) - 1 + DOUBLE_POW5_INV_BITCOUNT;
                assert(-e10 < DOUBLE_POW5_INV_TABLE_SIZE);
                m2 = mulShift64(m10, DOUBLE_POW5_INV_SPLIT[(uint)(-e10)], j);
                trailingZeros = multipleOfPowerOf5(m10, (uint)(-e10));
            }


            // Compute the final IEEE exponent.
            uint32_t ieee_e2 = (uint32_t)max32(0, (int32_t)(e2 + DOUBLE_EXPONENT_BIAS + floor_log2(m2)));

            if (ieee_e2 > 0x7fe)
            {
                // Final IEEE exponent is larger than the maximum representable; return +/-Infinity.
                uint64_t ieee = (((uint64_t)(signedM ? 1 : 0)) << (DOUBLE_EXPONENT_BITS + DOUBLE_MANTISSA_BITS)) | (0x7fful << DOUBLE_MANTISSA_BITS);
                *result = int64Bits2Double(ieee);
                return SUCCESS;
            }

            // We need to figure out how much we need to shift m2. The tricky part is that we need to take
            // the final IEEE exponent into account, so we need to reverse the bias and also special-case
            // the value 0.
            int32_t shift = (int32_t)((ieee_e2 == 0 ? 1 : ieee_e2) - e2 - DOUBLE_EXPONENT_BIAS - DOUBLE_MANTISSA_BITS);
            assert(shift >= 0);


            // We need to round up if the exact value is more than 0.5 above the value we computed. That's
            // equivalent to checking if the last removed bit was 1 and either the value was not just
            // trailing zeros or the result would otherwise be odd.
            //
            // We need to update trailingZeros given that we have the exact output exponent ieee_e2 now.
            trailingZeros &= (m2 & ((1ul << (shift - 1)) - 1)) == 0;
            uint64_t lastRemovedBit = (m2 >> (shift - 1)) & 1;
            bool roundUp = (lastRemovedBit != 0) && (!trailingZeros || (((m2 >> shift) & 1) != 0));


            uint64_t ieee_m2 = (m2 >> shift) + (roundUp ? 1U : 0);
            if (ieee_m2 == (1ul << (DOUBLE_MANTISSA_BITS + 1)))
            {
                // Due to how the IEEE represents +/-Infinity, we don't need to check for overflow here.
                ieee_e2++;
            }
            ieee_m2 &= (1ul << DOUBLE_MANTISSA_BITS) - 1;
            uint64_t ieee2 = (((((uint64_t)(signedM ? 1 : 0)) << DOUBLE_EXPONENT_BITS) | (uint64_t)ieee_e2) << DOUBLE_MANTISSA_BITS) | ieee_m2;
            *result = int64Bits2Double(ieee2);
            return SUCCESS;
        }

        static Status s2d(char* buffer, double* result)
        {
            return s2d_n(buffer, strlen(buffer), result);
        }
    }
}