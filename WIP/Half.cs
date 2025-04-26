using System.Runtime.InteropServices;

namespace Plato.Geometry.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Half
    {
        public ushort Value;
        public Half(float f) => Value = f.ToHalf();
        public static implicit operator Half(float f) => new Half(f);
        public static implicit operator float(Half h) => h.Value;
        public float ToFloat() => Value;
    }
}