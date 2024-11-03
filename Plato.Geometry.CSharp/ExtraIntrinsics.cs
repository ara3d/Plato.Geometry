
using System;
using System.Linq;

namespace Plato.DoublePrecision
{
    public static class ExtraIntrinsics
    {
        // NOTE: this addresses a problem with the Plato compiler not being able 
        // to add operators to the Number type. This is a workaround.
        public static T Multiply<T>(this Number scalar, T self) where T : IScalarArithmetic<T>
            => self.Multiply(scalar);

        public static Number MapComponents(this Number n, Func<Number, Number> f)
            => f(n);


        public static Curve3D Curve(PolyLine3D polyLine)
        {
            if (polyLine.Points.Count == 0)
                return Curve3D.Default;

            if (polyLine.Points.Count == 1)
                return polyLine.Points[0];

            var lines = polyLine.Lines;
            var lengths = lines.Map(line => line.Length).ToSystemArray();
            var totalLength = lengths.Sum(l => l.Value);

            return new Curve3D(t =>
                {
                    if (t <= 0) return lines[0].A;
                    if (t >= 1) return lines[lines.Count - 1].B;

                    var s = t * totalLength;

                    var i = 0;
                    while (s > lengths[i])
                    {
                        s -= lengths[i];
                        i++;
                    }

                    // Technically shouldn't be possible, but just in case. 
                    if (i >= lines.Count)
                        return lines[lines.Count - 1].B;

                    return lines[i].Eval(s / lengths[i]);
                },
                polyLine.Closed);
        }
    }
}