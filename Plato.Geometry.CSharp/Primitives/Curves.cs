namespace Plato.DoublePrecision
{
    public static class Curves
    {
        /* TODO: this has to be updated. 
        public static Curve2D Circle 
            => new Curve2D(x => x.CircleFunction, true);

        public static Curve3D TorusKnot(int p, int q)
            => new Curve3D(x => TorusKnotFunction(x.Turns, p, q), true);
        
        // https://en.wikipedia.org/wiki/Torus_knot
        public static Vector3D TorusKnotFunction(this Angle t, int p, int q)
        {
            var r = (q * t).Cos + 2;
            var x = r * (p * t).Cos;
            var y = r * (p * t).Sin;
            var z = -(q * t).Sin;
            return (x, y, z);
        }

        public static Curve3D TrefoilKnot
            => new Curve3D(x => x.Turns.TrefoilKnotFunction(), true);

        // https://en.wikipedia.org/wiki/Trefoil_knot
        public static Vector3D TrefoilKnotFunction(this Angle t)
            => (t.Sin + (2 * t).Sin * 2,
                t.Cos + (2 * t).Cos * 2,
                -(3 * t).Sin);

        public static Curve3D FigureEightKnot
            => new Curve3D(x => x.Turns.FigureEightKnotFunction(), true);

        // https://en.wikipedia.org/wiki/Figure-eight_knot_(mathematics)
        public static Vector3D FigureEightKnotFunction(this Angle t)
            => ((2 + (2 * t).Cos) * (3 * t).Cos,
                (2 + (2 * t).Cos) * (3 * t).Sin,
                (4 * t).Sin);

        public static Curve3D Helix(Number revolutions)
            => new Curve3D(x => x.HelixFunction(revolutions), true);

        // https://en.wikipedia.org/wiki/Parametric_equation#Helix
        public static Vector3D HelixFunction(this Number t, Number revolutions)
            => ((t * revolutions).Turns.Sin,
                (t * revolutions).Turns.Cos,
                t);

            */

    }
}