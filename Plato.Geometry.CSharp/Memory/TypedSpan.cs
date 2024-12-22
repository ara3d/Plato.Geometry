using System;
using System.Collections;
using System.Collections.Generic;

namespace Plato.Geometry.Memory
{
    public readonly unsafe struct TypedSpan<T> : IReadOnlyList<T>
        where T : unmanaged
    {
        public TypedSpan(T* pointer, int count)
        {
            Pointer = pointer;
            Count = count;
        }

        public readonly T* Pointer;
        public long ByteLength => Count * sizeof(T);
        public byte* BytePointer => (byte*)Pointer;
        
        public IEnumerator<T> GetEnumerator()
        {
            for (var i=0; i < Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count { get; }
        public T this[int index] => Pointer[index];
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