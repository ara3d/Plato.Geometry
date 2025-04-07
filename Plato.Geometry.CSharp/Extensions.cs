using System;
using System.Collections.Generic;
using System.Linq;

namespace Plato
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
            var first = Vector3.Default;
            var last = Vector3.UnitZ * length;
            var mid = first.Lerp(last, percentTail);

            var a = first + -Vector3.UnitX * minorRadius;
            var b = mid + -Vector3.UnitX * minorRadius;
            var c = mid + -Vector3.UnitX * majorRadius;

            var profile = Intrinsics.MakeArray(first, a, b, c, last);
            return profile.SurfaceOfRevolution(Vector3.UnitZ, radialSegments, true, true);
        }   

        public static IArray<Vector3> Transform(this IArray<Vector3> points, Transform3D t)
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

        public static ITransform3D FractionalRotation(Number numerator, Number denominator, Vector3 axis)
            => axis.AxisAngle(FractionalTurn(numerator, denominator));

        public static ITransform3D FractionalRotation(Number amount, Vector3 axis)
            => axis.AxisAngle(-amount.Turns);

        public static IArray<Vector3> TransformBy(this IArray<Vector3> self, ITransform3D t)
            => self.Map(t.Transform);

        public static QuadGrid3D SurfaceOfRevolution(this IArray<Vector3> points, Vector3 axis, Integer segments,
            Boolean closedU, Boolean closedV)
            => segments
                .LinearSpace
                .Map(x => points.TransformBy(FractionalRotation(x, axis)))
                .ToArray2D()
                .ToQuadGrid(closedU, closedV);

        public static QuadGrid3D ToQuadGrid(this IArray2D<Vector3> points, bool closedX, bool closedY)
            => new QuadGrid3D(points, closedX, closedY);

        public static QuadGrid3D ToQuadGrid(Func<Vector2, Vector3> f, Integer nGridSize, Boolean closedX,
            Boolean closedY)
            => nGridSize
                .MakeArray2D(nGridSize, (a, b) =>
                    new Vector2(a.Value / (double)nGridSize, b.Value / (double)nGridSize))
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
        public static Vector3 Barycentric(this Quad3D q, Vector2 uv)
            => q.A * (1 - uv.X) * (1 - uv.Y) + q.B * uv.X * (1 - uv.Y) + q.C * uv.X * uv.Y + q.D * (1 - uv.X) * uv.Y;

        public static QuadGrid3D ToGrid(this Quad3D quad, IArray2D<Vector2> uvs)
            => uvs.Map(uv => quad.Barycentric(uv)).ToQuadGrid(false, false);

        public static QuadGrid3D Subdivide(this Quad3D quad, IArray<Number> us, IArray<Number> vs)
        {
            return quad.ToGrid(
                us.PrependAndAppend(0, 1).CartesianProduct(
                    vs.PrependAndAppend(0, 1), (u, v) => new Vector2(u, v)));
        }

        //==
        // Array helpers 

        public static IArray<Vector3> Normalize(this IArray<Vector3> vectors)
            => vectors.Map(v => v.Normalize);
        
        //==
        // Primitives to points 

        public static PolyLine2D ToPolyLine2D(this Vector2[] self, bool closed)
            => self.ToIArray().ToPolyLine2D(closed);

        public static PolyLine2D ToPolyLine2D(this IArray<Vector2> self, bool closed)
            => new PolyLine2D(self, closed);

        public static PolyLine3D ToPolyLine3D(this IArray<Vector2> self, bool closed)
            => self.ToPolyLine2D(closed);

        public static PolyLine3D ToPolygon(this Vector3[] self)
            => self.ToPolyLine3D(true);

        public static PolyLine3D ToPolygon(this IArray<Vector3> self)
            => self.ToPolyLine3D(true);

        public static PolyLine3D ToPolyLine3D(this Vector3[] self, bool closed)
            => self.ToIArray().ToPolyLine3D(closed);

        public static PolyLine3D ToPolyLine3D(this IArray<Vector3> self, bool closed)
            => new PolyLine3D(self, closed);

        public static TriangleMesh3D ToMesh(this IArray<Triangle3D> self)
            => self.Points().ToTriangleMesh();

        public static TriangleMesh3D ToTriangleMesh(this IArray<Vector3> points, IArray<Integer> indices)
            => new TriangleMesh3D(points, indices);

        public static TriangleMesh3D ToTriangleMesh(this IArray<Vector3> points)
            => points.ToTriangleMesh(points.Indices());

        public static IArray<Integer> FlipWindingOrderTriangleIndices(this IArray<Integer> indices)
            => indices.Slices(3).FlatMap(slice => slice.Reverse());

        public static TriangleMesh3D DoubleSided(this TriangleMesh3D mesh)
            => mesh.Points.ToTriangleMesh(mesh.Indices.Concat(mesh.Indices.FlipWindingOrderTriangleIndices()));

        public static TriangleMesh3D Faceted(this TriangleMesh3D self)
            => self.Triangles.ToMesh();

        public static TriangleMesh3D FlipWindingOrder(this TriangleMesh3D mesh)
            => mesh.Points.ToTriangleMesh(mesh.Indices.FlipWindingOrderTriangleIndices());

        public static QuadMesh3D ToQuadMesh(this IArray<Vector3> points, IArray<Integer> indices)
            => new QuadMesh3D(points, indices);

        public static QuadMesh3D ToQuadMesh(this IArray<Vector3> points)
            => points.ToQuadMesh(points.Indices());

        //==
        // Transformation extension functions 

        public static Vector3 GetAxis(this int n)
            => n == 0 ? Vector3.UnitX
                : n == 1 ? Vector3.UnitY
                : n == 2 ? Vector3.UnitZ
                : throw new Exception("Invalid axis");

        //==
        // Extra Array<Vector3> functions

        public static Vector3 Sum(this IArray<Vector3> self)
            => self.Aggregate(Vector3.Default, (a, b) => a + b);

        public static Vector3 Average(this IArray<Vector3> self)
            => self.Sum() / self.Count;

        public static IArray<Vector3> To3D(this IArray<Vector2> self)
            => self.Map(v => v.To3D);

        //==
        // Deformations 

        public static Vector3 InverseLerp(this Bounds3D b, Vector3 v)
            => (v - b.Min) / b.Size;

        //==
        // Quad strips

        public static QuadGrid3D QuadStrip(this IArray<Vector3> bottom, IArray<Vector3> upper, bool closed)
        {
            if (bottom.Count != upper.Count)
                throw new Exception("Bottom and upper arrays must have the same length");
            return Intrinsics.MakeArray(bottom, upper).ToArray2D().ToQuadGrid(closed, false);
        }

        public static QuadGrid3D Extrude(this PolyLine3D polyLine, Vector3 direction)
            => polyLine.Sweep(v => v + direction);

        public static QuadGrid3D Sweep(this PolyLine3D polyLine, Func<Vector3, Vector3> f)
            => polyLine.Points.QuadStrip(polyLine.Deform(f).Points, polyLine.Closed);

        public static QuadGrid3D Extrude(this PolyLine2D polyLine, Vector3 direction)
            => polyLine.To3D.Extrude(direction);

        public static QuadGrid3D Extrude(this IArray<Vector3> points, Vector3 direction, bool closed)
            => points.ToPolyLine3D(closed).Extrude(direction);

        public static QuadGrid3D ToPrism(this PolyLine2D poly)
            => poly.ToPrism(1.0f);

        public static QuadGrid3D ToPrism(this PolyLine2D poly, Number extrusionAmount)
            => poly.Extrude(Vector3.UnitZ * extrusionAmount);

        public static TriangleMesh3D ToCappedPrism(this PolyLine2D poly, Number extrusionAmount)
        {
            var r = poly.ToPrism(extrusionAmount);
            var topPoints = r.Points.Skip(poly.Points.Count);
            var cap = topPoints.ToFan();
            return Combine(r, cap);
        }

        public static QuadGrid3D Sweep(this PolyLine2D polyLine, Func<Vector3, Number, Vector3> f, Integer segments, Boolean closed)
            => polyLine.To3D.Sweep(f, segments, closed);

        public static QuadGrid3D Sweep(this PolyLine3D polyLine, Func<Vector3, Number, Vector3> f, Integer segments, Boolean closed)
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

        public static TriangleMesh3D ToPyramid(this IArray<Vector3> points)
            => points.ToPyramid(1);

        public static TriangleMesh3D ToPyramid(this IArray<Vector3> points, Number height)
            => points.ToFan(Vector3.UnitZ * height, true);

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

        public static TriangleMesh3D ToFan(this IArray<Vector3> points)
            => points.ToFan(true);

        public static TriangleMesh3D ToFan(this IArray<Vector3> points, Boolean connected)
            => points.ToFan(points.Average(), connected);

        public static TriangleMesh3D ToFan(this IArray<Vector3> points, Vector3 top, Boolean connected)
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
        public static bool Intersection(this Line2D line1, Line2D line2, out Vector2 point, double epsilon = 0.000001f)
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
                point = Vector2.Default;
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

        public static bool Intersects(this Triangle3D t1, Triangle3D t2)
        {
            if (!t1.Bounds().Overlaps(t2.Bounds()))
                return false;

            // Step 2: For each edge of t1, check if it intersects t2
            if (SegmentIntersectsTriangle(t1.A, t1.B, t2) ||
                SegmentIntersectsTriangle(t1.B, t1.C, t2) || 
                SegmentIntersectsTriangle(t1.C, t1.A, t2)) return true;

            // Step 3: For each edge of t2, check if it intersects t1
            if (SegmentIntersectsTriangle(t2.A, t2.B, t1) || 
                SegmentIntersectsTriangle(t2.B, t2.C, t1) || 
                SegmentIntersectsTriangle(t2.C, t2.A, t1)) return true;

            // Step 4: Check if any vertex of t1 is inside t2
            if (t2.Contains(t1.A) || t2.Contains(t1.B) || t2.Contains(t1.C)) return true;

            // Step 5: Check if any vertex of t2 is inside t1
            if (t1.Contains(t2.A) || t1.Contains(t2.B) || t1.Contains(t2.C)) return true;

            // No intersection found
            return false;
        }

        public static Bounds3D Bounds(this Triangle3D tri)
            => ((tri.A.X.Min(tri.B.X).Min(tri.C.X),
                    tri.A.Y.Min(tri.B.Y).Min(tri.C.Y),
                    tri.A.Z.Min(tri.B.Z).Min(tri.C.Z)),
                (tri.A.X.Max(tri.B.X).Max(tri.C.X),
                    tri.A.Y.Max(tri.B.Y).Max(tri.C.Y),
                    tri.A.Z.Max(tri.B.Z).Max(tri.C.Z)));

        public static TriangleMesh3D TriangleMesh3D(this Bounds3D bounds)
            => PlatonicSolids.Cube.TriangleMesh3D.Scale(bounds.Size).Translate(bounds.Center).TriangleMesh3D;

        public static bool SegmentIntersectsTriangle(Vector3 p0, Vector3 p1, Triangle3D tri)
        {
            // Compute plane normal
            var edge1 = tri.B - tri.A;
            var edge2 = tri.C - tri.A;
            var normal = edge1.Cross(edge2);

            // Compute denominator to check if segment and plane are parallel
            var dir = p1 - p0;
            var denom = normal.Dot(dir);

            if (denom.Abs < 1e-8)
            {
                // Segment is parallel to the plane
                return false;
            }

            // Compute t where the segment intersects the plane
            var t = -(normal.Dot(p0 - tri.A)) / denom;

            if (t < 0.0 || t > 1.0)
            {
                // Intersection point is not on the segment
                return false;
            }

            // Compute the intersection point
            var p = p0 + dir * t;

            // Check if the point P is inside the triangle
            return tri.Contains(p);
        }

        public static bool Contains(this Triangle3D tri, Vector3 p)
        {
            var v0 = tri.C - tri.A;
            var v1 = tri.B - tri.A;
            var v2 = p - tri.A;

            // Compute dot products
            var dot00 = v0.Dot(v0);
            var dot01 = v0.Dot(v1);
            var dot02 = v0.Dot(v2);
            var dot11 = v1.Dot(v1);
            var dot12 = v1.Dot(v2);

            // Compute barycentric coordinates
            var denom = dot00 * dot11 - dot01 * dot01;
            if (denom.Abs < 1e-8)
            {
                // Triangle is degenerate
                return false;
            }
            var invDenom = 1.0 / denom;
            var u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            var v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            // Check if point is in triangle
            return (u >= 0) && (v >= 0) && (u + v <= 1);
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
                    t = t.Clamp(0f, 1f);
                    u = u.Clamp(0f, 1f);
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
        public static Number Distance(this Line2D line, Vector2 p, out Number t)
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

        public static Vector3 Average(this IReadOnlyList<Vector3> vectors)
            => vectors.Aggregate(Vector3.Default, (a, b) => a + b) / vectors.Count;

        public static IArray<Vector3> ComputeVertexNormals(this TriangleMesh3D mesh)
        {
            var faceNormals = mesh.Triangles.Map(f => f.Normal);
            var n = mesh.Points.Count;
            var sharedNormals = Enumerable.Range(0, n).Select(_ => new List<Vector3>()).ToList();
            for (var i = 0; i < mesh.Indices.Count; i++)
            {
                var index = mesh.Indices[i];
                var normal = faceNormals[i / 3];
                sharedNormals[index].Add(normal);
            }
            var r = new Vector3[mesh.Points.Count];
            for (var i= 0; i < n; i++)
            {
                r[i] = sharedNormals[i].Average();
            }
            return r.ToIArray();
        }

        public static Matrix4x4 YUpToZUp = new Matrix4x4(
            (1, 0, 0, 0),
            (0, 0, -1, 0),
            (0, 1, 0, 0),
            (0, 0, 0, 1));

        public static Number Unlerp(this Number a, Number b, Number x)
            => (x - a) / (b - a);

        //==
        // Curve functions

        public static IArray<Vector2> Points(this ICurve2D curve, Integer count, Number from, Number to)
            => (curve.Closed ? count.LinearSpaceExclusive : count.LinearSpace).Map(t => curve.Eval(from.Lerp(to, t)));

        public static PolyLine3D ToPolyLine3D(this ICurve2D curve, int segments, Number from, Number to)
            => new PolyLine3D(curve.Points(segments, from, to).To3D(), curve.Closed);

        // TODO: there are more of these that need to be added to the library.
        // Given just an interface, there is an obvious class that implements the interface
        public class Curve2D : ICurve2D
        {
            public Boolean Closed { get; }    
            public Func<Number, Vector2> Func { get; }
            public Vector2 Eval(Number t) => Func(t);
            public Curve2D(Func<Number, Vector2> f, Boolean closed) { Func = f; Closed = closed; }
            public Number Distance(Vector2 v) => throw new NotImplementedException();
        }

        public static ICurve2D ToCurve(this IRealFunction f)
            => new Curve2D(t => (t, f.Eval(t)), false);
    }
}
