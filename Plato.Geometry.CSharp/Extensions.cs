using System;
using System.Collections.Generic;
using System.Linq;

namespace Plato.DoublePrecision
{
    /// <summary>
    /// Many of these functions, once validated, will move into the Plato library.
    /// </summary>
    public static partial class Extensions2
    {
        public static IArray<T> ToIArray<T>(this IReadOnlyList<T> self)
            => new ListArray<T>(self);

        public static IArray<T> ToIArray<T>(this T[] self)
            => new PrimitiveArray<T>(self);

        public static IArray<T> Repeat<T>(this T self, int count)
            => ((Integer)count).MapRange(_ => self);

        public static IArray<T> MapRange<T>(this int count, Func<Integer, T> func)
            => ((Integer)count).MapRange(func);

        public static QuadGrid3D UpArrow(double length, double minorRadius, double majorRadius, int radialSegments,
            double percentTail)
        {
            var first = Vector3D.Default;
            var last = Vector3D.UnitZ * length;
            var mid = first.Lerp(last, percentTail);

            var a = first + -Vector3D.UnitX * minorRadius;
            var b = mid + -Vector3D.UnitX * minorRadius;
            var c = mid + -Vector3D.UnitX * majorRadius;

            var profile = Intrinsics.MakeArray(first, a, b, c, last);
            return profile.SurfaceOfRevolution(Vector3D.UnitZ, radialSegments, true, true);
        }

        public static IArray<Vector3D> Transform(this IArray<Vector3D> points, Transform3D t)
            => points.Map(t.Transform);

        public static IArray2D<T> ToArray2D<T>(this IArray<IArray<T>> rows)
        {
            if (rows.Count == 0)
                return new Array2D<T>(0, 0, (i, j) => default);
            var nColumns = rows[0].Count;
            if (!rows.All(xs => xs.Count == nColumns))
                throw new Exception("All rows must have the same length");
            return new Array2D<T>(nColumns, rows.Count, (i, j) => rows[j][i]);
        }

        public static ITransform3D FractionalRotation(Number numerator, Number denominator, Vector3D axis)
            => axis.AxisAngle(FractionalTurn(numerator, denominator));

        public static ITransform3D FractionalRotation(Number amount, Vector3D axis)
            => axis.AxisAngle(-amount.Turns);

        public static IArray<Vector3D> TransformBy(this IArray<Vector3D> self, ITransform3D t)
            => self.Map(t.Transform);

        public static QuadGrid3D SurfaceOfRevolution(this IArray<Vector3D> points, Vector3D axis, Integer segments,
            Boolean closedU, Boolean closedV)
            => segments
                .LinearSpace
                .Map(x => points.TransformBy(FractionalRotation(x, axis)))
                .ToArray2D()
                .ToQuadGrid(closedU, closedV);

        public static QuadGrid3D ToQuadGrid(this IArray2D<Vector3D> points, bool closedX, bool closedY)
            => new QuadGrid3D(points, closedX, closedY);

        public static QuadGrid3D ToQuadGrid(Func<Vector2D, Vector3D> f, Integer nGridSize, Boolean closedX,
            Boolean closedY)
            => nGridSize
                .MakeArray2D(nGridSize, (a, b) =>
                    new Vector2D(a.Value / (double)nGridSize, b.Value / (double)nGridSize))
                .Map(uv => f((uv.X, uv.Y)))
                .ToQuadGrid(closedX, closedY);

        public static TriangleMesh3D ToTriangleMesh(this IQuadMesh3D q)
        {
            var vertices = q.Points;
            var nFaces = q.Indices.Count / 4;
            var nxs = q.Indices;
            var indices = nFaces.Range.FlatMap(f => Intrinsics.MakeArray(
                nxs[f * 4 + 0], nxs[f * 4 + 1], nxs[f * 4 + 2],
                nxs[f * 4 + 2], nxs[f * 4 + 3], nxs[f * 4 + 0]));
            return new TriangleMesh3D(vertices, indices);
        }

        // TODO: I'm not confident about this 
        public static Vector3D Barycentric(this Quad3D q, Vector2D uv)
            => q.A * (1 - uv.X) * (1 - uv.Y) + q.B * uv.X * (1 - uv.Y) + q.C * uv.X * uv.Y + q.D * (1 - uv.X) * uv.Y;

        public static QuadGrid3D ToGrid(this Quad3D quad, IArray2D<Vector2D> uvs)
            => uvs.Map(uv => quad.Barycentric(uv)).ToQuadGrid(false, false);

