using Ara3D.Collections;
using Ara3D.Geometry;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class PolyLineOffset : FilterComponent<IPolyLine2D, IPolyLine2D>
    {
        public float Offset = 1f;
        public bool Reverse = false;
        public bool KeepOriginal = true; 
        public bool ForceClosed = false;

        public override IPolyLine2D EvalImpl(IPolyLine2D input)
        {
            var r = input.Points.Offset(Offset, input.Closed).ToPolyLine2D(input.Closed || ForceClosed);

            if (Reverse)
            {
                r = r.Points.Reverse().ToPolyLine2D(input.Closed || ForceClosed);
            }

            if (KeepOriginal)
            {
                r = input.Points.Concat(r.Points).ToPolyLine2D(input.Closed || ForceClosed);
            }

            return r;
        }
    }
}