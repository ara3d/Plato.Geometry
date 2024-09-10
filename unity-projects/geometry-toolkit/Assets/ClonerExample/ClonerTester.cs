using Unity.Jobs;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerTester : ClonerJobComponent
    {
        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            Debug.Log($"Clone data count = {previousData.Count}, is valid = {previousData.IsValid}");
            return (previousData, previousHandle);
        }
    }
}