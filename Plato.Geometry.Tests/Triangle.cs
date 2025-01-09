using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plato.Geometry.Tests
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct Triangle
    {
        public readonly Vector3 A;
        public readonly Vector3 B;
        public readonly Vector3 C;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(Vector3 a, Vector3 b, Vector3 c) => (A, B, C) = (a, b, c);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 Normal() => Vector3.Normalize(Vector3.Cross(B - A, C - A));
    }
}