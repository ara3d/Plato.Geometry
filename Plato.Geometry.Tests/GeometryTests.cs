using Plato.DoublePrecision;

namespace Plato.Geometry.Tests
{
    public static class GeometryTests
    {
        public static void OutputArray<T>(IArray<T> self, bool truncate = true)
        {
            for (var i = 0; i < self.Count && (!truncate || i < 5); i++)
            {
                Console.Write($"xs[{i}] = {self[i]} ");
            }
            if (truncate && self.Count > 5)
            {
                Console.Write("...");
                Console.Write($"xs[{self.Count-1}] == {self.Last()}");
            }
            Console.WriteLine();
        }

        [Test]
        public static void BasicArrayTests()
        {
            var xs = 5.MapRange(i => i);
            Console.WriteLine("Array");
            OutputArray(xs);
            Console.WriteLine("Indices");
            OutputArray(xs.Indices());
            Assert.IsTrue(5 == xs.Count);
            Assert.IsTrue(0 == xs[0]);
            Assert.IsTrue(4 == xs[4]);
            var xsRev = xs.Reverse();
            Console.WriteLine("Reversed");
            OutputArray(xsRev);
            Assert.IsTrue(5 == xsRev.Count);
            Assert.IsTrue(4 == xsRev[0]);
            Assert.IsTrue(0 == xsRev[4]);
            var xs2x3 = ((Integer)2).MakeArray2D(3, (a, b) => a + 2 * b);
            Console.WriteLine("Array 2 x 3");
            OutputArray(xs2x3);
            Assert.IsTrue(6 == xs2x3.Count);
            Assert.IsTrue(0 == xs2x3[0, 0]);
            Assert.IsTrue(1 == xs2x3[1, 0]);
            Assert.IsTrue(2 == xs2x3[0, 1]);
            Assert.IsTrue(5 == xs2x3[1, 2]);
        }

        [Test]
        public static void SlicesTests()
        {
            var xs = 9.MapRange(i => i);
            var ys = xs.Slices(3);
            OutputArray(ys.Map(g => $"{g[0]}, {g[1]}, {g[2]}"));
            ys = ys.Map(g => g.Reverse());
            OutputArray(ys.Map(g => $"{g[0]}, {g[1]}, {g[2]}"));

            OutputArray(xs, false);
            var zs = xs.FlipWindingOrderTriangleIndices();
            OutputArray(zs, false);
        }

        [Test]
        public static void FacetedTest()
        {
            // NOTE: this test passes, but facetedd meshes don't draw correctly./

            var m = PlatonicSolids.Tetrahedron;
            OutputTriMesh(m);

            var test = m.Indices.Slices(3).Map((xs) => $"{xs[0]},{xs[1]},{xs[2]}");
            OutputArray(test);

            var tris = m.Triangles;
            OutputArray(tris);
            for (var i=0; i < tris.Count; i++)
            {
                for (var j = i + 1; j < tris.Count; j++)
                {
                    Assert.IsTrue(!tris[i].Equals(tris[j]));
                }
            }

            var faceted = m.Faceted();
            var tris2 = faceted.Triangles;
            Assert.IsTrue(tris2.Count == tris.Count);
            for (var i = 0; i < tris2.Count; i++)
            {
                Assert.IsTrue(tris[i].Equals(tris2[i]));
                for (var j = i + 1; j < tris2.Count; j++)
                {
                    Assert.IsTrue(!tris2[i].Equals(tris2[j]));
                }
            }

            OutputTriMesh(faceted);
        }

        [Test]
        public static void TestBounds()
        {
            var m = PlatonicSolids.Tetrahedron;
            Console.WriteLine($"Min vector : {Vector3D.MinValue}, Max vector: {Vector3D.MaxValue}");
            var b1 = new Bounds3D((1, 1, 1), (2, 2, 2));
            Console.WriteLine($"Constructed bounds = {b1}");
            Console.WriteLine($"Empty: {Bounds3D.Empty}");
            var b = m.Points.Bounds();
            Console.WriteLine($"Bounds: {b}");
        }

        [Test]
        public static void TestBasicQuad()
        {
            var quad = Quad2D.Unit;
            var quad2 = quad.To3D;
            var (a, b, c, d) = quad2;
            var tris= quad2.Triangles;
            Assert.IsTrue(tris.Count == 2);
            var tri1 = tris[0];
            Assert.IsTrue(tri1.A == a);
            Assert.IsTrue(tri1.B == b);
            Assert.IsTrue(tri1.C == c);
            var tri2 = tris[1];
            Assert.IsTrue(tri2.A == c);
            Assert.IsTrue(tri2.B == d);
            Assert.IsTrue(tri2.C == a);
        }

        public static IArray<Number> SampleRange(Number from, Number to, Integer count)
            => count.LinearSpace.Map(i => from.Lerp(to, i));

        public static QuadGrid3D GetQuadGrid(int usegs, int vsegs, bool closedX = false, bool closedY = false)
        {
            var columns = SampleRange(0, 1, usegs);
            var rows = SampleRange(0, 1, vsegs);
            var points = columns.CartesianProduct(rows, (u, v) => new Vector3D(u, v, 0));
            return new QuadGrid3D(points, closedX, closedY);
        }

        [Test]
        public static void TestQuadPlane()
        {
            {
                var grid = GetQuadGrid(2, 2);
                OutputQuadGrid(grid);
            }
            {
                var grid = GetQuadGrid(3, 2);
                OutputQuadGrid(grid);
            }
            {
                var grid = GetQuadGrid(2, 3);
                OutputQuadGrid(grid);
            }
            {
                var grid = GetQuadGrid(3, 3);
                OutputQuadGrid(grid);
            }
        }

        public static void OutputQuadGrid(QuadGrid3D grid)
        {
            Console.WriteLine($"Quad Grid #columns = {grid.NumColumns}, #rows = {grid.NumRows}, #quads = {grid.Quads.Count}, ClosedX = {grid.ClosedX}, ClosedY = {grid.ClosedY}");
            Console.WriteLine("Quads");
            OutputArray(grid.Quads);
            var indexGroups = grid.PointGrid.AllQuadFaceIndices(grid.ClosedX, grid.ClosedY);
            Console.WriteLine("Index Groups");
            OutputArray(indexGroups);

            OutputIndexedGeometry(grid);
            var mesh = grid.TriangleMesh3D;
            OutputTriMesh(mesh);
        }

        public static void OutputTriMesh(TriangleMesh3D mesh)
        {
            Console.WriteLine($"Triangle mesh: #triangles = {mesh.Faces.Count}");
            OutputIndexedGeometry(mesh);
        }

        public static void OutputIndexedGeometry(IIndexedGeometry3D g)
        {
            Console.WriteLine($"Indexed Geometry: Points: {g.Points.Count}, Indices: {g.Indices.Count}, Primitives: {g.NumPrimitives}, Prim size: {g.PrimitiveSize}");
            Assert.IsTrue(g.NumPrimitives * g.PrimitiveSize == g.Indices.Count);
            foreach (var index in g.Indices)
            {
                Assert.IsTrue(index >= 0 && index < g.Points.Count);
            }
            Console.WriteLine("Points");
            OutputArray(g.Points);
            Console.WriteLine("Indices");
            OutputArray(g.Indices);
        }
    }
}
