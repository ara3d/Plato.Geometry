using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class UtilAlignMesh : MonoBehaviour
    {
        public Mesh Mesh;
        public Material Material;
        public Transform Target;
        public bool ApplyScale = true;
        public bool ApplyRotate = true;
        public bool ApplyOffset = true;
        public float XYScale = 0.1f;

        public void Update()
        {
            var p0 = transform.position;
            var p1 = Target.position;
            /*
            var v = p1 - p0;
            var bounds = Mesh.bounds;
            var rotation = Quaternion.FromToRotation(Vector3.up, v);
            var height = bounds.size.y;
            var bottom = bounds.min.y;
            var offset = Vector3.up * bottom;
            var scale = new Vector3(XYScale, v.magnitude / height, XYScale);
            */

            /*
            var offsetMesh = ApplyOffset ? Mesh.Transform(Matrix4x4.Translate(-offset)) : Mesh;
            var scaledMesh = ApplyScale ? offsetMesh.Transform(Matrix4x4.Scale(scale)) : offsetMesh; 
            var rotatatedMesh = ApplyRotate ? scaledMesh.Transform(Matrix4x4.Rotate(rotation)) : scaledMesh;
            Graphics.DrawMesh(rotatatedMesh, Matrix4x4.Translate(p0), Material, 0);
            */

            /*
            var t1 = Matrix4x4.Translate(-offset);
            var s = Matrix4x4.Scale(scale);
            var r = Matrix4x4.Rotate(rotation);
            var t2 = Matrix4x4.Translate(p0);
            var m = t2 * r * s * t1 * Matrix4x4.identity;
            Graphics.DrawMesh(Mesh, m, Material, 0);
            */

            var m = UnityExtensions.GetAlignmentMatrix(Mesh.bounds, p0, p1, XYScale);
            Graphics.DrawMesh(Mesh, m, Material, 0);
        }
    }
}