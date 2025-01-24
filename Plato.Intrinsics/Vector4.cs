using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Serialization;
using static System.Runtime.CompilerServices.MethodImplOptions;
using SNVector4 = System.Numerics.Vector4;

namespace Plato
{
    [DataContract]
    public partial struct Vector4 
    {
        // Fields

        [DataMember] public readonly SNVector4 Value;

        // Constructor

        [MethodImpl(AggressiveInlining)]
        public Vector4(SNVector4 v) => Value = v;

        [MethodImpl(AggressiveInlining)]
        public Vector4(Number x, Number y, Number z, Number w) => Value = new(x, y, z, w);

        [MethodImpl(AggressiveInlining)]
        public Vector4(Number x) => Value = new(x);

        //-------------------------------------------------------------------------------------
        // Indexer
        //-------------------------------------------------------------------------------------

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

        public Number W
        {
            [MethodImpl(AggressiveInlining)]
            get => Value.W;
        }

        // Immutable "setters"

        [MethodImpl(AggressiveInlining)]
        public Vector4 WithX(Number x)
            => new(x, Y, Z, W);

        [MethodImpl(AggressiveInlining)]
        public Vector4 WithY(Number y)
            => new(X, y, Z, W);

        [MethodImpl(AggressiveInlining)]
        public Vector4 WithZ(Number z)
            => new(X, Y, z, W);

        [MethodImpl(AggressiveInlining)]
        public Vector4 WithW(Number w)
            => new(X, Y, Z, w);

        // Implicit casts 

        [MethodImpl(AggressiveInlining)]
        public static Vector4 FromSystem(SNVector4 v) => Unsafe.As<SNVector4, Vector4>(ref v);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator SNVector4(Vector4 v) => v.Value;

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector4(SNVector4 v) => FromSystem(v);

        [MethodImpl(AggressiveInlining)]
        public static unsafe implicit operator Vector128<float>(Vector4 v) => *(Vector128<float>*)&v;

        [MethodImpl(AggressiveInlining)]
        public static unsafe implicit operator Vector4(Vector128<float> v) => *(Vector4*)&v;

        // Static operators  

        /// <summary>
        /// Adds two Vector4D instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator +(Vector4 left, Vector4 right) => left.Value + right.Value;

        /// <summary>
        /// Subtracts the right Vector4D from the left Vector4D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator -(Vector4 left, Vector4 right) => left.Value - right.Value;

        /// <summary>
        /// Multiplies two Vector4D instances element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator *(Vector4 left, Vector4 right) => left.Value * right.Value;

        /// <summary>
        /// Multiplies a Vector4D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator *(Vector4 left, Number scalar) => left.Value * scalar;

        /// <summary>
        /// Multiplies a scalar by a Vector4D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator *(Number scalar, Vector4 right) => scalar * right.Value;

        /// <summary>
        /// Divides the left Vector4D by the right Vector4D element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator /(Vector4 left, Vector4 right) => left.Value / right.Value;

        /// <summary>
        /// Divides a Vector4D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator /(Vector4 left, Number scalar) => left.Value / scalar;

        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator %(Vector4 left, Vector4 right)
            => left - right * (left / right).Truncate;

        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator %(Vector4 left, Number scalar) 
            => left % new Vector4(scalar);

        /// <summary>
        /// Negates the specified Vector4D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector4 operator -(Vector4 value) => -value.Value;
        
        /// <summary>
        /// Returns the dot product of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Dot(Vector4 right) => SNVector4.Dot(Value, right);

        /// <summary>
        /// Returns the Euclidean distance between two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Distance(Vector4 value2) => SNVector4.Distance(Value, value2);

        /// <summary>
        /// Returns the squared Euclidean distance between two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number DistanceSquared(Vector4 value2) => SNVector4.DistanceSquared(Value, value2);

        /// <summary>
        /// Returns a vector that clamps each element of the <see cref="Vector4"/> between the corresponding elements of the minimum and maximum vectors.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Clamp(Vector4 min, Vector4 max) => SNVector4.Clamp(Value, min, max);

        /// <summary>
        /// Returns a normalized version of the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 Normalize
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.Normalize(Value);
        }

        /// <summary>
        /// Returns the length of the <see cref="Vector4"/>.
        /// </summary>
        public Number Length
        {
            [MethodImpl(AggressiveInlining)] get => Value.Length();
        }

        /// <summary>
        /// Returns the squared length of the <see cref="Vector4"/>.
        /// </summary>
        public Number LengthSquared
        {
            [MethodImpl(AggressiveInlining)] get => Value.LengthSquared();
        }

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 Abs
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.Abs(Value);
        }

        /// <summary>
        /// Returns the square root of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 SquareRoot
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.SquareRoot(Value);
        }

        /// <summary>
        /// Returns the sine of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 Sin
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.Sin(Value);
        }

        /// <summary>
        /// Returns the cosine of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 Cos
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.Cos(Value);
        }

        /// <summary>
        /// Returns a vector whose elements are the sine and cosine of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public (Vector4 Sin, Vector4 Cos) SinCos
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                var (sin, cos) = SNVector4.SinCos(Value);
                return (sin, cos);
            }
        }

        /// <summary>
        /// Converts degrees to radians for each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 DegreesToRadians
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.DegreesToRadians(Value);
        }

        /// <summary>
        /// Converts radians to degrees for each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 RadiansToDegrees
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.RadiansToDegrees(Value);
        }

        /// <summary>
        /// Returns the exponential of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 Exp
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.Exp(Value);
        }

        /// <summary>
        /// Returns the natural logarithm (base e) of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 Log
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.Log(Value);
        }

        /// <summary>
        /// Returns the base-2 logarithm of each element in the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 Log2
        {
            [MethodImpl(AggressiveInlining)] get => SNVector4.Log2(Value);
        }

        /// <summary>
        /// Transforms a <see cref="Vector4"/> by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Transform(Matrix4x4 matrix) => SNVector4.Transform(Value, matrix);

        /// <summary>
        /// Transforms a <see cref="Vector4"/> by a quaternion rotation.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Transform(Quaternion rotation) => SNVector4.Transform(Value, rotation);
        
        /// <summary>
        /// Returns the maximum of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Max(Vector4 value2) => SNVector4.Max(Value, value2);

        /// <summary>
        /// Returns the minimum of two <see cref="Vector4"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector4 Min(Vector4 value2) => SNVector4.Min(Value, value2);

        /// <summary>
        /// Returns a truncated version of the specified <see cref="Vector4"/>.
        /// </summary>
        public Vector4 Truncate
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector4.Truncate(Value); 
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest even integer.
        /// </summary>
        public Vector4 Round
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector4.Round(Value, MidpointRounding.ToEven);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, towards zero.
        /// </summary>
        public Vector4 RoundTowardsZero
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector4.Round(Value, MidpointRounding.ToZero);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, away from zero.
        /// </summary>
        public Vector4 RoundAwayFromZero
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector4.Round(Value, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, towards negative infinity
        /// </summary>
        public Vector4 Floor
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector4.Round(Value, MidpointRounding.ToNegativeInfinity);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, towards positive infinity
        /// </summary>
        public Vector4 Ceiling
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector4.Round(Value, MidpointRounding.ToPositiveInfinity);
        }
    }
}
