namespace Ara3D.Geometry
{
    public static class ParametricSurfaces
    {
        public static ParametricSurface Sphere
            => new ParametricSurface(SurfaceFunctions.Sphere, true, true);

        public static ParametricSurface Torus(float r1, float r2)
            => new ParametricSurface(uv => uv.Torus(r1, r2), true, true);

        public static ParametricSurface MonkeySaddle
            => new ParametricSurface(SurfaceFunctions.MonkeySaddle, false, false);

        public static ParametricSurface Plane
            => new ParametricSurface(SurfaceFunctions.Plane, false, false);

        public static ParametricSurface Disc
            => new ParametricSurface(SurfaceFunctions.Disc, false, false);

        public static ParametricSurface Cylinder
            => new ParametricSurface(SurfaceFunctions.Cylinder, false, false);

        public static ParametricSurface ConicalSection(float r1, float r2)
            => new ParametricSurface(uv => uv.ConicalSection(r1, r2), true, false);

        public static ParametricSurface Trefoil(float r)
            => new ParametricSurface(uv => SurfaceFunctions.Trefoil(uv, r), true, true);
    }
}