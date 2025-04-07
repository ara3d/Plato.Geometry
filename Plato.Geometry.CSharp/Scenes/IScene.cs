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
        IArray<ISceneObject> Objects { get; }
        IArray<ISceneNode> Children { get; }
    }

    public interface ISceneObject
    {
    }

    public interface ISceneLine : ISceneObject
    {
        double Width { get; }
        bool Closed { get; }
        Material Material { get; }
        IReadOnlyList<Vector3> Points { get; }
    }

    public interface ISceneMesh : ISceneObject
    {
        Material Material { get; }
        TriangleMesh3D Mesh { get; }
    }

}