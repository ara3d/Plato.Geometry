using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Plato.DoublePrecision;
using Plato.Geometry.Scenes;
using Boolean = Plato.DoublePrecision.Boolean;

namespace Plato.Geometry
{
    /// <summary>
    /// Many of these functions, once validated, will move into the Plato library.
    /// </summary>
    public static class Extensions
    {
        public static IArray<T> ToIArray<T>(this IReadOnlyList<T> self) 
            => new ListArray<T>(self);
        
        public static IArray<T> ToIArray<T>(this T[] self) 
            => new PrimitiveArray<T>(self);

        public static IArray<T> Repeat<T>(this T self, int count)
            => ((Integer)count).MapRange(_ => self);

        public static IArray<T> MapRange<T>(this int count, System.Func<int, T> func)
            => ((Integer)count).MapRange(i => func(i));

        public static QuadGrid UpArrow(double length, double minorRadius, double majorRadius, int radialSegments, double percentTail)
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
            var n = rows[0].Count;
            if (!rows.All(xs => xs.Count == n))
                throw new Exception("All rows must have the same length");
            return new Array2D<T>(n, rows.Count, (i, j) => rows[j][i]);
        }

        public static Angle FractionalTurn(Number numerator, Number denominator)
            => (numerator / denominator).Turns;

        public static Transform3D FractionalRotation(Number numerator, Number denominator, Vector3D axis)
            => Quaternion.FromAxisAngle(axis, FractionalTurn(numerator, denominator)).ToTransform3D();

        public static Transform3D FractionalRotation(Number amount, Vector3D axis)
            => Quaternion.FromAxisAngle(axis, amount.Turns).ToTransform3D();

        public static QuadGrid SurfaceOfRevolution(this IArray<Vector3D> points, Vector3D axis, int segments, bool closedU = false, bool closedV = true)
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
        
        public static QuadGrid ToQuadGrid(Func<Vector2D, Vector3D> f, Integer nGridSize, Boolean closedX, Boolean closedY)
            => nGridSize
                .CartesianProduct(nGridSize, (a, b) => 
                    new Vector2D(a.Value / (double)nGridSize, b.Value / (double)nGridSize))
                .Map(uv => f((uv.X, uv.Y)))
                .ToQuadGrid(closedX, closedY);

        public static QuadGrid Sphere(int resolution)
            => ToQuadGrid(SphereFunction, resolution, true, true);

        public static QuadGrid Torus(int resolution)
            => ToQuadGrid(uv => TorusFunction(uv, 5.0, 0.5), resolution, true, true);

        public static QuadGrid Plane(int resolution)
            => ToQuadGrid(PlaneXYFunction, resolution, true, true);

        public static QuadGrid Cylinder(int resolution)
            => ToQuadGrid(CylinderFunction, resolution, true, true);

        public static QuadGrid Capsule(int resolution)
            => ToQuadGrid(CapsuleFunction, resolution, true, true);

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
            => uv.X.Turns.CircleFunction * uv.Y;

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

        public static Angle Turns(this int n)
            => ((Number)n).Turns;

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

        public static Vector3D Average(this IEnumerable<Vector3D> self)
        {
            var sum = Vector3D.Default;
            var cnt = 0;
            foreach (var x in self)
            {
                sum += x;
                cnt++;
            }
            return sum / cnt;
        }

        public static QuadGrid QuadStrip(this IArray<Vector3D> bottom, IArray<Vector3D> upper, bool closed)
        {
            if (bottom.Count != upper.Count)
                throw new Exception("Bottom and upper arrays must have the same length");

            var r = Intrinsics.MakeArray(bottom, upper).ToArray2D().ToQuadGrid(closed, false);
            Debug.Assert(r.NumColumns == bottom.Count);
            Debug.Assert(r.NumRows == 2);
            return r;
        }

        public static QuadGrid QuadStrip(this Quad3D q)
            => QuadStrip(Intrinsics.MakeArray(q.A, q.B), Intrinsics.MakeArray(q.D, q.C), false);
        
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
    }
}
