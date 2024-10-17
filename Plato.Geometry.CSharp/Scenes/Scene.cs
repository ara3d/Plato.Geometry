using System.Collections.Generic;
using Plato.DoublePrecision;
using Plato.Geometry.Graphics;

namespace Plato.Geometry.Scenes
{
    public class Scene : IScene
    {
        public SceneNode Root { get; }
        ISceneNode IScene.Root => Root;
        public Scene(SceneNode root = null) => Root = root ?? new SceneNode();
    }

    public class SceneNode : ISceneNode
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ITransform3D Transform { get; set; } = NullTransform.Instance;
        public readonly List<ISceneObject> Objects = new List<ISceneObject>();
        IReadOnlyList<ISceneObject> ISceneNode.Objects => Objects;
        public readonly List<ISceneNode> Children = new List<ISceneNode>();
        IReadOnlyList<ISceneNode> ISceneNode.Children => Children;
    }

    public class SceneMesh : ISceneMesh
    {
        public SceneMesh(Material material, TriangleMesh mesh)
        {
            Material = material;
            Mesh = mesh;
        }
        public Material Material { get; set; }
        public TriangleMesh Mesh { get; set; }
    }

    public class SceneLine : ISceneLine
    {
        public SceneLine(Material material, double width, bool closed, IReadOnlyList<Vector3D> points)
        {
            Material = material;
            Width = width;
            Closed = closed;
            Points = points;
        }
        public Material Material { get; set; }
        public double Width { get; set; }
        public bool Closed { get; set; }
        public IReadOnlyList<Vector3D> Points { get; }
    }

}
