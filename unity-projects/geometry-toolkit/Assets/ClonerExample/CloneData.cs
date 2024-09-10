using Unity.Collections;

namespace Assets.ClonerExample
{
    public struct CloneData
    {
        public int Count => CpuArray.Length;

        [NativeDisableParallelForRestriction] public NativeArray<CpuInstanceData> CpuArray;
        [NativeDisableParallelForRestriction] public NativeArray<GpuInstanceData> GpuArray;

        public ref CpuInstanceData CpuInstance(int i) => ref CpuArray.GetRef(i);
        public ref GpuInstanceData GpuInstance(int i) => ref GpuArray.GetRef(i);

        public void Resize(int n)
        {
            CpuArray.Resize(n);
            GpuArray.Resize(n);
        }
        
        public void Replicate(CloneData other, int count)
        {
            var n = other.Count * count;
            Resize(n);
        }

        public bool IsValid
            => CpuArray.IsCreated && GpuArray.IsCreated;

        
        public bool Expired(int i, float currentTime, float maxAge)
        {
            return CpuInstance(i).GetAge(currentTime) > maxAge;
        }

        public void Dispose()
        {
            CpuArray.SafeDispose();
            GpuArray.SafeDispose();
        }

        public void Update(float currentTime, int i)
        {
            CpuInstance(i).Update(currentTime, ref GpuInstance(i));
        }

        public void Swap(int i, int j)
        {
            (CpuInstance(i), CpuInstance(j)) = (CpuInstance(j), CpuInstance(i));
            (GpuInstance(i), GpuInstance(j)) = (GpuInstance(j), GpuInstance(i));
        }
    }
}