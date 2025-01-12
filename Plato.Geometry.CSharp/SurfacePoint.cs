using System;
using Plato.SinglePrecision;

namespace Plato.Geometry
{
    /// <summary>
    /// Usually the result of sampling a parametric surface.
    /// Each point (and vector to it) can be referred to by index, as can the quadrant formed between it and the the next point. See below:
    ///      a
    ///    3 | 0
    /// d -- p -- b
    ///    2 | 1
    ///      c
    /// The output tangent vector is from p to b.
    /// The output binormal vector is from p to a.
    /// The input tangent vector is from d to p.
    /// The input binormal vector is from c to p. 
    /// </summary>
    public interface ISurfacePoint
    {
        Vector2D UV { get; }
        Vector3D InBinormal { get; }
        Vector3D OutBinormal { get; }
        Vector3D InTangent { get; }
        Vector3D OutTangent { get; }
    }

    public class SurfacePoint
        : ISurfacePoint
    {
        public SurfacePoint(Vector3D center, Vector2D uv, Vector3D a, Vector3D b, Vector3D c, Vector3D d)
        {
            Center = center;
            UV = uv;
            V0 = a;
            V1 = b;
            V2 = c;
            V3 = d;
        }
        public Vector3D Center { get; }
        public Vector2D UV { get; }
        public Vector3D Normal => OutTangent.Cross(OutBinormal);
        public Vector3D V0 { get; }
        public Vector3D V1 { get; }
        public Vector3D V2 { get; }
        public Vector3D V3 { get; }
        public Vector3D InBinormal => Center - V2;
        public Vector3D OutBinormal => V0 - Center;
        public Vector3D InTangent => Center - V3;
        public Vector3D OutTangent => V1 - Center;

        public static SurfacePoint Create(Func<Vector2D, Vector3D> f, Vector2D uv)
        {
            var e = Constants.Epsilon;
            var p = f(uv);
            var a = f(uv - (0, e));
            var b = f(uv + (e, 0));
            var c = f(uv + (0, e));
            var d = f(uv - (e, 0));
            return new SurfacePoint(p, uv, a, b, c, d);
        }
    }
}