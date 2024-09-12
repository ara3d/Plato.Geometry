using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [BurstCompile(CompileSynchronously = true)]
    public struct JobReplicate : IJobParallelFor
    {
        public CloneData DataSource;
        public CloneData DataSink;
        public int Count;
        public float3 Translation;
        public Quaternion Rotation;

        public void Execute(int index)
        {
            Debug.Assert(DataSink.Count == DataSource.Count * Count);
            var pos = DataSource.GpuInstance(index).Pos;
            var rot = DataSource.GpuInstance(index).Orientation;
            for (var i = 0; i < Count; ++i)
            {
                var j = index * Count + i;
                DataSink.GpuArray[j] = DataSource.GpuArray[index];
                DataSink.CpuArray[j] = DataSource.CpuArray[index];
                DataSink.GpuInstance(j).Pos = pos;
                DataSink.GpuInstance(j).Orientation = rot;
                pos += Translation;
                rot *= Rotation;
            }
        }
    }
}