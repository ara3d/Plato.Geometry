using Plato.DoublePrecision;
using Plato.Geometry.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Plato.Geometry.Scenes
{
    public class SceneBuilder
    {
        private Scene _scene = new Scene();
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

        public LineObject AddLines(IEnumerable<Vector3D> points, bool closed, Material material = null, double width = 0.01, string name = null)
        {
            CheckFrozen();
            var obj = new LineObject
            {
                Name = name ?? $"Loop_{_id++}",
                Width = width,
                Material = material ?? DefaultLineMaterial,
                Closed = closed,
                Points = points.ToList()
            };
            _scene.Root.Children.Add(obj);
            return obj;
        }
    }
}