using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    /// <summary>
    /// This is a wrapper around Vector256&lt;float&gt; that provides a more user-friendly API.
    /// Note that the Vector256 class can be found in the Runtime.Intrinsics namespace, and
    /// has some design difference from Vector2, Vector3, and Vector4. One of the more notable
    /// differences is that a Vector8 is sometimes intended used as a bit-mask, or an array of booleans, 
    /// and there are a number of bit-oriented functions around them. The intent of the
    /// Vector256 type was as a wrapper around SIMD operations, and less as a general-purpose vector type.
    /// In the end, we decided to put it in the same namespace, and expose a similar API, as Vector4.  
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector8 : IEquatable<Vector8>
    {
        public readonly Vector256<float> Value;

        //-------------------------------------------------------------------------------------
        // Constructors
        //-------------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public Vector8(Vector256<float> value) => Value = value;

        [MethodImpl(AggressiveInlining)]
        public Vector8(Number scalar) => Value = Vector256.Create(scalar.Value);

        [MethodImpl(AggressiveInlining)]
        public Vector8(Number f0, Number f1, Number f2, Number f3, Number f4, Number f5, Number f6, Number f7)
            => Value = Vector256.Create(f0, f1, f2, f3, f4, f5, f6, f7);

        [MethodImpl(AggressiveInlining)]
        public Vector8(Vector128<float> upper, Vector128<float> lower) => Value = Vector256.Create(lower, upper);

        //-------------------------------------------------------------------------------------
        // Implicit operators 
        //-------------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector256<float>(Vector8 value) => value.Value;

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector8(Vector256<float> value) => new(value);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector8(Number value) => new(value);

        //-------------------------------------------------------------------------------------
        // Constants
        //-------------------------------------------------------------------------------------

        public static Vector8 Zero = new(0);
        public static Vector8 One = new(1);
        public static Vector8 AllBitsSet = new(Vector256<float>.AllBitsSet);
        public static Vector8 SignMask = Vector256.Create(0x80000000u).AsSingle();
        public static Vector8 Indices => Vector256<float>.Indices;

        //-------------------------------------------------------------------------------------
        // Indexer
        //-------------------------------------------------------------------------------------

        public Number this[int index]
        {
            [MethodImpl(AggressiveInlining)]
            get => Value.GetElement(index);
        }

        public int Count
        {
            [MethodImpl(AggressiveInlining)]
            get => 8;
        }

        public Vector128<float> Lower
        {
            [MethodImpl(AggressiveInlining)] get => Value.GetLower();
        }

        public Vector128<float> Upper
        {
            [MethodImpl(AggressiveInlining)] get => Value.GetUpper();
        }

        //-------------------------------------------------------------------------------------
        // Operator Overloads
        //-------------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator +(Vector8 left, Vector8 right) => Vector256.Add(left.Value, right.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator -(Vector8 left, Vector8 right) => Vector256.Subtract(left.Value, right.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator *(Vector8 left, Vector8 right) => Vector256.Multiply(left.Value, right.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator *(Vector8 left, Number scalar) => Vector256.Multiply(left.Value, scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator *(Number scalar, Vector8 right) => Vector256.Multiply(scalar, right.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator /(Vector8 left, Vector8 right) => Vector256.Divide(left.Value, right.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator /(Vector8 left, Number scalar) => Vector256.Divide(left.Value, scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator /(Number scalar, Vector8 right) => Vector256.Divide(new Vector8(scalar), right.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator -(Vector8 value) => Vector256.Negate(value.Value);

        //-------------------------------------------------------------------------------------
        // Bitwise functions
        //-------------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public static Vector8 AndNot(Vector8 a, Vector8 b) => Vector256.AndNot(a.Value, b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator &(Vector8 a, Vector8 b) => Vector256.BitwiseAnd(a.Value, b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator |(Vector8 a, Vector8 b) => Vector256.BitwiseOr(a.Value, b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator ~(Vector8 a) => Vector256.OnesComplement(a.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator ^(Vector8 a, Vector8 b) => Vector256.Xor(a.Value, b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 ConditionalSelect(Vector8 condition, Vector8 a, Vector8 b) => Vector256.ConditionalSelect(condition.Value, a.Value, b.Value);

        //-------------------------------------------------------------------------------------
        // Comparison operators 
        //-------------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator ==(Vector8 a, Vector8 b) => Vector256.Equals(a.Value, b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator !=(Vector8 a, Vector8 b) => ~Vector256.Equals(a.Value, b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator <(Vector8 a, Vector8 b) => Vector256.LessThan(a.Value, b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator <=(Vector8 a, Vector8 b) => Vector256.LessThanOrEqual(a.Value, b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator >(Vector8 a, Vector8 b) => Vector256.GreaterThan(a.Value, b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Vector8 operator >=(Vector8 a, Vector8 b) => Vector256.GreaterThanOrEqual(a.Value, b.Value);

        //-------------------------------------------------------------------------------------
        // Comparison functions
        //-------------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public Vector8 Max(Vector8 other) => Vector256.Max(Value, other.Value);

        [MethodImpl(AggressiveInlining)]
        public Vector8 Min(Vector8 other) => Vector256.Min(Value, other.Value);

        //-------------------------------------------------------------------------------------
        // Basic math functions 
        //-------------------------------------------------------------------------------------

        public Vector8 Sin
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Sin(Value);
        }

        public Vector8 Cos
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Cos(Value);
        }

        public (Vector8, Vector8) SinCos
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.SinCos(Value);
        }

        public Vector8 Abs
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Abs(Value);
        }

        public Vector8 Ceiling
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Ceiling(Value);
        }

        [MethodImpl(AggressiveInlining)]
        public Vector8 Clamp(Vector8 min, Vector8 max) => Vector256.Clamp(Value, min.Value, max.Value);

        public Vector8 DegreesToRadians
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.DegreesToRadians(Value);
        }

        [MethodImpl(AggressiveInlining)]
        public Vector8 CopySign(Vector8 sign) => Vector256.CopySign(Value, sign.Value);

        [MethodImpl(AggressiveInlining)]
        public Number Dot(Vector8 other) => Vector256.Dot(Value, other.Value);

        public Vector8 Exp
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Exp(Value);
        }

        public Vector8 Floor
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Floor(Value);
        }

        [MethodImpl(AggressiveInlining)]
        public Vector8 Hypot(Vector8 other) => Vector256.Hypot(Value, other.Value);

        public Vector8 IsNaN
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.IsNaN(Value);
        }

        public Vector8 IsNegative
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.IsNegative(Value);
        }

        public Vector8 IsPositive
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.IsPositive(Value);
        }

        public Vector8 IsPositiveInfinity
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.IsPositiveInfinity(Value);
        }

        public Vector8 IsZero
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.IsZero(Value);
        }

        [MethodImpl(AggressiveInlining)]
        public Vector8 Lerp(Vector8 b, Vector8 t) 
            => Vector256.Lerp(Value, b.Value, t.Value);

        public Vector8 Log
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Log(Value);
        }

        public Vector8 Log2
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Log2(Value);
        }

        public Vector8 RadiansToDegrees
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.RadiansToDegrees(Value);
        }

        /// <summary>Reciprocal (1/x) of each element</summary>
        public Vector8 Reciprocal
        {
            [MethodImpl(AggressiveInlining)] get => Avx.Reciprocal(Value);
        }

        /// <summary>Approximate reciprocal of the square root of each element: 1 / sqrt(x)</summary>
        public Vector8 ReciprocalSqrt
        {
            [MethodImpl(AggressiveInlining)] get => Avx.ReciprocalSqrt(Value);
        }

        [MethodImpl(AggressiveInlining)]
        public Vector8 Round(MidpointRounding mr) => Vector256.Round(Value, mr);

        public Vector8 Sign
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.CopySign(One.Value, Value);
        }

        public Vector8 Sqrt
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Sqrt(Value);
        }

        /// <summary>Square each element</summary>
        public Vector8 Square
        {
            [MethodImpl(AggressiveInlining)] get => this * this;
        }

        public Number Sum
        {
            [MethodImpl(AggressiveInlining)] get => Vector256.Sum(Value);
        }

        public Vector8 Tan
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                var (a, b) = SinCos;
                return a / b;
            }
        }

        [MethodImpl(AggressiveInlining)]
        public Number FirstElement() => Vector256.ToScalar(Value);

        [MethodImpl(AggressiveInlining)]
        public Vector8 Truncate() => Vector256.Truncate(Value);

        //-------------------------------------------------------------------------------------
        // Pseudo-mutation operators 
        //-------------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public Vector8 WithElement(int i, Number f) => Vector256.WithElement(Value, i, f);

        [MethodImpl(AggressiveInlining)]
        public Vector8 WithLower(Vector128<float> lower) => Vector256.WithLower(this, lower);

        [MethodImpl(AggressiveInlining)]
        public Vector8 WithUpper(Vector128<float> upper) => Vector256.WithUpper(this, upper);

        //-------------------------------------------------------------------------------------
        // Overrides
        //-------------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public override string ToString()
            => $"[{this[0]}, {this[1]}, {this[2]}, {this[3]}, {this[4]}, {this[5]}, {this[6]}, {this[7]}]";

        [MethodImpl(AggressiveInlining)]
        public override bool Equals(object? obj)
            => obj is Vector8 other && Vector256.EqualsAll(Value, other.Value);

        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode() 
            => Value.GetHashCode();

        [MethodImpl(AggressiveInlining)]
        public bool Equals(Vector8 other) 
            => Value.Equals(other.Value);
    }
}