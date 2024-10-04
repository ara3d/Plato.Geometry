using System;

namespace Plato.DoublePrecision
{
    public partial struct Quaternion
    {
        public Vector4D Vector4D => (X, Y, Z, W);
        public double Magnitude => Vector4D.Magnitude;
        public double MagnitudeSquared => Vector4D.MagnitudeSquared;
        public Quaternion(Vector4D v) => (X, Y, Z, W) = v;
        public static implicit operator Vector4D(Quaternion q) => q.Vector4D;
        public static implicit operator Quaternion(Vector4D v) => new Quaternion(v);
        public Quaternion Normalize => Vector4D.Normalize;
        public Quaternion Identity => (0, 0, 0, 1);

        /// <summary>
        /// Creates a quaternion representing a rotation around the specified axis by the given angle.
        /// </summary>
        public static Quaternion FromAxisAngle(Vector3D axis, Angle angle)
        {
            axis = axis.Normalize;
            var sinHalfAngle = angle.Half.Sin;
            var cosHalfAngle = angle.Half.Cos;
            return (
                axis.X * sinHalfAngle,
                axis.Y * sinHalfAngle,
                axis.Z * sinHalfAngle,
                cosHalfAngle
            );
        }

        public Vector3D Transform(Vector3D v)
        {
            var x2 = X + X;
            var y2 = Y + Y;
            var z2 = Z + Z;

            var wx2 = W * x2;
            var wy2 = W * y2;
            var wz2 = W * z2;
            var xx2 = X * x2;
            var xy2 = X * y2;
            var xz2 = X * z2;
            var yy2 = Y * y2;
            var yz2 = Y * z2;
            var zz2 = Z * z2;

            return new Vector3D(
                v.X * (1.0f - yy2 - zz2) + v.Y * (xy2 - wz2) + v.Z * (xz2 + wy2),
                v.X * (xy2 + wz2) + v.Y * (1.0f - xx2 - zz2) + v.Z * (yz2 - wx2),
                v.X * (xz2 - wy2) + v.Y * (yz2 + wx2) + v.Z * (1.0f - xx2 - yy2));
        }
    }
}