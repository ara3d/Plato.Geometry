using Plato.Geometry;

namespace Plato.DoublePrecision
{
    public partial struct TriangleMesh
    {
        public IArray<Vector3D> FaceNormals
            => Primitives.Map(t => t.Normal);

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