    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using static System.Runtime.CompilerServices.MethodImplOptions;

    namespace Plato
    {
        /// <summary>
        /// A simple wrapper around the built-in <c>Boolean</c> type,
        /// forwarding logical operations and common methods to <c>Boolean</c>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public partial struct Boolean : IEquatable<Boolean>, IComparable<Boolean>, IComparable
        {
            // -------------------------------------------------------------------------------
            // Field (the wrapped Boolean)
            // -------------------------------------------------------------------------------
            public readonly bool Value;

            // -------------------------------------------------------------------------------
            // Constructors
            // -------------------------------------------------------------------------------
            [MethodImpl(AggressiveInlining)]
            public Boolean(bool value) => Value = value;

            // -------------------------------------------------------------------------------
            // Convert to/from Boolean
            // -------------------------------------------------------------------------------
            [MethodImpl(AggressiveInlining)]
            public bool ToSystem() => Value;

            [MethodImpl(AggressiveInlining)]
            public static Boolean FromSystem(bool b) => new(b);

            [MethodImpl(AggressiveInlining)]
            public static implicit operator Boolean(bool b) => FromSystem(b);

            [MethodImpl(AggressiveInlining)]
            public static implicit operator bool(Boolean b) => b.ToSystem();

            [MethodImpl(AggressiveInlining)]
            public static bool operator true(Boolean b) => b.Value;

            [MethodImpl(AggressiveInlining)]
            public static bool operator false(Boolean b) => !b.Value;

            // -------------------------------------------------------------------------------
            // Common Constants
            // -------------------------------------------------------------------------------

            public static readonly bool True = true;
            public static readonly bool False = false;

            // -------------------------------------------------------------------------------
            // Operators (logical, equality, etc.)
            // -------------------------------------------------------------------------------

            /// <summary>
            /// Logical AND operator (bitwise AND for booleans).
            /// </summary>
            [MethodImpl(AggressiveInlining)]
            public static Boolean operator &(Boolean a, Boolean b)
                => a.Value & b.Value;

            /// <summary>
            /// Logical OR operator (bitwise OR for booleans).
            /// </summary>
            [MethodImpl(AggressiveInlining)]
            public static Boolean operator |(Boolean a, Boolean b)
                => a.Value | b.Value;

            /// <summary>
            /// Logical XOR operator (bitwise exclusive OR for booleans).
            /// </summary>
            [MethodImpl(AggressiveInlining)]
            public static Boolean operator ^(Boolean a, Boolean b)
                => a.Value ^ b.Value;

            /// <summary>
            /// Logical NOT operator (negates the value).
            /// </summary>
            [MethodImpl(AggressiveInlining)]
            public static Boolean operator !(Boolean b)
                => !b.Value;

            /// <summary>
            /// Equality operator.
            /// </summary>
            [MethodImpl(AggressiveInlining)]
            public static Boolean operator ==(Boolean a, Boolean b)
                => a.Value == b.Value;

            /// <summary>
            /// Inequality operator.
            /// </summary>
            [MethodImpl(AggressiveInlining)]
            public static Boolean operator !=(Boolean a, Boolean b)
                => a.Value != b.Value;

            // -------------------------------------------------------------------------------
            // Equality, hashing, and ToString
            // -------------------------------------------------------------------------------

            [MethodImpl(AggressiveInlining)]
            public bool Equals(Boolean other)
                => Value.Equals(other.Value);

            [MethodImpl(AggressiveInlining)]
            public override bool Equals(object obj)
                => obj is Boolean b && Equals(b);

            [MethodImpl(AggressiveInlining)]
            public override int GetHashCode()
                => Value.GetHashCode();

            [MethodImpl(AggressiveInlining)]
            public override string ToString()
                => Value.ToString();

            // -------------------------------------------------------------------------------
            // IComparable / IComparable<Boolean> Implementation
            // -------------------------------------------------------------------------------

            [MethodImpl(AggressiveInlining)]
            public int CompareTo(Boolean other)
            {
                if (Value == other.Value) return 0;
                return Value ? 1 : -1;
            }

            [MethodImpl(AggressiveInlining)]
            public int CompareTo(object obj)
            {
                if (obj is Boolean b)
                    return CompareTo(b);
                throw new ArgumentException("Object is not a Boolean");
            }

            // -------------------------------------------------------------------------------
            // Boolean-specific Helpers
            // -------------------------------------------------------------------------------

            /// <summary>
            /// Whether the wrapped value is <c>true</c>.
            /// </summary>
            public Boolean IsTrue
            {
                [MethodImpl(AggressiveInlining)] get => Value;
            }

            /// <summary>
            /// Whether the wrapped value is <c>false</c>.
            /// </summary>
            public Boolean IsFalse
            {
                [MethodImpl(AggressiveInlining)] get => !Value;
            }

            /// <summary>
            /// Inverts the wrapped <c>Boolean</c>.
            /// </summary>
            public Boolean Toggle
            {
                [MethodImpl(AggressiveInlining)] get => !Value;
            }
        }
    }