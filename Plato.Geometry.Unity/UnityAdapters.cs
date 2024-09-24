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

        public static Vector3 ToUnity(this Size3D v) => new Vector3((float)v.Width, (float)v.Height, (float)v.Depth);
        public static Vector2 ToUnity(this Size2D v) => new Vector2((float)v.Width, (float)v.Height);

        public static UQuaternion ToUnity(this Quaternion q) => new UQuaternion((float)q.X, (float)q.Y, (float)q.Z, (float)q.W);

        public static UMatrix4x4 ToUnity(this Matrix4x4 m) => new UMatrix4x4(
            new Vector4((float)m.M11, (float)m.M12, (float)m.M13, (float)m.M14),
            new Vector4((float)m.M21, (float)m.M22, (float)m.M23, (float)m.M24),
            new Vector4((float)m.M31, (float)m.M32, (float)m.M33, (float)m.M34),
            new Vector4((float)m.M41, (float)m.M42, (float)m.M43, (float)m.M44));

        public static Vector2D ToPlato(this Vector2 v) => (v.x, v.y);
        public static Vector3D ToPlato(this Vector3 v) => (v.x, v.y, v.z);
        public static Vector4D ToPlato(this Vector4 v) => (v.x, v.y, v.z, v.w);

        //public static Matrix3D ToPlato(this Matrix4x4 m) =>  ((m.M))

        public static Vector3[] ToUnityPositions(this Array<Vertex> verts)
        {
            var r = new Vector3[verts.Count];
            for (var i = 0; i < verts.Count; ++i)
                r[i] = verts.At(i).Position.ToUnity();
            return r;
        }

        public static int[] ToUnityIndices(this Array<Integer3> faces)
        {
            var r = new int[faces.Count * 3];
            for (var i = 0; i < faces.Count; ++i)
            {
                var f = faces.At(i);
                r[i * 3 + 0] = f.A;
                r[i * 3 + 1] = f.B;
                r[i * 3 + 2] = f.C;
            }
            return r;
        }

        public static Mesh ToUnity(this TriMesh mesh)
        {
            var unityMesh = new Mesh
            {
                vertices = mesh.Vertices.ToUnityPositions(),
                triangles = mesh.Faces.ToUnityIndices()
            };
            //unityMesh.normals = mesh.Vertices.ToUnityNormals();
            //unityMesh.uv = mesh.UVs.ToUnity();
            unityMesh.RecalculateNormals();
            return unityMesh;
        }
    }
}