using System;

namespace Plato.Geometry.Memory
{
    public readonly unsafe struct TypedSpan<T> where T : unmanaged
    {
        public readonly T* Pointer;
        public readonly int Length;

        public TypedSpan(T* pointer, int length)
        {
            Pointer = pointer;
            Length = length;
        }
    }

    public static unsafe class SpanExtensions
    {
        public static void WithTypedSpan<T>(this T[] self, Action<TypedSpan<T>> action)
            where T: unmanaged
        {
            fixed (T* ptr = self)
            {
                action(new TypedSpan<T>(ptr, self.Length));
            }
        }
    }
}