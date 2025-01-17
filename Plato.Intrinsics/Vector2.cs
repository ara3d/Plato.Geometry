using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using static System.Runtime.CompilerServices.MethodImplOptions;
using SNVector2 = System.Numerics.Vector2;

namespace Plato
{
    [DataContract]
    public partial struct Vector2 
    {
        // Fields

        [DataMember] public SNVector2 Value;

        // Constructor

        [MethodImpl(AggressiveInlining)]
        public Vector2(Number x, Number y) => Value = new(x, y);

        [MethodImpl(AggressiveInlining)]
        public Vector2(Number x) => Value = new(x);

        [MethodImpl(AggressiveInlining)]
        public Vector2(SNVector2 x) => Value = x;

        //-------------------------------------------------------------------------------------
        // Indexer
        //-------------------------------------------------------------------------------------

        public Number this[Integer index]
        {
            [MethodImpl(AggressiveInlining)]
            get => index == 0 ? X
                : index == 1 ? Y
                : throw new IndexOutOfRangeException();
        }

        public Integer Count
        {
            [MethodImpl(AggressiveInlining)]
            get => 2;
        }

        //-------------------------------------------------------------------------------------
        // Properties
        //-------------------------------------------------------------------------------------

        public Number X { [MethodImpl(AggressiveInlining)] get => Value.X; }
        public Number Y { [MethodImpl(AggressiveInlining)] get => Value.Y; }

        //-------------------------------------------------------------------------------------
        // Immutable "setters"
        //-------------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public Vector2 WithX(Number x) => new(x, Y);

        [MethodImpl(AggressiveInlining)]
        public Vector2 WithY(Number y) => new(X, y);

        // Implicit casts 
        
        [MethodImpl(AggressiveInlining)]
        public static Vector2 FromSystem(SNVector2 v) => Unsafe.As<SNVector2, Vector2>(ref v);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator SNVector2(Vector2 v) => v.Value;

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector2(SNVector2 v) => FromSystem(v);

        // Static operators  

        /// <summary>
        /// Adds two Vector2D instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator +(Vector2 left, Vector2 right) => left.Value + right.Value;

        /// <summary>
        /// Subtracts the right Vector2D from the left Vector2D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator -(Vector2 left, Vector2 right) => left.Value - right.Value;

        /// <summary>
        /// Multiplies two Vector2D instances element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator *(Vector2 left, Vector2 right) => left.Value * right.Value;

        /// <summary>
        /// Multiplies a Vector2D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator *(Vector2 left, float scalar) => left.Value * scalar;

        /// <summary>
        /// Multiplies a scalar by a Vector2D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator *(float scalar, Vector2 right) => scalar * right.Value;

        /// <summary>
        /// Divides the left Vector2D by the right Vector2D element-wise.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator /(Vector2 left, Vector2 right) => left.Value / right.Value;

        /// <summary>
        /// Divides a Vector2D by a scalar.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator /(Vector2 left, float scalar) => left.Value / scalar;

        /// <summary>
        /// Negates the specified Vector2D.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static Vector2 operator -(Vector2 value) => -value.Value;

        /// <summary>
        /// Returns the dot product of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float Dot(Vector2 right) => SNVector2.Dot(Value, right);

        /// <summary>
        /// Returns the Euclidean distance between two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float Distance(Vector2 value2) => SNVector2.Distance(Value, value2);

        /// <summary>
        /// Returns the squared Euclidean distance between two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public float DistanceSquared(Vector2 value2) => SNVector2.DistanceSquared(Value, value2);

        /// <summary>
        /// Returns a vector that clamps each element of the <see cref="Vector2"/> between the corresponding elements of the minimum and maximum vectors.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Clamp(Vector2 min, Vector2 max) => SNVector2.Clamp(Value, min, max);

        /// <summary>
        /// Returns a normalized version of the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Normalize
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Normalize(Value);
        }

