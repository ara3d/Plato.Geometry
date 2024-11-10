using System;
using System.Runtime.CompilerServices;

namespace Plato.DoublePrecision
{
    /// <summary>
    /// A structure encapsulating a 4x4 matrix.
    /// </summary>
    public partial struct Matrix4x4 
    {
        
        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public bool IsIdentity =>
            M11 == 1f && M22 == 1f && M33 == 1f && M44 == 1f && // Check diagonal element first for early out.
            M12 == 0f && M13 == 0f && M14 == 0f &&
            M21 == 0f && M23 == 0f && M24 == 0f &&
            M31 == 0f && M32 == 0f && M34 == 0f &&
            M41 == 0f && M42 == 0f && M43 == 0f;

        /// <summary>
        /// Creates a matrix from a position and oriented to look towards a point .
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 CreateLookAt(Vector3D pos, Vector3D target, Vector3D up)
        {
            var zaxis = (target - pos).Normalize;
            if (up.IsParallel(zaxis))
                up = zaxis.X.Abs < zaxis.Y.Abs 
                    ? (1, 0, 0) 
                    : (0, 1, 0);
            var xaxis = up.Cross(zaxis).Normalize;
            var yaxis = zaxis.Cross(xaxis);
            return CreateFromBasis(xaxis, yaxis, zaxis, pos);
        }

        public static Matrix4x4 CreateFromBasis(Vector3D xaxis, Vector3D yaxis, Vector3D zaxis, Vector3D pos)
        {
            var tx = -xaxis.Dot(pos);
            var ty = -yaxis.Dot(pos);
            var tz = -zaxis.Dot(pos);
            return (xaxis.Vector4D, yaxis.Vector4D, zaxis.Vector4D, (tx, ty, tz, 1));
        }

        /// <summary>
        /// Calculates the determinant of the 3x3 rotational component of the matrix.
        /// </summary>
        /// <returns>The determinant of the 3x3 rotational component matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Get3x3RotationDeterminant()
        {
            // | a b c |
            // | d e f | = a | e f | - b | d f | + c | d e |
            // | g h i |     | h i |     | g i |     | g h |
            //
            // a | e f | = a ( ei - fh )
            //   | h i | 
            //
            // b | d f | = b ( di - gf )
            //   | g i |
            //
            // c | d e | = c ( dh - eg )
            //   | g h |

            double a = M11, b = M12, c = M13;
            double d = M21, e = M22, f = M23;
            double g = M31, h = M32, i = M33;

            var ei_fh = e * i - f * h;
            var di_gf = d * i - g * f;
            var dh_eg = d * h - e * g;

            return a * ei_fh -
                   b * di_gf +
                   c * dh_eg;
        }

        /// <summary>
        /// Returns true if the 3x3 rotation determinant of the matrix is less than 0. This assumes the matrix represents
        /// an affine transform.
        /// </summary>
        // From: https://math.stackexchange.com/a/1064759
        // "If your matrix is the augmented matrix representing an affine transformation in 3D, then yes,
        // the proper thing to do to see if it switches orientation is checking the sign of the top 3×3 determinant.
        // This is easy to see: if your transformation is Ax+b, then the +b part is a translation and does not
        // affect orientation, and x->Ax switches orientation iff detA < 0."
        public bool IsReflection
            => Get3x3RotationDeterminant() < 0;

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <returns>The determinant of the matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDeterminant()
        {
            // | a b c d |     | f g h |     | e g h |     | e f h |     | e f g |
            // | e f g h | = a | j k l | - b | i k l | + c | i j l | - d | i j k |
            // | i j k l |     | n o p |     | m o p |     | m n p |     | m n o |
            // | m n o p |
            //
            //   | f g h |
            // a | j k l | = a ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //   | n o p |
            //
            //   | e g h |     
            // b | i k l | = b ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //   | m o p |     
            //
            //   | e f h |
            // c | i j l | = c ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //   | m n p |
            //
            //   | e f g |
            // d | i j k | = d ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //   | m n o |
            //
            // Cost of operation
            // 17 adds and 28 muls.
            //
            // add: 6 + 8 + 3 = 17
            // mul: 12 + 16 = 28

            double a = M11, b = M12, c = M13, d = M14;
            double e = M21, f = M22, g = M23, h = M24;
            double i = M31, j = M32, k = M33, l = M34;
            double m = M41, n = M42, o = M43, p = M44;

            var kp_lo = k * p - l * o;
            var jp_ln = j * p - l * n;
            var jo_kn = j * o - k * n;
            var ip_lm = i * p - l * m;
            var io_km = i * o - k * m;
            var in_jm = i * n - j * m;

            return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
                   b * (e * kp_lo - g * ip_lm + h * io_km) +
                   c * (e * jp_ln - f * ip_lm + h * in_jm) -
                   d * (e * jo_kn - f * io_km + g * in_jm);
        }

