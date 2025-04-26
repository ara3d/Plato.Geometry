using System.Collections.Generic;
using System.IO;

namespace Plato.Geometry.IO
{
    /// <summary>
    /// A very simple .ply file exporter
    /// https://paulbourke.net/dataformats/ply/
    /// https://en.wikipedia.org/wiki/PLY_(file_format)
    /// https://gamma.cs.unc.edu/POWERPLANT/papers/ply.pdf
    /// </summary> 
    public static class PlyExporter
    {
        public static void WritePly(this TriangleMesh3D mesh, string filePath)
            => File.WriteAllLines(filePath, PlyStrings(mesh));

        public static IEnumerable<string> PlyStrings(TriangleMesh3D g, IArray<ByteRGBA> colors = null)
        {
            var vertices = g.Points;
            var indices = g.Indices;
            
            //Write the header
            yield return "ply";
            yield return "format ascii 1.0";
            yield return "element vertex " + vertices.Count + "";
            yield return "property float x";
            yield return "property float y";
            yield return "property float z";

            if (colors != null)
            {
                yield return "property uchar red";
                yield return "property uchar green";
                yield return "property uchar blue";
            }

            yield return "element face " + g.NumPrimitives;
            yield return "property list uchar int vertex_index";
            yield return "end_header";

            // Write the vertices
            if (colors != null)
            {
                for (var i = 0; i < vertices.Count; i++)
                {
                    var v = vertices[i];
                    var c = colors[i];

                    yield return
                        $"{v.X} {v.Y} {v.Z} {c.R} {c.G} {c.B}";
                }
            }
            else
            {
                for (var i = 0; i < vertices.Count; i++)
                {
                    var v = vertices[i];
                    yield return
                        $"{v.X} {v.Y} {v.Z}";
                }
            }

            // Write the face indices
            var index = 0;
            for (var i = 0; i < g.NumPrimitives; i++)
            {
                yield return $"3 {indices[index++]} {indices[index++]} {indices[index++]}";
            }
        }
    }
}