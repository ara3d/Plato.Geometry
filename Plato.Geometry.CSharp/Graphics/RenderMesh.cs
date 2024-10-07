using System;
using Plato.DoublePrecision;
using Plato.Geometry.Memory;

namespace Plato.Geometry.Graphics
{
    /// <summary>
    /// Allocates a mesh appropriate for usage with low-level graphics APIs.
    /// Memory if
    /// </summary>
    public class RenderMesh : IDisposable
    {
        public readonly IBuffer<RenderVertex> Vertices;
        public readonly IBuffer<Integer> Indices;

        public RenderMesh(IBuffer<RenderVertex> vertices, IBuffer<Integer> indices)
        {
            Vertices = vertices;
            Indices = indices;
        }

        public void Dispose()
        {
            Vertices.Dispose();
            Indices.Dispose();
        }

        public static RenderMesh Empty
            => new RenderMesh(Buffer<RenderVertex>.Empty, Buffer<Integer>.Empty);

        public static RenderMesh Create(
            IArray<Vector3D> vertices, 
            IArray<Integer> indices = null, 
            IArray<Vector3D> normals = null, 
            IArray<Vector2D> uvs = null, 
            IArray<Color32> colors = null)
        {
            var vertexCount = vertices.Count;
            if (vertexCount == 0)
                return Empty;

            var indexBuffer = indices?.ToBuffer() ?? vertexCount.Range.ToBuffer();

            normals = normals ?? Vector3D.Default.Repeat(vertexCount);
            if (normals.Count != vertexCount)
                throw new InvalidOperationException($"Normals count {normals.Count} must match vertices count {vertexCount}.");

            uvs = uvs ?? Vector2D.Default.Repeat(vertexCount);
            if (uvs.Count != vertexCount)
                throw new InvalidOperationException($"UVs count {uvs.Count} must match vertices count {vertexCount}.");

            colors = colors ?? Color32.Default.Repeat(vertexCount);
            if (colors.Count != vertexCount)
                throw new InvalidOperationException($"Colors count {colors.Count} must match vertices count {vertexCount}.");

            var renderVertices = new MemoryBlockBuffer<RenderVertex>(vertexCount);
            for (var i=0; i < vertexCount; i++)
            {
                renderVertices[i] =
                    new RenderVertex(vertices[i], normals[i], uvs[i], colors[i]);
            }

            return new RenderMesh(renderVertices, indexBuffer);
        }
    }
}
