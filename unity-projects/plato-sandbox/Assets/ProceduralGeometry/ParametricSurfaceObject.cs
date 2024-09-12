using Ara3D.Geometry;
using Ara3D.UnityBridge;
using UnityEngine;
using Vector2 = Ara3D.Mathematics.Vector2;
using Vector3 = Ara3D.Mathematics.Vector3;

[ExecuteAlways]
public class ParametricSurfaceObject : ProceduralGeometryObject
{
    public int USegments = 20;
    public int VSegments = 20;

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

    public bool ClosedU = false;
    public bool ClosedV = false;

    [Range(-2,2)] public float URange = 1;
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
    public override ITriMesh ComputeGeometry()
    {
        return new ParametricSurface(uv => Eval(uv), ClosedU, ClosedV)
            .Tesselate(USegments, VSegments)
            .Triangulate();
    }
}