using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    public static partial class Intrinsics
    {
        [MethodImpl(AggressiveInlining)] public static TR Invoke<TR>(this Function0<TR> self) => self._function();
        [MethodImpl(AggressiveInlining)] public static TR Invoke<T0, TR>(this Function1<T0, TR> self, T0 arg) => self._function(arg);
        [MethodImpl(AggressiveInlining)] public static TR Invoke<T0, T1, TR>(this Function2<T0, T1, TR> self, T0 arg0, T1 arg1) => self._function(arg0, arg1);
        [MethodImpl(AggressiveInlining)] public static TR Invoke<T0, T1, T2, TR>(this Function3<T0, T1, T2, TR> self, T0 arg0, T1 arg1, T2 arg2) => self._function(arg0, arg1, arg2);
        [MethodImpl(AggressiveInlining)] public static TR Invoke<T0, T1, T2, T3, TR>(this Function4<T0, T1, T2, T3, TR> self, T0 arg0, T1 arg1, T2 arg2, T3 arg3) => self._function(arg0, arg1, arg2, arg3);
        
        [MethodImpl(AggressiveInlining)] public static Array<T> MapRange<T>(this Integer x, Func<Integer, T> f) => new Array<T>(x, f);
        [MethodImpl(AggressiveInlining)] public static Array<T> MakeArray<T>(params T[] args) => new Array<T>(args.Length, i => args[i]);
        [MethodImpl(AggressiveInlining)] public static Array2D<T> MakeArray2D<T>(this Integer columns, Integer rows, Func<Integer, Integer, T> f) => new Array2D<T>(columns, rows, f);

        [MethodImpl(AggressiveInlining)] public static Integer CombineHashCodes() => 17;
        [MethodImpl(AggressiveInlining)] public static Integer CombineHashCodes<T0>(T0 x0) => HashCode.Combine(x0);
        [MethodImpl(AggressiveInlining)] public static Integer CombineHashCodes<T0, T1>(T0 x0, T1 x1) => HashCode.Combine(x0, x1);
        [MethodImpl(AggressiveInlining)] public static Integer CombineHashCodes<T0, T1, T2>(T0 x0, T1 x1, T2 x2) => HashCode.Combine(x0, x1, x2);
        [MethodImpl(AggressiveInlining)] public static Integer CombineHashCodes<T0, T1, T2, T3>(T0 x0, T1 x1, T2 x2, T3 x3) => HashCode.Combine(x0, x1, x2, x3);
        [MethodImpl(AggressiveInlining)] public static Integer CombineHashCodes<T0, T1, T2, T3, T4>(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4) => HashCode.Combine(x0, x1, x2, x3, x4);
        [MethodImpl(AggressiveInlining)] public static Integer CombineHashCodes<T0, T1, T2, T3, T4, T5>(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5) => HashCode.Combine(x0, x1, x2, x3, x4, x5);
        [MethodImpl(AggressiveInlining)] public static Integer CombineHashCodes<T0, T1, T2, T3, T4, T5, T6>(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6) => HashCode.Combine(x0, x1, x2, x3, x4, x5, x6);
        [MethodImpl(AggressiveInlining)] public static Integer CombineHashCodes<T0, T1, T2, T3, T4, T5, T6, T7>(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7) => HashCode.Combine(x0, x1, x2, x3, x4, x5, x6, x7);

        [MethodImpl(AggressiveInlining)] public static (T0, T1) Tuple2<T0, T1>(this T0 item0, T1 item1) => (item0, item1);
        [MethodImpl(AggressiveInlining)] public static (T0, T1, T2) Tuple3<T0, T1, T2>(this T0 item0, T1 item1, T2 item2) => (item0, item1, item2);
        [MethodImpl(AggressiveInlining)] public static (T0, T1, T2, T3) Tuple4<T0, T1, T2, T3>(this T0 item0, T1 item1, T2 item2, T3 item3) => (item0, item1, item2, item3);
    }
}
