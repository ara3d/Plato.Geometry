using System.Collections.Generic;
using Ara3D.Collections;
using Ara3D.Geometry;
using Ara3D.UnityBridge;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class PlanarPoints : FilterComponent<object, IPoints>
    {
        public List<Vector2> points = new List<Vector2>()
        {
            new(5, 2),
            new(5, 3),
            new(4, 4),
            new(3, 3),
            new(3, 2),
            new(4, 3),
        };

        public override IPoints EvalImpl(object input)
            => new PointsGeometry(points.ToIArray().Select(p => p.ToAra3D().ToVector3()));
    }
}