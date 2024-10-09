namespace Plato.DoublePrecision
{
    public partial struct Matrix3x3
    {
        public Vector3D Row1 => (M11, M21, M31);
        public Vector3D Row2 => (M12, M22, M32);
        public Vector3D Row3 => (M13, M23, M33);

        public double M11 => Column1.X;
        public double M12 => Column2.X;
        public double M13 => Column3.X;
        public double M21 => Column1.Y;
        public double M22 => Column2.Y;
        public double M23 => Column3.Y;
        public double M31 => Column1.Z;
        public double M32 => Column2.Z;
        public double M33 => Column3.Z;

        public double Determinant
            => M11 * (M22 * M33 - M23 * M32)
               - M12 * (M21 * M33 - M23 * M31)
               + M13 * (M21 * M32 - M22 * M31);
    }
}