using System.Collections.Generic;
using System.Linq;
using Plato.DoublePrecision;

namespace Plato.Geometry.Scenes
{
    public static class SceneExtensions
    {
        public static TriangleMesh ToTriangleMesh(this IScene scene)
        {
            var points = new List<Vector3D>();
            var indices = new List<Integer>();
            AddMeshes(scene.Root, points, indices);
            return new TriangleMesh(new ListArray<Vector3D>(points), new ListArray<Integer>(indices));
        }

        public static void AddMeshes(ISceneNode node, List<Vector3D> points, List<Integer> indices)
        {
            foreach (var m in node.Objects.OfType<ISceneMesh>())
                AddMesh(m.Mesh, node.Transform, points, indices);
            foreach (var c in node.Children)
                AddMeshes(c, points, indices);
        }

        // TODO: this seems to be part of a MeshBuilder

        public static void AddMesh(ITriangleMesh mesh, Transform3D transform, List<Vector3D> points, List<Integer> indices)
        {
            var offset = points.Count;
            foreach (var p in mesh.Points)
                points.Add(transform.TransformPoint(p));
            foreach (var i in mesh.Indices)
                indices.Add(i + offset);
        }

        // TODO: a lot of this stuff needs to find its way back into the Plato library. 

        public static Vector3D TransformPoint(this Transform3D transform, Vector3D point)
            => transform.TransformVector(point) + transform.Translation;

        public static Vector3D TransformVector(this Transform3D transform, Vector3D vector)
            => transform.Rotation.Transform(vector * transform.Scale);

        public static Vector3D Transform(this Rotation3D rotation, Vector3D v)
            => rotation.Quaternion.Transform(v);

        public static Vector3D Transform(this Quaternion rotation, Vector3D v)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var wx2 = rotation.W * x2;
            var wy2 = rotation.W * y2;
            var wz2 = rotation.W * z2;
            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var xz2 = rotation.X * z2;
            var yy2 = rotation.Y * y2;
            var yz2 = rotation.Y * z2;
            var zz2 = rotation.Z * z2;

            return new Vector3D(
                v.X * (1.0f - yy2 - zz2) + v.Y * (xy2 - wz2) + v.Z * (xz2 + wy2),
                v.X * (xy2 + wz2) + v.Y * (1.0f - xx2 - zz2) + v.Z * (yz2 - wx2),
                v.X * (xz2 - wy2) + v.Y * (yz2 + wx2) + v.Z * (1.0f - xx2 - yy2));
        }
    }
}