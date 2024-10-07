using System.Collections.Generic;
using Plato.DoublePrecision;
using Boolean = Plato.DoublePrecision.Boolean;

namespace Plato.Geometry
{
    /// <summary>
    /// Many of these functions, once validated, will move into the Plato library.
    /// </summary>
    public static class Extensions
    {
        public static IArray<T> ToIArray<T>(this IReadOnlyList<T> self) 
            => new ListArray<T>(self);
        
        public static IArray<T> ToIArray<T>(this T[] self) 
            => new PrimitiveArray<T>(self);

        public static IArray<T> Repeat<T>(this T self, int count)
            => ((Integer)count).MapRange(_ => self);

        public static IArray<T> MapRange<T>(this int count, System.Func<int, T> func)
            => ((Integer)count).MapRange(i => func(i));        
    }
}
