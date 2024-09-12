using Ara3D.Geometry;
using System.Collections.Generic;
using System.Linq;
using Ara3D.Collections;
using Ara3D.UnityBridge;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class PolyLine : FilterComponent<object, IPolyLine2D>
    {
        public List<Vector2> points = new List<Vector2>()
        {
            new(-5, -5),
            new(5, -5),
            new(5, 5),
        };

        public bool Closed;

        public override IPolyLine2D EvalImpl(object input)
            => new PolyLine2D(points.Select(p => p.ToAra3D()).ToIArray(), Closed);
    }
}