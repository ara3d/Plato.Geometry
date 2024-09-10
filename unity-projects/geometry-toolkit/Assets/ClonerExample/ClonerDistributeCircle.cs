using Unity.Jobs;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerDistributeCircle : ClonerJobComponent
    {
        public float Radius = 10;
        [Range(0, 1)] public float Strength = 1;
        public bool ApplyRotation = true;

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            return (previousData,
                new JobDistribute() { Data = previousData, Radius = Radius, Strength = Strength, ApplyRotation = ApplyRotation }
                    .Schedule(previousData.Count, batchSize, previousHandle));
        }
    }
}