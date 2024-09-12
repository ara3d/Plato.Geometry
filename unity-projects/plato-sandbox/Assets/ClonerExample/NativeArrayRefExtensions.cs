using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Assets.ClonerExample
{
    public static unsafe class NativeArrayRefExtensions
    {
        public static ref T GetRef<T>(this NativeArray<T> array, int index)
            where T : struct
            => ref UnsafeUtility.ArrayElementAsRef<T>(array.GetUnsafePtr(), index);

        public static void Resize<T>(this ref NativeArray<T> array, int size)
            where T : struct
        {
            if (array.IsCreated && array.Length != size)
                array.Dispose();
            if (!array.IsCreated)
                array = new NativeArray<T>(size, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
        }

        public static void Assign<T>(this ref NativeArray<T> array, T[] other)
            where T : unmanaged
        {
            array.Resize(other.Length);
            for (var i=0; i < other.Length; i++)
                array[i] = other[i];
        }

        public static void Assign<T0, T1>(this ref NativeArray<T1> array, T0[] other, Func<T0, T1> f)
            where T1 : unmanaged
        {
            array.Resize(other.Length);
            for (var i = 0; i < other.Length; i++)
                array[i] = f(other[i]);
        }

        public static void SafeDispose<T>(this ref NativeArray<T> self)
            where T : unmanaged
        {
            if (self.IsCreated)
                self.Dispose();
        }

        public static void SafeDispose<T>(this ref NativeQueue<T> self)
            where T : unmanaged
        {
            if (self.IsCreated)
                self.Dispose();
        }

        public static void SafeDispose<T>(this ref NativeList<T> self)
            where T : unmanaged
        {
            if (self.IsCreated)
                self.Dispose();
        }

        public static void Clear<T>(this ref NativeArray<T> self) where T : unmanaged
            => UnsafeUtility.MemClear(self.GetUnsafePtr(), (long)self.Length * sizeof(T));
    }
}