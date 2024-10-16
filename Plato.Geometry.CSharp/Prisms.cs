using Plato.DoublePrecision;

namespace Plato.Geometry
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Prism_(geometry)
    /// </summary>
    public static class Prisms
    {
        public static QuadGrid TriangularPrism = Polygons.Triangle.ToPrism();
        public static QuadGrid SquarePrism = Polygons.Square.ToPrism();
        public static QuadGrid PentagonalPrism = Polygons.Pentagon.ToPrism();
        public static QuadGrid HexagonalPrism = Polygons.Hexagon.ToPrism();
        public static QuadGrid HeptagonalPrism = Polygons.Heptagon.ToPrism();
        public static QuadGrid OctagonalPrism = Polygons.Octagon.ToPrism();
        public static QuadGrid NonagonalPrism = Polygons.Nonagon.ToPrism();
        public static QuadGrid DecagonalPrism = Polygons.Decagon.ToPrism();
        public static QuadGrid IcosagonalPrism = Polygons.Icosagon.ToPrism();
        public static QuadGrid PentagramalPrism = Polygons.Pentagram.ToPrism();
        public static QuadGrid OctagramalPrism = Polygons.Octagram.ToPrism();
        public static QuadGrid DecagramalPrism = Polygons.Decagram.ToPrism();

        public static QuadGrid ToPrism(this IPolyLine2D polyLine, float extrusion = 1.0f)
            => polyLine.Extrude(extrusion);
    }
}