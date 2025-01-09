using System.Numerics;

namespace Plato.Geometry
{
    public static unsafe class BufferExtensions
    {
        public static Buffer<T> Map<T>(this Buffer<T> self, Buffer<T> target, Func<T, T> f) where T : unmanaged
        {
            for (var i = 0; i < self.Count; i++)
            {
                target[i] = f(self[i]);
            }

            return target;
        }

        public static Buffer<T> Map<T>(this Buffer<T> self, Buffer<T> target, Func<T, int, T> f) where T : unmanaged
        {
            for (var i = 0; i < self.Count; i++)
            {
                target[i] = f(self[i], i);
            }

            return target;
        }

        public static Buffer<T> Filter<T>(this Buffer<T> self, Buffer<T> target, Func<T, bool> f) where T : unmanaged
        {
            var n = 0;
            for (var i = 0; i < self.Count; i++)
            {
                if (f(self[i]))
                {
                    target[n++] = self[i];
                }
            }
            return new Buffer<T>(target.Pointer, n);
        }

        public static Buffer<T> Filter<T>(this Buffer<T> self, Buffer<T> target, Func<T, int, bool> f) where T : unmanaged
        {
            var n = 0;
            for (var i = 0; i < self.Count; i++)
            {
                if (f(self[i], i))
                {
                    target[n++] = self[i];
                }
            }
            return new Buffer<T>(target.Pointer, n);
        }

        public static TAcc FusedReduce<T0, T1, TAcc>(
            this IArray<T0> self, 
            TAcc accumulator,
            Func<T0, int, bool> preFilter,
            Func<T0, int, T1> transformer, 
            Func<T1, int, bool> postFilter, 
            Func<TAcc, T1, int, int, (TAcc, bool)> aggregator)
        {
            var n = 0;
            for (var i=0; i < self.Count; i++)
            {
                if (preFilter(self[i], i))
                {
                    var t = transformer(self[i], i);
                    if (postFilter(t, i))
                    {
                        (accumulator, var @continue) = aggregator(accumulator, t, i, n++);
                        if (!@continue)
                            return accumulator;
                    }
                }
            }
            
            return accumulator;
        }

    }
}