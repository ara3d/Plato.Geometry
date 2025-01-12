using System.Linq;
using Assimp;
using Plato.SinglePrecision;
using AVector2D = Assimp.Vector2D;
using PVector2D = Plato.SinglePrecision.Vector2D;
using AVector3D = Assimp.Vector3D;
using PVector3D = Plato.SinglePrecision.Vector3D;
using AMatrix4x4 = Assimp.Matrix4x4;
using PMatrix4x4 = Plato.SinglePrecision.Matrix4x4;
using PScene = Plato.Geometry.Scenes.Scene;
using AScene = Assimp.Scene;
using AMaterial = Assimp.Material;
using PMaterial = Plato.Geometry.Graphics.Material;
using System;
using Plato.Geometry.Scenes;

namespace Plato.Geometry.Assimp
{
    public static class AssimpExtensions
    {
        public static PVector2D ToPlato(this AVector2D v)
            => (v.X, v.Y);

        public static PVector3D ToPlato(this AVector3D v)
            => (v.X, v.Y, v.Z);

        public static Color ToPlato(this Color3D v)
            => (v.R, v.G, v.B, 1.0);

        public static Color ToPlato(this Color4D v)
            => (v.R, v.G, v.B, v.A);

        public static PMatrix4x4 ToPlato(this AMatrix4x4 m)
            => new PMatrix4x4(
                (m.A1, m.A2, m.A3, m.A4),
                (m.B1, m.B2, m.B3, m.B4),
                (m.C1, m.C2, m.C3, m.C4),
                (m.D1, m.D2, m.D3, m.D4));

        public static bool IsTriangular(this Mesh mesh)
            => mesh.Faces.All(f => f.IndexCount == 3);

        public static PScene ToPlato(this AScene scene)
        {
            var r = new PScene();
            var mats = scene.Materials.Select(m => m.ToPlato()).ToList();
            var meshes = scene.Meshes.Select(m => m.ToPlato()).ToList();
            var nodes = scene.GetNodes().ToList();

            foreach (var n in nodes)
            {
                if (n.MeshIndex < 0 || n.MeshIndex >= meshes.Count)
                    throw new Exception($"The mesh index {n.MeshIndex} is out of range");
                var t = n.Transform.ToPlato();
                var i = n.MeshIndex;
                if (i >= 0)
                {
                    var mesh = meshes[i];
                    var matIndex = scene.Meshes[i].MaterialIndex;
                    var mat = matIndex >= 0 ? mats[matIndex] : null;
                    r.Root.AddMesh(mesh, t, mat);
                }
            }

            return r;
        }

        public static PMaterial ToPlato(this AMaterial m)
        {
            var r = new PMaterial(m.ColorDiffuse.ToPlato());
            // TODO: spend more time looking at transmitting all of the proper characteristics 
            if (m.HasShininess)
                r.Metallic = m.Shininess;
            return r;
        }

        public static TriangleMesh3D ToPlato(this Mesh mesh)
        {
            // The Assimp mesh must be triangulated
            if (mesh.FaceCount == 0)
                return TriangleMesh3D.Default;


            foreach (var f in mesh.Faces)
            {
                if (f.IndexCount != 3)
                    throw new Exception($"Each face of the assimp mesh must have 3 corners .but found one with {f.IndexCount}");
            }

            var testIndices = mesh.GetIndices();
            if (testIndices.Length % 3 != 0)
                throw new Exception($"The mesh index buffer length {testIndices.Length} is not divisible by 3");

            var verts = mesh.Vertices.Select(ToPlato).ToList().ToIArray();
            var indices = mesh.GetIndices().Select(x => (Integer)x).ToList().ToIArray();

            /*
             * NOTE: there could be all sorts of fun things.
             *
            if (mesh.HasTangentBasis)
                bldr.Add(mesh.BiTangents.ToIArray().Select(ToMath3D).ToVertexBitangentAttribute());

            if (mesh.HasTangentBasis)
                bldr.Add(mesh.Tangents.ToIArray().Select(x => ToMath3D(x).ToVector4()).ToVertexTangentAttribute());

            if (mesh.HasNormals)
                bldr.Add(mesh.Normals.ToIArray().Select(ToMath3D).ToVertexNormalAttribute());

            for (var i = 0; i < mesh.TextureCoordinateChannelCount; ++i)
            {
                var uvChannel = mesh.TextureCoordinateChannels[i];
                bldr.Add(uvChannel.ToIArray().Select(ToMath3D).ToVertexUvwAttribute(i));
            }

            for (var i = 0; i < mesh.VertexColorChannelCount; ++i)
            {
                var vcChannel = mesh.VertexColorChannels[i];
                bldr.Add(vcChannel.ToIArray().Select(ToMath3D).ToVertexColorAttribute(i));
            }
            */

            return new TriangleMesh3D(verts, indices);
        }
    }
}
