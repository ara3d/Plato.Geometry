namespace Plato
{
    public readonly partial struct Function0<TR>
    {
        public readonly Func<TR> _function;
        public Function0(Func<TR> f) => _function = f;
        public static implicit operator Function0<TR>(Func<TR> f) => new Function0<TR>(f);
    }

    public readonly partial struct Function1<T0, TR>
    {
        public readonly Func<T0, TR> _function;
        public Function1(Func<T0, TR> f) => _function = f;
        public static implicit operator Function1<T0, TR>(Func<T0, TR> f) => new Function1<T0, TR>(f);
    }

    public readonly partial struct Function2<T0, T1, TR>
    {
        public readonly Func<T0, T1, TR> _function;
        public Function2(Func<T0, T1, TR> f) => _function = f;
        public static implicit operator Function2<T0, T1, TR>(Func<T0, T1, TR> f) => new Function2<T0, T1, TR>(f);
    }

    public readonly partial struct Function3<T0, T1, T2, TR>
    {
        public readonly Func<T0, T1, T2, TR> _function;
        public Function3(Func<T0, T1, T2, TR> f) => _function = f;
        public static implicit operator Function3<T0, T1, T2, TR>(Func<T0, T1, T2, TR> f) => new Function3<T0, T1, T2, TR>(f);
    }

    public readonly partial struct Function4<T0, T1, T2, T3, TR>
    {
        public readonly Func<T0, T1, T2, T3, TR> _function;
        public Function4(Func<T0, T1, T2, T3, TR> f) => _function = f;
        public static implicit operator Function4<T0, T1, T2, T3, TR>(Func<T0, T1, T2, T3, TR> f) => new Function4<T0, T1, T2, T3, TR>(f);
    }

}