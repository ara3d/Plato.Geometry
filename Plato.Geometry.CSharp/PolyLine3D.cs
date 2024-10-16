using System;

namespace Plato.DoublePrecision
{
    public class PolyLine3D : IPolyLine3D, IDeformable3D<PolyLine3D>
    {
        public IArray<Vector3D> Points { get; }
        public Boolean Closed { get; }

        public PolyLine3D(IArray<Vector3D> points, Boolean closed)
        {
            Points = points;
            Closed = closed;
        }

        public IArray<Line3D> ToLines()
        {
            var n = Closed ? Points.Count : Points.Count - 1;
            return n.MapRange(i => new Line3D(Points[i], Points.ModuloAt(i + 1)));
        }

        public PolyLine3D Deform(Func<Vector3D, Vector3D> f)
            => new PolyLine3D(Points.Map(f), Closed);

        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);

        public PolyLine3D Transform(Matrix4x4 matrix)
            => Deform(matrix.TransformPoint);

        ITransformable3D ITransformable3D.Transform(Matrix4x4 matrix)
            => Transform(matrix);

        public static implicit operator PointArray(PolyLine3D lines)
            => lines.Points.ToPoints();
    }
}