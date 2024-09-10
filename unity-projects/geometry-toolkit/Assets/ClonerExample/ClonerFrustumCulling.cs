using Assets.ClonerExample;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public interface IFilterJob
{
    ref NativeList<int> Indices { get; }
}

[ExecuteAlways]
public class ClonerFrustumCulling : ClonerJobComponent, ICloneJob, IFilterJob
{
    public int From;
    public int count;
    public int Count => count;
    public CloneData _cloneData;
    public ref CloneData CloneData => ref _cloneData;
    public NativeList<int> _indices;
    public ref NativeList<int> Indices => ref _indices;

    public JobHandle Schedule(ICloneJob previous)
    {
        CloneData.Resize(previous.Count);
        Indices = new NativeList<int>(Allocator.Persistent);
        var job = new JobComputeFilter()
        {
            From = From,
            Count = Count,
        };
        Handle = job.ScheduleAppend(Indices, previous.Count, previous.Handle);
        var job2 = new JobFilterClones()
        {
            Indices = Indices.AsDeferredJobArray(),
            InputData = previous.CloneData,
            OutputData = CloneData,
        };
        Handle = job2.Schedule(Indices, 256, Handle);
        return Handle;
    }

    public JobHandle Handle { get; set; }

    public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
    {
        Indices.SafeDispose();
        Indices = new NativeList<int>(Allocator.Persistent);
        var j1 = new JobComputeFilter()
        {
            From = From,
            Count = Count,
        };
        return (previousData, j1.ScheduleAppend(Indices, previousData.Count, previousHandle));
    }
}

[BurstCompile(CompileSynchronously = true)]
public struct JobComputeFilter : IJobFilter
{
    public int From;
    public int Count;

    public bool Execute(int i)
        => i >= From && i < From + Count;
}

[BurstCompile(CompileSynchronously = true)]
public struct JobFilterClones : IJobParallelForDefer
{
    public CloneData InputData;
    public CloneData OutputData;
    [ReadOnly] public NativeArray<int> Indices;

    public void Execute(int i)
    {
        OutputData.GpuInstance(i) = InputData.GpuInstance(Indices[i]);
    }
}

public static class UtilFrustumCulling
{
    private static readonly Plane[] _planes = new Plane[6];

    [BurstCompile]
    private struct FilterViewFrustumCulling : IJobFilter
    {
        [ReadOnly, DeallocateOnJobCompletion] public NativeArray<float4> FrustumPlanes;
        [ReadOnly] public NativeArray<float3> Positions;
        [ReadOnly] public NativeArray<float3> Extents;

        public bool Execute(int i)
        {
            for (var j = 0; j < 6; j++)
            {
                var planeNormal = FrustumPlanes[j].xyz;

                var planeConstant = FrustumPlanes[j].w;

                if (math.dot(Extents[i], math.abs(planeNormal)) +
                    math.dot(planeNormal, Positions[i]) +
                    planeConstant <= 0f)
                    return false;
            }

            return true;
        }
    }

    [BurstCompile]
    private struct FilterViewFrustumSingleSizeCulling : IJobFilter
    {
        [ReadOnly, DeallocateOnJobCompletion] public NativeArray<float4> FrustumPlanes;
        [ReadOnly] public NativeArray<float3> Positions;

        public bool Execute(int i)
        {
            for (var j = 0; j < 6; j++)
            {
                var planeNormal = FrustumPlanes[j].xyz;
                var planeConstant = FrustumPlanes[j].w;
                if (math.dot(planeNormal, Positions[i]) +
                    planeConstant <= 0f)
                    return false;
            }

            return true;
        }
    }

    private static NativeArray<float4> _frustumPlanes;

#if UNITY_EDITOR
    private static bool _exitingPlayMode = false;
    private static void DisposeOnQuit(UnityEditor.PlayModeStateChange state)
    {
        if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
        {
            _exitingPlayMode = true;
            _frustumPlanes.Dispose();
            UnityEditor.EditorApplication.playModeStateChanged -= DisposeOnQuit;
        }
        else if (state == UnityEditor.PlayModeStateChange.ExitingEditMode)
        {
            _exitingPlayMode = false;
        }
    }
#endif

    public static void SetFrustumPlanes(float4x4 worldProjectionMatrix)
    {
        if (_frustumPlanes.IsCreated == false)
        {
#if UNITY_EDITOR
            if (_exitingPlayMode)
            {
                Debug.LogWarning("Trying to set frustum planes while exiting play mode?");
                return; // TODO :: warn?
            }

            UnityEditor.EditorApplication.playModeStateChanged += DisposeOnQuit;
#endif

            _frustumPlanes = new NativeArray<float4>(6, Allocator.Persistent);
        }

        GeometryUtility.CalculateFrustumPlanes(worldProjectionMatrix, _planes);

        for (var i = 0; i < 6; ++i)
        {
            _frustumPlanes[i] = new float4(_planes[i].normal, _planes[i].distance);
        }
    }

    public static JobHandle ScheduleCullingJob(float4x4 worldProjectionMatrix, NativeArray<float3> positions,
        NativeArray<float3> extents, NativeList<int> outIndices, JobHandle previous)
    {
        SetFrustumPlanes(worldProjectionMatrix);
        return ScheduleCullingJob(positions, extents, outIndices, previous);
    }

    public static JobHandle ScheduleCullingJob(NativeArray<float3> positions,
        NativeArray<float3> extents, NativeList<int> outIndices, JobHandle previous)
    {
        if (!_frustumPlanes.IsCreated)
        {
            Debug.LogWarning("Trying to schedule a culling job before any frustum planes were set. Either call the ScheduleCullingJob with the WP matrix or call SetFrustumPlanes before.");
            return default(JobHandle);
        }

        return new FilterViewFrustumCulling
        {
            FrustumPlanes = new NativeArray<float4>(_frustumPlanes, Allocator.TempJob),
            Positions = positions,
            Extents = extents
        }.ScheduleAppend(outIndices, positions.Length, previous);
    }

    public static JobHandle ScheduleCullingJob(float4x4 worldProjectionMatrix, NativeArray<float3> positions,
        float3 extents, NativeList<int> outIndices, JobHandle previous)
    {
        SetFrustumPlanes(worldProjectionMatrix);
        return ScheduleCullingJob(positions, extents, outIndices, previous);
    }

    public static JobHandle ScheduleCullingJob(NativeArray<float3> positions,
        float3 extents, NativeList<int> outIndices, JobHandle previous)
    {
        if (!_frustumPlanes.IsCreated)
        {
            Debug.LogWarning("Trying to schedule a culling job before any frustum planes were set. Either call the ScheduleCullingJob with the WP matrix or call SetFrustumPlanes before.");
            return default(JobHandle);
        }

        // embed the extents into plane constants
        var frustumPlanes = new NativeArray<float4>(_frustumPlanes, Allocator.TempJob);

        for (var i = 0; i < 6; i++)
        {
            frustumPlanes[i] = new float4(frustumPlanes[i].xyz,
                math.dot(extents, math.abs(frustumPlanes[i].xyz)) + frustumPlanes[i].w);
        }

        return new FilterViewFrustumSingleSizeCulling
        {
            FrustumPlanes = frustumPlanes,
            Positions = positions
        }
        .ScheduleAppend(outIndices, positions.Length, previous);
    }
}