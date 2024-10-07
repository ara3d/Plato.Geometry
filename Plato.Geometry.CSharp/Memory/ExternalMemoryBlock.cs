using System;

namespace Plato.Geometry
{
    /// <summary>
    /// Represents a block of unmanaged memory provided externally, which is not owned by this instance.
    /// </summary>
    public class ExternalMemoryBlock : IMemoryBlock
    {
        /// <summary>
        /// Gets the pointer to the start of the memory block.
        /// </summary>
        public IntPtr Pointer { get; }

        /// <summary>
        /// Gets the size of the memory block in bytes.
        /// </summary>
        public long SizeInBytes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalMemoryBlock"/> class with the specified pointer and size.
        /// </summary>
        /// <param name="pointer">The pointer to the unmanaged memory.</param>
        /// <param name="sizeInBytes">The size of the memory block in bytes.</param>
        public ExternalMemoryBlock(IntPtr pointer, long sizeInBytes)
        {
            Pointer = pointer;
            SizeInBytes = sizeInBytes;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="ExternalMemoryBlock"/> instance.
        /// </summary>
        public void Dispose()
        {
            // No action needed since we do not own the memory
        }
    }
}