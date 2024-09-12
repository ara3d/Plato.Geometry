using Ara3D.Geometry;
using Ara3D.Mathematics;
using Ara3D.UnityBridge;
using UnityEngine;

[ExecuteAlways]
public class Polygon : ProceduralGeometryObject
{
    public CommonPolygonsEnum PolygonType = CommonPolygonsEnum.Triangle;
    public PlatonicSolidsEnum MarkerType = PlatonicSolidsEnum.Icosahedron;
    public float Size = 10;

    public override ITriMesh ComputeGeometry()
    {
        var polygon = PolygonType.ToPolygon().To3D().Scale(Size);
        var marker = MarkerType.ToMesh();
        var all = marker.Clone(polygon.Points);
        return all.Merge();
    }
}