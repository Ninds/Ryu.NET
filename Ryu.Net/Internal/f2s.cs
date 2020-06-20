﻿using int32_t = System.Int32;
using uint32_t = System.UInt32;
using uint64_t = System.UInt64;
using uint8_t = System.Byte;

namespace Ryu.Net.Internal
{
    unsafe partial class Ryu
    {
        const int FLOAT_MANTISSA_BITS = 23;
        const int FLOAT_EXPONENT_BITS = 8;
        const int FLOAT_BIAS = 127;

        static uint32_t pow5factor_32(uint32_t value)
        {
            uint32_t count = 0;
            for (; ; )
            {
                assert(value != 0);
                uint32_t q = value / 5;
                uint32_t r = value % 5;
                if (r != 0)
                {
                    break;
                }
                value = q;
                ++count;
            }
            return count;
        }

        // Returns true if value is divisible by 5^p.
        static bool multipleOfPowerOf5_32(uint32_t value, uint32_t p)
        {
            return pow5factor_32(value) >= p;
        }

        // Returns true if value is divisible by 2^p.
        static bool multipleOfPowerOf2_32(uint32_t value, uint32_t p)
        {
            // __builtin_ctz doesn't appear to be faster here.
            return (value & ((1u << (int)p) - 1)) == 0;
        }

        // It seems to be slightly faster to avoid uint128_t here, although the
        // generated code for uint128_t looks slightly nicer.
        static uint32_t mulShift32(uint32_t m, uint64_t factor, int32_t shift)
        {
            assert(shift > 32);

            // The casts here help MSVC to avoid calls to the __allmul library
            // function.
            uint32_t factorLo = (uint32_t)(factor);
            uint32_t factorHi = (uint32_t)(factor >> 32);
            uint64_t bits0 = (uint64_t)m * factorLo;
            uint64_t bits1 = (uint64_t)m * factorHi;

            uint64_t sum = (bits0 >> 32) + bits1;
            uint64_t shiftedSum = sum >> (shift - 32);
            assert(shiftedSum <= /*UINT32_MAX*/uint32_t.MaxValue);
            return (uint32_t)shiftedSum;
        }

        static uint32_t mulPow5InvDivPow2(uint32_t m, uint32_t q, int32_t j)
        {
            return mulShift32(m, FLOAT_POW5_INV_SPLIT[q], j);
        }

        static uint32_t mulPow5divPow2(uint32_t m, uint32_t i, int32_t j)
        {
            return mulShift32(m, FLOAT_POW5_SPLIT[i], j);
        }

