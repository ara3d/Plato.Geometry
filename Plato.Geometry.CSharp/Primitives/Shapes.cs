namespace Plato.DoublePrecision
{
    public static class Shapes
    {
        public static readonly Quad3D QuadXY 
            = Quad2D.Unit.To3D;
        
        public static readonly Quad3D QuadXZ 
            = new Quad3D((0, 0, 0), (1, 0, 0), (1, 0, 1), (0, 0, 1));
        
        public static readonly Quad3D QuadYZ 
            = new Quad3D((0, 0, 0), (0, 1, 0), (0, 1, 1), (0, 0, 1));
    }
}