using Ara3D.Collections;
using Ara3D.Geometry;
using Ara3D.Mathematics;
using Ara3D.Serialization.G3D;
using Ara3D.Serialization.VIM;
using UnityEngine;
using UnityEngine.Rendering;
using Matrix4x4 = Ara3D.Mathematics.Matrix4x4;
using Mesh = UnityEngine.Mesh;
using MeshTopology = UnityEngine.MeshTopology;
using Quaternion = Ara3D.Mathematics.Quaternion;
// Explicitly specify math types to make it clear what each function does
using UVector4 = UnityEngine.Vector4;
using UVector3 = UnityEngine.Vector3;
using UVector2 = UnityEngine.Vector2;
using UQuaternion = UnityEngine.Quaternion;
using Vector2 = Ara3D.Mathematics.Vector2;
using Vector3 = Ara3D.Mathematics.Vector3;
using Vector4 = Ara3D.Mathematics.Vector4;

namespace Ara3D.UnityBridge
{
    public static class UnityConverters
    {
        // When translating G3D faces to unity we need
        // to reverse the triangle winding.
        public static int PolyFaceToUnity(int index, int faceSize)
        {
            var faceIdx = index / faceSize;
            var vertIdx = index % faceSize;
            return (vertIdx == 0) ? index : (faceIdx * faceSize) + (faceSize - vertIdx);
        }
            
        // Remaps 1, 2, 3 to 1, 3, 2
        public static int TriFaceToUnity(int index)
            => PolyFaceToUnity(index, 3);

        public static UVector3 ToUnityFromAra3D(this Vector3 v)
            => ToUnityFromAra3D(v.X, v.Y, v.Z);

        public static UVector3 ToUnityFromVim(this Vector3 v)
            => ToUnityFromVim(v.X, v.Y, v.Z);

        public static UVector3 ToUnity(this Vector3 v)
            => new(v.X, v.Y, v.Z);

        public static UVector3 ToUnityFromAra3D(float x, float y, float z) 
            => new(-x, z, -y);

        public static UVector3 ToUnityFromVim(float x, float y, float z)
            => ToUnityFromAra3D(x, y, z) * FeetToMeters;
            
        public static UQuaternion ToUnity(this Quaternion rot)
            => new(rot.X, -rot.Z, rot.Y, rot.W);

        public static UVector3 SwizzleToUnity(float x, float y, float z) => new(x, z, y);

        public static UVector3 SwizzleToUnity(Vector3 v)
            => SwizzleToUnity(v.X, v.Z, v.Y);

        public static UVector3 ToUnityScale(this Vector3 scl)
            => SwizzleToUnity(scl);

        public static Bounds ToUnityFromVim(this AABox box) =>
            new(ToUnityFromVim(box.Center), SwizzleToUnity(box.Extent));

        public static UVector3[] ToUnityFromVim(this IArray<Vector3> vertices)
            => vertices.Select(ToUnityFromVim).ToArray();

        public static int[] ToUnityIndexBuffer(this IArray<int> indices)
            => indices.ReverseTriangleIndexOrder().ToArray();

        // 0, 1, 2, 3, 4, 5 ... => 2, 1, 0, 5, 4, 3 ...
        public static IArray<int> ReverseTriangleIndexOrder(this IArray<int> indices)
            => indices.SelectByIndex(indices.Count.Select(i => ((i / 3) + 1) * 3 - 1 - (i % 3)));

