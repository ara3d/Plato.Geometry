using System.Windows;
using Plato.DoublePrecision;

namespace Plato.Geometry.Adapters
{
    public static class WindowsConverters
    {
        public static Point ToWindows(this Vector2D v) => new Point(v.X, v.Y);
        public static Point ToWindows(this Point2D p) => new Point(p.X, p.Y);
        public static Rect ToWindows(this Rect2D r) => new Rect(r.Left, r.Top, r.Width, r.Height);
        public static Size ToWindows(this Size2D s) => new Size(s.Width, s.Height);

        public static Point2D ToPlato(this Point p) => (p.X, p.Y);
        public static Vector2D ToPlato(this Vector v) => (v.X, v.Y);
        public static Rect2D ToPlato(this Rect r) => ((r.Left, r.Top), (r.Width, r.Height));
        public static Size2D ToPlato(this Size s) => (s.Width, s.Height);
    }
}