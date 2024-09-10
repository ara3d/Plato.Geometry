using Ara3D.Geometry;
using Ara3D.UnityBridge;
using UnityEngine;

[ExecuteAlways]
public class PlatonicSolid : ProceduralGeometryObject
{
    public PlatonicSolidsEnum Type = PlatonicSolidsEnum.Tetrahedron;
    public bool Faceted = true;

    public override ITriMesh ComputeGeometry()
    {
        var r = PlatonicSolids.ToMesh(Type);
        if (Faceted) 
            r = r.Faceted();
        return r;
    }
}