namespace Plato.DoublePrecision
{
    public partial struct Vector3D
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
    }

    public partial struct Vector4D
    {
        public Vector3D Vector3D => (X, Y, Z);
    }
}