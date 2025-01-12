using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;
using SNVector4 = System.Numerics.Vector4;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector4 : IEquatable<Vector4>
    {
        // Fields

        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float W;

        // Constructor

        [MethodImpl(AggressiveInlining)]
        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(AggressiveInlining)]
        public Vector4(float x)
            : this(x,x,x, x)    
        {
        }

        // Static properties

        public static readonly Vector4 E = SNVector4.E;
        public static readonly Vector4 Epsilon = SNVector4.Epsilon;
        public static readonly Vector4 NaN = SNVector4.NaN;
        public static readonly Vector4 NegativeInfinity = SNVector4.NegativeInfinity;
        public static readonly Vector4 NegativeZero = SNVector4.NegativeZero;
        public static readonly Vector4 One = SNVector4.One;
        public static readonly Vector4 Pi = SNVector4.Pi;
        public static readonly Vector4 PositiveInfinity = SNVector4.PositiveInfinity;
        public static readonly Vector4 Tau = SNVector4.Tau;
        public static readonly Vector4 UnitX = SNVector4.UnitX;
        public static readonly Vector4 UnitY = SNVector4.UnitY;
        public static readonly Vector4 UnitZ = SNVector4.UnitZ;
        public static readonly Vector4 UnitW = SNVector4.UnitW;
        public static readonly Vector4 Zero = SNVector4.Zero;

        // Implicit casts 

        [MethodImpl(AggressiveInlining)]
        public SNVector4 ToSystem() => Unsafe.As<Vector4, SNVector4>(ref this);

        [MethodImpl(AggressiveInlining)]
        public static Vector4 FromSystem(SNVector4 v) => Unsafe.As<SNVector4, Vector4>(ref v);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator SNVector4(Vector4 v) => v.ToSystem();

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector4(SNVector4 v) => FromSystem(v);

        // Static operators  

        /// <summary>
        /// Adds two Vector4D instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator +(Vector4 left, Vector4 right) => left.ToSystem() + right.ToSystem();

        /// <summary>
        /// Subtracts the right Vector4D from the left Vector4D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator -(Vector4 left, Vector4 right) => left.ToSystem() - right.ToSystem();

        /// <summary>
        /// Multiplies two Vector4D instances element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator *(Vector4 left, Vector4 right) => left.ToSystem() * right.ToSystem();

        /// <summary>
        /// Multiplies a Vector4D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator *(Vector4 left, float scalar) => left.ToSystem() * scalar;

        /// <summary>
        /// Multiplies a scalar by a Vector4D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator *(float scalar, Vector4 right) => scalar * right.ToSystem();

        /// <summary>
        /// Divides the left Vector4D by the right Vector4D element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator /(Vector4 left, Vector4 right) => left.ToSystem() / right.ToSystem();

        /// <summary>
        /// Divides a Vector4D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator /(Vector4 left, float scalar) => left.ToSystem() / scalar;

        /// <summary>
        /// Negates the specified Vector4D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator -(Vector4 value) => -value.ToSystem();

        /// <summary>
        /// Determines whether two Vector4D instances are equal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Boolean operator ==(Vector4 left, Vector4 right) => left.ToSystem() == right.ToSystem();

        /// <summary>
        /// Determines whether two Vector4D instances are not equal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Boolean operator !=(Vector4 left, Vector4 right) => left.ToSystem() != right.ToSystem();

        #region IEquatable<Vector4D> Implementation

        /// <summary>
        /// Determines whether the specified Vector4D is equal to the current Vector4D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public bool Equals(Vector4 other) => ToSystem().Equals(other.ToSystem());

        /// <summary>
        /// Determines whether the specified object is equal to the current Vector4D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public override bool Equals(object? obj) => obj is Vector4 other && Equals(other);

        /// <summary>
        /// Returns a hash code for the Vector4D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y);

        /// <summary>
        /// Returns the dot product of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float Dot(Vector4 right) => SNVector4.Dot(this, right);

        /// <summary>
        /// Returns the Euclidean distance between two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float Distance(Vector4 value2) => SNVector4.Distance(this, value2);

        /// <summary>
        /// Returns the squared Euclidean distance between two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float DistanceSquared(Vector4 value2) => SNVector4.DistanceSquared(this, value2);

        /// <summary>
        /// Returns a vector that clamps each element of the <see cref="Vector4"/> between the corresponding elements of the minimum and maximum vectors.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Clamp(Vector4 min, Vector4 max) => SNVector4.Clamp(this, min, max);

        /// <summary>
        /// Returns a normalized version of the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Normalize() => SNVector4.Normalize(this);

        /// <summary>
        /// Returns the length of the <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float Length() => ToSystem().Length();

        /// <summary>
        /// Returns the squared length of the <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float LengthSquared() => ToSystem().LengthSquared();

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Abs() => SNVector4.Abs(this);

        /// <summary>
        /// Returns the square root of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 SquareRoot() => SNVector4.SquareRoot(this);

        #endregion

        #region Trigonometric Operations

        /// <summary>
        /// Returns the sine of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Sin() => SNVector4.Sin(this);

        /// <summary>
        /// Returns the cosine of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Cos() => SNVector4.Cos(this);

        /// <summary>
        /// Returns a vector whose elements are the sine and cosine of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public (Vector4 Sin, Vector4 Cos) SinCos()
        {
            var (sin, cos) = SNVector4.SinCos(this);
            return (sin, cos);
        }

        /// <summary>
        /// Converts degrees to radians for each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 DegreesToRadians() => SNVector4.DegreesToRadians(this);

        /// <summary>
        /// Converts radians to degrees for each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 RadiansToDegrees() => SNVector4.RadiansToDegrees(this);

        #endregion

        #region Exponential and Logarithmic Operations

        /// <summary>
        /// Returns the exponential of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Exp() => SNVector4.Exp(this);

        /// <summary>
        /// Returns the natural logarithm (base e) of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Log() => SNVector4.Log(this);

        /// <summary>
        /// Returns the base-2 logarithm of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Log2() => SNVector4.Log2(this);

        #endregion

        #region Transformation Operations

        /// <summary>
        /// Transforms a <see cref="Vector4"/> by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Transform(Matrix4x4 matrix) => SNVector4.Transform(this, matrix);

        /// <summary>
        /// Transforms a <see cref="Vector4"/> by a quaternion rotation.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Transform(Quaternion rotation) => SNVector4.Transform(this, rotation);
        
        #endregion

        #region Utility Operations

        /// <summary>
        /// Returns the maximum of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Max(Vector4 value2) => SNVector4.Max(this, value2);

        /// <summary>
        /// Returns the maximum magnitude of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 MaxMagnitude(Vector4 value2) => SNVector4.MaxMagnitude(this, value2);

        /// <summary>
        /// Returns the maximum magnitude float of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 MaxMagnitudeNumber(Vector4 value2) => SNVector4.MaxMagnitudeNumber(this, value2);

        /// <summary>
        /// Returns the maximum float of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 MaxNumber(Vector4 value2) => SNVector4.MaxNumber(this, value2);

        /// <summary>
        /// Returns the minimum of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Min(Vector4 value2) => SNVector4.Min(this, value2);

        /// <summary>
        /// Returns the minimum magnitude of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 MinMagnitude(Vector4 value2) => SNVector4.MinMagnitude(this, value2);

        /// <summary>
        /// Returns the minimum magnitude float of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 MinMagnitudeNumber(Vector4 value2) => SNVector4.MinMagnitudeNumber(this, value2);

        /// <summary>
        /// Returns the minimum float of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 MinNumber(Vector4 value2) => SNVector4.MinNumber(this, value2);

        /// <summary>
        /// Performs a fused multiply-add operation on the specified <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 FusedMultiplyAdd(Vector4 right, Vector4 addend) => SNVector4.FusedMultiplyAdd(this, right, addend);

        /// <summary>
        /// Performs a multiply-add estimate operation on the specified <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 MultiplyAddEstimate(Vector4 right, Vector4 addend) => SNVector4.MultiplyAddEstimate(this, right, addend);

        /// <summary>
        /// Returns a truncated version of the specified <see cref="Vector4"/>.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Truncate() => SNVector4.Truncate(this);

        #endregion

        #region Rounding Operations

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector4"/> to the nearest integer.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Round() => SNVector4.Round(this);

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector4"/> to the nearest integer using the specified rounding mode.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Round(MidpointRounding mode) => SNVector4.Round(this, mode);

        #endregion

        #region String Representation

        /// <summary>
        /// Returns the string representation of the <see cref="Vector4"/> using default formatting.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public override string ToString() => ToSystem().ToString();

        /// <summary>
        /// Returns the string representation of the <see cref="Vector4"/> using the specified format.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string ToString(string? format) => ToSystem().ToString(format);

        /// <summary>
        /// Returns the string representation of the <see cref="Vector4"/> using the specified format and format provider.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string ToString(string? format, IFormatProvider? formatProvider) => ToSystem().ToString(format, formatProvider);

        #endregion
    }
}
