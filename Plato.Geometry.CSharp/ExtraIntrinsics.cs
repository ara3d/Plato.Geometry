using Plato.DoublePrecision;

namespace Plato.Geometry
{
    public static class ExtraIntrinsics
    {
        public static T Multiply<T>(this Number scalar, T self) where T : IScalarArithmetic<T>
        {
            return self.Multiply(scalar);
        }
    }
}