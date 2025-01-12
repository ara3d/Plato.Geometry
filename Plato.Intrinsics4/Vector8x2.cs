using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plato
{
    /// <summary>
    /// Provides a similar interface to a Vector3, but with 8 floats per lane.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector8x2 : IEquatable<Vector8x2>
    {
        // Fields
        public readonly Vector8 X;
        public readonly Vector8 Y;

        // Constructor
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8x2(Vector8 x, Vector8 y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Adds two Vector8x3s component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x2 operator +(Vector8x2 left, Vector8x2 right)
            => new(left.X + right.X, left.Y + right.Y);

        /// <summary>Subtracts two Vector8x3s component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x2 operator -(Vector8x2 left, Vector8x2 right)
            => new(left.X - right.X, left.Y - right.Y);

        /// <summary>Negates each component of the Vector8x2.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x2 operator -(Vector8x2 value)
            => new(-value.X, -value.Y);

        /// <summary>Multiplies two Vector8x3s component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x2 operator *(Vector8x2 left, Vector8x2 right)
            => new(left.X * right.X, left.Y * right.Y);

        /// <summary>Multiplies a Vector8x2 by a scalar (Vector8), component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x2 operator *(Vector8x2 left, Vector8 scalar)
            => new(left.X * scalar, left.Y * scalar);

        /// <summary>Multiplies a scalar (Vector8) by a Vector8x2, component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x2 operator *(Vector8 scalar, Vector8x2 right)
            => new(scalar * right.X, scalar * right.Y);

        /// <summary>Divides two Vector8x3s component-wise (left / right).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x2 operator /(Vector8x2 left, Vector8x2 right)
            => new(left.X / right.X, left.Y / right.Y);

        /// <summary>Divides a Vector8x2 by a scalar (Vector8), component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x2 operator /(Vector8x2 left, Vector8 scalar)
            => new(left.X / scalar, left.Y / scalar);

        /// <summary>Checks if two Vector8x2 are exactly equal (component-wise).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector8x2 left, Vector8x2 right)
            => left.Equals(right);

        /// <summary>Checks if two Vector8x2 are not exactly equal (component-wise).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector8x2 left, Vector8x2 right)
            => !left.Equals(right);

        /// <summary>
        /// Returns the dot product of two Vector8x3s, computed per lane:
        /// Dot( (x1,y1,z1), (x2,y2,z2) ) = (x1*x2 + y1*y2 + z1*z2 ).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8 Dot(Vector8x2 right)
            => (X * right.X) + (Y * right.Y);
        
        /// <summary>
        /// Returns the squared length (magnitude) of the Vector8x2 per lane.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8 LengthSquared()
            => this.Dot(this);

        /// <summary>
        /// Returns the length (magnitude) of the Vector8x2 per lane.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8 Length()
            => LengthSquared().Sqrt;

        /// <summary>
        /// Returns a normalized Vector8x2 (each lane normalized independently).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8x2 Normalize()
            => this / this.Length();

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector8x2 other)
            => X.Equals(other.X) && Y.Equals(other.Y);

        /// <inheritdoc />
        public override bool Equals(object? obj)
            => obj is Vector8x2 other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            // You might want a more robust or faster hash depending on use case.
            // HashCode.Combine is .NET Standard 2.1+; if unavailable, implement your own.
            return HashCode.Combine(X, Y);
        }

        /// <inheritdoc />
        public override string ToString()
            => $"Vector8x2({X}, {Y})";
    }
}