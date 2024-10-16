using System;

namespace Plato.DoublePrecision
{
    public partial struct Line3D : IDeformable3D<Line3D>
    {
        public Line3D Deform(Func<Vector3D, Vector3D> f)
            => new Line3D(f(A), f(B));

        public Line3D Transform(Matrix4x4 matrix)
            => Deform(matrix.TransformPoint);

        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);

        ITransformable3D ITransformable3D.Transform(Matrix4x4 matrix)
            => Transform(matrix);

        public static implicit operator PolyLine3D(Line3D line)
            => line.ToPolyLine3D(false);

        public static implicit operator PointArray(Line3D line)
            => line.ToPoints();
    }
}