using System;
using System.Runtime.InteropServices;
using Plato.DoublePrecision;

namespace Plato.Geometry.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]    
    public struct RenderVertex
    {
        public float PX, PY, PZ;
        public float NX, NY, NZ;
        public float U, V;
        public float R;
        public float G;
        public float B;
        public float A;
    }

    public class RenderMesh
    {
        public RenderVertex[] Vertices;
        public int[] Indices;
    }

    public static class RenderMeshExtensions
    {
        public static RenderMesh ToRenderMesh(this TriangleMesh self, Color color)
        {
            var r = new RenderMesh();
            var n = self.NumFaces;
            r.Vertices = new RenderVertex[n];
            r.Indices = new int[n];
            for (var i = 0; i < self.Faces.Count; ++i)
            {
                var face = self.Faces[i];
            }

            throw new NotImplementedException();
        }
    }
}
