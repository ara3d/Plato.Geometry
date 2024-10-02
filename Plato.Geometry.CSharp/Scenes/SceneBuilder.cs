using Plato.DoublePrecision;
using Plato.Geometry.Graphics;
using System.Collections.Generic;

namespace Plato.Geometry.Scenes
{
    public class SceneBuilder
    {
        private readonly Scene _scene = new Scene();
        private bool _frozen;
        private int _id;
        public Material DefaultLineMaterial = Colors.Blue;
        public Material DefaultMeshMaterial = Colors.Gray;

        public IScene ToScene()
        {
            _frozen = true;
            return _scene;
        }

        public void CheckFrozen()
        {
            if (_frozen)
                throw new System.InvalidOperationException("Scene is frozen");
        }

        public SceneLine AddLines(IReadOnlyList<Vector3D> points, bool closed, Material material = null, double width = 0.01, string name = null)
        {
            CheckFrozen();
            var obj = new SceneLine(material ?? DefaultLineMaterial, width, closed, points);
            var node = new SceneNode
            {
                Name = name ?? $"Line {_id++}",
            };
            node.Objects.Add(obj);
            _scene.Root.Children.Add(node);
            return obj;
        }

        public SceneMesh AddMesh(ITriangleMesh mesh, ITransform3D transform, Material material = null, string name = null)
        {
            CheckFrozen();
            var obj = new SceneMesh(material ?? DefaultMeshMaterial, mesh);
            var node = new SceneNode
            {
                Name = name ?? $"Mesh {_id++}",
                Transform = transform ?? NullTransform.Instance,
            };
            node.Objects.Add(obj);
            _scene.Root.Children.Add(node);
            return obj;
        }
    }
}