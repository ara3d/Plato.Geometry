using System;
using System.Collections;
using System.Collections.Generic;
using System.DoubleNumerics;
using System.Linq;
using Objects;
using Objects.Geometry;
using Objects.Other;
using Objects.Utils;
using Plato.SinglePrecision;
using Plato.Geometry.Scenes;
using Speckle.Core.Models;
using Color = Plato.SinglePrecision.Color;
using Material = Plato.Geometry.Graphics.Material;
using Point = Objects.Geometry.Point;
using Quaternion = System.DoubleNumerics.Quaternion;
using PQuaternion = Plato.SinglePrecision.Quaternion;

namespace Plato.Geometry.Speckle
{
    public static class SpeckleConverter
    {
        public static object ToSpeckleObject(this object o, Dictionary<string, SpeckleObject> lookup)
        {
            if (o is Base b)
                return ToSpeckleObject(b, lookup);

            if (o is string s)
                return s;

            if (o is IDictionary d)
            {
                var r = new SpeckleObject { DotNetType = o.GetType().Name };
                foreach (var k in d.Keys)
                    r.Properties.Add((string)k, ToSpeckleObject(d[k], lookup));
                if (r.Properties.Count == 0)
                    return null;
                return r;
            }

            if (o is IEnumerable seq)
            {
                var r = new SpeckleObject { DotNetType = o.GetType().Name };
                foreach (var item in seq)
                    r.Elements.Add(ToSpeckleObject(item, lookup));
                if (r.Elements.Count == 0)
                    return null;
                return r;
            }

            return o;
        }

        public static SpeckleObject ToSpeckleObject(this Base b, Dictionary<string, SpeckleObject> lookup = null)
        {
            if (b == null)
                return null;

            lookup = lookup ?? new Dictionary<string, SpeckleObject>();

            // Get or compute the ID. 
            // NOTE: "ComputeId" would be a better name for ID.
            var id = b.id ?? b.GetId();

            if (lookup.TryGetValue(id, out var found))
                return found;

            // Create a new SpeckleObject
            var r = lookup[id]
                = new SpeckleObject { Id = id };

            if (b is Mesh m)
            {
                r.Mesh = m;
                r.Material = m["renderMaterial"] as RenderMaterial;
            }

            if (b is BlockDefinition block)
            {
                r.BasePoint = block.basePoint;
                foreach (var g in block.geometry)
                    r.Elements.Add(ToSpeckleObject(g, lookup));
            }

            if (b is Collection collection)
            {
                r.Name = collection.name;
                foreach (var x in collection.elements)
                    r.Elements.Add(ToSpeckleObject(x, lookup));
            }

            if (b is Instance instance)
            {
                var def = ToSpeckleObject(instance.definition, lookup);
                r.Transform = instance.transform;
                r.InstanceDefinition = def;
                def.Instances.Add(r);
            }

            if (b is IDisplayValue<List<Mesh>> displayMeshList)
                foreach (var mesh in displayMeshList.displayValue)
                    r.Elements.Add(ToSpeckleObject(mesh, lookup));

            var type = b.GetType();
            r.DotNetType = type.Name;
            r.SpeckleType = b.speckle_type;

            foreach (var kv in b.GetDynamicAndInstanceMembers())
                r.Properties.Add(kv.Key, ToSpeckleObject(kv.Value, lookup));

            return r;
        }
    }

    public static class SpeckleExtensions
    {
        public static IEnumerable<KeyValuePair<string, object>> GetDynamicAndInstanceMembers(this Base self)
            => self.GetMembers(DynamicBaseMemberType.Instance | DynamicBaseMemberType.Dynamic);
    }
    
    /// <summary>
     /// This is generated from a 'Base' object, which is a base class for all objects in the Speckle API.
     /// It is easier to navigate and convert to/from other formats. 
     /// </summary>
    public class SpeckleObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public List<object> Elements { get; set; } = new List<object>();
        public IEnumerable<SpeckleObject> Children => Elements.Concat(Properties.Values).OfType<SpeckleObject>();
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public bool IsSimpleDictionary => Properties.Count > 0 && Elements.Count == 0 && DotNetType == "Dictionary`2";
        public bool IsSimpleList => Properties.Count == 0 && Elements.Count > 0 && DotNetType == "List`1";

        public Mesh Mesh { get; set; }
        public RenderMaterial Material { get; set; }

