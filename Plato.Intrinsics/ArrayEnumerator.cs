using System.Collections;
using System.Runtime.CompilerServices;

namespace Plato
{
    public struct ArrayEnumerator<T> : IEnumerator<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayEnumerator(IArray<T> array)
        {
            Array = array;
            Index = -1;
            Count = array.Count;
        }

        public readonly IArray<T> Array;
        public int Index;
        public readonly int Count;

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Array[Index];
        }

        object IEnumerator.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Current; 
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose() { }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++Index < Count;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => Index = -1;
    }
}