using System.Collections.Generic;
using System.Diagnostics;

namespace Plato.DoublePrecision
{
    public static class Polygons
    {
        public static PolyLine2D RegularPolygon(int n)
            => Curves.Circle.ToPolyLine2D(n);

        public static readonly PolyLine2D Triangle = RegularPolygon(3);
        public static readonly PolyLine2D Square = RegularPolygon(4);
        public static readonly PolyLine2D Pentagon = RegularPolygon(5);
        public static readonly PolyLine2D Hexagon = RegularPolygon(6);
        public static readonly PolyLine2D Heptagon = RegularPolygon(7);
        public static readonly PolyLine2D Septagon = RegularPolygon(7);
        public static readonly PolyLine2D Octagon = RegularPolygon(8);
        public static readonly PolyLine2D Nonagon = RegularPolygon(9);
        public static readonly PolyLine2D Decagon = RegularPolygon(10);
        public static readonly PolyLine2D Dodecagon = RegularPolygon(12);
        public static readonly PolyLine2D Icosagon = RegularPolygon(20);
        public static readonly PolyLine2D Centagon = RegularPolygon(100);

        public static PolyLine2D RegularStarPolygon(int p, int q)
            => CirclePoints(p).EveryNth(q).ToPolyLine2D(true);

        public static IArray<Vector2D> CirclePoints(int n)
            => Curves.Circle.Sample<Vector2D>(n);

        public static PolyLine2D StarFigure(Integer p, Integer q)
        {
            Debug.Assert(p > 1);
            Debug.Assert(q > 1);
            if (p.RelativelyPrime(q))
                return RegularStarPolygon(p, q);
            var points = CirclePoints(p);
            var r = new List<Vector2D>();
            var connected = new bool[p];
            for (var i = 0; i < p; ++i)
            {
                if (connected[i])
                    break;
                var j = i;
                while (j != i)
                {
                    r.Add(points[j]);
                    j = (j + q) % p;
                    if (connected[j])
                        break;
                    connected[j] = true;
                }
            }
            return new PolyLine2D(r.ToIArray(), false);
        }

        // https://en.wikipedia.org/wiki/Pentagram
        public static readonly PolyLine2D Pentagram
            = RegularStarPolygon(5, 2);

        public static readonly PolyLine2D Hexagram
            = StarFigure(6, 2);

        // https://en.wikipedia.org/wiki/Heptagram
        public static readonly PolyLine2D Heptagram2
            = RegularStarPolygon(7, 2);

        // https://en.wikipedia.org/wiki/Heptagram
        public static readonly PolyLine2D Heptagram3
            = RegularStarPolygon(7, 3);

        // https://en.wikipedia.org/wiki/Octagram
        public static readonly PolyLine2D Octagram
            = RegularStarPolygon(8, 3);

        // https://en.wikipedia.org/wiki/Enneagram_(geometry)
        public static readonly PolyLine2D Nonagram2
            = RegularStarPolygon(9, 2);

        // https://en.wikipedia.org/wiki/Enneagram_(geometry)
        public static readonly PolyLine2D Nonagram4
            = RegularStarPolygon(9, 4);

        // https://en.wikipedia.org/wiki/Decagram_(geometry)
        public static readonly PolyLine2D Decagram
            = RegularStarPolygon(10, 3);

        public static PolyLine2D Star(Integer n, Number outerRadius, Number innerRadius)
            => (n * 2)
                .MapRange(i => 
                    i.FractionalTurn(n * 2).CircleFunction * 
                    (i.IsEven() 
                        ? outerRadius 
                        : innerRadius))
                .ToPolyLine2D(true);
    }
}