        /// <summary>
        /// Returns the length of the <see cref="Vector2"/>.
        /// </summary>
        public float Length
        {
            [MethodImpl(AggressiveInlining)] get => Value.Length();
        }

        /// <summary>
        /// Returns the squared length of the <see cref="Vector2"/>.
        /// </summary>
        public float LengthSquared
        {
            [MethodImpl(AggressiveInlining)]
            get =>  Value.LengthSquared();
        }

        /// <summary>
        /// Returns a vector that is the reflection of the specified vector off a plane defined by the specified normal.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Reflect(Vector2 normal) => SNVector2.Reflect(Value, normal);

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Abs
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Abs(Value);
        }

        /// <summary>
        /// Returns the square root of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 SquareRoot
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.SquareRoot(Value);
        }

        /// <summary>
        /// Returns the sine of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Sin
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Sin(Value);
        }

        /// <summary>
        /// Returns the cosine of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Cos
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Cos(Value);
        }

        /// <summary>
        /// Returns a vector whose elements are the sine and cosine of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public (Vector2 Sin, Vector2 Cos) SinCos
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                var (sin, cos) = SNVector2.SinCos(Value);
                return (sin, cos);
            }
        }

        /// <summary>
        /// Converts degrees to radians for each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 DegreesToRadians
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.DegreesToRadians(Value);
        }

        /// <summary>
        /// Converts radians to degrees for each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 RadiansToDegrees
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.RadiansToDegrees(Value);
        }

        /// <summary>
        /// Returns the exponential of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Exp
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Exp(Value);
        }

        /// <summary>
        /// Returns the natural logarithm (base e) of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Log
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Log(Value);
        }

        /// <summary>
        /// Returns the base-2 logarithm of each element in the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Log2
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Log2(Value);
        }

        /// <summary>
        /// Transforms a <see cref="Vector2"/> by a 3x2 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Transform(Matrix3x2 matrix) => SNVector2.Transform(Value, matrix);

        /// <summary>
        /// Transforms a <see cref="Vector2"/> by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Transform(Matrix4x4 matrix) => SNVector2.Transform(Value, matrix);

        /// <summary>
        /// Transforms a <see cref="Vector2"/> by a quaternion rotation.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Transform(Quaternion rotation) => SNVector2.Transform(Value, rotation);

        /// <summary>
        /// Transforms a normal vector by a 3x2 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 TransformNormal(Matrix3x2 matrix) => SNVector2.TransformNormal(Value, matrix);

        /// <summary>
        /// Transforms a normal vector by a 4x4 matrix.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 TransformNormal(Matrix4x4 matrix) => SNVector2.TransformNormal(Value, matrix);

        /// <summary>
        /// Returns the maximum of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Max(Vector2 value2) => SNVector2.Max(Value, value2);

        /// <summary>
        /// Returns the minimum of two <see cref="Vector2"/> instances.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Vector2 Min(Vector2 value2) => SNVector2.Min(Value, value2);

        /// <summary>
        /// Returns a truncated version of the specified <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Truncate
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Truncate(Value);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest even integer.
        /// </summary>
        public Vector2 Round
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector2.Round(Value, MidpointRounding.ToEven);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, towards zero.
        /// </summary>
        public Vector2 RoundTowardsZero
        {
            [MethodImpl(AggressiveInlining)] get => SNVector2.Round(Value, MidpointRounding.ToZero);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, away from zero.
        /// </summary>
        public Vector2 RoundAwayFromZero
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector2.Round(Value, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, towards negative infinity
        /// </summary>
        public Vector2 Floor
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector2.Round(Value, MidpointRounding.ToNegativeInfinity);
        }
        
        /// <summary>
        /// Rounds each element of the specified <see cref="Vector2"/> to the nearest integer, towards positive infinity
        /// </summary>
        public Vector2 Ceiling
        {
            [MethodImpl(AggressiveInlining)]
            get => SNVector2.Round(Value, MidpointRounding.ToPositiveInfinity);
        }
    }
}
