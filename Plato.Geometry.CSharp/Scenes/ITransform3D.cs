using Plato.DoublePrecision;

namespace Plato.Geometry.Scenes
{
    public interface ITransform3D
    {
        Vector3D TransformPoint(Vector3D point);
        Vector3D TransformVector(Vector3D vector);
    }

    public class TRSTransform : ITransform3D
    {
        public TRSTransform(Transform3D transform) => Transform = transform;
        public Transform3D Transform { get; }
        public Vector3D TransformPoint(Vector3D point) => Transform.TransformPoint(point);
        public Vector3D TransformVector(Vector3D vector) => Transform.TransformVector(vector);
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
        public static NullTransform Instance { get; } = new NullTransform();
    }
}