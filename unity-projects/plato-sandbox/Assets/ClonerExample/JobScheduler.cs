using Unity.Jobs;
using UnityEngine;

namespace Assets.ClonerExample
{
    public interface INoData { }

    public interface INativeVoxelData
    {
        ref VoxelData<float> Voxels { get; }
    }

    public interface ICloneJob
    {
        JobHandle Schedule(ICloneJob previous);
        JobHandle Handle { get; }
        ref CloneData CloneData { get; }
        int Count { get; }
    }

    public abstract class JobScheduler<TInput, TOutput> :
        MonoBehaviour,
        IJobScheduler<TOutput>
    {
        public abstract JobHandle ScheduleJob(TInput inputData, JobHandle previousHandle);

        public abstract TOutput Result { get; }

        public IJobResult<TOutput> Schedule()
        {
            var previous = this.GetPreviousComponent<IJobScheduler<TInput>>();
            var previousResult = previous != null ? previous.Schedule() : null;
            var h = previousResult != null
                ? ScheduleJob(previousResult.Result, previousResult.Handle)
                : ScheduleJob(default, default);
            return new JobResult<TOutput>(Result, h);
        }
    }

    public interface IJobResult
    {
        JobHandle Handle { get; }
    }

    public interface IJobResult<out TOutput>
        : IJobResult
    {
        TOutput Result { get; }
    }

    public class JobResult<TOutput> : IJobResult<TOutput>
    {
        public JobResult(TOutput result, JobHandle handle)
        {
            Result = result;
            Handle = handle;
        }

        public TOutput Result { get; }
        public JobHandle Handle { get; }
    }

    public interface IJobScheduler
    {
    }

    public interface IJobScheduler<out TOutput>
        : IJobScheduler
    {
        IJobResult<TOutput> Schedule();
    }

    public static class JobSchedulerExtensions
    {
        public static TOutput ScheduleNow<TOutput>(this IJobScheduler<TOutput> self)
        {
            var r = self.Schedule();
            r.Handle.Complete();
            return r.Result;
        }
    }
}