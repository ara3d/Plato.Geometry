using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerSimulate : ClonerJobComponent
    {
        public bool RunSimulation = false;
        public bool RestartSimulation = false;
        public int Count = 20;
        public float MinSpeed = 0.0f;
        public float MaxSpeed = 1.0f;
        public uint Seed = 58299;
        public CloneData CloneData;
        public Color Color = Color.blue;
        [Range(0, 1)] public float Metallic = 0.5f;
        [Range(0, 1)] public float Smoothness = 0.5f;

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            if (!RunSimulation)
                return (previousData, previousHandle);

            if (CloneData.Count != Count)
                RestartSimulation = true;

            if (RestartSimulation)
            {
                CloneData.Resize(Count);

                var r = (CloneData, new JobInitializeSimulation(
                        CloneData,
                        Metallic,
                        MinSpeed,
                        MaxSpeed,
                        RestartSimulation,
                        Smoothness,
                        new float3(1, 1, 1),
                        new(Color.r, Color.g, Color.b, Color.a),
                        Time.time,
                        Seed
                    )
                    .Schedule(Count, 16, previousHandle));

                RestartSimulation = false;
                return r;
            }

            return (CloneData, new JobUpdate(CloneData, Time.time)
                .Schedule(Count, batchSize, previousHandle));

        }
    }

    [BurstCompile(CompileSynchronously = true)]
    public struct JobInitializeSimulation : IJobParallelFor
    {
        private CloneData Data;
        private readonly float3 Scale;
        private readonly ulong Seed;
        private readonly float4 Color;
        private readonly float Metallic;
        private readonly float Smoothness;
        private readonly float MinSpeed;
        private readonly float MaxSpeed;
        private readonly bool Restart;
        private readonly float CurrentTime;

        public JobInitializeSimulation(
            CloneData data,
            float metallic,
            float minSpeed,
            float maxSpeed,
            bool restart,
            float smoothness,
            float3 scale,
            float4 color,
            float time, 
            ulong seed)
        {
            Data = data;
            Metallic = metallic;
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            Restart = restart;
            Smoothness = smoothness;
            Scale = scale;
            Color = color;
            Seed = seed;
            CurrentTime = time;
        }

        public void Execute(int i)
        {
            if (Restart)
            {
                Data.GpuArray[i] = new GpuInstanceData()
                {
                    Orientation = Rng.GetNthQuaternion(Seed, (ulong)i, -math.PI, math.PI),
                    Scl = Scale,
                    Color = Color,
                    Smoothness = Smoothness,
                    Metallic = Metallic,
                    Id = (uint)i
                };
                Data.CpuArray[i] = new CpuInstanceData(CurrentTime)
                {
                    Propulsion = Rng.GetNthFloat(Seed + 1, (ulong)i, MinSpeed, MaxSpeed),
                };
            }
        }
    }
}