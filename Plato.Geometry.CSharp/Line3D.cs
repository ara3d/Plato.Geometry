using System;

namespace Plato.DoublePrecision
{
    public partial struct Line3D : IDeformable3D<Line3D>, IProcedural<Number, Vector3D>
    {
        public Line3D Deform(Func<Vector3D, Vector3D> f)
            => (f(A), f(B));

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

        public Line3D Reverse
            => (B, A);

        public Quad3D Extrude(Vector3D direction)
            => (A, B, B + direction, A + direction);

        public Vector3D Eval(Number t)
            => A.Lerp(B, t);

        public Ray3D Ray
            => this;
    }
}