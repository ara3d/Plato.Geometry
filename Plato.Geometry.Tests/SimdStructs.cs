using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using Plato.Geometry.Experimental;

namespace Plato.Geometry.Tests
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct SimdVector3
    {
        public readonly f8 X;
        public readonly f8 Y;
        public readonly f8 Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimdVector3(in f8 x, in f8 y, in f8 z) => (X, Y, Z) = (x, y, z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 operator +(in SimdVector3 a, in SimdVector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 operator-(in SimdVector3 a, in SimdVector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 operator *(in SimdVector3 a, in float scalar) => new(a.X * scalar, a.Y * scalar, a.Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 Min(in SimdVector3 a, in SimdVector3 b) => new(f8.Min(a.X, b.X), f8.Min(a.Y, b.Y), f8.Min(a.Z, b.Z));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 Max(in SimdVector3 a, in SimdVector3 b) => new(f8.Max(a.X, b.X), f8.Max(a.Y, b.Y), f8.Max(a.Z, b.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 LengthSquared()  => (X * X) + (Y * Y) + (Z * Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Length() => LengthSquared().Sqrt();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 Cross(in SimdVector3 a, in SimdVector3 b) => new(a.Y * b.Z - a.Z * b.Y, a.Z* b.X - a.X* b.Z, a.X* b.Y - a.Y* b.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimdVector3 Normal()
        {
            var length = Length();
            return new(X / length, Y / length, Z / length);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct SimdTriangle
    {
        public readonly SimdVector3 A;
        public readonly SimdVector3 B;
        public readonly SimdVector3 C;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimdTriangle(in SimdVector3 a, in SimdVector3 b, in SimdVector3 c) => (A, B, C) = (a, b, c);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimdVector3 Normal() => SimdVector3.Cross(B - A, C - A).Normal();
    }
}