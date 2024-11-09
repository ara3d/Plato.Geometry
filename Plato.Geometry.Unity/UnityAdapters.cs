using Plato.DoublePrecision;
using UnityEngine;
using Color = Plato.DoublePrecision.Color;
using UColor = UnityEngine.Color;
using Matrix4x4 = Plato.DoublePrecision.Matrix4x4;
using UMatrix4x4 = UnityEngine.Matrix4x4;
using Quaternion = Plato.DoublePrecision.Quaternion;
using UQuaternion = UnityEngine.Quaternion;

namespace Plato.Geometry.Unity
{
    public static class UnityAdapters
    {
        public static UColor ToUnity(this Color c) => new UColor((float)c.R.Value, (float)c.G.Value, (float)c.B.Value, (float)c.A.Value);
        public static Vector4 ToUnity(this Vector4D v) => new Vector4((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);
        public static Vector3 ToUnity(this Vector3D v) => new Vector3((float)v.X, (float)v.Y, (float)v.Z);
        public static Vector2 ToUnity(this Vector2D v) => new Vector2((float)v.X, (float)v.Y);

        public static UQuaternion ToUnity(this Rotation3D r) => r.Quaternion.ToUnity();

        public static UQuaternion ToUnity(this Quaternion q) => new UQuaternion((float)q.X, (float)q.Y, (float)q.Z, (float)q.W);

        public static UMatrix4x4 ToUnity(this Matrix4x4 m)
        {
            return new UMatrix4x4(
                new Vector4((float)m.M11, (float)m.M12, (float)m.M13, (float)m.M14),
                new Vector4((float)m.M21, (float)m.M22, (float)m.M23, (float)m.M24),
                new Vector4((float)m.M31, (float)m.M32, (float)m.M33, (float)m.M34),
                new Vector4((float)m.M41, (float)m.M42, (float)m.M43, (float)m.M44));
        }

        public static Vector2D ToPlato(this Vector2 v) => (v.x, v.y);
        public static Vector3D ToPlato(this Vector3 v) => (v.x, v.y, v.z);
        public static Vector4D ToPlato(this Vector4 v) => (v.x, v.y, v.z, v.w);
        public static Color ToPlato(this UColor c) => (c.r, c.g, c.b, c.a);

        public static Vector3[] ToUnity(this IArray<Vector3D> verts)
            => verts.Map(ToUnity).ToSystemArray();

        public static int[] ToUnity(this IArray<Integer> indices)
            => indices.Map(i => (int)i.Value).ToSystemArray();

        public static Mesh ToUnity(this IQuadMesh3D mesh)
            => mesh.ToTriangleMesh().ToUnity();

        public static Mesh ToUnity(this ITriangleMesh3D mesh)
        {
            var unityMesh = new Mesh
            {
                vertices = mesh.Points.ToUnity(),
                triangles = mesh.Indices.ToUnity()
            };
            //unityMesh.normals = mesh.Vertices.ToUnityNormals();
            //unityMesh.uv = mesh.UVs.ToUnity();
            unityMesh.RecalculateNormals();
            return unityMesh;
        }

        public static void ApplyLocalTransformToUnity(this Matrix4x4 m, Transform t)
        {
            Matrix4x4.Decompose(m, out var scale, out var rotation, out var translation);
            t.localPosition = translation.ToUnity();
            t.localRotation = rotation.ToUnity();
            t.localScale = scale.ToUnity();
        }
    }
}