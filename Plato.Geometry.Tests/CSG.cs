using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ara3D.DoublePrecision;

namespace Plato.Geometry.Tests
{
    public enum PlaneClassification
    {
        Coplanar = 0,
        Front = 1,
        Back = 2,
        Spanning = 3,
    };

    public static class BSPExtensions
    {
        public static Triangle3D Flip(this Triangle3D t)
            => (t.C, t.B, t.A);

        public static Vector3D Cross(this Vector3D a, Vector3D b)
            => (
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X);

        public static Vector3D Normalized(this Vector3D v)
        {
            var m = v.Magnitude;
            return new Vector3D(v.X / m, v.Y / m, v.Z / m);
        }

        public static Vector3D Normal(this Triangle3D t)
            => (t.B - t.A).Cross(t.C - t.A).Normalized();

        public static Point3D Center(this Triangle3D t)
            => (t.A + t.B + t.C).ToVector / 3.0;

        public static Plane Plane(this Triangle3D t)
        {
            var n = t.Normal();
            return new Plane(n, n.Dot(t.A));
        }

        public static PlaneClassification Classify(this Plane p, Vector3D v)
        {
            var d = p.Normal.Dot(v) - p.D;
            return (d < -double.Epsilon) ? PlaneClassification.Back : (d > double.Epsilon) ? PlaneClassification.Front : PlaneClassification.Coplanar;
        }

        public static PlaneClassification Classify(this Plane p, Triangle3D t)
        {
            var r = 0;
            for (var i=0; i<3; i++) 
                r |= (int)p.Classify(t[i]);
            return (PlaneClassification)r;
        }

        public static List<Triangle3D> Split(this Triangle3D t, Plane p)
        {
            /*
            var classifications = new[] { p.Classify(t.A), p.Classify(t.B), p.Classify(t.C) };
            var all = classifications.Aggregate(0, (current, x) => current | (int)x);
            if ((PlaneClassification)all != PlaneClassification.Spanning)
                return all;
            */
            throw new NotImplementedException();
        }
    }

    public class BSP
    {
        public class Node
        {
            public List<Triangle3D> Front = new();
            public List<Triangle3D> Back = new();
            public List<Triangle3D> Coplanar = new();
    
            public Node(IReadOnlyList<Triangle3D> triangles)
            {
                if (triangles.Count == 0)
                    return;

                var plane = triangles.First().Plane();
                SplitPolygons(plane, triangles, Coplanar, Front, Back);
            }

            public static void SplitPolygons(Plane p, IEnumerable<Triangle3D> triangles, List<Triangle3D> coplanar,
                List<Triangle3D> front, List<Triangle3D> back)
            {
            }

            public static void SplitPolygon(
                Plane p, 
                Triangle3D tri, 
                List<Triangle3D> coplanarFront, 
                List<Triangle3D> coplanarBack, 
                List<Triangle3D> front, 
                List<Triangle3D> back)
            {
                const double EPS = double.E;

                // Classify each point as well as the entire polygon into one of the above
                // four classes.
                var polygonType = 0;
                var types = new PlaneClassification[3];
                for (var i = 0; i < 3; i++)
                {
                    var pos = tri[i];
                    var t = p.Normal.Dot(pos) - p.D;
                    var type = (t < -EPS) 
                        ? PlaneClassification.Back 
                        : (t > EPS) 
                            ? PlaneClassification.Front 
                            : PlaneClassification.Coplanar;
                    polygonType |= (int)type;
                    types[i] = type;
                }

                // Put the polygon in the correct list, splitting it when necessary.
                switch ((PlaneClassification)polygonType)
                {
                    case PlaneClassification.Coplanar:
                        (p.Normal.Dot(tri.Normal()) > 0 ? coplanarFront : coplanarBack).Add(tri);
                        break;

                    case PlaneClassification.Front:
                        front.Add(tri);
                        break;

                    case PlaneClassification.Back:
                        back.Add(tri);
                        break;

                    case PlaneClassification.Spanning:
                        var f = new List<Vector3D>(); 
                        var b = new List<Vector3D>();
                        for (var i = 0; i < 3; i++)
                        {
                            var j = (i + 1) % 3;
                            var ti = types[i];
                            var tj = types[j];
                            var vi = tri[i];
                            var vj = tri[j];
                            if (ti != PlaneClassification.Back) f.Add(vi);
                            if (ti != PlaneClassification.Front) b.Add(vi);
                            if ((ti | tj) == PlaneClassification.Spanning)
                            {
                                var t = (p.D - p.Normal.Dot(vi)) / p.Normal.Dot(vj - vi);
                                var v = vi.Lerp(vj, t);
                                f.Add(v);
                                b.Add(v);
                            }
                        }

                        if (f.Count >= 3)
                        {
                            if (f.Count >= 4) throw new Exception("Did not expected more than three triangles");
                            front.Add(new Triangle3D(f[0], f[1], f[2]));
                        }

                        if (b.Count >= 3)
                        {
                            if (b.Count >= 4) throw new Exception("Did not expected more than three triangles");
                            front.Add(new Triangle3D(b[0], b[1], b[2]));
                        }

                        break;
                }
            }

            
        };
    }

