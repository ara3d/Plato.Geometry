using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;
using SNVector2 = System.Numerics.Vector2;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector2 : IEquatable<Vector2>
    {
        // Fields

        public readonly float X;
        public readonly float Y;

        // Constructor

        [MethodImpl(AggressiveInlining)]
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }


        [MethodImpl(AggressiveInlining)]
        public Vector2(float x)
            : this(x, x)
        {
        }

        // Static properties

        public static readonly Vector2 E = SNVector2.E;
        public static readonly Vector2 Epsilon = SNVector2.Epsilon;
        public static readonly Vector2 NaN = SNVector2.NaN;
        public static readonly Vector2 NegativeInfinity = SNVector2.NegativeInfinity;
        public static readonly Vector2 NegativeZero = SNVector2.NegativeZero;
        public static readonly Vector2 One = SNVector2.One;
        public static readonly Vector2 Pi = SNVector2.Pi;
        public static readonly Vector2 PositiveInfinity = SNVector2.PositiveInfinity;
        public static readonly Vector2 Tau = SNVector2.Tau;
        public static readonly Vector2 UnitX = SNVector2.UnitX;
        public static readonly Vector2 UnitY = SNVector2.UnitY;
        public static readonly Vector2 Zero = SNVector2.Zero;

        // Implicit casts 

        [MethodImpl(AggressiveInlining)]
        public SNVector2 ToSystem() => Unsafe.As<Vector2, SNVector2>(ref this);

        [MethodImpl(AggressiveInlining)]
        public static Vector2 FromSystem(SNVector2 v) => Unsafe.As<SNVector2, Vector2>(ref v);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator SNVector2(Vector2 v) => v.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector2(SNVector2 v) => FromSystem(v);

        // Static operators  

        /// <summary>
        /// Adds two Vector2D instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator +(Vector2 left, Vector2 right) => left.ToSystem() + right.ToSystem();

        /// <summary>
        /// Subtracts the right Vector2D from the left Vector2D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator -(Vector2 left, Vector2 right) => left.ToSystem() - right.ToSystem();

        /// <summary>
        /// Multiplies two Vector2D instances element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator *(Vector2 left, Vector2 right) => left.ToSystem() * right.ToSystem();

        /// <summary>
        /// Multiplies a Vector2D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator *(Vector2 left, float scalar) => left.ToSystem() * scalar;

        /// <summary>
        /// Multiplies a scalar by a Vector2D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator *(float scalar, Vector2 right) => scalar * right.ToSystem();

        /// <summary>
        /// Divides the left Vector2D by the right Vector2D element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator /(Vector2 left, Vector2 right) => left.ToSystem() / right.ToSystem();

        /// <summary>
        /// Divides a Vector2D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator /(Vector2 left, float scalar) => left.ToSystem() / scalar;

        /// <summary>
        /// Negates the specified Vector2D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator -(Vector2 value) => -value.ToSystem();

        /// <summary>
        /// Determines whether two Vector2D instances are equal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Boolean operator ==(Vector2 left, Vector2 right) => left.ToSystem() == right.ToSystem();

        /// <summary>
        /// Determines whether two Vector2D instances are not equal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Boolean operator !=(Vector2 left, Vector2 right) => left.ToSystem() != right.ToSystem();

        /// <summary>
        /// Determines whether the specified Vector2D is equal to the current Vector2D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public bool Equals(Vector2 other) => ToSystem().Equals(other.ToSystem());

        /// <summary>
        /// Determines whether the specified object is equal to the current Vector2D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public override bool Equals(object? obj) => obj is Vector2 other && Equals(other);

        /// <summary>
        /// Returns a hash code for the Vector2D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y);

        /// <summary>
        /// Returns the dot product of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float Dot(Vector2 right) => SNVector2.Dot(this, right);

        /// <summary>
        /// Returns the Euclidean distance between two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float Distance(Vector2 value2) => SNVector2.Distance(this, value2);

        /// <summary>
        /// Returns the squared Euclidean distance between two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float DistanceSquared(Vector2 value2) => SNVector2.DistanceSquared(this, value2);

        /// <summary>
        /// Returns a vector that clamps each element of the <see cref="Vector2"/> between the corresponding elements of the minimum and maximum vectors.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Clamp(Vector2 min, Vector2 max) => SNVector2.Clamp(this, min, max);

        /// <summary>
        /// Returns a normalized version of the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Normalize
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Normalize(this);
        }

        /// <summary>
        /// Returns the length of the <see cref="Vector2"/>.
        /// </summary>
        public float Length
        {
            [MethodImpl(AggressiveInlining)] get => ToSystem().Length();
        }

        /// <summary>
        /// Returns the squared length of the <see cref="Vector2"/>.
        /// </summary>
        public float LengthSquared
        {
            [MethodImpl(AggressiveInlining)]
            get =>  ToSystem().LengthSquared();
        }

        /// <summary>
        /// Returns a vector that is the reflection of the specified vector off a plane defined by the specified normal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Reflect(Vector2 normal) => SNVector2.Reflect(this, normal);

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Abs
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Abs(this);
        }

        /// <summary>
        /// Returns the square root of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 SquareRoot
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.SquareRoot(this);
        }

        /// <summary>
        /// Returns the sine of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Sin
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Sin(this);
        }

        /// <summary>
        /// Returns the cosine of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Cos
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Cos(this);
        }

        /// <summary>
        /// Returns a vector whose elements are the sine and cosine of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public (Vector2 Sin, Vector2 Cos) SinCos
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                var (sin, cos) = SNVector2.SinCos(this);
                return (sin, cos);
            }
        }

        /// <summary>
        /// Converts degrees to radians for each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 DegreesToRadians
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.DegreesToRadians(this);
        }

        /// <summary>
        /// Converts radians to degrees for each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 RadiansToDegrees
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.RadiansToDegrees(this);
        }

        /// <summary>
        /// Returns the exponential of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Exp
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Exp(this);
        }

        /// <summary>
        /// Returns the natural logarithm (base e) of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Log
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Log(this);
        }

        /// <summary>
        /// Returns the base-2 logarithm of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Log2
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Log2(this);
        }

        /// <summary>
        /// Transforms a <see cref="Vector2"/> by a 3x2 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Transform(Matrix3x2 matrix) => SNVector2.Transform(this, matrix);

        /// <summary>
        /// Transforms a <see cref="Vector2"/> by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Transform(Matrix4x4 matrix) => SNVector2.Transform(this, matrix);

        /// <summary>
        /// Transforms a <see cref="Vector2"/> by a quaternion rotation.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Transform(Quaternion rotation) => SNVector2.Transform(this, rotation);

        /// <summary>
        /// Transforms a normal vector by a 3x2 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 TransformNormal(Matrix3x2 matrix) => SNVector2.TransformNormal(this, matrix);

        /// <summary>
        /// Transforms a normal vector by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 TransformNormal(Matrix4x4 matrix) => SNVector2.TransformNormal(this, matrix);

        /// <summary>
        /// Returns the maximum of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Max(Vector2 value2) => SNVector2.Max(this, value2);

        /// <summary>
        /// Returns the maximum magnitude of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 MaxMagnitude(Vector2 value2) => SNVector2.MaxMagnitude(this, value2);

        /// <summary>
        /// Returns the maximum magnitude number of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 MaxMagnitudeNumber(Vector2 value2) => SNVector2.MaxMagnitudeNumber(this, value2);

        /// <summary>
        /// Returns the maximum number of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 MaxNumber(Vector2 value2) => SNVector2.MaxNumber(this, value2);

        /// <summary>
        /// Returns the minimum of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Min(Vector2 value2) => SNVector2.Min(this, value2);

        /// <summary>
        /// Returns the minimum magnitude of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 MinMagnitude(Vector2 value2) => SNVector2.MinMagnitude(this, value2);

        /// <summary>
        /// Returns the minimum magnitude number of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 MinMagnitudeNumber(Vector2 value2) => SNVector2.MinMagnitudeNumber(this, value2);

        /// <summary>
        /// Returns the minimum number of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 MinNumber(Vector2 value2) => SNVector2.MinNumber(this, value2);

        /// <summary>
        /// Performs a fused multiply-add operation on the specified <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 FusedMultiplyAdd(Vector2 right, Vector2 addend) => SNVector2.FusedMultiplyAdd(this, right, addend);

        /// <summary>
        /// Performs a multiply-add estimate operation on the specified <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 MultiplyAddEstimate(Vector2 right, Vector2 addend) => SNVector2.MultiplyAddEstimate(this, right, addend);

        /// <summary>
        /// Returns a truncated version of the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Truncate
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Truncate(this);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Round() => SNVector2.Round(this);

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer using the specified rounding mode.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Round(MidpointRounding mode) => SNVector2.Round(this, mode);

        /// <summary>
        /// Returns the string representation of the <see cref="Vector2"/> using default formatting.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public override string ToString() => ToSystem().ToString();

        /// <summary>
        /// Returns the string representation of the <see cref="Vector2"/> using the specified format.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string ToString(string? format) => ToSystem().ToString(format);

        /// <summary>
        /// Returns the string representation of the <see cref="Vector2"/> using the specified format and format provider.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string ToString(string? format, IFormatProvider? formatProvider) => ToSystem().ToString(format, formatProvider);
    }
}
