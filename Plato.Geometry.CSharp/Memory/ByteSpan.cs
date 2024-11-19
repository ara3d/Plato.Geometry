using System;

namespace Plato.Geometry.Memory
{
    public readonly unsafe struct ByteSpan
    {
        public readonly byte* Pointer;
        public readonly uint Length;

        public ByteSpan(byte* pointer, uint length)
        {
            Pointer = pointer;
            Length = length;
        }
    }
}