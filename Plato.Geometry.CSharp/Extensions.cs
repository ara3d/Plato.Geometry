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

        public static Matrix4x4 Transpose(this Matrix4x4 m)
            => (
                (m.M11, m.M12, m.M13, m.M14),
                (m.M21, m.M22, m.M23, m.M24),
                (m.M31, m.M32, m.M33, m.M34),
                (m.M41, m.M42, m.M43, m.M44));
    }
}
