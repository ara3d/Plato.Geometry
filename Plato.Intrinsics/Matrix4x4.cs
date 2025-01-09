using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SNMatrix4x4 = System.Numerics.Matrix4x4;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Matrix4x4 : IEquatable<Matrix4x4>
    {
        public readonly Vector4 Row1;
        public readonly Vector4 Row2;
        public readonly Vector4 Row3;
        public readonly Vector4 Row4;

        // --------------------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public Matrix4x4(Vector4 row1, Vector4 row2, Vector4 row3, Vector4 row4) =>
            (Row1, Row2, Row3, Row4) = (row1, row2, row3, row4);

        [MethodImpl(AggressiveInlining)]
        public Matrix4x4(
            Number m11, Number m12, Number m13, Number m14,
            Number m21, Number m22, Number m23, Number m24,
            Number m31, Number m32, Number m33, Number m34,
            Number m41, Number m42, Number m43, Number m44)
            : this(
                new(m11, m12, m13, m14),
                new(m21, m22, m23, m24),
                new(m31, m32, m33, m34),
                new(m41, m42, m43, m44))
        { }

        // --------------------------------------------------------------------------------
        // Convert to/from System.Numerics.Matrix4x4
        // --------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public SNMatrix4x4 ToSystem()
            => Unsafe.As<Matrix4x4, SNMatrix4x4>(ref this);

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 FromSystem(SNMatrix4x4 sysMat)
            => Unsafe.As<SNMatrix4x4, Matrix4x4>(ref sysMat);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Matrix4x4(SNMatrix4x4 m) 
            => FromSystem(m);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator SNMatrix4x4(Matrix4x4 m) 
            => m.ToSystem();

        // --------------------------------------------------------------------------------
        // Identity
        // --------------------------------------------------------------------------------
        
        public static readonly Matrix4x4 Identity = SNMatrix4x4.Identity;

        // --------------------------------------------------------------------------------
        // Operators (forward to System.Numerics)
        // --------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 operator +(Matrix4x4 value1, Matrix4x4 value2)
            => value1.ToSystem() + value2.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 operator -(Matrix4x4 value1, Matrix4x4 value2)
            => value1.ToSystem() - value2.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 operator *(Matrix4x4 value1, Matrix4x4 value2)
            => value1.ToSystem() * value2.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 operator *(Matrix4x4 value1, Number f)
            => value1.ToSystem() * f;

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 operator *(Number f, Matrix4x4 value1)
            => value1.ToSystem() * f;

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 operator /(Matrix4x4 value1, Number f)
            => value1.ToSystem() * f.ReciprocalEstimate;

        // --------------------------------------------------------------------------------
        // One-to-one static methods (Add, Multiply, Subtract, etc.)
        // --------------------------------------------------------------------------------
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 Add(Matrix4x4 value1, Matrix4x4 value2)
            => SNMatrix4x4.Add(value1.ToSystem(), value2.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 Subtract(Matrix4x4 value1, Matrix4x4 value2)
            => SNMatrix4x4.Subtract(value1.ToSystem(), value2.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 Multiply(Matrix4x4 value1, Matrix4x4 value2)
            => SNMatrix4x4.Multiply(value1.ToSystem(), value2.ToSystem());

        // --------------------------------------------------------------------------------
        // Example "Create*" static methods (forwarded)
        // --------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateTranslation(Vector3 position)
            => SNMatrix4x4.CreateTranslation(position);

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateScale(Number scale)
            => SNMatrix4x4.CreateScale(scale);

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateScale(Number xScale, Number yScale, Number zScale)
            => SNMatrix4x4.CreateScale(xScale, yScale, zScale);

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateRotationX(Angle angle)
            => SNMatrix4x4.CreateRotationX(angle);

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateRotationY(Angle angle)
            => SNMatrix4x4.CreateRotationY(angle);

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateRotationZ(Angle angle)
            => SNMatrix4x4.CreateRotationZ(angle);

        // --------------------------------------------------------------------------------
        // Decompose, Determinant, Transpose, etc. (common instance methods)
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Attempts to extract scale, rotation (as a <see cref="Quaternion"/>),
        /// and translation from this matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            var success = SNMatrix4x4.Decompose(this, out var sysScale, out var sysQuat, out var sysTrans);
            scale = sysScale;
            rotation = sysQuat; 
            translation = sysTrans;
            return success;
        }

        public Number Determinant
        {
            [MethodImpl(AggressiveInlining)] get => ToSystem().GetDeterminant();
        }

        public Matrix4x4 Transpose
        {
            [MethodImpl(AggressiveInlining)] get => SNMatrix4x4.Transpose(ToSystem());
        }

        // --------------------------------------------------------------------------------
        // Equality, hashing, and ToString
        // --------------------------------------------------------------------------------
        [MethodImpl(AggressiveInlining)]
        public bool Equals(Matrix4x4 other)
            => ToSystem().Equals(other.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Matrix4x4 other && Equals(other);

        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode()
            => ToSystem().GetHashCode();

        [MethodImpl(AggressiveInlining)]
        public override string ToString()
            => ToSystem().ToString();

        [MethodImpl(AggressiveInlining)]
        public static bool operator ==(Matrix4x4 a, Matrix4x4 b) 
            => a.Equals(b);

        [MethodImpl(AggressiveInlining)]
        public static bool operator !=(Matrix4x4 a, Matrix4x4 b) 
            => !a.Equals(b);

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 Negate(Matrix4x4 value)
            => SNMatrix4x4.Negate(value.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 Lerp(Matrix4x4 matrix1, Matrix4x4 matrix2, Number amount)
            => SNMatrix4x4.Lerp(matrix1.ToSystem(), matrix2.ToSystem(), amount);

        [MethodImpl(AggressiveInlining)]
        public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
        {
            var success = SNMatrix4x4.Invert(matrix.ToSystem(), out var sysResult);
            result = sysResult;
            return success;
        }

        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreatePerspectiveFieldOfView(Number fieldOfView, Number aspectRatio, Number nearPlane, Number farPlane)
            => SNMatrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlane, farPlane);

        /// <summary>
        /// Creates a spherical billboard that rotates around a specified object position.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector)
            => SNMatrix4x4.CreateBillboard(
                objectPosition, cameraPosition, cameraUpVector, cameraForwardVector);

        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified axis.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3 cameraForwardVector, Vector3 objectForwardVector)
            => SNMatrix4x4.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, cameraForwardVector, objectForwardVector);

        /// <summary>
        /// Creates a rotation matrix from a specified axis and angle.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateFromAxisAngle(Vector3 axis, Angle angle)
            => SNMatrix4x4.CreateFromAxisAngle(axis, angle);

        /// <summary>
        /// Creates a rotation matrix from a quaternion.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateFromQuaternion(Quaternion quaternion)
            => SNMatrix4x4.CreateFromQuaternion(quaternion);

        /// <summary>
        /// Creates a rotation matrix from yaw, pitch, and roll values (in radians).
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateFromYawPitchRoll(Angle yaw, Angle pitch, Angle roll)
            => SNMatrix4x4.CreateFromYawPitchRoll(yaw, pitch, roll);

        /// <summary>
        /// Creates a view matrix for a camera looking at a target.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
            => SNMatrix4x4.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);

        /// <summary>
        /// Creates an orthographic projection matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateOrthographic(Number width, Number height, Number zNearPlane, Number zFarPlane)
            => SNMatrix4x4.CreateOrthographic(width, height, zNearPlane, zFarPlane);

        /// <summary>
        /// Creates a customized orthographic projection matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateOrthographicOffCenter(Number left, Number right, Number bottom, Number top, Number zNearPlane, Number zFarPlane)
            => SNMatrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);

        /// <summary>
        /// Creates a perspective projection matrix based on a width and height.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreatePerspective(Number width, Number height, Number nearPlaneDistance, Number farPlaneDistance)
            => SNMatrix4x4.CreatePerspective(width, height, nearPlaneDistance, farPlaneDistance);

        /// <summary>
        /// Creates a customized perspective projection matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreatePerspectiveOffCenter(Number left, Number right, Number bottom, Number top, Number nearPlaneDistance, Number farPlaneDistance)
            => SNMatrix4x4.CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance);

        /// <summary>
        /// Creates a matrix that reflects coordinates about a specified plane.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateReflection(Plane value)
            => SNMatrix4x4.CreateReflection(value);

        /// <summary>
        /// Creates a matrix that flattens geometry into a specified plane as if casting a shadow from a light source.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateShadow(Vector3 lightDirection, Plane plane)
            => SNMatrix4x4.CreateShadow(lightDirection, plane);

        /// <summary>
        /// Creates a world matrix with the specified parameters.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Matrix4x4 CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
            => SNMatrix4x4.CreateWorld(position, forward, up);
    }
}