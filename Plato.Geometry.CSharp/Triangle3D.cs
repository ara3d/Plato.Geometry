using System;

namespace Plato.DoublePrecision
{
    public partial struct Triangle3D : IDeformable3D<Triangle3D>
    {
        public Triangle3D Deform(Func<Vector3D, Vector3D> f)
            => new Triangle3D(f(A), f(B), f(C));
        
        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);

        public static implicit operator PolyLine3D(Triangle3D t)
            => t.ToPolyLine3D(true);

        public static implicit operator PointArray(Triangle3D t)
            => t.ToPoints();

        public static implicit operator TriangleMesh(Triangle3D t)
            => t.ToTriangleMesh();
    }
}