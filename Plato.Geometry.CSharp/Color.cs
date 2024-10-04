using Plato.Geometry.Graphics;

namespace Plato.DoublePrecision
{
    public readonly partial struct Color
    {
        public Color Lerp(Color c, double t) 
            => (R.Lerp(c.R, t), G.Lerp(c.G, t), B.Lerp(c.B, t), A.Lerp(c.A, t));

        public Color Tint(double t)
            => Lerp(Colors.White, t);

        public Color Shade(double t)
            => Lerp(Colors.Black, t);
    }
}