using System;
using System.Collections;
using System.Collections.Generic;

namespace Plato.DoublePrecision
{
    public readonly struct PointArray 
        : IArray<Vector3D>, IPoints3D, IDeformable3D<PointArray>
    {
        public Vector3D At(Integer n)
            => Points[n];

        public Vector3D this[Integer n]
            => At(n);

        public Vector3D this[int index] 
            => Points[index];

        public Integer Count 
            => Points.Count;

        public IArray<Vector3D> Points { get; }

        public PointArray(IArray<Vector3D> points)
            => Points = points;

        public PointArray Deform(Func<Vector3D, Vector3D> f)
            => new PointArray(Points.Map(f));

        public PointArray Transform(Matrix4x4 matrix)
            => Deform(matrix.TransformPoint);

        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);

        ITransformable3D ITransformable3D.Transform(Matrix4x4 matrix)
            => Transform(matrix);

        public PolyLine3D AsPolyLine(bool closed)
            => Points.ToIArray().ToPolyLine3D(closed);

        public static implicit operator PointArray(Vector3D[] points)
            => new PointArray(points.ToIArray());

        public IEnumerator<Vector3D> GetEnumerator()
            => Points.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        int IReadOnlyCollection<Vector3D>.Count 
            => Count;
    }
}