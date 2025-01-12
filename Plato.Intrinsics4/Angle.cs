using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    /// <summary>
    /// A value type that represents angles internally as radians.
    /// Separating angles from floats, makes working with them easier, and
    /// less prone to unit-based errors.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Angle : IEquatable<Angle>, IComparable<Angle>, IComparable
    {
        // -------------------------------------------------------------------------------
        // Field (the wrapped float)
        // -------------------------------------------------------------------------------

        public readonly float Value;

        // -------------------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public Angle(float value) 
            => Value = value;

        // -------------------------------------------------------------------------------
        // Converting the angle to a numerical value using different units 
        // -------------------------------------------------------------------------------

        public float Radians
        {
            [MethodImpl(AggressiveInlining)] get => Value;
        }

        public float Degrees
        {
            [MethodImpl(AggressiveInlining)] get => Value * 180f / (float)Math.PI;
        }

        public float Turns
        {
            [MethodImpl(AggressiveInlining)] get => HalfTurns / 2f;
        }

        public float HalfTurns
        {
            [MethodImpl(AggressiveInlining)] get => Value / (float)Math.PI;
        }

        // -------------------------------------------------------------------------------
        // Convert to/from float
        // -------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Angle(float f) => new(f);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator float(Angle n) => n.Value;
        
        // -------------------------------------------------------------------------------
        // Common Constants
        // -------------------------------------------------------------------------------

        public static readonly Angle FullTurn = MathF.PI * 2;
        public static readonly Angle HalfTurn = MathF.PI;
        public static readonly Angle QuarterTurn = MathF.PI / 4;
        public static readonly Angle Degree = MathF.PI / 180;
        public static readonly Angle Zero = 0;

        // -------------------------------------------------------------------------------
        // Operators (forward to float)
        // -------------------------------------------------------------------------------
        
        [MethodImpl(AggressiveInlining)]
        public static Angle operator +(Angle a, Angle b)
            => new(a.Value + b.Value);

        [MethodImpl(AggressiveInlining)]
        public static Angle operator -(Angle a, Angle b)
            => new(a.Value - b.Value);

        [Obsolete("This method is illegal and should not be used.", true)]
        public static Angle operator *(Angle a, Angle b)
            => throw new Exception("Multiplying two angles is not well-defined");

        [Obsolete("This method is illegal and should not be used.", true)]
        public static Angle operator /(Angle a, Angle b)
            => throw new Exception("Dividing two angles is not well-defined");

        [MethodImpl(AggressiveInlining)]
        public static Angle operator *(Angle a, float x)
            => new(a.Value * x);

        [MethodImpl(AggressiveInlining)]
        public static Angle operator *(float x, Angle a)
            => x * a.Value;

        [MethodImpl(AggressiveInlining)]
        public static Angle operator /(Angle a, float x)
            => a.Value / x;

        [MethodImpl(AggressiveInlining)]
        public static Angle operator -(Angle n)
            => -n.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator ==(Angle a, Angle b)
            => a.Value == b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator !=(Angle a, Angle b)
            => a.Value != b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator <(Angle a, Angle b)
            => a.Value < b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator <=(Angle a, Angle b)
            => a.Value <= b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator >(Angle a, Angle b)
            => a.Value > b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Boolean operator >=(Angle a, Angle b)
            => a.Value >= b.Value;

        // -------------------------------------------------------------------------------
        // Equality, hashing, and ToString
        // -------------------------------------------------------------------------------
        
        [MethodImpl(AggressiveInlining)]
        public bool Equals(Angle other)
            => Value.Equals(other.Value);

        [MethodImpl(AggressiveInlining)]
        public override bool Equals(object? obj)
            => obj is Angle n && Equals(n);

        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode()
            => Value.GetHashCode();

        [MethodImpl(AggressiveInlining)]
        public override string ToString()
            => Value.ToString();

        // -------------------------------------------------------------------------------
        // IComparable / IComparable<Angle> Implementation
        // -------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public int CompareTo(Angle other)
            => Value.CompareTo(other.Value);

        [MethodImpl(AggressiveInlining)]
        public int CompareTo(object? obj)
        {
            if (obj is Angle n)
                return CompareTo(n);
            throw new ArgumentException("Object is not a Angle");
        }

        // -------------------------------------------------------------------------------
        // Trigonometric Functions
        // https://en.wikipedia.org/wiki/Trigonometric_functions
        // -------------------------------------------------------------------------------

        /// <summary>
        /// Cosine 
        /// </summary>
        public float Cos { [MethodImpl(AggressiveInlining)] get => MathF.Cos(Value); }

        /// <summary>
        /// Hyperbolic cosine.
        /// </summary>
        public float Cosh { [MethodImpl(AggressiveInlining)] get => MathF.Cosh(Value); }

        /// <summary>
        /// Sine
        /// </summary>
        public float Sin { [MethodImpl(AggressiveInlining)] get => MathF.Sin(Value); }

        /// <summary>
        /// Sine and cosine.
        /// </summary>
        public (float Sin, float Cos) SinCos { [MethodImpl(AggressiveInlining)] get => MathF.SinCos(Value); }

        /// <summary>
        /// Hyperbolic sine
        /// </summary>
        public float Sinh { [MethodImpl(AggressiveInlining)] get => MathF.Sinh(Value); }

        /// <summary>
        /// The tangent.
        /// </summary>
        public float Tan { [MethodImpl(AggressiveInlining)] get => MathF.Tan(Value); }

        /// <summary>
        /// The hyperbolic tangent. 
        /// </summary>
        public float Tanh { [MethodImpl(AggressiveInlining)] get => MathF.Tanh(Value); }
    }
}
