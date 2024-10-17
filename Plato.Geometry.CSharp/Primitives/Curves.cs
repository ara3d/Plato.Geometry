namespace Plato.DoublePrecision
{
    public static class Curves
    {
        public static Curve2D Circle 
            => new Curve2D(x => x.CircleFunction, true);
    }
}