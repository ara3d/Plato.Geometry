namespace Plato.DoublePrecision
{
    public partial struct Line2D : IProcedural<Number, Vector2D>
    {
        public Line3D To3D
            => (A.To3D, B.To3D);

        public static readonly Line2D Unit
            = ((0, 0), (1, 1));

        public Vector2D Eval(Number amount)
            => A.Lerp(B, amount);
    }
}