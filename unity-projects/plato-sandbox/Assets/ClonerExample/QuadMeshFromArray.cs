using System;
using System.Collections.Generic;
using Ara3D.Collections;
using Ara3D.Geometry;
using Ara3D.Mathematics;
using Ara3D.UnityBridge;
using UnityEngine;
using IPoints = Ara3D.Geometry.IPoints;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class QuadMeshFromArray : FilterComponent<List<object>, ITriMesh>
    {
        public bool ClosedX = true;
        public bool ClosedY = false;

        public override ITriMesh EvalImpl(List<object> input)
        {
            var pointsArray = input.ToIArray().Select(o => (IPoints)o);
            var points = pointsArray.Select(p => p.Points).ToArray2D();
            var gridMesh = new GridMesh(points, ClosedX, ClosedY);
            return gridMesh.Triangulate();
        }
    }
}