using System;

namespace Ara3D.Mathematics
{
  

    public partial struct Vector4 : ITransformable<Vector4>
    {
        public Vector4(Vector3 v, float w)
            : this(v.X, v.Y, v.Z, w)
        { }

        public Vector4(Vector2 v, float z, float w)
            : this(v.X, v.Y, z, w)
        { }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        public Vector4 Transform(Matrix4x4 matrix)
            => (
                X * matrix.M11 + Y * matrix.M21 + Z * matrix.M31 + W * matrix.M41,
                X * matrix.M12 + Y * matrix.M22 + Z * matrix.M32 + W * matrix.M42,
                X * matrix.M13 + Y * matrix.M23 + Z * matrix.M33 + W * matrix.M43,
                X * matrix.M14 + Y * matrix.M24 + Z * matrix.M34 + W * matrix.M44);

        public Vector3 XYZ => new Vector3(X, Y, Z);
        public Vector2 XY => new Vector2(X, Y);
    }

    public partial struct Vector3 : ITransformable<Vector3>
    {
        public Vector3(float x, float y)
            : this(x, y, 0)
        { }

        public Vector3(Vector2 xy, float z)
            : this(xy.X, xy.Y, z)
        { }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        public Vector3 Transform(Matrix4x4 matrix)
            => (
                X * matrix.M11 + Y * matrix.M21 + Z * matrix.M31 + matrix.M41,
                X * matrix.M12 + Y * matrix.M22 + Z * matrix.M32 + matrix.M42,
                X * matrix.M13 + Y * matrix.M23 + Z * matrix.M33 + matrix.M43);

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        public Vector3 Cross(Vector3 vector2)
            => new Vector3(
                Y * vector2.Z - Z * vector2.Y,
                Z * vector2.X - X * vector2.Z,
                X * vector2.Y - Y * vector2.X);

        /// <summary>
        /// Returns the mixed product
        /// </summary>
        public double MixedProduct(Vector3 v1, Vector3 v2)
            => Cross(v1).Dot(v2);

        /// <summary>
        /// Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        public Vector3 Reflect(Vector3 normal)
            => this - normal * Dot(normal) * 2f;

        /// <summary>
        /// Transforms a vector normal by the given matrix.
        /// </summary>
        public Vector3 TransformNormal(Matrix4x4 matrix)
            => new Vector3(
                X * matrix.M11 + Y * matrix.M21 + Z * matrix.M31,
                X * matrix.M12 + Y * matrix.M22 + Z * matrix.M32,
                X * matrix.M13 + Y * matrix.M23 + Z * matrix.M33);

        public Vector3 Clamp(AABox box)
            => this.Clamp(box.Min, box.Max);

        public Vector2 XY => new Vector2(X, Y);
        public Vector2 XZ => new Vector2(X, Z);
        public Vector2 YZ => new Vector2(Y, Z);
        public Vector3 XZY => new Vector3(X, Z, Y);
        public Vector3 ZXY => new Vector3(Z, X, Y);
        public Vector3 ZYX => new Vector3(Z, Y, Z);
        public Vector3 YXZ => new Vector3(Y, X, Z);
        public Vector3 YZX => new Vector3(Y, Z, X);
    }

    public partial struct Line : ITransformable<Line>, IPoints, IMappable<Line, Vector3>
    {
        public Vector3 Vector => B - A;
        public Ray Ray => new Ray(A, Vector);
        public float Length => A.Distance(B);
        public float LengthSquared => A.DistanceSquared(B);
        public Vector3 MidPoint => A.Average(B);
        public Line Normalize => new Line(A, A + Vector.Normalize());
        public Line Inverse => new Line(B, A);

        public Vector3 Lerp(float amount)
            => A.Lerp(B, amount);

        public Line SetLength(float length)
            => new Line(A, A + Vector.Along(length));

        public Line Transform(Matrix4x4 mat)
            => (A.Transform(mat), B.Transform(mat));

        public int NumPoints => 2;

        public Vector3 GetPoint(int n) => n == 0 ? A : B;

        public Line Map(Func<Vector3, Vector3> f)
            => new Line(f(A), f(B));
    }

  

    public partial struct Vector2
    {
        public Vector3 ToVector3()
            => new Vector3(X, Y, 0);

       public static implicit operator Vector3(Vector2 self)
            => self.ToVector3();

        public double PointCrossProduct(Vector2 other) => X * other.Y - other.X * Y;

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        public float Cross(Vector2 v2) => X * v2.Y - Y * v2.X;
    }

    public partial struct Line2D
    {
        public AABox2D BoundingBox() => AABox2D.Create(A.Min(B), A.Max(B));