        static floating_decimal_32 f2d(uint32_t ieeeMantissa, uint32_t ieeeExponent)
        {
            int32_t e2;
            uint32_t m2;
            if (ieeeExponent == 0)
            {
                // We subtract 2 so that the bounds computation has 2 additional bits.
                e2 = 1 - FLOAT_BIAS - FLOAT_MANTISSA_BITS - 2;
                m2 = ieeeMantissa;
            }
            else
            {
                e2 = (int32_t)ieeeExponent - FLOAT_BIAS - FLOAT_MANTISSA_BITS - 2;
                m2 = (1u << FLOAT_MANTISSA_BITS) | ieeeMantissa;
            }
            bool even = (m2 & 1) == 0;
            bool acceptBounds = even;


            // Step 2: Determine the interval of valid decimal representations.
            uint32_t mv = 4 * m2;
            uint32_t mp = 4 * m2 + 2;
            // Implicit bool -> int conversion. True is 1, false is 0.
            uint32_t mmShift = (ieeeMantissa != 0 || ieeeExponent <= 1) ? 1U : 0;
            uint32_t mm = 4 * m2 - 1 - mmShift;

            // Step 3: Convert to a decimal power base using 64-bit arithmetic.
            uint32_t vr, vp, vm;
            int32_t e10;
            bool vmIsTrailingZeros = false;
            bool vrIsTrailingZeros = false;
            uint8_t lastRemovedDigit = 0;
            if (e2 >= 0)
            {
                uint32_t q = log10Pow2(e2);
                e10 = (int32_t)q;
                int32_t k = FLOAT_POW5_INV_BITCOUNT + pow5bits((int32_t)q) - 1;
                int32_t i = -e2 + (int32_t)q + k;
                vr = mulPow5InvDivPow2(mv, q, i);
                vp = mulPow5InvDivPow2(mp, q, i);
                vm = mulPow5InvDivPow2(mm, q, i);

                if (q != 0 && (vp - 1) / 10 <= vm / 10)
                {
                    // We need to know one removed digit even if we are not going to loop below. We could use
                    // q = X - 1 above, except that would require 33 bits for the result, and we've found that
                    // 32-bit arithmetic is faster even on 64-bit machines.
                    int32_t l = FLOAT_POW5_INV_BITCOUNT + pow5bits((int32_t)(q - 1)) - 1;
                    lastRemovedDigit = (uint8_t)(mulPow5InvDivPow2(mv, q - 1, -e2 + (int32_t)q - 1 + l) % 10);
                }
                if (q <= 9)
                {
                    // The largest power of 5 that fits in 24 bits is 5^10, but q <= 9 seems to be safe as well.
                    // Only one of mp, mv, and mm can be a multiple of 5, if any.
                    if (mv % 5 == 0)
                    {
                        vrIsTrailingZeros = multipleOfPowerOf5_32(mv, q);
                    }
                    else if (acceptBounds)
                    {
                        vmIsTrailingZeros = multipleOfPowerOf5_32(mm, q);
                    }
                    else
                    {
                        if (multipleOfPowerOf5_32(mp, q)) --vp;
                    }
                }
            }
            else
            {
                uint32_t q = log10Pow5(-e2);
                e10 = (int32_t)q + e2;
                int32_t i = -e2 - (int32_t)q;
                int32_t k = pow5bits(i) - FLOAT_POW5_BITCOUNT;
                int32_t j = (int32_t)q - k;
                vr = mulPow5divPow2(mv, (uint32_t)i, j);
                vp = mulPow5divPow2(mp, (uint32_t)i, j);
                vm = mulPow5divPow2(mm, (uint32_t)i, j);

                if (q != 0 && (vp - 1) / 10 <= vm / 10)
                {
                    j = (int32_t)q - 1 - (pow5bits(i + 1) - FLOAT_POW5_BITCOUNT);
                    lastRemovedDigit = (uint8_t)(mulPow5divPow2(mv, (uint32_t)(i + 1), j) % 10);
                }
                if (q <= 1)
                {
                    // {vr,vp,vm} is trailing zeros if {mv,mp,mm} has at least q trailing 0 bits.
                    // mv = 4 * m2, so it always has at least two trailing 0 bits.
                    vrIsTrailingZeros = true;
                    if (acceptBounds)
                    {
                        // mm = mv - 1 - mmShift, so it has 1 trailing 0 bit iff mmShift == 1.
                        vmIsTrailingZeros = mmShift == 1;
                    }
                    else
                    {
                        // mp = mv + 2, so it always has at least one trailing 0 bit.
                        --vp;
                    }
                }
                else if (q < 31)
                { // TODO(ulfjack): Use a tighter bound here.
                    vrIsTrailingZeros = multipleOfPowerOf2_32(mv, q - 1);

                }
            }


            // Step 4: Find the shortest decimal representation in the interval of valid representations.
            int32_t removed = 0;
            uint32_t output;
            if (vmIsTrailingZeros || vrIsTrailingZeros)
            {
                // General case, which happens rarely (~4.0%).
                while (vp / 10 > vm / 10)
                {
#if __clang__ // https://bugs.llvm.org/show_bug.cgi?id=23106
                    // The compiler does not realize that vm % 10 can be computed from vm / 10
                    // as vm - (vm / 10) * 10.
                    vmIsTrailingZeros &= vm - (vm / 10) * 10 == 0;
#else
                    vmIsTrailingZeros &= vm % 10 == 0;
#endif
                    vrIsTrailingZeros &= lastRemovedDigit == 0;
                    lastRemovedDigit = (uint8_t)(vr % 10);
                    vr /= 10;
                    vp /= 10;
                    vm /= 10;
                    ++removed;
                }

                if (vmIsTrailingZeros)
                {
                    while (vm % 10 == 0)
                    {
                        vrIsTrailingZeros &= lastRemovedDigit == 0;
                        lastRemovedDigit = (uint8_t)(vr % 10);
                        vr /= 10;
                        vp /= 10;
                        vm /= 10;
                        ++removed;
                    }
                }

                if (vrIsTrailingZeros && lastRemovedDigit == 5 && vr % 2 == 0)
                {
                    // Round even if the exact number is .....50..0.
                    lastRemovedDigit = 4;
                }
                // We need to take vr + 1 if vr is outside bounds or we need to round up.
                output = vr;
                if ((vr == vm && (!acceptBounds || !vmIsTrailingZeros)) || lastRemovedDigit >= 5) ++output;
            }
            else
            {
                // Specialized for the common case (~96.0%). Percentages below are relative to this.
                // Loop iterations below (approximately):
                // 0: 13.6%, 1: 70.7%, 2: 14.1%, 3: 1.39%, 4: 0.14%, 5+: 0.01%
                while (vp / 10 > vm / 10)
                {
                    lastRemovedDigit = (uint8_t)(vr % 10);
                    vr /= 10;
                    vp /= 10;
                    vm /= 10;
                    ++removed;
                }

                // We need to take vr + 1 if vr is outside bounds or we need to round up.
                output = vr;
                if (vr == vm || lastRemovedDigit >= 5) ++output;
            }
            int32_t exp = e10 + removed;



            floating_decimal_32 fd = default;
            fd.exponent = exp;
            fd.mantissa = output;
            return fd;
        }

