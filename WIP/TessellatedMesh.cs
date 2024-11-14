using Ara3D.Collections;
using Ara3D.Mathematics;
using Ara3D.Utils;
using System;

namespace Ara3D.Geometry
{
    public class TessellatedMesh : PointsGeometry, IQuadMesh, IDeformable<TessellatedMesh>
    {
        public TessellatedMesh(IArray<SurfacePoint> points, IArray<Int4> faceIndices)
            : base(points.Select(p => p.Center))
        {
            Points = points;
            FaceIndices = faceIndices;
        }
        public new IArray<SurfacePoint> Points { get; }
        public IArray<int> Indices => FaceIndices.SelectMany(f => f.ToTuple());
        public IArray<Int4> FaceIndices { get; }

        public TessellatedMesh Deform(Func<Vector3, Vector3> f)
            => new TessellatedMesh(Points.Select(p => p.Deform(f)), FaceIndices);

        public TessellatedMesh Transform(Matrix4x4 mat)
            => Deform(p => p.Transform(mat));

        IQuadMesh ITransformable<IQuadMesh>.Transform(Matrix4x4 mat) 
            => Transform(mat);

        IQuadMesh IDeformable<IQuadMesh>.Deform(Func<Vector3, Vector3> f) 
            => Deform(f);
    }
}
