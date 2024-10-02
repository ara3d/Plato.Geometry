using System.Runtime.InteropServices;
using Plato.DoublePrecision;

namespace Plato.Geometry.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Color32
    {
        public byte R, G, B, A;

        public Color32(byte r, byte g, byte b, byte a) => (R, G, B, A) = (r, g, b, a);
        
        public static implicit operator Color32(Color color) => new Color32(
            Extensions.ScaleToByte(color.R.Value),
            Extensions.ScaleToByte(color.G.Value),
            Extensions.ScaleToByte(color.B.Value),
            Extensions.ScaleToByte(color.A.Value));

        public Color ToColor()
            => new Color(R / 255.0, G / 255.0, B / 255.0, A / 255.0);

        public static implicit operator Color(Color32 color) 
            => color.ToColor();

        public static Color32 Default 
            = new Color32(0, 0, 0, 0);
    }
}