       public static Mesh ToUnity(this ITriMesh self, bool fromZUpToYUp, bool invertTriangles, bool doubleSided)
        {
            var mesh = new Mesh();
            mesh.indexFormat = IndexFormat.UInt32;

            var points = fromZUpToYUp
                ? self.Points.Select(p => new UVector3(p.X, p.Z, p.Y))
                : self.Points.Select(p => new UVector3(p.X, p.Y, p.Z));

            mesh.vertices = doubleSided ? points.Concat(points).ToArray() : points.ToArray();

            var flippedIndices = self.Indices.ReverseTriangleIndexOrder();
            var indices = invertTriangles ? flippedIndices : self.Indices;

            if (doubleSided)
            {
                indices = indices.Concat(indices.ReverseTriangleIndexOrder().Select(i => i + self.Points.Count));
            }

            mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

        public static UnityEngine.Matrix4x4 ToUnityRaw(this Matrix4x4 matrix)
            => new(
                new UVector4(matrix.M11, matrix.M12, matrix.M13, matrix.M14),
                new UVector4(matrix.M21, matrix.M22, matrix.M23, matrix.M24),
                new UVector4(matrix.M31, matrix.M32, matrix.M33, matrix.M34),
                new UVector4(matrix.M41, matrix.M42, matrix.M43, matrix.M44)
            );

        private const float FeetToMeters = 0.3408f;

        public static UnityEngine.Matrix4x4 ConversionMatrix = new(
            new UVector4(-FeetToMeters, 0, 0, 0),
            new UVector4(0, 0, -FeetToMeters, 0),
            new UVector4(0, FeetToMeters, 0, 0),
            new UVector4(0, 0, 0, 1)
        );

        public static UnityEngine.Matrix4x4 ToUnity(this Matrix4x4 matrix)
            => ConversionMatrix * ToUnityRaw(matrix);

        public static Vector2 ToAra3D(this UVector2 v) 
            => new(v.x, v.y);

        public static Vector3 ToAra3D(this UVector3 v) 
            => new(v.x, v.y, v.z);

        public static Vector4 ToAra3D(this UVector4 v) 
            => new(v.x, v.y, v.z, v.w);

        public static Quaternion ToAra3D(this UQuaternion q) 
            => new(q.x, q.y, q.z, q.w);

        public static IArray<Vector2> ToAra3D(this UVector2[] xs)
            => xs.ToIArray().Select(x => x.ToAra3D());

        public static IArray<Vector3> ToAra3D(this UVector3[] xs)
            => xs.ToIArray().Select(x => x.ToAra3D());

        public static IArray<Vector4> ToAra3D(this UVector4[] xs)
            => xs.ToIArray().Select(x => x.ToAra3D());

        public static IArray<Quaternion> ToAra3D(this UQuaternion[] xs)
            => xs.ToIArray().Select(x => x.ToAra3D());

        public static UVector4 ToUnity(this Vector4 v) => new(v.X, v.Y, v.Z, v.W);

        public static Color ToUnityColor(this Vector4 v)
            => v.ToUnity();

        public static AABox ToAra3D(this Bounds bounds)
            => (bounds.min.ToAra3D(), bounds.max.ToAra3D());

        public static UnityTriMesh ToUnity(this G3dMesh mesh)
        {
            return new UnityTriMesh()
            {
                UnityIndices = mesh.Indices.ToArray(),
                UnityVertices = mesh.Vertices.ToUnityFromVim(),
                // TODO: normals and UVs
            };
        }

        public static UnityMeshScene ToUnity(this SerializableDocument doc)
        {
            var g = doc.Geometry;
            var r = new UnityMeshScene();
            
            var defaultColor = new Color(0.6f, 0.6f, 0.75f, 1f);
            for (var i =0; i < g.Meshes.Count; i++)
            {
                var m = g.Meshes[i];

                var matIndex = m.Submeshes.Count > 0 
                    ? m.Submeshes[0].MaterialIndex 
                    : -1;

                var set = new UnityMeshInstanceSet(
                    m.ToUnity(),
                    matIndex >= 0
                        ? g.MaterialColors[matIndex].ToUnityColor()
                        : defaultColor
                );
                r.InstanceSets.Add(set);
            }

            for (var i = 0; i < g.InstanceTransforms.Count; i++)
            {
                var t = g.InstanceTransforms[i];
                var idx = g.InstanceMeshes[i];
                if (idx < 0) continue;
                if (idx > r.InstanceSets.Count) continue;
                var set = r.InstanceSets[idx];
                var decomposition = t.Decompose();
                if (decomposition.Decomposed)
                    set.Transforms.Add(decomposition);
                else
                    Debug.Log($"Failed to decompose matrix {i} =S {t}");
            }

            return r;
        }

        public static UnityTriMesh ToAra3D(this Mesh mesh)
            => new UnityTriMesh(mesh);
    }
}
