using Plato.Geometry;

namespace Plato.DoublePrecision
{
    public static class MeshExtensions
    {
        public static IArray<Integer> Indices<T>(this IArray<T> self)
            => self.Count.Range;

        public static IArray<Vector3D> Points(this IArray<Triangle3D> self)
            => self.FlatMap(t => t);

        public static TriangleMesh ToTriangleMesh(this IArray<Vector3D> points)
            => new TriangleMesh(points, points.Indices());

        public static TriangleMesh ToMesh(this IArray<Triangle3D> triangles)
            => triangles.Points().ToTriangleMesh();
    }

    public partial struct TriangleMesh
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
    }
}