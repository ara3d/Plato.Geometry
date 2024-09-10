using Ara3D.Geometry;
using Ara3D.UnityBridge;
using Assets.ClonerExample;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = Ara3D.Mathematics.Vector2;
using Vector3 = Ara3D.Mathematics.Vector3;


[ExecuteAlways]
public class ClonerSurfaceDistribution : ClonerJobComponent
{
    public enum GeometryType
    {
        Sphere,
        Cylinder,
        Cone,
        HalfCone,
        Plane,
        Torus,
        Disc,
        MonkeySaddle,
        Trefoil,
        Capsule,
        Handkerchief,
        CrossedTrough,
        SinPlusCos,
    }

    public GeometryType Type = GeometryType.Sphere;

    [Range(-2, 2)] public float URange = 1;
    [Range(-2, 2)] public float VRange = 1f;
    [Range(-2, 2)] public float UOffset = 0;
    [Range(-2, 2)] public float VOffset = 0;

    public Vector3 Eval(Vector2 uv)
    {
        uv *= (URange, VRange);
        uv += (UOffset, VOffset);

        switch (Type)
        {
            case GeometryType.Sphere:
                return SurfaceFunctions.Sphere(uv);

            case GeometryType.Cylinder:
                return SurfaceFunctions.Cylinder(uv);

            case GeometryType.Cone:
                return SurfaceFunctions.ConicalSection(uv, 1, 0);

            case GeometryType.HalfCone:
                return SurfaceFunctions.ConicalSection(uv, 1, 0.5f);

            case GeometryType.Plane:
                return uv.Plane();

            case GeometryType.Torus:
                return SurfaceFunctions.Torus(uv, 1, 0.2f);

            case GeometryType.Disc:
                return SurfaceFunctions.Disc(uv);

            case GeometryType.MonkeySaddle:
                return SurfaceFunctions.MonkeySaddle(uv);

            case GeometryType.Trefoil:
                return SurfaceFunctions.Trefoil(uv, 0.2f);

            case GeometryType.Capsule:
                return SurfaceFunctions.Capsule(uv);

            case GeometryType.CrossedTrough:
                return SurfaceFunctions.CrossedTrough(uv);

            case GeometryType.Handkerchief:
                return SurfaceFunctions.Handkerchief(uv);

            case GeometryType.SinPlusCos:
                return SurfaceFunctions.SinPlusCos(uv);

            default:
                return uv;
        }
    }

    public NativeArray<float3> CachedPoints; 

    public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
    {
        var ps = new ParametricSurface(Eval, false, false);
        var n = previousData.Count;
        CachedPoints.Resize(n);

        // HACK: this isn't scheduled correctly   
        for (var i = 0; i < n; ++i)
        {
            var uv = (UnityEngine.Vector2)previousData.CpuInstance(i).Uv;
            var p = ps.Eval(uv.ToAra3D()).ToUnityFromAra3D();
            CachedPoints[i] = p;
        }

        return (previousData, new JobSetPosition()
        {
            CachedPoints = CachedPoints, 
            CloneData = previousData
        }
            .Schedule(previousData.Count, batchSize, previousHandle));
    }
}

[BurstCompile(CompileSynchronously = true)]
public struct JobSetPosition : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<float3> CachedPoints;
    public CloneData CloneData; 

    public void Execute(int index)
    {
        CloneData.GpuInstance(index).Pos = CachedPoints[index];
    }
}