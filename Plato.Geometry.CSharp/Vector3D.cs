using System;

namespace Plato.DoublePrecision
{
    public partial struct Vector3D : IDeformable3D<Vector3D>
    {
        public static Vector3D UnitX = (1, 0, 0);
        public static Vector3D UnitY = (0, 1, 0);
        public static Vector3D UnitZ = (0, 0, 1);

        public Vector3D Abs
            => (X.Abs, Y.Abs, Z.Abs);

        public Quaternion LookRotation 
            => Quaternion.GetLookRotation(this);

        public Vector3D Deform(Func<Vector3D, Vector3D> f)
            => f(this);

        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);
        
        public Vector3D WithComponent(Integer axis, Number n)
            => axis == 0 ? (n, Y, Z) 
                : axis == 1 ? (X, n, Z)
                    : axis == 2 ? (X, Y, n)
                    : throw new IndexOutOfRangeException();
    }
}