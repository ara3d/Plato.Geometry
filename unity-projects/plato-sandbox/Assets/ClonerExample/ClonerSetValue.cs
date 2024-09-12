using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerSetValue : ClonerJobComponent
    {
        public bool UseSelection = false;
        [Range(0, 1)] public float Strength = 1.0f;

        public bool SetPosition;
        public Vector3 Position;

        public bool SetRotation = true;
        public Quaternion Rotation = Quaternion.identity;

        public bool SetScaling = true;
        public Vector3 Scaling = Vector3.one;

        public bool SetColor = true;
        public Color Color;

        public bool SetMetallic = true;
        [Range(0, 1)] public float Metallic = 0.5f;

        public bool SetSmoothness = true;
        [Range(0, 1)] public float Smoothness  = 0.5f;

        public override (CloneData, JobHandle) Schedule(CloneData cloneData, JobHandle h, int batchSize)
        {
            return (cloneData, new JobSetValue()
                {
                    Data = cloneData,
                    SetRotation = SetRotation,
                    SetScaling = SetScaling,
                    SetTranslation = SetPosition,
                    UseSelection = UseSelection,
                    SetColor = SetColor,
                    Strength = Strength,
                    Translation = Position,
                    Rotation = Rotation,
                    Scaling = Scaling,
                    SetMetallic = SetMetallic,
                    Metallic = Metallic,
                    SetSmoothness = SetSmoothness,
                    Smoothness = Smoothness,
                    Color = new float4(Color.r, Color.g, Color.b, Color.a),
                }
                .Schedule(cloneData.Count, batchSize, h));
        }
    }

    [BurstCompile(CompileSynchronously = true)]
    public struct JobSetValue : IJobParallelFor
    {
        public CloneData Data;

        public bool UseSelection;
        public float Strength;

        public bool SetTranslation;
        public float3 Translation;

        public bool SetRotation;
        public quaternion Rotation;

        public bool SetScaling;
        public float3 Scaling;

        public bool SetColor;
        public float4 Color;

        public bool SetSmoothness;
        public float Smoothness;

        public bool SetMetallic;
        public float Metallic;

        public void Execute(int i)
        {
            var sel = UseSelection
                ? math.lerp(0, Data.CpuInstance(i).Selection, Strength)
                : Strength;
            if (SetColor)
                Data.GpuInstance(i).Color = math.lerp(Data.GpuInstance(i).Color, Color, sel);
            if (SetTranslation)
                Data.GpuInstance(i).Pos = math.lerp(Data.GpuInstance(i).Pos, Translation, sel);
            if (SetScaling)
                Data.GpuInstance(i).Scl = math.lerp(Data.GpuInstance(i).Scl, Scaling, sel);
            if (SetRotation)
                Data.GpuInstance(i).Orientation = math.slerp(Data.GpuInstance(i).Orientation, Rotation, sel);
            if (SetSmoothness)
                Data.GpuInstance(i).Smoothness = math.lerp(Data.GpuInstance(i).Smoothness, Smoothness, sel);
            if (SetMetallic)
                Data.GpuInstance(i).Metallic = math.lerp(Data.GpuInstance(i).Metallic, Metallic, sel);
        }

        /*
         * double gauss(double x, double a, double b, double c)
        {
            var v1 = (x - b);
            var v2 = (v1 * v1) / (2 * (c*c));
            var v3 = a * Math.Exp(-v2);
            return v3;
        }
         */
    }
}