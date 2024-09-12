using System;
using Ara3D.Collections;
using Ara3D.Mathematics;

namespace Ara3D.Geometry
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
        : IVertexNeighbourhood
    {
        Vector2 UV { get; }
        Vector3 InBinormal { get; }
        Vector3 OutBinormal { get; }
        Vector3 InTangent { get; }
        Vector3 OutTangent { get; }
    }

    public class SurfacePoint 
        : ISurfacePoint, IDeformable<SurfacePoint>
    {
        public SurfacePoint(Vector3 center, Vector2 uv, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            Center = center;
            UV = uv;
            V0 = a;
            V1 = b; 
            V2 = c;
            V3 = d;
            Neighbours = LinqArray.Create(V0, V1, V2, V3);
        }
        public Vector3 Center { get; }
        public IArray<Vector3> Neighbours { get; }
        public Vector2 UV { get; }
        public Vector3 Normal => this.AverageNormal();
        public Vector3 V0 { get; }
        public Vector3 V1 { get; }
        public Vector3 V2 { get; }
        public Vector3 V3 { get; }
        public Vector3 InBinormal => Center - V2;
        public Vector3 OutBinormal => V0 - Center;
        public Vector3 InTangent => Center - V3;
        public Vector3 OutTangent => V1 - Center;

        public SurfacePoint Transform(Matrix4x4 mat)
            => Deform(v => v.Transform(mat));

        public SurfacePoint Deform(Func<Vector3, Vector3> f)
            => new SurfacePoint(f(Center), UV, f(V0), f(V1), f(V2), f(V3));

        IVertexNeighbourhood ITransformable<IVertexNeighbourhood>.Transform(Matrix4x4 mat)
            => Transform(mat);

        IVertexNeighbourhood IDeformable<IVertexNeighbourhood>.Deform(Func<Vector3, Vector3> f)
            => Deform(f);
    }
}