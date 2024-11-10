using Ara3D.IfcLoader;
using Ara3D.Utils;
using Plato.DoublePrecision;
using Plato.Geometry.Graphics;
using Plato.Geometry.Memory;
using Plato.Geometry.Scenes;

namespace Plato.Geometry.Ifc
{
    public static class IfcConverters
    {
        public static IScene ToScene(this IfcFile file)
        {
            var scene = new Scene();
            foreach (var g in file.Model.GetGeometries())
            {
                var node = new SceneNode()
                {
                    Name = $"Geometry {g.Id}"
                };

                foreach (var m in g.GetMeshes())
                {
                    node.AddMesh(m.ToTriangleMesh(), m.GetTransform(), m.GetMaterial(), $"Mesh {m.Id}");
                }

                scene.Root.Children.Add(node);
            }
            scene.Root.Name = file.FilePath.GetFileName();
            scene.Root.Transform = Extensions2.YUpToZUp;
            return scene;
        }

        // Copies the data from the C++ layer into arrays.
        public static TriangleMesh3D ToTriangleMesh(this IfcMesh m)
        {
            var vertexBuffer = m.Vertices.ToTemporaryBuffer<IfcVertex>(m.NumVertices);
            var vertices = vertexBuffer.Clone().ToIArray().Map(v => new Vector3D(v.PX, v.PY, v.PZ));
            var indexBuffer = m.Indices.ToTemporaryBuffer<Integer>(m.NumIndices);
            var indices = indexBuffer.Clone().ToIArray();
            return new TriangleMesh3D(vertices, indices);
        }

        public static Material GetMaterial(this IfcMesh m)
            => m.GetColor();
        
        public static unsafe Color GetColor(this IfcMesh m)
            => *(Color*)m.Color.ToPointer();

        public static unsafe Matrix4x4 GetMatrix(this IfcMesh m)
            => *(Matrix4x4*)m.Transform.ToPointer();

        public static ITransform3D GetTransform(this IfcMesh m)
            => m.GetMatrix().Transpose;
    }
}
