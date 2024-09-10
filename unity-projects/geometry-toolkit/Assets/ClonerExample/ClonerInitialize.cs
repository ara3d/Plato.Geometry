using System;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerInitialize : ClonerJobComponent, ICloneJob
    {
        public CloneData _cloneData;

        public bool RunSimulation;

        public int Rows = 5;
        public int Columns = 5;
        public float Spacing = 1.5f;
        public int Count => Rows * Columns;
        public Color Color = Color.blue;
        public Quaternion Rotation = Quaternion.identity;
        public Vector3 Scale = Vector3.one;
        [Range(0, 1)] public float Metallic = 0.5f;
        [Range(0, 1)] public float Smoothness = 0.5f;

        public ICloneJob Previous => null;
        public JobHandle Handle { get; set; }
        public ref CloneData CloneData => ref _cloneData;

        public JobHandle Schedule(ICloneJob previous)
        { 
            CloneData.Resize(Count);
            return Handle = CreateJob(ref CloneData).Schedule(Count, 256);
        }

        public void OnValidate()
        {
            Rows = Math.Max(1, Rows);
            Columns = Math.Max(1, Columns);
        }
        
        public JobInitializeData CreateJob(ref CloneData cloneData)
        {
            return new JobInitializeData(
                cloneData,
                Time.time,
                Rows,
                Columns,
                Spacing,
                Rotation,
                Scale,
                new float4(Color.r, Color.g, Color.b, Color.a),
                Metallic,
                Smoothness);
        }

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        { 
            CloneData.Resize(Count);
            if (RunSimulation)
            {
                return (CloneData, 
                    new JobUpdate(CloneData, Time.time)
                        .Schedule(Count, batchSize, previousHandle));
            }

            return (CloneData, CreateJob(ref CloneData).Schedule(Count, batchSize, previousHandle));
        }

        private void OnDisable()
        {
            CloneData.Dispose();
        }

    }

    [BurstCompile(CompileSynchronously = true)]
    public struct JobUpdate : IJobParallelFor
    {
        public JobUpdate(CloneData data, float currentTime)
            => (Data, CurrentTime) = (data, currentTime);

        private CloneData Data;
        private readonly float CurrentTime;

        public void Execute(int i)
            => Data.Update(CurrentTime, i);
    }


    [BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast, OptimizeFor = OptimizeFor.Performance, Debug = false, DisableSafetyChecks = true)]
    public struct JobInitializeData : IJobParallelFor
    {
        private CloneData Data;
        
        private readonly float CurrentTime;
        private readonly int Rows;
        private readonly int Columns;
        private readonly float Spacing;
        private readonly quaternion Rotation;
        private readonly float3 Scale;
        private readonly float4 Color;
        private readonly float Metallic;
        private readonly float Smoothness;

        public JobInitializeData(CloneData data, float currentTime,
            int rows, int columns, float spacing, quaternion rotation, float3 scale,
            float4 color, float metallic, float smoothness)
        {
            Data = data;
            CurrentTime = currentTime;
            Rows = rows;
            Columns = columns;
            Spacing = spacing;
            Rotation = rotation;
            Scale = scale;
            Color = color;
            Metallic = metallic;
            Smoothness = smoothness;
        }

        [SkipLocalsInit]
        public void Execute(int i)
        {
            var col = i % Columns;
            var row = (i / Columns) % Rows;

            var position = col * new float3(1, 0, 0) * Spacing
                           + row * new float3(0, 0, 1) * Spacing;
            var rotation = Rotation;
            var scale = Scale;
            var uv = new float2(
                (float)col / (Columns + 1),
                (float)row / (Rows + 1));

            Data.GpuArray[i] = new GpuInstanceData()
            {
                Pos = position,
                Orientation = rotation,
                Scl = scale,
                Color = Color,
                Smoothness = Smoothness,
                Metallic = Metallic,
                Id = (uint)i
            };
            Data.CpuArray[i] = new CpuInstanceData(CurrentTime)
            {
                Uv = uv,
            };
        }
    }
}   