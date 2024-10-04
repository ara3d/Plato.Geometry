using Plato.DoublePrecision;
using System;

namespace Plato.Geometry.Scenes
{
    public interface ITransform3D
    {
        Vector3D TransformPoint(Vector3D point);
        Vector3D TransformVector(Vector3D vector);
        Matrix4x4 Matrix { get; }
    }

    public class TRSTransform : ITransform3D
    {
        public TRSTransform(Transform3D transform) => Transform = transform;
        public Transform3D Transform { get; }
        public Matrix4x4 Matrix => Transform;
        public Vector3D TransformPoint(Vector3D point) => Transform.TransformPoint(point);
        public Vector3D TransformVector(Vector3D vector) => Transform.TransformVector(vector);
        public static implicit operator TRSTransform(Transform3D transform) => new TRSTransform(transform);
        public static implicit operator TRSTransform(Quaternion q) => new Transform3D(Vector3D.Default, q, (1,1,1));
        public static Quaternion YUpToZUp = Quaternion.FromAxisAngle((1, 0, 0), -Math.PI / 2);
    }

    public class MatrixTransform : ITransform3D
    {
        public MatrixTransform(Matrix4x4 matrix) => Matrix = matrix;
        public Matrix4x4 Matrix { get; }
        public Vector3D TransformPoint(Vector3D point) => Matrix * point;
        public Vector3D TransformVector(Vector3D point) => Matrix.TransformVector(point);
    }

    public class NullTransform : ITransform3D
    {
        public Vector3D TransformPoint(Vector3D point) => point;
        public Vector3D TransformVector(Vector3D vector) => vector;
        public Matrix4x4 Matrix { get; } = new Matrix4x4((1, 0, 0, 0), (0, 1, 0, 0), (0, 0, 1, 0), (0, 0, 0, 0));
        public static NullTransform Instance { get; } = new NullTransform();
    }
}