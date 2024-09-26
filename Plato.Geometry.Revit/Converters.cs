using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using Plato.DoublePrecision;
using RPlane = Autodesk.Revit.DB.Plane;
using Plane = Plato.DoublePrecision.Plane;
using Color = Plato.DoublePrecision.Color;

namespace Plato.Geometry.Revit
{
    public static class Converters
    {
        public static XYZ ToRevit(this Vector3D self) => new XYZ(self.X, self.Y, self.Z);
        public static UV ToRevit(this Vector2D self) => new UV(self.X, self.Y);

        public static Vector3D ToPlato(this XYZ self) => new Vector3D(self.X, self.Y, self.Z);
        public static Vector2D ToPlato(this UV self) => new Vector2D(self.U, self.V);
        public static Line3D ToPlato(this XYZ self, XYZ other) => (self.ToPlato(), other.ToPlato());

        public static BoundingBoxXYZ ToRevit(this Bounds3D self) => new BoundingBoxXYZ() { Min = self.Min.ToRevit(), Max = self.Max.ToRevit() };
        public static BoundingBoxUV ToRevit(this Bounds2D self) => new BoundingBoxUV() { Min = self.Min.ToRevit(), Max = self.Max.ToRevit() };

        public static Plane ToPlato(this ClipPlane self) => new Plane(self.Normal.ToPlato(), self.Origin.DistanceTo(self.Normal));
        public static Bounds3D ToPlato(this BoundingBoxXYZ self) => new Bounds3D(self.Min.ToPlato(), self.Max.ToPlato());
        public static Bounds2D ToPlato(this BoundingBoxUV self) => new Bounds2D(self.Min.ToPlato(), self.Max.ToPlato())
    }

    public static class RevitHelpers
    {
        public static void Fill(this VertexBuffer buffer, IEnumerable<XYZ> positions)
        {
            var verts = positions.Select(p => new VertexPosition(p)).ToList();
            buffer.GetVertexStreamPosition().AddVertices(verts);
        }

        public static void Fill(this VertexBuffer buffer, IEnumerable<XYZ> positions, IEnumerable<XYZ> normals)
        {
            var verts = positions.Zip(normals, (p, n) => new VertexPositionNormal(p, n)).ToList();
            buffer.GetVertexStreamPositionNormal().AddVertices(verts);
        }

        public static IEnumerable<T4> Zip<T1, T2, T3, T4>(this IEnumerable<T1> xs, IEnumerable<T2> ys, IEnumerable<T3> zs, Func<T1, T2, T3, T4> f)
        {
            using (var xe = xs.GetEnumerator())
            using (var ye = ys.GetEnumerator())
            using (var ze = zs.GetEnumerator())
            {
                while (xe.MoveNext() && ye.MoveNext() && ze.MoveNext())
                    yield return f(xe.Current, ye.Current, ze.Current);
            }
        }

        public static void Fill(this VertexBuffer buffer, IEnumerable<XYZ> positions, IEnumerable<XYZ> normals, IEnumerable<ColorWithTransparency> colors)
        {
            buffer.Fill(positions.Zip(normals, colors, (p, n, c) => new VertexPositionNormalColored(p, n, c)).ToList());
        }

        public static void Fill(this VertexBuffer buffer, IList<VertexPositionNormalColored> verts)
        {
            buffer.GetVertexStreamPositionNormalColored().AddVertices(verts);
        }

      
        public static void Test()
        {
            DrawContext.FlushBuffer();
        }
    }
}
