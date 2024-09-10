using System;
using System.Collections.Generic;
using System.Diagnostics;
using Ara3D.Mathematics;

// https://github.com/NMO13/earclipper
// https://www.habrador.com/tutorials/math/10-triangulation/
// https://www.geometrictools.com/Documentation/TriangulationByEarClipping.pdf

namespace EarClipperLib
{
    // Implementation of Triangulation by Ear Clipping
    // by David Eberly
    public class EarClipping
    {
        private Polygon _mainPointList;
        private List<Polygon> _holes;
        private Vector2 Normal;
        public List<Vector2> Result { get; }
        public Dictionary<Vector2, List<ConnectionEdge>> IncidentEdges = new();
        
        public EarClipping(List<Vector2> points, List<List<Vector2>> holes = null)
        {
            if (points == null || points.Count < 3)
            {
                throw new ArgumentException("No list or an empty list passed");
            }

            _mainPointList = new Polygon();
            LinkAndAddToList(_mainPointList, points);

            if (holes != null)
            {
                _holes = new List<Polygon>();
                for (var i = 0; i < holes.Count; i++)
                {
                    var p = new Polygon();
                    LinkAndAddToList(p, holes[i]);
                    _holes.Add(p);
                }
            }

            Result = new List<Vector2>();
        }

        private void LinkAndAddToList(Polygon polygon, List<Vector2> points)
        {
            ConnectionEdge prev = null, first = null;
            for (var i = 0; i < points.Count; i++)
            {
                // we don't wanna have duplicates
                var p0 = points[i];
                IncidentEdges[p0]  = new List<ConnectionEdge>();
                var current = new ConnectionEdge(p0, polygon);
                IncidentEdges[p0].Add(current)
                    ;
                first = (i == 0) 
                    ? current 
                    : first; // remember first

                if (prev != null)
                {
                    prev.Next = current;
                }

                current.Prev = prev;
                prev = current;
            }

            first.Prev = prev;
            prev.Next = first;
            polygon.Start = first;
            polygon.PointCount = IncidentEdges.Count;
        }

        public void Triangulate()
        {
            if (_holes != null && _holes.Count > 0)
            {
                ProcessHoles();
            }

            var nonConvexPoints = FindNonConvexPoints(_mainPointList);

            if (nonConvexPoints.Count == _mainPointList.PointCount)
                throw new ArgumentException("The triangle input is not valid");

            while (_mainPointList.PointCount > 2)
            {
                var guard = false;
                foreach (var cur in _mainPointList.GetPolygonCirculator())
                {
                    if (!IsConvex(cur))
                        continue;

                    if (!IsPointInTriangle(cur.Prev.Origin, cur.Origin, cur.Next.Origin, nonConvexPoints))
                    {
                        // cut off ear
                        guard = true;
                        Result.Add(cur.Prev.Origin);
                        Result.Add(cur.Origin);
                        Result.Add(cur.Next.Origin);

                        // Check if prev and next are still nonconvex. If not, then remove from non convex list
                        if (IsConvex(cur.Prev))
                        {
                            var index = nonConvexPoints.FindIndex(x => x == cur.Prev);
                            if (index >= 0)
                                nonConvexPoints.RemoveAt(index);
                        }

                        if (IsConvex(cur.Next))
                        {
                            var index = nonConvexPoints.FindIndex(x => x == cur.Next);
                            if (index >= 0)
                                nonConvexPoints.RemoveAt(index);
                        }

                        _mainPointList.Remove(cur, IncidentEdges);
                        break;
                    }
                }

                if (PointsOnLine(_mainPointList))
                    break;

                if (!guard)
                    throw new Exception("No progression. The input must be wrong");
            }
        }

        private bool PointsOnLine(Polygon pointList)
        {
            foreach (var connectionEdge in pointList.GetPolygonCirculator())
            {
                if (Misc.GetOrientation(connectionEdge.Prev.Origin, connectionEdge.Origin, connectionEdge.Next.Origin,
                        Normal) != 0)
                    return false;
            }

            return true;
        }

        private bool IsConvex(ConnectionEdge curPoint)
            => Misc.GetOrientation(curPoint.Prev.Origin, curPoint.Origin, curPoint.Next.Origin, Normal) == 1;

        private void ProcessHoles()
        {
            for (var h = 0; h < _holes.Count; h++)
            {
                var polygons = new List<Polygon>();
                polygons.Add(_mainPointList);
                polygons.AddRange(_holes);
                ConnectionEdge M, P;
                GetVisiblePoints(h + 1, polygons, out M, out P);
                if (M.Origin.Equals(P.Origin))
                    throw new Exception();

                var insertionEdge = P;
                InsertNewEdges(insertionEdge, M);
                _holes.RemoveAt(h);
                h--;
            }
        }

