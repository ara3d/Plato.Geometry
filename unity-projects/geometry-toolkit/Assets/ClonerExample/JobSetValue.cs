using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Jobs;

namespace Assets.ClonerExample
{
    // https://docs.unity3d.com/Packages/com.unity.burst@1.6/manual/docs/OptimizationGuidelines.html#generic-jobs

    [BurstCompile(
        FloatPrecision = FloatPrecision.Low, 
        FloatMode = FloatMode.Fast, 
        CompileSynchronously = true,
        Debug = false, DisableSafetyChecks = false, 
        OptimizeFor = OptimizeFor.Performance)]
    public struct ComposeJob<TJob1, TJob2> : IJobParallelFor
        where TJob1 : struct, IJobParallelFor
        where TJob2 : struct, IJobParallelFor
    {
        private TJob1 Job1;
        private TJob2 Job2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ComposeJob(TJob1 job1, TJob2 job2)
        {
            Job1 = job1;
            Job2 = job2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), SkipLocalsInit]
        public void Execute(int index)
        {
            Job1.Execute(index);
            Job2.Execute(index);
        }
    }



}