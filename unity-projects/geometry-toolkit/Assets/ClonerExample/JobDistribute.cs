using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [BurstCompile(CompileSynchronously = true)]
    public struct JobDistribute : IJobParallelFor
    {
        public float Radius;
        public float Strength;
        public CloneData Data;
        public bool ApplyRotation; 

        public void Execute(int i)
        {
            var t = Mathf.PI * 2 * i / Data.Count;
            var p0 = Data.GpuInstance(i).Pos;
            var p1 = new float3(math.sin(t) * Radius, 0, math.cos(t) * Radius);
            Data.GpuInstance(i).Pos = math.lerp(p0, p1, Strength);

            if (ApplyRotation)
            {
                var r0 = Data.GpuInstance(i).Orientation;
                var r1 = math.mul(r0, quaternion.RotateY(t));
                Data.GpuInstance(i).Orientation = math.slerp(r0, r1, Strength);
            }

        }
    }
}