        private void InsertNewEdges(ConnectionEdge insertionEdge, ConnectionEdge m)
        {
            insertionEdge.Polygon.PointCount += m.Polygon.PointCount;
            var cur = m;
            var forwardEdge = new ConnectionEdge(insertionEdge.Origin, insertionEdge.Polygon);
            IncidentEdges[insertionEdge.Origin].Add(forwardEdge);
            forwardEdge.Prev = insertionEdge.Prev;
            forwardEdge.Prev.Next = forwardEdge;
            forwardEdge.Next = m;
            forwardEdge.Next.Prev = forwardEdge;
            var end = insertionEdge;
            ConnectionEdge prev = null;
            do
            {
                cur.Polygon = insertionEdge.Polygon;
                prev = cur;
                cur = cur.Next;
            } while (m != cur);

            var backEdge = new ConnectionEdge(cur.Origin, insertionEdge.Polygon);
            IncidentEdges[cur.Origin].Add(backEdge);
            cur = prev;
            cur.Next = backEdge;
            backEdge.Prev = cur;
            backEdge.Next = end;
            end.Prev = backEdge;
        }

        private void GetVisiblePoints(int holeIndex, List<Polygon> polygons, out ConnectionEdge M, out ConnectionEdge P)
        {
            M = FindLargest(polygons[holeIndex]);

            var direction = (polygons[holeIndex].Start.Next.Origin - polygons[holeIndex].Start.Origin).Cross(Normal);
            var I = FindPointI(M, polygons, holeIndex, Vector2.Zero);

            Vector2 res;
            if (polygons[I.PolyIndex].Contains(I.I, out res))
            {
                var incidentEdges = IncidentEdges[res];
                foreach (var connectionEdge in incidentEdges)
                {
                    if (Misc.IsBetween(connectionEdge.Origin, connectionEdge.Next.Origin, connectionEdge.Prev.Origin,
                            M.Origin, Normal) == 1)
                    {
                        P = connectionEdge;
                        return;
                    }
                }

                throw new Exception();
            }

            P = FindVisiblePoint(I, polygons, M, Vector2.Zero);   
        }

        private ConnectionEdge FindVisiblePoint(Candidate I, List<Polygon> polygons, ConnectionEdge M, Vector2 direction)
        {
            ConnectionEdge P = null;

            if (I.Origin.Origin.Dot(direction).CompareTo(I.Origin.Next.Origin.Dot(direction)) > 0)
            {
                P = I.Origin;
            }
            else
            {
                P = I.Origin.Next;
            }

            var nonConvexPoints = FindNonConvexPoints(polygons[I.PolyIndex]);


            nonConvexPoints.Remove(P);

            var m = M.Origin;
            var i = I.I;
            var p = P.Origin;
            var candidates = new List<ConnectionEdge>();

            // invert i and p if triangle is oriented CW
            if (Misc.GetOrientation(m, i, p, Normal) == -1)
            {
                (i, p) = (p, i);
            }

            foreach (var nonConvexPoint in nonConvexPoints)
            {
                if (Misc.PointInOrOnTriangle(m, i, p, nonConvexPoint.Origin, Normal))
                {
                    candidates.Add(nonConvexPoint);
                }
            }

            if (candidates.Count == 0)
                return P;
            return FindMinimumAngle(candidates, m, direction);
        }

        private ConnectionEdge FindMinimumAngle(List<ConnectionEdge> candidates, Vector2 M, Vector2 direction)
        {
            var angle = -double.MaxValue;
            ConnectionEdge result = null;
            foreach (var R in candidates)
            {
                var a = direction;
                var b = R.Origin - M;
                var num = a.Dot(b) * a.Dot(b);
                var denom = b.Dot(b);
                var res = num / denom;
                if (res.CompareTo(angle) > 0)
                {
                    result = R;
                    angle = res;
                }
            }

            return result;
        }

        private Candidate FindPointI(ConnectionEdge M, List<Polygon> polygons, int holeIndex, Vector2 direction)
        {
            var candidate = new Candidate();
            for (var i = 0; i < polygons.Count; i++)
            {
                if (i == holeIndex) // Don't test the hole with itself
                    continue;
                foreach (var connectionEdge in polygons[i].GetPolygonCirculator())
                {
                    if (RaySegmentIntersection(out var intersectionPoint, out var rayDistanceSquared, M.Origin, direction,
                            connectionEdge.Origin,
                            connectionEdge.Next.Origin, direction))
                    {
                        if (rayDistanceSquared ==
                            candidate
                                .currentDistance) // if this is an M/I edge, then both edge and his twin have the same distance; we take the edge where the point is on the left side
                        {
                            if (Misc.GetOrientation(connectionEdge.Origin, connectionEdge.Next.Origin, M.Origin,
                                    Normal) == 1)
                            {
                                candidate.currentDistance = rayDistanceSquared;
                                candidate.Origin = connectionEdge;
                                candidate.PolyIndex = i;
                                candidate.I = intersectionPoint;
                            }
                        }
                        else if (rayDistanceSquared.CompareTo(candidate.currentDistance) < 0)
                        {
                            candidate.currentDistance = rayDistanceSquared;
                            candidate.Origin = connectionEdge;
                            candidate.PolyIndex = i;
                            candidate.I = intersectionPoint;
                        }
                    }
                }
            }

            return candidate;
        }

