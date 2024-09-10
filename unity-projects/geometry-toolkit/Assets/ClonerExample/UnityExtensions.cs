using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Vector3 = UnityEngine.Vector3;

namespace Assets.ClonerExample
{
    public static class UnityExtensions
    {
        // TODO: this could cause problems if the wrong type is previous. 
        public static T GetPreviousComponent<T>(this MonoBehaviour self)
        {
            var comps = self.gameObject.GetComponents<T>();
            var r = default(T);
            foreach (var comp in comps)
            {
                if (comp is MonoBehaviour mb)
                {
                    if (mb == self)
                        return r;
                    if (!mb.enabled)
                        continue;
                    if (mb is T t)
                        r = t;
                }
            }           
            return r;
        }

        public static Mesh Transform(this Mesh mesh, Matrix4x4 m)
        {
            var r = new Mesh()
            {
                vertices = mesh.vertices.Select(m.MultiplyPoint).ToArray(),
                triangles = mesh.triangles,
                uv = mesh.uv,
            };
            r.RecalculateNormals();
            r.RecalculateBounds();
            return r;
        }

        public static Matrix4x4 GetAlignmentMatrix(Bounds bounds, Vector3 p0, Vector3 p1, float xyScale)
        {
            var v = p1 - p0;
            var rotation = UnityEngine.Quaternion.FromToRotation(Vector3.up, v);
            var height = bounds.size.y;
            var bottom = bounds.min.y;
            var offset = Vector3.up * bottom;
            var scale = new Vector3(xyScale, v.magnitude / height, xyScale);

            /*
            var offsetMesh = ApplyOffset ? Mesh.Transform(Matrix4x4.Translate(-offset)) : Mesh;
            var scaledMesh = ApplyScale ? offsetMesh.Transform(Matrix4x4.Scale(scale)) : offsetMesh; 
            var rotatatedMesh = ApplyRotate ? scaledMesh.Transform(Matrix4x4.Rotate(rotation)) : scaledMesh;
            Graphics.DrawMesh(rotatatedMesh, Matrix4x4.Translate(p0), Material, 0);
            */

            var t1 = Matrix4x4.Translate(-offset);
            var s = Matrix4x4.Scale(scale);
            var r = Matrix4x4.Rotate(rotation);
            var t2 = Matrix4x4.Translate(p0);
            return t2 * r * s * t1 * Matrix4x4.identity;
        }

        public static Ara3D.Mathematics.Vector3 ToAra3D(this float3 v)
            => (v.x, v.y, v.z);
    }
}