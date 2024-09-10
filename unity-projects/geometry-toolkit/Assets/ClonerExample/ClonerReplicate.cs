using System;
using Unity.Jobs;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerReplicate : ClonerJobComponent
    {
        public int Count;
        public Vector3 Translation;
        public Quaternion Rotation = Quaternion.identity;

        public CloneData CloneData;

        public void OnValidate()
        {
            Count = Math.Max(1, Count);
        }

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            CloneData.Replicate(previousData, Count);
            if (CloneData.Count == 0)
                return (CloneData, default);

            return (CloneData, new JobReplicate()
                {
                    Count = Count,
                    Translation = Translation,
                    Rotation = Rotation,
                    DataSource = previousData,
                    DataSink = CloneData,
                }
                .Schedule(previousData.Count, batchSize, previousHandle));
        }

        public void OnDisable()
        {
            CloneData.Dispose();
        }
    }
}