        private ConnectionEdge FindLargest(Polygon testHole)
        {
            var maximum = 0.0;
            ConnectionEdge maxEdge = null;
            var v0 = testHole.Start.Origin;
            var v1 = testHole.Start.Next.Origin;
            foreach (var connectionEdge in testHole.GetPolygonCirculator())
            {
                // we take the first two points as a reference line

                if (Misc.GetOrientation(v0, v1, connectionEdge.Origin, Normal) < 0)
                {
                    var r = Misc.PointLineDistance(v0, v1, connectionEdge.Origin);
                    if (r.CompareTo(maximum) > 0)
                    {
                        maximum = r;
                        maxEdge = connectionEdge;
                    }
                }
            }

            if (maxEdge == null)
                return testHole.Start;
            return maxEdge;
        }

        private bool IsPointInTriangle(Vector2 prevPoint, Vector2 curPoint, Vector2 nextPoint,
            List<ConnectionEdge> nonConvexPoints)
        {
            foreach (var nonConvexPoint in nonConvexPoints)
            {
                if (nonConvexPoint.Origin == prevPoint || nonConvexPoint.Origin == curPoint ||
                    nonConvexPoint.Origin == nextPoint)
                    continue;
                if (Misc.PointInOrOnTriangle(prevPoint, curPoint, nextPoint, nonConvexPoint.Origin, Normal))
                    return true;
            }

            return false;
        }

        private List<ConnectionEdge> FindNonConvexPoints(Polygon p)
        {
            var resultList = new List<ConnectionEdge>();
            foreach (var connectionEdge in p.GetPolygonCirculator())
            {
                if (Misc.GetOrientation(connectionEdge.Prev.Origin, connectionEdge.Origin, connectionEdge.Next.Origin,
                        Normal) != 1)
                    resultList.Add(connectionEdge);
            }

            return resultList;
        }

        public bool RaySegmentIntersection(out Vector2 intersection, out double distanceSquared,
            Vector2 linePoint1, Vector2 lineVec1, Vector2 linePoint3, Vector2 linePoint4, Vector2 direction)
        {
            var lineVec2 = linePoint4 - linePoint3;
            var lineVec3 = linePoint3 - linePoint1;
            var crossVec1and2 = lineVec1.ToVector3().Cross(lineVec2.ToVector3());
            var crossVec3and2 = lineVec3.ToVector3().Cross(lineVec2.ToVector3());

            var res = Misc.PointLineDistance(linePoint3, linePoint4, linePoint1);
            if (res.Abs() <= double.Epsilon) // line and ray are collinear
            {
                var p = linePoint1 + lineVec1;
                var res2 = Misc.PointLineDistance(linePoint3, linePoint4, p);
                if (res2.Abs() <= double.Epsilon)
                {
                    var s = linePoint3 - linePoint1;
                    if (s.X == direction.X && s.Y == direction.Y)
                    {
                        intersection = linePoint3;
                        distanceSquared = s.LengthSquared();
                        return true;
                    }
                }
            }

            //is coplanar, and not parallel
            if ( /*planarFactor == 0.0f && */crossVec1and2.LengthSquared() > 0)
            {
                var s = crossVec3and2.Dot(crossVec1and2) / crossVec1and2.LengthSquared();
                if (s >= 0)
                {
                    intersection = linePoint1 + (lineVec1 * s);
                    distanceSquared = (lineVec1 * s).LengthSquared();
                    if (((intersection - linePoint3).LengthSquared() + (intersection - linePoint4).LengthSquared())
                        .CompareTo(lineVec2.LengthSquared()) <=
                        0)
                        return true;
                }
            }

            intersection = Vector2.Zero;
            distanceSquared = 0;
            return false;
        }
    }

    public class Candidate
    {
        public double currentDistance = double.MaxValue;
        public Vector2 I;
        public ConnectionEdge Origin;
        public int PolyIndex;
    }

    public class ConnectionEdge
    {
        protected bool Equals(ConnectionEdge other)
        {
            return Next.Origin.Equals(other.Next.Origin) && Origin.Equals(other.Origin);
        }

        public Vector2 Origin { get; set; }
        public ConnectionEdge Prev;
        public ConnectionEdge Next;
        public Polygon Polygon { get; set; }

