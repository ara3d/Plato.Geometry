using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerRandomizeTransform : ClonerJobComponent
    {
        public ulong Seed = 92226;

        public bool SetPosition;
        public Vector3 MinPosition;
        public Vector3 MaxPosition;

        public bool SetRotation = true;
        public Vector3 MinRotation;
        public Vector3 MaxRotation;

        public bool SetScaling = true;
        public Vector3 MinScaling = Vector3.one;
        public Vector3 MaxScaling = Vector3.one;

        public override (CloneData, JobHandle) Schedule(CloneData cloneData, JobHandle h, int batchSize)
        {
            return (cloneData, new JobRandomize()
                {
                    Data = cloneData,
                    Seed = Seed,
                    SetRotation = SetRotation,
                    SetScaling = SetScaling,
                    SetTranslation = SetPosition,
                    MinTranslation = MinPosition,
                    MinRotation = MinRotation,
                    MinScaling = MinScaling,
                    MaxTranslation = MaxPosition,
                    MaxRotation = MaxRotation,
                    MaxScaling = MaxScaling,
                }
                .Schedule(cloneData.Count, batchSize, h));
        }
    }

    [BurstCompile(CompileSynchronously = true)]
    public struct JobRandomize : IJobParallelFor
    {
        public CloneData Data;

        public ulong Seed;

        public bool SetTranslation;
        public float3 MinTranslation;
        public float3 MaxTranslation;

        public bool SetRotation;
        public float3 MinRotation;
        public float3 MaxRotation;

        public bool SetScaling;
        public float3 MinScaling;
        public float3 MaxScaling;

        public void Execute(int i)
        {
            var r = Rng.GetNthFloat3(Seed, (ulong)i);
            if (SetTranslation)
                Data.GpuInstance(i).Pos = math.lerp(MinTranslation, MaxTranslation, Rng.GetNthFloat3(Seed, (ulong)(i * 3)));
            if (SetScaling)
                Data.GpuInstance(i).Scl = math.lerp(MinScaling, MaxScaling, Rng.GetNthFloat3(Seed, (ulong)(i * 3) + 1));
            if (SetRotation)
                Data.GpuInstance(i).Orientation = quaternion.EulerXYZ(
                    math.lerp(MinRotation, MaxRotation, Rng.GetNthFloat3(Seed, (ulong)(i * 3) + 2)));
        }
    }
}