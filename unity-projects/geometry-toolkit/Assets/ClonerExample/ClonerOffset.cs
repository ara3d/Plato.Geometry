using Unity.Jobs;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerOffset : ClonerJobComponent, ICloneJob
    {
        public Vector3 Translation;
        public Quaternion Rotation;
        [Range(0, 1)] public float Strength = 1;
        public bool UseSelection;
        
        public ICloneJob Previous { get; set; }
        public JobHandle Handle { get; set; } 
        public int Count => Previous?.Count ?? 0;
        public ref CloneData CloneData => ref Previous.CloneData;

        public JobHandle Schedule(ICloneJob previous)
        {
            Previous = previous;
            return Handle = CreateJob(ref previous.CloneData).Schedule(previous.Count, 256, previous.Handle);
        }

        public JobOffset CreateJob(ref CloneData cloneData)
        {
            return new JobOffset()
            {
                Data = cloneData, 
                Translation = Translation, 
                Rotation = Rotation, 
                Strength = Strength,
                UseSelection = UseSelection
            };
        }

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            Debug.Log($"Offset see {previousData.Count}");
            return (previousData,
                CreateJob(ref previousData).Schedule(previousData.Count, batchSize, previousHandle));
        }
    }
}   