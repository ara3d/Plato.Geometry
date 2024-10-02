using System.Collections.Generic;
using System.IO;
using Plato.DoublePrecision;
using Plato.Geometry.Graphics;

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
        public static void WritePly(this TriangleMesh mesh, string filePath)
            => File.WriteAllLines(filePath, PlyStrings(mesh));

        public static IEnumerable<string> PlyStrings(TriangleMesh g, IArray<Color32> colors = null)
        {
            var vertices = g.Vertices;
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
                yield return "property uint8 red";
                yield return "property uint8 green";
                yield return "property uint8 blue";
            }

            yield return "element face " + g.NumFaces;
            yield return "property list uint8 int32 vertex_index";
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
            for (var i = 0; i < g.NumFaces; i++)
            {
                yield return $"3 {indices[index++]} {indices[index++]} {indices[index++]}";
            }
        }
    }
}