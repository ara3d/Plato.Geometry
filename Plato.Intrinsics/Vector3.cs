using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using static System.Runtime.CompilerServices.MethodImplOptions;
using SNVector3 = System.Numerics.Vector3;

namespace Plato
{
    [DataContract]
    public partial struct Vector3 
    {
        // Fields

        [DataMember] public readonly SNVector3 Value;

        // Constructor

        [MethodImpl(AggressiveInlining)]
        public Vector3(SNVector3 v) => Value = v;

        [MethodImpl(AggressiveInlining)]
        public Vector3(Number x, Number y, Number z) => Value = new(x, y, z);

        [MethodImpl(AggressiveInlining)]
        public Vector3(Number x) => Value = new(x);

        // Properties

        public Number X
        {
            [MethodImpl(AggressiveInlining)]
            get => Value.X;
        }

        public Number Y
        {
            [MethodImpl(AggressiveInlining)]
            get => Value.Y;
        }

        public Number Z
        {
            [MethodImpl(AggressiveInlining)]
            get => Value.Z;
        }

        // Immutable "setters"

        [MethodImpl(AggressiveInlining)]
        public Vector3 WithX(Number x)
            => new(x, Y, Z);

        [MethodImpl(AggressiveInlining)]
        public Vector3 WithY(Number y)
            => new(X, y, Z);

        [MethodImpl(AggressiveInlining)]
        public Vector3 WithZ(Number z)
            => new(X, Y, z);

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
        public static Vector3 operator *(Vector3 left, Number scalar) => left.Value * scalar;

        /// <summary>
        /// Multiplies a scalar by a Vector3D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator *(Number scalar, Vector3 right) => scalar * right.Value;

        /// <summary>
        /// Divides the left Vector3D by the right Vector3D element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator /(Vector3 left, Vector3 right) => left.Value / right.Value;

        /// <summary>
        /// Divides a Vector3D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator /(Vector3 left, Number scalar) => left.Value / scalar;

        /// <summary>
        /// Negates the specified Vector3D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector3 operator -(Vector3 value) => -value.Value;
        
        /// <summary>
        /// Returns the dot product of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Dot(Vector3 right) => SNVector3.Dot(Value, right);

        /// <summary>
        /// Returns the dot product of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Cross(Vector3 right) => SNVector3.Cross(Value, right);

        /// <summary>
        /// Returns the Euclidean distance between two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Distance(Vector3 value2) => SNVector3.Distance(Value, value2);

        /// <summary>
        /// Returns the squared Euclidean distance between two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number DistanceSquared(Vector3 value2) => SNVector3.DistanceSquared(Value, value2);

        /// <summary>
        /// Returns a vector that clamps each element of the <see cref="Vector3"/> between the corresponding elements of the minimum and maximum vectors.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Clamp(Vector3 min, Vector3 max) => SNVector3.Clamp(Value, min, max);

        /// <summary>
        /// Returns a normalized version of the specified <see cref="Vector3"/>.
        /// </summary>
        public Vector3 Normalize
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Normalize(Value);
        }

        /// <summary>
        /// Returns the length of the <see cref="Vector3"/>.
        /// </summary>
        public Number Length
        {
            [MethodImpl(AggressiveInlining)] get => Value.Length();
        }

        /// <summary>
        /// Returns the squared length of the <see cref="Vector3"/>.
        /// </summary>
        public Number LengthSquared
        {
            [MethodImpl(AggressiveInlining)] get => Value.LengthSquared();
        }

        /// <summary>
        /// Returns a vector that is the reflection of the specified vector off a plane defined by the specified normal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Reflect(Vector3 normal) => SNVector3.Reflect(Value, normal);

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each element.
        /// </summary>
        public Vector3 Abs
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Abs(Value);
        }

        /// <summary>
        /// Returns the square root of each element.
        /// </summary>
        public Vector3 SquareRoot
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.SquareRoot(Value);
        }

        /// <summary>
        /// Returns the sine of each element.
        /// </summary>
        public Vector3 Sin
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Sin(Value);
        }

        /// <summary>
        /// Returns the cosine of each element.
        /// </summary>
        public Vector3 Cos
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Cos(Value);
        }

        /// <summary>
        /// Returns a vector whose elements are the sine and cosine of each element.
        /// </summary>
        public (Vector3 Sin, Vector3 Cos) SinCos
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                var (sin, cos) = SNVector3.SinCos(Value);
                return (sin, cos);
            }
        }

        /// <summary>
        /// Converts degrees to radians for each element.
        /// </summary>
        public Vector3 DegreesToRadians
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.DegreesToRadians(Value);
        }

        /// <summary>
        /// Converts radians to degrees for each element.
        /// </summary>
        public Vector3 RadiansToDegrees
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.RadiansToDegrees(Value);
        }

        /// <summary>
        /// Returns the exponential of each element.
        /// </summary>
        public Vector3 Exp
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Exp(Value);
        }

        /// <summary>
        /// Returns the natural logarithm (base e) of each element.
        /// </summary>
        public Vector3 Log
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Log(Value);
        }

        /// <summary>
        /// Returns the base-2 logarithm of each element.
        /// </summary>
        public Vector3 Log2
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Log2(Value);
        }

        /// <summary>
        /// Transforms by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Transform(Matrix4x4 matrix) => SNVector3.Transform(Value, matrix);

        /// <summary>
        /// Transforms by a quaternion rotation.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Transform(Quaternion rotation) => SNVector3.Transform(Value, rotation);

        /// <summary>
        /// Transforms a normal vector by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 TransformNormal(Matrix4x4 matrix) => SNVector3.TransformNormal(Value, matrix);
        
        /// <summary>
        /// Returns the maximum of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Max(Vector3 value2) => SNVector3.Max(Value, value2);

        /// <summary>
        /// Returns the minimum of two <see cref="Vector3"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector3 Min(Vector3 value2) => SNVector3.Min(Value, value2);

        /// <summary>
        /// Returns a truncated version of the specified <see cref="Vector3"/>.
        /// </summary>
        public Vector3 Truncate
        {
            [MethodImpl(AggressiveInlining)] get => SNVector3.Truncate(Value);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest even integer
        /// </summary>
        public Vector3 Round
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector3.Round(Value, MidpointRounding.ToEven);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, towards zero.
        /// </summary>
        public Vector3 RoundTowardsZero
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector3.Round(Value, MidpointRounding.ToZero);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, away from zero.
        /// </summary>
        public Vector3 RoundAwayFromZero
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector3.Round(Value, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, towards negative infinity
        /// </summary>
        public Vector3 Floor
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector3.Round(Value, MidpointRounding.ToNegativeInfinity);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, towards positive infinity
        /// </summary>
        public Vector3 Ceiling
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector3.Round(Value, MidpointRounding.ToPositiveInfinity);
        }
    }
}
