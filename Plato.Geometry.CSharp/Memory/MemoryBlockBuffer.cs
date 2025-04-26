using System.Collections;
using System.Collections.Generic;
using Plato.Geometry.Memory;

namespace Plato.Geometry
{
    /// <summary>
    /// Represents a block of unmanaged memory with a specified alignment.
    /// </summary>
    public unsafe class MemoryBlockBuffer<T> : MemoryBlock, IBuffer<T>
        where T : unmanaged
    {
        public const int DefaultAlignment = 8;
        public static readonly int ElementSize = sizeof(T);
        public Buffer<T> Buffer { get; }

        public MemoryBlockBuffer(int numElements)
            : base(numElements * ElementSize, DefaultAlignment)
        {
            Buffer = new Buffer<T>(this, true);
        }

        public IEnumerator<T> GetEnumerator() => Buffer.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Buffer).GetEnumerator();
        int IReadOnlyCollection<T>.Count => Buffer.Count;
        public T At(Integer n) => Buffer.At(n);
        public T this[Integer n] { get => Buffer[n]; set => Buffer[n] = value; }
        public T this[int index] { get => Buffer[index]; set => Buffer[index] = value; }
        Integer IArray<T>.Count => Buffer.Count;
        public new T* Pointer => Buffer.Pointer;
    }
}