        /// <summary>
        /// Attempts to calculate the inverse of the given matrix. If successful, result will contain the inverted matrix.
        /// </summary>
        /// <param name="matrix">The source matrix to invert.</param>
        /// <param name="result">If successful, contains the inverted matrix.</param>
        /// <returns>True if the source matrix could be inverted; False otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
        {
            //                                       -1
            // If you have matrix M, inverse Matrix M   can compute
            //
            //     -1       1      
            //    M   = --------- A
            //            det(M)
            //
            // A is adjugate (adjoint) of M, where,
            //
            //      T
            // A = C
            //
            // C is Cofactor matrix of M, where,
            //           i + j
            // C   = (-1)      * det(M  )
            //  ij                    ij
            //
            //     [ a b c d ]
            // M = [ e f g h ]
            //     [ i j k l ]
            //     [ m n o p ]
            //
            // First Row
            //           2 | f g h |
            // C   = (-1)  | j k l | = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //  11         | n o p |
            //
            //           3 | e g h |
            // C   = (-1)  | i k l | = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //  12         | m o p |
            //
            //           4 | e f h |
            // C   = (-1)  | i j l | = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //  13         | m n p |
            //
            //           5 | e f g |
            // C   = (-1)  | i j k | = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //  14         | m n o |
            //
            // Second Row
            //           3 | b c d |
            // C   = (-1)  | j k l | = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
            //  21         | n o p |
            //
            //           4 | a c d |
            // C   = (-1)  | i k l | = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
            //  22         | m o p |
            //
            //           5 | a b d |
            // C   = (-1)  | i j l | = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
            //  23         | m n p |
            //
            //           6 | a b c |
            // C   = (-1)  | i j k | = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
            //  24         | m n o |
            //
            // Third Row
            //           4 | b c d |
            // C   = (-1)  | f g h | = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
            //  31         | n o p |
            //
            //           5 | a c d |
            // C   = (-1)  | e g h | = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
            //  32         | m o p |
            //
            //           6 | a b d |
            // C   = (-1)  | e f h | = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
            //  33         | m n p |
            //
            //           7 | a b c |
            // C   = (-1)  | e f g | = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
            //  34         | m n o |
            //
            // Fourth Row
            //           5 | b c d |
            // C   = (-1)  | f g h | = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
            //  41         | j k l |
            //
            //           6 | a c d |
            // C   = (-1)  | e g h | = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
            //  42         | i k l |
            //
            //           7 | a b d |
            // C   = (-1)  | e f h | = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
            //  43         | i j l |
            //
            //           8 | a b c |
            // C   = (-1)  | e f g | = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
            //  44         | i j k |
            //
            // Cost of operation
            // 53 adds, 104 muls, and 1 div.
            double a = matrix.M11, b = matrix.M12, c = matrix.M13, d = matrix.M14;
            double e = matrix.M21, f = matrix.M22, g = matrix.M23, h = matrix.M24;
            double i = matrix.M31, j = matrix.M32, k = matrix.M33, l = matrix.M34;
            double m = matrix.M41, n = matrix.M42, o = matrix.M43, p = matrix.M44;

            var kp_lo = k * p - l * o;
            var jp_ln = j * p - l * n;
            var jo_kn = j * o - k * n;
            var ip_lm = i * p - l * m;
            var io_km = i * o - k * m;
            var in_jm = i * n - j * m;

            var a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            var a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            var a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            var a14 = -(e * jo_kn - f * io_km + g * in_jm);

            var det = a * a11 + b * a12 + c * a13 + d * a14;

            if (Math.Abs(det) <= double.Epsilon)
            {
                result = new Matrix4x4((double.NaN, double.NaN, double.NaN, double.NaN),
                                       (double.NaN, double.NaN, double.NaN, double.NaN),
                                       (double.NaN, double.NaN, double.NaN, double.NaN),
                                       (double.NaN, double.NaN, double.NaN, double.NaN));
                return false;
            }

            var invDet = 1.0f / det;

            var M11 = a11 * invDet;
            var M21 = a12 * invDet;
            var M31 = a13 * invDet;
            var M41 = a14 * invDet;

            var M12 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
            var M22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
            var M32 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
            var M42 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

            var gp_ho = g * p - h * o;
            var fp_hn = f * p - h * n;
            var fo_gn = f * o - g * n;
            var ep_hm = e * p - h * m;
            var eo_gm = e * o - g * m;
            var en_fm = e * n - f * m;

            var M13 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
            var M23 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
            var M33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
            var M43 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

            var gl_hk = g * l - h * k;
            var fl_hj = f * l - h * j;
            var fk_gj = f * k - g * j;
            var el_hi = e * l - h * i;
            var ek_gi = e * k - g * i;
            var ej_fi = e * j - f * i;

            var M14 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
            var M24 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
            var M34 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
            var M44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

            result = (
                (M11, M21, M31, M41),
                (M12, M22, M32, M42),
                (M13, M23, M33, M43),
                (M14, M24, M34, M44));

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Decompose(Matrix4x4 matrix, out Vector3D scale, out Quaternion rotation, out Vector3D translation)
        {
            var result = true;

            var scales = new double[3];
            const double EPSILON = 1e-10;

            var canonicalBasis = new[] {
                Vector3D.UnitX,
                Vector3D.UnitY,
                Vector3D.UnitZ,
            };

            var basis = new[] {
                matrix.Row1.ToVector3D,
                matrix.Row2.ToVector3D,
                matrix.Row3.ToVector3D
            };

            scales[0] = basis[0].Length;
            scales[1] = basis[1].Length;
            scales[2] = basis[2].Length;

            uint a, b, c;
            var (x, y, z) = (scales[0], scales[1], scales[2]);
            if (x < y)
            {
                if (y < z)  
                {
                    a = 2; b = 1; c = 0;
                }   
                else 
                {
                    if (x < z)
                    {
                        a = 1; b = 2; c = 0;
                    }
                    else
                    {
                        a = 1; b = 0; c = 2;
                    }
                }
            }
            else
            {
                if (x < z)
                {
                    a = 2; b = 0; c = 1;
                }
                else
                {
                    if (y < z)
                    {
                        a = 0; b = 2; c = 1;
                    }
                    else
                    {
                        a = 0; b = 1; c = 2;
                    }
                }
            }

            if (scales[a] < EPSILON)
                basis[a] = canonicalBasis[a];

            basis[a] = basis[a].Normalize;

            if (scales[b] < EPSILON)
            {
                var abs = basis[a].Abs;

                var cc = abs.X < abs.Y ? 
                    abs.Y >= abs.Z && abs.X >= abs.Z ? 2 : 0 :
                    abs.X >= abs.Z && abs.Y >= abs.Z ? 2 : 1;

                basis[b] = basis[a].Cross(canonicalBasis[cc]);
            }

            basis[b] = basis[b].Normalize;

            if (scales[c] < EPSILON)
                basis[c] = basis[a].Cross(basis[b]);

            basis[c] = basis[c].Normalize;

            // Create a temporary rotation matrix, and get the determinant.
            var matTemp = CreateFromRows(basis[0], basis[1], basis[2]);
            var det = matTemp.GetDeterminant();

            // use Kramer's rule to check for handedness of coordinate system
            if (det < 0.0f)
            {
                // switch coordinate system by negating the scale and inverting the basis vector on the x-axis
                scales[a] = -scales[a];
                basis[a] = -basis[a];
                det = -det;

                // Recreate the temporary matrix 
                matTemp = CreateFromRows(basis[0], basis[1], basis[2]);
            }

            det -= 1.0f;
            det *= det;

            if (EPSILON < det)
            {
                // Non-SRT matrix encountered
                rotation = Quaternion.Identity;
                result = false;
            }
            else
            {
                // Generate the quaternion from the matrix
                rotation = matTemp.QuaternionFromRotationMatrix;
            }

            translation = matrix.Translation;
            scale = new Vector3D(scales[0], scales[1], scales[2]);
            return result;
        }

        /*
         // The following code is suggested by ChatGPT as an improvement, and needs testing, and A 3x3 matrix class
          * public static bool Decompose(Matrix4x4 matrix, out Vector3D scale, out Quaternion rotation, out Vector3D translation)
{
    const double EPSILON = 1e-10;
    bool result = true;

    // Extract the translation component
    translation = matrix.Translation;

    // Extract the basis vectors (columns of the upper-left 3x3 matrix)
    var basis = new[]
    {
        new Vector3D(matrix.M11, matrix.M12, matrix.M13),
        new Vector3D(matrix.M21, matrix.M22, matrix.M23),
        new Vector3D(matrix.M31, matrix.M32, matrix.M33)
    };

    // Compute the scales (lengths of the basis vectors)
    var scales = new double[3];
    for (int i = 0; i < 3; i++)
    {
        scales[i] = basis[i].Length;
        if (scales[i] < EPSILON)
        {
            // Handle zero scale by setting the basis vector to a canonical vector
            basis[i] = Vector3D.UnitX; // Or UnitY/UnitZ based on i
            scales[i] = 0.0;
        }
    }

    // Normalize the basis vectors to extract the rotation matrix
    for (int i = 0; i < 3; i++)
    {
        if (scales[i] >= EPSILON)
        {
            basis[i] /= scales[i];
        }
        else
        {
            // Cannot normalize a zero vector
            result = false;
        }
    }

    // Reconstruct the rotation matrix
    var rotationMatrix = new Matrix3x3(basis[0], basis[1], basis[2]);

    // Check if the rotation matrix is orthonormal
    double det = rotationMatrix.GetDeterminant();
    if (det < 0.0)
    {
        // Adjust for left-handed coordinate system
        scales[0] = -scales[0];
        basis[0] = -basis[0];
        rotationMatrix = new Matrix3x3(basis[0], basis[1], basis[2]);
        det = -det;
    }

    if (Math.Abs(det - 1.0) > EPSILON)
    {
        // The rotation matrix is not valid
        rotation = Quaternion.Identity;
        result = false;
    }
    else
    {
        // Convert the rotation matrix to a quaternion
        rotation = Quaternion.CreateFromRotationMatrix(rotationMatrix);
    }

    scale = new Vector3D(scales[0], scales[1], scales[2]);
    return result;
}
         */
    }
}
