 using System.Collections.Generic;
 using Ara3D.Geometry;
 using Unity.Mathematics;
 using UnityEngine;
 using Vector3 = UnityEngine.Vector3;

 namespace Assets.ClonerExample
 {
     [ExecuteAlways]
     public class PolyCurve : FilterComponent<object, ICurve3D>, ICurve3D
     {
         public bool Closed => true;

         public List<Vector3> Points = new()
         {
             new Vector3(0, 0, 0),
             new Vector3(0.5f, 0, 0.5f),
             new Vector3(1f, 0, 0.5f),
         };

         public Vector3 EvalBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
             => (1 - t) * (1 - t) * (1 - t) * p0 + 3 * (1 - t) * (1 - t) * t * p1 + 3 * (1 - t) * t * t * p2 +
                t * t * t * p3;

         public Ara3D.Mathematics.Vector3 Eval(float x)
         {
             if (Points.Count < 3) return (0, 0, 0);
             var segs = Points.Count - 2;
             var i = (int)math.floor(segs * x);
             var t = (segs * x) - i;
             var p0 = Points[i];
             var p1 = Points[(i + 1) % Points.Count];
             var p2 = Points[(i + 2) % Points.Count];
             var p3 = Points[(i + 3) % Points.Count];
             var r = EvalBezier(t, p0, p1, p2, p3);
             return (r.x, r.y, r.z);
         }

         public override ICurve3D EvalImpl(object input)
             => this;
     }
 }