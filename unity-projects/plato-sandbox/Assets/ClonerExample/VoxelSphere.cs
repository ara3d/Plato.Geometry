using Assets.ClonerExample;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;

[ExecuteAlways]
public class VoxelSphere : JobScheduler<INoData, INativeVoxelData>, INativeVoxelData
{
    public int GridResolution = 10;
    public Bounds Bounds = new(Vector3.zero, Vector3.one * 10);
    public VoxelData<float> _voxels;
    public float Radius;
    private float cachedRadius;
    public bool Computed = false;

    public void OnDisable()
    {
        _voxels.SafeDispose();
    }
    
    public override JobHandle ScheduleJob(INoData noData, JobHandle previous)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (Radius != cachedRadius)
        {
            cachedRadius = Radius;
            Computed = false;
        }

        if (GridResolution != _voxels.GridHeight
            || GridResolution != _voxels.GridWidth
            || GridResolution != _voxels.GridDepth)
        {
            Computed = false;
        }

        if (_voxels.IsValid && Computed)
            return previous;

        _voxels.Update(Bounds, GridResolution);
        _voxels.FromSDF(p => Radius - math.length(p));

        Computed = true;
        return previous;
    }

    public override INativeVoxelData Result => this;
    public ref VoxelData<float> Voxels => ref _voxels;
}

