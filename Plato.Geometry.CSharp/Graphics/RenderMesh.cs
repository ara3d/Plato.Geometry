using System;
using System.Collections.Generic;
using System.Linq;
using Plato.DoublePrecision;
using Plato.Geometry.Memory;

namespace Plato.Geometry.Graphics
{
    public class RenderMesh : IDisposable
    {
        public PinnedArray<RenderVertex> Vertices;
        public PinnedArray<int> Indices;

        public RenderMesh(IEnumerable<RenderVertex> vertices, IEnumerable<int> indices)
        {
            Vertices = new PinnedArray<RenderVertex>(vertices.ToArray());
            Indices = new PinnedArray<int>(indices.ToArray());
        }

        public void Dispose()
        {
            Vertices.Dispose();
            Indices.Dispose();
        }
    }
}