    /*
        public readonly IEnumerable<Triangle3D> Triangles;

        public BSP(IEnumerable<Triangle3D> triangles) 
            => Triangles = triangles;

        public BSP Invert()
            => new BSP(Triangles.Select(t => t.Flip()));

        public BSP Union(BSP bsp)
        {
            throw new NotImplementedException();
        }

        public BSP Subtract(BSP bsp)
        {
            throw new NotImplementedException();
        }

        public BSP Intersect(BSP bsp)
        {
            throw new NotImplementedException();
        }

        public BSP ClipTo(BSP bsp)
        {
            throw new NotImplementedException();
        }
    }
    */

    /*
// Constructive Solid Geometry (CSG) is a modeling technique that uses Boolean
// operations like union and intersection to combine 3D solids. This library
// implements CSG operations on meshes elegantly and concisely using BSP trees,
// and is meant to serve as an easily understandable implementation of the
// algorithm. All edge cases involving overlapping coplanar polygons in both
// solids are correctly handled.
// 
// Example usage:
// 
//     var cube = CSG.cube();
//     var sphere = CSG.sphere({ radius: 1.3 });
//     var polygons = cube.subtract(sphere).toPolygons();
// 
// ## Implementation Details
// 
// All CSG operations are implemented in terms of two functions, `clipTo()` and
// `invert()`, which remove parts of a BSP tree inside another BSP tree and swap
// solid and empty space, respectively. To find the union of `a` and `b`, we
// want to remove everything in `a` inside `b` and everything in `b` inside `a`,
// then combine polygons from `a` and `b` into one solid:
// 
//     a.clipTo(b);
//     b.clipTo(a);
//     a.build(b.allPolygons());
// 
// The only tricky part is handling overlapping coplanar polygons in both trees.
// The code above keeps both copies, but we need to keep them in one tree and
// remove them in the other tree. To remove them from `b` we can clip the
// inverse of `b` against `a`. The code for union now looks like this:
// 
//     a.clipTo(b);
//     b.clipTo(a);
//     b.invert();
//     b.clipTo(a);
//     b.invert();
//     a.build(b.allPolygons());
// 
// Subtraction and intersection naturally follow from set operations. If
// union is `A | B`, subtraction is `A - B = ~(~A | B)` and intersection is
// `A & B = ~(~A | ~B)` where `~` is the complement operator.
// 
// ## License
// 
// Copyright (c) 2011 Evan Wallace (http://madebyevan.com/), under the MIT license.

// # class CSG

// Holds a binary space partition tree representing a 3D solid. Two solids can
// be combined using the `union()`, `subtract()`, and `intersect()` methods.

    public class BspPolyTree
    {
        public static BspTree Union(BspTree left, BspTree right)
        {

        };

        public static BspTree Subtract(Bsp)
    }


    // 
  // Return a new CSG solid representing space in either this solid or in the
  // solid `csg`. Neither this solid nor the solid `csg` are modified.
  // 
  //     A.union(B)
  // 
  //     +-------+            +-------+
  //     |       |            |       |
  //     |   A   |            |       |
  //     |    +--+----+   =   |       +----+
  //     +----+--+    |       +----+       |
  //          |   B   |            |       |
  //          |       |            |       |
  //          +-------+            +-------+
  // 
  Csg union: function(csg)
    {
        var a = new CSG.Node(this.clone().polygons);
        var b = new CSG.Node(csg.clone().polygons);
        a.clipTo(b);
        b.clipTo(a);
        b.invert();
        b.clipTo(a);
        b.invert();
        a.build(b.allPolygons());
        return CSG.fromPolygons(a.allPolygons());
    }
  ;

  // Return a new CSG solid representing space in this solid but not in the
  // solid `csg`. Neither this solid nor the solid `csg` are modified.
  // 
  //     A.subtract(B)
  // 
  //     +-------+            +-------+
  //     |       |            |       |
  //     |   A   |            |       |
  //     |    +--+----+   =   |    +--+
  //     +----+--+    |       +----+
  //          |   B   |
  //          |       |
  //          +-------+
  // 
  subtract: function(csg)
    {
        var a = new CSG.Node(this.clone().polygons);
        var b = new CSG.Node(csg.clone().polygons);
        a.invert();
        a.clipTo(b);
        b.clipTo(a);
        b.invert();
        b.clipTo(a);
        b.invert();
        a.build(b.allPolygons());
        a.invert();
        return CSG.fromPolygons(a.allPolygons());
    },

  // Return a new CSG solid representing space both this solid and in the
  // solid `csg`. Neither this solid nor the solid `csg` are modified.
  // 
  //     A.intersect(B)
  // 
  //     +-------+
  //     |       |
  //     |   A   |
  //     |    +--+----+   =   +--+
  //     +----+--+    |       +--+
  //          |   B   |
  //          |       |
  //          +-------+
  // 
  intersect: function(csg)
    {
        var a = new CSG.Node(this.clone().polygons);
        var b = new CSG.Node(csg.clone().polygons);
        a.invert();
        b.clipTo(a);
        b.invert();
        a.clipTo(b);
        b.clipTo(a);
        a.build(b.allPolygons());
        a.invert();
        return CSG.fromPolygons(a.allPolygons());
    },

  // Return a new CSG solid with solid and empty space switched. This solid is
  // not modified.
  inverse: function()
    {
        var csg = this.clone();
        csg.polygons.map(function(p) { p.flip(); });
        return csg;
    }
};

// Construct an axis-aligned solid cuboid. Optional parameters are `center` and
// `radius`, which default to `[0, 0, 0]` and `[1, 1, 1]`. The radius can be
// specified using a single number or a list of three numbers, one for each axis.
// 
// Example code:
// 
//     var cube = CSG.cube({
//       center: [0, 0, 0],
//       radius: 1
//     });
CSG.cube = function(options) {
    options = options || { };
    var c = new CSG.Vector(options.center || [0, 0, 0]);
    var r = !options.radius ? [1, 1, 1] : options.radius.length ?
             options.radius : [options.radius, options.radius, options.radius];
    return CSG.fromPolygons([
      [[0, 4, 6, 2], [-1, 0, 0]],
    [[1, 3, 7, 5], [+1, 0, 0]],
    [[0, 1, 5, 4], [0, -1, 0]],
    [[2, 6, 7, 3], [0, +1, 0]],
    [[0, 2, 3, 1], [0, 0, -1]],
    [[4, 5, 7, 6], [0, 0, +1]]
    ].map(function(info) {
        return new CSG.Polygon(info[0].map(function(i) {
      var pos = new CSG.Vector(
        c.x + r[0] * (2 * !!(i & 1) - 1),
        c.y + r[1] * (2 * !!(i & 2) - 1),
        c.z + r[2] * (2 * !!(i & 4) - 1)
      );
      return new CSG.Vertex(pos, new CSG.Vector(info[1]));
    }));
}));
};


// Construct a solid cylinder. Optional parameters are `start`, `end`,
// `radius`, and `slices`, which default to `[0, -1, 0]`, `[0, 1, 0]`, `1`, and
// `16`. The `slices` parameter controls the tessellation.
// 
// Example usage:
// 
//     var cylinder = CSG.cylinder({
//       start: [0, -1, 0],
//       end: [0, 1, 0],
//       radius: 1,
//       slices: 16
//     });
CSG.cylinder = function(options) {
    options = options || { };
    var s = new CSG.Vector(options.start || [0, -1, 0]);
    var e = new CSG.Vector(options.end || [0, 1, 0]);
    var ray = e.minus(s);
    var r = options.radius || 1;
    var slices = options.slices || 16;
    var axisZ = ray.unit(), isY = (Math.abs(axisZ.y) > 0.5);
    var axisX = new CSG.Vector(isY, !isY, 0).cross(axisZ).unit();
    var axisY = axisX.cross(axisZ).unit();
    var start = new CSG.Vertex(s, axisZ.negated());
    var end = new CSG.Vertex(e, axisZ.unit());
    var polygons = [];
    function point(stack, slice, normalBlend)
    {
        var angle = slice * Math.PI * 2;
        var out = axisX.times(Math.cos(angle)).plus(axisY.times(Math.sin(angle)));
        var pos = s.plus(ray.times(stack)).plus(out.times(r));
            var normal = out.times(1 - Math.abs(normalBlend)).plus(axisZ.times(normalBlend));
        return new CSG.Vertex(pos, normal);
    }
    for (var i = 0; i < slices; i++)
    {
        var t0 = i / slices, t1 = (i + 1) / slices;
        polygons.push(new CSG.Polygon([start, point(0, t0, -1), point(0, t1, -1)]));
        polygons.push(new CSG.Polygon([point(0, t1, 0), point(0, t0, 0), point(1, t0, 0), point(1, t1, 0)]));
        polygons.push(new CSG.Polygon([end, point(1, t1, 1), point(1, t0, 1)]));
    }
    return CSG.fromPolygons(polygons);
};


// # class Vertex


// # class Plane

// Represents a plane in 3D space.

CSG.Plane = function(normal, w) {
    this.normal = normal;
    this.w = w;
};

// `CSG.Plane.EPSILON` is the tolerance used by `splitPolygon()` to decide if a
// point is on the plane.
CSG.Plane.EPSILON = 1e-5;

CSG.Plane.fromPoints = function(a, b, c) {
    var n = b.minus(a).cross(c.minus(a)).unit();
    return new CSG.Plane(n, n.dot(a));
};

CSG.Plane.prototype = {
    clone: function() {
        return new CSG.Plane(this.normal.clone(), this.w);
    },

  flip: function() {
        this.normal = this.normal.negated();
        this.w = -this.w;
    },

  // Split `polygon` by this plane if needed, then put the polygon or polygon
  // fragments in the appropriate lists. Coplanar polygons go into either
  // `coplanarFront` or `coplanarBack` depending on their orientation with
  // respect to this plane. Polygons in front or in back of this plane go into
  // either `front` or `back`.
  splitPolygon: function(polygon, coplanarFront, coplanarBack, front, back) {
        var COPLANAR = 0;
        var FRONT = 1;
        var BACK = 2;
        var SPANNING = 3;

        // Classify each point as well as the entire polygon into one of the above
        // four classes.
        var polygonType = 0;
        var types = [];
        for (var i = 0; i < polygon.vertices.length; i++)
        {
            var t = this.normal.dot(polygon.vertices[i].pos) - this.w;
            var type = (t < -CSG.Plane.EPSILON) ? BACK : (t > CSG.Plane.EPSILON) ? FRONT : COPLANAR;
            polygonType |= type;
            types.push(type);
        }

        // Put the polygon in the correct list, splitting it when necessary.
        switch (polygonType)
        {
            case COPLANAR:
                (this.normal.dot(polygon.plane.normal) > 0 ? coplanarFront : coplanarBack).push(polygon);
                break;
            case FRONT:
                front.push(polygon);
                break;
            case BACK:
                back.push(polygon);
                break;
            case SPANNING:
                var f = [], b = [];
                for (var i = 0; i < polygon.vertices.length; i++)
                {
                    var j = (i + 1) % polygon.vertices.length;
                    var ti = types[i], tj = types[j];
                    var vi = polygon.vertices[i], vj = polygon.vertices[j];
                    if (ti != BACK) f.push(vi);
                    if (ti != FRONT) b.push(ti != BACK ? vi.clone() : vi);
                    if ((ti | tj) == SPANNING)
                    {
                        var t = (this.w - this.normal.dot(vi.pos)) / this.normal.dot(vj.pos.minus(vi.pos));
                        var v = vi.interpolate(vj, t);
                        f.push(v);
                        b.push(v.clone());
                    }
                }
                if (f.length >= 3) front.push(new CSG.Polygon(f, polygon.shared));
                if (b.length >= 3) back.push(new CSG.Polygon(b, polygon.shared));
                break;
        }
    }
};

// # class Polygon

// Represents a convex polygon. The vertices used to initialize a polygon must
// be coplanar and form a convex loop. They do not have to be `CSG.Vertex`
// instances but they must behave similarly (duck typing can be used for
// customization).
// 
// Each convex polygon has a `shared` property, which is shared between all
// polygons that are clones of each other or were split from the same polygon.
// This can be used to define per-polygon properties (such as surface color).

CSG.Polygon = function(vertices, shared) {
    this.vertices = vertices;
    this.shared = shared;
    this.plane = CSG.Plane.fromPoints(vertices[0].pos, vertices[1].pos, vertices[2].pos);
};

CSG.Polygon.prototype = {
    clone: function() {
        var vertices = this.vertices.map(function(v) { return v.clone(); });
        return new CSG.Polygon(vertices, this.shared);
    },

  flip: function() {
        this.vertices.reverse().map(function(v) { v.flip(); });
        this.plane.flip();
    }
};

// # class Node

// Holds a node in a BSP tree. A BSP tree is built from a collection of polygons
// by picking a polygon to split along. That polygon (and all other coplanar
// polygons) are added directly to that node and the other polygons are added to
// the front and/or back subtrees. This is not a leafy BSP tree since there is
// no distinction between internal and leaf nodes.

CSG.Node = function(polygons) {
    this.plane = null;
    this.front = null;
    this.back = null;
    this.polygons = [];
    if (polygons) this.build(polygons);
};

CSG.Node.prototype = {
    clone: function() {
        var node = new CSG.Node();
        node.plane = this.plane && this.plane.clone();
        node.front = this.front && this.front.clone();
        node.back = this.back && this.back.clone();
        node.polygons = this.polygons.map(function(p) { return p.clone(); });
        return node;
    },

  // Convert solid space to empty space and empty space to solid space.
  invert: function() {
        for (var i = 0; i < this.polygons.length; i++)
        {
            this.polygons[i].flip();
        }
        this.plane.flip();
        if (this.front) this.front.invert();
        if (this.back) this.back.invert();
        var temp = this.front;
        this.front = this.back;
        this.back = temp;
    },

  // Recursively remove all polygons in `polygons` that are inside this BSP
  // tree.
  clipPolygons: function(polygons) {
        if (!this.plane) return polygons.slice();
        var front = [], back = [];
        for (var i = 0; i < polygons.length; i++)
        {
            this.plane.splitPolygon(polygons[i], front, back, front, back);
        }
        if (this.front) front = this.front.clipPolygons(front);
        if (this.back) back = this.back.clipPolygons(back);
        else back = [];
        return front.concat(back);
    },

  // Remove all polygons in this BSP tree that are inside the other BSP tree
  // `bsp`.
  clipTo: function(bsp) {
        this.polygons = bsp.clipPolygons(this.polygons);
        if (this.front) this.front.clipTo(bsp);
        if (this.back) this.back.clipTo(bsp);
    },

  // Return a list of all polygons in this BSP tree.
  allPolygons: function() {
        var polygons = this.polygons.slice();
        if (this.front) polygons = polygons.concat(this.front.allPolygons());
        if (this.back) polygons = polygons.concat(this.back.allPolygons());
        return polygons;
    },

  // Build a BSP tree out of `polygons`. When called on an existing tree, the
  // new polygons are filtered down to the bottom of the tree and become new
  // nodes there. Each set of polygons is partitioned using the first polygon
  // (no heuristic is used to pick a good split).
  build: function(polygons) {
        if (!polygons.length) return;
        if (!this.plane) this.plane = polygons[0].plane.clone();
        var front = [], back = [];
        for (var i = 0; i < polygons.length; i++)
        {
            this.plane.splitPolygon(polygons[i], this.polygons, this.polygons, front, back);
        }
        if (front.length)
        {
            if (!this.front) this.front = new CSG.Node();
            this.front.build(front);
        }
        if (back.length)
        {
            if (!this.back) this.back = new CSG.Node();
            this.back.build(back);
        }
    }
};
    */

}