        public static QuadGrid3D Subdivide(this Quad3D quad, IArray<Number> us, IArray<Number> vs)
        {
            return quad.ToGrid(
                us.PrependAndAppend(0, 1).CartesianProduct(
                    vs.PrependAndAppend(0, 1), (u, v) => new Vector2D(u, v)));
        }

        //==
        // Array helpers 

        public static IArray<Vector3D> Normalize(this IArray<Vector3D> vectors)
            => vectors.Map(v => v.Normalize);
        
        //==
        // Primitives to points 

        public static PolyLine2D ToPolyLine2D(this Vector2D[] self, bool closed)
            => self.ToIArray().ToPolyLine2D(closed);

        public static PolyLine2D ToPolyLine2D(this IArray<Vector2D> self, bool closed)
            => new PolyLine2D(self, closed);

        public static PolyLine3D ToPolyLine3D(this IArray<Vector2D> self, bool closed)
            => self.ToPolyLine2D(closed);

        public static PolyLine3D ToPolygon(this Vector3D[] self)
            => self.ToPolyLine3D(true);

        public static PolyLine3D ToPolygon(this IArray<Vector3D> self)
            => self.ToPolyLine3D(true);

        public static PolyLine3D ToPolyLine3D(this Vector3D[] self, bool closed)
            => self.ToIArray().ToPolyLine3D(closed);

        public static PolyLine3D ToPolyLine3D(this IArray<Vector3D> self, bool closed)
            => new PolyLine3D(self, closed);

        public static TriangleMesh3D ToMesh(this IArray<Triangle3D> self)
            => self.Points().ToTriangleMesh();

        public static TriangleMesh3D ToTriangleMesh(this IArray<Vector3D> points, IArray<Integer> indices)
            => new TriangleMesh3D(points, indices);

        public static TriangleMesh3D ToTriangleMesh(this IArray<Vector3D> points)
            => points.ToTriangleMesh(points.Indices());

        public static IArray<Integer> FlipWindingOrderTriangleIndices(this IArray<Integer> indices)
            => indices.Indices().Slices(3).FlatMap(slice => slice.Reverse());

        public static TriangleMesh3D DoubleSided(this TriangleMesh3D mesh)
            => mesh.Points.ToTriangleMesh(mesh.Indices.Concat(mesh.Indices.FlipWindingOrderTriangleIndices()));

        public static TriangleMesh3D Faceted(this TriangleMesh3D self)
            => self.Triangles.ToMesh();

        public static TriangleMesh3D FlipWindingOrder(this TriangleMesh3D mesh)
            => mesh.Points.ToTriangleMesh(mesh.Indices.FlipWindingOrderTriangleIndices());

        public static QuadMesh3D ToQuadMesh(this IArray<Vector3D> points, IArray<Integer> indices)
            => new QuadMesh3D(points, indices);

        public static QuadMesh3D ToQuadMesh(this IArray<Vector3D> points)
            => points.ToQuadMesh(points.Indices());

        //==
        // Transformation extension functions 

        public static Vector3D GetAxis(this int n)
            => n == 0 ? Vector3D.UnitX
                : n == 1 ? Vector3D.UnitY
                : n == 2 ? Vector3D.UnitZ
                : throw new Exception("Invalid axis");

        //==
        // Extra Array<Vector3D> functions

        public static Vector3D Sum(this IArray<Vector3D> self)
            => self.Aggregate(Vector3D.Default, (a, b) => a + b);

        public static Vector3D Average(this IArray<Vector3D> self)
            => self.Sum() / self.Count;

        public static IArray<Vector3D> To3D(this IArray<Vector2D> self)
            => self.Map(v => v.To3D);

        //==
        // Deformations 

        public static Vector3D InverseLerp(this Bounds3D b, Vector3D v)
            => (v - b.Min) / b.Size;

        //==
        // Quad strips

        public static QuadGrid3D QuadStrip(this IArray<Vector3D> bottom, IArray<Vector3D> upper, bool closed)
        {
            if (bottom.Count != upper.Count)
                throw new Exception("Bottom and upper arrays must have the same length");
            return Intrinsics.MakeArray(bottom, upper).ToArray2D().ToQuadGrid(closed, false);
        }

        public static QuadGrid3D Extrude(this PolyLine3D polyLine, Vector3D direction)
            => polyLine.Sweep(v => v.Translate(direction));

        public static QuadGrid3D Sweep(this PolyLine3D polyLine, Func<Vector3D, Vector3D> f)
            => polyLine.Points.QuadStrip(polyLine.Deform(f).Points, polyLine.Closed);

