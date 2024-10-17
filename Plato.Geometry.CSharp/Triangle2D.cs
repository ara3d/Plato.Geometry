using System;

namespace Plato.DoublePrecision
{
    public partial struct Triangle2D
    {
        public static readonly Triangle2D Unit = 
            ((-0.5, -Math.Sqrt(3) / 2), 
            (-0.5, Math.Sqrt(3) / 2), 
            (0, 1));

        public Triangle3D To3D
            => (A.To3D, B.To3D, C.To3D);
    }
}