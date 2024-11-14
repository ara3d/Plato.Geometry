using System;
using System.Collections.Generic;
using System.Linq;
using Plato.DoublePrecision;
using Plato.Geometry.Graphics;

namespace Plato.Geometry.Scenes
{
    public static class SceneExtensions
    {
        public static readonly Material DefaultLineMaterial = Colors.Blue;
        public static readonly Material DefaultMeshMaterial = Colors.Gray;
        
        public static SceneNode AddNode(this SceneNode self, string name = null)
        {
            var id = Guid.NewGuid().ToString();
            var node = new SceneNode
            {
                Id = id,
                Name = name ?? $"Node {id}"
            };
            self.Children.Add(node);
            return node;
        }

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

        public static SceneMesh AddMesh(this SceneNode self, TriangleMesh3D mesh, ITransform3D transform = null, Material material = null, string name = null)
        {
            var obj = new SceneMesh(material ?? DefaultMeshMaterial, mesh);
            var id = Guid.NewGuid().ToString();
            var node = new SceneNode
            {
                Id = id,
                Name = name ?? $"Mesh {id}",
                Transform = transform ?? IdentityTransform3D.Default,
            };
            node.Objects.Add(obj);
            self.Children.Add(node);
            return obj;
        }

        public static TriangleMesh3D ToTriangleMesh(this IScene scene)
        {
            var points = new List<Vector3D>();
            var indices = new List<Integer>();
            AddMeshes(scene.Root, points, indices);
            return new TriangleMesh3D(new ListArray<Vector3D>(points), new ListArray<Integer>(indices));
        }

        public static void AddMeshes(ISceneNode node, List<Vector3D> points, List<Integer> indices)
        {
            foreach (var m in node.Objects.OfType<ISceneMesh>())
                AddMesh(m.Mesh, node.Transform, points, indices);
            foreach (var c in node.Children)
                AddMeshes(c, points, indices);
        }

        // TODO: this seems to be part of a MeshBuilder

        public static void AddMesh(ITriangleMesh3D mesh, ITransform3D transform, List<Vector3D> points, List<Integer> indices)
        {
            var offset = points.Count;
            foreach (var p in mesh.Points)
                points.Add(transform.Transform(p));
            foreach (var i in mesh.Indices)
                indices.Add(i + offset);
        }

        public static IEnumerable<ISceneNode> GetSelfAndDescendants(this ISceneNode node)
        {
            if (node == null)
                yield break;
            yield return node;
            foreach (var c in node.Children)
                foreach (var d in c.GetSelfAndDescendants())
                    yield return d;
        }

        public static IEnumerable<ISceneNode> GetNodes(this IScene scene)
            => scene.Root.GetSelfAndDescendants();

        // NOTE: this doesn't seem to work 
        public static ISceneNode Filter(this ISceneNode node, Func<ISceneNode, bool> filter)
        {
            var children = node.Children.Select(c => c.Filter(filter)).Where(c => c != null).ToList();
            // NOTE: this is not very efficient. But it could work. 
            var r = new SceneNode()
            {
                Id = node.Id,
                Name = node.Name,
                Properties = node.GetProps().ToDictionary(kv => kv.Key, kv => kv.Value),
            };
            r.Objects.AddRange(node.Objects);
            r.Children.AddRange(children);
            if (!filter(r))
                return null;
            return r;
        }

        public static IEnumerable<ISceneNode> GetNodesWithMeshes(this IScene scene)
            => scene.GetNodes().Where(n => n.GetMeshes().Any());

        public static IEnumerable<ISceneMesh> GetMeshes(this ISceneNode node)
            => node.Objects.OfType<ISceneMesh>();
    }
}