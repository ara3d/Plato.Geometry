using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Plato.DoublePrecision;
using Plato.Geometry.Graphics;
using Plato.Geometry.Scenes;
using Color = Plato.DoublePrecision.Color;
using Colors = Plato.Geometry.Graphics.Colors;
using Material = Plato.Geometry.Graphics.Material;
using Vector3D = Plato.DoublePrecision.Vector3D;
using WVector3D = System.Windows.Media.Media3D.Vector3D;
using WColor = System.Windows.Media.Color;
using WTransform3D = System.Windows.Media.Media3D.Transform3D;

namespace Plato.Geometry.WPF
{
    public static class WpfConverters
    {
        public static WVector3D ToWpf(this Vector3D v) 
            => new WVector3D(v.X, v.Y, v.Z);
        
        public static Point3D ToPoint(this Vector3D v) 
            => new Point3D(v.X, v.Y, v.Z);
        
        public static Vector3D ToPlato(this WVector3D v) 
            => (v.X, v.Y, v.Z);

        public static void AddTriangle(this MeshGeometry3D r, Triangle3D t)
        {
            var n = t.Normal.ToWpf();
            var i = r.Positions.Count;
            r.Positions.Add(ToPoint(t.A));
            r.Positions.Add(ToPoint(t.B));
            r.Positions.Add(ToPoint(t.C));
            r.Normals.Add(n);
            r.Normals.Add(n);
            r.Normals.Add(n);
            r.TriangleIndices.Add(i);
            r.TriangleIndices.Add(i + 1);
            r.TriangleIndices.Add(i + 2);
        }

        public static readonly SolidColorBrush DefaultBrush
            = new SolidColorBrush(System.Windows.Media.Colors.DarkSlateGray);

        public static readonly DiffuseMaterial DefaultDiffuseMaterial
            = new DiffuseMaterial(DefaultBrush);

        public static SpecularMaterial GetDefaultSpecular(Color color)
            => SpecularSettings.FromPBR(color, 0, 0.7).ToSpecularMaterial();

        public static SpecularMaterial DefaultSpecular
            => GetDefaultSpecular(Colors.DarkSlateGray);

        public static System.Windows.Media.Media3D.Material ToWpf(this Material m)
        {
            var r = new MaterialGroup();
            var diffuse = m.ToDiffuseMaterial();
            var specular = m == null 
                ? DefaultSpecular 
                : m.UseSpecular 
                    ? m.GetSpecularSettings().ToSpecularMaterial() 
                    : GetDefaultSpecular(m.Color);
            r.Children.Add(diffuse);
            r.Children.Add(specular);
            return r;
        }

        public static DiffuseMaterial ToDiffuseMaterial(this Color color)
            => new DiffuseMaterial(new SolidColorBrush(color.ToWpf()));

        public static SpecularMaterial ToSpecularMaterial(this SpecularSettings self)
            => self.Color.ToSpecularMaterial(self.Power);

        public static SpecularMaterial ToSpecularMaterial(this Color color, double power)
            => new SpecularMaterial(new SolidColorBrush(color.ToWpf()), power);

        public static MeshGeometry3D ToWpfFaceted(this TriangleMesh mesh)
        {
            var triangles = mesh.Primitives;
            var r = new MeshGeometry3D();

            foreach (var t in triangles)
            {
                AddTriangle(r, t);
            }
            
            return r;
        }

        public static MeshGeometry3D ToWpf(this TriangleMesh mesh)
        {
            var r = new MeshGeometry3D();

            for (var i = 0; i < mesh.Vertices.Count; ++i)
            {
                var v = mesh.Vertices[i];
                r.Positions.Add(new Point3D(v.X, v.Y, v.Z));
            }

            for (var i = 0; i < mesh.Indices.Count; ++i)
            {
                r.TriangleIndices.Add(mesh.Indices[i]);
            }

            r.Normals = new Vector3DCollection(mesh.ComputeVertexNormalsFaceted().Select(ToWpf));

            return r;
        }

        public static GeometryModel3D ToWpf(this ISceneMesh sm)
            => new GeometryModel3D 
            {
                Geometry = sm.Mesh.ToWpf(), 
                Material = sm.Material.ToWpf(),
            };

        public static WColor ToWpf(this Color32 c)
            => WColor.FromArgb(c.A, c.R, c.G, c.B);

        public static WColor ToWpf(this Color c)
            => ToWpf((Color32)c);

        public static SolidColorBrush ToWpfBrush(this Color c)
            => new SolidColorBrush(c.ToWpf());

        public static DiffuseMaterial ToDiffuseMaterial(this Material mat)
            => new DiffuseMaterial(mat?.Color.ToWpfBrush() ?? DefaultBrush);

        public static Matrix3D ToWpf(this Matrix4x4 m)
        {
            //m = m.Transpose();
            return new(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }

        public static WTransform3D ToWpf(this ITransform3D t)
            => t == null || t is NullTransform
                ? WTransform3D.Identity 
                : new MatrixTransform3D(t.Matrix.ToWpf());

        public static readonly WTransform3D ZUpToYUp 
            = new RotateTransform3D(new AxisAngleRotation3D(new WVector3D(1, 0, 0), -90));

        public static ModelVisual3D ToWpf(this IScene scene)
        {
            var content = scene.Root.ToWpf();
            //var newParent = new Model3DGroup();
            //var transparent = content.ExtractTransparentModels();
            //newParent.Children.Add(content);
            //newParent.Children.Add(transparent);
            content.Freeze();            
            return new ModelVisual3D()
            {
                Content = content
            };
        }

        public static Model3DGroup ToWpf(this ISceneNode node)
        {
            var meshes = node.GetMeshObjects().Select(ToWpf).ToList();
            var children = node.Children.Select(ToWpf).ToList();
                
            var r = new Model3DGroup {
                Transform = node.Transform.ToWpf(),
            };
            foreach (var m in meshes)
                r.Children.Add(m);
            foreach (var c in children)
                r.Children.Add(c);
            return r;
        }

        public static bool IsTransparent(this System.Windows.Media.Media3D.Material m)
            => (m is DiffuseMaterial dm && dm.Brush is SolidColorBrush sb && sb.Color.A < 255)
            || (m is MaterialGroup mg) && mg.Children.Any(IsTransparent);

        public static bool IsTransparent(this Model3D m)
            => m is GeometryModel3D gm && gm.Material.IsTransparent();

        public static Model3DGroup ExtractTransparentModels(this Model3DGroup group)
        {
            var r = new Model3DGroup();
            var transparentObjects = new List<Model3D>();
            for (var i = group.Children.Count; i > 0; --i)
            {
                var model = group.Children[i-1];
                if (model.IsTransparent())
                {
                    group.Children.RemoveAt(i);
                    transparentObjects.Add(model);
                }
            }
            var childGroups = group.Children.OfType<Model3DGroup>().ToList();
            var newChildGroups = childGroups.Select(ExtractTransparentModels).ToList();
            foreach (var child in transparentObjects)
                r.Children.Add(child);
            foreach (var child in newChildGroups)
                r.Children.Add(child);
            return r;
        }
    }
}
