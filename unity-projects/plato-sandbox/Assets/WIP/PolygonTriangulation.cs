// https://en.wikipedia.org/wiki/Polygon_triangulation
// https://www.habrador.com/tutorials/math/10-triangulation/

using System.Collections.Generic;
using System.Linq;
using Ara3D.Collections;
using Ara3D.Geometry;
using Ara3D.Mathematics;

public class Vertex
{
    public Vertex Next;
    public Vertex Previous;
    public bool IsReflex => 
        MathUtility.IsTriangleOrientedClockwise(Previous, Pos, Next);
    public bool IsConvex => !IsReflex;
    public readonly Vector2 Pos;
    public readonly int Index;
    public Vertex(int i, Vector2 p)
        => (Index, Pos) = (i, p);
    public static implicit operator Vector2(Vertex v) 
        => v.Pos;
}

public class Triangle
{
    public readonly Vertex V1;
    public readonly Vertex V2;
    public readonly Vertex V3;
    public Int3 Indices => new(V1.Index, V2.Index, V3.Index);
    public Triangle(Vertex v1, Vertex v2, Vertex v3)
        => (V1, V2, V3) = (v1, v2, v3);
}

public static class PolygonTriangulation
{   
    // This assumes that we have a polygon and now we want to triangulate it
    // The points on the polygon should be ordered counter-clockwise
    // This algorithm is called ear clipping and it's O(n*n) Another common algorithm is dividing it into trapezoids and it's O(n log n)
    // One can maybe do it in O(n) time but no such version is known
    public static IArray<Triangle> TriangulateConcavePolygon(IArray<Vector2> points)
    {
		//The list with triangles the method returns
        var triangles = new List<Triangle>();
        
        // Degenerate case
        if (points.Count < 3) 
            return triangles.ToIArray();

        //Step 1. Store the vertices in a list and we also need to know the next and prev vertex
        var vertices = points.Select((p,i) => new Vertex(i, p)).ToList();

        // Find the next and previous vertex
        for (var i = 0; i < vertices.Count; i++)
        {
            var nextPos = (i + 1) % vertices.Count;
            var prevPos = (i + vertices.Count - 1) % vertices.Count;
            vertices[i].Previous = vertices[prevPos];
            vertices[i].Next = vertices[nextPos];
        }

        var earVertices = vertices.Where(t => IsVertexEar(t, vertices)).ToList();

        //Step 3. Triangulate!
        while (true)
        {
            //This means we have just one triangle left
            if (vertices.Count == 3)
            {
                // The final triangle
                triangles.Add(new Triangle(vertices[0], vertices[0].Previous, vertices[0].Next));
                break;
            }

            // Make a triangle of the first ear
            var earVertex = earVertices[0];

            var earVertexPrev = earVertex.Previous;
            var earVertexNext = earVertex.Next;
            var newTriangle = new Triangle(earVertex, earVertexPrev, earVertexNext);
            triangles.Add(newTriangle);

            // Remove the vertex from the lists
            earVertices.Remove(earVertex);
            vertices.Remove(earVertex);

            // Update the previous vertex and next vertex to point to the ear. 
            earVertexPrev.Next = earVertexNext;
            earVertexNext.Previous = earVertexPrev;

            // NOTE: these are each Log(N) over the the number of ear vertices 
            earVertices.Remove(earVertexPrev);
            earVertices.Remove(earVertexNext);

            if (IsVertexEar(earVertexPrev, vertices))
                earVertices.Add(earVertexPrev);
            if (IsVertexEar(earVertexNext, vertices))
                earVertices.Add(earVertexNext);
        }

        return triangles.ToIArray();
    }
	
    public static bool IsVertexEar(Vertex v, IEnumerable<Vertex> vertices)
    {
        // A reflex vertex cant be an ear!
        if (v.IsReflex)
            return false;

        return vertices.All(t => !t.IsReflex 
            || !MathUtility.IsPointInTriangle(v.Previous, v, v.Next, t.Pos));
    }
}

public static class MathUtility
{
    // Where is a point in relation to a line?
	// Where is p in relation to a-b
	// < 0 -> to the right
	// = 0 -> on the line
	// > 0 -> to the left
	public static float IsAPointLeftOfVectorOrOnTheLine(Vector2 a, Vector2 b, Vector2 p)
	{
        // Computes a determinant 
		return (a.X - p.X) * (b.Y - p.Y) - (a.Y - p.Y) * (b.X - p.X);
	}
    
