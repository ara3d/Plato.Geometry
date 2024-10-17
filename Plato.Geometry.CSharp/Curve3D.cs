using System;
using System.Linq;

namespace Plato.DoublePrecision
{
    public class Curve3D : ICurve3D, IDeformable3D<Curve3D>
    {
        public readonly Func<Number, Vector3D> _func;
        public Boolean _closed;

        public Curve3D(Func<Number, Vector3D> func, Boolean closed)
        {
            _func = func;
            _closed = closed;
        }

        public Vector3D Eval(Number amount) => _func(amount);
        public Boolean Closed => _closed;

        public static implicit operator Curve3D(Line3D line)
            => new Curve3D(t => line.LerpAlong(t), false);

        public static implicit operator Curve3D(Vector3D point)
            => new Curve3D(t => point, false);

        public static readonly Curve3D Default
            = new Curve3D(_ => Vector3D.Default, false);

        public static implicit operator Curve3D(PolyLine3D poly)
        {
            var lines = poly.ToLines().ToSystemArray();

            if (lines.Length == 0)
                return Default;

            if (lines.Length == 1)
                return lines[0];

            var lengths = lines.Select(line => line.Length.Value).ToArray();
            var totalLength = lengths.Sum();

            return new Curve3D(t =>
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

        public Curve3D Deform(Func<Vector3D, Vector3D> f)
            => new Curve3D(t => f(Eval(t)), Closed);

        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);

        public Curve3D Transform(Matrix4x4 matrix)
            => Deform(matrix.TransformPoint);

        ITransformable3D ITransformable3D.Transform(Matrix4x4 matrix)
            => Transform(matrix);
    }
}