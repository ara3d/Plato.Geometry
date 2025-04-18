﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Plato.Geometry.Memory
{
    /// <summary>
    /// Represents a pinned managed array, providing low-level access to its memory.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    public unsafe class PinnedArray<T> : IReadOnlyList<T>, IMemoryBlock where T : unmanaged
    {
        private GCHandle _handle;

        /// <summary>
        /// Gets the pointer to the start of the pinned array.
        /// </summary>
        public IntPtr Pointer { get; private set; }

        /// <summary>
        /// Gets the size of the memory block in bytes.
        /// </summary>
        public long SizeInBytes { get; }

        /// <summary>
        /// Gets the underlying managed array.
        /// </summary>
        public T[] Array { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PinnedArray{T}"/> class, pinning the specified array.
        /// </summary>
        /// <param name="array">The array to pin.</param>
        public PinnedArray(T[] array)
        {
            Array = array ?? throw new ArgumentNullException(nameof(array));
            if (array.Length == 0)
                throw new ArgumentException("Array cannot be empty.", nameof(array));

            _handle = GCHandle.Alloc(array, GCHandleType.Pinned);
            Pointer = _handle.AddrOfPinnedObject();
            SizeInBytes = array.Length * sizeof(T);
        }

        /// <summary>
        /// Releases the pinned array.
        /// </summary>
        public void Dispose()
        {
            if (_handle.IsAllocated)
            {
                _handle.Free();
                Pointer = IntPtr.Zero;
            }
            // No need to call GC.SuppressFinalize since there's no finalizer
        }

        public IEnumerator<T> GetEnumerator() => (IEnumerator<T>)Array.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() =>Array.GetEnumerator();
        public int Count => Array.Length;
        public T this[int index] => Array[index];
    }
}