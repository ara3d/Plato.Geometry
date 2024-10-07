using System;
using Plato.DoublePrecision;

namespace Plato.Geometry.Memory
{
    /// <summary>
    /// Presents a typed view of a range of properly aligned memory.
    /// Usually this is used in conjunction with IBuffer.  
    /// </summary>
    public unsafe interface IBuffer<T> 
        : IDisposable, IArray<T> 
        where T : unmanaged
    {
        T* Pointer { get; }
        new T this[int index] { get; set; }
    }
}