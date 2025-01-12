
using System;
using System.Linq;

namespace Plato.SinglePrecision
{
    public static class ExtraIntrinsics
    {
        // NOTE: this addresses a problem with the Plato compiler not being able 
        // to add operators to the Number type. This is a workaround.
        public static T Multiply<T>(this Number scalar, T self) where T : IScalarArithmetic<T>
            => self.Multiply(scalar);

        public static Number MapComponents(this Number n, Func<Number, Number> f)
            => f(n);
    }
}