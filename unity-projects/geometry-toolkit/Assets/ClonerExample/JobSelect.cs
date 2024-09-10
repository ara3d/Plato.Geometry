using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;

namespace Assets.ClonerExample
{
    [BurstCompile(CompileSynchronously = true)]
    public struct JobSelect : IJobParallelFor
    {
        public CloneData Data;
        public float Center;
        public float Extent;
        public bool Invert;
        public bool Randomize;
        public uint Seed;
        public float MinStrength;
        public float MaxStrength;
        public bool WrapAround; 

        public void Execute(int i)
        {
            if (Randomize)
            {
                Data.CpuInstance(i).Selection = Rng.GetNthFloat(Seed, (ulong)i);
                return;
            }

            var relIndex = (float)i / Data.Count;
            var halfExtent = Extent / 2;
            var dist = math.abs(Center - relIndex);
            if (WrapAround) dist = math.min(dist, math.abs(Center - (relIndex + 1)));
            var amount = 1.0f - (dist / halfExtent);
            if (Invert) amount = 1.0f - amount;
            var sel = math.lerp(MinStrength, MaxStrength, amount);
            Data.CpuInstance(i).Selection = sel;
        }

        /*
         * double gauss(double x, double a, double b, double c)
        {
            var v1 = (x - b);
            var v2 = (v1 * v1) / (2 * (c*c));
            var v3 = a * Math.Exp(-v2);
            return v3;
        }
         */
    }
}