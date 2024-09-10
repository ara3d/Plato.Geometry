using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    public static class MathExtensions
    {
        public static float3 Center(this float3x2 f2)
            => (f2.c0 + f2.c1) / 2f;

        public static float3 Size(this float3x2 f2)
            => f2.c1 - f2.c0;

        public static float3 Center(this float3x3 f3)
            => (f3.c0 + f3.c1 + f3.c2) / 3f;

        public static float4 ToFloat4(this Color c)
            => new float4(c.r, c.g, c.b, c.a);
    }
}