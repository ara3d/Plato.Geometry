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

        
    }
}
