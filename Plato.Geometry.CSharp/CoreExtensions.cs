using System;

namespace Plato.DoublePrecision
{
    public static class CoreExtensions
    {

        public static Number MapComponents(this Number n, Func<Number, Number> f) => f(n);

        /*
        public static IArray<T> Map<T>(this int n, Func<Integer, T> f) => ((Integer)n).Map(f);
        public static bool IsEmpty<T>(this IArray<T> xs) => xs.Count == 0;
        public static T First<T>(this IArray<T> xs) => xs[0];
        public static T Last<T>(this IArray<T> xs) => xs[xs.Count - 1];
        public static T Middle<T>(this IArray<T> xs) => xs[xs.Count / 2];
        public static IArray<T> Slice<T>(this IArray<T> xs, Integer from, Integer to) => xs.Subarray(from, to - from);
        public static IArray<T> Subarray<T>(this IArray<T> xs, Integer from, Integer count) => count.Map(i => xs[i + from]);
        public static IArray<T> Skip<T>(this IArray<T> xs, Integer n) => xs.Subarray(n, xs.Count - n);
        public static IArray<T> Take<T>(this IArray<T> xs, Integer n) => xs.Subarray(0, n);
        public static IArray<T> Drop<T>(this IArray<T> xs, Integer n) => xs.Take(xs.Count - n);
        public static IArray<T> SkipDrop<T>(this Integer xs, Integer n1, Integer n2) => xs.Skip(n1).Drop(n2);
        public static IArray<T> Rest<T>(this IArray<T> xs) => xs.Skip(1);
        public static IArray<T2> Map<T1, T2>(this IArray<T1> xs, Func<T1, T2> f) => xs.Count.Map(i => f(xs[i]));
        public static T2 Reduce<T1, T2>(this IArray<T1> xs, T2 acc, Func<T1, T2, T2> f)
        {
            for (var i = 0; i < xs.Count; ++i)
                acc = f(xs[i], acc);
            return acc;
        }

        public static bool All<T>(this IArray<T> self, Func<T, Boolean> p)
        {
            for (var i = 0; i < self.Count; ++i)
                if (!p(self[i]))
                    return false;
            return true;
        }

        public static bool All(this Array<Boolean> self)
            => self.All(x => x);

        public static bool Any<T>(this Array<T> self, Func<T, Boolean> p)
        {
            for (var i = 0; i < self.Count; ++i)
                if (p(self[i]))
                    return true;
            return false;
        }

        public static bool Any(this Array<Boolean> self)
            => self.Any(x => x);

        public static Array<T2> PairwiseMap<T1, T2>(this Array<T1> xs, Func<T1, T1, T2> f)
            => (xs.Count - 1).Map(i => f(xs[i], xs[i + 1]));

        public static Array<T2> PairwiseMapModulo<T1, T2>(this Array<T1> xs, Func<T1, T1, T2> f)
            => xs.Count.Map(i => f(xs[i], xs[(i + 1) % xs.Count]));

        public static Array<T3> Zip<T1, T2, T3>(this Array<T1> xs, Array<T2> ys, Func<T1, T2, T3> f)
            => (xs.Count.Lesser(ys.Count)).Map((i) => f(xs[i], ys[i]));

        public static Array<T4> Zip<T1, T2, T3, T4>(this Array<T1> xs, Array<T2> ys, Array<T3> zs, Func<T1, T2, T3, T4> f)
            => (xs.Count.Lesser(ys.Count).Lesser(zs.Count)).Map((i) => f(xs[i], ys[i], zs[i]));
        */
    }
}
