using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Ara3D.Buffers;
using Ara3D.Serialization.G3D;
using Ara3D.Serialization.VIM;

namespace Plato.Geometry.VIM
{
    public record Material(Color Color, Number Glossiness, Number Smoothness);
    public record Node(Matrix4x4 Matrix, TriangleMesh3D Mesh, Material Material, int ElementId);
   
    public class VimGeometryAdapter
    {
        public readonly G3D G3D;
        public IBuffer<Point3D> Points;
        public IBuffer<Integer3> Indices;
        public IBuffer<Matrix4x4> Transforms;
        
        public record SubMesh(int MaterialId, TriangleMesh3D Mesh);
        
        public List<List<SubMesh>> SubMeshes = new();
        public List<Node> Nodes = new();
        public List<Material> Materials = new();

        public TriangleMesh3D CreateMesh(G3dSubMesh mesh)
        {
            var points = Points;
            var indices = Indices.Slice(mesh.IndexOffset, mesh.IndexCount);
            return new(points.ToIArray(), indices.ToIArray());
        }

        public SubMesh CreateSubMesh(G3dSubMesh subMesh)
            => new(subMesh.MaterialIndex, CreateMesh(subMesh));

        public Material CreateMaterial(G3dMaterial material)
        {
            var color = new Color(material.Color.X, material.Color.Y, material.Color.Z, material.Color.W);
            var glossiness = new Number(material.Glossiness);
            var shininess = new Number(material.Smoothness);
            return new Material(color, glossiness, shininess);
        }

        public List<SubMesh> CreateSubMeshes(G3dMesh m)
            => m.Submeshes.Select(CreateSubMesh).ToList();

        public VimGeometryAdapter(SerializableDocument doc)
        {
            G3D = doc.Geometry;

            Points = G3D.Vertices.ToBuffer().Reinterpret<Point3D>();
            Indices = G3D.Indices.ToBuffer().Reinterpret<Integer3>();
            Transforms = G3D.InstanceTransforms.ToBuffer().Reinterpret<Matrix4x4>();

            Materials = G3D.Materials.Select(CreateMaterial).ToList();
            SubMeshes = G3D.Meshes.Select(CreateSubMeshes).ToList();

            if (G3D.InstanceMeshes.Length != G3D.InstanceTransforms.Length)
                throw new Exception("Instance meshes and transforms do not match");

            for (int i = 0; i < G3D.InstanceMeshes.Length; i++)
            {
                var meshIndex = G3D.InstanceMeshes[i];
                if (meshIndex < 0)
                    continue;
                if (meshIndex >= SubMeshes.Count)
                    throw new Exception($"Mesh index {meshIndex} is out of range");

                foreach (var subMesh in SubMeshes[meshIndex])
                {
                    var mesh = subMesh.Mesh;
                    var material = subMesh.MaterialId >= 0 ? Materials[subMesh.MaterialId] : null;
                    var node = new Node(Transforms[i], mesh, material, i);
                    Nodes.Add(node);
                }
            }
        }
    }

    public static class Extensions
    {
        public static IArray<T> ToIArray<T>(this IBuffer<T> buffer)
            => new Array<T>(buffer.Count, i => buffer[i]);
    }
}
