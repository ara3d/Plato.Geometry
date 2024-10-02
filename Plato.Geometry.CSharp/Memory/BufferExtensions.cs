using System;

namespace Plato.Geometry.Memory
{
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