        public double LinePointCrossProduct(Vector2 point)
        {
            var tmpLine = new Line2D(Vector2.Zero, B - A);
            var tmpPoint = point - A;
            return tmpLine.B.PointCrossProduct(tmpPoint);
        }

        public bool IsPointOnLine(Vector2 point)
            => Math.Abs(LinePointCrossProduct(point)) < Constants.Tolerance;

        public bool IsPointRightOfLine(Vector2 point)
            => LinePointCrossProduct(point) < 0;

        public bool TouchesOrCrosses(Line2D other)
            => IsPointOnLine(other.A)
               || IsPointOnLine(other.B)
               || (IsPointRightOfLine(other.A) ^ IsPointRightOfLine(other.B));

        public bool Intersects(AABox2D thisBox, Line2D otherLine, AABox2D otherBox)
            => thisBox.Intersects(otherBox)
               && TouchesOrCrosses(otherLine)
               && otherLine.TouchesOrCrosses(this);

        public bool Intersects(Line2D other) =>
            // Inspired by: https://martin-thoma.com/how-to-check-if-two-line-segments-intersect/
            Intersects(BoundingBox(), other, other.BoundingBox());

        public Vector2 Vector 
            => (B - A).Normalize();

        public Vector2 Tangent 
            => (-Vector.Y, Vector.X);

        public Line2D ParallelOffset(float x = 1)
            => Offset(Tangent * x);

        public (Line2D, Line2D) ParallelOffsets(float x = 1)
            => (ParallelOffset(x), ParallelOffset(-x));

        public Line2D Offset(Vector2 v)
            => (A + v, B + B);

        public Vector2 Lerp(float t)
            => A.Lerp(B, t);

        public float LengthSquared()
            => (B - A).LengthSquared();

        public float Length()
            => LengthSquared().Sqrt();
    }

    public partial struct Pose
    {
        public static Pose Identity => new Pose(Vector3.Zero, Quaternion.Identity);

        public static implicit operator Pose(Vector3 position)
            => (position, Quaternion.Identity);

        public static implicit operator Pose(Quaternion rotation)
            => (Vector3.Zero, rotation);

        public static implicit operator Matrix4x4(Pose pose)
            => Matrix4x4.CreateTranslationRotation(pose.Position, pose.Orientation);

        public static Pose Create(Vector3 position, Vector3 forward, Vector3 normal)
            => (position, Quaternion.CreateRotationFromAToB(forward, normal));

        public static Pose operator -(Pose a, Pose b)
            => (a.Position - b.Position, a.Orientation.RotationalDifference(b.Orientation));
    }

    /// <summary>
    /// A frame of reference. Defined as a location, a forward vector and an up vector.
    /// Facilitates converting between different coordinate systems, particularly when scale is not concerned. 
    /// By default the 2D plane is assumed to have the Y-axis "(0,1)" as up
    /// and the X-axis "(1,0)" as the right vector. 
    /// This implies that the negative Z axis is forward "(0,0,-1)".
    /// We imagine shapes on the 2D plane as if they are on a sheet of paper.  
    /// The forward and up vector are assumed to be orthogonal,
    /// or at least non-parallel. If they are parallel then the reference frame will choose one arbitrary arbitrary
    /// https://en.wikipedia.org/wiki/Frame_of_reference
    /// </summary>
    public partial struct Frame
    {
        public Frame Normalize()
            => (Position, Forward.Normalize(), Up.Normalize());

        public static Frame Default2D 
            = new Frame(Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY);

        public Vector3 Right
            => Forward.Cross(Up).Normalize();

        public Quaternion GetRotation(Vector3 newForwardVector)
            => Quaternion.CreateRotationFromAToB(Forward, newForwardVector);

        public Vector3 Align(Vector2 v)
            => v.ToVector3().Transform(GetAlignmentMatrix2DPlane());

        public Frame CreateNormalized(Vector3 position, Vector3 forward, Vector3 up)
            => (position, forward.Normalize(), up.Normalize());

        public Pose GetAlignmentPoseFrom2DPlane()
            => (Position, Default2D.GetRotation(Up));

        public Matrix4x4 GetAlignmentMatrix2DPlane()
            => GetAlignmentPoseFrom2DPlane();
    }

    /// <summary>
    /// A method of representing an orientation, that includes only that does not allow "roll". 
    /// Can be useful for first person cameras.
    /// https://en.wikipedia.org/wiki/Horizontal_coordinate_system
    /// </summary>
    public partial struct HorizontalCoordinate
    {
        public float Yaw => Azimuth;
        public float Pitch => Inclination;
        public float Roll => 0;
        
        public static explicit operator Vector2(HorizontalCoordinate angle)
            => (angle.Azimuth, angle.Inclination);
        
        public static implicit operator HorizontalCoordinate(Vector2 vector)
            => (vector.X, vector.Y);
    }

}
