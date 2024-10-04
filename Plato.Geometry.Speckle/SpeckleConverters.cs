using System;
using System.Collections.Generic;
using System.DoubleNumerics;
using System.Linq;
using Ara3D.Speckle.Data;
using Objects.Geometry;
using Objects.Other;
using Objects.Utils;
using Plato.DoublePrecision;
using Plato.Geometry.Scenes;
using Material = Plato.Geometry.Graphics.Material;
using Quaternion = System.DoubleNumerics.Quaternion;
using PQuaternion = Plato.DoublePrecision.Quaternion;

namespace Plato.Geometry.Speckle
{
    public static class SpeckleConverters
    {
        public static IScene ToScene(this SpeckleObject self)
        {
            var d = new Dictionary<string, SceneNode>();
            var r = self.ToPlato(d);
            var scl = 0.001;
            r.Transform =
                new TRSTransform(new Transform3D(Vector3D.Default, PQuaternion.Default.Identity, (scl, scl, scl)));
            return new Scene(r);
        }

        public static SceneNode ToPlato(this SpeckleObject self, Dictionary<string, SceneNode> d)
        {
            if (d.TryGetValue(self.Id, out var node))
                return node;
            var r = d[self.Id] = new SceneNode
            {
                Name = self.Name,
                Id = self.Id
            };
            var mat = self.Material?.ToMaterial();
            var mesh = self.Mesh?.ToPlato();
            if (self.Transform == null) 
                r.Transform = NullTransform.Instance;
            else
            {
                self.Transform.Decompose(out var scale, out var rot, out var pos);
                r.Transform = new TRSTransform(new Transform3D(pos.ToPlato(), rot.ToPlato(), scale.ToPlato()));
            }
            if (mesh != null) 
                r.Objects.Add(new SceneMesh(mat, mesh.Value));
            r.Children.AddRange(self.Children.Select(c => c.ToPlato(d)));
            return r;
        }

        public static Vector3D ToPlato(this Vector3 self)
            => (self.X, self.Y, self.Z);
        
        public static Vector3D ToPlato(this Vector4 self)
            => (self.X, self.Y, self.Z);
        
        public static Rotation3D ToPlato(this Quaternion self)
            => new Plato.DoublePrecision.Quaternion(self.X, self.Y, self.Z, self.W);
        
        public static Material ToMaterial(this RenderMaterial self)
            => new Material(self.diffuseColor.ToColor(self.opacity))
            {
                Name =  self.name,
                Roughness = self.roughness,
                Metallic = self.metalness
            };

        public static Color ToColor(this System.Drawing.Color self, double opacity)
            => new Color(self.R / 255.0, self.G / 255.0, self.B / 255.0, opacity);
        
        public static IArray<Vector3D> ToPlato(this IReadOnlyList<double> self)
        {
            var n = self.Count / 3;
            var r = new List<Vector3D>(n);
            for (var i = 0; i < n; ++i)
            {
                r.Add(new Vector3D(self[i * 3], self[i * 3 + 1], self[i * 3 + 2]));
            }
            return r.ToIArray();
        }

        public static IArray<Integer> ToPlatoIndices(this IReadOnlyList<int> self)
        {
            var n = self.Count / 4;
            var r = new List<Integer>(n * 3);
            for (var i = 0; i < n; ++i)
            {
                if (self[i * 4] != 3) 
                    throw new Exception("Forgot to triangulate the mesh");
                r.Add(self[i * 4 + 1]);
                r.Add(self[i * 4 + 2]);
                r.Add(self[i * 4 + 3]);
            }
            return r.ToIArray();
        }

        public static bool NeedsTriangulation(this Mesh mesh)
        {
            for (var i = 0; i < mesh.faces.Count; i += 4)
                if (mesh.faces[i] != 3) return true;
            return false;
        }

        public static TriangleMesh ToPlato(this Mesh self)
        {
            if (self.NeedsTriangulation())
                self.TriangulateMesh();
            return new TriangleMesh(self.vertices.ToPlato(), self.faces.ToPlatoIndices());
        }
    }
}
