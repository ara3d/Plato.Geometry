using System;
using System.Collections;
using System.Collections.Generic;

namespace Plato.Geometry.Memory
{
    public unsafe interface IBuffer<out T> 
        : IDisposable, IReadOnlyList<T> 
        where T : unmanaged
    {
        T* Pointer { get; }
    }

    /// <summary>
    /// Represents a buffer of unmanaged memory containing elements of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the buffer.</typeparam>
    public unsafe class Buffer<T> : IBuffer<T> where T : unmanaged
    {
        /// <summary>
        /// A pointer to raw memory.
        /// </summary>
        public T* Pointer { get; }

        /// <summary>
        /// Gets a value indicating whether this buffer owns the memory block.
        /// </summary>
        public bool IsOwner { get; }

        /// <summary>
        /// The block of memory. 
        /// </summary>
        public IMemoryBlock MemoryBlock { get; }

        /// <summary>
        /// Gets the size of each element in bytes.
        /// </summary>
        public int ElementSize => sizeof(T);

        /// <summary>
        /// Initializes a new instance of the <see cref="Buffer{T}"/> class with the specified memory block.
        /// </summary>
        /// <param name="memoryBlock">The memory block containing the data.</param>
        /// <param name="isOwner">Indicates whether this buffer owns the memory block.</param>
        public Buffer(IMemoryBlock memoryBlock, bool isOwner)
        {
            MemoryBlock = memoryBlock ?? throw new ArgumentNullException(nameof(memoryBlock));
            IsOwner = isOwner;
            Pointer = (T*)memoryBlock.Pointer;

            // Check alignment of _pointer to ElementSize bytes
            var address = (long)Pointer;
            if ((address % ElementSize) != 0)
            {
                throw new InvalidOperationException($"Pointer address {address} is not aligned to {ElementSize} bytes.");
            }

            if (memoryBlock.SizeInBytes % ElementSize != 0)
                throw new ArgumentException($"Memory size ({memoryBlock.SizeInBytes}) is not a multiple of the element size ({ElementSize}).");

            Count = (int)(memoryBlock.SizeInBytes / ElementSize);
        }

        /// <summary>
        /// Gets the number of elements in the buffer.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element.</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range.</exception>
        public T this[int index]
            => Pointer[index];

        /// <summary>
        /// Returns an enumerator that iterates through the buffer.
        /// </summary>
        /// <returns>An enumerator for the buffer.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return this[i];
        }

        /// <summary>
        /// Forwards the implementation of the non-generic IEnumerable.GetEnumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="Buffer{T}"/> instance.
        /// </summary>
        public void Dispose()
        {
            if (IsOwner)
            {
                MemoryBlock.Dispose();
            }
        }
    }

    public static unsafe class BufferExtensions
    {
        public static long SizeInBytes<T>(this IBuffer<T> self) where T : unmanaged
            => self.Count * sizeof(T);
        
        public static IBuffer<T1> Cast<T0, T1>(this IBuffer<T0> self) where T0 : unmanaged where T1 : unmanaged
        {
            var p = new IntPtr(self.Pointer);
            var mem = new ExternalMemoryBlock(p, self.SizeInBytes());
            return new Buffer<T1>(mem, false);
        }

        public static IBuffer<T> Slice<T>(this IBuffer<T> self, int start, int count) where T : unmanaged
        {
            if (start < 0 || start >= self.Count)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (count < 0 || start + count > self.Count)
                throw new ArgumentOutOfRangeException(nameof(count));
            var p = self.Pointer + start;
            var mem = new ExternalMemoryBlock(new IntPtr(p), count * sizeof(T));
            return new Buffer<T>(mem, false);
        }
    }
}
