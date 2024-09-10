using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    public struct GpuInstanceData
    {
        public float3 Pos;
        public quaternion Orientation;
        public float3 Scl;
        public float4 Color;
        public float Smoothness;
        public float Metallic;
        public uint Id;
        public static int Size => 17 * 4;

        public static GpuInstanceData FromMatrix(Matrix4x4 mat, int id, Color color, float smoothness = 0.5f, float metallic = 0.5f)
        {
            var scl = mat.lossyScale;
            var pos = mat.GetPosition();
            var rot = mat.rotation;
            return new GpuInstanceData()
            {
                Color = color.ToFloat4(),
                Id = (uint)id,
                Metallic = metallic,
                Orientation = rot,
                Pos = pos,
                Scl = scl,
                Smoothness = smoothness,
            };
        }
    }
}   