using System;
using System.Collections;
using System.Collections.Generic;
using Plato.DoublePrecision;

namespace Plato.Geometry.Memory
{
    /// <summary>
    /// Represents a buffer of unmanaged memory containing elements of type <typeparamref name="T"/>.
    /// The constructor takes an IMemoryBlock and a boolean indicating whether the buffer owns the memory block.
    /// If the buffer owns the memory block, it will dispose of it when the buffer is disposed.
    /// The constructor will check that the memory block is properly aligned and that the size is a multiple of the element size.
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
        public Buffer(IMemoryBlock memoryBlock, bool isOwner = true)
        {
            MemoryBlock = memoryBlock ?? throw new ArgumentNullException(nameof(memoryBlock));
            IsOwner = isOwner;
            Pointer = (T*)memoryBlock.Pointer;

            // Check alignment of _pointer to ElementSize bytes
            var address = (long)Pointer;
            if ((address % ElementSize) != 0)
                throw new InvalidOperationException($"Pointer address {address} is not aligned to {ElementSize} bytes.");

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

        // Implementation of IArray.Count
        Integer IArray<T>.Count => Count;

        // Implementation of IArray.At
        public T At(Integer n) => this[n];

        // Implementation of IArray indexer property
        public T this[Integer n] => this[n.Value];
    }
}
