using System;
using Plato.DoublePrecision;
using Boolean = Plato.DoublePrecision.Boolean;

namespace Plato.Geometry
{
    public static class SurfaceFunctions
    {

        public static Vector3D Sphere(this Vector2D uv)
            => Sphere(uv.X.Turns, uv.Y.Turns);

        public static Vector3D Sphere(Angle u, Angle v)
            => (-u.Cos * v.Sin, v.Cos, u.Sin * v.Sin);

        // https://en.wikipedia.org/wiki/Torus#Geometry
        public static Vector3D Torus(this Vector2D uv, Number r1, Number r2)
            => Torus(uv.X.Turns, uv.Y.Turns, r1, r2);

        public static Vector3D Torus(Angle u, Angle v, Number r1, Number r2)
            => ((r1 + r2 * u.Cos) * v.Cos,
                (r1 + r2 * u.Cos) * v.Sin,
                r2 * u.Sin);

        public static Vector3D PlaneXY(this Vector2D uv)
            => uv;

        public static Vector2D Disc(this Vector2D uv)
            => uv.X.Turns.CircleFunction * uv.Y;

        public static Vector3D Disc3D(this Vector2D uv)
            => uv.Disc();

        public static Vector3D Cylinder(this Vector2D uv)
            => ((Vector3D)uv.X.Turns.CircleFunction).WithZ(uv.Y);

        public static Vector3D ConicalSection(this Vector2D uv, Number r1, Number r2)
            => (uv.X.CircleFunction * r1.Lerp(r2, uv.Y)).Vector3D.WithZ(uv.Y);

        public static Vector3D Capsule(this Vector2D uv)
        {
            uv *= (1, 2);
            if (uv.Y < 0.5) return Sphere((uv.Y, uv.X));
            if (uv.Y > 1.5) return Sphere((uv.Y - 1, uv.X)) + (0, 0, 1);
            return (uv + (0, -0.5f)).Cylinder();
        }

        public static Angle Turns(this int n)
            => ((Number)n).Turns;

        // https://commons.wikimedia.org/wiki/File:Parametric_surface_illustration_(trefoil_knot).png
      

        //===
        // Height fields converted into surface functions 
        //===

        /*
        public static Func<Vector2D, Vector3D> ToSurfaceFunction(this Func<Vector2D, float> f)
            => uv => (uv.X, uv.Y, f(uv));

        public static Vector3D MonkeySaddle(this Vector2D uv)
            => ToSurfaceFunction(HeightFieldFunctions.MonkeySaddle)(uv);

        public static Vector3D Handkerchief(this Vector2D uv)
            => ToSurfaceFunction(HeightFieldFunctions.Handkerchief)(uv);

        public static Vector3D CrossedTrough(this Vector2D uv)
            => ToSurfaceFunction(HeightFieldFunctions.CrossedTrough)(uv);

        public static Vector3D SinPlusCos(this Vector2D uv)
            => ToSurfaceFunction(HeightFieldFunctions.SinPlusCos)(uv);
        */
    }

    /*
    public class SurfaceImpl : ParametricSurface
    {
        private Func<Vector2D, Vector3D> Func;

        public SurfaceImpl(Func<Vector2D, Vector3D> f, bool closedX, bool closedY)
        {
            Func = f;
            PeriodicU = closedX;
            PeriodicV = closedY;
        }

        public Vector3D Eval(Vector2D p)
            => Func(p);

        public Boolean PeriodicU { get; }
        public Boolean PeriodicV { get; }
    }
    */

    /// <summary>
    /// Usually the result of sampling a parametric surface.
    /// Each point (and vector to it) can be referred to by index, as can the quadrant formed between it and the the next point. See below:
    ///      a
    ///    3 | 0
    /// d -- p -- b
    ///    2 | 1
    ///      c
    /// The output tangent vector is from p to b.
    /// The output binormal vector is from p to a.
    /// The input tangent vector is from d to p.
    /// The input binormal vector is from c to p. 
    /// </summary>
    public interface ISurfacePoint
    {
        Vector2D UV { get; }
        Vector3D InBinormal { get; }
        Vector3D OutBinormal { get; }
        Vector3D InTangent { get; }
        Vector3D OutTangent { get; }
    }

    public class SurfacePoint
        : ISurfacePoint
    {
        public SurfacePoint(Vector3D center, Vector2D uv, Vector3D a, Vector3D b, Vector3D c, Vector3D d)
        {
            Center = center;
            UV = uv;
            V0 = a;
            V1 = b;
            V2 = c;
            V3 = d;
        }
        public Vector3D Center { get; }
        public Vector2D UV { get; }
        public Vector3D Normal => OutTangent.Cross(OutBinormal);
        public Vector3D V0 { get; }
        public Vector3D V1 { get; }
        public Vector3D V2 { get; }
        public Vector3D V3 { get; }
        public Vector3D InBinormal => Center - V2;
        public Vector3D OutBinormal => V0 - Center;
        public Vector3D InTangent => Center - V3;
        public Vector3D OutTangent => V1 - Center;

        public static SurfacePoint Create(Func<Vector2D, Vector3D> f, Vector2D uv)
        {
            var e = Constants.Epsilon;
            var p = f(uv);
            var a = f(uv - (0, e));
            var b = f(uv + (e, 0));
            var c = f(uv + (0, e));
            var d = f(uv - (e, 0));
            return new SurfacePoint(p, uv, a, b, c, d);
        }
    }


    /// <summary>
    /// Many of these functions, once validated, will move into the Plato library.
    /// </summary>
    public static class Extensions
    {
        //==
        // Surface constructors
        /*
        public static QuadMesh ToQuadMesh(this ParametricSurface s)
        {

        }

        public static TriMesh ToTriMesh(this QuadMesh q)
        {

        }*/

    }
}
