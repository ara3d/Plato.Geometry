using Plato.DoublePrecision;
using Plato.Geometry.Graphics;
using System.Collections.Generic;

namespace Plato.Geometry.Scenes
{
    public interface IScene
    {
        ISceneNode Root { get; }
    }

    public interface ISceneNode
    {
        string Id { get; }
        string Name { get; }
        ITransform3D Transform { get; }
        IReadOnlyList<ISceneObject> Objects { get; }
        IReadOnlyList<ISceneNode> Children { get; }
        IEnumerable<KeyValuePair<string, object>> GetProps();
        object GetProp(string key);
    }

    public interface ISceneObject
    {
        Material Material { get; }
    }

    public interface ISceneLine : ISceneObject
    {
        double Width { get; }
        bool Closed { get; }
        IReadOnlyList<Vector3D> Points { get; }
    }

    public interface ISceneMesh : ISceneObject
    {
        TriangleMesh3D Mesh { get; }
    }

}