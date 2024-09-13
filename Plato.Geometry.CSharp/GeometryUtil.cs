using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plato.DoublePrecision;

namespace Plato.Geometry
{
    public enum StandardPlane
    {
        Xy,
        Xz,
        Yz,
    }

    public static class GeometryUtil
    {
        public static Array2D<V> CartesianProduct<T, U, V>(this Array<T> self, Array<U> other, Func<U, T, V> func)
            => New.Array(self.Count, other.Count, (i, j) => func(other.At(j), self.At(i)));

        public static Vector3D LerpAlong(this Line3D self, double t)
            => self.A.Lerp(self.B, t);

        public static Vector2D LerpAlong(this Line2D self, double t)
            => self.A.Lerp(self.B, t);

        public static T ModuloAt<T>(this Array<T> a, Integer i)
            => a.At(i % a.Count);

        public static Array<Vector3D> Normalize(this Array<Vector3D> vectors)
            => vectors.Map(v => v.Normalize);

        public static Vector3D Average(this Vector3D a, Vector3D b)
            => (a + b) / 2;

        public static Vector2D Average(this Vector2D a, Vector2D b)
            => (a + b) / 2;

        /*
        public static bool SequenceAlmostEquals(this Array<Vector3D> vs1, Array<Vector3D> vs2, double tolerance)
            => vs1.Count == vs2.Count && vs1.Indices().All(i => vs1[i].AlmostEquals(vs2[i], tolerance));

        public static bool AreColinear(this IEnumerable<Vector3D> vectors, Vector3D reference,
            double tolerance)
            => !reference.IsNaN() && vectors.All(v => v.Colinear(reference, tolerance));

        public static bool AreColinear(this IEnumerable<Vector3D> vectors,
            double tolerance = (double)Constants.OneTenthOfADegree)
            => vectors.ToList().AreColinear(tolerance);

        public static bool AreColinear(this System.Collections.Generic.IList<Vector3D> vectors,
            double tolerance = (double)Constants.OneTenthOfADegree)
            => vectors.Count <= 1 || vectors.Skip(1).AreColinear(vectors[0], tolerance);

        public static AABox BoundingBox(this Array<Vector3D> vertices)
            => AABox.Create(vertices.ToEnumerable());

          public static Array<Vector3D> Rotate(this Array<Vector3D> self, Vector3D axis, double angle)
               => self.Transform(Matrix4x4.CreateFromAxisAngle(axis, angle));

           public static Array<Vector3D> Transform(this Array<Vector3D> self, Matrix4x4 matrix)
               => self.Select(x => x.Transform(matrix));

           public static Int3 Sort(this Int3 v)
           {
               if (v.X < v.Y)
               {
                   return (v.Y < v.Z)
                       ? new Int3(v.X, v.Y, v.Z)
                       : (v.X < v.Z)
                           ? new Int3(v.X, v.Z, v.Y)
                           : new Int3(v.Z, v.X, v.Y);
               }
               else
               {
                   return (v.X < v.Z)
                       ? new Int3(v.Y, v.X, v.Z)
                       : (v.Y < v.Z)
                           ? new Int3(v.Y, v.Z, v.X)
                           : new Int3(v.Z, v.Y, v.X);
               }
           }*/

        public static Array<double> Interpolate(this int count)
            => InterpolateExclusive(count);

        public static Array<double> InterpolateInclusive(this int count)
            => count <= 0
                ? New.Array<double>()
                : count == 1
                    ? New.Array(0.0)
                    : count.Map(i => i / (double)(count - 1));

        public static Array<double> InterpolateExclusive(this int count)
            => count <= 0
                ? New.Array<double>()
                : count.Map(i => i / (double)count);

        public static Array<Vector3D> InterpolateInclusive(this int count, Func<double, Vector3D> function)
            => count.InterpolateInclusive().Map(function);

        public static Array<Vector3D> Interpolate(this Line3D self, int count)
            => count.InterpolateInclusive(x => self.LerpAlong(x));


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
            point = p1.Average(p2);

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
            var closestPoint = a + t * (b - a); // Projection falls on the segment
            return (p - closestPoint).Length;
        }
        
        /*
        public static Array<Vector2D> Offset(this Array<Vector2D> points, double offset, bool closed)
        {
            if (points.Count < 2) return Intrinsics.MakeArray<Vector2D>();
            var lines = points.ToLines(closed).Map(line => line.ParallelOffset(offset));
            var r = new List<Vector2D>();
            var n = lines.Count - (closed ? 0 : 1);

            if (!closed)
            {
                r.Add(lines[0].A);
            }

            for (var i = 0; i < n; ++i)
            {
                var line1 = lines[i];
                var line2 = lines.ElementAtModulo(i + 1);
                var intersects = line1.Intersection(line2, out var intersection);
                if (intersects)
                {
                    // They interesect
                    r.Add(intersection);
                }
                else
                {
                    // NOTE: this should be exceedingly rare or impossible.
                    // We probably have virtually coincident points, or maybe a bug in the line algorithm.
                    Debugger.Break();

                    // If we couldn't determine an intersection point 
                    // Add the end of the first line, and the beginning of the next
                    r.Add(line1.B);
                    r.Add(line2.A);
                }
            }

            if (!closed)
            {
                r.Add(lines.Last().B);
            }

            return Intrinsics.MakeArray(r.ToArray());
        }
        */

        public static Array<Line2D> ToLines(this Array<Vector2D> points, bool closed)
            => (points.Count - (closed ? 0 : 1)).Map(i => new Line2D(points.At(i), points.ModuloAt(i + 1)));
    }
}
