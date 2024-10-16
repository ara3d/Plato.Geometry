using System;

namespace Plato.DoublePrecision
{
    public partial struct TriangleMesh : IDeformable3D<TriangleMesh>
    {
        public IArray<Vector3D> FaceNormals
            => Primitives.Map(t => t.Normal);

        public TriangleMesh Faceted()
            => Primitives.ToMesh();

        public IArray<Vector3D> ComputeVertexNormalsFaceted()
        {
            var r = new Vector3D[Vertices.Count];
            var triangles = Primitives;
            for (int i = 0; i < triangles.Count; i++)
            {
                var n = triangles[i].Normal;
                var tmp = FaceIndices(i);
                foreach (var j in tmp)
                {
                    r[j] = n;
                }
            }
            return r.ToIArray();
        }

        public TriangleMesh Deform(Func<Vector3D, Vector3D> f)
            => new TriangleMesh(Points.Map(f), Indices);

        public TriangleMesh Transform(Matrix4x4 matrix)
            => Deform(matrix.TransformPoint);

        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);

        ITransformable3D ITransformable3D.Transform(Matrix4x4 matrix)
            => Transform(matrix);

        public static implicit operator PointArray(TriangleMesh mesh)
            => mesh.Points.ToPoints();
    }
}