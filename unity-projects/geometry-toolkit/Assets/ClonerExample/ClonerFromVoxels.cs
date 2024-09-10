using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerFromVoxels : ClonerJobComponent
    {
        public CloneData CloneData;
        public Color Color = Color.blue;
        [Range(0, 1)] public float Metallic = 0.5f;
        [Range(0, 1)] public float Smoothness = 0.5f;

        public float Threshold;
        private NativeQueue<float3> _queue;
        private VoxelsToQueueJob _job;

        public ref NativeArray<float3> Points => ref _points;
        public NativeArray<float3> _points;

        public void OnDisable()
        {
            _queue.SafeDispose();
            _points.SafeDispose();
            CloneData.Dispose();
        }

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            var previousJob = this.GetPreviousComponent<IJobScheduler<INativeVoxelData>>();
            if (previousJob == null)
                throw new Exception("No voxels generator found");

            var voxels = previousJob.ScheduleNow().Voxels;

            _queue.SafeDispose();
            _queue = new NativeQueue<float3>(Allocator.Persistent);

            _job.Voxels = voxels;
            _job.PointWriter = _queue.AsParallelWriter();
            _job.Threshold = Threshold;

            var h = _job.Schedule(voxels.VoxelCount, 64, previousHandle);
            h.Complete();

            _points.SafeDispose();
            _points = _queue.ToArray(Allocator.Persistent);
            var count = _points.Length;

            CloneData.Resize(count);
            var job = new JobInitializeFromPoints(CloneData,
                _points,
                new float4(Color.r, Color.g, Color.b, Color.a),
                Metallic,
                Smoothness,
                voxels.VoxelSize);
            var h2 = job.Schedule(count, batchSize, previousHandle);
            return (CloneData, h2);
        }

    }
}