using Plato.SinglePrecision;
using UnityEngine;
using Quaternion = Plato.SinglePrecision.Quaternion;
using UQuaternion = UnityEngine.Quaternion;

namespace Plato.Geometry.Unity
{
    public static class UnityAdapters
    {
        public static Vector4 ToUnity(this Vector4D v) => new Vector4(v.X, v.Y, v.Z, v.W);
        public static Vector3 ToUnity(this Vector3D v) => new Vector3(v.X, v.Y, v.Z);
        public static Vector2 ToUnity(this Vector2D v) => new Vector2(v.X, v.Y);

        public static Vector3 ToUnity(this Size3D v) => new Vector3(v.Width, v.Height, v.Depth);
        public static Vector2 ToUnity(this Size2D v) => new Vector2(v.Width, v.Height);

        public static Vector3 ToUnity(this Point3D v) => new Vector3(v.X, v.Y, v.Z);
        public static Vector2 ToUnity(this Point2D v) => new Vector2(v.X, v.Y);

        public static UQuaternion ToUnity(this Quaternion q) => new UQuaternion(q.X, q.Y, q.Z, q.W);

        public static Matrix4x4 ToUnity(this Matrix3D m) => new Matrix4x4(
            new Vector4(m.M11, m.M12, m.M13, m.M14),
            new Vector4(m.M21, m.M22, m.M23, m.M24),
            new Vector4(m.M31, m.M32, m.M33, m.M34),
            new Vector4(m.M41, m.M42, m.M43, m.M44));

        public static Vector2D ToPlato(this Vector2 v) => (v.x, v.y);
        public static Vector3D ToPlato(this Vector3 v) => (v.x, v.y, v.z);
        public static Vector4D ToPlato(this Vector4 v) => (v.x, v.y, v.z, v.w);

        //public static Matrix3D ToPlato(this Matrix4x4 m) =>  ((m.M))

        public static Vector3[] ToUnityPositions(this Array<SimpleVertex> verts)
        {
            var r = new Vector3[verts.Count];
            for (var i = 0; i < verts.Count; ++i)
                r[i] = verts.At(i).Position.ToUnity();
            return r;
        }

        public static int[] ToUnityIndices(this Array<>)

        public static Mesh ToUnity(this TriMesh mesh)
        {
            var unityMesh = new Mesh();
            unityMesh.vertices = mesh.Vertices.ToUnityPositions();
            unityMesh.triangles = mesh.Faces.ToUnityIndices();
            unityMesh.normals = mesh.Normals.ToUnity();
            unityMesh.uv = mesh.UVs.ToUnity();
            return unityMesh;
        }
    }
}