namespace Plato.DoublePrecision
{
    public static class IntegerExtensions
    {

        /// <summary>
        /// Returns greatest common divisor
        /// https://stackoverflow.com/a/41766138
        /// https://en.wikipedia.org/wiki/Greatest_common_divisor
        /// </summary>
        public static int Gcd(this Integer a, Integer b)
        {
            if (a < 0) a = -a;
            if (b < 0) b = -b;
            while (a != 0 && b != 0)
            {
                if (a > b) a %= b;
                else b %= a;
            }

            return a | b;
        }

        /// <summary>
        /// Returns greatest common divisor
        /// https://stackoverflow.com/a/41766138
        /// https://en.wikipedia.org/wiki/Greatest_common_divisor
        /// https://en.wikipedia.org/wiki/Euclidean_algorithm
        /// </summary>
        public static ulong Gcd(this ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b) a %= b;
                else b %= a;
            }
            return a | b;
        }

        /// <summary>
        /// Two integers are relatively prime if they share no common positive factors (divisors) except 1.
        /// https://mathworld.wolfram.com/RelativelyPrime.html
        /// </summary>
        public static bool RelativelyPrime(this Integer a, Integer b)
            => a.Gcd(b) == 1;
    }
}