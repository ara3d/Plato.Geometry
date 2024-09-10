using System;
using Unity.Jobs;
using UnityEngine;

namespace Assets.ClonerExample
{
    public abstract class ClonerJobComponent : MonoBehaviour
    {

        public float CurrentTime;
        public abstract (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize);

        /// <summary>
        /// Called once per-frame, the presence of an empty function assures that the user is provided
        /// an enable/disable checkbox on the component. 
        /// </summary>
        public void Update()
        {
            CurrentTime = Time.time;
        }

        public virtual void Map(ref CloneData cloneData, int from, int upTo) => throw new NotImplementedException();
        public virtual bool IsMap() => false;

        public virtual bool IsGenerator() => false;
        public virtual int InitGenerator(ref CloneData cloneData) => throw new NotImplementedException();

        public virtual JobHandle Transform(ref CloneData cloneData, JobHandle previous, int batchCount) => throw new NotImplementedException();
        public virtual JobHandle Compose(ref CloneData cloneData, ClonerJobComponent previous) => throw new NotImplementedException();
        public virtual JobHandle Compose<T>(ref CloneData cloneData, T nextJob) where T: struct, IJobParallelFor => throw new NotImplementedException();
    }
}