using System;
using System.Collections.Generic;
using System.Text;
using Plato.DoublePrecision;

namespace Plato.Geometry.Experimental
{
    public interface ISignedDistanceField
    {
        Number Distance(Vector3D point);
    }

    public class SignedDistanceField : ISignedDistanceField
    {
        public SignedDistanceField(Func<Vector3D, Number> func) => Func = func;
        public Func<Vector3D, Number> Func;
        public Number Distance(Vector3D point) => Func(point);
    }

    public enum SDFType
    {
        XYPlane,
        XZPlane,
        YZPlane,
        XAxis,
        YAxis,
        ZAxis,
        Sphere,
        Cylinder,
        Capsule,
        Box,
        Cone,
        Pyramid,
        Torus,
    }

    public static class SignedDistanceFields
    {
        public static SignedDistanceField XYPlane => new SignedDistanceField(XYPlaneDistance);
        public static SignedDistanceField XZPlane => new SignedDistanceField(XZPlaneDistance);
        public static SignedDistanceField YZPlane => new SignedDistanceField(YZPlaneDistance);

        public static SignedDistanceField XAxis => new SignedDistanceField(XAxisDistance);
        public static SignedDistanceField YAxis => new SignedDistanceField(YAxisDistance);
        public static SignedDistanceField ZAxis => new SignedDistanceField(ZAxisDistance);

        public static SignedDistanceField Sphere => new SignedDistanceField(SphereDistance);
        public static SignedDistanceField Cylinder => new SignedDistanceField(CylinderDistance);
        public static SignedDistanceField Capsule => new SignedDistanceField(CapsuleDistance);
        public static SignedDistanceField Box => new SignedDistanceField(BoxDistance);
        public static SignedDistanceField Cone => new SignedDistanceField(p => ConeDistance(p));
        public static SignedDistanceField Pyramid => new SignedDistanceField(PyramidDistance);
        public static SignedDistanceField Torus => new SignedDistanceField(TorusDistance);

        public static SignedDistanceField GetField(SDFType type)
        {
            switch (type)
            {
                case SDFType.XYPlane: return XYPlane;
                case SDFType.XZPlane: return XZPlane;
                case SDFType.YZPlane: return YZPlane;
                case SDFType.XAxis: return XAxis;
                case SDFType.YAxis: return YAxis;
                case SDFType.ZAxis: return ZAxis;
                case SDFType.Sphere: return Sphere;
                case SDFType.Cylinder: return Cylinder;
                case SDFType.Capsule: return Capsule;
                case SDFType.Box: return Box;
                case SDFType.Cone: return Cone;
                case SDFType.Pyramid: return Pyramid;
                case SDFType.Torus: return Torus;
            };
            return Sphere;
        }

        public static SignedDistanceField Union(SignedDistanceField a, SignedDistanceField b)
        {
            return new SignedDistanceField(p => a.Distance(p).Min(b.Distance(p)));
        }

        public static SignedDistanceField Intersection(SignedDistanceField a, SignedDistanceField b)
        {
            return new SignedDistanceField(p => a.Distance(p).Max(b.Distance(p)));
        }

        public static SignedDistanceField Difference(SignedDistanceField a, SignedDistanceField b)
        {
            return new SignedDistanceField(p => a.Distance(p).Max(-b.Distance(p)));
        }

        public static SignedDistanceField Transform(SignedDistanceField sdf, ITransform3D t)
        {
            // TODO: technically this should inverse the transform, but I haven't implemented that yet
            return new SignedDistanceField(p => sdf.Distance(t.Transform(p)));
        }

        public static SignedDistanceField Inverse(SignedDistanceField sdf)
        {
            return new SignedDistanceField(p => -sdf.Distance(p));
        }

        public static SignedDistanceField Lerp(SignedDistanceField a, SignedDistanceField b, Number t)
        {
            return new SignedDistanceField(p => a.Distance(p).Lerp(b.Distance(p), t));
        }

        public static Number XYPlaneDistance(Vector3D v) => v.Z;
        public static Number XZPlaneDistance(Vector3D v) => v.Y;
        public static Number YZPlaneDistance(Vector3D v) => v.X;
        public static Number SphereDistance(Vector3D v) => v.Length - 1.0;
        public static Number XAxisDistance(Vector3D v) => v.YZ.Length;
        public static Number YAxisDistance(Vector3D v) => v.XZ.Length;
        public static Number ZAxisDistance(Vector3D v) => v.XY.Length;
        public static Number CylinderDistance(Vector3D v) => new Vector2D(v.X, v.Z).Length - 1.0;
        public static Number CapsuleDistance(Vector3D v) => Math.Max(new Vector2D(v.X, v.Z).Length - 1.0, Math.Abs(v.Y) - 1.0);

        public static Number BoxDistance(Vector3D v) => new Vector3D(Math.Max(Math.Abs(v.X) - 1.0, 0.0), Math.Max(Math.Abs(v.Y) - 1.0, 0.0), Math.Max(Math.Abs(v.Z) - 1.0, 0.0)).Length;

        public static Number ConeDistance(Vector3D p, double h = 1, double r = 1)
        {
            // Shift the cone to be centered at the origin
            var halfHeight = h / 2;
            var qx = Math.Sqrt(p.X * p.X + p.Y * p.Y);
            var qy = p.Z + halfHeight;

            // Cone parameters
            var k = r / h; // Slope of the cone's side
            var c = -k * qx + qy; // Dot product with cone direction vector

            // Clamp the projection onto the cone's axis
            var f = Math.Min(Math.Max(c, 0.0), h);

            // Normalization factor
            var nk = Math.Sqrt(k * k + 1.0);
            var nx = -k / nk;
            var ny = 1.0f / nk;

            // Compute the distance
            var dx = qx - k * f;
            var dy = qy - f;
            var d = Math.Sqrt(dx * dx + dy * dy);

            // Determine the sign based on the point's location
            var s = (nx * dx + ny * dy) < 0.0 ? 1.0 : -1.0;

            return s * d;
        }

        public static Number PyramidDistance(Vector3D v)
        {
            var q = new Vector3D(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z));
            var d = Math.Max(q.Z - 1.0, Math.Max(q.X, q.Y) - 1.0);
            return d;
        }

        public static Number TorusDistance(Vector3D v)
        {
            var q = new Vector2D(new Vector2D(v.X, v.Z).Length - 1.0, v.Y);
            var d = q.Length - 0.5;
            return d;
        }
    }
}
