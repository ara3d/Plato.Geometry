using System;

namespace Plato.Geometry
{
    /// <summary>
    /// Represents a block of unmanaged memory.
    /// </summary>
    public interface IMemoryBlock : IDisposable
    {
        /// <summary>
        /// Gets the pointer to the start of the memory block.
        /// </summary>
        IntPtr Pointer { get; }

        /// <summary>
        /// Gets the size of the memory block in bytes.
        /// </summary>
        long SizeInBytes { get; }
    }
}