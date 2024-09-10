using System;
using Ara3D.Collections;
using Ara3D.Geometry;
using Assets.ClonerExample;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Matrix4x4 = Ara3D.Mathematics.Matrix4x4;
using Vector3 = Ara3D.Mathematics.Vector3;

[ExecuteAlways]
public class VoxelsToPoints : JobScheduler<INativeVoxelData, IPoints>, IPoints, IArray<Vector3>
{
    public float Threshold;
    private NativeQueue<float3> _queue;
    private VoxelsToQueueJob _job;

    public ref NativeArray<float3> Points => ref _points;
    public NativeArray<float3> _points;
    public override IPoints Result => this;
    public IIterator<Vector3> Iterator => new ArrayIterator<Vector3>(this);

    public int Count => _points.Length;
    public Vector3 this[int index] => _points[index].ToAra3D(); 

    public void OnDisable()
    {
        _queue.SafeDispose();
        _points.SafeDispose();
    }

    public override JobHandle ScheduleJob(INativeVoxelData inputData, JobHandle previous)
    {
        var voxels = inputData.Voxels;
        _queue = new NativeQueue<float3>(Allocator.Persistent);

        _job.Voxels = voxels;
        _job.PointWriter = _queue.AsParallelWriter();
        _job.Threshold = Threshold;

        var h = _job.Schedule(voxels.VoxelCount, 64, previous);
        h.Complete();

        _points = _queue.ToArray(Allocator.Persistent);
        return h;
    }

    public IGeometry Transform(Matrix4x4 mat)
    {
        throw new NotImplementedException();
    }

    public IGeometry Deform(Func<Vector3, Vector3> f)
    {
        throw new NotImplementedException();
    }

    IArray<Vector3> IPoints.Points { get; }
}

[BurstCompile(FloatPrecision.Standard, FloatMode.Fast, CompileSynchronously = true)]
public struct VoxelsToQueueJob : IJobParallelFor
{
    [ReadOnly] public VoxelData<float> Voxels;
    [WriteOnly] public NativeQueue<float3>.ParallelWriter PointWriter;
    [ReadOnly] public float Threshold;

    public void Execute(int i)
    {
        var f = Voxels.GetValue(i);
        if (f > Threshold)
        {
            PointWriter.Enqueue(Voxels.GetCenter(i));
        }
    }
}

