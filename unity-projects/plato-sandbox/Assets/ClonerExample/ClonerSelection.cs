using Unity.Jobs;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerSelection : ClonerJobComponent
    {
        [Range(0, 1)] public float Center = 0.5f;
        [Range(0, 1)] public float Extent = 1f;
        public bool Invert;
        public bool Randomize;
        public uint Seed = 428749;
        public float MinStrength = 0f;
        public float MaxStrength = 1f;
        public bool WrapAround = true;

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            return (previousData, new JobSelect()
                {
                    Data = previousData,
                    Center = Center,
                    Extent = Extent,
                    Invert = Invert,
                    Randomize = Randomize,
                    Seed = Seed,
                    MinStrength = MinStrength,
                    MaxStrength = MaxStrength,
                    WrapAround = WrapAround,
                }
                .Schedule(previousData.Count, batchSize, previousHandle));
        }
    }
}