        public static QuadGrid3D Extrude(this PolyLine2D polyLine, Vector3D direction)
            => polyLine.To3D.Extrude(direction);

        public static QuadGrid3D Extrude(this IArray<Vector3D> points, Vector3D direction, bool closed)
            => points.ToPolyLine3D(closed).Extrude(direction);

        public static QuadGrid3D ToPrism(this PolyLine2D poly)
            => poly.ToPrism(1.0);

        public static QuadGrid3D ToPrism(this PolyLine2D poly, Number extrusionAmount)
            => poly.Extrude(Vector3D.UnitZ * extrusionAmount);

        public static TriangleMesh3D ToCappedPrism(this PolyLine2D poly, Number extrusionAmount)
        {
            var r = poly.ToPrism(extrusionAmount);
            var topPoints = r.Points.Skip(poly.Points.Count);
            var cap = topPoints.ToFan();
            return Combine(r, cap);
        }

        public static QuadGrid3D Sweep(this PolyLine2D polyLine, Func<Vector3D, Number, Vector3D> f, Integer segments, Boolean closed)
            => polyLine.To3D.Sweep(f, segments, closed);

        public static QuadGrid3D Sweep(this PolyLine3D polyLine, Func<Vector3D, Number, Vector3D> f, Integer segments, Boolean closed)
            => segments
                .LinearSpaceExclusive
                .Map(t => polyLine.Deform(p => f(p, t)).Points)
                .ToArray2D()
                .ToQuadGrid(polyLine.Closed, closed);

        /* TODO:
        public static QuadGrid3D Sweep(this PolyLine3D polyLine, Ray3D pointAndAxis, Integer segments, Angle amount)
            => polyLine.Sweep((p, t) => new AxisAngle(pointAndAxis, amount * t), segments, false);
        */

        //==
        // Fans and Pyramids

        public static TriangleMesh3D ToPyramid(this IArray<Vector3D> points)
            => points.ToPyramid(1);

        public static TriangleMesh3D ToPyramid(this IArray<Vector3D> points, Number height)
            => points.ToFan(Vector3D.UnitZ * height, true);

        public static IArray<Integer> FanIndices(Integer count, Boolean connected)
        {
            var r = new List<Integer>();
            for (var i = 0; i < count - 1; i++)
            {
                r.Add(i);
                r.Add(i + 1);
                r.Add(count);
            }

            if (connected)
            {
                r.Add(count - 1);
                r.Add(0);
                r.Add(count);
            }

            return r.ToIArray();
        }

        public static TriangleMesh3D ToFan(this IArray<Vector3D> points)
            => points.ToFan(true);

        public static TriangleMesh3D ToFan(this IArray<Vector3D> points, Boolean connected)
            => points.ToFan(points.Average(), connected);

        public static TriangleMesh3D ToFan(this IArray<Vector3D> points, Vector3D top, Boolean connected)
            => (points.Append(top), FanIndices(points.Count, connected));

        //==
        // Line2D functions
        // Particularly useful for offsets and intersections. 

        // Fins the intersection between two lines.
        // Returns true if they intersect
        // References:
        // https://www.codeproject.com/Tips/862988/Find-the-Intersection-Point-of-Two-Line-Segments
        // https://gist.github.com/unitycoder/10241239e080720376830f84511ccd3c
        // https://en.m.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line
        // https://stackoverflow.com/questions/4543506/algorithm-for-intersection-of-2-lines
        public static bool Intersection(this Line2D line1, Line2D line2, out Vector2D point, double epsilon = 0.000001f)
        {

            var x1 = line1.A.X;
            var y1 = line1.A.Y;
            var x2 = line1.B.X;
            var y2 = line1.B.Y;
            var x3 = line2.A.X;
            var y3 = line2.A.Y;
            var x4 = line2.B.X;
            var y4 = line2.B.Y;

            var denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (denominator.Abs < epsilon)
            {
                point = Vector2D.Default.Zero;
                return false;
            }

            var num1 = (x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4);
            var num2 = (x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3);
            var t1 = num1 / denominator;
            var t2 = -num2 / denominator;
            var p1 = line1.Lerp(t1);
            var p2 = line2.Lerp(t2);
            point = p1.MidPoint(p2);
            return true;
        }

