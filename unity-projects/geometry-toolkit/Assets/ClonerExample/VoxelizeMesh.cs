using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class VoxelizeMesh : JobScheduler<INoData, INativeVoxelData>, INativeVoxelData
    {
        public Mesh Mesh;
        public int GridResolution = 10;
        private int _gridResolution;
        private VoxelData<float> _voxels;
        public Bounds Bounds;
        public bool CubesOnly;
        public bool Recompute;

        public unsafe void Update()
        {
            if (Mesh == null)
                return;
            if (_gridResolution != GridResolution)
                Recompute = true;
            if (!Recompute)
                return;
            Recompute = false;
            Bounds = Mesh.bounds;
            if (CubesOnly)
            {
                float3 size = Bounds.size;
                var maxSide = math.cmax(size);
                Bounds = new Bounds(Bounds.center, new Vector3(maxSide, maxSide, maxSide));
            }
            var nTris = Mesh.triangles.Length / 3;

            GridResolution = Mathf.Clamp(GridResolution, 0, 10000);
            _gridResolution = GridResolution;
            _voxels.Update(Bounds, GridResolution);

            using var dataArray = Mesh.AcquireReadOnlyMeshData(Mesh);
            var job = new VoxelizeJob()
            {
                Voxels = _voxels
            };
            var md = dataArray[0];
            using var verts = new NativeArray<float3>(md.vertexCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            job.Vertices = verts;
            md.GetVertices(job.Vertices.Reinterpret<Vector3>());
            using var indices = new NativeArray<int3>(nTris, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            job.Indices = indices;
            md.GetIndices(job.Indices.Reinterpret<int>(sizeof(int3)), 0);
            job.Schedule(nTris, 64).Complete();
        }

        public override JobHandle ScheduleJob(INoData inputData, JobHandle previous)
        {
            return previous;
        }

        public override INativeVoxelData Result => this;
        public ref VoxelData<float> Voxels => ref _voxels;
    }

    [BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast)]
    public struct VoxelizeJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<float3> Vertices;
        [ReadOnly] public NativeArray<int3> Indices;
        [WriteOnly] public VoxelData<float> Voxels;

        public void Execute(int i)
        {
            var i3 = Indices[i];
            var tri = math.float3x3(Vertices[i3.x], Vertices[i3.y], Vertices[i3.z]);
            Voxels.VoxelizeTriangle(tri, 1);
        }
    }
}