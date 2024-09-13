using System;
using Plato.DoublePrecision;

namespace Plato.Geometry
{
    /// <summary>
    /// This is a factory class. It is used to create new instances of the various interfaces.
    /// </summary>
    public static class New
    {
        public static Array<T> Array<T>(params T[] items) 
            => Array(items.Length, i => items[i]);

        public static Array<T> Array<T>(Integer n, Func<Integer, T> f)
            => new LazyArray<T>(n, f);

        public static Array2D<T> Array<T>(Integer nCols, Integer nRows, Func<Integer, Integer, T> f)
            => new LazyArray2D<T>(nCols, nRows, f);
    }
}