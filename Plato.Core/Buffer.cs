using System.Runtime.Intrinsics;

namespace Plato.Geometry
{
    public readonly unsafe struct Buffer<T> where T : unmanaged
    {
        public readonly T* Pointer;
        public readonly int Count;
        public ref T this[int index] => ref Pointer[index];
        public Buffer(T* ptr, int count)
        {
            Pointer = ptr;
            Count = count;
        }
    }
}