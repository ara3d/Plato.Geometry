using System.Collections.Generic;
using Plato.SinglePrecision;
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

        // TODO: I am going to need to replace this with a property database for performance reasons.
        // This is effectively going to allow us to implement efficient property databases like we are doing for IFC
        // So that we can have millions of objects each with hundreds of properties and still be able to store and query them efficiently
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
        public IEnumerable<KeyValuePair<string, object>> GetProps() => Properties;

        public ITransform3D Transform { get; set; } = IdentityTransform3D.Default;
        public readonly List<ISceneObject> Objects = new List<ISceneObject>();
        IReadOnlyList<ISceneObject> ISceneNode.Objects => Objects;
        public readonly List<ISceneNode> Children = new List<ISceneNode>();
        IReadOnlyList<ISceneNode> ISceneNode.Children => Children;
        public object GetProp(string key) => Properties.TryGetValue(key, out var r) ? r : null;
    }

    public class SceneMesh : ISceneMesh
    {
        public SceneMesh(Material material, TriangleMesh3D mesh)
        {
            Material = material;
            Mesh = mesh;
        }
        public Material Material { get; set; }
        public TriangleMesh3D Mesh { get; set; }
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
