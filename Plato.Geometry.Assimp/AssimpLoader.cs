using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assimp;

namespace Plato.Geometry.Assimp
{
    public static class AssimpLoader
    {
        public static AssimpContext Context = new AssimpContext();

        public class AssimpNode
        {
            public int MeshIndex { get; }
            public Matrix4x4 Transform { get; }
            public AssimpNode(int index, Matrix4x4 transform)
                => (MeshIndex, Transform) = (index, transform);
        }

        public static IEnumerable<AssimpNode> GetNodes(this Scene scene)
            => scene == null || scene.RootNode == null
                ? Enumerable.Empty<AssimpNode>()
                : GetNodes(scene, scene.RootNode, scene.RootNode.Transform);

        public static IEnumerable<AssimpNode> GetNodes(this Scene scene, Node node, Matrix4x4 transform)
            => node.MeshIndices.Select(idx => new AssimpNode(idx, node.Transform))
            .Concat(node.Children.SelectMany(c => GetNodes(scene, c, transform * c.Transform)));

        public static Scene Load(string filePath, bool triangulate = true)
            => Context.ImportFile(filePath, triangulate ? PostProcessSteps.Triangulate : PostProcessSteps.None);

        public static bool CanLoad(string filePath)
            => Context.IsImportFormatSupported(Path.GetExtension(filePath));
    }
}
