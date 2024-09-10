using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class VoxelizeNoise : JobScheduler<INoData, INativeVoxelData>, INativeVoxelData
    {
        public int GridResolution = 20;
        private int _gridResolution;
        private VoxelData<float> _voxels;
        public bool Recompute = false;
        public Bounds Bounds = new Bounds(Vector3.zero, Vector3.one * 5);
        public NoiseType NoiseType = NoiseType.SimplexNoise;
        public float Frequency = 1.0f;
        public float Amplitude = 1.0f;
        public Vector3 Offset = Vector3.zero;
        public NoiseJob Job;

        public unsafe void Update()
        {
            if (_gridResolution != GridResolution 
                || !Job.Amplitude.Equals(Amplitude) 
                || !Job.Frequency.Equals(Frequency)
                || Job.NoiseType != NoiseType 
                || !Job.Offset.Equals(Offset))
                Recompute = true;
            if (!Recompute)
                return;
            Recompute = false;
            GridResolution = Mathf.Clamp(GridResolution, 0, 10000);
            _gridResolution = GridResolution;
            _voxels.Update(Bounds, GridResolution);
            Job = new NoiseJob()
            {
                Amplitude = Amplitude,
                Frequency = Frequency,
                NoiseType = NoiseType,
                Offset = Offset,
                Voxels = _voxels,
            };
            Job.Schedule(Voxels.VoxelCount, 64).Complete();
        }

        public override JobHandle ScheduleJob(INoData inputData, JobHandle previous)
        {
            return previous;
        }

        public override INativeVoxelData Result => this;
        public ref VoxelData<float> Voxels => ref _voxels;
    }

    public enum NoiseType
    {
        PerlinNoise,
        SimplexNoise,
        WorleyNoise,
        WorleyNoiseF2,
    }

    // https://medium.com/@5argon/various-noise-functions-76327e056450

    [BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast)]
    public struct NoiseJob : IJobParallelFor
    {
        [ReadOnly] public NoiseType NoiseType;
        [WriteOnly] public VoxelData<float> Voxels;
        public float3 Frequency;
        public float Amplitude;
        public float3 Offset;

        public void Execute(int i)
        {
            var i3 = Voxels.ToIndex3(i);
            var f3 = Voxels.GetRelPos(i3);
            f3 = f3 * Frequency + Offset;
            switch (NoiseType)
            {
                case NoiseType.PerlinNoise:
                    Voxels.SetValue(i3, noise.cnoise(f3) * Amplitude);
                    break;
                case NoiseType.SimplexNoise:
                    Voxels.SetValue(i3, noise.snoise(f3) * Amplitude);
                    break;
                case NoiseType.WorleyNoise:
                    Voxels.SetValue(i3, noise.cellular(f3).x * Amplitude);
                    break;
                case NoiseType.WorleyNoiseF2:
                    Voxels.SetValue(i3, noise.cellular(f3).y * Amplitude);
                    break;
            }
        }
    }
}