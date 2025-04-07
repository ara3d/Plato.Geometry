using System.Diagnostics;
using g3;
using Plato.SinglePrecision;

namespace Plato.Geometry.G3Sharp
{
    public static class G3SharpBridge
    {
        // https://github.com/gradientspace/geometry3Sharp/issues/3
        public static DMesh3 Compact(this DMesh3 mesh, bool compactFlag = true)
            => compactFlag ? new DMesh3(mesh, true) : mesh;    

        public static DMesh3 Slice(this DMesh3 self, Plane plane, bool compact = true)
        {
            var normal = plane.Normal;
            var origin = normal * -plane.D;
            var cutter = new MeshPlaneCut(self, origin.ToVector3D(), normal.ToVector3D());
            var result = cutter.Cut();
            Console.WriteLine($"Cutting result = {result}");
            Console.WriteLine($"Cut loops = {cutter.CutLoops.Count}");
            Console.WriteLine($"Cut spans = {cutter.CutSpans.Count}");
            Console.WriteLine($"Cut faces = {cutter.CutFaceSet?.Count ?? 0}");
            return cutter.Mesh.Compact(compact);
        }

        public static DMesh3 ReduceWithProjection(this DMesh3 mesh, float percent, bool compactResult = true)
            => mesh.Reduce(percent, compactResult, mesh.AABBTree());

        public static DMesh3 Reduce(this DMesh3 mesh, float percent, bool compactResult = true, ISpatial target = null)
        {
            // TODO: not sure what triggers this
            //if (!mesh.CheckValidity(eFailMode: FailMode.ReturnOnly)) return mesh;

            var r = new Reducer(mesh);

            if (target != null)
            {
                r.SetProjectionTarget(new MeshProjectionTarget(mesh, target));

                // http://www.gradientspace.com/tutorials/2017/8/30/mesh-simplification
                // r.ProjectionMode = Reducer.TargetProjectionMode.Inline;
            }

            return r.Reduce((int)(mesh.VertexCount * percent / 100.0), compactResult);
        }


        public static DMesh3 Reduce(this Reducer reducer, int newVertexCount, bool compactResult = true)
        {
            reducer.ReduceToVertexCount(newVertexCount);
            return reducer.Mesh.Compact(compactResult);
        }

        public static DMeshAABBTree3 AABBTree(this DMesh3 mesh)
        {
            var tree = new DMeshAABBTree3(mesh);
            tree.Build();
            return tree;
        }

        public static double? DistanceToTree(this DMeshAABBTree3 tree, Ray3d ray)
        {
            var hit_tid = tree.FindNearestHitTriangle(ray);
            if (hit_tid == DMesh3.InvalidID) return null;
            var intr = MeshQueries.TriangleIntersection(tree.Mesh, hit_tid, ray);
            return ray.Origin.Distance(ray.PointAt(intr.RayParameter));
        }

        public static Vector3d NearestPoint(this DMeshAABBTree3 tree, Vector3d point)
        {
            var tid = tree.FindNearestTriangle(point);
            if (tid == DMesh3.InvalidID)
                throw new Exception("Could not find nearest triangle");

            var dist = MeshQueries.TriangleDistance(tree.Mesh, tid, point);
            return dist.TriangleClosest;
        }

        public static double DistanceToTree(this DMeshAABBTree3 tree, Vector3d point)
        {
            var tid = tree.FindNearestTriangle(point);
            if (tid == DMesh3.InvalidID)
                throw new Exception("Could not find nearest triangle");

            var dist = MeshQueries.TriangleDistance(tree.Mesh, tid, point);
            return Math.Sqrt(dist.GetSquared());
        }

        public static IArray<Vector3> NearestPoints(this TriangleMesh3D self, IArray<Vector3d> points)
        {
            var tree = self.ToG3Sharp().AABBTree();
            return points.Map(p => tree.NearestPoint(p).ToPlato());
        }

