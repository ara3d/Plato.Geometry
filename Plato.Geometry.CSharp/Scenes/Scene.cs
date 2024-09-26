using System.Collections.Generic;
using Plato.DoublePrecision;
using Plato.Geometry.Graphics;

namespace Plato.Geometry.Scenes
{
    public interface IScene
    {
        ISceneObject Root { get; }
    }

    public interface ISceneObject
    {
        string Name { get; }
        Material Material { get; }
        Transform3D Transform { get; }
        IReadOnlyList<ISceneObject> Children { get; }
    }

    public class LineObject : SceneObject
    {
        public double Width;
        public bool Closed;
        public List<Vector3D> Points = new List<Vector3D>();
    }

    public class MeshObject : SceneObject
    {
        public List<Vector3D> Vertices = new List<Vector3D>();
        public List<int> Indices = new List<int>();
    }

    public class SceneObject : ISceneObject
    {
        public string Name { get; set; }
        public Material Material { get; set; }
        public Transform3D Transform { get; set; } = new Transform3D(Vector3D.Default, Rotation3D.Default, (1, 1, 1));
        public List<ISceneObject> Children = new List<ISceneObject>();
        IReadOnlyList<ISceneObject> ISceneObject.Children => Children;
    }

    public class Scene : IScene
    {
        public SceneObject Root { get; } = new SceneObject();
        ISceneObject IScene.Root => Root;
    }
}
