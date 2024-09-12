using System;
using Ara3D.Collections;
using Ara3D.Geometry;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class PolyCurveToPoints : FilterComponent<ICurve3D, IPoints>
    {
        public int NumberOfSamples;
       
        public override IPoints EvalImpl(ICurve3D curve)
            => new PointsGeometry(NumberOfSamples.InterpolateInclusive().Select(curve.Eval));
    }
}