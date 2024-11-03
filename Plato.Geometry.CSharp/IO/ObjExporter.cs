using System.Collections.Generic;
using System.IO;
using Plato.DoublePrecision;

namespace Plato.Geometry.IO
{
    /// <summary>
    /// This is a simple ObjExporter
    /// https://en.wikipedia.org/wiki/Wavefront_.obj_file
    /// https://paulbourke.net/dataformats/obj/
    /// https://fegemo.github.io/cefet-cg/attachments/obj-spec.pdf
    /// </summary>
    public static class ObjExporter
    {
        public static IEnumerable<string> ObjLines(TriangleMesh3D mesh, IArray<Vector2D> uvs = null)
        {
            // Write the vertices 
            foreach (var v in mesh.Vertices)
                yield return $"v {v.X} {v.Y} {v.Z}";

            if (uvs != null)
            {
                for (var v = 0; v < uvs.Count; v++)
                    yield return $"vt {uvs[v].X} {uvs[v].Y}";
            }

            foreach (var f in mesh.AllFaceIndices)
            {
                var a = f[0] + 1;
                var b = f[1] + 1;
                var c = f[2] + 1;
                if (uvs == null)
                    yield return $"f {a} {b} {c}";
                else
                    yield return $"f {a}/{a} {b}/{b} {c}/{c}";    
            }
        }

        public static void WriteObj(this TriangleMesh3D mesh, string filePath)
            => File.WriteAllLines(filePath, ObjLines(mesh));
    }
}
