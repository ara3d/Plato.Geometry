using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SNMatrix3x2 = System.Numerics.Matrix3x2;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Matrix3x2 : IEquatable<Matrix3x2>
    {
        // -------------------------------------------------------------------------------
        // Fields (layout must match System.Numerics.Matrix3x2)
        // -------------------------------------------------------------------------------

        public readonly Vector2 Row1;
        public readonly Vector2 Row2;
        public readonly Vector2 Row3;

        // -------------------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------------------
        
        [MethodImpl(AggressiveInlining)]
        public Matrix3x2(
            Number m11, Number m12,
            Number m21, Number m22,
            Number m31, Number m32) : 
            this(
                new(m11, m12), 
                new(m21, m22), 
                new(m31, m32))
        {
        }

        [MethodImpl(AggressiveInlining)]
        public Matrix3x2(
            Vector2 row1,
            Vector2 row2,
            Vector2 row3)
        {
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        // -------------------------------------------------------------------------------
        // Convert to/from System.Numerics.Matrix3x2 (by reinterpreting memory)
        // -------------------------------------------------------------------------------
        
        [MethodImpl(AggressiveInlining)]
        public SNMatrix3x2 ToSystem()
            => Unsafe.As<Matrix3x2, SNMatrix3x2>(ref this);

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 FromSystem(SNMatrix3x2 sysMat)
            => Unsafe.As<SNMatrix3x2, Matrix3x2>(ref sysMat);

        // Implicit conversion operators
        [MethodImpl(AggressiveInlining)]
        public static implicit operator SNMatrix3x2(Matrix3x2 mat)
            => mat.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Matrix3x2(SNMatrix3x2 sysMat)
            => FromSystem(sysMat);

        // -------------------------------------------------------------------------------
        // Identity
        // -------------------------------------------------------------------------------
       
        public static readonly Matrix3x2 Identity = SNMatrix3x2.Identity;

        // -------------------------------------------------------------------------------
        // Operators
        // -------------------------------------------------------------------------------
        
        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 operator +(Matrix3x2 value1, Matrix3x2 value2)
            => FromSystem(value1.ToSystem() + value2.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 operator -(Matrix3x2 value1, Matrix3x2 value2)
            => FromSystem(value1.ToSystem() - value2.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 operator *(Matrix3x2 value1, Matrix3x2 value2)
            => FromSystem(value1.ToSystem() * value2.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 operator *(Matrix3x2 value1, Number scalar)
            => FromSystem(value1.ToSystem() * scalar);

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 operator *(Number scalar, Matrix3x2 value1)
            => value1 * scalar;

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 operator /(Matrix3x2 value1, Number scalar)
            => value1 * (1f / scalar);

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator ==(Matrix3x2 a, Matrix3x2 b)
            => a.Equals(b);

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator !=(Matrix3x2 a, Matrix3x2 b)
            => !a.Equals(b);

        // -------------------------------------------------------------------------------
        // Common 2D transform creation methods (forwarded)
        // -------------------------------------------------------------------------------
        
        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 CreateTranslation(Number xPosition, Number yPosition)
            => SNMatrix3x2.CreateTranslation(xPosition, yPosition);

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 CreateTranslation(Vector2 position)
            => SNMatrix3x2.CreateTranslation(position);

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 CreateScale(Number scale)
            => SNMatrix3x2.CreateScale(scale);

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 CreateScale(Number xScale, Number yScale)
            => SNMatrix3x2.CreateScale(xScale, yScale);

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 CreateScale(Vector2 scales)
            => SNMatrix3x2.CreateScale(scales);

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 CreateScale(Number xScale, Number yScale, Vector2 centerPoint)
            => SNMatrix3x2.CreateScale(xScale, yScale, centerPoint);

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 CreateRotation(Number radians)
            => SNMatrix3x2.CreateRotation(radians);

        [MethodImpl(AggressiveInlining)]
        public static Matrix3x2 CreateRotation(Number radians, Vector2 centerPoint)
            => SNMatrix3x2.CreateRotation(radians, centerPoint);

        // -------------------------------------------------------------------------------
        // Other useful static methods: Invert, Lerp, etc.
        // -------------------------------------------------------------------------------
        
        [MethodImpl(AggressiveInlining)]
        public (Matrix3x2, Boolean) Invert()
        {
            var success = SNMatrix3x2.Invert(ToSystem(), out var result);
            return (result, success);
        }

        [MethodImpl(AggressiveInlining)]
        public Matrix3x2 Lerp(Matrix3x2 matrix2, Number amount)
            => FromSystem(SNMatrix3x2.Lerp(ToSystem(), matrix2.ToSystem(), amount));

        // -------------------------------------------------------------------------------
        // Instance methods
        // -------------------------------------------------------------------------------

        /// <summary>
        /// Gets the determinant of this 3x2 matrix.
        /// </summary>
        public Number Determinant
        {
            [MethodImpl(AggressiveInlining)] get => ToSystem().GetDeterminant();
        }

        // -------------------------------------------------------------------------------
        // Equality, hashing, and ToString
        // -------------------------------------------------------------------------------
        
        [MethodImpl(AggressiveInlining)]
        public bool Equals(Matrix3x2 other)
            => ToSystem().Equals(other.ToSystem());

        [MethodImpl(AggressiveInlining)]
        public override bool Equals(object? obj)
            => obj is Matrix3x2 mat && Equals(mat);

        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode()
            => ToSystem().GetHashCode();

        [MethodImpl(AggressiveInlining)]
        public override string ToString()
            => ToSystem().ToString();
    }
}
