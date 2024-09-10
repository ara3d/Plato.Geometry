using Unity.Burst;  
using Unity.Jobs;
namespace Assets.ClonerExample
{
    [BurstCompile(CompileSynchronously = true)]
    public struct JobClonerToVoxels : IJobParallelFor
    {
        public CloneData Data;
        public VoxelData<float> Voxels;

        public void Execute(int i)
        {
            var pos = Data.GpuInstance(i).Pos;
            var index = Voxels.ToIndex3(pos);
            Voxels.SetValue(index, 1);
        }
    }
}