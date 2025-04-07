using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Plato.Geometry.Tests
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct Triangle
    {
        public readonly global::Plato.Vector3 A;
        public readonly global::Plato.Vector3 B;
        public readonly global::Plato.Vector3 C;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(global::Plato.Vector3 a, global::Plato.Vector3 b, global::Plato.Vector3 c) => (A, B, C) = (a, b, c);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public global::Plato.Vector3 Normal() => global::Plato.Vector3.Normalize(global::Plato.Vector3.Cross(B - A, C - A));
    }
}