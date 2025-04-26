namespace Plato.SinglePrecision
{
    public static class Shapes
    {
        public static readonly Quad3D QuadXY 
            = Quad2D.Unit.To3D;
        
        public static readonly Quad3D QuadXZ 
            = new Quad3D((0, 0, 0), (1, 0, 0), (1, 0, 1), (0, 0, 1));
        
        public static readonly Quad3D QuadYZ 
            = new Quad3D((0, 0, 0), (0, 1, 0), (0, 1, 1), (0, 0, 1));

        public static QuadGrid3D Sphere(int resolution)
            => Extensions2.ToQuadGrid(SphereFunction, resolution, true, true);

        public static QuadGrid3D Plane(int resolution)
            => Extensions2.ToQuadGrid(PlaneXYFunction, resolution, true, true);

        public static QuadGrid3D Cylinder(int resolution)
            => Extensions2.ToQuadGrid(CylinderFunction, resolution, true, true);

        public static QuadGrid3D Capsule(int resolution)
            => Extensions2.ToQuadGrid(CapsuleFunction, resolution, true, true);

        public static QuadGrid3D Disc(int resolution)
            => Extensions2.ToQuadGrid(uv => DiscFunction(uv), resolution, true, false);

        public static Vector3 SphereFunction(this Vector2 uv)
            => SphereFunction(uv.X.Turns, uv.Y.Turns);

        public static Vector3 SphereFunction(Angle u, Angle v)
            => (-u.Cos * v.Sin, v.Cos, u.Sin * v.Sin);

        // https://en.wikipedia.org/wiki/Torus#Geometry

        public static QuadGrid3D Torus(int resolution)
            => Extensions2.ToQuadGrid(uv => TorusFunction(uv, 1.0, 0.2), resolution, true, true);

        public static Vector3 TorusFunction(this Vector2 uv, Number r1, Number r2)
            => TorusFunction(uv.X.Turns, uv.Y.Turns, r1, r2);

        public static Vector3 TorusFunction(Angle u, Angle v, Number r1, Number r2)
            => ((r1 + r2 * u.Cos) * v.Cos,
                (r1 + r2 * u.Cos) * v.Sin,
                r2 * u.Sin);

        public static Vector3 PlaneXYFunction(this Vector2 uv)
            => uv.Vector3(0);

        public static Vector2 DiscFunction(this Vector2 uv)
            => uv.X.Turns.UnitCircle * (1 - uv.Y);

        public static Vector3 CylinderFunction(this Vector2 uv)
            => ((Vector3)uv.X.Turns.UnitCircle).WithZ(uv.Y);

        public static Vector3 ConicalSectionFunction(this Vector2 uv, Number r1, Number r2)
            => (uv.X.Turns.UnitCircle * r1.Lerp(r2, uv.Y)).Vector3.WithZ(uv.Y);

        public static Vector3 CapsuleFunction(this Vector2 uv)
        {
            uv *= (1, 2);
            if (uv.Y < 0.5) return SphereFunction((uv.Y, uv.X));
            if (uv.Y > 1.5) return SphereFunction((uv.Y - 1, uv.X)) + (0, 0, 1);
            return (uv + (0, -0.5f)).CylinderFunction();
        }

        public static Triangle3D TriangleXY = Triangle2D.Unit.To3D;
        public static Triangle3D TriangleXZ = (TriangleXY.A.XZY, TriangleXY.B.XZY, TriangleXY.C.XZY);
        public static Triangle3D TriangleYZ = (TriangleXY.A.YZX, TriangleXY.B.YZX, TriangleXY.C.YZX);

        public static Triangle3D TriangleYX = TriangleXY.Flip;
        public static Triangle3D TriangleZX = TriangleXZ.Flip;
        public static Triangle3D TriangleZY = TriangleYZ.Flip;
    }
}