        public static List<DMesh3> LoadMeshes(string path)
        {
            var builder = new DMesh3Builder();
            var reader = new StandardMeshReader {MeshBuilder = builder};
            var result = reader.Read(path, ReadOptions.Defaults);
            if (result.code == IOCode.Ok)
                return builder.Meshes;
            return null;
        }

        public static void WriteFile(this DMesh3 mesh, string filePath)
            => mesh.WriteFile(filePath, WriteOptions.Defaults);

        public static void WriteFileBinary(this DMesh3 mesh, string filePath)
            => mesh.WriteFile(filePath, new WriteOptions {bWriteBinary = true});

        public static void WriteFileAscii(this DMesh3 mesh, string filePath)
            => mesh.WriteFile(filePath, new WriteOptions { bWriteBinary = false });

       public static void WriteFile(this DMesh3 mesh, string filePath, WriteOptions opts)
        {
            var writer = new StandardMeshWriter();
            var m = new WriteMesh(mesh);
            var result = writer.Write(filePath, new List<WriteMesh> { m }, opts);
            if (!result.Equals(IOWriteResult.Ok))
                throw new Exception($"Failed to write file to {filePath} with result {result.ToString()}");
        }
        public static IArray<Vector3> ToPlato(this DVector<double> self)
        {
            return (self.Length / 3).MapRange(i => new Vector3(self[i * 3], self[i * 3 + 1], self[i * 3 + 2]));
        }

        public static Vector3 ToPlato(this Vector3d self)
        {
            return (self.x, self.y, self.z);
        }

        public static Bounds3D ToPlato(this AxisAlignedBox3d box)
        {
            return (box.Min.ToPlato(), box.Max.ToPlato());
        }

        public static TriangleMesh3D ToPlato(this DMesh3 self)
        {
            var verts = self.Vertices().Select(ToPlato).ToList().ToIArray();
            var indices = self.TrianglesBuffer.Select(t => (Integer)t).ToList().ToIArray();
            return new TriangleMesh3D(verts, indices);
        }

        public static Vector3d ToVector3D(this Vector3 self)
            => new Vector3d(self.X, self.Y, self.Z);        

        public static Vector3d ToG3Sharp(this Vector3 self)
            => new Vector3d(self.X, self.Y, self.Z);        

        public static DMesh3 ToG3Sharp(this TriangleMesh3D self)
        {
            var r = new DMesh3();
            foreach (var v in self.Points)
                r.AppendVertex(v.ToVector3D());
            var indices = self.Indices;
            for (var i = 0; i < indices.Count; i += 3)
            {
                var result = r.AppendTriangle(indices[i], indices[i + 1], indices[i + 2]);
                if (result < 0)
                {
                    if (result == DMesh3.NonManifoldID)
                        throw new Exception("Can't create non-manifold mesh");
                    if (result == DMesh3.InvalidID)
                        throw new Exception("Invalid vertex ID");
                    throw new Exception("Unknown error creating mesh");
                }
            }
            Debug.Assert(r.CheckValidity(false, FailMode.DebugAssert));
            return r;
        }

        public static TriangleMesh3D Reduce(this TriangleMesh3D self, float percent)
            => self.ToG3Sharp().Reduce(percent).ToPlato();

        public static TriangleMesh3D Reduce(TriangleMesh3D source, int vertexCount, bool project = false, bool compact = true)
        {
            var dmesh = source.ToG3Sharp();
            var reducer = new Reducer(dmesh);

            reducer.SetExternalConstraints(new MeshConstraints());
            MeshConstraintUtil.FixAllBoundaryEdges(reducer.Constraints, dmesh);

            // reducer.ProjectionMode = Reducer.TargetProjectionMode.

            reducer.PreserveBoundaryShape = true;
            if (project)
            {
                var aabbTree = dmesh.AABBTree();
                var projectionTarget = new MeshProjectionTarget(dmesh, aabbTree);
                reducer.SetProjectionTarget(projectionTarget);
            }

            return reducer.Reduce(vertexCount, compact).ToPlato();
        }
    }
}
