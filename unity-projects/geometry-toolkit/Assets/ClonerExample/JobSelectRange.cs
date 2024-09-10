using Unity.Burst;
using Unity.Jobs;

namespace Assets.ClonerExample
{
    [BurstCompile(CompileSynchronously = true)]
    public struct JobSelectRange : IJobParallelFor
    {
        public CloneData Data;
        public int From;
        public int Count;
        public int Stride;
        public float Strength;

        public void Execute(int i)
        {
            var n = i - From;
            Data.CpuInstance(i).Selection = n >= 0 && n < Count && (n % Stride == 0) ? Strength : 0;
        }
    }
}