        public Transform Transform { get; set; }
        public SpeckleObject InstanceDefinition { get; set; }
        public string InstanceDefinitionId => InstanceDefinition?.Id ?? "";
        public List<SpeckleObject> Instances { get; set; } = new List<SpeckleObject>();
        public bool IsInstanced { get; set; }
        public Point BasePoint { get; set; }
        public string SpeckleType { get; set; }
        public string DotNetType { get; set; }
    }

    public static class SpeckleConverters
    {
        //==
        // From Speckle to Plato.Geometry

        public static Scene ToScene(this SpeckleObject self, Dictionary<string, (SpeckleObject, SceneNode)> d = null)
        {
            d = d ?? new Dictionary<string, (SpeckleObject, SceneNode)>();
            var r = self.ToPlato(d);
            // TODO: this is a hack and needs to be replaced, while sitill making things work.. 
            var scl = 0.001;
            r.Transform = new Transform3D(Vector3D.Default, PQuaternion.Identity, (scl, scl, scl));
            return new Scene(r);
        }

        public static SceneNode ToPlato(this SpeckleObject self, Dictionary<string, (SpeckleObject, SceneNode)> d)
        {
            if (d.TryGetValue(self.Id, out var pair))
                return pair.Item2;
            var r = new SceneNode
            {
                Name = self.Name,
                Id = self.Id
            };
            d[self.Id] = (self, r);
            var mat = self.Material?.ToMaterial();
            var mesh = self.Mesh?.ToPlato();
            if (self.Transform == null)
                r.Transform = IdentityTransform3D.Default;
            else
            {
                self.Transform.Decompose(out var scale, out var rot, out var pos);
                r.Transform = new Transform3D(pos.ToPlato(), rot.ToPlato(), scale.ToPlato());
            }

            // Copy the properties over 
            foreach (var kv in self.Properties)
            {
                r.Properties[kv.Key] = kv.Value;
                r.Properties["_Name"] = self.Name;
                r.Properties["_Id"] = self.Id;
                r.Properties["_Type"] = self.SpeckleType;
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
            => new PQuaternion(self.X, self.Y, self.Z, self.W);

        public static Material ToMaterial(this RenderMaterial self)
            => new Material(self.diffuseColor.ToColor(self.opacity))
            {
                Name = self.name,
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
                if (mesh.faces[i] != 3)
                    return true;
            return false;
        }

        public static TriangleMesh3D ToPlato(this Mesh self)
        {
            if (self.NeedsTriangulation())
                self.TriangulateMesh();
            return new TriangleMesh3D(self.vertices.ToPlato(), self.faces.ToPlatoIndices());
        }

        //==
        // From Plato.Geometry to Speckle 

        public static Mesh ToSpeckle(this TriangleMesh3D mesh)
        {
            var r = new Mesh();

            for (var i = 0; i < mesh.Points.Count; i++)
            {
                var p = mesh.Points[i];
                r.vertices.Add(p.X);
                r.vertices.Add(p.Y);
                r.vertices.Add(p.Z);
            }

            for (var i = 0; i < mesh.Indices.Count; i += 3)
            {
                // NOTE: I originally thought this should be 3, but 0 is what is returned from other files . 
                r.faces.Add(0);
                r.faces.Add(mesh.Indices[i]);
                r.faces.Add(mesh.Indices[i+1]);
                r.faces.Add(mesh.Indices[i+2]);
            }
            return r;
        }

        public static Mesh ToSpeckle(this TriangleMesh3D mesh, Material material)
        {
            var r = mesh.ToSpeckle();
            r["renderMaterial"] = material.ToSpeckle();
            return r;
        }

        public static RenderMaterial ToSpeckle(this Material material)
        {
            var rm = new RenderMaterial();
            var color = material.Color;
            rm.diffuseColor = System.Drawing.Color.FromArgb(
                (int)(color.A.Value * 255),
                (int)(color.R.Value * 255),
                (int)(color.G.Value * 255),
                (int)(color.B.Value * 255));
            rm.metalness = material.Metallic;
            rm.roughness = material.Roughness;
            return rm;
        }

        public static Base ToSpeckle(this Scene scene)
        {
            return scene.Root.ToSpeckle();
        }

        public static Base ToSpeckle(this ISceneNode node)
        {
            var r = new Base();
            var children = node.Children.Select(c => c.ToSpeckle()).ToList();
            r["Name"] = node.Name;
            r["elements"] = children;

            var meshes = node.Objects.OfType<SceneMesh>().Select(m => m.Mesh.ToSpeckle(m.Material)).ToList();
            if (meshes.Count > 0)
            {
                r["displayValue"] = meshes;
            }

            // TODO: add transforms 
            //node.Transform.Decompose(out var scale, out var rot, out var pos);

            return r;
        }
    }
}
