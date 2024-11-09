using System.Windows;
using Plato.DoublePrecision;

namespace Plato.Geometry.Adapters
{
    public static class WindowsConverters
    {
        public static Point ToWindows(this Vector2D v) => new Point(v.X, v.Y);
        public static Rect ToWindows(this Rect2D r) => new Rect(r.Left, r.Top, r.Width, r.Height);

        public static Vector2D ToPlato(this Vector v) => (v.X, v.Y);
        public static Rect2D ToPlato(this Rect r) => ((r.Left, r.Top), (r.Width, r.Height));
    }
}