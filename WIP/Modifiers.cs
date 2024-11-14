using System;
using Ara3D.Collections;
using Ara3D.Mathematics;
using Ara3D.Utils;

namespace Ara3D.Geometry
{
    public enum Axis
    {
        X,
        Y,
        Z,
        NegativeX,
        NegativeY,
        NegativeZ,
    }

    public static class Modifiers
    {
        public static TriMesh Triangulate(this IQuadMesh self)
            => new TriMesh(self.Points, self.FaceIndices.SelectMany(f =>
                LinqArray.Create(
                    new Int3(f.X, f.Y, f.Z),
                    new Int3(f.Z, f.W, f.X))));

        public static TriMesh FlipFaces(this ITriMesh mesh)
            => new TriMesh(mesh.Points, mesh.FaceIndices.Select(f => new Int3(f.Z, f.Y, f.X)));

        public static TriMesh Faceted(this ITriMesh mesh)
            => mesh.Points.SelectByIndex(mesh.Indices).ToTriMesh();

        public static ITriMesh ResetPivot(this ITriMesh triMesh)
            => triMesh.Translate(-triMesh.BoundingBox().CenterBottom);

        public static T SnapPoints<T>(this T mesh, float snapSize) where T : IDeformable<T>
            => MathUtil.Abs(snapSize) >= Constants.Tolerance
                ? mesh.Deform(v => (v * MathUtil.Inverse(snapSize)).Truncate() * snapSize)
                : mesh.Deform(v => Vector3.Zero);

        //==

        public static Vector3 Skew(Vector3 v, Line line, Vector3 from, Vector3 to)
            => throw new NotImplementedException();

        public static Vector3 Taper(Vector3 v)
            => throw new NotImplementedException();

        public static Vector3 Bend(Vector3 v)
            => throw new NotFiniteNumberException();

        public static Vector3 Twist(Vector3 v)
            => throw new NotImplementedException();

        public static Line Axis(this AABox box, Axis axis)
            => throw new NotImplementedException();

        public static Vector3 Translate(Vector3 v, Vector3 amount)
            => throw new NotImplementedException();

        public static Func<Vector3, Vector3> ApplyFallOff(Func<Vector3, Vector3> f, Func<Vector3, float> fallOff)
            => throw new NotImplementedException();
        
        public static Func<Vector3, float> AmountAlongLine(Vector3 v, Line line)
            => throw new NotImplementedException();

        public static Func<Vector3, float> AmountAlongAxis(Vector3 v, AABox box, Axis axis)
            => throw new NotImplementedException();

        public static Func<Vector3, float> DistanceAsAmount(Vector3 v, Vector3 center, float max)
            => throw new NotImplementedException();

    }
}