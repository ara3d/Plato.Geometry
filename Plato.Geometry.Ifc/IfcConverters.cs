using System;
using Ara3D.IfcLoader;
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
            var sb = new SceneBuilder();
            foreach (var g in file.Model.GetGeometries())
            {
                foreach (var m in g.GetMeshes())
                {
                    sb.AddMesh(m.ToTriangleMesh(), m.GetTransform(), m.GetMaterial());
                }
            }
            return sb.ToScene();
        }

        // Copies the data from the C++ layer into arrays.
        public static TriangleMesh ToTriangleMesh(this IfcMesh m)
        {
            var vertexBuffer = m.Vertices.ToTemporaryBuffer<IfcVertex>(m.NumVertices);
            var vertices = vertexBuffer.Clone().ToIArray().Map(v => new Vector3D(v.PX, v.PY, v.PZ));
            var indexBuffer = m.Indices.ToTemporaryBuffer<Integer>(m.NumIndices);
            var indices = indexBuffer.Clone().ToIArray();
            return new TriangleMesh(vertices, indices);
        }

        public static Material GetMaterial(this IfcMesh m)
            => m.GetColor();
        
        public static unsafe Color GetColor(this IfcMesh m)
            => *(Color*)m.Color.ToPointer();

        public static unsafe Matrix4x4 GetMatrix(this IfcMesh m)
            => *(Matrix4x4*)m.Transform.ToPointer();

        public static ITransform3D GetTransform(this IfcMesh m)
            => new MatrixTransform(m.GetMatrix().Transpose());
    }
}
