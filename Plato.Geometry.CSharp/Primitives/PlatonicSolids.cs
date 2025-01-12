using System;

namespace Plato.SinglePrecision
{

    /// <summary>
    /// These are the five unit-sized platonic-solids.
    /// </summary>
    public static class PlatonicSolids
    {
        private static readonly double _t = (1 + Math.Sqrt(5)) / 2;
        private static readonly double _rt = 1.0 / _t;
        public static readonly double Sqrt2 = Math.Sqrt(2);

        public static TriangleMesh3D ToTriangleMesh(this IArray<Vector3D> points, params Integer3[] faces)
            => new TriangleMesh3D(points, faces.ToIArray().FlatMap(i => i));

        public static QuadMesh3D ToQuadMesh(this IArray<Vector3D> points, params Integer4[] faces)
            => new QuadMesh3D(points, faces.ToIArray().FlatMap(i => i));

        public static IArray<Vector3D> Points(params Vector3D[] points)
            => Intrinsics.MakeArray(points);

        // https://mathworld.wolfram.com/RegularTetrahedron.html
        // https://github.com/mrdoob/three.js/blob/master/src/geometries/TetrahedronGeometry.js
        public static readonly TriangleMesh3D Tetrahedron
            = Points(
                (1, 1, 1), 
                (-1, -1, 1), 
                (-1, 1, -1), 
                (1, -1, -1)
            )
            .Normalize()
            .ToTriangleMesh(
                (2, 1, 0), (0, 3, 2),
                (1, 3, 0), (2, 3, 1));

        public static readonly QuadMesh3D Cube
            = Points(
                // Front quad
                (-0.5, -0.5, -0.5),
                (0.5, -0.5, -0.5),
                (0.5, -0.5, 0.5),
                (-0.5, -0.5, 0.5),
                // Back quad
                (-0.5, 0.5, -0.5),
                (0.5, 0.5, -0.5),
                (0.5, 0.5, 0.5),
                (-0.5, 0.5, 0.5)
            )
            .ToQuadMesh(
                (0, 1, 2, 3), // Front
                (1, 5, 6, 2), // Right-side
                (5, 4, 7, 6), // Back
                (4, 0, 3, 7), // Left-side
                (3, 2, 6, 7), // Top
                (4, 5, 1, 0)); // Bottom

        // https://mathworld.wolfram.com/RegularOctahedron.html
        // https://github.com/mrdoob/three.js/blob/master/src/geometries/OctahedronGeometry.js
        public static readonly TriangleMesh3D Octahedron
            = Points(
                (1, 0, 0), (-1, 0, 0), (0, 1, 0),
                (0, -1, 0), (0, 0, 1), (0, 0, -1))
            .Normalize()
            .ToTriangleMesh(
                (0, 2, 4), (0, 4, 3), (0, 3, 5),
                (0, 5, 2), (1, 2, 5), (1, 5, 3),
                (1, 3, 4), (1, 4, 2));

        // https://mathworld.wolfram.com/RegularDodecahedron.html
        // https://github.com/mrdoob/three.js/blob/master/src/geometries/DodecahedronGeometry.js
        public static readonly TriangleMesh3D Dodecahedron
            = Points(
                // (±1, ±1, ±1)
                (-1, -1, -1), (-1, -1, 1),
                (-1, 1, -1), (-1, 1, 1),
                (1, -1, -1), (1, -1, 1),
                (1, 1, -1), (1, 1, 1),

                // (0, ±1/φ, ±φ)
                (0, -_rt, -_t), (0, -_rt, _t),
                (0, _rt, -_t), (0, _rt, _t),

                // (±1/φ, ±φ, 0)
                (-_rt, -_t, 0), (-_rt, _t, 0),
                (_rt, -_t, 0), (_rt, _t, 0),

                // (±φ, 0, ±1/φ)
                (-_t, 0, -_rt), (_t, 0, -_rt),
                (-_t, 0, _rt), (_t, 0, _rt))
            .Normalize()
            .ToTriangleMesh(
                (3, 11, 7), (3, 7, 15), (3, 15, 13),
                (7, 19, 17), (7, 17, 6), (7, 6, 15),
                (17, 4, 8), (17, 8, 10), (17, 10, 6),
                (8, 0, 16), (8, 16, 2), (8, 2, 10),
                (0, 12, 1), (0, 1, 18), (0, 18, 16),
                (6, 10, 2), (6, 2, 13), (6, 13, 15),
                (2, 16, 18), (2, 18, 3), (2, 3, 13),
                (18, 1, 9), (18, 9, 11), (18, 11, 3),
                (4, 14, 12), (4, 12, 0), (4, 0, 8),
                (11, 9, 5), (11, 5, 19), (11, 19, 7),
                (19, 5, 14), (19, 14, 4), (19, 4, 17),
                (1, 12, 14), (1, 14, 5), (1, 5, 9));

        // https://mathworld.wolfram.com/RegularIcosahedron.html
        // https://github.com/mrdoob/three.js/blob/master/src/geometries/IcosahedronGeometry.js
        public static readonly TriangleMesh3D Icosahedron
            = Points(
                (-1, _t, 0),
                (1, _t, 0),
                (-1, -_t, 0),
                (1, -_t, 0),
                (0, -1, _t),
                (0, 1, _t),
                (0, -1, -_t),
                (0, 1, -_t),
                (_t, 0, -1),
                (_t, 0, 1),
                (-_t, 0, -1),
                (-_t, 0, 1))
            .Normalize()
            .ToTriangleMesh(
                (0, 11, 5), (0, 5, 1), (0, 1, 7), (0, 7, 10), (0, 10, 11),
                (1, 5, 9), (5, 11, 4), (11, 10, 2), (10, 7, 6), (7, 1, 8),
                (3, 9, 4), (3, 4, 2), (3, 2, 6), (3, 6, 8), (3, 8, 9),
                (4, 9, 5), (2, 4, 11), (6, 2, 10), (8, 6, 7), (9, 8, 1));
    }
}