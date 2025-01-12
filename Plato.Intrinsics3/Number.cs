using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    /// <summary>
    /// A simple wrapper around the built-in <c>float</c> type, 
    /// forwarding all arithmetic and common methods to <c>float</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Number : IEquatable<Number>, IComparable<Number>, IComparable
    {
        // -------------------------------------------------------------------------------
        // Field (the wrapped float)
        // -------------------------------------------------------------------------------
        public readonly float Value;

        // -------------------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public Number(float value) => Value = value;

        // -------------------------------------------------------------------------------
        // Convert to/from float
        // -------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public float ToSystem() => Value;

        [MethodImpl(AggressiveInlining)]
        public static Number FromSystem(float f) => new(f);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Number(float f) => FromSystem(f);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator float(Number n) => n.ToSystem();

        // -------------------------------------------------------------------------------
        // Common Constants
        // -------------------------------------------------------------------------------
        
        public static readonly Number Zero = 0f;
        public static readonly Number One = 1f;
        public static readonly Number NegativeOne = -1f;
        public static readonly Number E = MathF.E;
        public static readonly Number Pi = MathF.PI;
        public static readonly Number Tau = MathF.Tau;
        public static readonly Number HalfPi = MathF.PI / 2f;
        public static readonly Number TwoPi = MathF.PI * 2f;
        public static readonly Number Sqrt2 = MathF.Sqrt(2f);
        public static readonly Number Sqrt3 = MathF.Sqrt(3f);
        public static readonly Number NegativeInfinity = float.NegativeInfinity;
        public static readonly Number PositiveInfinity = float.PositiveInfinity;
        public static readonly Number NaN = float.NaN;
        public static readonly Number MinValue = float.MinValue;
        public static readonly Number MaxValue = float.MaxValue;
        public static readonly Number Epsilon = float.Epsilon;

        // -------------------------------------------------------------------------------
        // Operators (forward to float)
        // -------------------------------------------------------------------------------
        [MethodImpl(AggressiveInlining)]
        public static Number operator +(Number a, Number b)
            => a.Value + b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Number operator -(Number a, Number b)
            => a.Value - b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Number operator *(Number a, Number b)
            => a.Value * b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Number operator /(Number a, Number b)
            => a.Value / b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Number operator -(Number n)
            => -n.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator ==(Number a, Number b)
            => a.Value == b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator !=(Number a, Number b)
            => a.Value != b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator <(Number a, Number b)
            => a.Value < b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator <=(Number a, Number b)
            => a.Value <= b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator >(Number a, Number b)
            => a.Value > b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator >=(Number a, Number b)
            => a.Value >= b.Value;

        // -------------------------------------------------------------------------------
        // Equality, hashing, and ToString
        // -------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public bool Equals(Number other)
            => Value.Equals(other.Value);

        [MethodImpl(AggressiveInlining)]
        public override bool Equals(object? obj)
            => obj is Number n && Equals(n);

        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode()
            => Value.GetHashCode();

        [MethodImpl(AggressiveInlining)]
        public override string ToString()
            => Value.ToString();

        // -------------------------------------------------------------------------------
        // IComparable / IComparable<Number> Implementation
        // -------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public int CompareTo(Number other)
            => Value.CompareTo(other.Value);

        // CompareTo(object) just boxes 'other' and calls CompareTo(Number)
        [MethodImpl(AggressiveInlining)]
        public int CompareTo(object obj)
        {
            if (obj is Number n)
                return CompareTo(n);
            throw new ArgumentException("Object is not a Number");
        }

        //-------------------------------------------------------------------------------
        // Conversions to Angles
        //-------------------------------------------------------------------------------

        /// <summary>
        /// The angle represented by this number of half-turns.
        /// </summary>
        public Angle HalfTurns
        {
            [MethodImpl(AggressiveInlining)] get => this * Pi;
        }

        /// <summary>
        /// The angle represented by this number of full-turns.
        /// </summary>
        public Angle Turns
        {
            [MethodImpl(AggressiveInlining)] get => this * TwoPi;
        }

        /// <summary>
        /// The angle represented by this number of degrees.
        /// </summary>
        public Angle Degrees
        {
            [MethodImpl(AggressiveInlining)] get => this * (float)Math.PI / 180f;
        }

        /// <summary>
        /// The angle represented by this number of radians.
        /// </summary>
        public Angle Radians
        {
            [MethodImpl(AggressiveInlining)] get => this; 
        }

        //-------------------------------------------------------------------------------
        // Math Intrinsic Functions
        //-------------------------------------------------------------------------------

        /// <summary>
        /// The absolute value of a single-precision floating-point number.
        /// </summary>
        public Number Abs
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Abs(Value);
        }

        /// <summary>
        /// The angle whose cosine is the specified number.
        /// </summary>
        public Angle Acos
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Acos(Value);
        }

        /// <summary>
        /// The angle whose hyperbolic cosine is the specified number.
        /// </summary>
        public Angle Acosh
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Acosh(Value);
        }

        /// <summary>
        /// The angle whose sine is the specified number.
        /// </summary>
        public Angle Asin
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Asin(Value);
        }

        /// <summary>
        /// The angle whose hyperbolic sine is the specified number.
        /// </summary>
        public Angle Asinh
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Asinh(Value);
        }

        /// <summary>
        /// The angle whose tangent is the specified number.
        /// </summary>
        public Angle Atan
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Atan(Value);
        }

        /// <summary>
        /// The angle whose tangent is the quotient of this number with another.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Angle Atan2(Number x) => MathF.Atan2(Value, x);

        /// <summary>
        /// The angle whose hyperbolic tangent is the specified number.
        /// </summary>
        public Angle Atanh
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Atanh(Value);
        }

        /// <summary>
        /// The largest value that compares less than the value.
        /// </summary>
        public Number BitDecrement
        {
            [MethodImpl(AggressiveInlining)] get => MathF.BitDecrement(Value);
        }

        /// <summary>
        /// The smallest value that compares greater than the value.
        /// </summary>
        public Number BitIncrement
        {
            [MethodImpl(AggressiveInlining)] get => MathF.BitIncrement(Value);
        }

        /// <summary>
        /// The cube root of the number.
        /// </summary>
        public Number Cbrt
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Cbrt(Value);
        }

        /// <summary>
        /// The smallest integral value that is greater than or equal to the specified single-precision floating-point number.
        /// </summary>
        public Number Ceiling
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Ceiling(Value);
        }

        /// <summary>
        /// A value with the magnitude of this number and the sign of another number.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number CopySign(Number y) => MathF.CopySign(Value, y);

        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        public Number Exp
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Exp(Value);
        }

        /// <summary>
        /// The largest integral value less than or equal to this value.
        /// </summary>
        public Number Floor
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Floor(Value);
        }

        /// <summary>
        /// Returns (Value * y) + z, rounded as one ternary operation.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number FusedMultiplyAdd(Number y, Number z) => MathF.FusedMultiplyAdd(Value, y, z);

        /// <summary>
        /// The remainder resulting from the division of by another number.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number IEEERemainder(Number y) => MathF.IEEERemainder(Value, y);

        /// <summary>
        /// The base 2 integer logarithm of the number.
        /// </summary>
        public Number ILogB
        {
            [MethodImpl(AggressiveInlining)] get => MathF.ILogB(Value);
        }

        /// <summary>
        /// The logarithm of the number in the base.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Log(Number newBase) => MathF.Log(Value, newBase);

        /// <summary>
        /// The natural (base e) logarithm of the number.
        /// </summary>
        public Number NaturalLog
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Log(Value);
        }

        /// <summary>
        /// The base 10 logarithm of the number.
        /// </summary>
        public Number Log10
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Log10(Value);
        }

        /// <summary>
        /// The base 2 logarithm of the number.
        /// </summary>
        public Number Log2
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Log2(Value);
        }

        /// <summary>
        /// The larger of two single-precision floating-point numbers.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Max(Number other) => MathF.Max(Value, other);

        /// <summary>
        /// The larger magnitude of two single-precision floating-point numbers.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number MaxMagnitude(Number other) => MathF.MaxMagnitude(Value, other);

        /// <summary>
        /// The smaller of two single-precision floating-point numbers.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Min(Number other) => MathF.Min(Value, other);

        /// <summary>
        /// The smaller magnitude of two single-precision floating-point numbers.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number MinMagnitude(Number other) => MathF.MinMagnitude(Value, other);

        /// <summary>
        /// The number raised to the specified power.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Pow(Number power) => MathF.Pow(Value, power);

        /// <summary>
        /// The reciprocal of the number.
        /// </summary>
        public Number Reciprocal
        {
            [MethodImpl(AggressiveInlining)] get => 1f / Value;
        }

        /// <summary>
        /// An estimate of the reciprocal of the number.
        /// </summary>
        public Number ReciprocalEstimate
        {
            [MethodImpl(AggressiveInlining)] get => MathF.ReciprocalEstimate(Value);
        }

        /// <summary>
        /// An estimate of the reciprocal square root of the number.
        /// </summary>
        public Number ReciprocalSqrtEstimate
        {
            [MethodImpl(AggressiveInlining)] get => MathF.ReciprocalSqrtEstimate(Value);
        }

        /// <summary>
        /// Rounds the value to the number of fractional digits using the specified rounding convention.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Round(Integer digits, MidpointRounding mode) => MathF.Round(Value, digits, mode);

        /// <summary>
        /// Rounds the value to the number of fractional digits, rounding midpoint values to the nearest even number.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Round(Integer digits) => MathF.Round(Value, digits);

        /// <summary>
        /// Rounds the value to an integer using the specified rounding convention.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Round(MidpointRounding mode) => MathF.Round(Value, mode);

        /// <summary>
        /// Rounds the value to the nearest integral value, rounding midpoint values to the nearest even number.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number Round() => MathF.Round(Value);

        /// <summary>
        /// Returns x multiplied by 2 raised to the power of n, computed efficiently.
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public Number ScaleB(Integer n) => MathF.ScaleB(Value, n);

        /// <summary>
        /// An integer that indicates the sign of the number.
        /// </summary>
        public Integer Sign
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Sign(Value);
        }

        /// <summary>
        /// The square root of the number.
        /// </summary>
        public Number Sqrt
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Sqrt(Value);
        }

        /// <summary>
        /// Calculates the integral part of the single-precision floating-point number.
        /// </summary>
        public Number Truncate
        {
            [MethodImpl(AggressiveInlining)] get => MathF.Truncate(Value);
        }
    }
}
