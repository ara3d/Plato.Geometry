using System;
using System.Collections.Generic;
using System.Linq;

namespace Plato.Geometry.Memory
{
    public static unsafe class BufferExtensions
    {
        public static long SizeInBytes<T>(this IBuffer<T> self) where T : unmanaged
            => self.Count * sizeof(T);
        
        public static IBuffer<T1> Reinterpret<T0, T1>(this IBuffer<T0> self) where T0 : unmanaged where T1 : unmanaged
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

        public static IBuffer<T> ToTemporaryBuffer<T>(this IntPtr self, int elementCount) where T : unmanaged
            => new Buffer<T>(new ExternalMemoryBlock(self, elementCount * sizeof(T)), false);

        public static T[] Clone<T>(this IBuffer<T> self) where T : unmanaged
        {
            var result = new T[self.Count];
            for (var i = 0; i < self.Count; ++i)
                result[i] = self[i];
            return result;
        }

        public static IBuffer<T> ToBuffer<T>(this T[] self) where T : unmanaged
            => new Buffer<T>(new PinnedArray<T>(self), true);

        public static IBuffer<T> ToBuffer<T>(this IEnumerable<T> self) where T : unmanaged
            => self.ToArray().ToBuffer();
    }
}