        static int to_chars(floating_decimal_32 v, bool sign, char* result)
        {
            // Step 5: Print the decimal representation.
            int index = 0;
            if (sign)
            {
                result[index++] = '-';
            }

            uint32_t output = v.mantissa;
            uint32_t olength = decimalLength9(output);


            // Print the decimal digits.
            // The following code is equivalent to:
            // for (uint32_t i = 0; i < olength - 1; ++i) {
            //   const uint32_t c = output % 10; output /= 10;
            //   result[index + olength - i] = (char) ('0' + c);
            // }
            // result[index] = '0' + output % 10;
            uint32_t i = 0;
            while (output >= 10000)
            {
#if __clang__ // https://bugs.llvm.org/show_bug.cgi?id=38217
                uint32_t c = output - 10000 * (output / 10000);
#else
                uint32_t c = output % 10000;
#endif
                output /= 10000;
                uint32_t c0 = (c % 100) << 1;
                uint32_t c1 = (c / 100) << 1;
                memcpy(result + index + olength - i - 1, DIGIT_TABLE + c0, 2);
                memcpy(result + index + olength - i - 3, DIGIT_TABLE + c1, 2);
                i += 4;
            }
            if (output >= 100)
            {
                uint32_t c = (output % 100) << 1;
                output /= 100;
                memcpy(result + index + olength - i - 1, DIGIT_TABLE + c, 2);
                i += 2;
            }
            if (output >= 10)
            {
                uint32_t c = output << 1;
                // We can't use memcpy here: the decimal dot goes between these two digits.
                result[index + olength - i] = DIGIT_TABLE[c + 1];
                result[index] = DIGIT_TABLE[c];
            }
            else
            {
                result[index] = (char)('0' + output);
            }

            // Print decimal point if needed.
            if (olength > 1)
            {
                result[index + 1] = '.';
                index += (int)olength + 1;
            }
            else
            {
                ++index;
            }

            // Print the exponent.
            result[index++] = 'E';
            int32_t exp = v.exponent + (int32_t)olength - 1;
            if (exp < 0)
            {
                result[index++] = '-';
                exp = -exp;
            }

            if (exp >= 10)
            {
                memcpy(result + index, DIGIT_TABLE + (uint)(2 * exp), 2);
                index += 2;
            }
            else
            {
                result[index++] = (char)('0' + exp);
            }

            return index;
        }

        public static int f2s_buffered_n(float f, char* result)
        {
            // Step 1: Decode the floating-point number, and unify normalized and subnormal cases.
            uint32_t bits = float_to_bits(f);



            // Decode bits into sign, mantissa, and exponent.
            bool ieeeSign = ((bits >> (FLOAT_MANTISSA_BITS + FLOAT_EXPONENT_BITS)) & 1) != 0;
            uint32_t ieeeMantissa = bits & ((1u << FLOAT_MANTISSA_BITS) - 1);
            uint32_t ieeeExponent = (bits >> FLOAT_MANTISSA_BITS) & ((1u << FLOAT_EXPONENT_BITS) - 1);

            // Case distinction; exit early for the easy cases.
            if (ieeeExponent == ((1u << FLOAT_EXPONENT_BITS) - 1u) || (ieeeExponent == 0 && ieeeMantissa == 0))
            {
                return copy_special_str(result, ieeeSign, ieeeExponent != 0, ieeeMantissa != 0);
            }

            floating_decimal_32 v = f2d(ieeeMantissa, ieeeExponent);
            return to_chars(v, ieeeSign, result);
        }

        public static void f2s_buffered(float f, char* result)
        {
            int index = f2s_buffered_n(f, result);

            // Terminate the string.
            result[index] = '\0';
        }

    }
}