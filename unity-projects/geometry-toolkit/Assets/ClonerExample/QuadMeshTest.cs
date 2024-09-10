using System.Collections.Generic;
using Ara3D.Collections;
using Ara3D.Geometry;
using Ara3D.UnityBridge;
using UnityEngine;
using Matrix4x4 = Ara3D.Mathematics.Matrix4x4;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class QuadMeshTest : ProceduralGeometryObject
    {
        public List<Vector3> Points = new List<Vector3>()
        {
            new Vector3(0, 0, 0),
            new Vector3(2, 0, 1),
            new Vector3(4, 0, 2),
        };

        public bool ClosedX;
        public bool ClosedY;
        public int Rows = 3;
        public Vector3 Offset;
        
        public override ITriMesh ComputeGeometry()
        {
            var points0 = Points.ToIArray().Select(p => p.ToAra3D());
            var points = Rows.Select(i => points0.Transform(Matrix4x4.CreateTranslation(Offset.ToAra3D() * i)));
            return new GridMesh(points.ToArray2D(), ClosedX, ClosedY).Triangulate();
        }
    }
}