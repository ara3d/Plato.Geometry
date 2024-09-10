using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class UtilRandomTriangle : JobScheduler<INoData, INativeVoxelData>, INativeVoxelData
    {
        public Bounds Bounds = new Bounds(Vector3.zero, Vector3.one * 10f);
        public bool DrawMesh = true;
        public bool DrawVoxels = true;

        public ulong Seed = 1;
        private ulong _cachedSeed = 0;

        public int GridResolution = 10;

        public float3x3 Triangle { get; private set; }
        private VoxelData<float> _voxels;
        public Mesh Mesh;
        public Material MeshMaterial;

        public void Update()
        {
            if (Mesh == null || Seed != _cachedSeed || _voxels.Update(Bounds, GridResolution))
            {
                _cachedSeed = Seed;
                Triangle = Rng.GetNthFloat3x3(Seed, 0, Bounds.min, Bounds.max);
                Mesh = new Mesh
                {
                    vertices = new Vector3[] { Triangle.c0, Triangle.c1, Triangle.c2 },
                    triangles = new[] { 0, 1, 2, 2, 1, 0 }
                };
                Mesh.RecalculateBounds();
                Mesh.RecalculateNormals();
                _voxels.Clear();
                if (DrawVoxels)
                    _voxels.VoxelizeTriangle(Triangle, 1);
            }

            if (DrawMesh && MeshMaterial != null)
            {
                Graphics.DrawMesh(Mesh, Matrix4x4.identity, MeshMaterial, 0);
            }
        }

        public override JobHandle ScheduleJob(INoData inputData, JobHandle previous)
        {
            return previous;
        }

        public override INativeVoxelData Result => this;
        public ref VoxelData<float> Voxels => ref _voxels;
    }
}