
using System;

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


        public static Curve3D Curve(IPolyLine3D polyLine) 
        {
            var lines = polyLine.Lines;
            if (lines.Count == 0)
                return Curve3D.Default;

            if (lines.Length == 1)
                return lines[0];

            var lengths = lines.Map(line => line.Length);
            var totalLength = lengths.Sum;

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

                    return lines[i].Eval(s / lengths[i]);
                },
                polyLine.Closed);    }
}