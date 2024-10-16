using System;
using System.Linq;

namespace Plato.DoublePrecision
{
    public class Curve2D : ICurve2D
    {
        public Func<Number, Vector2D> _func;
        public Boolean _closed;

        public Curve2D(Func<Number, Vector2D> func, Boolean closed)
        {
            _func = func;
            _closed = closed;
        }

        public Vector2D Eval(Number amount) => _func(amount);
        public Boolean Closed => _closed;

        public static implicit operator Curve3D(Curve2D curve)
            => new Curve3D(t => curve.Eval(t), curve.Closed);

        public static implicit operator Curve2D(Line2D line)
            => new Curve2D(t => line.LerpAlong(t), false);

        public static implicit operator Curve2D(Vector2D point)
            => new Curve2D(t => point, false);

        public static Curve2D Default
            = new Curve2D(_ => Vector2D.Default, false);

        public static implicit operator Curve2D(PolyLine2D poly)
        {
            var lines = poly.ToLines().ToSystemArray();
            
            if (lines.Length == 0)
                return Default;
            
            if (lines.Length == 1)
                return lines[0];

            var lengths = lines.Select(line => line.Length.Value).ToArray();
            var totalLength = lengths.Sum();

            return new Curve2D(t =>
            {
                if (t <= 0) return lines[0].A;
                if (t >= 1) return lines[lines.Length - 1].B;

                var s = t * totalLength;
                
                var i = 0;
                while (s > lengths[i])
                {
                    s -= lengths[i];
                    i++;
                }
                
                // Technically shouldn't be possible, but just in case. 
                if (i >= lines.Length)
                    return lines[lines.Length - 1].B;

                return lines[i].LerpAlong(s / lengths[i]);
            }, 
                poly.Closed);
        }
    }
}