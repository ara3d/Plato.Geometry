namespace Plato.DoublePrecision
{
    public class PolyLine2D : IPolyLine2D
    {
        public IArray<Vector2D> Points { get; }
        public Boolean Closed { get; }

        public PolyLine2D(IArray<Vector2D> points, Boolean closed)
        {
            Points = points;
            Closed = closed;
        }

        public IArray<Line2D> ToLines()
        {
            var n = Closed ? Points.Count : Points.Count - 1;
            return n.MapRange(i => new Line2D(Points[i], Points.ModuloAt(i + 1)));
        }
    }
}