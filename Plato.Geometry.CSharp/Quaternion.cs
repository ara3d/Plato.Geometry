using System;
using System.Runtime.CompilerServices;

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
        public static Quaternion Identity => (0, 0, 0, 1);

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

        /* TODO: put this somewhere.
        // Helper method to rotate a vector using a quaternion
        public static Vector3D RotateVector(Vector3D vector, Quaternion rotation)
        {
            rotation.Normalize();
            Quaternion qVector = new Quaternion(vector, 0);
            Quaternion qConj = rotation;
            qConj.Conjugate();
            Quaternion qResult = rotation * qVector * qConj;
            return new Vector3D(qResult.X, qResult.Y, qResult.Z);
        }
        */

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

        /// <summary>
        /// Creates a Quaternion from the given rotation matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
        {
            var trace = matrix.M11 + matrix.M22 + matrix.M33;

            if (trace > 0.0f)
            {
                var s = (trace + 1.0).Sqrt;
                var w = s * 0.5;
                s = 0.5 / s;
                return (
                    (matrix.M23 - matrix.M32) * s,
                    (matrix.M31 - matrix.M13) * s,
                    (matrix.M12 - matrix.M21) * s,
                    w);
            }
            if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var s = (1.0 + matrix.M11 - matrix.M22 - matrix.M33).Sqrt;
                var invS = 0.5 / s;
                return (s.Half,
                    (matrix.M12 + matrix.M21) * invS,
                    (matrix.M13 + matrix.M31) * invS,
                    (matrix.M23 - matrix.M32) * invS);
            }
            if (matrix.M22 > matrix.M33)
            {
                var s = (1.0 + matrix.M22 - matrix.M11 - matrix.M33).Sqrt;
                var invS = 0.5 / s;
                return (
                   (matrix.M21 + matrix.M12) * invS,
                   s.Half,
                   (matrix.M32 + matrix.M23) * invS,
                   (matrix.M31 - matrix.M13) * invS);
            }
            {
                var s = (1.0 + matrix.M33 - matrix.M11 - matrix.M22).Sqrt;
                var invS = 0.5 / s;
                return (
                    (matrix.M31 + matrix.M13) * invS,
                    (matrix.M32 + matrix.M23) * invS,
                    s.Half,
                    (matrix.M12 - matrix.M21) * invS);
            }
        }

        /// <summary>
        /// Calculates the dot product of two Quaternions.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Number Dot(Quaternion b)
            => Vector4D.Dot(b.Vector4D);

        public static Quaternion operator*(Quaternion q, Number scalar)
            => q.Vector4D * scalar;

        public static Quaternion operator/(Quaternion q, Number scalar)
            => q.Vector4D / scalar;

        public static Quaternion operator +(Quaternion q1, Quaternion q2)
            => q1.Vector4D + q2.Vector4D;

        public static Quaternion operator -(Quaternion q1, Quaternion q2)
            => q1.Vector4D - q2.Vector4D;

        public static Quaternion operator -(Quaternion q)
            => -q.Vector4D;

        /// <summary>
        /// Interpolates between two quaternions, using spherical linear interpolation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Slerp(Quaternion q1, Quaternion q2, double t)
        {
            const double epsilon = 1e-8;

            var cosOmega = q1.Dot(q2);

            var flip = false;

            if (cosOmega < 0.0)
            {
                flip = true;
                cosOmega = -cosOmega;
            }

            double s1, s2;

            if (cosOmega > (1.0 - epsilon))
            {
                // Too close, do straight linear interpolation.
                s1 = 1.0 - t;
                s2 = (flip) ? -t : t;
            } 
            else
            {
                var omega = cosOmega.Acos;
                var invSinOmega = 1 / omega.Sin;

                s1 = ((1.0 - t) * omega).Sin * invSinOmega;
                s2 = (flip)
                    ? -(t * omega).Sin * invSinOmega
                    : (t * omega).Sin * invSinOmega;
            }

            return q1 * s1 + q2 * s2;
        }

        /// <summary>
        /// Concatenates two Quaternions in reverse order that they are passed;
        /// the result represents the q1 rotation followed by the q2 rotation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ReverseConcatenate(Quaternion q2, Quaternion q1)
        {
            var av = q2.Vector3D;
            var bv = q1.Vector3D;
            var cv = av.Cross(bv);
            var dot = av.Dot(bv);
            return new Quaternion(
                q2.X * q1.W + q1.X * q2.W + cv.X,
                q2.Y * q1.W + q1.Y * q2.W + cv.Y,
                q2.Z * q1.W + q1.Z * q2.W + cv.Z,
                q2.W * q1.W - dot);
        }

        /// <summary>
        /// Concatenates two Quaternions; the result represents the q1 rotation followed by the q2 rotation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Concatenate(Quaternion q1, Quaternion q2)
            => ReverseConcatenate(q2, q1);

        public Vector3D Vector3D => (X, Y, Z);
    }
}