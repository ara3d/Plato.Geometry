namespace Ara3D.Mathematics
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Returns greatest common divisor
        /// https://stackoverflow.com/a/41766138
        /// https://en.wikipedia.org/wiki/Greatest_common_divisor
        /// </summary>
        public static long Gcd(this long a, long b)
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
        /// </summary>
        public static int Gcd(this int a, int b)
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
        /// Returns greatest common divisor
        /// https://stackoverflow.com/a/41766138
        /// https://en.wikipedia.org/wiki/Greatest_common_divisor
        /// https://en.wikipedia.org/wiki/Euclidean_algorithm
        /// </summary>
        public static uint Gcd(this uint a, uint b)
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
        public static bool RelativelyPrime(this long a, long b)
            => a.Gcd(b) == 1;

        /// <summary>
        /// Two integers are relatively prime if they share no common positive factors (divisors) except 1.
        /// https://mathworld.wolfram.com/RelativelyPrime.html
        /// </summary>
        public static bool RelativelyPrime(this int a, int b)
            => a.Gcd(b) == 1;

        /// <summary>
        /// Two integers are relatively prime if they share no common positive factors (divisors) except 1.
        /// https://mathworld.wolfram.com/RelativelyPrime.html
        /// </summary>
        public static bool RelativelyPrime(this ulong a, ulong b)
            => a.Gcd(b) == 1;

        /// <summary>
        /// Two integers are relatively prime if they share no common positive factors (divisors) except 1.
        /// https://mathworld.wolfram.com/RelativelyPrime.html
        /// </summary>
        public static bool RelativelyPrime(this uint a, uint b)
            => a.Gcd(b) == 1;
    }
}