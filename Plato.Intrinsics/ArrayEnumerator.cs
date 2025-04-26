﻿using System.Collections;
using System.Runtime.CompilerServices;

namespace Plato
{
    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    public struct ArrayEnumerator<T>(IReadOnlyList<T> array) : IEnumerator<T>
    {
        public readonly IReadOnlyList<T> Array = array;
        public int Index = -1;
        public readonly int Count = array.Count;

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