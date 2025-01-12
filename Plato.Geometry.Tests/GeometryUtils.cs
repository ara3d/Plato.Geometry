using System.Numerics;
using Plato.SinglePrecision;
using Line = Ara3D.Mathematics.Line;

namespace Plato.Geometry.Tests.Plato.Geometry
{
    public static class GeometryUtils
    {
        /// <summary>
        /// Computes the union of bounding boxes (element-wise).
        /// For each index i, the union encloses a[i] and b[i].
        /// </summary>
        public static void Union(Bounds3D[] a, Bounds[] b, out Bounds[] r)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Arrays must have the same length.");

            r = new Bounds[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                var min = Vector3.Min(a[i].Min, b[i].Min);
                var max = Vector3.Max(a[i].Max, b[i].Max);
                r[i] = new Bounds { Min = min, Max = max };
            }
        }

        /// <summary>
        /// Computes the union (smallest enclosing sphere) of each pair of spheres a[i] and b[i].
        /// </summary>
        public static void Union(Sphere[] a, Sphere[] b, out Sphere[] r)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Arrays must have the same length.");

            r = new Sphere[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                r[i] = ComputeEnclosingSphere(a[i], b[i]);
            }
        }

        /// <summary>
        /// Computes a bounding box that encloses an array of Vector3 points.
        /// </summary>
        public static Bounds ComputeBounds(Vector3[] v)
        {
            if (v == null || v.Length == 0)
                throw new ArgumentException("Array of points must be non-empty.");

            Vector3 minV = new Vector3(float.MaxValue);
            Vector3 maxV = new Vector3(float.MinValue);

            foreach (var p in v)
            {
                minV = Vector3.Min(minV, p);
                maxV = Vector3.Max(maxV, p);
            }

            return new Bounds { Min = minV, Max = maxV };
        }

        /// <summary>
        /// Computes a (naive) bounding sphere for an array of Vector3 points.
        /// Center is average of points; radius is max distance from center.
        /// </summary>
        public static Sphere ComputeSphere(Vector3[] v)
        {
            if (v == null || v.Length == 0)
                throw new ArgumentException("Array of points must be non-empty.");

            // Compute center via average
            Vector3 center = Vector3.Zero;
            foreach (var p in v)
                center += p;
            center /= v.Length;

            // Compute radius
            float maxDist = 0;
            foreach (var p in v)
            {
                float dist = Vector3.Distance(p, center);
                if (dist > maxDist) maxDist = dist;
            }

            return new Sphere { Center = center, Radius = maxDist };
        }

        /// <summary>
        /// Computes the distance from each Triangle t[i] to each point p[i], storing results in r[i].
        /// </summary>
        public static void Distance(Triangle[] t, Vector3[] p, out float[] r)
        {
            if (t.Length != p.Length)
                throw new ArgumentException("Triangles and points arrays must have same length.");

            r = new float[t.Length];
            for (int i = 0; i < t.Length; i++)
            {
                r[i] = DistanceTrianglePoint(t[i], p[i]);
            }
        }

        /// <summary>
        /// Computes the distance from each Line l[i] to each point p[i], storing results in r[i].
        /// </summary>
        public static void Distance(Line[] l, Vector3[] p, out float[] r)
        {
            if (l.Length != p.Length)
                throw new ArgumentException("Lines and points arrays must have same length.");

            r = new float[l.Length];
            for (int i = 0; i < l.Length; i++)
            {
                r[i] = DistanceLinePoint(l[i], p[i]);
            }
        }

        /// <summary>
        /// Computes the distance from each Line a[i] to each Line b[i], storing results in r[i].
        /// </summary>
        public static void Distance(Line[] a, Line[] b, out float[] r)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Line arrays must have the same length.");

            r = new float[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                r[i] = DistanceLineLine(a[i], b[i]);
            }
        }

        /// <summary>
        /// Computes a normal (as a unit vector) for each Triangle t[i].
        /// </summary>
        public static void ComputeNormal(Triangle[] t, out Vector3[] r)
        {
            r = new Vector3[t.Length];
            for (int i = 0; i < t.Length; i++)
            {
                var edge1 = t[i].B - t[i].A;
                var edge2 = t[i].C - t[i].A;
                var n = Vector3.Cross(edge1, edge2);
                if (n.LengthSquared() > 1e-8f)
                    n = Vector3.Normalize(n);
                r[i] = n;
            }
        }

        // ------------------------------------------------------------------------
        // Private / Helper Methods
        // ------------------------------------------------------------------------

        /// <summary>
        /// Computes the minimal bounding sphere that encloses sphere A and B.
        /// Standard formula if the spheres do not fully contain each other.
        /// </summary>
        private static Sphere ComputeEnclosingSphere(Sphere A, Sphere B)
        {
            var cA = A.Center;
            var cB = B.Center;
            var rA = A.Radius;
            var rB = B.Radius;

            Vector3 dir = cB - cA;
            float dist = dir.Length();

            // If one sphere is already inside the other, pick the bigger
            if (dist + rA <= rB)
                return B;
            if (dist + rB <= rA)
                return A;

            // Otherwise, find smallest sphere that encloses both.
            // Center is along the line connecting cA -> cB.
            float alpha = (dist + rA - rB) / (2f * dist);
            Vector3 center = cA + alpha * dir;
            float radius = (dist + rA + rB) / 2f;

            return new Sphere { Center = center, Radius = radius };
        }

        /// <summary>
        /// Computes distance from a point to a triangle (A,B,C).
        /// Simple approach using:
        ///   1) project p onto triangle plane,
        ///   2) if inside, distance = distance to plane,
        ///   3) else, distance = min distance to edges.
        /// </summary>
        private static float DistanceTrianglePoint(Triangle tri, Vector3 p)
        {
            // Edges
            Vector3 A = tri.A;
            Vector3 B = tri.B;
            Vector3 C = tri.C;

            // Normal
            var n = Vector3.Cross(B - A, C - A);
            if (n.LengthSquared() < 1e-8f)
            {
                // Degenerate triangle. Fall back on distance to any point (say A).
                return Vector3.Distance(p, A);
            }
            n = Vector3.Normalize(n);

            // Distance from plane
            float distPlane = MathF.Abs(Vector3.Dot(p - A, n));

            // Check if projection is inside the triangle via barycentric coordinates
            var (u, v, w) = Barycentric(p, A, B, C);
            if (u >= 0 && v >= 0 && w >= 0)
            {
                // inside or on the triangle
                return distPlane;
            }
            else
            {
                // outside the triangle: distance to nearest edge
                float distAB = DistancePointSegment(p, A, B);
                float distBC = DistancePointSegment(p, B, C);
                float distCA = DistancePointSegment(p, C, A);
                return MathF.Min(distAB, MathF.Min(distBC, distCA));
            }
        }

        /// <summary>
        /// Distance from a point to a segment [A,B].
        /// </summary>
        private static float DistancePointSegment(Vector3 p, Vector3 A, Vector3 B)
        {
            var AB = B - A;
            float denom = Vector3.Dot(AB, AB);
            if (denom < 1e-8f)
                return Vector3.Distance(p, A);

            float t = Vector3.Dot(p - A, AB) / denom;
            t = MathF.Max(0, MathF.Min(1, t));
            var closest = A + t * AB;
            return Vector3.Distance(p, closest);
        }

        /// <summary>
        /// Returns barycentric coordinates (u, v, w) such that
        /// p = u*A + v*B + w*C, with u+v+w=1.
        /// If inside the triangle, all of u,v,w >= 0.
        /// </summary>
        private static (float u, float v, float w) Barycentric(Vector3 p, Vector3 A, Vector3 B, Vector3 C)
        {
            var v0 = B - A;
            var v1 = C - A;
            var v2 = p - A;

            float d00 = Vector3.Dot(v0, v0);
            float d01 = Vector3.Dot(v0, v1);
            float d11 = Vector3.Dot(v1, v1);
            float d20 = Vector3.Dot(v2, v0);
            float d21 = Vector3.Dot(v2, v1);
            float denom = d00 * d11 - d01 * d01;

            if (MathF.Abs(denom) < 1e-8f)
            {
                // Degenerate
                return (0, 0, 0);
            }

            float invDenom = 1f / denom;
            float v = (d11 * d20 - d01 * d21) * invDenom;
            float w = (d00 * d21 - d01 * d20) * invDenom;
            float u = 1f - v - w;
            return (u, v, w);
        }

        /// <summary>
        /// Distance from a point to a line segment l (A,B).
        /// </summary>
        private static float DistanceLinePoint(Line l, Vector3 p)
        {
            return DistancePointSegment(p, l.A, l.B);
        }

        /// <summary>
        /// Distance between two line segments a and b in 3D.
        /// This is the minimal distance between any points pa on a and pb on b.
        /// </summary>
        private static float DistanceLineLine(Line la, Line lb)
        {
            // Let la = [A1, A2], lb = [B1, B2]
            Vector3 A1 = la.A, A2 = la.B;
            Vector3 B1 = lb.A, B2 = lb.B;
            // Adapted from standard line-segment to line-segment distance approach

            Vector3 u = A2 - A1; // direction of segment A
            Vector3 v = B2 - B1; // direction of segment B
            Vector3 w = A1 - B1;

            float a = Vector3.Dot(u, u);    // |u|^2
            float b = Vector3.Dot(u, v);
            float c = Vector3.Dot(v, v);    // |v|^2
            float d = Vector3.Dot(u, w);
            float e = Vector3.Dot(v, w);

            float denom = a * c - b * b;

            float s, t;

            // If denom is zero or extremely small, the lines are almost parallel
            if (denom < 1e-8f)
            {
                s = 0.0f;
                t = (e / c);
            }
            else
            {
                s = (b * e - c * d) / denom;
                t = (a * e - b * d) / denom;
            }

            // Clamp s, t to [0,1] (segment endpoints)
            s = MathF.Max(0, MathF.Min(1, s));
            t = MathF.Max(0, MathF.Min(1, t));

            Vector3 closestA = A1 + s * u;
            Vector3 closestB = B1 + t * v;
            return Vector3.Distance(closestA, closestB);
        }
    }
}