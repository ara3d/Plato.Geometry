using System;
using System.Collections.Generic;
using System.DoubleNumerics;
using Ara3D.Speckle.Data;
using Objects.Other;
using Plato.DoublePrecision;
using Plato.Geometry.Scenes;
using Material = Plato.Geometry.Graphics.Material;
using Quaternion = System.DoubleNumerics.Quaternion;

namespace Plato.Geometry.Speckle
{
    public static class SpeckleAdapters
    {
        public static IScene ToScene(this SpeckleObject self)
        {
        }


        public static ISceneNode ToPlato(this SpeckleObject self)
        {
            SceneNode r; 

            if (self.Mesh != null)
            {
                var mesh = self.Mesh;
                r = new MeshObject()
                {
                    Vertices = mesh.vertices.ToPlato(),
                    Indices = mesh.faces.ToPlato(),
                };
            }
            else
            {
                r = new SceneNode();
            }

            r.Name = self.Name;
            r.Material = self.Material?.ToMaterial();

            // TODO: convert transforms.
            self.Transform.Decompose(out var scale, out var rot, out var pos);
            r.Transform = new Transform3D(pos.ToPlato(), rot.ToPlato(), scale.ToPlato());

            return r;
        }

        public static Vector3D ToPlato(this Vector3 self)
        {
            return new Vector3D(self.X, self.Y, self.Z);
        }

        public static Vector3D ToPlato(this Vector4 self)
        {
            return new Vector3D(self.X, self.Y, self.Z);
        }

        public static Rotation3D ToPlato(this System.DoubleNumerics.Quaternion self)
        {
            return new Rotation3D(new Plato.DoublePrecision.Quaternion(self.X, self.Y, self.Z, self.W));
        }

        public static Material ToMaterial(this RenderMaterial self)
        {
            var r = new Material(self.diffuseColor.ToColor());
            r.Roughness = self.roughness;
            r.Metallic = self.metalness;
            return r;
        }

        public static Color ToColor(this System.Drawing.Color self)
        {
            return new Color(self.R / 255.0, self.G / 255.0, self.B / 255.0, self.A / 255.0);
        }

        public static List<Vector3D> ToPlato(this IReadOnlyList<double> self)
        {
            var n = self.Count / 3;
            var r = new List<Vector3D>(n);
            for (var i = 0; i < n; ++i)
            {
                r[i] = new Vector3D(self[i * 3], self[i * 3 + 1], self[i * 3 + 2]);
            }
            return r;
        }

        public static List<int> ToPlato(this IReadOnlyList<int> self)
        {
            var n = self.Count / 4;
            var r = new List<int>(n * 3);
            for (var i = 0; i < n; ++i)
            {
                if (self[i * 4] != 3) 
                    throw new Exception("Forgot to triangulate the mesh");
                r.Add(self[i * 4 + 1]);
                r.Add(self[i * 4 + 2]);
                r.Add(self[i * 4 + 3]);
            }
            return r;
        }
    }
}
