using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerInitializeFromMesh : ClonerJobComponent
    {
        public CloneData CloneData;
        public Mesh Mesh;
        public Color Color = Color.blue;
        public bool PointsOrEdges;
        [Range(0, 1)] public float Metallic = 0.5f;
        [Range(0, 1)] public float Smoothness = 0.5f;

        public NativeArray<Vector3> GetTrianglePoints()
        {
            var indices = Mesh.GetIndices(0);
            var vertices = Mesh.vertices;
            var n = indices.Length;
            var r = new NativeArray<Vector3>(n, Allocator.TempJob);
            for (var i = 0; i < n; i++)
            {
                r[i] = vertices[indices[i]];
            }

            return r;
        }

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            var count = Mesh != null
                ? PointsOrEdges
                    ? Mesh.vertexCount
                    : (int)Mesh.GetIndexCount(0)
                : 0;

            CloneData.Resize(count);
            
            var points = Mesh != null  
                ? PointsOrEdges 
                    ? new NativeArray<Vector3>(Mesh.vertices, Allocator.TempJob)
                    : GetTrianglePoints()
                : new NativeArray<Vector3>(0, Allocator.TempJob);
            
            return PointsOrEdges ? 
                (CloneData, new JobInitializeFromPoints
                {
                    CloneData = CloneData,
                    Points = points.Reinterpret<float3>(),
                    Color = new float4(Color.r, Color.g, Color.b, Color.a),
                    Metallic = Metallic,
                    Smoothness = Smoothness,
                }
                .Schedule(count, batchSize, previousHandle))
                :
                (CloneData, new JobInitializeFromEdges()
                    {
                        CloneData = CloneData,
                        Points = points.Reinterpret<float3>(),
                        Color = new float4(Color.r, Color.g, Color.b, Color.a),
                        Metallic = Metallic,
                        Smoothness = Smoothness,
                    }
                    .Schedule(count, batchSize, previousHandle));
        }

        void OnDisable()
        {
            CloneData.Dispose();
        }
    }

    [BurstCompile(CompileSynchronously = true)]
    public struct JobInitializeFromPoints : IJobParallelFor
    {
        public CloneData CloneData;
        public float4 Color;
        public float Metallic;
        public float Smoothness;
        public float3 Scale;
        [ReadOnly] public NativeArray<float3> Points;

        public JobInitializeFromPoints(CloneData cloneData, NativeArray<float3> points, float4 color, float metallic, float smoothness, float3 scale)
        {
            CloneData = cloneData;
            Color = color;
            Metallic = metallic;
            Smoothness = smoothness;
            Points = points;
            Scale = scale;
        }

        public void Execute(int i)
        {
            var rotation = quaternion.identity;

            CloneData.GpuArray[i] = new GpuInstanceData()
            {
                Pos = Points[i],
                Orientation = rotation,
                Scl = Scale,
                Color = Color,
                Smoothness = Smoothness,
                Metallic = Metallic,
                Id = (uint)i
            };
            CloneData.CpuArray[i] = new CpuInstanceData();
        }
    }

    [BurstCompile(CompileSynchronously = true)]
    public struct JobInitializeFromEdges : IJobParallelFor
    {
        public CloneData CloneData;
        public float4 Color;
        public float Metallic;
        public float Smoothness;

        public static readonly float3 up = new float3(0, 1, 0);

        [ReadOnly] public NativeArray<float3> Points;

        public void Execute(int i)
        {
            var a = Points[i];

            var b = Points[i + ((i % 3 == 2) ? -2 : 1)];
            var v = b - a;

            var rotation = quaternion.LookRotationSafe(v, up);
            var scale = new float3(0.1F, math.length(v), 0.1F);
            
            CloneData.GpuArray[i] = new GpuInstanceData()
            {
                Pos = a,
                Orientation = rotation,
                Scl = scale,
                Color = Color,
                Smoothness = Smoothness,
                Metallic = Metallic,
                Id = (uint)i
            };
            CloneData.CpuArray[i] = new CpuInstanceData();
        }
    }
}