using System;
using System.Runtime.InteropServices;

namespace RyuDotNet.Internal
{
    internal ref struct AlphaSpan
    {
        readonly int _shift;
        readonly Span<byte> _span;

        public AlphaSpan(Span<byte> span) { _shift = 0; _span = span; }
        public AlphaSpan(Span<char> span) { _shift = 1; _span = MemoryMarshal.AsBytes(span); }
        private AlphaSpan(Span<byte> span, int shift) { _shift = shift; _span = span; }

        
        public byte this[int index]
        {
            get
            {
                return _span[index << _shift];
            }
            set
            {
                _span[index << _shift] = value;
            }
        }
       
        public int Length => _span.Length >> _shift;


        public AlphaSpan Slice(int start)
        {
            return new AlphaSpan(_span.Slice(start << _shift), _shift);
        }

        public AlphaSpan Slice(int start, int length)
        {
            return new AlphaSpan(_span.Slice(start << _shift, length << _shift), _shift);
        }

        public void Fill(byte value)
        {
            if (_shift == 0) _span.Fill(value);
            else
            {
                for(int i =0; i < _span.Length >> _shift; ++i)
                {
                    _span[i << _shift] = value;
                }
            }
        }

    }

    internal ref struct ReadOnlyAlphaSpan
    {
        readonly int _shift;
        readonly ReadOnlySpan<byte> _span;

        public ReadOnlyAlphaSpan(ReadOnlySpan<byte> span) { _shift = 0; _span = span; }
        public ReadOnlyAlphaSpan(ReadOnlySpan<char> span) { _shift = 1; _span = MemoryMarshal.AsBytes(span); }
        private ReadOnlyAlphaSpan(ReadOnlySpan<byte> span, int shift) { _shift = shift; _span = span; }

        public byte this[int index] => _span[index << _shift];
            

        public int Length => _span.Length >> _shift;

        public ReadOnlyAlphaSpan Slice(int start)
        {
            return new ReadOnlyAlphaSpan(_span.Slice(start << _shift), _shift);
        }

        public ReadOnlyAlphaSpan Slice(int start, int length)
        {
            return new ReadOnlyAlphaSpan(_span.Slice(start << _shift, length << _shift), _shift);
        }

    }

}