        public ConnectionEdge(Vector2 p0, Polygon parentPolygon)
        {
            Origin = p0;
            Polygon = parentPolygon;
        }

        public override string ToString()
        {
            return "Origin: " + Origin + " Next: " + Next.Origin;
        }
    }

    public class Polygon
    {
        public ConnectionEdge Start;
        public int PointCount = 0;

        public IEnumerable<ConnectionEdge> GetPolygonCirculator()
        {
            if (Start == null)
            {
                yield break;
            }

            var h = Start;
            do
            {
                yield return h;
                h = h.Next;
            } while (h != Start);
        }

        public void Remove(ConnectionEdge cur, Dictionary<Vector2, List<ConnectionEdge>> incidentEdgeLookup)
        {
            cur.Prev.Next = cur.Next;
            cur.Next.Prev = cur.Prev;
            var incidentEdges = incidentEdgeLookup[cur.Origin];
            var index = incidentEdges.IndexOf(cur);
            Debug.Assert(index >= 0);
            incidentEdges.RemoveAt(index);
            if (incidentEdges.Count == 0)
                PointCount--;
            if (cur == Start)
                Start = cur.Prev;
        }

        public bool Contains(Vector2 vector2M, out Vector2 res)
        {
            foreach (var connectionEdge in GetPolygonCirculator())
            {
                if (!connectionEdge.Origin.Equals(vector2M)) 
                    continue;
                res = connectionEdge.Origin;
                return true;
            }
            
            res = Vector2.Zero;
            return false;
        }
    }

    public static class Misc
    {
        public static int GetOrientation(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 normal)
        {
            var res = (v0 - v1).Cross(v2 - v1);
            if (res.LengthSquared() <= 0)
                return 0;
            if (res.X.Sign() != normal.X.Sign() || res.Y.Sign() != normal.Y.Sign() || res.Z.Sign() != normal.Z.Sign())
                return 1;
            return -1;
        }

        // Is testPoint between a and b in ccw order?
        // > 0 if strictly yes
        // < 0 if strictly no
        // = 0 if testPoint lies either on a or on b
        public static int IsBetween(Vector3 Origin, Vector3 a, Vector3 b, Vector3 testPoint, Vector3 normal)
        {
            var psca = GetOrientation(Origin, a, testPoint, normal);
            var pscb = GetOrientation(Origin, b, testPoint, normal);

            // where does b in relation to a line? Left, right or collinear?
            
            var psb = GetOrientation(Origin, a, b, normal);
            
            if (psb > 0) // left
            {
                // if left then testPoint lies between a and b iff testPoint left of a AND testPoint right of b
                if (psca > 0 && pscb < 0)
                    return 1;
                if (psca == 0)
                {
                    var t = a - Origin;
                    var t2 = testPoint - Origin;
                    if (t.X.Sign() != t2.X.Sign() || t.Y.Sign() != t2.Y.Sign())
                        return -1;
                    return 0;
                }

                if (pscb == 0)
                {
                    var t = b - Origin;
                    var t2 = testPoint - Origin;
                    if (t.X.Sign() != t2.X.Sign() || t.Y.Sign() != t2.Y.Sign())
                        return -1;
                    return 0;
                }
            }
            else if (psb < 0) // right
            {
                // if right then testPoint lies between a and b iff testPoint left of a OR testPoint right of b
                if (psca > 0 || pscb < 0)
                    return 1;

                if (psca == 0)
                {
                    var t = a - Origin;
                    var t2 = testPoint - Origin;
                    if (t.X.Sign() != t2.X.Sign() || t.Y.Sign() != t2.Y.Sign())
                        return 1;
                    return 0;
                }

                if (pscb == 0)
                {
                    var t = b - Origin;
                    var t2 = testPoint - Origin;
                    if (t.X.Sign() != t2.X.Sign() || t.Y.Sign() != t2.Y.Sign())
                        return 1;
                    return 0;
                }
            }
            else // (psb == 0)
            {
                if (psca > 0)
                    return 1;
                if (psca < 0)
                    return -1;
                return 0;
            }

            return -1;
        }

        public static bool PointInOrOnTriangle(Vector3 prevPoint, Vector3 curPoint, Vector3 nextPoint,
            Vector3 nonConvexPoint, Vector3 normal)
        {
            var res0 = GetOrientation(prevPoint, nonConvexPoint, curPoint, normal);
            var res1 = GetOrientation(curPoint, nonConvexPoint, nextPoint, normal);
            var res2 = GetOrientation(nextPoint, nonConvexPoint, prevPoint, normal);
            return res0 != 1 && res1 != 1 && res2 != 1;
        }

        public static double PointLineDistance(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            return (p2 - p1).Cross(p3 - p1).LengthSquared();
        }
    }
}