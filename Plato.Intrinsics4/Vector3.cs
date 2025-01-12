using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;
using SNVector3 = System.Numerics.Vector3;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector3 : IEquatable<Vector3>
    {
        // Fields

        public readonly SNVector3 Value;
        
        // Constructor

        [MethodImpl(AggressiveInlining)]
        public Vector3(float x, float y, float z) 
            => Value = new(x, y, z);

        [MethodImpl(AggressiveInlining)]
        public Vector3(float x) : this(x, x, x) { }


        // Properties

        public float X
        {
            [MethodImpl(AggressiveInlining)]
            get => Value.X;
        }

        public float Y
        {
            [MethodImpl(AggressiveInlining)]
            get => Value.Y;
        }

        public float Z
        {
            [MethodImpl(AggressiveInlining)]
            get => Value.Z;
        }

        // Static properties

        public static readonly Vector3 E = SNVector3.E;
        public static readonly Vector3 Epsilon = SNVector3.Epsilon;
        public static readonly Vector3 NaN = SNVector3.NaN;
        public static readonly Vector3 NegativeInfinity = SNVector3.NegativeInfinity;
        public static readonly Vector3 NegativeZero = SNVector3.NegativeZero;
        public static readonly Vector3 One = SNVector3.One;
        public static readonly Vector3 Pi = SNVector3.Pi;
        public static readonly Vector3 PositiveInfinity = SNVector3.PositiveInfinity;
        public static readonly Vector3 Tau = SNVector3.Tau;
        public static readonly Vector3 UnitX = SNVector3.UnitX;
        public static readonly Vector3 UnitY = SNVector3.UnitY;
        public static readonly Vector3 UnitZ = SNVector3.UnitZ;
        public static readonly Vector3 Zero = SNVector3.Zero;

        // Implicit casts 
        
        [MethodImpl(AggressiveInlining)]
        public static Vector3 FromSystem(SNVector3 v) => Unsafe.As<SNVector3, Vector3>(ref v);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator SNVector3(Vector3 v) => v.Value;

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector3(SNVector3 v) => FromSystem(v);
        
        // Static operators  

        /// <summary>
        /// Adds two Vector3D instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator +(Vector3 left, Vector3 right) => left.Value + right.Value;

        /// <summary>
        /// Subtracts the right Vector3D from the left Vector3D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator -(Vector3 left, Vector3 right) => left.Value - right.Value;

        /// <summary>
        /// Multiplies two Vector3D instances element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator *(Vector3 left, Vector3 right) => left.Value * right.Value;

        /// <summary>
        /// Multiplies a Vector3D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator *(Vector3 left, float scalar) => left.Value * scalar;

        /// <summary>
        /// Multiplies a scalar by a Vector3D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator *(float scalar, Vector3 right) => scalar * right.Value;

        /// <summary>
        /// Divides the left Vector3D by the right Vector3D element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator /(Vector3 left, Vector3 right) => left.Value / right.Value;

        /// <summary>
        /// Divides a Vector3D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator /(Vector3 left, float scalar) => left.Value / scalar;

        /// <summary>
        /// Negates the specified Vector3D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator -(Vector3 value) => -value.Value;

        /// <summary>
        /// Determines whether two Vector3D instances are equal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Boolean operator ==(Vector3 left, Vector3 right) => left.Value == right.Value;

        /// <summary>
        /// Determines whether two Vector3D instances are not equal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Boolean operator !=(Vector3 left, Vector3 right) => left.Value != right.Value;

        /// <summary>
        /// Determines whether the specified Vector3D is equal to the current Vector3D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public bool Equals(Vector3 other) => Value.Equals(other.Value);

        /// <summary>
        /// Determines whether the specified object is equal to the current Vector3D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public override bool Equals(object? obj) => obj is Vector3 other && Equals(other);

        /// <summary>
        /// Returns a hash code for the Vector3D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Returns the dot product of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float Dot(Vector3 right) => SNVector3.Dot(this, right);

        /// <summary>
        /// Returns the dot product of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Cross(Vector3 right) => SNVector3.Cross(this, right);

        /// <summary>
        /// Returns the Euclidean distance between two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float Distance(Vector3 value2) => SNVector3.Distance(this, value2);

        /// <summary>
        /// Returns the squared Euclidean distance between two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float DistanceSquared(Vector3 value2) => SNVector3.DistanceSquared(this, value2);

        /// <summary>
        /// Returns a vector that clamps each element of the <see cref="Vector3"/> between the corresponding elements of the minimum and maximum vectors.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Clamp(Vector3 min, Vector3 max) => SNVector3.Clamp(this, min, max);

        /// <summary>
        /// Returns a normalized version of the specified <see cref="Vector3"/>.
        /// </summary>
        public Vector3 Normalize
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Normalize(this);
        }

        /// <summary>
        /// Returns the length of the <see cref="Vector3"/>.
        /// </summary>
        public float Length
        {
            [MethodImpl(AggressiveInlining)] get => Value.Length();
        }

        /// <summary>
        /// Returns the squared length of the <see cref="Vector3"/>.
        /// </summary>
        public float LengthSquared
        {
            [MethodImpl(AggressiveInlining)] get => Value.LengthSquared();
        }

        /// <summary>
        /// Returns a vector that is the reflection of the specified vector off a plane defined by the specified normal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Reflect(Vector3 normal) => SNVector3.Reflect(this, normal);

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each element.
        /// </summary>
        public Vector3 Abs
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Abs(this);
        }

        /// <summary>
        /// Returns the square root of each element.
        /// </summary>
        public Vector3 SquareRoot
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.SquareRoot(this);
        }

        /// <summary>
        /// Returns the sine of each element.
        /// </summary>
        public Vector3 Sin
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Sin(this);
        }

        /// <summary>
        /// Returns the cosine of each element.
        /// </summary>
        public Vector3 Cos
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Cos(this);
        }

        /// <summary>
        /// Returns a vector whose elements are the sine and cosine of each element.
        /// </summary>
        public (Vector3 Sin, Vector3 Cos) SinCos
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                var (sin, cos) = SNVector3.SinCos(this);
                return (sin, cos);
            }
        }

        /// <summary>
        /// Converts degrees to radians for each element.
        /// </summary>
        public Vector3 DegreesToRadians
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.DegreesToRadians(this);
        }

        /// <summary>
        /// Converts radians to degrees for each element.
        /// </summary>
        public Vector3 RadiansToDegrees
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.RadiansToDegrees(this);
        }

        /// <summary>
        /// Returns the exponential of each element.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Exp() => SNVector3.Exp(this);

        /// <summary>
        /// Returns the natural logarithm (base e) of each element.
        /// </summary>
        public Vector3 Log
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Log(this);
        }

        /// <summary>
        /// Returns the base-2 logarithm of each element.
        /// </summary>
        public Vector3 Log2
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Log2(this);
        }

        /// <summary>
        /// Transforms by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Transform(Matrix4x4 matrix) => SNVector3.Transform(this, matrix);

        /// <summary>
        /// Transforms by a quaternion rotation.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Transform(Quaternion rotation) => SNVector3.Transform(this, rotation);

        /// <summary>
        /// Transforms a normal vector by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 TransformNormal(Matrix4x4 matrix) => SNVector3.TransformNormal(this, matrix);
        
        /// <summary>
        /// Returns the maximum of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Max(Vector3 value2) => SNVector3.Max(this, value2);

        /// <summary>
        /// Returns the maximum magnitude of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 MaxMagnitude(Vector3 value2) => SNVector3.MaxMagnitude(this, value2);

        /// <summary>
        /// Returns the maximum magnitude float of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 MaxMagnitudeNumber(Vector3 value2) => SNVector3.MaxMagnitudeNumber(this, value2);

        /// <summary>
        /// Returns the maximum float of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 MaxNumber(Vector3 value2) => SNVector3.MaxNumber(this, value2);

        /// <summary>
        /// Returns the minimum of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Min(Vector3 value2) => SNVector3.Min(this, value2);

        /// <summary>
        /// Returns the minimum magnitude of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 MinMagnitude(Vector3 value2) => SNVector3.MinMagnitude(this, value2);

        /// <summary>
        /// Returns the minimum magnitude float of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 MinMagnitudeNumber(Vector3 value2) => SNVector3.MinMagnitudeNumber(this, value2);

        /// <summary>
        /// Returns the minimum float of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 MinNumber(Vector3 value2) => SNVector3.MinNumber(this, value2);

        /// <summary>
        /// Performs a fused multiply-add operation on the specified <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 FusedMultiplyAdd(Vector3 right, Vector3 addend) => SNVector3.FusedMultiplyAdd(this, right, addend);

        /// <summary>
        /// Performs a multiply-add estimate operation on the specified <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 MultiplyAddEstimate(Vector3 right, Vector3 addend) => SNVector3.MultiplyAddEstimate(this, right, addend);

        /// <summary>
        /// Returns a truncated version of the specified <see cref="Vector3"/>.
        /// </summary>
        public Vector3 Truncate
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Truncate(this);
        }
        
        /// <summary>
        /// Rounds each element of the specified <see cref="Vector3"/> to the nearest integer.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Round() => SNVector3.Round(this);

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector3"/> to the nearest integer using the specified rounding mode.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Round(MidpointRounding mode) => SNVector3.Round(this, mode);

        /// <summary>
        /// Returns the string representation of the <see cref="Vector3"/> using default formatting.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public override string ToString() => Value.ToString();

        /// <summary>
        /// Returns the string representation of the <see cref="Vector3"/> using the specified format.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string ToString(string? format) => Value.ToString(format);

        /// <summary>
        /// Returns the string representation of the <see cref="Vector3"/> using the specified format and format provider.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string ToString(string? format, IFormatProvider? formatProvider) => Value.ToString(format, formatProvider);
    }
}