    // Is a triangle oriented clockwise
    //Is a triangle in 2d space oriented clockwise or counter-clockwise
    //https://math.stackexchange.com/questions/1324179/how-to-tell-if-3-connected-points-are-connected-clockwise-or-counter-clockwise
    //https://en.wikipedia.org/wiki/Curve_orientation
    public static bool IsTriangleOrientedClockwise(Vector2 p1, Vector2 p2, Vector2 p3)
	{
		return p1.X * p2.Y + p3.X * p1.Y + p2.X * p3.Y - p1.X * p3.Y - p3.X * p2.Y - p2.X * p1.Y <= 0;
	}

	// Is a point in a triangle?
    // From http://totologic.blogspot.se/2014/01/accurate-point-in-triangle-test.html
    // p is the testpoint, and the other points are corners in the triangle
    public static bool IsPointInTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p)
	{
		var isWithinTriangle = false;

		//Based on Barycentric coordinates
		var denominator = ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));

		var a = ((p2.Y - p3.Y) * (p.X - p3.X) + (p3.X - p2.X) * (p.Y - p3.Y)) / denominator;
		var b = ((p3.Y - p1.Y) * (p.X - p3.X) + (p1.X - p3.X) * (p.Y - p3.Y)) / denominator;
		var c = 1 - a - b;

		//The point is within the triangle or on the border if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
		//if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
		//{
		//    isWithinTriangle = true;
		//}

		//The point is within the triangle
		if (a > 0f && a < 1f && b > 0f && b < 1f && c > 0f && c < 1f)
        {
			isWithinTriangle = true;
		}

		return isWithinTriangle;
	}

    // Are two lines intersecting?
    //http://thirdpartyninjas.com/blog/2008/10/07/line-segment-intersection/
    public static bool AreLinesIntersecting(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2, bool shouldIncludeEndPoints)
	{
		var isIntersecting = false;

		var denominator = (l2_p2.Y - l2_p1.Y) * (l1_p2.X - l1_p1.X) - (l2_p2.X - l2_p1.X) * (l1_p2.Y - l1_p1.Y);

		//Make sure the denominator is > 0, if not the lines are parallel
		if (denominator != 0f)
		{
			var u_a = ((l2_p2.X - l2_p1.X) * (l1_p1.Y - l2_p1.Y) - (l2_p2.Y - l2_p1.Y) * (l1_p1.X - l2_p1.X)) / denominator;
			var u_b = ((l1_p2.X - l1_p1.X) * (l1_p1.Y - l2_p1.Y) - (l1_p2.Y - l1_p1.Y) * (l1_p1.X - l2_p1.X)) / denominator;

			//Are the line segments intersecting if the end points are the same
			if (shouldIncludeEndPoints)
			{
				//Is intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
				if (u_a >= 0f && u_a <= 1f && u_b >= 0f && u_b <= 1f)
				{
					isIntersecting = true;
				}
			}
			else
			{
				//Is intersecting if u_a and u_b are between 0 and 1
				if (u_a > 0f && u_a < 1f && u_b > 0f && u_b < 1f)
                {
                    isIntersecting = true;
                }
            }
        }

		return isIntersecting;
	}
    
    // ...and if we know they are intersecting it might be useful to know the coordinate of the intersection point
    //Whats the coordinate of an intersection point between two lines in 2d space if we know they are intersecting
    //http://thirdpartyninjas.com/blog/2008/10/07/line-segment-intersection/
    public static Vector2 GetLineLineIntersectionPoint(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2)
	{
		var denominator = (l2_p2.Y - l2_p1.Y) * (l1_p2.X - l1_p1.X) - (l2_p2.X - l2_p1.X) * (l1_p2.Y - l1_p1.Y);
        var u_a = ((l2_p2.X - l2_p1.X) * (l1_p1.Y - l2_p1.Y) - (l2_p2.Y - l2_p1.Y) * (l1_p1.X - l2_p1.X)) / denominator;
        var intersectionPoint = l1_p1 + u_a * (l1_p2 - l1_p1);
        return intersectionPoint;
	}

    // Where is a point in relation to a plane?
    //Is a point to the left, to the right, or on a plane
    //https://gamedevelopment.tutsplus.com/tutorials/understanding-sutherland-hodgman-clipping-for-physics-engines--gamedev-11917
    //Notice that the plane normal doesnt have to be normalized
    public static float DistanceFromPointToPlane(Vector3 planeNormal, Vector3 planePos, Vector3 pointPos)
	{
		//Positive distance denotes that the point p is on the front side of the plane 
		//Negative means it's on the back side
		return Vector3.Dot(planeNormal, pointPos - planePos);
	}

	// Whats the coordinate where a ray is intersecting a plane?
    //Get the coordinate if we know a ray-plane is intersecting
    public static Vector3 GetRayPlaneIntersectionCoordinate(Vector3 planePos, Vector3 planeNormal, Vector3 rayStart, Vector3 rayDir)
	{
		var denominator = Vector3.Dot(-planeNormal, rayDir);
        var vecBetween = planePos - rayStart;
        var t = Vector3.Dot(vecBetween, -planeNormal) / denominator;
        var intersectionPoint = rayStart + rayDir * t;
        return intersectionPoint;
	}

    // Is a line intersecting with a plane?
    //Is a line-plane intersecting?
    public static bool AreLinePlaneIntersecting(Vector3 planeNormal, Vector3 planePos, Vector3 linePos1, Vector3 linePos2, float epsilon = 0.000001f)
	{
		var areIntersecting = false;
        var lineDir = (linePos1 - linePos2).Normalize();
        var denominator = Vector3.Dot(-planeNormal, lineDir);

		//No intersection if the line and plane are parallell
		if (denominator.Abs() > epsilon)
		{
			var vecBetween = planePos - linePos1;
            var t = Vector3.Dot(vecBetween, -planeNormal) / denominator;
            var intersectionPoint = linePos1 + lineDir * t;
            if (IsPointBetweenPoints(linePos1, linePos2, intersectionPoint))
			{
				areIntersecting = true;
			}
		}

		return areIntersecting;
	}

    //...which requires that we know how to tell if a point is between two points:

    //Is a point c between point a and b (we assume all 3 are on the same line)
    public static bool IsPointBetweenPoints(Vector3 a, Vector3 b, Vector3 c)
	{
		//Entire line segment
		var ab = b - a;

        //The intersection and the first point
		var ac = c - a;

        //Need to check 2 things: 
        //1. If the vectors are pointing in the same direction = if the dot product is positive
        //2. If the length of the vector between the intersection and the first point is smaller than the entire line
        return (ab.Dot(ac) > 0f && ab.LengthSquared() >= ac.LengthSquared());
	}

    // ...and we might also need to know the point of intersection if we know they are intersecting:

    //We know a line plane is intersecting and now we want the coordinate of intersection
    public static Vector3 GetLinePlaneIntersectionCoordinate(Vector3 planeNormal, Vector3 planePos, Vector3 linePos1, Vector3 linePos2)
	{
		var vecBetween = planePos - linePos1;
        var lineDir = (linePos1 - linePos2).Normalize();
        var denominator = (-planeNormal).Dot(lineDir);
        var t = vecBetween.Dot(-planeNormal) / denominator;
        var intersectionPoint = linePos1 + lineDir * t;
        return intersectionPoint;
	}

	// Is a point inside a polygon?
    //The list describing the polygon has to be sorted either clockwise or counter-clockwise because we have to identify its edges
    
    public static bool IsPointInPolygon(List<Vector2> polygonPoints, Vector2 point)
    {
        //Step 1. Find a point outside of the polygon
        //Pick a point with a x position larger than the polygons max x position, which is always outside
        var maxXPosVertex = polygonPoints[0];

        for (var i = 1; i < polygonPoints.Count; i++)
        {
            if (polygonPoints[i].X > maxXPosVertex.X)
            {
                maxXPosVertex = polygonPoints[i];
            }
        }

        //The point should be outside so just pick a number to make it outside
        var pointOutside = maxXPosVertex + new Vector2(10f, 0f);

        //Step 2. Create an edge between the point we want to test with the point thats outside
        var l1_p1 = point;
        var l1_p2 = pointOutside;

        //Step 3. Find out how many edges of the polygon this edge is intersecting
        var numberOfIntersections = 0;

        for (var i = 0; i < polygonPoints.Count; i++)
        {
            //Line 2
            var l2_p1 = polygonPoints[i];
            var iPlusOne = (i + 1) % polygonPoints.Count;
            var l2_p2 = polygonPoints[iPlusOne];

            //Are the lines intersecting?
            if (AreLinesIntersecting(l1_p1, l1_p2, l2_p1, l2_p2, true))
            {
                numberOfIntersections += 1;
            }
        }

		//Step 4. Is the point inside or outside?
		//The point is outside the polygon if number of intersections is even or 0
        return numberOfIntersections % 2 != 0;
    }

    public static TriMesh ToMesh(this IArray<Vector2> points, IArray<Triangle> triangles)
        => new(points.Select(p => new Vector3(p.X, 0f, p.Y)), triangles.Select(tri => tri.Indices));
}