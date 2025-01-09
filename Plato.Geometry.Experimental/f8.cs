using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Runtime.InteropServices;

namespace Plato.Geometry.Experimental
{
    /// <summary>
    /// SIMD types can provide up to and beyond 8x improvements over traditional operations involving floats, by using extra wide
    /// registers on the system, and providing special opcodes for operating on them.
    ///
    /// A specific set of opcodes that can operate on 8 floats at a time known as AVX (Advanced Vector Extensions) is 
    /// widely available on the CPUs of most modern laptop and desktop computers. 
    /// https://en.wikipedia.org/wiki/Advanced_Vector_Extensions#CPUs_with_AVX
    ///  
    /// Working with SIMD types and intrinsics in C# however can be quite confusing for the uninitiated.
    /// There are over a thousand intrinsic opcodes, and many are poorly documented, and scattered across dozens of classes. 
    /// ChatGPT is unaware of recent introductions of utility functions in .NET 9 that make working with SIMD types much easier.
    ///
    /// This class provides a wrapper around the Vector256&lt;float&gt; type and provides many of the basic math operations
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct f8
    {
        public readonly Vector256<float> Value;

        //-------------------------------------------------------------------------------------
        // Constructors
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8(Vector256<float> value) => Value = value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8(float scalar) => Value = Vector256.Create(scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8(float f0, float f1, float f2, float f3, float f4, float f5, float f6, float f7)
            => Value = Vector256.Create(f0, f1, f2, f3, f4, f5, f6, f7);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8(Vector128<float> upper, Vector128<float> lower) => Value = Vector256.Create(lower, upper);

        //-------------------------------------------------------------------------------------
        // Implicit operators 
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector256<float>(in f8 value) => value.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator f8(in Vector256<float> value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator f8(float value) => new(value);

        //-------------------------------------------------------------------------------------
        // Constants
        //-------------------------------------------------------------------------------------

        public static f8 Zero = new(0);
        public static f8 One = new(1);
        public static f8 AllBitsSet = new(Vector256<float>.AllBitsSet);
        public static f8 SignMask = Vector256.Create(0x80000000u).AsSingle();
        public static f8 Indices => Vector256<float>.Indices;

        //-------------------------------------------------------------------------------------
        // Indexer
        //-------------------------------------------------------------------------------------

        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Value.GetElement(index);
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => 8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetElement(int index) => Value.GetElement(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector128<float> GetLower() => Value.GetLower();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector128<float> GetUpper() => Value.GetUpper();

        //-------------------------------------------------------------------------------------
        // Operator Overloads
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator +(in f8 left, in f8 right) => Vector256.Add<float>(left.Value, right.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator -(in f8 left, in f8 right) => Vector256.Subtract(left.Value, right.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator *(in f8 left, in f8 right) => Vector256.Multiply<float>(left.Value, right.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator *(in f8 left, float scalar) => Vector256.Multiply<float>(left.Value, scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator *(float scalar, in f8 right) => Vector256.Multiply(scalar, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator /(in f8 left, in f8 right) => Vector256.Divide<float>(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator /(in f8 left, float scalar) => Vector256.Divide<float>(left.Value, scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator -(in f8 value) => Vector256.Negate<float>(value);

        //-------------------------------------------------------------------------------------
        // Memory Load/Store
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe f8 Load(float* source) => new(Avx.LoadAlignedVector256(source));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void StoreAligned(float* destination) => Avx.Store(destination, Value);

        //-------------------------------------------------------------------------------------
        // Bitwise functions
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 AndNot(in f8 a, in f8 b) => Vector256.AndNot(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator &(in f8 a, in f8 b) => Vector256.BitwiseAnd(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator |(in f8 a, in f8 b) => Vector256.BitwiseOr(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator ~(in f8 a) => Vector256.OnesComplement(a.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator^(in f8 a, in f8 b) => Vector256.Xor(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 ConditionalSelect(f8 condition, f8 a, f8 b) => Vector256.ConditionalSelect(condition.Value, a.Value, b.Value);

        //-------------------------------------------------------------------------------------
        // Comparison operators 
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator==(in f8 a, in f8 b) => Vector256.Equals(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator!=(in f8 a, in f8 b) => ~Vector256.Equals(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator<(in f8 a, in f8 b) => Vector256.LessThan(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator<=(in f8 a, in f8 b) => Vector256.LessThanOrEqual(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator>(in f8 a, in f8 b) => Vector256.GreaterThan(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 operator>=(in f8 a, in f8 b) => Vector256.GreaterThanOrEqual(a.Value, b.Value);

        //-------------------------------------------------------------------------------------
        // Comparison functions
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 Max(in f8 a, in f8 b) => Vector256.Max(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 Min(in f8 a, in f8 b) => Vector256.Min(a.Value, b.Value);

        //-------------------------------------------------------------------------------------
        // Basic math functions 
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Sin() => Vector256.Sin(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Cos() => Vector256.Cos(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (f8, f8) SinCos() => Vector256.SinCos(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Abs() => Vector256.Abs(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Ceiling() => Vector256.Ceiling(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Clamp(in f8 min, in f8 max) => Vector256.Clamp(Value, min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 DegreesToRadians() => Vector256.DegreesToRadians(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 CopySign(in f8 value, in f8 sign) => Vector256.CopySign<float>(value, sign);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(in f8 a, in f8 b) => Vector256.Dot<float>(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Exp() => Vector256.Exp(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Floor() => Vector256.Floor(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 Hypot(in f8 x, in f8 y) => Vector256.Hypot(x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 IsNaN() => Vector256.IsNaN(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 IsNegative() => Vector256.IsNegative(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 IsPositive() => Vector256.IsPositive(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 IsPositiveInfinity() => Vector256.IsPositiveInfinity(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 IsZero() => Vector256.IsZero(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static f8 Lerp(Vector256<float> a, Vector256<float> b, Vector256<float> t) => Vector256.Lerp(a, b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Log() => Vector256.Log(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Log2() => Vector256.Log2(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 RadiansToDegrees() => Vector256.RadiansToDegrees(Value);

        /// <summary>Reciprocal (1/x) of each element</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Reciprocal() => Avx.Reciprocal(Value);

        /// <summary>Approximate reciprocal of the square root of each element: 1 / sqrt(x)</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 ReciprocalSqrt() => Avx.ReciprocalSqrt(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Round(MidpointRounding mr) => Vector256.Round(Value, mr);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Sign() => Vector256.CopySign(One.Value, Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Sqrt() => Vector256.Sqrt(Value);

        /// <summary>Square each element</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Square() => this * this;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Sum() => Vector256.Sum(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Tan()
        {
            var (a, b) = SinCos();
            return a / b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float FirstElement() => Vector256.ToScalar(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Truncate() => Vector256.Truncate(Value);

        //-------------------------------------------------------------------------------------
        // Pseudo-mutation operators 
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 WithElement(int i, float f) => Vector256.WithElement(this, i, f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 WithLower(Vector128<float> lower) => Vector256.WithLower(this, lower);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 WithUpper(Vector128<float> upper) => Vector256.WithUpper(this, upper);

        //-------------------------------------------------------------------------------------
        // Overrides
        //-------------------------------------------------------------------------------------

        public override string ToString()
            => $"[{this[0]}, {this[1]}, {this[2]}, {this[3]}, {this[4]}, {this[5]}, {this[6]}, {this[7]}]";

        public override bool Equals(object? obj)
            => obj is f8 other && Vector256.EqualsAll(Value, other.Value);

        public override int GetHashCode()
        {
            // Combine hash codes from each element
            int hash = 17;
            for (int i = 0; i < 8; i++) 
                hash = hash * 31 + this[i].GetHashCode();
            return hash;
        }
    }
}