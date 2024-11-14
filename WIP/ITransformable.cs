using System.Linq;

namespace Ara3D.Mathematics
{
    public interface ITransformable<out T>
        where T: ITransformable<T>
    {
        T Transform(Matrix4x4 mat);
    }

    public static class Transformable
    {
        public static Matrix4x4 Multiply(params Matrix4x4[] matrices)
            => matrices.Aggregate(Matrix4x4.Identity, (m1, m2) => m1 * m2);

        public static T Transform<T>(this T self, params Matrix4x4[] matrices) where T : ITransformable<T>
            => self.Transform(Multiply(matrices));

        public static T Translate<T>(this T self, Vector3 offset) where T : ITransformable<T>
            => self.Transform(Matrix4x4.CreateTranslation(offset));

        public static T Translate<T>(this T self, float x, float y, float z) where T : ITransformable<T>
            => self.Translate((x, y, z));

        public static T Rotate<T>(this T self, Quaternion q) where T : ITransformable<T>
            => self.Transform(Matrix4x4.CreateRotation(q));

        public static T Scale<T>(this T self, Vector3 scales) where T : ITransformable<T>
            => self.Transform(Matrix4x4.CreateScale(scales));

        public static T Scale<T>(this T self, float x, float y, float z) where T : ITransformable<T>
            => self.Scale((x, y, z));

        public static T Scale<T>(this T self, float x) where T : ITransformable<T>
            => self.Scale((x, x, x));

        public static T ScaleX<T>(this T self, float x) where T : ITransformable<T>
            => self.Scale(x, 1, 1);

        public static T ScaleY<T>(this T self, float y) where T : ITransformable<T>
            => self.Scale(1, y, 1);

        public static T ScaleZ<T>(this T self, float z) where T : ITransformable<T>
            => self.Scale(1, 1, z);

        public static T LookAt<T>(this T self, Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
            where T : ITransformable<T>
            => self.Transform(Matrix4x4.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector));

        public static T RotateAround<T>(this T self, Vector3 axis, float angle) where T : ITransformable<T>
            => self.Transform(Matrix4x4.CreateFromAxisAngle(axis, angle));

        public static T Rotate<T>(this T self, float yaw, float pitch, float roll) where T : ITransformable<T>
            => self.Transform(Matrix4x4.CreateFromYawPitchRoll(yaw, pitch, roll));

        public static T Reflect<T>(this T self, Plane plane) where T : ITransformable<T>
            => self.Transform(Matrix4x4.CreateReflection(plane));

        public static T RotateX<T>(this T self, float angle) where T : ITransformable<T>
            => self.RotateAround(Vector3.UnitX, angle);

        public static T RotateY<T>(this T self, float angle) where T : ITransformable<T>
            => self.RotateAround(Vector3.UnitY, angle);

        public static T RotateZ<T>(this T self, float angle) where T : ITransformable<T>
            => self.RotateAround(Vector3.UnitZ, angle);

        public static T TranslateRotateScale<T>(this T self, Vector3 pos, Quaternion rot, Vector3 scale)
            where T : ITransformable<T>
            => self.Transform(Matrix4x4.CreateTRS(pos, rot, scale));
    }
}
