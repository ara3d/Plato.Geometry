namespace Plato.DoublePrecision
{
    public partial struct Quad2D
    {
        public static readonly Quad2D Unit
            = ((-0.5, -0.5), (0.5, -0.5), 
                (0.5, 0.5), (-0.5, 0.5));

        public Quad3D To3D
            => (A.To3D, B.To3D, C.To3D, D.To3D);
    }
}