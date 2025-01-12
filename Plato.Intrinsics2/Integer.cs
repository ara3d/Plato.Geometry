using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    /// <summary>
    /// A simple wrapper around the built-in <c>int</c> type,
    /// forwarding all arithmetic and common methods to <c>int</c>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Integer : IEquatable<Integer>, IComparable<Integer>, IComparable
    {
        // -------------------------------------------------------------------------------
        // Field (the wrapped int)
        // -------------------------------------------------------------------------------
        public readonly int Value;

        // -------------------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------------------
        [MethodImpl(AggressiveInlining)]
        public Integer(int value) => Value = value;

        // -------------------------------------------------------------------------------
        // Convert to/from int
        // -------------------------------------------------------------------------------
        [MethodImpl(AggressiveInlining)]
        public int ToInt() => Value;

        [MethodImpl(AggressiveInlining)]
        public static Integer FromInt(int i) => new(i);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Integer(int i) => FromInt(i);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator int(Integer n) => n.ToInt();

        // -------------------------------------------------------------------------------
        // Common Constants
        // -------------------------------------------------------------------------------
        
        public static readonly Integer Zero = 0;
        public static readonly Integer One = 1;
        public static readonly Integer NegativeOne = -1;
        public static readonly Integer MinValue = int.MinValue;
        public static readonly Integer MaxValue = int.MaxValue;

        // -------------------------------------------------------------------------------
        // Operators (forward to int)
        // -------------------------------------------------------------------------------
        
        [MethodImpl(AggressiveInlining)]
        public static Integer operator +(Integer a, Integer b)
            => a.Value + b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Integer operator -(Integer a, Integer b)
            => a.Value - b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Integer operator *(Integer a, Integer b)
            => a.Value * b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Integer operator /(Integer a, Integer b)
            => a.Value / b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Integer operator %(Integer a, Integer b)
            => a.Value % b.Value;

        [MethodImpl(AggressiveInlining)]
        public static Integer operator -(Integer n)
            => -n.Value;

        [MethodImpl(AggressiveInlining)]
        public static bool operator ==(Integer a, Integer b)
            => a.Value == b.Value;

        [MethodImpl(AggressiveInlining)]
        public static bool operator !=(Integer a, Integer b)
            => a.Value != b.Value;

        [MethodImpl(AggressiveInlining)]
        public static bool operator <(Integer a, Integer b)
            => a.Value < b.Value;

        [MethodImpl(AggressiveInlining)]
        public static bool operator <=(Integer a, Integer b)
            => a.Value <= b.Value;

        [MethodImpl(AggressiveInlining)]
        public static bool operator >(Integer a, Integer b)
            => a.Value > b.Value;

        [MethodImpl(AggressiveInlining)]
        public static bool operator >=(Integer a, Integer b)
            => a.Value >= b.Value;

        // -------------------------------------------------------------------------------
        // Equality, hashing, and ToString
        // -------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public bool Equals(Integer other)
            => Value.Equals(other.Value);

        [MethodImpl(AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Integer n && Equals(n);

        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode()
            => Value.GetHashCode();

        [MethodImpl(AggressiveInlining)]
        public override string ToString()
            => Value.ToString();

        // -------------------------------------------------------------------------------
        // IComparable / IComparable<Integer> Implementation
        // -------------------------------------------------------------------------------

        [MethodImpl(AggressiveInlining)]
        public int CompareTo(Integer other)
            => Value.CompareTo(other.Value);

        [MethodImpl(AggressiveInlining)]
        public int CompareTo(object obj)
        {
            if (obj is Integer n)
                return CompareTo(n);
            throw new ArgumentException("Object is not an Integer");
        }

        // -------------------------------------------------------------------------------
        // Integer-specific Helper Properties/Methods
        // -------------------------------------------------------------------------------

        /// <summary>
        /// The absolute value of the integer.
        /// </summary>
        public Integer Abs
        {
            [MethodImpl(AggressiveInlining)]
            get => Math.Abs(Value);
        }

        /// <summary>
        /// An integer that indicates the sign of this integer (-1, 0, or 1).
        /// </summary>
        public Integer Sign
        {
            [MethodImpl(AggressiveInlining)]
            get => Math.Sign(Value);
        }

        /// <summary>
        /// Returns this integer incremented by one.
        /// </summary>
        public Integer Increment
        {
            [MethodImpl(AggressiveInlining)]
            get => Value + 1;
        }

        /// <summary>
        /// Returns this integer decremented by one.
        /// </summary>
        public Integer Decrement
        {
            [MethodImpl(AggressiveInlining)]
            get => Value - 1;
        }
    }
}
