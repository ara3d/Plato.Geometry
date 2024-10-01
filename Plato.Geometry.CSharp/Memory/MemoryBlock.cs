using System;
using System.Runtime.InteropServices;

namespace Plato.Geometry
{
    /// <summary>
    /// Represents a block of unmanaged memory with a specified alignment.
    /// </summary>
    public class MemoryBlock : IMemoryBlock
    {
        /// <summary>
        /// Gets the aligned pointer to the start of the memory block.
        /// </summary>
        public IntPtr Pointer { get; private set; }

        /// <summary>
        /// Gets the size of the memory block in bytes.
        /// </summary>
        public long SizeInBytes { get; }

        /// <summary>
        /// Gets the unaligned pointer returned by the memory allocation function.
        /// </summary>
        public IntPtr UnalignedPointer { get; private set; }

        /// <summary>
        /// Gets the alignment of the memory block in bytes.
        /// </summary>
        public int Alignment { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryBlock"/> class with the specified size and alignment.
        /// </summary>
        /// <param name="sizeInBytes">The size of the memory block in bytes.</param>
        /// <param name="alignment">The alignment in bytes.</param>
        public MemoryBlock(long sizeInBytes, int alignment)
        {
            if (sizeInBytes <= 0)
                throw new ArgumentOutOfRangeException(nameof(sizeInBytes), "Size must be positive.");
            if (alignment <= 0 || (alignment & (alignment - 1)) != 0)
                throw new ArgumentException("Alignment must be a positive power of two.", nameof(alignment));

            Alignment = alignment;
            var paddedSize = sizeInBytes + Alignment;
            SizeInBytes = sizeInBytes;
            UnalignedPointer = Marshal.AllocHGlobal(new IntPtr(paddedSize));

            if (UnalignedPointer == IntPtr.Zero)
                throw new OutOfMemoryException("Failed to allocate unmanaged memory.");

            var rawAddress = UnalignedPointer.ToInt64();
            var offset = (Alignment - (rawAddress % Alignment)) % Alignment;
            Pointer = new IntPtr(rawAddress + offset);
        }

        /// <summary>
        /// Releases the unmanaged memory.
        /// </summary>
        public void Dispose()
        {
            if (UnalignedPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(UnalignedPointer);
                UnalignedPointer = IntPtr.Zero;
            }
            Pointer = IntPtr.Zero;
            // No need to call GC.SuppressFinalize since there's no finalizer
        }

        // Removed the finalizer since we are calling Dispose manually and there's no unmanaged resources held directly.
    }
}