        // Returns the distance between two lines
        // t and u are the distances if the intersection points along the two lines
        public static Number LineLineDistance(Line2D line1, Line2D line2, out Number t, out Number u, double epsilon = 0.0000001f)
        {
            var x1 = line1.A.X;
            var y1 = line1.A.Y;
            var x2 = line1.B.X;
            var y2 = line1.B.Y;
            var x3 = line2.A.X;
            var y3 = line2.A.Y;
            var x4 = line2.B.X;
            var y4 = line2.B.Y;

            var denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (denominator.Abs >= epsilon)
            {
                // Lines are not parallel, they should intersect nicely
                var num1 = (x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4);
                var num2 = (x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3);

                t = num1 / denominator;
                u = -num2 / denominator;

                var e = 0.0;
                if (t >= -e && t <= 1.0 + e && u >= -e && u <= 1.0 + e)
                {
                    t = t.Clamp(0.0, 1.0);
                    u = u.Clamp(0.0, 1.0);
                    return 0;
                }
            }

            // Parallel or non intersecting lines - default to point to line checks

            u = 0.0f;
            var minDistance = Distance(line1, line2.A, out t);
            var distance = Distance(line1, line2.B, out var amount);
            if (distance < minDistance)
            {
                minDistance = distance;
                t = amount;
                u = 1.0f;
            }

            distance = Distance(line2, line1.A, out amount);
            if (distance < minDistance)
            {
                minDistance = distance;
                u = amount;
                t = 0.0f;
            }

            distance = Distance(line2, line1.B, out amount);
            if (distance < minDistance)
            {
                minDistance = distance;
                u = amount;
                t = 1.0f;
            }

            return minDistance;
        }

        // Returns the distance between a line and a point.
        // t is the distance along the line of the closest point
        public static Number Distance(this Line2D line, Vector2D p, out Number t)
        {
            var (a, b) = line;

            // Return minimum distance between line segment vw and point p
            var l2 = (a - b).LengthSquared; // i.e. |w-v|^2 -  avoid a sqrt
            if (l2 == 0.0f) // v == w case
            {
                t = 0.5f;
                return (p - a).Length;
            }

            // Consider the line extending the segment, parameterized as v + t (w - v).
            // We find projection of point p onto the line.
            // It falls where t = [(p-v) . (w-v)] / |w-v|^2
            // We clamp t from [0,1] to handle points outside the segment vw.
            t = ((p - a).Dot(b - a) / l2).Clamp(0.0f, 1.0f);
            var closestPoint = a + (b - a) * t; // Projection falls on the segment
            return (p - closestPoint).Length;
        }
        
        //==
        // Angle functions

        public static Angle FractionalTurn(this Number numerator, Number denominator)
            => (numerator / denominator).Turns;

        public static Angle FractionalTurn(this Integer numerator, Number denominator)
            => (numerator / denominator).Turns;

        public static Angle FractionalTurn(this int count)
            => ((Integer)count).FractionalTurn();

        public static Angle FractionalTurn(this Integer count)
            => 1.Turns() / (Number)count;

        public static Angle Turns(this int n)
            => ((Number)n).Turns;

        public static Angle Turns(this double d)
            => ((Number)d).Turns;

        public static Angle Degrees(this int n)
            => ((Number)n).Degrees;

        //==
        // Mesh helpers

        public static TriangleMesh3D Combine(this TriangleMesh3D mesh, TriangleMesh3D other)
            => mesh.Points.Concat(other.Points).ToTriangleMesh(mesh.Indices.Concat(other.Indices.Map(i => i + mesh.Points.Count)));

        public static Vector3D Average(this IReadOnlyList<Vector3D> vectors)
            => vectors.Aggregate(Vector3D.Default, (a, b) => a + b) / vectors.Count;

        public static IArray<Vector3D> ComputeVertexNormals(this ITriangleMesh3D mesh)
        {
            var faceNormals = mesh.Triangles.Map(f => f.Normal);
            var n = mesh.Points.Count;
            var sharedNormals = Enumerable.Range(0, n).Select(_ => new List<Vector3D>()).ToList();
            for (var i = 0; i < mesh.Indices.Count; i++)
            {
                var index = mesh.Indices[i];
                var normal = faceNormals[i / 3];
                sharedNormals[index].Add(normal);
            }
            var r = new Vector3D[mesh.Points.Count];
            for (var i= 0; i < n; i++)
            {
                r[i] = sharedNormals[i].Average();
            }
            return r.ToIArray();
        }

        // TODO: why aren't my matrices express as rows ? The idea of storing them as columns seems silly
        public static Matrix4x4 YUpToZUp = Matrix4x4.CreateFromRows(
            (1, 0, 0, 0),
            (0, 0, -1, 0),
            (0, 1, 0, 0),
            (0, 0, 0, 1));

    }
}
