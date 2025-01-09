using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SNPlane = System.Numerics.Plane;

namespace Plato
{
    /// <summary>
    /// A lightweight wrapper around <see cref="System.Numerics.Plane"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Plane : IEquatable<Plane>
    {
        // --------------------------------------------------------------------
        // Fields
        // --------------------------------------------------------------------
        
        public readonly Vector3 Normal;
        public readonly float D;

        // --------------------------------------------------------------------
        // Constructors
        // --------------------------------------------------------------------

        /// <summary>
        /// Creates a new <see cref="Plane"/> with the specified normal and distance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(Vector3 normal, float d)
        {
            Normal = normal;
            D = d;
        }

        // --------------------------------------------------------------------
        // Internal: Convert to/from Plane
        // --------------------------------------------------------------------

        /// <summary>
        /// Converts this wrapper to a <see cref="Plane"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SNPlane ToSystem()
            => Unsafe.As<Plane, SNPlane>(ref this);

        /// <summary>
        /// Converts a <see cref="Plane"/> to this wrapper.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Plane FromSystem(SNPlane plane)
            => Unsafe.As<SNPlane, Plane>(ref plane);

        // --------------------------------------------------------------------
        // Implicit conversions
        // --------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SNPlane(Plane plane)
            => plane.ToSystem();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Plane(SNPlane plane)
            => FromSystem(plane);

        // --------------------------------------------------------------------
        // Equality, hashing, and string representation
        // --------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Plane other)
            => ToSystem().Equals(other.ToSystem());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
            => obj is Plane other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => ToSystem().GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ToSystem().ToString();

        /// <summary>
        /// Equality operator.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Plane left, Plane right)
            => left.Equals(right);

        /// <summary>
        /// Inequality operator.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Plane left, Plane right)
            => !left.Equals(right);

        // --------------------------------------------------------------------
        // Static Methods (forwarded to Plane)
        // --------------------------------------------------------------------

        /// <summary>
        /// Creates a <see cref="Plane"/> from three vertices.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Plane CreateFromVertices(Vector3 point1, Vector3 point2, Vector3 point3)
            => SNPlane.CreateFromVertices(point1, point2, point3);

        /// <summary>
        /// Returns the dot product of a plane and a 4D vector.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Dot(Vector4 value)
            => SNPlane.Dot(ToSystem(), value);

        /// <summary>
        /// Returns the dot product of a plane's normal with a 3D coordinate, plus the plane's D value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float DotCoordinate(Vector3 value)
            => SNPlane.DotCoordinate(ToSystem(), value);

        /// <summary>
        /// Returns the dot product of a plane's normal with a 3D normal.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float DotNormal(Vector3 value)
            => SNPlane.DotNormal(ToSystem(), value);

        /// <summary>
        /// Returns a copy of the plane with a normal of length of 1.
        /// </summary>
        public Plane Normalize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => SNPlane.Normalize(ToSystem());
        }

        /// <summary>
        /// Transforms by a rotation (Quaternion).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane Transform(Quaternion rotation)
            => SNPlane.Transform(ToSystem(), rotation);

        /// <summary>
        /// Transforms by a 4x4 matrix.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane Transform(Matrix4x4 matrix)
            => SNPlane.Transform(ToSystem(), matrix);
    }
}
