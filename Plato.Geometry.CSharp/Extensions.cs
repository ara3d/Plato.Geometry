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

        public static IArray<T> MapRange<T>(this int count, System.Func<Integer, T> func)
            => ((Integer)count).MapRange(func);

        public static QuadGrid UpArrow(double length, double minorRadius, double majorRadius, int radialSegments,
            double percentTail)
        {
            var first = Vector3D.Default;
            var last = Vector3D.UnitZ * length;
            var mid = first.Lerp(last, percentTail);

            var a = first + -Vector3D.UnitX * minorRadius;
            var b = mid + -Vector3D.UnitX * minorRadius;
            var c = mid + -Vector3D.UnitX * majorRadius;

            var profile = Intrinsics.MakeArray(first, a, b, c, last);
            return profile.SurfaceOfRevolution(Vector3D.UnitZ, radialSegments);
        }

        // TODO: make this an implicit cast
        public static Transform3D ToTransform3D(this Quaternion q)
            => new Transform3D(Vector3D.Default, new Rotation3D(q), (1, 1, 1));

        public static IArray<Vector3D> Transform(this IArray<Vector3D> points, Transform3D t)
            => points.Map(p => t.TransformPoint(p));

        public static IArray2D<T> ToArray2D<T>(this IArray<IArray<T>> rows)
        {
            if (rows.Count == 0)
                return new Array2D<T>(0, 0, (i, j) => default);
            var nColumns = rows[0].Count;
            if (!rows.All(xs => xs.Count == nColumns))
                throw new Exception("All rows must have the same length");
            return new Array2D<T>(nColumns, rows.Count, (i, j) => rows[j][i]);
        }

        public static Transform3D FractionalRotation(Number numerator, Number denominator, Vector3D axis)
            => Quaternion.FromAxisAngle(axis, FractionalTurn(numerator, denominator)).ToTransform3D();

        public static Transform3D FractionalRotation(Number amount, Vector3D axis)
            => Quaternion.FromAxisAngle(axis, amount.Turns).ToTransform3D();

        public static QuadGrid SurfaceOfRevolution(this IArray<Vector3D> points, Vector3D axis, int segments,
            bool closedU = false, bool closedV = true)
            => segments
                .Interpolate()
                .Map(x => points.Transform(FractionalRotation(x, axis)))
                .ToArray2D()
                .ToQuadGrid(closedU, closedV);

        public static QuadGrid ToQuadGrid(this IArray2D<Vector3D> points, bool closedX, bool closedY)
            => new QuadGrid(points, closedX, closedY);

        public static IArray2D<T1> Map<T0, T1>(this IArray2D<T0> self, Func<T0, T1> f)
            => new Array2D<T1>(self.ColumnCount, self.RowCount, (i, j) => f(self.At(i, j)));

        public static IArray2D<T> CartesianProduct<T>(this Integer a, Integer b, Func<Integer, Integer, T> f)
            => a.Range.CartesianProduct(b.Range, f);

        public static QuadGrid ToQuadGrid(Func<Vector2D, Vector3D> f, Integer nGridSize, Boolean closedX,
            Boolean closedY)
            => nGridSize
                .CartesianProduct(nGridSize, (a, b) =>
                    new Vector2D(a.Value / (double)nGridSize, b.Value / (double)nGridSize))
                .Map(uv => f((uv.X, uv.Y)))
                .ToQuadGrid(closedX, closedY);

        public static QuadGrid Sphere(int resolution)
            => ToQuadGrid(SphereFunction, resolution, true, true);

        public static QuadGrid Torus(int resolution)
            => ToQuadGrid(uv => TorusFunction(uv, 1.0, 0.2), resolution, true, true);

        public static QuadGrid Plane(int resolution)
            => ToQuadGrid(PlaneXYFunction, resolution, true, true);

        public static QuadGrid Cylinder(int resolution)
            => ToQuadGrid(CylinderFunction, resolution, true, true);

        public static QuadGrid Capsule(int resolution)
            => ToQuadGrid(CapsuleFunction, resolution, true, true);

        public static QuadGrid Disc(int resolution)
            => ToQuadGrid(uv => DiscFunction(uv), resolution, true, false);

        public static Vector3D SphereFunction(this Vector2D uv)
            => SphereFunction(uv.X.Turns, uv.Y.Turns);

        public static Vector3D SphereFunction(Angle u, Angle v)
            => (-u.Cos * v.Sin, v.Cos, u.Sin * v.Sin);

        // https://en.wikipedia.org/wiki/Torus#Geometry
        public static Vector3D TorusFunction(this Vector2D uv, Number r1, Number r2)
            => TorusFunction(uv.X.Turns, uv.Y.Turns, r1, r2);

        public static Vector3D TorusFunction(Angle u, Angle v, Number r1, Number r2)
            => ((r1 + r2 * u.Cos) * v.Cos,
                (r1 + r2 * u.Cos) * v.Sin,
                r2 * u.Sin);

        public static Vector3D PlaneXYFunction(this Vector2D uv)
            => uv;

        public static Vector2D DiscFunction(this Vector2D uv)
            => uv.X.Turns.CircleFunction * (1 - uv.Y);

        public static Vector3D CylinderFunction(this Vector2D uv)
            => ((Vector3D)uv.X.Turns.CircleFunction).WithZ(uv.Y);

        public static Vector3D ConicalSectionFunction(this Vector2D uv, Number r1, Number r2)
            => (uv.X.CircleFunction * r1.Lerp(r2, uv.Y)).Vector3D.WithZ(uv.Y);

        public static Vector3D CapsuleFunction(this Vector2D uv)
        {
            uv *= (1, 2);
            if (uv.Y < 0.5) return SphereFunction((uv.Y, uv.X));
            if (uv.Y > 1.5) return SphereFunction((uv.Y - 1, uv.X)) + (0, 0, 1);
            return (uv + (0, -0.5f)).CylinderFunction();
        }

        public static TriangleMesh ToTriangleMesh(this IQuadMesh q)
        {
            var vertices = q.Points;
            var nFaces = q.Indices.Count / 4;
            var nxs = q.Indices;
            var indices = nFaces.Range.FlatMap(f => Intrinsics.MakeArray(
                nxs[f * 4 + 0], nxs[f * 4 + 1], nxs[f * 4 + 2],
                nxs[f * 4 + 2], nxs[f * 4 + 3], nxs[f * 4 + 0]));
            return new TriangleMesh(vertices, indices);
        }

        // TODO: I'm not confident about this 
        public static Vector3D Barycentric(this Quad3D q, Vector2D uv)
            => q.A * (1 - uv.X) * (1 - uv.Y) + q.B * uv.X * (1 - uv.Y) + q.C * uv.X * uv.Y + q.D * (1 - uv.X) * uv.Y;

        public static QuadGrid ToGrid(this Quad3D quad, IArray2D<Vector2D> uvs)
            => uvs.Map(uv => quad.Barycentric(uv)).ToQuadGrid(false, false);

        public static IArray<T> Prepend<T>(this IArray<T> self, T value)
            => (self.Count + 1).MapRange(i => i == 0 ? value : self[i - 1]);

        public static IArray<T> Append<T>(this IArray<T> self, T value)
            => (self.Count + 1).MapRange(i => i < self.Count ? self[i] : value);

        public static IArray<T> PrependAndAppend<T>(this IArray<T> self, T before, T after)
            => self.Prepend(before).Append(after);

        public static IArray<Number> PrependAndAppend01(this IArray<Number> self)
            => self.PrependAndAppend(0, 1);

        public static QuadGrid Subdivide(this Quad3D quad, IArray<Number> us, IArray<Number> vs)
        {
            return quad.ToGrid(
                us.PrependAndAppend01().CartesianProduct(
                    vs.PrependAndAppend01(), (u, v) => new Vector2D(u, v)));
        }

        // A handful of transforms things 

        public static Vector3D TransformPoint(this Transform3D transform, Vector3D point)
            => transform.TransformVector(point) + transform.Translation;

        public static Vector3D TransformVector(this Transform3D transform, Vector3D vector)
            => transform.Rotation.Transform(vector * transform.Scale);

        public static Vector3D Transform(this Rotation3D rotation, Vector3D v)
            => rotation.Quaternion.Transform(v);

        //==
        // Array helpers 

        public static IArray<Integer> Indices<T>(this IArray<T> self)
            => self.Count.Range;

        public static IArray<T> EveryNth<T>(this IArray<T> self, int n)
            => self.Indices().Map(i => self.ModuloAt(i * n));

        public static IArray2D<TResult> CartesianProduct<TColumn, TRow, TResult>(this IArray<TColumn> columns, IArray<TRow> rows, Func<TColumn, TRow, TResult> func)
            => new Array2D<TResult>(columns.Count, rows.Count, (i, j) => func(columns[i], rows[j]));

        public static Vector3D LerpAlong(this Line3D self, double t)
            => self.A.Lerp(self.B, t);

        public static Vector2D LerpAlong(this Line2D self, double t)
            => self.A.Lerp(self.B, t);

        public static T ModuloAt<T>(this IArray<T> a, Integer i)
            => a.At(i % a.Count);

        public static IArray<Vector3D> Normalize(this IArray<Vector3D> vectors)
            => vectors.Map(v => v.Normalize);

        public static IArray<Number> Interpolate(this int count)
            => InterpolateExclusive(count);

        public static IArray<Number> Interpolate(this Integer count, Boolean inclusive)
            => inclusive ? count.InterpolateInclusive() : count.InterpolateExclusive(); 

        public static IArray<Number> InterpolateInclusive(this Integer count)
            => count <= 0
                ? Intrinsics.MakeArray<Number>()
                : count == 1
                    ? Intrinsics.MakeArray<Number>(0.0)
                    : count.MapRange(i => i / (Number)(count - 1));

        public static IArray<Number> InterpolateExclusive(this Integer count)
            => count <= 0
                ? Intrinsics.MakeArray<Number>()
                : count == 1
                    ? Intrinsics.MakeArray<Number>(0.0)
                    : count.MapRange(i => i / (Number)(count));

        public static IArray<Vector3D> InterpolateInclusive(this Integer count, Func<Number, Vector3D> function)
            => count.InterpolateInclusive().Map(function);

        public static IArray<Vector3D> Interpolate(this Line3D self, Integer count)
            => count.InterpolateInclusive(x => self.LerpAlong(x));

        public static IArray<T> Reverse<T>(this IArray<T> self)
            => self.Indices().Map(i => self[self.Count - 1 - i]);

        public static IArray<T> Concat<T>(this IArray<T> self, IArray<T> other)
            => (self.Count + other.Count).MapRange(i => i < self.Count ? self[i] : other[i - self.Count]);

        public static IArray<T> Sample<T>(this IProcedural<Number, T> self, Integer n)
            => n.InterpolateExclusive().Map(self.Eval);

        //==
        // Primitives to points 

        public static PointArray Points(this IArray<Triangle3D> self)
            => self.FlatMap(t => t).ToPoints();

        public static PointArray Points(this IArray<Quad3D> self)
            => self.FlatMap(t => t).ToPoints();

        public static PointArray ToPoints(this IArray<Vector3D> self)
            => new PointArray(self);

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

        public static TriangleMesh ToMesh(this IArray<Triangle3D> self)
            => self.Points().ToTriangleMesh();

        public static TriangleMesh ToTriangleMesh(this IArray<Vector3D> points, IArray<Integer> indices)
            => new TriangleMesh(points, indices);

        public static TriangleMesh ToTriangleMesh(this IArray<Vector3D> points)
            => points.ToTriangleMesh(points.Indices());

        public static TriangleMesh DoubleSided(this TriangleMesh mesh)
            => mesh.Points.ToTriangleMesh(mesh.Indices.Concat(mesh.Indices.Reverse()));

        public static TriangleMesh FlipWindingOrder(this TriangleMesh mesh)
            => mesh.Points.ToTriangleMesh(mesh.Indices.Reverse());

        public static QuadMesh ToQuadMesh(this IArray<Vector3D> points, IArray<Integer> indices)
            => new QuadMesh(points, indices);

        public static QuadMesh ToQuadMesh(this IArray<Vector3D> points)
            => points.ToQuadMesh(points.Indices());

        //==
        // Transformation extension functions 

        public static T Translate<T>(this T self, Vector3D v) where T : ITransformable3D<T>
            => self.Transform(Matrix4x4.CreateTranslation(v));

        public static T Rotate<T>(this T self, Rotation3D r) where T : ITransformable3D<T>
            => self.Transform(Matrix4x4.CreateRotation(r));

        public static Vector3D GetAxis(this int n)
            => n == 0 ? Vector3D.UnitX
                : n == 1 ? Vector3D.UnitY
                : n == 2 ? Vector3D.UnitZ
                : throw new Exception("Invalid axis");

        public static Vector3D Translate(this Vector3D self, Vector3D v)
            => self + v;

        public static T Rotate<T>(this T self, Ray3D ray, Angle angle) where T : ITransformable3D<T>
            => self.Translate(-ray.Position).Rotate(ray.Direction, angle).Translate(ray.Position);

        public static T Rotate<T>(this T self, Vector3D axis, Angle angle) where T : ITransformable3D<T>
            => self.Rotate((axis, angle));

        public static T Rotate<T>(this T self, AxisAngle axisAngle) where T : ITransformable3D<T>
            => self.Transform(Matrix4x4.CreateRotation(axisAngle));

        public static T Scale<T>(this T self, Number n) where T : ITransformable3D<T>
            => self.Scale((n, n, n));

        public static T Scale<T>(this T self, Vector3D v) where T : ITransformable3D<T>
            => self.Transform(Matrix4x4.CreateScale(v));

        //==
        // Extra Array<Vector3D> functions

        public static Vector3D Sum(this IArray<Vector3D> self)
            => self.Aggregate(Vector3D.Default, (a, b) => a + b);

        public static Vector3D Average(this IArray<Vector3D> self)
            => self.Sum() / self.Count;

        public static IArray<Vector3D> To3D(this IArray<Vector2D> self)
            => self.Map(v => v.To3D);

        //==
        // Bounds functions

        public static Bounds3D Bounds(this IArray<Vector3D> self)
        {
            if (self.Count == 0)
                return Bounds3D.Default;
            var tmp = self[0];
            var min = tmp;
            var max = tmp;
            for (var i = 1; i < self.Count; i++)
            {
                min = Min(min, self[i]);
                max = Max(max, self[i]);
            }

            return (min, max);
        }

        public static Vector3D Min(Vector3D a, Vector3D b)
            => new Vector3D(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));

        public static Vector3D Max(Vector3D a, Vector3D b)
            => new Vector3D(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));

        //==
        // Deformations 

        public static Vector3D InverseLerp(this Bounds3D b, Vector3D v)
            => (v - b.Min) / b.Size;

        public static T DeformAlong<T>(this IDeformable3D<T> self, Bounds3D b, int axis,
            Func<Vector3D, Number, Vector3D> f)
            where T : IDeformable3D<T>
            => self.Deform(v => f(v, b.InverseLerp(v)[axis]));

        public static T Twist<T>(this IDeformable3D<T> self, Bounds3D b, int axis, Angle amount)
            where T : IDeformable3D<T>
            => self.DeformAlong(b, axis, (v, t) => v.Rotate(axis.GetAxis(), amount * t));

        public static T Skew<T>(this IDeformable3D<T> self, Bounds3D b, int axis, Vector3D offset)
            where T : IDeformable3D<T>
            => self.DeformAlong(b, axis, (v, t) => v.Lerp(v.Translate(offset), t));

        public static T Taper<T>(this IDeformable3D<T> self, Bounds3D b, int axis, Number scl)
            where T : IDeformable3D<T>
            => self.DeformAlong(b, axis, (v, t) => v.Lerp(v.Scale(scl), t));

        //==
        // Quad strips

        public static QuadGrid QuadStrip(this IArray<Vector3D> bottom, IArray<Vector3D> upper, bool closed)
        {
            if (bottom.Count != upper.Count)
                throw new Exception("Bottom and upper arrays must have the same length");
            return Intrinsics.MakeArray(bottom, upper).ToArray2D().ToQuadGrid(closed, false);
        }

        public static QuadGrid Extrude(this PolyLine3D polyLine, Vector3D direction)
            => polyLine.Sweep(v => v.Translate(direction));

        public static QuadGrid Sweep(this PolyLine3D polyLine, Func<Vector3D, Vector3D> f)
            => polyLine.Points.QuadStrip(polyLine.Deform(f).Points, polyLine.Closed);

        public static QuadGrid Extrude(this PolyLine2D polyLine, Vector3D direction)
            => polyLine.To3D.Extrude(direction);

        public static QuadGrid Extrude(this IArray<Vector3D> points, Vector3D direction, bool closed)
            => points.ToPolyLine3D(closed).Extrude(direction);

        public static QuadGrid ToPrism(this IArray<Vector3D> points)
            => points.ToPrism(1.0);

        public static QuadGrid ToPrism(this IArray<Vector3D> points, Number extrusionAmount)
            => points.Extrude(Vector3D.UnitZ * extrusionAmount, true);

        public static QuadGrid Sweep(this PolyLine2D polyLine, Func<Vector3D, Number, Vector3D> f, Integer segments, Boolean closed)
            => polyLine.To3D.Sweep(f, segments, closed);

        public static QuadGrid Sweep(this PolyLine3D polyLine, Func<Vector3D, Number, Vector3D> f, Integer segments, Boolean closed)
            => segments
                .InterpolateExclusive()
                .Map(t => polyLine.Deform(p => f(p, t)).Points)
                .ToArray2D()
                .ToQuadGrid(polyLine.Closed, closed);

        public static QuadGrid Sweep(this PolyLine3D polyLine, Ray3D pointAndAxis, Integer segments, Angle amount)
            => polyLine.Sweep((p, t) => p.Rotate(pointAndAxis, amount * t), segments, false);

        //==
        // Fans and Pyramids

        public static TriangleMesh ToPyramid(this IArray<Vector3D> points)
            => points.ToPyramid(1);

        public static TriangleMesh ToPyramid(this IArray<Vector3D> points, Number height)
            => points.ToFan(Vector3D.UnitZ * height, true);

        public static IArray<Integer> FanIndices(Integer count, Boolean connected)
        {
            var r = new List<Integer>();
            for (var i = 0; i < count - 2; i++)
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

        public static TriangleMesh ToFan(this IArray<Vector3D> points)
            => points.ToFan(true);

        public static TriangleMesh ToFan(this IArray<Vector3D> points, Boolean connected)
            => points.ToFan(points.Average(), connected);

        public static TriangleMesh ToFan(this IArray<Vector3D> points, Vector3D top, Boolean connected)
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
            var p1 = line1.LerpAlong(t1);
            var p2 = line2.LerpAlong(t2);
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
        // Curve functions

        public static PolyLine3D ToPolyLine3D(this ICurve3D self, Integer count)
            => self.Sample(count).ToPolyLine3D(self.Closed);
        
        public static PolyLine3D ToPolyLine3D(this ICurve2D self, Integer count)
            => self.Sample(count).To3D().ToPolyLine3D(self.Closed);

        public static PolyLine2D ToPolyLine2D(this ICurve2D self, Integer count)
            => self.Sample(count).ToPolyLine2D(self.Closed);

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

    }
}
