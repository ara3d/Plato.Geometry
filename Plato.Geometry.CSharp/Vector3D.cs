using System;

namespace Plato.DoublePrecision
{
    public partial struct Vector3D : IDeformable3D<Vector3D>
    {
        public static Vector3D UnitX = (1, 0, 0);
        public static Vector3D UnitY = (0, 1, 0);
        public static Vector3D UnitZ = (0, 0, 1);

        public bool IsParallel(Vector3D v) 
            => Dot(v).Abs > 1 - 1e-10;

        public Vector3D Abs
            => (X.Abs, Y.Abs, Z.Abs);

        public Quaternion LookRotation 
            => Quaternion.GetLookRotation(this);

        public Vector3D Deform(Func<Vector3D, Vector3D> f)
            => f(this);

        public Vector3D Transform(Matrix4x4 matrix)
            => matrix.TransformPoint(this);

        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);

        ITransformable3D ITransformable3D.Transform(Matrix4x4 matrix)
            => Transform(matrix);
    }
}