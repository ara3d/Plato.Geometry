using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Plato.DoublePrecision;
using Plato.Geometry.Graphics;

namespace Plato.Geometry.Scenes
{
    public static class SceneExtensions
    {
        public static IEnumerable<ISceneMesh> GetMeshObjects(this ISceneNode node)
            => node.Objects.OfType<ISceneMesh>();

        public static Material DefaultLineMaterial = Colors.Blue;
        public static Material DefaultMeshMaterial = Colors.Gray;

        public static SceneLine AddLines(this SceneNode self, IReadOnlyList<Vector3D> points, bool closed, Material material = null, double width = 0.01, string name = null)
        {
            var obj = new SceneLine(material ?? DefaultLineMaterial, width, closed, points);
            var id = Guid.NewGuid().ToString();
            var node = new SceneNode
            {
                Id = id,
                Name = name ?? $"Line {id}"
            };
            node.Objects.Add(obj);
            self.Children.Add(node);
            return obj;
        }

        public static SceneMesh AddMesh(this SceneNode self, TriangleMesh mesh, ITransform3D transform, Material material = null, string name = null)
        {
            var obj = new SceneMesh(material ?? DefaultMeshMaterial, mesh);
            var id = Guid.NewGuid().ToString();
            var node = new SceneNode
            {
                Id = id,
                Name = name ?? $"Mesh {id}",
                Transform = transform ?? NullTransform.Instance,
            };
            node.Objects.Add(obj);
            self.Children.Add(node);
            return obj;
        }

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

        public static void AddMesh(ITriangleMesh mesh, ITransform3D transform, List<Vector3D> points, List<Integer> indices)
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
    }
}