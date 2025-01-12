using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plato
{
    /// <summary>
    /// Provides a similar interface to a Vector3, but with 8 floats per lane.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector8x3 : IEquatable<Vector8x3>
    {
        // Fields
        public readonly Vector8 X;
        public readonly Vector8 Y;
        public readonly Vector8 Z;

        // Constructor
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8x3(Vector8 x, Vector8 y, Vector8 z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8x3(float x)
            : this(new(x), new(x), new(x))
        {
        }

        //==
        // Static properties 
        //==

        public static readonly Vector8x3 Zero
            = new(Vector8.Zero, Vector8.Zero, Vector8.Zero);

        public static readonly Vector8x3 One
            = new(Vector8.One, Vector8.One, Vector8.One);

        //==
        // Operators
        //==

        /// <summary>Adds two Vector8x3s component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x3 operator +(Vector8x3 left, Vector8x3 right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        /// <summary>Subtracts two Vector8x3s component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x3 operator -(Vector8x3 left, Vector8x3 right)
            => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        /// <summary>Negates each component of the Vector8x3.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x3 operator -(Vector8x3 value)
            => new(-value.X, -value.Y, -value.Z);

        /// <summary>Multiplies two Vector8x3s component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x3 operator *(Vector8x3 left, Vector8x3 right)
            => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        /// <summary>Multiplies a Vector8x3 by a scalar (Vector8), component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x3 operator *(Vector8x3 left, Vector8 scalar)
            => new(left.X * scalar, left.Y * scalar, left.Z * scalar);

        /// <summary>Multiplies a scalar (Vector8) by a Vector8x3, component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x3 operator *(Vector8 scalar, Vector8x3 right)
            => new(scalar * right.X, scalar * right.Y, scalar * right.Z);

        /// <summary>Divides two Vector8x3s component-wise (left / right).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x3 operator /(Vector8x3 left, Vector8x3 right)
            => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        /// <summary>Divides a Vector8x3 by a scalar (Vector8), component-wise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector8x3 operator /(Vector8x3 left, Vector8 scalar)
            => new(left.X / scalar, left.Y / scalar, left.Z / scalar);

        /// <summary>Checks if two Vector8x3 are exactly equal (component-wise).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector8x3 left, Vector8x3 right)
            => left.Equals(right);

        /// <summary>Checks if two Vector8x3 are not exactly equal (component-wise).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector8x3 left, Vector8x3 right)
            => !left.Equals(right);

        /// <summary>
        /// Returns the dot product of two Vector8x3s, computed per lane:
        /// Dot( (x1,y1,z1), (x2,y2,z2) ) = (x1*x2 + y1*y2 + z1*z2 ).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8 Dot(Vector8x3 right)
            => (X * right.X) + (Y * right.Y) + (Z * right.Z);

        /// <summary>
        /// Returns the cross product of two Vector8x3s, computed per lane:
        /// Cross( (x1,y1,z1), (x2,y2,z2) ) = (y1*z2 - z1*y2, z1*x2 - x1*z2, x1*y2 - y1*x2).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8x3 Cross(Vector8x3 right)
            => new((Y * right.Z) - (Z * right.Y), (Z * right.X) - (X * right.Z), (X * right.Y) - (Y * right.X));

        /// <summary>
        /// Returns the squared length (magnitude) of the Vector8x3 per lane.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8 LengthSquared()
            => this.Dot(this);

        /// <summary>
        /// Returns the length (magnitude) of the Vector8x3 per lane.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8 Length()
            => LengthSquared().Sqrt;

        /// <summary>
        /// Returns a normalized Vector8x3 (each lane normalized independently).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8x3 Normalize()
            => this / this.Length();

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector8x3 other)
            => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

        /// <inheritdoc />
        public override bool Equals(object? obj)
            => obj is Vector8x3 other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            // You might want a more robust or faster hash depending on use case.
            // HashCode.Combine is .NET Standard 2.1+; if unavailable, implement your own.
            return HashCode.Combine(X, Y, Z);
        }

        /// <inheritdoc />
        public override string ToString()
            => $"Vector8x3({X}, {Y}, {Z})";
    }
}