using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Assets.ClonerExample
{
    [BurstCompile(CompileSynchronously = true)]
    public struct JobAssignColors : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<float4> Colors;
        public CloneData Data;
        public float Strength;
        public bool Randomize;
        public ulong RandomSeed;

        public void Execute(int index)
        {
            var c0 = Data.GpuInstance(index).Color;
            var n = Randomize ? Rng.GetNthInt(RandomSeed, (ulong)index) : index;
            var c1 = Colors[n % Colors.Length];
            Data.GpuInstance(index).Color = math.lerp(c0, c1, Strength);
        }
    }
}