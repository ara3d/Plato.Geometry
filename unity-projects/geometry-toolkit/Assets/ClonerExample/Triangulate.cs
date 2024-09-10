using Ara3D.Collections;
using Ara3D.Geometry;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class Triangulate : FilterComponent<IPoints, ITriMesh>
    {
        public override ITriMesh EvalImpl(IPoints pts)
        {
            var pts2d = pts.Points.Select(p => p.XY);
            return pts2d.ToMesh(PolygonTriangulation.TriangulateConcavePolygon(pts2d));
        }
    }
}