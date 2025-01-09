using System;
using System.Collections;

namespace Plato
{
    public readonly struct FunctionalArray<T> : IReadOnlyList<T>
    {
        public readonly Func<int, T> Func;
        public readonly int Count;
        public FunctionalArray(Func<int, T> func, int count) => (Func, Count) = (func, count);
        T IReadOnlyList<T>.this[int index] => Func(index);
        int IReadOnlyCollection<T>.Count => Count;
        public IEnumerator<T> GetEnumerator() => new Enumerator(Func, Count);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<T>
        {
            public Enumerator(Func<int, T> func, int count) => (Func, Count, Index) = (func, count, -1);
            public readonly Func<int, T> Func;
            public readonly int Count;
            public int Index;
            public T Current => Func(Index);
            object IEnumerator.Current => Current;
            public void Dispose() { }
            public bool MoveNext() => ++Index < Count;
            public void Reset() => Index = -1;
        }
    }
}