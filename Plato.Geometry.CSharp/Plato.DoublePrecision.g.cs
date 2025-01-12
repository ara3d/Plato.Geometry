using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace Plato.SinglePrecision
{
        public static partial class Intrinsics
    {
        public static TR Invoke<TR>(this Function0<TR> self) => self._function();
        public static TR Invoke<T0, TR>(this Function1<T0, TR> self, T0 arg) => self._function(arg);
        public static TR Invoke<T0, T1, TR>(this Function2<T0, T1, TR> self, T0 arg0, T1 arg1) => self._function(arg0, arg1);
        public static TR Invoke<T0, T1, T2, TR>(this Function3<T0, T1, T2, TR> self, T0 arg0, T1 arg1, T2 arg2) => self._function(arg0, arg1, arg2);
        public static TR Invoke<T0, T1, T2, T3, TR>(this Function4<T0, T1, T2, T3, TR> self, T0 arg0, T1 arg1, T2 arg2, T3 arg3) => self._function(arg0, arg1, arg2, arg3);

        public static T ChangePrecision<T>(this T self) => self;
        public static float ChangePrecision(this double self) => (float)self;
        public static string ChangePrecision(this string self) => self;

        public static Number MinNumber => double.MinValue;
        public static Number MaxNumber => double.MaxValue;

        public static Number Cos(Angle x) => (double)System.Math.Cos(x.Radians);
        public static Number Sin(Angle x) => (double)System.Math.Sin(x.Radians);
        public static Number Tan(Angle x) => (double)System.Math.Tan(x.Radians);

        public static Number Ln(Number x) => (double)System.Math.Log(x.Value);
        public static Number Exp(Number x) => (double)System.Math.Exp(x.Value);

        public static Number Floor(Number x) => (double)System.Math.Floor(x.Value);
        public static Number Ceiling(Number x) => (double)System.Math.Ceiling(x.Value);
        public static Number Round(Number x) => (double)System.Math.Round(x.Value);
        public static Number Truncate(Number x) => (double)System.Math.Truncate(x.Value);

        public static Angle Acos(Number x) => new Angle((double)System.Math.Acos(x));
        public static Angle Asin(Number x) => new Angle((double)System.Math.Asin(x));
        public static Angle Atan(Number x) => new Angle((double)System.Math.Atan(x));
        public static Angle Atan2(Number y, Number x) => new Angle((double)System.Math.Atan2(y, x));

        public static Number Pow(Number x, Number y) => (double)System.Math.Pow(x, y);
        public static Number Log(Number x, Number y) => (double)System.Math.Log(x, y);
        public static Number NaturalLog(Number x) => (double)System.Math.Log(x);
        public static Number NaturalPower(Number x) => (double)System.Math.Pow(x, System.Math.E);

        public static Number Add(Number x, Number y) => x.Value + y.Value;
        public static Number Subtract(Number x, Number y) => x.Value - y.Value;
        public static Number Divide(Number x, Number y) => x.Value / y.Value;
        public static Number Multiply(Number x, Number y) => x.Value * y.Value;
        public static Number Modulo(Number x, Number y) => x.Value % y.Value;
        public static Number Negative(Number x) => -x.Value;

        public static Integer Add(Integer x, Integer y) => x.Value + y.Value;
        public static Integer Subtract(Integer x, Integer y) => x.Value - y.Value;
        public static Integer Divide(Integer x, Integer y) => x.Value / y.Value;
        public static Integer Multiply(Integer x, Integer y) => x.Value * y.Value;
        public static Integer Modulo(Integer x, Integer y) => x.Value % y.Value;
        public static Integer Negative(Integer x) => -x.Value;

        // These are the two ways to make an array. 
        public static IArray<T> MapRange<T>(this Integer x, System.Func<Integer, T> f) => new Array<T>(x, f);
        public static IArray<T> MakeArray<T>(params T[] args) => new PrimitiveArray<T>(args);
        public static IArray2D<T> MakeArray2D<T>(this Integer columns, Integer rows, System.Func<Integer, Integer, T> f) => new Array2D<T>(columns, rows, f);

        // This is a built-in implementation
        public static IArray<T1> FlatMap<T0, T1>(IArray<T0> xs, System.Func<T0, IArray<T1>> f) 
        {
            var r = new System.Collections.Generic.List<T1>();
            for (var i=0; i < xs.Count; ++i)
                r.AddRange(f(xs[i]));            
            return new ListArray<T1>(r);
        }

        public static Boolean And(Boolean x, Boolean y) => x.Value && y.Value;
        public static Boolean Or(Boolean x, Boolean y) => x.Value || y.Value;
        public static Boolean Not(Boolean x) => !x.Value;

        public static Number Number(Integer x) => x.Value;
        public static Number Number(Character x) => x.Value;
        public static Integer Integer(Character x) => x.Value;

        public static Character At(String x, Integer n) => x.Value[n];
        public static Integer Count(String x) => x.Value.Length;

        public static int CombineHashCodes(params object[] objects)
        {
            if (objects.Length == 0) return 0;
            var r = objects[0].GetHashCode();
            for (var i = 1; i < objects.Length; ++i)
                r = CombineHashCodes(r, objects[i].GetHashCode());
            return r;
        }

        public static (T0, T1) Tuple2<T0, T1>(this T0 item0, T1 item1) => (item0, item1);
        public static (T0, T1, T2) Tuple3<T0, T1, T2>(this T0 item0, T1 item1, T2 item2) => (item0, item1, item2);
        public static (T0, T1, T2, T3) Tuple4<T0, T1, T2, T3>(this T0 item0, T1 item1, T2 item2, T3 item3) => (item0, item1, item2, item3);

        public static int CombineHashCodes(int h1, int h2)
        {
            unchecked
            {
                var rol5 = ((uint)h1 << 5) | ((uint)h1 >> 27);
                return ((int)rol5 + h1) ^ h2;
            }
        }     
        
        public static Dynamic New(Type type, IArray<IAny> args) 
            => Dynamic.New(System.Activator.CreateInstance(type.Value, args.ToSystemArray()));

        public static T[] ToSystemArray<T>(this IArray<T> self) 
        {
           var r = new T[self.Count];
           for (var i=0; i< self.Count; i++)
			   r[i] = self.At(i);
           return r;
        }

        public static Array<T> ToPrimitiveArray<T>(this IArray<T> self)
            => self is Array<T> a ? a : new Array<T>(self.Count, self.At);

        public static Boolean Equals(this Number a, Number b) => a.Value.Equals(b.Value);
        public static Boolean Equals(this Character a, Character b) => a.Value.Equals(b.Value);
        public static Boolean Equals(this Integer a, Integer b) => a.Value.Equals(b.Value);
        public static Boolean Equals(this Boolean a, Boolean b) => a.Value.Equals(b.Value);
        public static Boolean Equals(this String a, String b) => a.Value.Equals(b.Value);

        public static Boolean LessThanOrEquals(this Number a, Number b) => a.Value <= b.Value;
        public static Boolean LessThanOrEquals(this Character a, Character b) => a.Value <= b.Value;
        public static Boolean LessThanOrEquals(this Integer a, Integer b) => a.Value <= b.Value;
        public static Boolean LessThanOrEquals(this Boolean a, Boolean b) => !a || (a && b);
        public static Boolean LessThanOrEquals(this String a, String b) => a.Value.CompareTo(b.Value) <= 0;

        public static String ToString(this Number x) => x.Value.ToString();
        public static String ToString(this Boolean x) => x ? "true" : "false";
        public static String ToString(this Character x) => x.Value.ToString();
        public static String ToString(this Integer x) => x.Value.ToString();
        public static String ToString(this String x) => $"\"{x.Value}\"";
        public static String ToString(this Type x) => $"\"{x.Value}\"";

        public static Vector2D Eval(this ICurve2D self, Number x) => throw new System.NotImplementedException();
        public static Vector3D Eval(this ICurve3D self, Number x) => throw new System.NotImplementedException();
        public static Vector3D Eval(this ISurface self, Vector2D uv) => throw new System.NotImplementedException();

        public static Number Distance(this ICurve2D self, Vector2D uv) => throw new System.NotImplementedException();
        public static Number Distance(this ICurve3D self, Vector3D p) => throw new System.NotImplementedException();
        public static Number Distance(this ISurface self, Vector3D p) => throw new System.NotImplementedException();
    
        public static Bounds3D Deform(this Bounds3D self, System.Func<Vector3D, Vector3D> f) => throw new System.NotImplementedException();
    
        public static Boolean ClosedX(this ISurface self) => throw new System.NotImplementedException();
        public static Boolean ClosedY(this ISurface self) => throw new System.NotImplementedException();

        public static Vector2D Divide(this Number s, Vector2D v) => (s / v.X, s / v.Y);
        public static Vector3D Divide(this Number s, Vector3D v) => (s / v.X, s / v.Y, s / v.Z);
        public static Vector4D Divide(this Number s, Vector4D v) => (s / v.X, s / v.Y, s / v.Z, s / v.W);
    }
    
    public interface IArray<T> : System.Collections.Generic.IReadOnlyList<T>
    {
        Integer Count { get; }
        T At(Integer n);
        T this[Integer n] { get; }
    }

    public readonly partial struct Number
    {
    }

    public readonly struct Array<T> : IArray<T>
    {
        private readonly System.Func<Integer, T> _func;
        public Integer Count { get; }
        public T At(Integer n) => _func(VerifyIndex(n));
        public T this[Integer n] => _func(VerifyIndex(n));
#if DEBUG
        public int VerifyIndex(int x) => x < 0 || x >= Count ? throw new System.ArgumentOutOfRangeException($"Index was {x} limit is {Count}") : x;
#else
        public int VerifyIndex(int x) => x;
#endif

        public Array(Integer count, System.Func<Integer, T> func)
        {
            Count = count;
            _func = func;
        }
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (var i = 0; i < Count; i++)
				yield return this[i];
		}
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        T System.Collections.Generic.IReadOnlyList<T>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<T>.Count => this.Count;
    }

    public readonly struct Array2D<T> : IArray2D<T>
    {
        private readonly System.Func<Integer, Integer, T> _func;
        public Integer Count => NumColumns * NumRows;
        public Integer NumColumns { get; }
        public Integer NumRows { get; }
#if DEBUG
        public int VerifyColumn(int x) => x < 0 || x >= NumColumns ? throw new System.ArgumentOutOfRangeException($"Column was {x} limit is {NumColumns}") : x;
        public int VerifyRow(int x) => x < 0 || x >= NumRows ? throw new System.ArgumentOutOfRangeException($"Row was {x} limit is {NumRows}") : x;
#else
        public int VerifyColumn(int x) => x;
        public int VerifyRow(int x) => x;
#endif
        public T At(Integer n) => At(n % NumColumns, n / NumColumns);
        public T this[Integer n] => At(n % NumColumns, n / NumColumns);
        public T At(Integer col, Integer row) => _func(VerifyColumn(col), VerifyRow(row));
        public T this[Integer col, Integer row] => _func(VerifyColumn(col), VerifyRow(row));
        public Array2D(Integer numCols, Integer numRows, System.Func<Integer, Integer, T> func)
        {
            NumColumns = numCols;
            NumRows = numRows;
            _func = func;
        }
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (var i = 0; i < Count; i++)
				yield return this[i];
		}
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        T System.Collections.Generic.IReadOnlyList<T>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<T>.Count => this.Count;
    }

    public readonly struct PrimitiveArray<T> : IArray<T>
    {
        private readonly T[] _data;
        public Integer Count => _data.Length;
        public T At(Integer n) => _data[n];
        public T this[Integer n] => _data[n];
        public PrimitiveArray(T[] data) => _data = data;    
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (var i = 0; i < Count; i++)
				yield return this[i];
		}
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        T System.Collections.Generic.IReadOnlyList<T>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<T>.Count => this.Count;
    }

    public readonly struct ListArray<T> : IArray<T>
    {
        private readonly System.Collections.Generic.IReadOnlyList<T> _data;
        public Integer Count => _data.Count;
        public T At(Integer n) => _data[n];
        public T this[Integer n] => _data[n];
        public ListArray(System.Collections.Generic.IReadOnlyList<T> data) => _data = data;
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (var i = 0; i < Count; i++)
				yield return this[i];
		}
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        T System.Collections.Generic.IReadOnlyList<T>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<T>.Count => this.Count;
    }

    public readonly partial struct String
    {
    }

    public readonly partial struct Boolean
    {
        public static bool operator true(Boolean b) => b.Value;
        public static bool operator false(Boolean b) => !b.Value;
    }
    
    public readonly partial struct Integer
    {
    }

    public readonly partial struct Character
    {
    }

    public readonly partial struct Dynamic
    {
        public readonly object Value;
        public Dynamic WithValue(object value) => new Dynamic(value);
        public Dynamic(object value) => (Value) = (value);
        public static Dynamic Default = new Dynamic();
        public static Dynamic New(object value) => new Dynamic(value);
        public String TypeName => "Dynamic";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>("Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        public T As<T>() => (T)Value;
    }

    public readonly partial struct Function0<TR>
    {
        public readonly System.Func<TR> _function;
        public Function0(System.Func<TR> f) => _function = f;
        public static implicit operator Function0<TR>(System.Func<TR> f) => new Function0<TR>(f);
    }

    public readonly partial struct Function1<T0, TR>
    {
        public readonly System.Func<T0, TR> _function;
        public Function1(System.Func<T0, TR> f) => _function = f;
        public static implicit operator Function1<T0, TR>(System.Func<T0, TR> f) => new Function1<T0, TR>(f);
    }

    public readonly partial struct Function2<T0, T1, TR>
    {
        public readonly System.Func<T0, T1, TR> _function;
        public Function2(System.Func<T0, T1, TR> f) => _function = f;
        public static implicit operator Function2<T0, T1, TR>(System.Func<T0, T1, TR> f) => new Function2<T0, T1, TR>(f);
    }

    public readonly partial struct Function3<T0, T1, T2, TR>
    {
        public readonly System.Func<T0, T1, T2, TR> _function;
        public Function3(System.Func<T0, T1, T2, TR> f) => _function = f;
        public static implicit operator Function3<T0, T1, T2, TR>(System.Func<T0, T1, T2, TR> f) => new Function3<T0, T1, T2, TR>(f);
    }
    
    public readonly partial struct Function4<T0, T1, T2, T3, TR>
    {
        public readonly System.Func<T0, T1, T2, T3, TR> _function;
        public Function4(System.Func<T0, T1, T2, T3, TR> f) => _function = f;
        public static implicit operator Function4<T0, T1, T2, T3, TR>(System.Func<T0, T1, T2, T3, TR> f) => new Function4<T0, T1, T2, T3, TR>(f);
    }

    public interface IArray2D<T>: IArray<T>
    {
        Integer NumRows { get; }
        Integer NumColumns { get; }
        T At(Integer column, Integer row);
        T this[Integer column, Integer row] { get; }
    }
    public interface IArray3D<T>: IArray<T>
    {
        Integer NumRows { get; }
        Integer NumColumns { get; }
        Integer NumLayers { get; }
        T At(Integer column, Integer row, Integer layer);
        T this[Integer column, Integer row, Integer layer] { get; }
    }
    public interface IAny
    {
        IArray<String> FieldNames { get; }
        IArray<Dynamic> FieldValues { get; }
        String TypeName { get; }
    }
    public interface IValue<Self>: IAny, IEquatable<Self>, IValue
    {
    }
    public interface IValue: IAny, IEquatable
    {
    }
    public interface INumerical<Self>: IValue<Self>, IScalarArithmetic<Self>, IAdditive<Self>, INumerical
    {
        IArray<Number> Components { get; }
        Self FromComponents(IArray<Number> xs);
    }
    public interface INumerical: IValue, IScalarArithmetic, IAdditive
    {
        IArray<Number> Components { get; }
        INumerical FromComponents(IArray<Number> xs);
    }
    public interface INumberLike<Self>: INumerical<Self>, IOrderable<Self>, INumberLike
    {
        Number ToNumber { get; }
        Self FromNumber(Number n);
    }
    public interface INumberLike: INumerical, IOrderable
    {
        Number ToNumber { get; }
        INumberLike FromNumber(Number n);
    }
    public interface IReal<Self>: INumberLike<Self>, IAlgebraic<Self>, IArithmetic<Self>, IReal
    {
    }
    public interface IReal: INumberLike, IAlgebraic, IArithmetic
    {
    }
    public interface IWholeNumber<Self>: IValue<Self>, IOrderable<Self>, IArithmetic<Self>, IWholeNumber
    {
    }
    public interface IWholeNumber: IValue, IOrderable, IArithmetic
    {
    }
    public interface IMeasure<Self>: INumberLike<Self>, IAdditive<Self>, IInterpolatable<Self>, IMeasure
    {
    }
    public interface IMeasure: INumberLike, IAdditive, IInterpolatable
    {
    }
    public interface IVector<Self>: INumerical<Self>, IArithmetic<Self>, IArray<Number>, IInterpolatable<Self>, IAlgebraic<Self>, IVector
    {
    }
    public interface IVector: INumerical, IArithmetic, IArray<Number>, IInterpolatable, IAlgebraic
    {
    }
    public interface ICoordinate<Self>: IValue<Self>, ICoordinate
    {
    }
    public interface ICoordinate: IValue
    {
    }
    public interface IOrderable<Self>: IEquatable<Self>, IOrderable
    {
        Boolean LessThanOrEquals(Self y);
    }
    public interface IOrderable: IEquatable
    {
        Boolean LessThanOrEquals(IOrderable y);
    }
    public interface IEquatable<Self>: IAny, IEquatable
    {
        Boolean Equals(Self b);
    }
    public interface IEquatable: IAny
    {
        Boolean Equals(IEquatable b);
    }
    public interface IAdditive<Self>: IAny, IAdditive
    {
        Self Add(Self b);
        Self Subtract(Self b);
        Self Negative { get; }
    }
    public interface IAdditive: IAny
    {
        IAdditive Add(IAdditive b);
        IAdditive Subtract(IAdditive b);
        IAdditive Negative { get; }
    }
    public interface IScalarArithmetic<Self>: IAny, IScalarArithmetic
    {
        Self Modulo(Number other);
        Self Divide(Number other);
        Self Multiply(Number other);
    }
    public interface IScalarArithmetic: IAny
    {
        IScalarArithmetic Modulo(Number other);
        IScalarArithmetic Divide(Number other);
        IScalarArithmetic Multiply(Number other);
    }
    public interface IMultiplicative<Self>: IAny, IMultiplicative
    {
        Self Multiply(Self b);
    }
    public interface IMultiplicative: IAny
    {
        IMultiplicative Multiply(IMultiplicative b);
    }
    public interface IInvertible<Self>: IAny, IInvertible
    {
        Self Inverse { get; }
    }
    public interface IInvertible: IAny
    {
        IInvertible Inverse { get; }
    }
    public interface IMultiplicativeWithInverse<Self>: IMultiplicative<Self>, IInvertible<Self>, IMultiplicativeWithInverse
    {
    }
    public interface IMultiplicativeWithInverse: IMultiplicative, IInvertible
    {
    }
    public interface IAlgebraic<Self>: IInterpolatable<Self>, IMultiplicative<Self>, IAlgebraic
    {
    }
    public interface IAlgebraic: IInterpolatable, IMultiplicative
    {
    }
    public interface IInterpolatable<Self>: IAdditive<Self>, IScalarArithmetic<Self>, IInterpolatable
    {
    }
    public interface IInterpolatable: IAdditive, IScalarArithmetic
    {
    }
    public interface IDivisible<Self>: IAny, IDivisible
    {
        Self Divide(Self b);
    }
    public interface IDivisible: IAny
    {
        IDivisible Divide(IDivisible b);
    }
    public interface IModulo<Self>: IAny, IModulo
    {
        Self Modulo(Self b);
    }
    public interface IModulo: IAny
    {
        IModulo Modulo(IModulo b);
    }
    public interface IArithmetic<Self>: IAdditive<Self>, IMultiplicative<Self>, IDivisible<Self>, IModulo<Self>, IArithmetic
    {
    }
    public interface IArithmetic: IAdditive, IMultiplicative, IDivisible, IModulo
    {
    }
    public interface IBoolean<Self>: IAny, IBoolean
    {
        Self And(Self b);
        Self Or(Self b);
        Self Not { get; }
    }
    public interface IBoolean: IAny
    {
        IBoolean And(IBoolean b);
        IBoolean Or(IBoolean b);
        IBoolean Not { get; }
    }
    public interface IInterval<Self, T>: IEquatable<Self>, IValue<Self>, IArray<T>, IInterval<T>
    {
        T Start { get; }
        T End { get; }
    }
    public interface IInterval<T>: IEquatable, IValue, IArray<T>
    {
        T Start { get; }
        T End { get; }
    }
    public interface IBounds<Self, T>: IEquatable<Self>, IValue<Self>, IBounds<T>
    {
        T Min { get; }
        T Max { get; }
    }
    public interface IBounds<T>: IEquatable, IValue
    {
        T Min { get; }
        T Max { get; }
    }
    public interface IRealFunction
    {
        Number Eval(Number x);
    }
    public interface IAngularCurve2D: ICurve2D
    {
        Vector2D GetPoint(Angle t);
    }
    public interface IPolarCurve: IAngularCurve2D
    {
        Number GetRadius(Angle t);
    }
    public interface IAngularCurve3D: ICurve3D
    {
        Vector3D GetPoint(Angle t);
    }
    public interface IBounded2D
    {
        Bounds2D Bounds { get; }
    }
    public interface IBounded3D
    {
        Bounds3D Bounds { get; }
    }
    public interface IDeformable2D<Self>: IDeformable2D
    {
        Self Deform(System.Func<Vector2D, Vector2D> f);
    }
    public interface IDeformable2D
    {
        IDeformable2D Deform(System.Func<Vector2D, Vector2D> f);
    }
    public interface IOpenClosedShape
    {
        Boolean Closed { get; }
    }
    public interface IDeformable3D<Self>: IDeformable3D
    {
        Self Deform(System.Func<Vector3D, Vector3D> f);
    }
    public interface IDeformable3D
    {
        IDeformable3D Deform(System.Func<Vector3D, Vector3D> f);
    }
    public interface IGeometry
    {
    }
    public interface IGeometry2D: IGeometry
    {
    }
    public interface IGeometry3D: IGeometry
    {
    }
    public interface IShape2D: IGeometry2D
    {
    }
    public interface IShape3D: IGeometry3D
    {
    }
    public interface IOpenShape: IOpenClosedShape
    {
    }
    public interface IClosedShape: IOpenClosedShape
    {
    }
    public interface IOpenShape2D: IGeometry2D, IOpenShape
    {
    }
    public interface IClosedShape2D: IGeometry2D, IClosedShape
    {
    }
    public interface IOpenShape3D: IGeometry3D, IOpenShape
    {
    }
    public interface IClosedShape3D: IGeometry3D, IClosedShape
    {
    }
    public interface IProcedural<TDomain, TRange>
    {
        TRange Eval(TDomain t);
    }
    public interface ICurve<TRange>: IProcedural<Number, TRange>, IOpenClosedShape
    {
    }
    public interface IDistanceField2D
    {
        Number Distance(Vector2D p);
    }
    public interface IDistanceField3D
    {
        Number Distance(Vector3D p);
    }
    public interface ICurve1D: ICurve<Number>
    {
    }
    public interface ICurve2D: IGeometry2D, ICurve<Vector2D>, IDistanceField2D
    {
    }
    public interface IClosedCurve2D: ICurve2D, IClosedShape2D
    {
    }
    public interface IOpenCurve2D: ICurve2D, IOpenShape2D
    {
    }
    public interface ICurve3D: IGeometry3D, ICurve<Vector3D>, IDistanceField3D
    {
    }
    public interface IClosedCurve3D: ICurve3D, IClosedShape3D
    {
    }
    public interface IOpenCurve3D: ICurve3D, IOpenShape3D
    {
    }
    public interface ISurface: IGeometry3D, IDistanceField3D
    {
    }
    public interface IProceduralSurface: IProcedural<Vector2D, Vector3D>, ISurface
    {
        Boolean ClosedX { get; }
        Boolean ClosedY { get; }
    }
    public interface IExplicitSurface: IProcedural<Vector2D, Number>, ISurface
    {
    }
    public interface IImplicitProcedural<TDomain>: IProcedural<TDomain, Boolean>
    {
    }
    public interface IImplicitSurface: ISurface, IImplicitProcedural<Vector3D>
    {
    }
    public interface IImplicitCurve2D: IGeometry2D, IImplicitProcedural<Vector2D>
    {
    }
    public interface IImplicitVolume: IGeometry3D, IImplicitProcedural<Vector3D>
    {
    }
    public interface IPolyLine2D: IPointGeometry2D, IOpenClosedShape, ICurve2D
    {
    }
    public interface IPolyLine3D<Self>: IPointGeometry3D<Self>, IOpenClosedShape, ICurve3D, IPolyLine3D
    {
    }
    public interface IPolyLine3D: IPointGeometry3D, IOpenClosedShape, ICurve3D
    {
    }
    public interface IClosedPolyLine2D: IPolyLine2D, IClosedShape2D
    {
    }
    public interface IClosedPolyLine3D<Self>: IPolyLine3D<Self>, IClosedShape3D, IClosedPolyLine3D
    {
    }
    public interface IClosedPolyLine3D: IPolyLine3D, IClosedShape3D
    {
    }
    public interface IPolygon2D: IClosedPolyLine2D, IArray<Vector2D>
    {
    }
    public interface IPolygon3D<Self>: IClosedPolyLine3D<Self>, IArray<Vector3D>, IPolygon3D
    {
    }
    public interface IPolygon3D: IClosedPolyLine3D, IArray<Vector3D>
    {
    }
    public interface ISolid: IProceduralSurface
    {
    }
    public interface IPrimitiveGeometry
    {
        Integer PrimitiveSize { get; }
        Integer NumPrimitives { get; }
    }
    public interface IPointPrimitives: IPrimitiveGeometry
    {
    }
    public interface ILinePrimitives: IPrimitiveGeometry
    {
    }
    public interface ITrianglePrimitives: IPrimitiveGeometry
    {
    }
    public interface IQuadPrimitives: IPrimitiveGeometry
    {
    }
    public interface IPointGeometry2D: IGeometry2D
    {
        IArray<Vector2D> Points { get; }
    }
    public interface IPointGeometry3D<Self>: IGeometry3D, IDeformable3D<Self>, IPointGeometry3D
    {
        IArray<Vector3D> Points { get; }
    }
    public interface IPointGeometry3D: IGeometry3D, IDeformable3D
    {
        IArray<Vector3D> Points { get; }
    }
    public interface IPrimitiveGeometry2D: IPointGeometry2D, IPrimitiveGeometry
    {
    }
    public interface IPrimitiveGeometry3D<Self>: IPointGeometry3D<Self>, IPrimitiveGeometry, IPrimitiveGeometry3D
    {
    }
    public interface IPrimitiveGeometry3D: IPointGeometry3D, IPrimitiveGeometry
    {
    }
    public interface ILineGeometry2D: IPrimitiveGeometry2D, ILinePrimitives
    {
        IArray<Line2D> Lines { get; }
    }
    public interface ILineGeometry3D<Self>: IPrimitiveGeometry3D<Self>, ILinePrimitives, ILineGeometry3D
    {
        IArray<Line3D> Lines { get; }
    }
    public interface ILineGeometry3D: IPrimitiveGeometry3D, ILinePrimitives
    {
        IArray<Line3D> Lines { get; }
    }
    public interface ITriangleGeometry2D: IPrimitiveGeometry2D, ITrianglePrimitives
    {
        IArray<Triangle2D> Triangles { get; }
    }
    public interface ITriangleGeometry3D<Self>: IPrimitiveGeometry3D<Self>, ITrianglePrimitives, ITriangleGeometry3D
    {
        IArray<Triangle3D> Triangles { get; }
    }
    public interface ITriangleGeometry3D: IPrimitiveGeometry3D, ITrianglePrimitives
    {
        IArray<Triangle3D> Triangles { get; }
    }
    public interface IQuadGeometry2D: IPrimitiveGeometry2D, IQuadPrimitives
    {
        IArray<Quad2D> Quads { get; }
    }
    public interface IQuadGeometry3D<Self>: IPrimitiveGeometry3D<Self>, IQuadPrimitives, IQuadGeometry3D
    {
        IArray<Quad3D> Quads { get; }
    }
    public interface IQuadGeometry3D: IPrimitiveGeometry3D, IQuadPrimitives
    {
        IArray<Quad3D> Quads { get; }
    }
    public interface IIndexedGeometry: IPrimitiveGeometry
    {
        IArray<Integer> Indices { get; }
    }
    public interface IIndexedGeometry2D: IIndexedGeometry, IPrimitiveGeometry2D
    {
    }
    public interface IIndexedGeometry3D<Self>: IIndexedGeometry, IPrimitiveGeometry3D<Self>, IIndexedGeometry3D
    {
    }
    public interface IIndexedGeometry3D: IIndexedGeometry, IPrimitiveGeometry3D
    {
    }
    public interface ILineMesh2D: IIndexedGeometry2D, ILineGeometry2D
    {
    }
    public interface ILineMesh3D<Self>: IIndexedGeometry3D<Self>, ILineGeometry3D<Self>, ILineMesh3D
    {
    }
    public interface ILineMesh3D: IIndexedGeometry3D, ILineGeometry3D
    {
    }
    public interface ITriangleMesh2D: IIndexedGeometry2D, ITriangleGeometry2D
    {
    }
    public interface ITriangleMesh3D<Self>: IIndexedGeometry3D<Self>, ITriangleGeometry3D<Self>, ITriangleMesh3D
    {
    }
    public interface ITriangleMesh3D: IIndexedGeometry3D, ITriangleGeometry3D
    {
    }
    public interface IQuadMesh2D: IIndexedGeometry2D, IQuadGeometry2D
    {
    }
    public interface IQuadMesh3D<Self>: IIndexedGeometry3D<Self>, IQuadGeometry3D<Self>, IQuadMesh3D
    {
    }
    public interface IQuadMesh3D: IIndexedGeometry3D, IQuadGeometry3D
    {
    }
    public interface IPointArray2D: IPointGeometry2D
    {
    }
    public interface IPointArray3D<Self>: IPointGeometry3D<Self>, IPointArray3D
    {
    }
    public interface IPointArray3D: IPointGeometry3D
    {
    }
    public interface ILineArray2D: ILineMesh2D
    {
    }
    public interface ILineArray3D<Self>: ILineMesh3D<Self>, ILineArray3D
    {
    }
    public interface ILineArray3D: ILineMesh3D
    {
    }
    public interface ITriangleArray2D: ITriangleMesh2D
    {
    }
    public interface ITriangleArray3D<Self>: ITriangleMesh3D<Self>, ITriangleArray3D
    {
    }
    public interface ITriangleArray3D: ITriangleMesh3D
    {
    }
    public interface IQuadArray2D: IQuadMesh2D
    {
    }
    public interface IQuadArray3D<Self>: IQuadMesh3D<Self>, IQuadArray3D
    {
    }
    public interface IQuadArray3D: IQuadMesh3D
    {
    }
    public interface IQuadGrid3D<Self>: IQuadMesh3D<Self>, IQuadGrid3D
    {
        IArray2D<Vector3D> PointGrid { get; }
        Boolean ClosedX { get; }
        Boolean ClosedY { get; }
    }
    public interface IQuadGrid3D: IQuadMesh3D
    {
        IArray2D<Vector3D> PointGrid { get; }
        Boolean ClosedX { get; }
        Boolean ClosedY { get; }
    }
    public interface ITransform3D
    {
        Vector3D Transform(Vector3D v);
        Vector3D TransformNormal(Vector3D v);
        Matrix4x4 Matrix { get; }
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Unit: IReal<Unit>
    {
        [DataMember] public readonly Number Value;
        public Unit WithValue(Number value) => new Unit(value);
        public Unit(Number value) => (Value) = (value);
        public static Unit Default = new Unit();
        public static Unit New(Number value) => new Unit(value);
        public Plato.SinglePrecision.Unit ChangePrecision() => (Value.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Unit(Unit self) => self.ChangePrecision();
        public static implicit operator Number(Unit self) => self.Value;
        public static implicit operator Unit(Number value) => new Unit(value);
        public static implicit operator Unit(Integer value) => new Unit(value);
        public static implicit operator Unit(int value) => new Integer(value);
        public static implicit operator Unit(double value) => new Number(value);
        public static implicit operator double(Unit value) => value.Value;
        public override bool Equals(object obj) { if (!(obj is Unit)) return false; var other = (Unit)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => $"{{ \"Value\" = {Value} }}";
        public static implicit operator Dynamic(Unit self) => new Dynamic(self);
        public static implicit operator Unit(Dynamic value) => value.As<Unit>();
        public String TypeName => "Unit";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        Number INumberLike.ToNumber => this.ToNumber;
        INumberLike INumberLike.FromNumber(Number n) => this.FromNumber((Number)n);
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Unit)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Unit)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Unit)b);
        IAdditive IAdditive.Negative => this.Negative;
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Unit)y);
        IMultiplicative IMultiplicative.Multiply(IMultiplicative b) => this.Multiply((Unit)b);
        IDivisible IDivisible.Divide(IDivisible b) => this.Divide((Unit)b);
        IModulo IModulo.Modulo(IModulo b) => this.Modulo((Unit)b);
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(Value);
        public Unit FromComponents(IArray<Number> numbers) => new Unit(numbers[0]);
        // Implemented concept functions and type functions
        public Number Magnitude => this.Value;
        public Boolean GtZ => this.GreaterThan(this.Zero);
        public Boolean LtZ => this.LessThan(this.Zero);
        public Boolean GtEqZ => this.GreaterThanOrEquals(this.Zero);
        public Boolean LtEqZ => this.LessThanOrEquals(this.Zero);
        public Boolean IsPositive => this.GtEqZ;
        public Boolean IsNegative => this.LtZ;
        public Integer Sign => this.LtZ ? ((Integer)1).Negative : this.GtZ ? ((Integer)1) : ((Integer)0);
        public Unit Abs => this.LtZ ? this.Negative : this;
        public Unit Inverse => this.One.Divide(this);
        public Boolean Between(Unit min, Unit max) => this.GreaterThanOrEquals(min).And(this.LessThanOrEquals(max));
        public Unit Multiply(Unit y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Unit operator *(Unit x, Unit y) => x.Multiply(y);
        public Unit Divide(Unit y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Unit operator /(Unit x, Unit y) => x.Divide(y);
        public Unit Modulo(Unit y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public static Unit operator %(Unit x, Unit y) => x.Modulo(y);
        public Number Number => this.ToNumber;
        public Number ToNumber => this.Component(((Integer)0));
        public Unit FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Unit b) => this.ToNumber.LessThan(b.ToNumber) ? ((Integer)1).Negative : this.ToNumber.GreaterThan(b.ToNumber) ? ((Integer)1) : ((Integer)0);
        public Unit Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
        public static Unit operator +(Unit x, Number y) => x.Add(y);
        public Unit Subract(Number y) => this.FromNumber(this.ToNumber.Subtract(y));
        public Unit PlusOne => this.Add(this.One);
        public Unit MinusOne => this.Subtract(this.One);
        public Unit FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Unit MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Unit ZipComponents(Unit y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Unit Zero => this.MapComponents((i) => ((Number)0));
        public Unit One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Unit MinValue => this.MapComponents((x) => x.MinValue);
        public Unit MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Unit Clamp(Unit a, Unit b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Unit ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Unit Clamp01 => this.ClampZeroOne;
        public Unit Min(Unit y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Unit Max(Unit y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Unit Floor => this.MapComponents((c) => c.Floor);
        public Unit Fract => this.MapComponents((c) => c.Fract);
        public Unit Multiply(Number s){
            var _var366 = s;
            return this.MapComponents((i) => i.Multiply(_var366));
        }
        public static Unit operator *(Unit x, Number s) => x.Multiply(s);
        public Unit Divide(Number s){
            var _var367 = s;
            return this.MapComponents((i) => i.Divide(_var367));
        }
        public static Unit operator /(Unit x, Number s) => x.Divide(s);
        public Unit Modulo(Number s){
            var _var368 = s;
            return this.MapComponents((i) => i.Modulo(_var368));
        }
        public static Unit operator %(Unit x, Number s) => x.Modulo(s);
        public Unit Add(Unit y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Unit operator +(Unit x, Unit y) => x.Add(y);
        public Unit Subtract(Unit y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Unit operator -(Unit x, Unit y) => x.Subtract(y);
        public Unit Negative => this.MapComponents((a) => a.Negative);
        public static Unit operator -(Unit x) => x.Negative;
        public IArray<Unit> Repeat(Integer n){
            var _var369 = this;
            return n.MapRange((i) => _var369);
        }
        public Boolean Equals(Unit b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Unit a, Unit b) => a.Equals(b);
        public Boolean NotEquals(Unit b) => this.Equals(b).Not;
        public static Boolean operator !=(Unit a, Unit b) => a.NotEquals(b);
        public Unit Half => this.Divide(((Number)2));
        public Unit Quarter => this.Divide(((Number)4));
        public Unit Eight => this.Divide(((Number)8));
        public Unit Sixteenth => this.Divide(((Number)16));
        public Unit Tenth => this.Divide(((Number)10));
        public Unit Twice => this.Multiply(((Number)2));
        public Unit Hundred => this.Multiply(((Number)100));
        public Unit Thousand => this.Multiply(((Number)1000));
        public Unit Million => this.Thousand.Thousand;
        public Unit Billion => this.Thousand.Million;
        public Boolean LessThan(Unit b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Unit a, Unit b) => a.LessThan(b);
        public Boolean GreaterThan(Unit b) => b.LessThan(this);
        public static Boolean operator >(Unit a, Unit b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Unit b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Unit a, Unit b) => a.GreaterThanOrEquals(b);
        public Unit Lesser(Unit b) => this.LessThanOrEquals(b) ? this : b;
        public Unit Greater(Unit b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Unit b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Unit CubicBezier(Unit b, Unit c, Unit d, Number t) => this.Multiply(((Number)1).Subtract(t).Cube).Add(b.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr.Multiply(t))).Add(c.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Multiply(t.Sqr))).Add(d.Multiply(t.Cube))));
        public Unit CubicBezierDerivative(Unit b, Unit c, Unit d, Number t) => b.Subtract(this).Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr)).Add(c.Subtract(b).Multiply(((Number)6).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(d.Subtract(c).Multiply(((Number)3).Multiply(t.Sqr))));
        public Unit CubicBezierSecondDerivative(Unit b, Unit c, Unit d, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(((Number)1).Subtract(t))).Add(d.Subtract(c.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(t)));
        public Unit QuadraticBezier(Unit b, Unit c, Number t) => this.Multiply(((Number)1).Subtract(t).Sqr).Add(b.Multiply(((Number)2).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(c.Multiply(t.Sqr)));
        public Unit QuadraticBezierDerivative(Unit b, Unit c, Number t) => b.Subtract(b).Multiply(((Number)2).Multiply(((Number)1).Subtract(t))).Add(c.Subtract(b).Multiply(((Number)2).Multiply(t)));
        public Unit QuadraticBezierSecondDerivative(Unit b, Unit c, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this));
        public Unit Lerp(Unit b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Unit Barycentric(Unit v2, Unit v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        public Unit Parabola => this.Sqr;
        public Unit Pow2 => this.Multiply(this);
        public Unit Pow3 => this.Pow2.Multiply(this);
        public Unit Pow4 => this.Pow3.Multiply(this);
        public Unit Pow5 => this.Pow4.Multiply(this);
        public Unit Square => this.Pow2;
        public Unit Sqr => this.Pow2;
        public Unit Cube => this.Pow3;
        // Unimplemented concept functions
        public Boolean LessThanOrEquals(Unit y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Unit x, Unit y) => x.LessThanOrEquals(y);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Probability: IMeasure<Probability>
    {
        [DataMember] public readonly Number Value;
        public Probability WithValue(Number value) => new Probability(value);
        public Probability(Number value) => (Value) = (value);
        public static Probability Default = new Probability();
        public static Probability New(Number value) => new Probability(value);
        public Plato.SinglePrecision.Probability ChangePrecision() => (Value.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Probability(Probability self) => self.ChangePrecision();
        public static implicit operator Number(Probability self) => self.Value;
        public static implicit operator Probability(Number value) => new Probability(value);
        public static implicit operator Probability(Integer value) => new Probability(value);
        public static implicit operator Probability(int value) => new Integer(value);
        public static implicit operator Probability(double value) => new Number(value);
        public static implicit operator double(Probability value) => value.Value;
        public override bool Equals(object obj) { if (!(obj is Probability)) return false; var other = (Probability)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => $"{{ \"Value\" = {Value} }}";
        public static implicit operator Dynamic(Probability self) => new Dynamic(self);
        public static implicit operator Probability(Dynamic value) => value.As<Probability>();
        public String TypeName => "Probability";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        Number INumberLike.ToNumber => this.ToNumber;
        INumberLike INumberLike.FromNumber(Number n) => this.FromNumber((Number)n);
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Probability)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Probability)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Probability)b);
        IAdditive IAdditive.Negative => this.Negative;
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Probability)y);
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(Value);
        public Probability FromComponents(IArray<Number> numbers) => new Probability(numbers[0]);
        // Implemented concept functions and type functions
        public Probability Multiply(Probability y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Probability operator *(Probability x, Probability y) => x.Multiply(y);
        public Probability Divide(Probability y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Probability operator /(Probability x, Probability y) => x.Divide(y);
        public Probability Modulo(Probability y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public static Probability operator %(Probability x, Probability y) => x.Modulo(y);
        public Number Number => this.ToNumber;
        public Number ToNumber => this.Component(((Integer)0));
        public Probability FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Probability b) => this.ToNumber.LessThan(b.ToNumber) ? ((Integer)1).Negative : this.ToNumber.GreaterThan(b.ToNumber) ? ((Integer)1) : ((Integer)0);
        public Probability Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
        public static Probability operator +(Probability x, Number y) => x.Add(y);
        public Probability Subract(Number y) => this.FromNumber(this.ToNumber.Subtract(y));
        public Probability PlusOne => this.Add(this.One);
        public Probability MinusOne => this.Subtract(this.One);
        public Probability FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Probability MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Probability ZipComponents(Probability y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Probability Zero => this.MapComponents((i) => ((Number)0));
        public Probability One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Probability MinValue => this.MapComponents((x) => x.MinValue);
        public Probability MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Probability a, Probability b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Probability Clamp(Probability a, Probability b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Probability ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Probability Clamp01 => this.ClampZeroOne;
        public Probability Abs => this.MapComponents((i) => i.Abs);
        public Probability Min(Probability y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Probability Max(Probability y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Probability Floor => this.MapComponents((c) => c.Floor);
        public Probability Fract => this.MapComponents((c) => c.Fract);
        public Probability Multiply(Number s){
            var _var370 = s;
            return this.MapComponents((i) => i.Multiply(_var370));
        }
        public static Probability operator *(Probability x, Number s) => x.Multiply(s);
        public Probability Divide(Number s){
            var _var371 = s;
            return this.MapComponents((i) => i.Divide(_var371));
        }
        public static Probability operator /(Probability x, Number s) => x.Divide(s);
        public Probability Modulo(Number s){
            var _var372 = s;
            return this.MapComponents((i) => i.Modulo(_var372));
        }
        public static Probability operator %(Probability x, Number s) => x.Modulo(s);
        public Probability Add(Probability y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Probability operator +(Probability x, Probability y) => x.Add(y);
        public Probability Subtract(Probability y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Probability operator -(Probability x, Probability y) => x.Subtract(y);
        public Probability Negative => this.MapComponents((a) => a.Negative);
        public static Probability operator -(Probability x) => x.Negative;
        public IArray<Probability> Repeat(Integer n){
            var _var373 = this;
            return n.MapRange((i) => _var373);
        }
        public Boolean Equals(Probability b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Probability a, Probability b) => a.Equals(b);
        public Boolean NotEquals(Probability b) => this.Equals(b).Not;
        public static Boolean operator !=(Probability a, Probability b) => a.NotEquals(b);
        public Probability Half => this.Divide(((Number)2));
        public Probability Quarter => this.Divide(((Number)4));
        public Probability Eight => this.Divide(((Number)8));
        public Probability Sixteenth => this.Divide(((Number)16));
        public Probability Tenth => this.Divide(((Number)10));
        public Probability Twice => this.Multiply(((Number)2));
        public Probability Hundred => this.Multiply(((Number)100));
        public Probability Thousand => this.Multiply(((Number)1000));
        public Probability Million => this.Thousand.Thousand;
        public Probability Billion => this.Thousand.Million;
        public Boolean LessThan(Probability b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Probability a, Probability b) => a.LessThan(b);
        public Boolean GreaterThan(Probability b) => b.LessThan(this);
        public static Boolean operator >(Probability a, Probability b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Probability b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Probability a, Probability b) => a.GreaterThanOrEquals(b);
        public Probability Lesser(Probability b) => this.LessThanOrEquals(b) ? this : b;
        public Probability Greater(Probability b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Probability b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Probability Lerp(Probability b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Probability Barycentric(Probability v2, Probability v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        // Unimplemented concept functions
        public Boolean LessThanOrEquals(Probability y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Probability x, Probability y) => x.LessThanOrEquals(y);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Complex: IVector<Complex>
    {
        [DataMember] public readonly Number IReal;
        [DataMember] public readonly Number Imaginary;
        public Complex WithIReal(Number iReal) => new Complex(iReal, Imaginary);
        public Complex WithImaginary(Number imaginary) => new Complex(IReal, imaginary);
        public Complex(Number iReal, Number imaginary) => (IReal, Imaginary) = (iReal, imaginary);
        public static Complex Default = new Complex();
        public static Complex New(Number iReal, Number imaginary) => new Complex(iReal, imaginary);
        public Plato.SinglePrecision.Complex ChangePrecision() => (IReal.ChangePrecision(), Imaginary.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Complex(Complex self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Complex self) => (self.IReal, self.Imaginary);
        public static implicit operator Complex((Number, Number) value) => new Complex(value.Item1, value.Item2);
        public void Deconstruct(out Number iReal, out Number imaginary) { iReal = IReal; imaginary = Imaginary; }
        public override bool Equals(object obj) { if (!(obj is Complex)) return false; var other = (Complex)obj; return IReal.Equals(other.IReal) && Imaginary.Equals(other.Imaginary); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(IReal, Imaginary);
        public override string ToString() => $"{{ \"IReal\" = {IReal}, \"Imaginary\" = {Imaginary} }}";
        public static implicit operator Dynamic(Complex self) => new Dynamic(self);
        public static implicit operator Complex(Dynamic value) => value.As<Complex>();
        public String TypeName => "Complex";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"IReal", (String)"Imaginary");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(IReal), new Dynamic(Imaginary));
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Complex)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Complex)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Complex)b);
        IAdditive IAdditive.Negative => this.Negative;
        IMultiplicative IMultiplicative.Multiply(IMultiplicative b) => this.Multiply((Complex)b);
        IDivisible IDivisible.Divide(IDivisible b) => this.Divide((Complex)b);
        IModulo IModulo.Modulo(IModulo b) => this.Modulo((Complex)b);
        // Array predefined functions
        public Complex(IArray<Number> xs) : this(xs[0], xs[1]) { }
        public Complex(Number[] xs) : this(xs[0], xs[1]) { }
        public static Complex New(IArray<Number> xs) => new Complex(xs);
        public static Complex New(Number[] xs) => new Complex(xs);
        public static implicit operator Number[](Complex self) => self.ToSystemArray();
        public static implicit operator Array<Number>(Complex self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Number> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Number System.Collections.Generic.IReadOnlyList<Number>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Number>.Count => this.Count;
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(IReal, Imaginary);
        public Complex FromComponents(IArray<Number> numbers) => new Complex(numbers[0], numbers[1]);
        // Implemented concept functions and type functions
        public Integer Count => ((Integer)2);
        public Number At(Integer n) => n.Equals(((Integer)0)) ? this.IReal : this.Imaginary;
        public Number this[Integer n] => At(n);
        public Complex Multiply(Complex y) => this.ZipComponents(y, (a, b) => a.Multiply(b));
        public static Complex operator *(Complex x, Complex y) => x.Multiply(y);
        public Complex Divide(Complex y) => this.ZipComponents(y, (a, b) => a.Divide(b));
        public static Complex operator /(Complex x, Complex y) => x.Divide(y);
        public Complex Modulo(Complex y) => this.ZipComponents(y, (a, b) => a.Modulo(b));
        public static Complex operator %(Complex x, Complex y) => x.Modulo(y);
        public Number Length => this.Magnitude;
        public Number LengthSquared => this.MagnitudeSquared;
        public Number Sum => this.Reduce(((Number)0), (a, b) => a.Add(b));
        public Number SumSquares => this.Square.Sum;
        public Number MagnitudeSquared => this.SumSquares;
        public Number Magnitude => this.MagnitudeSquared.SquareRoot;
        public Number Dot(Complex v2) => this.Multiply(v2).Sum;
        public Number Average => this.Sum.Divide(this.Count);
        public Complex Normalize => this.MagnitudeSquared.GreaterThan(((Integer)0)) ? this.Divide(this.Magnitude) : this.Zero;
        public Complex Reflect(Complex normal) => this.Subtract(normal.Multiply(this.Dot(normal).Multiply(((Number)2))));
        public Complex Project(Complex other) => other.Multiply(this.Dot(other));
        public Number Distance(Complex b) => b.Subtract(this).Magnitude;
        public Number DistanceSquared(Complex b) => b.Subtract(this).Magnitude;
        public Angle Angle(Complex b) => this.Dot(b).Divide(this.Magnitude.Multiply(b.Magnitude)).Acos;
        public Complex PlusOne => this.Add(this.One);
        public Complex MinusOne => this.Subtract(this.One);
        public Complex FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Complex MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Complex ZipComponents(Complex y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Complex Zero => this.MapComponents((i) => ((Number)0));
        public Complex One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Complex MinValue => this.MapComponents((x) => x.MinValue);
        public Complex MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Complex a, Complex b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Complex Clamp(Complex a, Complex b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Complex ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Complex Clamp01 => this.ClampZeroOne;
        public Complex Abs => this.MapComponents((i) => i.Abs);
        public Complex Min(Complex y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Complex Max(Complex y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Complex Floor => this.MapComponents((c) => c.Floor);
        public Complex Fract => this.MapComponents((c) => c.Fract);
        public Complex Multiply(Number s){
            var _var374 = s;
            return this.MapComponents((i) => i.Multiply(_var374));
        }
        public static Complex operator *(Complex x, Number s) => x.Multiply(s);
        public Complex Divide(Number s){
            var _var375 = s;
            return this.MapComponents((i) => i.Divide(_var375));
        }
        public static Complex operator /(Complex x, Number s) => x.Divide(s);
        public Complex Modulo(Number s){
            var _var376 = s;
            return this.MapComponents((i) => i.Modulo(_var376));
        }
        public static Complex operator %(Complex x, Number s) => x.Modulo(s);
        public Complex Add(Complex y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Complex operator +(Complex x, Complex y) => x.Add(y);
        public Complex Subtract(Complex y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Complex operator -(Complex x, Complex y) => x.Subtract(y);
        public Complex Negative => this.MapComponents((a) => a.Negative);
        public static Complex operator -(Complex x) => x.Negative;
        public IArray<Complex> Repeat(Integer n){
            var _var377 = this;
            return n.MapRange((i) => _var377);
        }
        public Boolean Equals(Complex b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Complex a, Complex b) => a.Equals(b);
        public Boolean NotEquals(Complex b) => this.Equals(b).Not;
        public static Boolean operator !=(Complex a, Complex b) => a.NotEquals(b);
        public Complex Half => this.Divide(((Number)2));
        public Complex Quarter => this.Divide(((Number)4));
        public Complex Eight => this.Divide(((Number)8));
        public Complex Sixteenth => this.Divide(((Number)16));
        public Complex Tenth => this.Divide(((Number)10));
        public Complex Twice => this.Multiply(((Number)2));
        public Complex Hundred => this.Multiply(((Number)100));
        public Complex Thousand => this.Multiply(((Number)1000));
        public Complex Million => this.Thousand.Thousand;
        public Complex Billion => this.Thousand.Million;
        public Complex Pow2 => this.Multiply(this);
        public Complex Pow3 => this.Pow2.Multiply(this);
        public Complex Pow4 => this.Pow3.Multiply(this);
        public Complex Pow5 => this.Pow4.Multiply(this);
        public Complex Square => this.Pow2;
        public Complex Sqr => this.Pow2;
        public Complex Cube => this.Pow3;
        public Complex Parabola => this.Sqr;
        public Complex Lerp(Complex b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Complex Barycentric(Complex v2, Complex v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        public Complex CubicBezier(Complex b, Complex c, Complex d, Number t) => this.Multiply(((Number)1).Subtract(t).Cube).Add(b.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr.Multiply(t))).Add(c.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Multiply(t.Sqr))).Add(d.Multiply(t.Cube))));
        public Complex CubicBezierDerivative(Complex b, Complex c, Complex d, Number t) => b.Subtract(this).Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr)).Add(c.Subtract(b).Multiply(((Number)6).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(d.Subtract(c).Multiply(((Number)3).Multiply(t.Sqr))));
        public Complex CubicBezierSecondDerivative(Complex b, Complex c, Complex d, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(((Number)1).Subtract(t))).Add(d.Subtract(c.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(t)));
        public Complex QuadraticBezier(Complex b, Complex c, Number t) => this.Multiply(((Number)1).Subtract(t).Sqr).Add(b.Multiply(((Number)2).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(c.Multiply(t.Sqr)));
        public Complex QuadraticBezierDerivative(Complex b, Complex c, Number t) => b.Subtract(b).Multiply(((Number)2).Multiply(((Number)1).Subtract(t))).Add(c.Subtract(b).Multiply(((Number)2).Multiply(t)));
        public Complex QuadraticBezierSecondDerivative(Complex b, Complex c, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this));
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Integer2: IValue<Integer2>, IArray<Integer>
    {
        [DataMember] public readonly Integer A;
        [DataMember] public readonly Integer B;
        public Integer2 WithA(Integer a) => new Integer2(a, B);
        public Integer2 WithB(Integer b) => new Integer2(A, b);
        public Integer2(Integer a, Integer b) => (A, B) = (a, b);
        public static Integer2 Default = new Integer2();
        public static Integer2 New(Integer a, Integer b) => new Integer2(a, b);
        public Plato.SinglePrecision.Integer2 ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Integer2(Integer2 self) => self.ChangePrecision();
        public static implicit operator (Integer, Integer)(Integer2 self) => (self.A, self.B);
        public static implicit operator Integer2((Integer, Integer) value) => new Integer2(value.Item1, value.Item2);
        public void Deconstruct(out Integer a, out Integer b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Integer2)) return false; var other = (Integer2)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B} }}";
        public static implicit operator Dynamic(Integer2 self) => new Dynamic(self);
        public static implicit operator Integer2(Dynamic value) => value.As<Integer2>();
        public String TypeName => "Integer2";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Integer2)b);
        // Array predefined functions
        public Integer2(IArray<Integer> xs) : this(xs[0], xs[1]) { }
        public Integer2(Integer[] xs) : this(xs[0], xs[1]) { }
        public static Integer2 New(IArray<Integer> xs) => new Integer2(xs);
        public static Integer2 New(Integer[] xs) => new Integer2(xs);
        public static implicit operator Integer[](Integer2 self) => self.ToSystemArray();
        public static implicit operator Array<Integer>(Integer2 self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Integer> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Integer System.Collections.Generic.IReadOnlyList<Integer>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Integer>.Count => this.Count;
        // Implemented concept functions and type functions
        public IArray<Integer2> Repeat(Integer n){
            var _var378 = this;
            return n.MapRange((i) => _var378);
        }
        public Boolean Equals(Integer2 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Integer2 a, Integer2 b) => a.Equals(b);
        public Boolean NotEquals(Integer2 b) => this.Equals(b).Not;
        public static Boolean operator !=(Integer2 a, Integer2 b) => a.NotEquals(b);
        // Unimplemented concept functions
        public Integer Count => 2;
        public Integer At(Integer n) => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
        public Integer this[Integer n] => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Integer3: IValue<Integer3>, IArray<Integer>
    {
        [DataMember] public readonly Integer A;
        [DataMember] public readonly Integer B;
        [DataMember] public readonly Integer C;
        public Integer3 WithA(Integer a) => new Integer3(a, B, C);
        public Integer3 WithB(Integer b) => new Integer3(A, b, C);
        public Integer3 WithC(Integer c) => new Integer3(A, B, c);
        public Integer3(Integer a, Integer b, Integer c) => (A, B, C) = (a, b, c);
        public static Integer3 Default = new Integer3();
        public static Integer3 New(Integer a, Integer b, Integer c) => new Integer3(a, b, c);
        public Plato.SinglePrecision.Integer3 ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Integer3(Integer3 self) => self.ChangePrecision();
        public static implicit operator (Integer, Integer, Integer)(Integer3 self) => (self.A, self.B, self.C);
        public static implicit operator Integer3((Integer, Integer, Integer) value) => new Integer3(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Integer a, out Integer b, out Integer c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is Integer3)) return false; var other = (Integer3)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C} }}";
        public static implicit operator Dynamic(Integer3 self) => new Dynamic(self);
        public static implicit operator Integer3(Dynamic value) => value.As<Integer3>();
        public String TypeName => "Integer3";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Integer3)b);
        // Array predefined functions
        public Integer3(IArray<Integer> xs) : this(xs[0], xs[1], xs[2]) { }
        public Integer3(Integer[] xs) : this(xs[0], xs[1], xs[2]) { }
        public static Integer3 New(IArray<Integer> xs) => new Integer3(xs);
        public static Integer3 New(Integer[] xs) => new Integer3(xs);
        public static implicit operator Integer[](Integer3 self) => self.ToSystemArray();
        public static implicit operator Array<Integer>(Integer3 self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Integer> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Integer System.Collections.Generic.IReadOnlyList<Integer>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Integer>.Count => this.Count;
        // Implemented concept functions and type functions
        public IArray<Integer3> Repeat(Integer n){
            var _var379 = this;
            return n.MapRange((i) => _var379);
        }
        public Boolean Equals(Integer3 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Integer3 a, Integer3 b) => a.Equals(b);
        public Boolean NotEquals(Integer3 b) => this.Equals(b).Not;
        public static Boolean operator !=(Integer3 a, Integer3 b) => a.NotEquals(b);
        // Unimplemented concept functions
        public Integer Count => 3;
        public Integer At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Integer this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Integer4: IValue<Integer4>, IArray<Integer>
    {
        [DataMember] public readonly Integer A;
        [DataMember] public readonly Integer B;
        [DataMember] public readonly Integer C;
        [DataMember] public readonly Integer D;
        public Integer4 WithA(Integer a) => new Integer4(a, B, C, D);
        public Integer4 WithB(Integer b) => new Integer4(A, b, C, D);
        public Integer4 WithC(Integer c) => new Integer4(A, B, c, D);
        public Integer4 WithD(Integer d) => new Integer4(A, B, C, d);
        public Integer4(Integer a, Integer b, Integer c, Integer d) => (A, B, C, D) = (a, b, c, d);
        public static Integer4 Default = new Integer4();
        public static Integer4 New(Integer a, Integer b, Integer c, Integer d) => new Integer4(a, b, c, d);
        public Plato.SinglePrecision.Integer4 ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision(), D.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Integer4(Integer4 self) => self.ChangePrecision();
        public static implicit operator (Integer, Integer, Integer, Integer)(Integer4 self) => (self.A, self.B, self.C, self.D);
        public static implicit operator Integer4((Integer, Integer, Integer, Integer) value) => new Integer4(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Integer a, out Integer b, out Integer c, out Integer d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is Integer4)) return false; var other = (Integer4)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C}, \"D\" = {D} }}";
        public static implicit operator Dynamic(Integer4 self) => new Dynamic(self);
        public static implicit operator Integer4(Dynamic value) => value.As<Integer4>();
        public String TypeName => "Integer4";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Integer4)b);
        // Array predefined functions
        public Integer4(IArray<Integer> xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public Integer4(Integer[] xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public static Integer4 New(IArray<Integer> xs) => new Integer4(xs);
        public static Integer4 New(Integer[] xs) => new Integer4(xs);
        public static implicit operator Integer[](Integer4 self) => self.ToSystemArray();
        public static implicit operator Array<Integer>(Integer4 self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Integer> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Integer System.Collections.Generic.IReadOnlyList<Integer>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Integer>.Count => this.Count;
        // Implemented concept functions and type functions
        public IArray<Integer4> Repeat(Integer n){
            var _var380 = this;
            return n.MapRange((i) => _var380);
        }
        public Boolean Equals(Integer4 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Integer4 a, Integer4 b) => a.Equals(b);
        public Boolean NotEquals(Integer4 b) => this.Equals(b).Not;
        public static Boolean operator !=(Integer4 a, Integer4 b) => a.NotEquals(b);
        // Unimplemented concept functions
        public Integer Count => 4;
        public Integer At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Integer this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Color: ICoordinate<Color>
    {
        [DataMember] public readonly Unit R;
        [DataMember] public readonly Unit G;
        [DataMember] public readonly Unit B;
        [DataMember] public readonly Unit A;
        public Color WithR(Unit r) => new Color(r, G, B, A);
        public Color WithG(Unit g) => new Color(R, g, B, A);
        public Color WithB(Unit b) => new Color(R, G, b, A);
        public Color WithA(Unit a) => new Color(R, G, B, a);
        public Color(Unit r, Unit g, Unit b, Unit a) => (R, G, B, A) = (r, g, b, a);
        public static Color Default = new Color();
        public static Color New(Unit r, Unit g, Unit b, Unit a) => new Color(r, g, b, a);
        public Plato.SinglePrecision.Color ChangePrecision() => (R.ChangePrecision(), G.ChangePrecision(), B.ChangePrecision(), A.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Color(Color self) => self.ChangePrecision();
        public static implicit operator (Unit, Unit, Unit, Unit)(Color self) => (self.R, self.G, self.B, self.A);
        public static implicit operator Color((Unit, Unit, Unit, Unit) value) => new Color(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Unit r, out Unit g, out Unit b, out Unit a) { r = R; g = G; b = B; a = A; }
        public override bool Equals(object obj) { if (!(obj is Color)) return false; var other = (Color)obj; return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(R, G, B, A);
        public override string ToString() => $"{{ \"R\" = {R}, \"G\" = {G}, \"B\" = {B}, \"A\" = {A} }}";
        public static implicit operator Dynamic(Color self) => new Dynamic(self);
        public static implicit operator Color(Dynamic value) => value.As<Color>();
        public String TypeName => "Color";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"R", (String)"G", (String)"B", (String)"A");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(R), new Dynamic(G), new Dynamic(B), new Dynamic(A));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Color)b);
        // Implemented concept functions and type functions
        public IArray<Color> Repeat(Integer n){
            var _var381 = this;
            return n.MapRange((i) => _var381);
        }
        public Boolean Equals(Color b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Color a, Color b) => a.Equals(b);
        public Boolean NotEquals(Color b) => this.Equals(b).Not;
        public static Boolean operator !=(Color a, Color b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ColorLUV: ICoordinate<ColorLUV>
    {
        [DataMember] public readonly Unit Lightness;
        [DataMember] public readonly Unit U;
        [DataMember] public readonly Unit V;
        public ColorLUV WithLightness(Unit lightness) => new ColorLUV(lightness, U, V);
        public ColorLUV WithU(Unit u) => new ColorLUV(Lightness, u, V);
        public ColorLUV WithV(Unit v) => new ColorLUV(Lightness, U, v);
        public ColorLUV(Unit lightness, Unit u, Unit v) => (Lightness, U, V) = (lightness, u, v);
        public static ColorLUV Default = new ColorLUV();
        public static ColorLUV New(Unit lightness, Unit u, Unit v) => new ColorLUV(lightness, u, v);
        public Plato.SinglePrecision.ColorLUV ChangePrecision() => (Lightness.ChangePrecision(), U.ChangePrecision(), V.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ColorLUV(ColorLUV self) => self.ChangePrecision();
        public static implicit operator (Unit, Unit, Unit)(ColorLUV self) => (self.Lightness, self.U, self.V);
        public static implicit operator ColorLUV((Unit, Unit, Unit) value) => new ColorLUV(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Unit lightness, out Unit u, out Unit v) { lightness = Lightness; u = U; v = V; }
        public override bool Equals(object obj) { if (!(obj is ColorLUV)) return false; var other = (ColorLUV)obj; return Lightness.Equals(other.Lightness) && U.Equals(other.U) && V.Equals(other.V); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Lightness, U, V);
        public override string ToString() => $"{{ \"Lightness\" = {Lightness}, \"U\" = {U}, \"V\" = {V} }}";
        public static implicit operator Dynamic(ColorLUV self) => new Dynamic(self);
        public static implicit operator ColorLUV(Dynamic value) => value.As<ColorLUV>();
        public String TypeName => "ColorLUV";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Lightness", (String)"U", (String)"V");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Lightness), new Dynamic(U), new Dynamic(V));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((ColorLUV)b);
        // Implemented concept functions and type functions
        public IArray<ColorLUV> Repeat(Integer n){
            var _var382 = this;
            return n.MapRange((i) => _var382);
        }
        public Boolean Equals(ColorLUV b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(ColorLUV a, ColorLUV b) => a.Equals(b);
        public Boolean NotEquals(ColorLUV b) => this.Equals(b).Not;
        public static Boolean operator !=(ColorLUV a, ColorLUV b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ColorLAB: ICoordinate<ColorLAB>
    {
        [DataMember] public readonly Unit Lightness;
        [DataMember] public readonly Number A;
        [DataMember] public readonly Number B;
        public ColorLAB WithLightness(Unit lightness) => new ColorLAB(lightness, A, B);
        public ColorLAB WithA(Number a) => new ColorLAB(Lightness, a, B);
        public ColorLAB WithB(Number b) => new ColorLAB(Lightness, A, b);
        public ColorLAB(Unit lightness, Number a, Number b) => (Lightness, A, B) = (lightness, a, b);
        public static ColorLAB Default = new ColorLAB();
        public static ColorLAB New(Unit lightness, Number a, Number b) => new ColorLAB(lightness, a, b);
        public Plato.SinglePrecision.ColorLAB ChangePrecision() => (Lightness.ChangePrecision(), A.ChangePrecision(), B.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ColorLAB(ColorLAB self) => self.ChangePrecision();
        public static implicit operator (Unit, Number, Number)(ColorLAB self) => (self.Lightness, self.A, self.B);
        public static implicit operator ColorLAB((Unit, Number, Number) value) => new ColorLAB(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Unit lightness, out Number a, out Number b) { lightness = Lightness; a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is ColorLAB)) return false; var other = (ColorLAB)obj; return Lightness.Equals(other.Lightness) && A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Lightness, A, B);
        public override string ToString() => $"{{ \"Lightness\" = {Lightness}, \"A\" = {A}, \"B\" = {B} }}";
        public static implicit operator Dynamic(ColorLAB self) => new Dynamic(self);
        public static implicit operator ColorLAB(Dynamic value) => value.As<ColorLAB>();
        public String TypeName => "ColorLAB";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Lightness", (String)"A", (String)"B");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Lightness), new Dynamic(A), new Dynamic(B));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((ColorLAB)b);
        // Implemented concept functions and type functions
        public IArray<ColorLAB> Repeat(Integer n){
            var _var383 = this;
            return n.MapRange((i) => _var383);
        }
        public Boolean Equals(ColorLAB b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(ColorLAB a, ColorLAB b) => a.Equals(b);
        public Boolean NotEquals(ColorLAB b) => this.Equals(b).Not;
        public static Boolean operator !=(ColorLAB a, ColorLAB b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ColorLCh: ICoordinate<ColorLCh>
    {
        [DataMember] public readonly Unit Lightness;
        [DataMember] public readonly PolarCoordinate ChromaHue;
        public ColorLCh WithLightness(Unit lightness) => new ColorLCh(lightness, ChromaHue);
        public ColorLCh WithChromaHue(PolarCoordinate chromaHue) => new ColorLCh(Lightness, chromaHue);
        public ColorLCh(Unit lightness, PolarCoordinate chromaHue) => (Lightness, ChromaHue) = (lightness, chromaHue);
        public static ColorLCh Default = new ColorLCh();
        public static ColorLCh New(Unit lightness, PolarCoordinate chromaHue) => new ColorLCh(lightness, chromaHue);
        public Plato.SinglePrecision.ColorLCh ChangePrecision() => (Lightness.ChangePrecision(), ChromaHue.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ColorLCh(ColorLCh self) => self.ChangePrecision();
        public static implicit operator (Unit, PolarCoordinate)(ColorLCh self) => (self.Lightness, self.ChromaHue);
        public static implicit operator ColorLCh((Unit, PolarCoordinate) value) => new ColorLCh(value.Item1, value.Item2);
        public void Deconstruct(out Unit lightness, out PolarCoordinate chromaHue) { lightness = Lightness; chromaHue = ChromaHue; }
        public override bool Equals(object obj) { if (!(obj is ColorLCh)) return false; var other = (ColorLCh)obj; return Lightness.Equals(other.Lightness) && ChromaHue.Equals(other.ChromaHue); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Lightness, ChromaHue);
        public override string ToString() => $"{{ \"Lightness\" = {Lightness}, \"ChromaHue\" = {ChromaHue} }}";
        public static implicit operator Dynamic(ColorLCh self) => new Dynamic(self);
        public static implicit operator ColorLCh(Dynamic value) => value.As<ColorLCh>();
        public String TypeName => "ColorLCh";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Lightness", (String)"ChromaHue");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Lightness), new Dynamic(ChromaHue));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((ColorLCh)b);
        // Implemented concept functions and type functions
        public IArray<ColorLCh> Repeat(Integer n){
            var _var384 = this;
            return n.MapRange((i) => _var384);
        }
        public Boolean Equals(ColorLCh b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(ColorLCh a, ColorLCh b) => a.Equals(b);
        public Boolean NotEquals(ColorLCh b) => this.Equals(b).Not;
        public static Boolean operator !=(ColorLCh a, ColorLCh b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ColorHSV: ICoordinate<ColorHSV>
    {
        [DataMember] public readonly Angle Hue;
        [DataMember] public readonly Unit S;
        [DataMember] public readonly Unit V;
        public ColorHSV WithHue(Angle hue) => new ColorHSV(hue, S, V);
        public ColorHSV WithS(Unit s) => new ColorHSV(Hue, s, V);
        public ColorHSV WithV(Unit v) => new ColorHSV(Hue, S, v);
        public ColorHSV(Angle hue, Unit s, Unit v) => (Hue, S, V) = (hue, s, v);
        public static ColorHSV Default = new ColorHSV();
        public static ColorHSV New(Angle hue, Unit s, Unit v) => new ColorHSV(hue, s, v);
        public Plato.SinglePrecision.ColorHSV ChangePrecision() => (Hue.ChangePrecision(), S.ChangePrecision(), V.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ColorHSV(ColorHSV self) => self.ChangePrecision();
        public static implicit operator (Angle, Unit, Unit)(ColorHSV self) => (self.Hue, self.S, self.V);
        public static implicit operator ColorHSV((Angle, Unit, Unit) value) => new ColorHSV(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Angle hue, out Unit s, out Unit v) { hue = Hue; s = S; v = V; }
        public override bool Equals(object obj) { if (!(obj is ColorHSV)) return false; var other = (ColorHSV)obj; return Hue.Equals(other.Hue) && S.Equals(other.S) && V.Equals(other.V); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Hue, S, V);
        public override string ToString() => $"{{ \"Hue\" = {Hue}, \"S\" = {S}, \"V\" = {V} }}";
        public static implicit operator Dynamic(ColorHSV self) => new Dynamic(self);
        public static implicit operator ColorHSV(Dynamic value) => value.As<ColorHSV>();
        public String TypeName => "ColorHSV";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Hue", (String)"S", (String)"V");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Hue), new Dynamic(S), new Dynamic(V));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((ColorHSV)b);
        // Implemented concept functions and type functions
        public IArray<ColorHSV> Repeat(Integer n){
            var _var385 = this;
            return n.MapRange((i) => _var385);
        }
        public Boolean Equals(ColorHSV b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(ColorHSV a, ColorHSV b) => a.Equals(b);
        public Boolean NotEquals(ColorHSV b) => this.Equals(b).Not;
        public static Boolean operator !=(ColorHSV a, ColorHSV b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ColorHSL: ICoordinate<ColorHSL>
    {
        [DataMember] public readonly Angle Hue;
        [DataMember] public readonly Unit Saturation;
        [DataMember] public readonly Unit Luminance;
        public ColorHSL WithHue(Angle hue) => new ColorHSL(hue, Saturation, Luminance);
        public ColorHSL WithSaturation(Unit saturation) => new ColorHSL(Hue, saturation, Luminance);
        public ColorHSL WithLuminance(Unit luminance) => new ColorHSL(Hue, Saturation, luminance);
        public ColorHSL(Angle hue, Unit saturation, Unit luminance) => (Hue, Saturation, Luminance) = (hue, saturation, luminance);
        public static ColorHSL Default = new ColorHSL();
        public static ColorHSL New(Angle hue, Unit saturation, Unit luminance) => new ColorHSL(hue, saturation, luminance);
        public Plato.SinglePrecision.ColorHSL ChangePrecision() => (Hue.ChangePrecision(), Saturation.ChangePrecision(), Luminance.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ColorHSL(ColorHSL self) => self.ChangePrecision();
        public static implicit operator (Angle, Unit, Unit)(ColorHSL self) => (self.Hue, self.Saturation, self.Luminance);
        public static implicit operator ColorHSL((Angle, Unit, Unit) value) => new ColorHSL(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Angle hue, out Unit saturation, out Unit luminance) { hue = Hue; saturation = Saturation; luminance = Luminance; }
        public override bool Equals(object obj) { if (!(obj is ColorHSL)) return false; var other = (ColorHSL)obj; return Hue.Equals(other.Hue) && Saturation.Equals(other.Saturation) && Luminance.Equals(other.Luminance); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Hue, Saturation, Luminance);
        public override string ToString() => $"{{ \"Hue\" = {Hue}, \"Saturation\" = {Saturation}, \"Luminance\" = {Luminance} }}";
        public static implicit operator Dynamic(ColorHSL self) => new Dynamic(self);
        public static implicit operator ColorHSL(Dynamic value) => value.As<ColorHSL>();
        public String TypeName => "ColorHSL";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Hue", (String)"Saturation", (String)"Luminance");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Hue), new Dynamic(Saturation), new Dynamic(Luminance));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((ColorHSL)b);
        // Implemented concept functions and type functions
        public IArray<ColorHSL> Repeat(Integer n){
            var _var386 = this;
            return n.MapRange((i) => _var386);
        }
        public Boolean Equals(ColorHSL b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(ColorHSL a, ColorHSL b) => a.Equals(b);
        public Boolean NotEquals(ColorHSL b) => this.Equals(b).Not;
        public static Boolean operator !=(ColorHSL a, ColorHSL b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ColorYCbCr: ICoordinate<ColorYCbCr>
    {
        [DataMember] public readonly Unit Y;
        [DataMember] public readonly Unit Cb;
        [DataMember] public readonly Unit Cr;
        public ColorYCbCr WithY(Unit y) => new ColorYCbCr(y, Cb, Cr);
        public ColorYCbCr WithCb(Unit cb) => new ColorYCbCr(Y, cb, Cr);
        public ColorYCbCr WithCr(Unit cr) => new ColorYCbCr(Y, Cb, cr);
        public ColorYCbCr(Unit y, Unit cb, Unit cr) => (Y, Cb, Cr) = (y, cb, cr);
        public static ColorYCbCr Default = new ColorYCbCr();
        public static ColorYCbCr New(Unit y, Unit cb, Unit cr) => new ColorYCbCr(y, cb, cr);
        public Plato.SinglePrecision.ColorYCbCr ChangePrecision() => (Y.ChangePrecision(), Cb.ChangePrecision(), Cr.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ColorYCbCr(ColorYCbCr self) => self.ChangePrecision();
        public static implicit operator (Unit, Unit, Unit)(ColorYCbCr self) => (self.Y, self.Cb, self.Cr);
        public static implicit operator ColorYCbCr((Unit, Unit, Unit) value) => new ColorYCbCr(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Unit y, out Unit cb, out Unit cr) { y = Y; cb = Cb; cr = Cr; }
        public override bool Equals(object obj) { if (!(obj is ColorYCbCr)) return false; var other = (ColorYCbCr)obj; return Y.Equals(other.Y) && Cb.Equals(other.Cb) && Cr.Equals(other.Cr); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Y, Cb, Cr);
        public override string ToString() => $"{{ \"Y\" = {Y}, \"Cb\" = {Cb}, \"Cr\" = {Cr} }}";
        public static implicit operator Dynamic(ColorYCbCr self) => new Dynamic(self);
        public static implicit operator ColorYCbCr(Dynamic value) => value.As<ColorYCbCr>();
        public String TypeName => "ColorYCbCr";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Y", (String)"Cb", (String)"Cr");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Y), new Dynamic(Cb), new Dynamic(Cr));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((ColorYCbCr)b);
        // Implemented concept functions and type functions
        public IArray<ColorYCbCr> Repeat(Integer n){
            var _var387 = this;
            return n.MapRange((i) => _var387);
        }
        public Boolean Equals(ColorYCbCr b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(ColorYCbCr a, ColorYCbCr b) => a.Equals(b);
        public Boolean NotEquals(ColorYCbCr b) => this.Equals(b).Not;
        public static Boolean operator !=(ColorYCbCr a, ColorYCbCr b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct SphericalCoordinate: ICoordinate<SphericalCoordinate>
    {
        [DataMember] public readonly Number RadialDistance;
        [DataMember] public readonly Angle Azimuth;
        [DataMember] public readonly Angle Polar;
        public SphericalCoordinate WithRadialDistance(Number radialDistance) => new SphericalCoordinate(radialDistance, Azimuth, Polar);
        public SphericalCoordinate WithAzimuth(Angle azimuth) => new SphericalCoordinate(RadialDistance, azimuth, Polar);
        public SphericalCoordinate WithPolar(Angle polar) => new SphericalCoordinate(RadialDistance, Azimuth, polar);
        public SphericalCoordinate(Number radialDistance, Angle azimuth, Angle polar) => (RadialDistance, Azimuth, Polar) = (radialDistance, azimuth, polar);
        public static SphericalCoordinate Default = new SphericalCoordinate();
        public static SphericalCoordinate New(Number radialDistance, Angle azimuth, Angle polar) => new SphericalCoordinate(radialDistance, azimuth, polar);
        public Plato.SinglePrecision.SphericalCoordinate ChangePrecision() => (RadialDistance.ChangePrecision(), Azimuth.ChangePrecision(), Polar.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.SphericalCoordinate(SphericalCoordinate self) => self.ChangePrecision();
        public static implicit operator (Number, Angle, Angle)(SphericalCoordinate self) => (self.RadialDistance, self.Azimuth, self.Polar);
        public static implicit operator SphericalCoordinate((Number, Angle, Angle) value) => new SphericalCoordinate(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number radialDistance, out Angle azimuth, out Angle polar) { radialDistance = RadialDistance; azimuth = Azimuth; polar = Polar; }
        public override bool Equals(object obj) { if (!(obj is SphericalCoordinate)) return false; var other = (SphericalCoordinate)obj; return RadialDistance.Equals(other.RadialDistance) && Azimuth.Equals(other.Azimuth) && Polar.Equals(other.Polar); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(RadialDistance, Azimuth, Polar);
        public override string ToString() => $"{{ \"RadialDistance\" = {RadialDistance}, \"Azimuth\" = {Azimuth}, \"Polar\" = {Polar} }}";
        public static implicit operator Dynamic(SphericalCoordinate self) => new Dynamic(self);
        public static implicit operator SphericalCoordinate(Dynamic value) => value.As<SphericalCoordinate>();
        public String TypeName => "SphericalCoordinate";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"RadialDistance", (String)"Azimuth", (String)"Polar");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(RadialDistance), new Dynamic(Azimuth), new Dynamic(Polar));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((SphericalCoordinate)b);
        // Implemented concept functions and type functions
        public IArray<SphericalCoordinate> Repeat(Integer n){
            var _var388 = this;
            return n.MapRange((i) => _var388);
        }
        public Boolean Equals(SphericalCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(SphericalCoordinate a, SphericalCoordinate b) => a.Equals(b);
        public Boolean NotEquals(SphericalCoordinate b) => this.Equals(b).Not;
        public static Boolean operator !=(SphericalCoordinate a, SphericalCoordinate b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct PolarCoordinate: ICoordinate<PolarCoordinate>
    {
        [DataMember] public readonly Number Radius;
        [DataMember] public readonly Angle Angle;
        public PolarCoordinate WithRadius(Number radius) => new PolarCoordinate(radius, Angle);
        public PolarCoordinate WithAngle(Angle angle) => new PolarCoordinate(Radius, angle);
        public PolarCoordinate(Number radius, Angle angle) => (Radius, Angle) = (radius, angle);
        public static PolarCoordinate Default = new PolarCoordinate();
        public static PolarCoordinate New(Number radius, Angle angle) => new PolarCoordinate(radius, angle);
        public Plato.SinglePrecision.PolarCoordinate ChangePrecision() => (Radius.ChangePrecision(), Angle.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.PolarCoordinate(PolarCoordinate self) => self.ChangePrecision();
        public static implicit operator (Number, Angle)(PolarCoordinate self) => (self.Radius, self.Angle);
        public static implicit operator PolarCoordinate((Number, Angle) value) => new PolarCoordinate(value.Item1, value.Item2);
        public void Deconstruct(out Number radius, out Angle angle) { radius = Radius; angle = Angle; }
        public override bool Equals(object obj) { if (!(obj is PolarCoordinate)) return false; var other = (PolarCoordinate)obj; return Radius.Equals(other.Radius) && Angle.Equals(other.Angle); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius, Angle);
        public override string ToString() => $"{{ \"Radius\" = {Radius}, \"Angle\" = {Angle} }}";
        public static implicit operator Dynamic(PolarCoordinate self) => new Dynamic(self);
        public static implicit operator PolarCoordinate(Dynamic value) => value.As<PolarCoordinate>();
        public String TypeName => "PolarCoordinate";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius", (String)"Angle");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius), new Dynamic(Angle));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((PolarCoordinate)b);
        // Implemented concept functions and type functions
        public Vector2D Vector2D => Vector2D.New(this.Angle.Cos, this.Angle.Sin).Multiply(this.Radius);
        public static implicit operator Vector2D(PolarCoordinate coord) => coord.Vector2D;
        public IArray<PolarCoordinate> Repeat(Integer n){
            var _var389 = this;
            return n.MapRange((i) => _var389);
        }
        public Boolean Equals(PolarCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(PolarCoordinate a, PolarCoordinate b) => a.Equals(b);
        public Boolean NotEquals(PolarCoordinate b) => this.Equals(b).Not;
        public static Boolean operator !=(PolarCoordinate a, PolarCoordinate b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct LogPolarCoordinate: ICoordinate<LogPolarCoordinate>
    {
        [DataMember] public readonly Number Rho;
        [DataMember] public readonly Angle Azimuth;
        public LogPolarCoordinate WithRho(Number rho) => new LogPolarCoordinate(rho, Azimuth);
        public LogPolarCoordinate WithAzimuth(Angle azimuth) => new LogPolarCoordinate(Rho, azimuth);
        public LogPolarCoordinate(Number rho, Angle azimuth) => (Rho, Azimuth) = (rho, azimuth);
        public static LogPolarCoordinate Default = new LogPolarCoordinate();
        public static LogPolarCoordinate New(Number rho, Angle azimuth) => new LogPolarCoordinate(rho, azimuth);
        public Plato.SinglePrecision.LogPolarCoordinate ChangePrecision() => (Rho.ChangePrecision(), Azimuth.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.LogPolarCoordinate(LogPolarCoordinate self) => self.ChangePrecision();
        public static implicit operator (Number, Angle)(LogPolarCoordinate self) => (self.Rho, self.Azimuth);
        public static implicit operator LogPolarCoordinate((Number, Angle) value) => new LogPolarCoordinate(value.Item1, value.Item2);
        public void Deconstruct(out Number rho, out Angle azimuth) { rho = Rho; azimuth = Azimuth; }
        public override bool Equals(object obj) { if (!(obj is LogPolarCoordinate)) return false; var other = (LogPolarCoordinate)obj; return Rho.Equals(other.Rho) && Azimuth.Equals(other.Azimuth); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Rho, Azimuth);
        public override string ToString() => $"{{ \"Rho\" = {Rho}, \"Azimuth\" = {Azimuth} }}";
        public static implicit operator Dynamic(LogPolarCoordinate self) => new Dynamic(self);
        public static implicit operator LogPolarCoordinate(Dynamic value) => value.As<LogPolarCoordinate>();
        public String TypeName => "LogPolarCoordinate";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Rho", (String)"Azimuth");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Rho), new Dynamic(Azimuth));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((LogPolarCoordinate)b);
        // Implemented concept functions and type functions
        public IArray<LogPolarCoordinate> Repeat(Integer n){
            var _var390 = this;
            return n.MapRange((i) => _var390);
        }
        public Boolean Equals(LogPolarCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(LogPolarCoordinate a, LogPolarCoordinate b) => a.Equals(b);
        public Boolean NotEquals(LogPolarCoordinate b) => this.Equals(b).Not;
        public static Boolean operator !=(LogPolarCoordinate a, LogPolarCoordinate b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct CylindricalCoordinate: ICoordinate<CylindricalCoordinate>
    {
        [DataMember] public readonly Number RadialDistance;
        [DataMember] public readonly Angle Azimuth;
        [DataMember] public readonly Number Height;
        public CylindricalCoordinate WithRadialDistance(Number radialDistance) => new CylindricalCoordinate(radialDistance, Azimuth, Height);
        public CylindricalCoordinate WithAzimuth(Angle azimuth) => new CylindricalCoordinate(RadialDistance, azimuth, Height);
        public CylindricalCoordinate WithHeight(Number height) => new CylindricalCoordinate(RadialDistance, Azimuth, height);
        public CylindricalCoordinate(Number radialDistance, Angle azimuth, Number height) => (RadialDistance, Azimuth, Height) = (radialDistance, azimuth, height);
        public static CylindricalCoordinate Default = new CylindricalCoordinate();
        public static CylindricalCoordinate New(Number radialDistance, Angle azimuth, Number height) => new CylindricalCoordinate(radialDistance, azimuth, height);
        public Plato.SinglePrecision.CylindricalCoordinate ChangePrecision() => (RadialDistance.ChangePrecision(), Azimuth.ChangePrecision(), Height.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.CylindricalCoordinate(CylindricalCoordinate self) => self.ChangePrecision();
        public static implicit operator (Number, Angle, Number)(CylindricalCoordinate self) => (self.RadialDistance, self.Azimuth, self.Height);
        public static implicit operator CylindricalCoordinate((Number, Angle, Number) value) => new CylindricalCoordinate(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number radialDistance, out Angle azimuth, out Number height) { radialDistance = RadialDistance; azimuth = Azimuth; height = Height; }
        public override bool Equals(object obj) { if (!(obj is CylindricalCoordinate)) return false; var other = (CylindricalCoordinate)obj; return RadialDistance.Equals(other.RadialDistance) && Azimuth.Equals(other.Azimuth) && Height.Equals(other.Height); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(RadialDistance, Azimuth, Height);
        public override string ToString() => $"{{ \"RadialDistance\" = {RadialDistance}, \"Azimuth\" = {Azimuth}, \"Height\" = {Height} }}";
        public static implicit operator Dynamic(CylindricalCoordinate self) => new Dynamic(self);
        public static implicit operator CylindricalCoordinate(Dynamic value) => value.As<CylindricalCoordinate>();
        public String TypeName => "CylindricalCoordinate";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"RadialDistance", (String)"Azimuth", (String)"Height");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(RadialDistance), new Dynamic(Azimuth), new Dynamic(Height));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((CylindricalCoordinate)b);
        // Implemented concept functions and type functions
        public IArray<CylindricalCoordinate> Repeat(Integer n){
            var _var391 = this;
            return n.MapRange((i) => _var391);
        }
        public Boolean Equals(CylindricalCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(CylindricalCoordinate a, CylindricalCoordinate b) => a.Equals(b);
        public Boolean NotEquals(CylindricalCoordinate b) => this.Equals(b).Not;
        public static Boolean operator !=(CylindricalCoordinate a, CylindricalCoordinate b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct HorizontalCoordinate: ICoordinate<HorizontalCoordinate>
    {
        [DataMember] public readonly Angle Altitude;
        [DataMember] public readonly Angle Azimuth;
        public HorizontalCoordinate WithAltitude(Angle altitude) => new HorizontalCoordinate(altitude, Azimuth);
        public HorizontalCoordinate WithAzimuth(Angle azimuth) => new HorizontalCoordinate(Altitude, azimuth);
        public HorizontalCoordinate(Angle altitude, Angle azimuth) => (Altitude, Azimuth) = (altitude, azimuth);
        public static HorizontalCoordinate Default = new HorizontalCoordinate();
        public static HorizontalCoordinate New(Angle altitude, Angle azimuth) => new HorizontalCoordinate(altitude, azimuth);
        public Plato.SinglePrecision.HorizontalCoordinate ChangePrecision() => (Altitude.ChangePrecision(), Azimuth.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.HorizontalCoordinate(HorizontalCoordinate self) => self.ChangePrecision();
        public static implicit operator (Angle, Angle)(HorizontalCoordinate self) => (self.Altitude, self.Azimuth);
        public static implicit operator HorizontalCoordinate((Angle, Angle) value) => new HorizontalCoordinate(value.Item1, value.Item2);
        public void Deconstruct(out Angle altitude, out Angle azimuth) { altitude = Altitude; azimuth = Azimuth; }
        public override bool Equals(object obj) { if (!(obj is HorizontalCoordinate)) return false; var other = (HorizontalCoordinate)obj; return Altitude.Equals(other.Altitude) && Azimuth.Equals(other.Azimuth); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Altitude, Azimuth);
        public override string ToString() => $"{{ \"Altitude\" = {Altitude}, \"Azimuth\" = {Azimuth} }}";
        public static implicit operator Dynamic(HorizontalCoordinate self) => new Dynamic(self);
        public static implicit operator HorizontalCoordinate(Dynamic value) => value.As<HorizontalCoordinate>();
        public String TypeName => "HorizontalCoordinate";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Altitude", (String)"Azimuth");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Altitude), new Dynamic(Azimuth));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((HorizontalCoordinate)b);
        // Implemented concept functions and type functions
        public IArray<HorizontalCoordinate> Repeat(Integer n){
            var _var392 = this;
            return n.MapRange((i) => _var392);
        }
        public Boolean Equals(HorizontalCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(HorizontalCoordinate a, HorizontalCoordinate b) => a.Equals(b);
        public Boolean NotEquals(HorizontalCoordinate b) => this.Equals(b).Not;
        public static Boolean operator !=(HorizontalCoordinate a, HorizontalCoordinate b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct GeoCoordinate: ICoordinate<GeoCoordinate>
    {
        [DataMember] public readonly Angle Latitude;
        [DataMember] public readonly Angle Longitude;
        public GeoCoordinate WithLatitude(Angle latitude) => new GeoCoordinate(latitude, Longitude);
        public GeoCoordinate WithLongitude(Angle longitude) => new GeoCoordinate(Latitude, longitude);
        public GeoCoordinate(Angle latitude, Angle longitude) => (Latitude, Longitude) = (latitude, longitude);
        public static GeoCoordinate Default = new GeoCoordinate();
        public static GeoCoordinate New(Angle latitude, Angle longitude) => new GeoCoordinate(latitude, longitude);
        public Plato.SinglePrecision.GeoCoordinate ChangePrecision() => (Latitude.ChangePrecision(), Longitude.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.GeoCoordinate(GeoCoordinate self) => self.ChangePrecision();
        public static implicit operator (Angle, Angle)(GeoCoordinate self) => (self.Latitude, self.Longitude);
        public static implicit operator GeoCoordinate((Angle, Angle) value) => new GeoCoordinate(value.Item1, value.Item2);
        public void Deconstruct(out Angle latitude, out Angle longitude) { latitude = Latitude; longitude = Longitude; }
        public override bool Equals(object obj) { if (!(obj is GeoCoordinate)) return false; var other = (GeoCoordinate)obj; return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Latitude, Longitude);
        public override string ToString() => $"{{ \"Latitude\" = {Latitude}, \"Longitude\" = {Longitude} }}";
        public static implicit operator Dynamic(GeoCoordinate self) => new Dynamic(self);
        public static implicit operator GeoCoordinate(Dynamic value) => value.As<GeoCoordinate>();
        public String TypeName => "GeoCoordinate";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Latitude", (String)"Longitude");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Latitude), new Dynamic(Longitude));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((GeoCoordinate)b);
        // Implemented concept functions and type functions
        public IArray<GeoCoordinate> Repeat(Integer n){
            var _var393 = this;
            return n.MapRange((i) => _var393);
        }
        public Boolean Equals(GeoCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(GeoCoordinate a, GeoCoordinate b) => a.Equals(b);
        public Boolean NotEquals(GeoCoordinate b) => this.Equals(b).Not;
        public static Boolean operator !=(GeoCoordinate a, GeoCoordinate b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct GeoCoordinateWithAltitude: ICoordinate<GeoCoordinateWithAltitude>
    {
        [DataMember] public readonly GeoCoordinate ICoordinate;
        [DataMember] public readonly Number Altitude;
        public GeoCoordinateWithAltitude WithICoordinate(GeoCoordinate iCoordinate) => new GeoCoordinateWithAltitude(iCoordinate, Altitude);
        public GeoCoordinateWithAltitude WithAltitude(Number altitude) => new GeoCoordinateWithAltitude(ICoordinate, altitude);
        public GeoCoordinateWithAltitude(GeoCoordinate iCoordinate, Number altitude) => (ICoordinate, Altitude) = (iCoordinate, altitude);
        public static GeoCoordinateWithAltitude Default = new GeoCoordinateWithAltitude();
        public static GeoCoordinateWithAltitude New(GeoCoordinate iCoordinate, Number altitude) => new GeoCoordinateWithAltitude(iCoordinate, altitude);
        public Plato.SinglePrecision.GeoCoordinateWithAltitude ChangePrecision() => (ICoordinate.ChangePrecision(), Altitude.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.GeoCoordinateWithAltitude(GeoCoordinateWithAltitude self) => self.ChangePrecision();
        public static implicit operator (GeoCoordinate, Number)(GeoCoordinateWithAltitude self) => (self.ICoordinate, self.Altitude);
        public static implicit operator GeoCoordinateWithAltitude((GeoCoordinate, Number) value) => new GeoCoordinateWithAltitude(value.Item1, value.Item2);
        public void Deconstruct(out GeoCoordinate iCoordinate, out Number altitude) { iCoordinate = ICoordinate; altitude = Altitude; }
        public override bool Equals(object obj) { if (!(obj is GeoCoordinateWithAltitude)) return false; var other = (GeoCoordinateWithAltitude)obj; return ICoordinate.Equals(other.ICoordinate) && Altitude.Equals(other.Altitude); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(ICoordinate, Altitude);
        public override string ToString() => $"{{ \"ICoordinate\" = {ICoordinate}, \"Altitude\" = {Altitude} }}";
        public static implicit operator Dynamic(GeoCoordinateWithAltitude self) => new Dynamic(self);
        public static implicit operator GeoCoordinateWithAltitude(Dynamic value) => value.As<GeoCoordinateWithAltitude>();
        public String TypeName => "GeoCoordinateWithAltitude";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"ICoordinate", (String)"Altitude");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(ICoordinate), new Dynamic(Altitude));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((GeoCoordinateWithAltitude)b);
        // Implemented concept functions and type functions
        public IArray<GeoCoordinateWithAltitude> Repeat(Integer n){
            var _var394 = this;
            return n.MapRange((i) => _var394);
        }
        public Boolean Equals(GeoCoordinateWithAltitude b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(GeoCoordinateWithAltitude a, GeoCoordinateWithAltitude b) => a.Equals(b);
        public Boolean NotEquals(GeoCoordinateWithAltitude b) => this.Equals(b).Not;
        public static Boolean operator !=(GeoCoordinateWithAltitude a, GeoCoordinateWithAltitude b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Rational: IValue<Rational>
    {
        [DataMember] public readonly Integer Numerator;
        [DataMember] public readonly Integer Denominator;
        public Rational WithNumerator(Integer numerator) => new Rational(numerator, Denominator);
        public Rational WithDenominator(Integer denominator) => new Rational(Numerator, denominator);
        public Rational(Integer numerator, Integer denominator) => (Numerator, Denominator) = (numerator, denominator);
        public static Rational Default = new Rational();
        public static Rational New(Integer numerator, Integer denominator) => new Rational(numerator, denominator);
        public Plato.SinglePrecision.Rational ChangePrecision() => (Numerator.ChangePrecision(), Denominator.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Rational(Rational self) => self.ChangePrecision();
        public static implicit operator (Integer, Integer)(Rational self) => (self.Numerator, self.Denominator);
        public static implicit operator Rational((Integer, Integer) value) => new Rational(value.Item1, value.Item2);
        public void Deconstruct(out Integer numerator, out Integer denominator) { numerator = Numerator; denominator = Denominator; }
        public override bool Equals(object obj) { if (!(obj is Rational)) return false; var other = (Rational)obj; return Numerator.Equals(other.Numerator) && Denominator.Equals(other.Denominator); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Numerator, Denominator);
        public override string ToString() => $"{{ \"Numerator\" = {Numerator}, \"Denominator\" = {Denominator} }}";
        public static implicit operator Dynamic(Rational self) => new Dynamic(self);
        public static implicit operator Rational(Dynamic value) => value.As<Rational>();
        public String TypeName => "Rational";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Numerator", (String)"Denominator");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Numerator), new Dynamic(Denominator));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Rational)b);
        // Implemented concept functions and type functions
        public IArray<Rational> Repeat(Integer n){
            var _var395 = this;
            return n.MapRange((i) => _var395);
        }
        public Boolean Equals(Rational b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Rational a, Rational b) => a.Equals(b);
        public Boolean NotEquals(Rational b) => this.Equals(b).Not;
        public static Boolean operator !=(Rational a, Rational b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Fraction: IValue<Fraction>
    {
        [DataMember] public readonly Number Numerator;
        [DataMember] public readonly Number Denominator;
        public Fraction WithNumerator(Number numerator) => new Fraction(numerator, Denominator);
        public Fraction WithDenominator(Number denominator) => new Fraction(Numerator, denominator);
        public Fraction(Number numerator, Number denominator) => (Numerator, Denominator) = (numerator, denominator);
        public static Fraction Default = new Fraction();
        public static Fraction New(Number numerator, Number denominator) => new Fraction(numerator, denominator);
        public Plato.SinglePrecision.Fraction ChangePrecision() => (Numerator.ChangePrecision(), Denominator.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Fraction(Fraction self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Fraction self) => (self.Numerator, self.Denominator);
        public static implicit operator Fraction((Number, Number) value) => new Fraction(value.Item1, value.Item2);
        public void Deconstruct(out Number numerator, out Number denominator) { numerator = Numerator; denominator = Denominator; }
        public override bool Equals(object obj) { if (!(obj is Fraction)) return false; var other = (Fraction)obj; return Numerator.Equals(other.Numerator) && Denominator.Equals(other.Denominator); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Numerator, Denominator);
        public override string ToString() => $"{{ \"Numerator\" = {Numerator}, \"Denominator\" = {Denominator} }}";
        public static implicit operator Dynamic(Fraction self) => new Dynamic(self);
        public static implicit operator Fraction(Dynamic value) => value.As<Fraction>();
        public String TypeName => "Fraction";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Numerator", (String)"Denominator");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Numerator), new Dynamic(Denominator));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Fraction)b);
        // Implemented concept functions and type functions
        public IArray<Fraction> Repeat(Integer n){
            var _var396 = this;
            return n.MapRange((i) => _var396);
        }
        public Boolean Equals(Fraction b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Fraction a, Fraction b) => a.Equals(b);
        public Boolean NotEquals(Fraction b) => this.Equals(b).Not;
        public static Boolean operator !=(Fraction a, Fraction b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Angle: IMeasure<Angle>
    {
        [DataMember] public readonly Number Radians;
        public Angle WithRadians(Number radians) => new Angle(radians);
        public Angle(Number radians) => (Radians) = (radians);
        public static Angle Default = new Angle();
        public static Angle New(Number radians) => new Angle(radians);
        public Plato.SinglePrecision.Angle ChangePrecision() => (Radians.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Angle(Angle self) => self.ChangePrecision();
        public static implicit operator Number(Angle self) => self.Radians;
        public static implicit operator Angle(Number value) => new Angle(value);
        public static implicit operator Angle(Integer value) => new Angle(value);
        public static implicit operator Angle(int value) => new Integer(value);
        public static implicit operator Angle(double value) => new Number(value);
        public static implicit operator double(Angle value) => value.Radians;
        public override bool Equals(object obj) { if (!(obj is Angle)) return false; var other = (Angle)obj; return Radians.Equals(other.Radians); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radians);
        public override string ToString() => $"{{ \"Radians\" = {Radians} }}";
        public static implicit operator Dynamic(Angle self) => new Dynamic(self);
        public static implicit operator Angle(Dynamic value) => value.As<Angle>();
        public String TypeName => "Angle";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radians");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radians));
        Number INumberLike.ToNumber => this.ToNumber;
        INumberLike INumberLike.FromNumber(Number n) => this.FromNumber((Number)n);
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Angle)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Angle)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Angle)b);
        IAdditive IAdditive.Negative => this.Negative;
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Angle)y);
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(Radians);
        public Angle FromComponents(IArray<Number> numbers) => new Angle(numbers[0]);
        // Implemented concept functions and type functions
        public Number Turns => this.Radians.Divide(Constants.TwoPi);
        public Number Degrees => this.Turns.Multiply(((Number)360));
        public Number Gradians => this.Turns.Multiply(((Number)400));
        public Number Sec => this.Cos.Reciprocal;
        public Number Csc => this.Sin.Reciprocal;
        public Number Cot => this.Tan.Reciprocal;
        public Vector2D UnitCircle => this.Cos.Tuple2(this.Sin);
        public Vector2D Circle(Vector2D center, Number radius) => this.UnitCircle.Multiply(radius).Add(center);
        public Vector2D Ellipse(Vector2D center, Vector2D size) => this.UnitCircle.Multiply(size).Add(center);
        public Vector2D Epicycloid(Number R, Number r) => R.Add(r).Multiply(this.Cos).Subtract(r.Multiply(R.Add(r).Multiply(this.Divide(r).Cos))).Tuple2(R.Add(r).Multiply(this.Sin).Subtract(r.Multiply(R.Add(r).Multiply(this.Divide(r).Sin))));
        public Vector2D Hypocycloid(Number R, Number r) => R.Subtract(r).Multiply(this.Cos).Add(r.Multiply(R.Subtract(r).Multiply(this.Divide(r).Cos))).Tuple2(R.Subtract(r).Multiply(this.Sin).Subtract(r.Multiply(R.Subtract(r).Multiply(this.Divide(r).Sin))));
        public Vector2D Epitrochoid(Number R, Number r, Number d) => R.Add(r).Multiply(this.Cos).Subtract(d.Multiply(R.Add(r).Multiply(this.Divide(r).Cos))).Tuple2(R.Add(r).Multiply(this.Sin).Subtract(d.Multiply(R.Add(r).Multiply(this.Divide(r).Sin))));
        public Vector2D Hypotrochoid(Number R, Number r, Number d) => R.Subtract(r).Multiply(this.Cos).Add(d.Multiply(R.Subtract(r).Multiply(this.Divide(r).Cos))).Tuple2(R.Subtract(r).Multiply(this.Sin).Subtract(d.Multiply(R.Subtract(r).Multiply(this.Divide(r).Sin))));
        public Vector2D ButterflyCurve => this.Divide(((Number)6)).ButterflyCurveSection;
        public Vector2D ButterflyCurveSection => this.Multiply(this.Cos.Exp.Subtract(((Number)2).Multiply(this.Multiply(((Number)4)).Cos).Subtract(this.Divide(((Number)12)).Sin.Pow(((Number)5))))).Sin.Tuple2(this.Multiply(this.Cos.Exp.Subtract(((Number)2).Multiply(this.Multiply(((Number)4)).Cos).Subtract(this.Divide(((Number)12)).Sin.Pow(((Number)5))))).Cos);
        public Vector2D Lissajous(Number a, Number b, Angle d) => this.Add(d).Sin.Tuple2(b.Sin);
        public Number CycloidOfCeva => ((Number)1).Add(this.Multiply(((Number)2)).Cos.Multiply(((Number)2)));
        public Number Limacon(Number a, Number b) => a.Multiply(this.Cos).Add(b);
        public Number Cardoid => ((Number)1).Add(this.Cos);
        public Number TschirnhausenCubic(Number a) => a.Multiply(this.Divide(((Number)3)).Sec.Cube);
        public Number Rose(Number k) => k.Multiply(this.Cos);
        public Number ArchimedeanSpiral(Number a, Number b) => a.Add(b.Multiply(this.Turns));
        public Number ConicSection(Number semiLatusRectum, Number eccentricity) => semiLatusRectum.Divide(((Number)1).Subtract(eccentricity.Multiply(this.Cos)));
        public Number LemniscateOfBernoulli(Number a) => a.Sqr.Multiply(this.Multiply(((Number)2)).Cos).Sqrt;
        public Number TrisectrixOfMaclaurin(Number a) => ((Number)2).Multiply(a).Divide(this.Divide(((Number)3)).Cos);
        public Number ConchoidOfDeSluze(Number a) => this.Sec.Add(a.Multiply(this.Cos));
        public Number SinusoidalSpiral(Number a, Number n) => a.Pow(n).Multiply(this.Multiply(n).Cos).InversePow(a);
        public Number FermatsSpiral(Number a) => a.Multiply(this.Turns.Sqr).Sqrt;
        public Number LogarithmicSpiral(Number a, Number k) => a.Multiply(this.Radians.Multiply(k).Exp);
        public Vector3D TorusKnot(Number p, Number q){
            var r = this.Multiply(q).Cos.Add(((Number)2));
            var x = r.Multiply(this.Multiply(p).Cos);
            var y = r.Multiply(this.Multiply(p).Sin);
            var z = this.Multiply(q).Sin.Negative;
            return x.Tuple3(y, z);
        }
        public Vector3D TrefoilKnot => this.Sin.Add(this.Multiply(((Number)2)).Sin.Multiply(((Number)2))).Tuple3(this.Cos.Add(this.Multiply(((Number)2)).Cos.Multiply(((Number)2))), this.Multiply(((Number)3)).Sin.Negative);
        public Vector3D FigureEightKnot => ((Number)2).Add(this.Multiply(((Number)2)).Cos).Multiply(this.Multiply(((Number)3)).Cos).Tuple3(((Number)2).Add(this.Multiply(((Number)2)).Cos).Multiply(this.Multiply(((Number)3)).Sin), this.Multiply(((Number)4)).Sin);
        public Vector3D Helix(Number revs) => this.Multiply(revs).Sin.Tuple3(this.Multiply(revs).Cos, this.Turns);
        public Number Cos => Intrinsics.Cos(this);
        public Number Sin => Intrinsics.Sin(this);
        public Number Tan => Intrinsics.Tan(this);
        public Quaternion XRotation => Constants.XAxis3D.Rotation(this);
        public Quaternion YRotation => Constants.YAxis3D.Rotation(this);
        public Quaternion ZRotation => Constants.ZAxis3D.Rotation(this);
        public Angle Multiply(Angle y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Angle operator *(Angle x, Angle y) => x.Multiply(y);
        public Angle Divide(Angle y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Angle operator /(Angle x, Angle y) => x.Divide(y);
        public Angle Modulo(Angle y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public static Angle operator %(Angle x, Angle y) => x.Modulo(y);
        public Number Number => this.ToNumber;
        public Number ToNumber => this.Component(((Integer)0));
        public Angle FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Angle b) => this.ToNumber.LessThan(b.ToNumber) ? ((Integer)1).Negative : this.ToNumber.GreaterThan(b.ToNumber) ? ((Integer)1) : ((Integer)0);
        public Angle Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
        public static Angle operator +(Angle x, Number y) => x.Add(y);
        public Angle Subract(Number y) => this.FromNumber(this.ToNumber.Subtract(y));
        public Angle PlusOne => this.Add(this.One);
        public Angle MinusOne => this.Subtract(this.One);
        public Angle FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Angle MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Angle ZipComponents(Angle y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Angle Zero => this.MapComponents((i) => ((Number)0));
        public Angle One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Angle MinValue => this.MapComponents((x) => x.MinValue);
        public Angle MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Angle a, Angle b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Angle Clamp(Angle a, Angle b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Angle ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Angle Clamp01 => this.ClampZeroOne;
        public Angle Abs => this.MapComponents((i) => i.Abs);
        public Angle Min(Angle y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Angle Max(Angle y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Angle Floor => this.MapComponents((c) => c.Floor);
        public Angle Fract => this.MapComponents((c) => c.Fract);
        public Angle Multiply(Number s){
            var _var397 = s;
            return this.MapComponents((i) => i.Multiply(_var397));
        }
        public static Angle operator *(Angle x, Number s) => x.Multiply(s);
        public Angle Divide(Number s){
            var _var398 = s;
            return this.MapComponents((i) => i.Divide(_var398));
        }
        public static Angle operator /(Angle x, Number s) => x.Divide(s);
        public Angle Modulo(Number s){
            var _var399 = s;
            return this.MapComponents((i) => i.Modulo(_var399));
        }
        public static Angle operator %(Angle x, Number s) => x.Modulo(s);
        public Angle Add(Angle y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Angle operator +(Angle x, Angle y) => x.Add(y);
        public Angle Subtract(Angle y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Angle operator -(Angle x, Angle y) => x.Subtract(y);
        public Angle Negative => this.MapComponents((a) => a.Negative);
        public static Angle operator -(Angle x) => x.Negative;
        public IArray<Angle> Repeat(Integer n){
            var _var400 = this;
            return n.MapRange((i) => _var400);
        }
        public Boolean Equals(Angle b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Angle a, Angle b) => a.Equals(b);
        public Boolean NotEquals(Angle b) => this.Equals(b).Not;
        public static Boolean operator !=(Angle a, Angle b) => a.NotEquals(b);
        public Angle Half => this.Divide(((Number)2));
        public Angle Quarter => this.Divide(((Number)4));
        public Angle Eight => this.Divide(((Number)8));
        public Angle Sixteenth => this.Divide(((Number)16));
        public Angle Tenth => this.Divide(((Number)10));
        public Angle Twice => this.Multiply(((Number)2));
        public Angle Hundred => this.Multiply(((Number)100));
        public Angle Thousand => this.Multiply(((Number)1000));
        public Angle Million => this.Thousand.Thousand;
        public Angle Billion => this.Thousand.Million;
        public Boolean LessThan(Angle b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Angle a, Angle b) => a.LessThan(b);
        public Boolean GreaterThan(Angle b) => b.LessThan(this);
        public static Boolean operator >(Angle a, Angle b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Angle b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Angle a, Angle b) => a.GreaterThanOrEquals(b);
        public Angle Lesser(Angle b) => this.LessThanOrEquals(b) ? this : b;
        public Angle Greater(Angle b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Angle b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Angle Lerp(Angle b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Angle Barycentric(Angle v2, Angle v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        // Unimplemented concept functions
        public Boolean LessThanOrEquals(Angle y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Angle x, Angle y) => x.LessThanOrEquals(y);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Length: IMeasure<Length>
    {
        [DataMember] public readonly Number Meters;
        public Length WithMeters(Number meters) => new Length(meters);
        public Length(Number meters) => (Meters) = (meters);
        public static Length Default = new Length();
        public static Length New(Number meters) => new Length(meters);
        public Plato.SinglePrecision.Length ChangePrecision() => (Meters.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Length(Length self) => self.ChangePrecision();
        public static implicit operator Number(Length self) => self.Meters;
        public static implicit operator Length(Number value) => new Length(value);
        public static implicit operator Length(Integer value) => new Length(value);
        public static implicit operator Length(int value) => new Integer(value);
        public static implicit operator Length(double value) => new Number(value);
        public static implicit operator double(Length value) => value.Meters;
        public override bool Equals(object obj) { if (!(obj is Length)) return false; var other = (Length)obj; return Meters.Equals(other.Meters); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Meters);
        public override string ToString() => $"{{ \"Meters\" = {Meters} }}";
        public static implicit operator Dynamic(Length self) => new Dynamic(self);
        public static implicit operator Length(Dynamic value) => value.As<Length>();
        public String TypeName => "Length";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Meters");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Meters));
        Number INumberLike.ToNumber => this.ToNumber;
        INumberLike INumberLike.FromNumber(Number n) => this.FromNumber((Number)n);
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Length)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Length)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Length)b);
        IAdditive IAdditive.Negative => this.Negative;
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Length)y);
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(Meters);
        public Length FromComponents(IArray<Number> numbers) => new Length(numbers[0]);
        // Implemented concept functions and type functions
        public Length Multiply(Length y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Length operator *(Length x, Length y) => x.Multiply(y);
        public Length Divide(Length y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Length operator /(Length x, Length y) => x.Divide(y);
        public Length Modulo(Length y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public static Length operator %(Length x, Length y) => x.Modulo(y);
        public Number Number => this.ToNumber;
        public Number ToNumber => this.Component(((Integer)0));
        public Length FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Length b) => this.ToNumber.LessThan(b.ToNumber) ? ((Integer)1).Negative : this.ToNumber.GreaterThan(b.ToNumber) ? ((Integer)1) : ((Integer)0);
        public Length Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
        public static Length operator +(Length x, Number y) => x.Add(y);
        public Length Subract(Number y) => this.FromNumber(this.ToNumber.Subtract(y));
        public Length PlusOne => this.Add(this.One);
        public Length MinusOne => this.Subtract(this.One);
        public Length FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Length MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Length ZipComponents(Length y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Length Zero => this.MapComponents((i) => ((Number)0));
        public Length One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Length MinValue => this.MapComponents((x) => x.MinValue);
        public Length MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Length a, Length b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Length Clamp(Length a, Length b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Length ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Length Clamp01 => this.ClampZeroOne;
        public Length Abs => this.MapComponents((i) => i.Abs);
        public Length Min(Length y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Length Max(Length y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Length Floor => this.MapComponents((c) => c.Floor);
        public Length Fract => this.MapComponents((c) => c.Fract);
        public Length Multiply(Number s){
            var _var401 = s;
            return this.MapComponents((i) => i.Multiply(_var401));
        }
        public static Length operator *(Length x, Number s) => x.Multiply(s);
        public Length Divide(Number s){
            var _var402 = s;
            return this.MapComponents((i) => i.Divide(_var402));
        }
        public static Length operator /(Length x, Number s) => x.Divide(s);
        public Length Modulo(Number s){
            var _var403 = s;
            return this.MapComponents((i) => i.Modulo(_var403));
        }
        public static Length operator %(Length x, Number s) => x.Modulo(s);
        public Length Add(Length y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Length operator +(Length x, Length y) => x.Add(y);
        public Length Subtract(Length y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Length operator -(Length x, Length y) => x.Subtract(y);
        public Length Negative => this.MapComponents((a) => a.Negative);
        public static Length operator -(Length x) => x.Negative;
        public IArray<Length> Repeat(Integer n){
            var _var404 = this;
            return n.MapRange((i) => _var404);
        }
        public Boolean Equals(Length b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Length a, Length b) => a.Equals(b);
        public Boolean NotEquals(Length b) => this.Equals(b).Not;
        public static Boolean operator !=(Length a, Length b) => a.NotEquals(b);
        public Length Half => this.Divide(((Number)2));
        public Length Quarter => this.Divide(((Number)4));
        public Length Eight => this.Divide(((Number)8));
        public Length Sixteenth => this.Divide(((Number)16));
        public Length Tenth => this.Divide(((Number)10));
        public Length Twice => this.Multiply(((Number)2));
        public Length Hundred => this.Multiply(((Number)100));
        public Length Thousand => this.Multiply(((Number)1000));
        public Length Million => this.Thousand.Thousand;
        public Length Billion => this.Thousand.Million;
        public Boolean LessThan(Length b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Length a, Length b) => a.LessThan(b);
        public Boolean GreaterThan(Length b) => b.LessThan(this);
        public static Boolean operator >(Length a, Length b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Length b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Length a, Length b) => a.GreaterThanOrEquals(b);
        public Length Lesser(Length b) => this.LessThanOrEquals(b) ? this : b;
        public Length Greater(Length b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Length b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Length Lerp(Length b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Length Barycentric(Length v2, Length v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        // Unimplemented concept functions
        public Boolean LessThanOrEquals(Length y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Length x, Length y) => x.LessThanOrEquals(y);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Mass: IMeasure<Mass>
    {
        [DataMember] public readonly Number Kilograms;
        public Mass WithKilograms(Number kilograms) => new Mass(kilograms);
        public Mass(Number kilograms) => (Kilograms) = (kilograms);
        public static Mass Default = new Mass();
        public static Mass New(Number kilograms) => new Mass(kilograms);
        public Plato.SinglePrecision.Mass ChangePrecision() => (Kilograms.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Mass(Mass self) => self.ChangePrecision();
        public static implicit operator Number(Mass self) => self.Kilograms;
        public static implicit operator Mass(Number value) => new Mass(value);
        public static implicit operator Mass(Integer value) => new Mass(value);
        public static implicit operator Mass(int value) => new Integer(value);
        public static implicit operator Mass(double value) => new Number(value);
        public static implicit operator double(Mass value) => value.Kilograms;
        public override bool Equals(object obj) { if (!(obj is Mass)) return false; var other = (Mass)obj; return Kilograms.Equals(other.Kilograms); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Kilograms);
        public override string ToString() => $"{{ \"Kilograms\" = {Kilograms} }}";
        public static implicit operator Dynamic(Mass self) => new Dynamic(self);
        public static implicit operator Mass(Dynamic value) => value.As<Mass>();
        public String TypeName => "Mass";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Kilograms");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Kilograms));
        Number INumberLike.ToNumber => this.ToNumber;
        INumberLike INumberLike.FromNumber(Number n) => this.FromNumber((Number)n);
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Mass)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Mass)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Mass)b);
        IAdditive IAdditive.Negative => this.Negative;
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Mass)y);
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(Kilograms);
        public Mass FromComponents(IArray<Number> numbers) => new Mass(numbers[0]);
        // Implemented concept functions and type functions
        public Mass Multiply(Mass y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Mass operator *(Mass x, Mass y) => x.Multiply(y);
        public Mass Divide(Mass y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Mass operator /(Mass x, Mass y) => x.Divide(y);
        public Mass Modulo(Mass y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public static Mass operator %(Mass x, Mass y) => x.Modulo(y);
        public Number Number => this.ToNumber;
        public Number ToNumber => this.Component(((Integer)0));
        public Mass FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Mass b) => this.ToNumber.LessThan(b.ToNumber) ? ((Integer)1).Negative : this.ToNumber.GreaterThan(b.ToNumber) ? ((Integer)1) : ((Integer)0);
        public Mass Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
        public static Mass operator +(Mass x, Number y) => x.Add(y);
        public Mass Subract(Number y) => this.FromNumber(this.ToNumber.Subtract(y));
        public Mass PlusOne => this.Add(this.One);
        public Mass MinusOne => this.Subtract(this.One);
        public Mass FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Mass MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Mass ZipComponents(Mass y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Mass Zero => this.MapComponents((i) => ((Number)0));
        public Mass One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Mass MinValue => this.MapComponents((x) => x.MinValue);
        public Mass MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Mass a, Mass b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Mass Clamp(Mass a, Mass b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Mass ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Mass Clamp01 => this.ClampZeroOne;
        public Mass Abs => this.MapComponents((i) => i.Abs);
        public Mass Min(Mass y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Mass Max(Mass y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Mass Floor => this.MapComponents((c) => c.Floor);
        public Mass Fract => this.MapComponents((c) => c.Fract);
        public Mass Multiply(Number s){
            var _var405 = s;
            return this.MapComponents((i) => i.Multiply(_var405));
        }
        public static Mass operator *(Mass x, Number s) => x.Multiply(s);
        public Mass Divide(Number s){
            var _var406 = s;
            return this.MapComponents((i) => i.Divide(_var406));
        }
        public static Mass operator /(Mass x, Number s) => x.Divide(s);
        public Mass Modulo(Number s){
            var _var407 = s;
            return this.MapComponents((i) => i.Modulo(_var407));
        }
        public static Mass operator %(Mass x, Number s) => x.Modulo(s);
        public Mass Add(Mass y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Mass operator +(Mass x, Mass y) => x.Add(y);
        public Mass Subtract(Mass y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Mass operator -(Mass x, Mass y) => x.Subtract(y);
        public Mass Negative => this.MapComponents((a) => a.Negative);
        public static Mass operator -(Mass x) => x.Negative;
        public IArray<Mass> Repeat(Integer n){
            var _var408 = this;
            return n.MapRange((i) => _var408);
        }
        public Boolean Equals(Mass b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Mass a, Mass b) => a.Equals(b);
        public Boolean NotEquals(Mass b) => this.Equals(b).Not;
        public static Boolean operator !=(Mass a, Mass b) => a.NotEquals(b);
        public Mass Half => this.Divide(((Number)2));
        public Mass Quarter => this.Divide(((Number)4));
        public Mass Eight => this.Divide(((Number)8));
        public Mass Sixteenth => this.Divide(((Number)16));
        public Mass Tenth => this.Divide(((Number)10));
        public Mass Twice => this.Multiply(((Number)2));
        public Mass Hundred => this.Multiply(((Number)100));
        public Mass Thousand => this.Multiply(((Number)1000));
        public Mass Million => this.Thousand.Thousand;
        public Mass Billion => this.Thousand.Million;
        public Boolean LessThan(Mass b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Mass a, Mass b) => a.LessThan(b);
        public Boolean GreaterThan(Mass b) => b.LessThan(this);
        public static Boolean operator >(Mass a, Mass b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Mass b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Mass a, Mass b) => a.GreaterThanOrEquals(b);
        public Mass Lesser(Mass b) => this.LessThanOrEquals(b) ? this : b;
        public Mass Greater(Mass b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Mass b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Mass Lerp(Mass b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Mass Barycentric(Mass v2, Mass v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        // Unimplemented concept functions
        public Boolean LessThanOrEquals(Mass y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Mass x, Mass y) => x.LessThanOrEquals(y);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Temperature: IMeasure<Temperature>
    {
        [DataMember] public readonly Number Celsius;
        public Temperature WithCelsius(Number celsius) => new Temperature(celsius);
        public Temperature(Number celsius) => (Celsius) = (celsius);
        public static Temperature Default = new Temperature();
        public static Temperature New(Number celsius) => new Temperature(celsius);
        public Plato.SinglePrecision.Temperature ChangePrecision() => (Celsius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Temperature(Temperature self) => self.ChangePrecision();
        public static implicit operator Number(Temperature self) => self.Celsius;
        public static implicit operator Temperature(Number value) => new Temperature(value);
        public static implicit operator Temperature(Integer value) => new Temperature(value);
        public static implicit operator Temperature(int value) => new Integer(value);
        public static implicit operator Temperature(double value) => new Number(value);
        public static implicit operator double(Temperature value) => value.Celsius;
        public override bool Equals(object obj) { if (!(obj is Temperature)) return false; var other = (Temperature)obj; return Celsius.Equals(other.Celsius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Celsius);
        public override string ToString() => $"{{ \"Celsius\" = {Celsius} }}";
        public static implicit operator Dynamic(Temperature self) => new Dynamic(self);
        public static implicit operator Temperature(Dynamic value) => value.As<Temperature>();
        public String TypeName => "Temperature";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Celsius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Celsius));
        Number INumberLike.ToNumber => this.ToNumber;
        INumberLike INumberLike.FromNumber(Number n) => this.FromNumber((Number)n);
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Temperature)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Temperature)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Temperature)b);
        IAdditive IAdditive.Negative => this.Negative;
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Temperature)y);
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(Celsius);
        public Temperature FromComponents(IArray<Number> numbers) => new Temperature(numbers[0]);
        // Implemented concept functions and type functions
        public Temperature Multiply(Temperature y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Temperature operator *(Temperature x, Temperature y) => x.Multiply(y);
        public Temperature Divide(Temperature y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Temperature operator /(Temperature x, Temperature y) => x.Divide(y);
        public Temperature Modulo(Temperature y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public static Temperature operator %(Temperature x, Temperature y) => x.Modulo(y);
        public Number Number => this.ToNumber;
        public Number ToNumber => this.Component(((Integer)0));
        public Temperature FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Temperature b) => this.ToNumber.LessThan(b.ToNumber) ? ((Integer)1).Negative : this.ToNumber.GreaterThan(b.ToNumber) ? ((Integer)1) : ((Integer)0);
        public Temperature Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
        public static Temperature operator +(Temperature x, Number y) => x.Add(y);
        public Temperature Subract(Number y) => this.FromNumber(this.ToNumber.Subtract(y));
        public Temperature PlusOne => this.Add(this.One);
        public Temperature MinusOne => this.Subtract(this.One);
        public Temperature FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Temperature MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Temperature ZipComponents(Temperature y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Temperature Zero => this.MapComponents((i) => ((Number)0));
        public Temperature One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Temperature MinValue => this.MapComponents((x) => x.MinValue);
        public Temperature MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Temperature a, Temperature b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Temperature Clamp(Temperature a, Temperature b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Temperature ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Temperature Clamp01 => this.ClampZeroOne;
        public Temperature Abs => this.MapComponents((i) => i.Abs);
        public Temperature Min(Temperature y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Temperature Max(Temperature y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Temperature Floor => this.MapComponents((c) => c.Floor);
        public Temperature Fract => this.MapComponents((c) => c.Fract);
        public Temperature Multiply(Number s){
            var _var409 = s;
            return this.MapComponents((i) => i.Multiply(_var409));
        }
        public static Temperature operator *(Temperature x, Number s) => x.Multiply(s);
        public Temperature Divide(Number s){
            var _var410 = s;
            return this.MapComponents((i) => i.Divide(_var410));
        }
        public static Temperature operator /(Temperature x, Number s) => x.Divide(s);
        public Temperature Modulo(Number s){
            var _var411 = s;
            return this.MapComponents((i) => i.Modulo(_var411));
        }
        public static Temperature operator %(Temperature x, Number s) => x.Modulo(s);
        public Temperature Add(Temperature y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Temperature operator +(Temperature x, Temperature y) => x.Add(y);
        public Temperature Subtract(Temperature y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Temperature operator -(Temperature x, Temperature y) => x.Subtract(y);
        public Temperature Negative => this.MapComponents((a) => a.Negative);
        public static Temperature operator -(Temperature x) => x.Negative;
        public IArray<Temperature> Repeat(Integer n){
            var _var412 = this;
            return n.MapRange((i) => _var412);
        }
        public Boolean Equals(Temperature b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Temperature a, Temperature b) => a.Equals(b);
        public Boolean NotEquals(Temperature b) => this.Equals(b).Not;
        public static Boolean operator !=(Temperature a, Temperature b) => a.NotEquals(b);
        public Temperature Half => this.Divide(((Number)2));
        public Temperature Quarter => this.Divide(((Number)4));
        public Temperature Eight => this.Divide(((Number)8));
        public Temperature Sixteenth => this.Divide(((Number)16));
        public Temperature Tenth => this.Divide(((Number)10));
        public Temperature Twice => this.Multiply(((Number)2));
        public Temperature Hundred => this.Multiply(((Number)100));
        public Temperature Thousand => this.Multiply(((Number)1000));
        public Temperature Million => this.Thousand.Thousand;
        public Temperature Billion => this.Thousand.Million;
        public Boolean LessThan(Temperature b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Temperature a, Temperature b) => a.LessThan(b);
        public Boolean GreaterThan(Temperature b) => b.LessThan(this);
        public static Boolean operator >(Temperature a, Temperature b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Temperature b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Temperature a, Temperature b) => a.GreaterThanOrEquals(b);
        public Temperature Lesser(Temperature b) => this.LessThanOrEquals(b) ? this : b;
        public Temperature Greater(Temperature b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Temperature b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Temperature Lerp(Temperature b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Temperature Barycentric(Temperature v2, Temperature v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        // Unimplemented concept functions
        public Boolean LessThanOrEquals(Temperature y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Temperature x, Temperature y) => x.LessThanOrEquals(y);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Time: IMeasure<Time>
    {
        [DataMember] public readonly Number Seconds;
        public Time WithSeconds(Number seconds) => new Time(seconds);
        public Time(Number seconds) => (Seconds) = (seconds);
        public static Time Default = new Time();
        public static Time New(Number seconds) => new Time(seconds);
        public Plato.SinglePrecision.Time ChangePrecision() => (Seconds.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Time(Time self) => self.ChangePrecision();
        public static implicit operator Number(Time self) => self.Seconds;
        public static implicit operator Time(Number value) => new Time(value);
        public static implicit operator Time(Integer value) => new Time(value);
        public static implicit operator Time(int value) => new Integer(value);
        public static implicit operator Time(double value) => new Number(value);
        public static implicit operator double(Time value) => value.Seconds;
        public override bool Equals(object obj) { if (!(obj is Time)) return false; var other = (Time)obj; return Seconds.Equals(other.Seconds); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Seconds);
        public override string ToString() => $"{{ \"Seconds\" = {Seconds} }}";
        public static implicit operator Dynamic(Time self) => new Dynamic(self);
        public static implicit operator Time(Dynamic value) => value.As<Time>();
        public String TypeName => "Time";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Seconds");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Seconds));
        Number INumberLike.ToNumber => this.ToNumber;
        INumberLike INumberLike.FromNumber(Number n) => this.FromNumber((Number)n);
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Time)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Time)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Time)b);
        IAdditive IAdditive.Negative => this.Negative;
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Time)y);
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(Seconds);
        public Time FromComponents(IArray<Number> numbers) => new Time(numbers[0]);
        // Implemented concept functions and type functions
        public Time Multiply(Time y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Time operator *(Time x, Time y) => x.Multiply(y);
        public Time Divide(Time y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Time operator /(Time x, Time y) => x.Divide(y);
        public Time Modulo(Time y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public static Time operator %(Time x, Time y) => x.Modulo(y);
        public Number Number => this.ToNumber;
        public Number ToNumber => this.Component(((Integer)0));
        public Time FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Time b) => this.ToNumber.LessThan(b.ToNumber) ? ((Integer)1).Negative : this.ToNumber.GreaterThan(b.ToNumber) ? ((Integer)1) : ((Integer)0);
        public Time Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
        public static Time operator +(Time x, Number y) => x.Add(y);
        public Time Subract(Number y) => this.FromNumber(this.ToNumber.Subtract(y));
        public Time PlusOne => this.Add(this.One);
        public Time MinusOne => this.Subtract(this.One);
        public Time FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Time MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Time ZipComponents(Time y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Time Zero => this.MapComponents((i) => ((Number)0));
        public Time One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Time MinValue => this.MapComponents((x) => x.MinValue);
        public Time MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Time a, Time b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Time Clamp(Time a, Time b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Time ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Time Clamp01 => this.ClampZeroOne;
        public Time Abs => this.MapComponents((i) => i.Abs);
        public Time Min(Time y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Time Max(Time y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Time Floor => this.MapComponents((c) => c.Floor);
        public Time Fract => this.MapComponents((c) => c.Fract);
        public Time Multiply(Number s){
            var _var413 = s;
            return this.MapComponents((i) => i.Multiply(_var413));
        }
        public static Time operator *(Time x, Number s) => x.Multiply(s);
        public Time Divide(Number s){
            var _var414 = s;
            return this.MapComponents((i) => i.Divide(_var414));
        }
        public static Time operator /(Time x, Number s) => x.Divide(s);
        public Time Modulo(Number s){
            var _var415 = s;
            return this.MapComponents((i) => i.Modulo(_var415));
        }
        public static Time operator %(Time x, Number s) => x.Modulo(s);
        public Time Add(Time y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Time operator +(Time x, Time y) => x.Add(y);
        public Time Subtract(Time y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Time operator -(Time x, Time y) => x.Subtract(y);
        public Time Negative => this.MapComponents((a) => a.Negative);
        public static Time operator -(Time x) => x.Negative;
        public IArray<Time> Repeat(Integer n){
            var _var416 = this;
            return n.MapRange((i) => _var416);
        }
        public Boolean Equals(Time b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Time a, Time b) => a.Equals(b);
        public Boolean NotEquals(Time b) => this.Equals(b).Not;
        public static Boolean operator !=(Time a, Time b) => a.NotEquals(b);
        public Time Half => this.Divide(((Number)2));
        public Time Quarter => this.Divide(((Number)4));
        public Time Eight => this.Divide(((Number)8));
        public Time Sixteenth => this.Divide(((Number)16));
        public Time Tenth => this.Divide(((Number)10));
        public Time Twice => this.Multiply(((Number)2));
        public Time Hundred => this.Multiply(((Number)100));
        public Time Thousand => this.Multiply(((Number)1000));
        public Time Million => this.Thousand.Thousand;
        public Time Billion => this.Thousand.Million;
        public Boolean LessThan(Time b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Time a, Time b) => a.LessThan(b);
        public Boolean GreaterThan(Time b) => b.LessThan(this);
        public static Boolean operator >(Time a, Time b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Time b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Time a, Time b) => a.GreaterThanOrEquals(b);
        public Time Lesser(Time b) => this.LessThanOrEquals(b) ? this : b;
        public Time Greater(Time b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Time b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Time Lerp(Time b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Time Barycentric(Time v2, Time v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        // Unimplemented concept functions
        public Boolean LessThanOrEquals(Time y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Time x, Time y) => x.LessThanOrEquals(y);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct DateTime: ICoordinate<DateTime>
    {
        [DataMember] public readonly Number Value;
        public DateTime WithValue(Number value) => new DateTime(value);
        public DateTime(Number value) => (Value) = (value);
        public static DateTime Default = new DateTime();
        public static DateTime New(Number value) => new DateTime(value);
        public Plato.SinglePrecision.DateTime ChangePrecision() => (Value.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.DateTime(DateTime self) => self.ChangePrecision();
        public static implicit operator Number(DateTime self) => self.Value;
        public static implicit operator DateTime(Number value) => new DateTime(value);
        public static implicit operator DateTime(Integer value) => new DateTime(value);
        public static implicit operator DateTime(int value) => new Integer(value);
        public static implicit operator DateTime(double value) => new Number(value);
        public static implicit operator double(DateTime value) => value.Value;
        public override bool Equals(object obj) { if (!(obj is DateTime)) return false; var other = (DateTime)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => $"{{ \"Value\" = {Value} }}";
        public static implicit operator Dynamic(DateTime self) => new Dynamic(self);
        public static implicit operator DateTime(Dynamic value) => value.As<DateTime>();
        public String TypeName => "DateTime";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((DateTime)b);
        // Implemented concept functions and type functions
        public IArray<DateTime> Repeat(Integer n){
            var _var417 = this;
            return n.MapRange((i) => _var417);
        }
        public Boolean Equals(DateTime b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(DateTime a, DateTime b) => a.Equals(b);
        public Boolean NotEquals(DateTime b) => this.Equals(b).Not;
        public static Boolean operator !=(DateTime a, DateTime b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct AnglePair: IInterval<AnglePair, Angle>
    {
        [DataMember] public readonly Angle Start;
        [DataMember] public readonly Angle End;
        public AnglePair WithStart(Angle start) => new AnglePair(start, End);
        public AnglePair WithEnd(Angle end) => new AnglePair(Start, end);
        public AnglePair(Angle start, Angle end) => (Start, End) = (start, end);
        public static AnglePair Default = new AnglePair();
        public static AnglePair New(Angle start, Angle end) => new AnglePair(start, end);
        public Plato.SinglePrecision.AnglePair ChangePrecision() => (Start.ChangePrecision(), End.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.AnglePair(AnglePair self) => self.ChangePrecision();
        public static implicit operator (Angle, Angle)(AnglePair self) => (self.Start, self.End);
        public static implicit operator AnglePair((Angle, Angle) value) => new AnglePair(value.Item1, value.Item2);
        public void Deconstruct(out Angle start, out Angle end) { start = Start; end = End; }
        public override bool Equals(object obj) { if (!(obj is AnglePair)) return false; var other = (AnglePair)obj; return Start.Equals(other.Start) && End.Equals(other.End); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Start, End);
        public override string ToString() => $"{{ \"Start\" = {Start}, \"End\" = {End} }}";
        public static implicit operator Dynamic(AnglePair self) => new Dynamic(self);
        public static implicit operator AnglePair(Dynamic value) => value.As<AnglePair>();
        public String TypeName => "AnglePair";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Start", (String)"End");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Start), new Dynamic(End));
        Angle IInterval<AnglePair, Angle>.Start => Start;
        Angle IInterval<AnglePair, Angle>.End => End;
        Angle IInterval<Angle>.Start => this.Start;
        Angle IInterval<Angle>.End => this.End;
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((AnglePair)b);
        // Array predefined functions
        public AnglePair(IArray<Angle> xs) : this(xs[0], xs[1]) { }
        public AnglePair(Angle[] xs) : this(xs[0], xs[1]) { }
        public static AnglePair New(IArray<Angle> xs) => new AnglePair(xs);
        public static AnglePair New(Angle[] xs) => new AnglePair(xs);
        public static implicit operator Angle[](AnglePair self) => self.ToSystemArray();
        public static implicit operator Array<Angle>(AnglePair self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Angle> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Angle System.Collections.Generic.IReadOnlyList<Angle>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Angle>.Count => this.Count;
        // Implemented concept functions and type functions
        public Angle Size => this.End.Subtract(this.Start);
        public Angle Lerp(Number amount) => this.Start.Lerp(this.End, amount);
        public AnglePair Reverse => this.End.Tuple2(this.Start);
        public Angle Center => this.Lerp(((Number)0.5));
        public Boolean Contains(Angle value) => value.Between(this.Start, this.End);
        public Boolean Contains(AnglePair y) => this.Contains(y.Start).And(this.Contains(y.End));
        public Boolean Overlaps(AnglePair y) => this.Contains(y.Start).Or(this.Contains(y.End).Or(y.Contains(this.Start).Or(y.Contains(this.End))));
        public Tuple2<AnglePair, AnglePair> SplitAt(Number t) => this.Left(t).Tuple2(this.Right(t));
        public Tuple2<AnglePair, AnglePair> Split => this.SplitAt(((Number)0.5));
        public AnglePair Left(Number t) => this.Start.Tuple2(this.Lerp(t));
        public AnglePair Right(Number t) => this.Lerp(t).Tuple2(this.End);
        public AnglePair MoveTo(Angle v) => v.Tuple2(v.Add(this.Size));
        public AnglePair LeftHalf => this.Left(((Number)0.5));
        public AnglePair RightHalf => this.Right(((Number)0.5));
        public AnglePair Recenter(Angle c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public AnglePair Clamp(AnglePair y) => this.Clamp(y.Start).Tuple2(this.Clamp(y.End));
        public Angle Clamp(Angle value) => value.Clamp(this.Start, this.End);
        public IArray<Angle> LinearSpace(Integer count){
            var _var418 = this;
            return count.LinearSpace.Map((x) => _var418.Lerp(x));
        }
        public IArray<Angle> LinearSpaceExclusive(Integer count){
            var _var419 = this;
            return count.LinearSpaceExclusive.Map((x) => _var419.Lerp(x));
        }
        public IArray<Angle> GeometricSpace(Integer count){
            var _var420 = this;
            return count.GeometricSpace.Map((x) => _var420.Lerp(x));
        }
        public IArray<Angle> GeometricSpaceExclusive(Integer count){
            var _var421 = this;
            return count.GeometricSpaceExclusive.Map((x) => _var421.Lerp(x));
        }
        public AnglePair Subdivide(Number start, Number end) => this.Lerp(start).Tuple2(this.Lerp(end));
        public AnglePair Subdivide(NumberInterval subInterval) => this.Subdivide(subInterval.Start, subInterval.End);
        public IArray<AnglePair> Subdivide(Integer count){
            var _var422 = this;
            return count.Intervals.Map((i) => _var422.Subdivide(i));
        }
        public Boolean Equals(AnglePair b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(AnglePair a, AnglePair b) => a.Equals(b);
        public Boolean NotEquals(AnglePair b) => this.Equals(b).Not;
        public static Boolean operator !=(AnglePair a, AnglePair b) => a.NotEquals(b);
        public IArray<AnglePair> Repeat(Integer n){
            var _var423 = this;
            return n.MapRange((i) => _var423);
        }
        // Unimplemented concept functions
        public Integer Count => 2;
        public Angle At(Integer n) => n == 0 ? Start : n == 1 ? End : throw new System.IndexOutOfRangeException();
        public Angle this[Integer n] => n == 0 ? Start : n == 1 ? End : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct NumberInterval: IInterval<NumberInterval, Number>
    {
        [DataMember] public readonly Number Start;
        [DataMember] public readonly Number End;
        public NumberInterval WithStart(Number start) => new NumberInterval(start, End);
        public NumberInterval WithEnd(Number end) => new NumberInterval(Start, end);
        public NumberInterval(Number start, Number end) => (Start, End) = (start, end);
        public static NumberInterval Default = new NumberInterval();
        public static NumberInterval New(Number start, Number end) => new NumberInterval(start, end);
        public Plato.SinglePrecision.NumberInterval ChangePrecision() => (Start.ChangePrecision(), End.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.NumberInterval(NumberInterval self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(NumberInterval self) => (self.Start, self.End);
        public static implicit operator NumberInterval((Number, Number) value) => new NumberInterval(value.Item1, value.Item2);
        public void Deconstruct(out Number start, out Number end) { start = Start; end = End; }
        public override bool Equals(object obj) { if (!(obj is NumberInterval)) return false; var other = (NumberInterval)obj; return Start.Equals(other.Start) && End.Equals(other.End); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Start, End);
        public override string ToString() => $"{{ \"Start\" = {Start}, \"End\" = {End} }}";
        public static implicit operator Dynamic(NumberInterval self) => new Dynamic(self);
        public static implicit operator NumberInterval(Dynamic value) => value.As<NumberInterval>();
        public String TypeName => "NumberInterval";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Start", (String)"End");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Start), new Dynamic(End));
        Number IInterval<NumberInterval, Number>.Start => Start;
        Number IInterval<NumberInterval, Number>.End => End;
        Number IInterval<Number>.Start => this.Start;
        Number IInterval<Number>.End => this.End;
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((NumberInterval)b);
        // Array predefined functions
        public NumberInterval(IArray<Number> xs) : this(xs[0], xs[1]) { }
        public NumberInterval(Number[] xs) : this(xs[0], xs[1]) { }
        public static NumberInterval New(IArray<Number> xs) => new NumberInterval(xs);
        public static NumberInterval New(Number[] xs) => new NumberInterval(xs);
        public static implicit operator Number[](NumberInterval self) => self.ToSystemArray();
        public static implicit operator Array<Number>(NumberInterval self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Number> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Number System.Collections.Generic.IReadOnlyList<Number>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Number>.Count => this.Count;
        // Implemented concept functions and type functions
        public Number Size => this.End.Subtract(this.Start);
        public Number Lerp(Number amount) => this.Start.Lerp(this.End, amount);
        public NumberInterval Reverse => this.End.Tuple2(this.Start);
        public Number Center => this.Lerp(((Number)0.5));
        public Boolean Contains(Number value) => value.Between(this.Start, this.End);
        public Boolean Contains(NumberInterval y) => this.Contains(y.Start).And(this.Contains(y.End));
        public Boolean Overlaps(NumberInterval y) => this.Contains(y.Start).Or(this.Contains(y.End).Or(y.Contains(this.Start).Or(y.Contains(this.End))));
        public Tuple2<NumberInterval, NumberInterval> SplitAt(Number t) => this.Left(t).Tuple2(this.Right(t));
        public Tuple2<NumberInterval, NumberInterval> Split => this.SplitAt(((Number)0.5));
        public NumberInterval Left(Number t) => this.Start.Tuple2(this.Lerp(t));
        public NumberInterval Right(Number t) => this.Lerp(t).Tuple2(this.End);
        public NumberInterval MoveTo(Number v) => v.Tuple2(v.Add(this.Size));
        public NumberInterval LeftHalf => this.Left(((Number)0.5));
        public NumberInterval RightHalf => this.Right(((Number)0.5));
        public NumberInterval Recenter(Number c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public NumberInterval Clamp(NumberInterval y) => this.Clamp(y.Start).Tuple2(this.Clamp(y.End));
        public Number Clamp(Number value) => value.Clamp(this.Start, this.End);
        public IArray<Number> LinearSpace(Integer count){
            var _var424 = this;
            return count.LinearSpace.Map((x) => _var424.Lerp(x));
        }
        public IArray<Number> LinearSpaceExclusive(Integer count){
            var _var425 = this;
            return count.LinearSpaceExclusive.Map((x) => _var425.Lerp(x));
        }
        public IArray<Number> GeometricSpace(Integer count){
            var _var426 = this;
            return count.GeometricSpace.Map((x) => _var426.Lerp(x));
        }
        public IArray<Number> GeometricSpaceExclusive(Integer count){
            var _var427 = this;
            return count.GeometricSpaceExclusive.Map((x) => _var427.Lerp(x));
        }
        public NumberInterval Subdivide(Number start, Number end) => this.Lerp(start).Tuple2(this.Lerp(end));
        public NumberInterval Subdivide(NumberInterval subInterval) => this.Subdivide(subInterval.Start, subInterval.End);
        public IArray<NumberInterval> Subdivide(Integer count){
            var _var428 = this;
            return count.Intervals.Map((i) => _var428.Subdivide(i));
        }
        public Boolean Equals(NumberInterval b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(NumberInterval a, NumberInterval b) => a.Equals(b);
        public Boolean NotEquals(NumberInterval b) => this.Equals(b).Not;
        public static Boolean operator !=(NumberInterval a, NumberInterval b) => a.NotEquals(b);
        public IArray<NumberInterval> Repeat(Integer n){
            var _var429 = this;
            return n.MapRange((i) => _var429);
        }
        // Unimplemented concept functions
        public Integer Count => 2;
        public Number At(Integer n) => n == 0 ? Start : n == 1 ? End : throw new System.IndexOutOfRangeException();
        public Number this[Integer n] => n == 0 ? Start : n == 1 ? End : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct LinearEquation: IRealFunction
    {
        [DataMember] public readonly Number Slope;
        [DataMember] public readonly Number YIntercept;
        public LinearEquation WithSlope(Number slope) => new LinearEquation(slope, YIntercept);
        public LinearEquation WithYIntercept(Number yIntercept) => new LinearEquation(Slope, yIntercept);
        public LinearEquation(Number slope, Number yIntercept) => (Slope, YIntercept) = (slope, yIntercept);
        public static LinearEquation Default = new LinearEquation();
        public static LinearEquation New(Number slope, Number yIntercept) => new LinearEquation(slope, yIntercept);
        public Plato.SinglePrecision.LinearEquation ChangePrecision() => (Slope.ChangePrecision(), YIntercept.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.LinearEquation(LinearEquation self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(LinearEquation self) => (self.Slope, self.YIntercept);
        public static implicit operator LinearEquation((Number, Number) value) => new LinearEquation(value.Item1, value.Item2);
        public void Deconstruct(out Number slope, out Number yIntercept) { slope = Slope; yIntercept = YIntercept; }
        public override bool Equals(object obj) { if (!(obj is LinearEquation)) return false; var other = (LinearEquation)obj; return Slope.Equals(other.Slope) && YIntercept.Equals(other.YIntercept); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Slope, YIntercept);
        public override string ToString() => $"{{ \"Slope\" = {Slope}, \"YIntercept\" = {YIntercept} }}";
        public static implicit operator Dynamic(LinearEquation self) => new Dynamic(self);
        public static implicit operator LinearEquation(Dynamic value) => value.As<LinearEquation>();
        public String TypeName => "LinearEquation";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Slope", (String)"YIntercept");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Slope), new Dynamic(YIntercept));
        // Implemented concept functions and type functions
        public Number Eval(Number x) => x.Linear(this.Slope, this.YIntercept);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Quadratic: IRealFunction
    {
        [DataMember] public readonly Number A;
        [DataMember] public readonly Number B;
        [DataMember] public readonly Number C;
        public Quadratic WithA(Number a) => new Quadratic(a, B, C);
        public Quadratic WithB(Number b) => new Quadratic(A, b, C);
        public Quadratic WithC(Number c) => new Quadratic(A, B, c);
        public Quadratic(Number a, Number b, Number c) => (A, B, C) = (a, b, c);
        public static Quadratic Default = new Quadratic();
        public static Quadratic New(Number a, Number b, Number c) => new Quadratic(a, b, c);
        public Plato.SinglePrecision.Quadratic ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Quadratic(Quadratic self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number)(Quadratic self) => (self.A, self.B, self.C);
        public static implicit operator Quadratic((Number, Number, Number) value) => new Quadratic(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number a, out Number b, out Number c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is Quadratic)) return false; var other = (Quadratic)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C} }}";
        public static implicit operator Dynamic(Quadratic self) => new Dynamic(self);
        public static implicit operator Quadratic(Dynamic value) => value.As<Quadratic>();
        public String TypeName => "Quadratic";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        // Implemented concept functions and type functions
        public Number Eval(Number x) => x.Quadratic(this.A, this.B, this.C);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Cubic: IRealFunction
    {
        [DataMember] public readonly Number A;
        [DataMember] public readonly Number B;
        [DataMember] public readonly Number C;
        [DataMember] public readonly Number D;
        public Cubic WithA(Number a) => new Cubic(a, B, C, D);
        public Cubic WithB(Number b) => new Cubic(A, b, C, D);
        public Cubic WithC(Number c) => new Cubic(A, B, c, D);
        public Cubic WithD(Number d) => new Cubic(A, B, C, d);
        public Cubic(Number a, Number b, Number c, Number d) => (A, B, C, D) = (a, b, c, d);
        public static Cubic Default = new Cubic();
        public static Cubic New(Number a, Number b, Number c, Number d) => new Cubic(a, b, c, d);
        public Plato.SinglePrecision.Cubic ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision(), D.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Cubic(Cubic self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number, Number)(Cubic self) => (self.A, self.B, self.C, self.D);
        public static implicit operator Cubic((Number, Number, Number, Number) value) => new Cubic(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Number a, out Number b, out Number c, out Number d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is Cubic)) return false; var other = (Cubic)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C}, \"D\" = {D} }}";
        public static implicit operator Dynamic(Cubic self) => new Dynamic(self);
        public static implicit operator Cubic(Dynamic value) => value.As<Cubic>();
        public String TypeName => "Cubic";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        // Implemented concept functions and type functions
        public Number Eval(Number x) => x.Cubic(this.A, this.B, this.C, this.D);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Parabola: IRealFunction, IOpenShape
    {
        public static Parabola Default = new Parabola();
        public static Parabola New() => new Parabola();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Parabola self) => new Dynamic(self);
        public static implicit operator Parabola(Dynamic value) => value.As<Parabola>();
        public String TypeName => "Parabola";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        public Number Eval(Number x) => x.Parabola;
        public Boolean Closed => ((Boolean)false);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct SineWave: IRealFunction, IOpenShape
    {
        [DataMember] public readonly Number Amplitude;
        [DataMember] public readonly Number Frequency;
        [DataMember] public readonly Number Phase;
        public SineWave WithAmplitude(Number amplitude) => new SineWave(amplitude, Frequency, Phase);
        public SineWave WithFrequency(Number frequency) => new SineWave(Amplitude, frequency, Phase);
        public SineWave WithPhase(Number phase) => new SineWave(Amplitude, Frequency, phase);
        public SineWave(Number amplitude, Number frequency, Number phase) => (Amplitude, Frequency, Phase) = (amplitude, frequency, phase);
        public static SineWave Default = new SineWave();
        public static SineWave New(Number amplitude, Number frequency, Number phase) => new SineWave(amplitude, frequency, phase);
        public Plato.SinglePrecision.SineWave ChangePrecision() => (Amplitude.ChangePrecision(), Frequency.ChangePrecision(), Phase.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.SineWave(SineWave self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number)(SineWave self) => (self.Amplitude, self.Frequency, self.Phase);
        public static implicit operator SineWave((Number, Number, Number) value) => new SineWave(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number amplitude, out Number frequency, out Number phase) { amplitude = Amplitude; frequency = Frequency; phase = Phase; }
        public override bool Equals(object obj) { if (!(obj is SineWave)) return false; var other = (SineWave)obj; return Amplitude.Equals(other.Amplitude) && Frequency.Equals(other.Frequency) && Phase.Equals(other.Phase); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Amplitude, Frequency, Phase);
        public override string ToString() => $"{{ \"Amplitude\" = {Amplitude}, \"Frequency\" = {Frequency}, \"Phase\" = {Phase} }}";
        public static implicit operator Dynamic(SineWave self) => new Dynamic(self);
        public static implicit operator SineWave(Dynamic value) => value.As<SineWave>();
        public String TypeName => "SineWave";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Amplitude", (String)"Frequency", (String)"Phase");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Amplitude), new Dynamic(Frequency), new Dynamic(Phase));
        // Implemented concept functions and type functions
        public Number Eval(Number x) => x.SineWave(this.Amplitude, this.Frequency, this.Phase);
        public Boolean Closed => ((Boolean)false);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Spiral: IOpenCurve2D
    {
        [DataMember] public readonly Number Radius1;
        [DataMember] public readonly Number Radius2;
        [DataMember] public readonly Number NumTurns;
        public Spiral WithRadius1(Number radius1) => new Spiral(radius1, Radius2, NumTurns);
        public Spiral WithRadius2(Number radius2) => new Spiral(Radius1, radius2, NumTurns);
        public Spiral WithNumTurns(Number numTurns) => new Spiral(Radius1, Radius2, numTurns);
        public Spiral(Number radius1, Number radius2, Number numTurns) => (Radius1, Radius2, NumTurns) = (radius1, radius2, numTurns);
        public static Spiral Default = new Spiral();
        public static Spiral New(Number radius1, Number radius2, Number numTurns) => new Spiral(radius1, radius2, numTurns);
        public Plato.SinglePrecision.Spiral ChangePrecision() => (Radius1.ChangePrecision(), Radius2.ChangePrecision(), NumTurns.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Spiral(Spiral self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number)(Spiral self) => (self.Radius1, self.Radius2, self.NumTurns);
        public static implicit operator Spiral((Number, Number, Number) value) => new Spiral(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number radius1, out Number radius2, out Number numTurns) { radius1 = Radius1; radius2 = Radius2; numTurns = NumTurns; }
        public override bool Equals(object obj) { if (!(obj is Spiral)) return false; var other = (Spiral)obj; return Radius1.Equals(other.Radius1) && Radius2.Equals(other.Radius2) && NumTurns.Equals(other.NumTurns); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius1, Radius2, NumTurns);
        public override string ToString() => $"{{ \"Radius1\" = {Radius1}, \"Radius2\" = {Radius2}, \"NumTurns\" = {NumTurns} }}";
        public static implicit operator Dynamic(Spiral self) => new Dynamic(self);
        public static implicit operator Spiral(Dynamic value) => value.As<Spiral>();
        public String TypeName => "Spiral";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius1", (String)"Radius2", (String)"NumTurns");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius1), new Dynamic(Radius2), new Dynamic(NumTurns));
        // Implemented concept functions and type functions
        public Vector2D Eval(Number t) => t.Spiral(this.Radius1, this.Radius2, this.NumTurns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var430 = this;
            return numPoints.LinearSpace.Map((x) => _var430.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct QuadraticBezier2D: IArray<Vector2D>, IOpenCurve2D
    {
        [DataMember] public readonly Vector2D A;
        [DataMember] public readonly Vector2D B;
        [DataMember] public readonly Vector2D C;
        public QuadraticBezier2D WithA(Vector2D a) => new QuadraticBezier2D(a, B, C);
        public QuadraticBezier2D WithB(Vector2D b) => new QuadraticBezier2D(A, b, C);
        public QuadraticBezier2D WithC(Vector2D c) => new QuadraticBezier2D(A, B, c);
        public QuadraticBezier2D(Vector2D a, Vector2D b, Vector2D c) => (A, B, C) = (a, b, c);
        public static QuadraticBezier2D Default = new QuadraticBezier2D();
        public static QuadraticBezier2D New(Vector2D a, Vector2D b, Vector2D c) => new QuadraticBezier2D(a, b, c);
        public Plato.SinglePrecision.QuadraticBezier2D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.QuadraticBezier2D(QuadraticBezier2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Vector2D, Vector2D)(QuadraticBezier2D self) => (self.A, self.B, self.C);
        public static implicit operator QuadraticBezier2D((Vector2D, Vector2D, Vector2D) value) => new QuadraticBezier2D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D a, out Vector2D b, out Vector2D c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is QuadraticBezier2D)) return false; var other = (QuadraticBezier2D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C} }}";
        public static implicit operator Dynamic(QuadraticBezier2D self) => new Dynamic(self);
        public static implicit operator QuadraticBezier2D(Dynamic value) => value.As<QuadraticBezier2D>();
        public String TypeName => "QuadraticBezier2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        // Array predefined functions
        public QuadraticBezier2D(IArray<Vector2D> xs) : this(xs[0], xs[1], xs[2]) { }
        public QuadraticBezier2D(Vector2D[] xs) : this(xs[0], xs[1], xs[2]) { }
        public static QuadraticBezier2D New(IArray<Vector2D> xs) => new QuadraticBezier2D(xs);
        public static QuadraticBezier2D New(Vector2D[] xs) => new QuadraticBezier2D(xs);
        public static implicit operator Vector2D[](QuadraticBezier2D self) => self.ToSystemArray();
        public static implicit operator Array<Vector2D>(QuadraticBezier2D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector2D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector2D System.Collections.Generic.IReadOnlyList<Vector2D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector2D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Vector2D Eval(Number t) => this.A.QuadraticBezier(this.B, this.C, t);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var431 = this;
            return numPoints.LinearSpace.Map((x) => _var431.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct CubicBezier2D: IArray<Vector2D>, IOpenCurve2D
    {
        [DataMember] public readonly Vector2D A;
        [DataMember] public readonly Vector2D B;
        [DataMember] public readonly Vector2D C;
        [DataMember] public readonly Vector2D D;
        public CubicBezier2D WithA(Vector2D a) => new CubicBezier2D(a, B, C, D);
        public CubicBezier2D WithB(Vector2D b) => new CubicBezier2D(A, b, C, D);
        public CubicBezier2D WithC(Vector2D c) => new CubicBezier2D(A, B, c, D);
        public CubicBezier2D WithD(Vector2D d) => new CubicBezier2D(A, B, C, d);
        public CubicBezier2D(Vector2D a, Vector2D b, Vector2D c, Vector2D d) => (A, B, C, D) = (a, b, c, d);
        public static CubicBezier2D Default = new CubicBezier2D();
        public static CubicBezier2D New(Vector2D a, Vector2D b, Vector2D c, Vector2D d) => new CubicBezier2D(a, b, c, d);
        public Plato.SinglePrecision.CubicBezier2D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision(), D.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.CubicBezier2D(CubicBezier2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Vector2D, Vector2D, Vector2D)(CubicBezier2D self) => (self.A, self.B, self.C, self.D);
        public static implicit operator CubicBezier2D((Vector2D, Vector2D, Vector2D, Vector2D) value) => new CubicBezier2D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector2D a, out Vector2D b, out Vector2D c, out Vector2D d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is CubicBezier2D)) return false; var other = (CubicBezier2D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C}, \"D\" = {D} }}";
        public static implicit operator Dynamic(CubicBezier2D self) => new Dynamic(self);
        public static implicit operator CubicBezier2D(Dynamic value) => value.As<CubicBezier2D>();
        public String TypeName => "CubicBezier2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        // Array predefined functions
        public CubicBezier2D(IArray<Vector2D> xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public CubicBezier2D(Vector2D[] xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public static CubicBezier2D New(IArray<Vector2D> xs) => new CubicBezier2D(xs);
        public static CubicBezier2D New(Vector2D[] xs) => new CubicBezier2D(xs);
        public static implicit operator Vector2D[](CubicBezier2D self) => self.ToSystemArray();
        public static implicit operator Array<Vector2D>(CubicBezier2D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector2D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector2D System.Collections.Generic.IReadOnlyList<Vector2D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector2D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Vector2D Eval(Number t) => this.A.QuadraticBezier(this.B, this.C, t);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var432 = this;
            return numPoints.LinearSpace.Map((x) => _var432.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Circle: IAngularCurve2D, IClosedCurve2D
    {
        [DataMember] public readonly Vector2D Center;
        [DataMember] public readonly Number Radius;
        public Circle WithCenter(Vector2D center) => new Circle(center, Radius);
        public Circle WithRadius(Number radius) => new Circle(Center, radius);
        public Circle(Vector2D center, Number radius) => (Center, Radius) = (center, radius);
        public static Circle Default = new Circle();
        public static Circle New(Vector2D center, Number radius) => new Circle(center, radius);
        public Plato.SinglePrecision.Circle ChangePrecision() => (Center.ChangePrecision(), Radius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Circle(Circle self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Number)(Circle self) => (self.Center, self.Radius);
        public static implicit operator Circle((Vector2D, Number) value) => new Circle(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D center, out Number radius) { center = Center; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is Circle)) return false; var other = (Circle)obj; return Center.Equals(other.Center) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Radius);
        public override string ToString() => $"{{ \"Center\" = {Center}, \"Radius\" = {Radius} }}";
        public static implicit operator Dynamic(Circle self) => new Dynamic(self);
        public static implicit operator Circle(Dynamic value) => value.As<Circle>();
        public String TypeName => "Circle";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Radius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Radius));
        // Implemented concept functions and type functions
        public Vector2D GetPoint(Angle t) => t.Circle(this.Center, this.Radius);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var433 = this;
            return numPoints.LinearSpace.Map((x) => _var433.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Ellipse: IAngularCurve2D, IClosedCurve2D
    {
        [DataMember] public readonly Vector2D Center;
        [DataMember] public readonly Vector2D Size;
        public Ellipse WithCenter(Vector2D center) => new Ellipse(center, Size);
        public Ellipse WithSize(Vector2D size) => new Ellipse(Center, size);
        public Ellipse(Vector2D center, Vector2D size) => (Center, Size) = (center, size);
        public static Ellipse Default = new Ellipse();
        public static Ellipse New(Vector2D center, Vector2D size) => new Ellipse(center, size);
        public Plato.SinglePrecision.Ellipse ChangePrecision() => (Center.ChangePrecision(), Size.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Ellipse(Ellipse self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Vector2D)(Ellipse self) => (self.Center, self.Size);
        public static implicit operator Ellipse((Vector2D, Vector2D) value) => new Ellipse(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D center, out Vector2D size) { center = Center; size = Size; }
        public override bool Equals(object obj) { if (!(obj is Ellipse)) return false; var other = (Ellipse)obj; return Center.Equals(other.Center) && Size.Equals(other.Size); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Size);
        public override string ToString() => $"{{ \"Center\" = {Center}, \"Size\" = {Size} }}";
        public static implicit operator Dynamic(Ellipse self) => new Dynamic(self);
        public static implicit operator Ellipse(Dynamic value) => value.As<Ellipse>();
        public String TypeName => "Ellipse";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Size");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Size));
        // Implemented concept functions and type functions
        public Vector2D GetPoint(Angle t) => t.UnitCircle.Multiply(this.Size).Add(this.Center);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var434 = this;
            return numPoints.LinearSpace.Map((x) => _var434.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Epicycloid: IAngularCurve2D, IOpenShape
    {
        [DataMember] public readonly Number Radius1;
        [DataMember] public readonly Number Radius2;
        public Epicycloid WithRadius1(Number radius1) => new Epicycloid(radius1, Radius2);
        public Epicycloid WithRadius2(Number radius2) => new Epicycloid(Radius1, radius2);
        public Epicycloid(Number radius1, Number radius2) => (Radius1, Radius2) = (radius1, radius2);
        public static Epicycloid Default = new Epicycloid();
        public static Epicycloid New(Number radius1, Number radius2) => new Epicycloid(radius1, radius2);
        public Plato.SinglePrecision.Epicycloid ChangePrecision() => (Radius1.ChangePrecision(), Radius2.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Epicycloid(Epicycloid self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Epicycloid self) => (self.Radius1, self.Radius2);
        public static implicit operator Epicycloid((Number, Number) value) => new Epicycloid(value.Item1, value.Item2);
        public void Deconstruct(out Number radius1, out Number radius2) { radius1 = Radius1; radius2 = Radius2; }
        public override bool Equals(object obj) { if (!(obj is Epicycloid)) return false; var other = (Epicycloid)obj; return Radius1.Equals(other.Radius1) && Radius2.Equals(other.Radius2); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius1, Radius2);
        public override string ToString() => $"{{ \"Radius1\" = {Radius1}, \"Radius2\" = {Radius2} }}";
        public static implicit operator Dynamic(Epicycloid self) => new Dynamic(self);
        public static implicit operator Epicycloid(Dynamic value) => value.As<Epicycloid>();
        public String TypeName => "Epicycloid";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius1", (String)"Radius2");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius1), new Dynamic(Radius2));
        // Implemented concept functions and type functions
        public Vector2D GetPoint(Angle t) => t.Epicycloid(this.Radius1, this.Radius2);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var435 = this;
            return numPoints.LinearSpace.Map((x) => _var435.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Hypocycloid: IAngularCurve2D, IOpenShape
    {
        [DataMember] public readonly Number Radius1;
        [DataMember] public readonly Number Radius2;
        public Hypocycloid WithRadius1(Number radius1) => new Hypocycloid(radius1, Radius2);
        public Hypocycloid WithRadius2(Number radius2) => new Hypocycloid(Radius1, radius2);
        public Hypocycloid(Number radius1, Number radius2) => (Radius1, Radius2) = (radius1, radius2);
        public static Hypocycloid Default = new Hypocycloid();
        public static Hypocycloid New(Number radius1, Number radius2) => new Hypocycloid(radius1, radius2);
        public Plato.SinglePrecision.Hypocycloid ChangePrecision() => (Radius1.ChangePrecision(), Radius2.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Hypocycloid(Hypocycloid self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Hypocycloid self) => (self.Radius1, self.Radius2);
        public static implicit operator Hypocycloid((Number, Number) value) => new Hypocycloid(value.Item1, value.Item2);
        public void Deconstruct(out Number radius1, out Number radius2) { radius1 = Radius1; radius2 = Radius2; }
        public override bool Equals(object obj) { if (!(obj is Hypocycloid)) return false; var other = (Hypocycloid)obj; return Radius1.Equals(other.Radius1) && Radius2.Equals(other.Radius2); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius1, Radius2);
        public override string ToString() => $"{{ \"Radius1\" = {Radius1}, \"Radius2\" = {Radius2} }}";
        public static implicit operator Dynamic(Hypocycloid self) => new Dynamic(self);
        public static implicit operator Hypocycloid(Dynamic value) => value.As<Hypocycloid>();
        public String TypeName => "Hypocycloid";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius1", (String)"Radius2");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius1), new Dynamic(Radius2));
        // Implemented concept functions and type functions
        public Vector2D GetPoint(Angle t) => t.Hypocycloid(this.Radius1, this.Radius2);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var436 = this;
            return numPoints.LinearSpace.Map((x) => _var436.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Epitrochoid: IAngularCurve2D, IOpenShape
    {
        [DataMember] public readonly Number Radius1;
        [DataMember] public readonly Number Radius2;
        [DataMember] public readonly Number Dist;
        public Epitrochoid WithRadius1(Number radius1) => new Epitrochoid(radius1, Radius2, Dist);
        public Epitrochoid WithRadius2(Number radius2) => new Epitrochoid(Radius1, radius2, Dist);
        public Epitrochoid WithDist(Number dist) => new Epitrochoid(Radius1, Radius2, dist);
        public Epitrochoid(Number radius1, Number radius2, Number dist) => (Radius1, Radius2, Dist) = (radius1, radius2, dist);
        public static Epitrochoid Default = new Epitrochoid();
        public static Epitrochoid New(Number radius1, Number radius2, Number dist) => new Epitrochoid(radius1, radius2, dist);
        public Plato.SinglePrecision.Epitrochoid ChangePrecision() => (Radius1.ChangePrecision(), Radius2.ChangePrecision(), Dist.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Epitrochoid(Epitrochoid self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number)(Epitrochoid self) => (self.Radius1, self.Radius2, self.Dist);
        public static implicit operator Epitrochoid((Number, Number, Number) value) => new Epitrochoid(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number radius1, out Number radius2, out Number dist) { radius1 = Radius1; radius2 = Radius2; dist = Dist; }
        public override bool Equals(object obj) { if (!(obj is Epitrochoid)) return false; var other = (Epitrochoid)obj; return Radius1.Equals(other.Radius1) && Radius2.Equals(other.Radius2) && Dist.Equals(other.Dist); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius1, Radius2, Dist);
        public override string ToString() => $"{{ \"Radius1\" = {Radius1}, \"Radius2\" = {Radius2}, \"Dist\" = {Dist} }}";
        public static implicit operator Dynamic(Epitrochoid self) => new Dynamic(self);
        public static implicit operator Epitrochoid(Dynamic value) => value.As<Epitrochoid>();
        public String TypeName => "Epitrochoid";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius1", (String)"Radius2", (String)"Dist");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius1), new Dynamic(Radius2), new Dynamic(Dist));
        // Implemented concept functions and type functions
        public Vector2D GetPoint(Angle t) => t.Epitrochoid(this.Radius1, this.Radius2, this.Dist);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var437 = this;
            return numPoints.LinearSpace.Map((x) => _var437.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Hypotrochoid: IAngularCurve2D, IOpenShape
    {
        [DataMember] public readonly Number Radius1;
        [DataMember] public readonly Number Radius2;
        [DataMember] public readonly Number Dist;
        public Hypotrochoid WithRadius1(Number radius1) => new Hypotrochoid(radius1, Radius2, Dist);
        public Hypotrochoid WithRadius2(Number radius2) => new Hypotrochoid(Radius1, radius2, Dist);
        public Hypotrochoid WithDist(Number dist) => new Hypotrochoid(Radius1, Radius2, dist);
        public Hypotrochoid(Number radius1, Number radius2, Number dist) => (Radius1, Radius2, Dist) = (radius1, radius2, dist);
        public static Hypotrochoid Default = new Hypotrochoid();
        public static Hypotrochoid New(Number radius1, Number radius2, Number dist) => new Hypotrochoid(radius1, radius2, dist);
        public Plato.SinglePrecision.Hypotrochoid ChangePrecision() => (Radius1.ChangePrecision(), Radius2.ChangePrecision(), Dist.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Hypotrochoid(Hypotrochoid self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number)(Hypotrochoid self) => (self.Radius1, self.Radius2, self.Dist);
        public static implicit operator Hypotrochoid((Number, Number, Number) value) => new Hypotrochoid(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number radius1, out Number radius2, out Number dist) { radius1 = Radius1; radius2 = Radius2; dist = Dist; }
        public override bool Equals(object obj) { if (!(obj is Hypotrochoid)) return false; var other = (Hypotrochoid)obj; return Radius1.Equals(other.Radius1) && Radius2.Equals(other.Radius2) && Dist.Equals(other.Dist); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius1, Radius2, Dist);
        public override string ToString() => $"{{ \"Radius1\" = {Radius1}, \"Radius2\" = {Radius2}, \"Dist\" = {Dist} }}";
        public static implicit operator Dynamic(Hypotrochoid self) => new Dynamic(self);
        public static implicit operator Hypotrochoid(Dynamic value) => value.As<Hypotrochoid>();
        public String TypeName => "Hypotrochoid";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius1", (String)"Radius2", (String)"Dist");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius1), new Dynamic(Radius2), new Dynamic(Dist));
        // Implemented concept functions and type functions
        public Vector2D GetPoint(Angle t) => t.Hypotrochoid(this.Radius1, this.Radius2, this.Dist);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var438 = this;
            return numPoints.LinearSpace.Map((x) => _var438.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ButterflyCurve: IAngularCurve2D, IOpenShape
    {
        public static ButterflyCurve Default = new ButterflyCurve();
        public static ButterflyCurve New() => new ButterflyCurve();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(ButterflyCurve self) => new Dynamic(self);
        public static implicit operator ButterflyCurve(Dynamic value) => value.As<ButterflyCurve>();
        public String TypeName => "ButterflyCurve";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        public Vector2D GetPoint(Angle t) => t.ButterflyCurve;
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var439 = this;
            return numPoints.LinearSpace.Map((x) => _var439.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Lissajous: IAngularCurve2D, IOpenShape
    {
        [DataMember] public readonly Angle Delta;
        [DataMember] public readonly Number A;
        [DataMember] public readonly Number B;
        public Lissajous WithDelta(Angle delta) => new Lissajous(delta, A, B);
        public Lissajous WithA(Number a) => new Lissajous(Delta, a, B);
        public Lissajous WithB(Number b) => new Lissajous(Delta, A, b);
        public Lissajous(Angle delta, Number a, Number b) => (Delta, A, B) = (delta, a, b);
        public static Lissajous Default = new Lissajous();
        public static Lissajous New(Angle delta, Number a, Number b) => new Lissajous(delta, a, b);
        public Plato.SinglePrecision.Lissajous ChangePrecision() => (Delta.ChangePrecision(), A.ChangePrecision(), B.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Lissajous(Lissajous self) => self.ChangePrecision();
        public static implicit operator (Angle, Number, Number)(Lissajous self) => (self.Delta, self.A, self.B);
        public static implicit operator Lissajous((Angle, Number, Number) value) => new Lissajous(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Angle delta, out Number a, out Number b) { delta = Delta; a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Lissajous)) return false; var other = (Lissajous)obj; return Delta.Equals(other.Delta) && A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Delta, A, B);
        public override string ToString() => $"{{ \"Delta\" = {Delta}, \"A\" = {A}, \"B\" = {B} }}";
        public static implicit operator Dynamic(Lissajous self) => new Dynamic(self);
        public static implicit operator Lissajous(Dynamic value) => value.As<Lissajous>();
        public String TypeName => "Lissajous";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Delta", (String)"A", (String)"B");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Delta), new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public Vector2D GetPoint(Angle t) => t.Lissajous(this.A, this.B, this.Delta);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var440 = this;
            return numPoints.LinearSpace.Map((x) => _var440.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct CycloidOfCeva: IPolarCurve, IOpenShape
    {
        public static CycloidOfCeva Default = new CycloidOfCeva();
        public static CycloidOfCeva New() => new CycloidOfCeva();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(CycloidOfCeva self) => new Dynamic(self);
        public static implicit operator CycloidOfCeva(Dynamic value) => value.As<CycloidOfCeva>();
        public String TypeName => "CycloidOfCeva";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.CycloidOfCeva;
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var441 = this;
            return numPoints.LinearSpace.Map((x) => _var441.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Limacon: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number A;
        [DataMember] public readonly Number B;
        public Limacon WithA(Number a) => new Limacon(a, B);
        public Limacon WithB(Number b) => new Limacon(A, b);
        public Limacon(Number a, Number b) => (A, B) = (a, b);
        public static Limacon Default = new Limacon();
        public static Limacon New(Number a, Number b) => new Limacon(a, b);
        public Plato.SinglePrecision.Limacon ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Limacon(Limacon self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Limacon self) => (self.A, self.B);
        public static implicit operator Limacon((Number, Number) value) => new Limacon(value.Item1, value.Item2);
        public void Deconstruct(out Number a, out Number b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Limacon)) return false; var other = (Limacon)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B} }}";
        public static implicit operator Dynamic(Limacon self) => new Dynamic(self);
        public static implicit operator Limacon(Dynamic value) => value.As<Limacon>();
        public String TypeName => "Limacon";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.Limacon(this.A, this.B);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var442 = this;
            return numPoints.LinearSpace.Map((x) => _var442.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Cardoid: IPolarCurve, IClosedShape
    {
        public static Cardoid Default = new Cardoid();
        public static Cardoid New() => new Cardoid();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Cardoid self) => new Dynamic(self);
        public static implicit operator Cardoid(Dynamic value) => value.As<Cardoid>();
        public String TypeName => "Cardoid";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.Cardoid;
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)true);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var443 = this;
            return numPoints.LinearSpace.Map((x) => _var443.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Rose: IPolarCurve, IClosedShape
    {
        [DataMember] public readonly Integer K;
        public Rose WithK(Integer k) => new Rose(k);
        public Rose(Integer k) => (K) = (k);
        public static Rose Default = new Rose();
        public static Rose New(Integer k) => new Rose(k);
        public Plato.SinglePrecision.Rose ChangePrecision() => (K.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Rose(Rose self) => self.ChangePrecision();
        public static implicit operator Integer(Rose self) => self.K;
        public static implicit operator Rose(Integer value) => new Rose(value);
        public override bool Equals(object obj) { if (!(obj is Rose)) return false; var other = (Rose)obj; return K.Equals(other.K); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(K);
        public override string ToString() => $"{{ \"K\" = {K} }}";
        public static implicit operator Dynamic(Rose self) => new Dynamic(self);
        public static implicit operator Rose(Dynamic value) => value.As<Rose>();
        public String TypeName => "Rose";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"K");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(K));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.Rose(this.K);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)true);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var444 = this;
            return numPoints.LinearSpace.Map((x) => _var444.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ArchimedeanSpiral: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number A;
        [DataMember] public readonly Number B;
        public ArchimedeanSpiral WithA(Number a) => new ArchimedeanSpiral(a, B);
        public ArchimedeanSpiral WithB(Number b) => new ArchimedeanSpiral(A, b);
        public ArchimedeanSpiral(Number a, Number b) => (A, B) = (a, b);
        public static ArchimedeanSpiral Default = new ArchimedeanSpiral();
        public static ArchimedeanSpiral New(Number a, Number b) => new ArchimedeanSpiral(a, b);
        public Plato.SinglePrecision.ArchimedeanSpiral ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ArchimedeanSpiral(ArchimedeanSpiral self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(ArchimedeanSpiral self) => (self.A, self.B);
        public static implicit operator ArchimedeanSpiral((Number, Number) value) => new ArchimedeanSpiral(value.Item1, value.Item2);
        public void Deconstruct(out Number a, out Number b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is ArchimedeanSpiral)) return false; var other = (ArchimedeanSpiral)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B} }}";
        public static implicit operator Dynamic(ArchimedeanSpiral self) => new Dynamic(self);
        public static implicit operator ArchimedeanSpiral(Dynamic value) => value.As<ArchimedeanSpiral>();
        public String TypeName => "ArchimedeanSpiral";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.ArchimedeanSpiral(this.A, this.B);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var445 = this;
            return numPoints.LinearSpace.Map((x) => _var445.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ConicSection: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number Eccentricity;
        [DataMember] public readonly Number SemiLatusRectum;
        public ConicSection WithEccentricity(Number eccentricity) => new ConicSection(eccentricity, SemiLatusRectum);
        public ConicSection WithSemiLatusRectum(Number semiLatusRectum) => new ConicSection(Eccentricity, semiLatusRectum);
        public ConicSection(Number eccentricity, Number semiLatusRectum) => (Eccentricity, SemiLatusRectum) = (eccentricity, semiLatusRectum);
        public static ConicSection Default = new ConicSection();
        public static ConicSection New(Number eccentricity, Number semiLatusRectum) => new ConicSection(eccentricity, semiLatusRectum);
        public Plato.SinglePrecision.ConicSection ChangePrecision() => (Eccentricity.ChangePrecision(), SemiLatusRectum.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ConicSection(ConicSection self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(ConicSection self) => (self.Eccentricity, self.SemiLatusRectum);
        public static implicit operator ConicSection((Number, Number) value) => new ConicSection(value.Item1, value.Item2);
        public void Deconstruct(out Number eccentricity, out Number semiLatusRectum) { eccentricity = Eccentricity; semiLatusRectum = SemiLatusRectum; }
        public override bool Equals(object obj) { if (!(obj is ConicSection)) return false; var other = (ConicSection)obj; return Eccentricity.Equals(other.Eccentricity) && SemiLatusRectum.Equals(other.SemiLatusRectum); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Eccentricity, SemiLatusRectum);
        public override string ToString() => $"{{ \"Eccentricity\" = {Eccentricity}, \"SemiLatusRectum\" = {SemiLatusRectum} }}";
        public static implicit operator Dynamic(ConicSection self) => new Dynamic(self);
        public static implicit operator ConicSection(Dynamic value) => value.As<ConicSection>();
        public String TypeName => "ConicSection";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Eccentricity", (String)"SemiLatusRectum");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Eccentricity), new Dynamic(SemiLatusRectum));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.ConicSection(this.SemiLatusRectum, this.Eccentricity);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var446 = this;
            return numPoints.LinearSpace.Map((x) => _var446.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct LemniscateOfBernoulli: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number A;
        public LemniscateOfBernoulli WithA(Number a) => new LemniscateOfBernoulli(a);
        public LemniscateOfBernoulli(Number a) => (A) = (a);
        public static LemniscateOfBernoulli Default = new LemniscateOfBernoulli();
        public static LemniscateOfBernoulli New(Number a) => new LemniscateOfBernoulli(a);
        public Plato.SinglePrecision.LemniscateOfBernoulli ChangePrecision() => (A.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.LemniscateOfBernoulli(LemniscateOfBernoulli self) => self.ChangePrecision();
        public static implicit operator Number(LemniscateOfBernoulli self) => self.A;
        public static implicit operator LemniscateOfBernoulli(Number value) => new LemniscateOfBernoulli(value);
        public static implicit operator LemniscateOfBernoulli(Integer value) => new LemniscateOfBernoulli(value);
        public static implicit operator LemniscateOfBernoulli(int value) => new Integer(value);
        public static implicit operator LemniscateOfBernoulli(double value) => new Number(value);
        public static implicit operator double(LemniscateOfBernoulli value) => value.A;
        public override bool Equals(object obj) { if (!(obj is LemniscateOfBernoulli)) return false; var other = (LemniscateOfBernoulli)obj; return A.Equals(other.A); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A);
        public override string ToString() => $"{{ \"A\" = {A} }}";
        public static implicit operator Dynamic(LemniscateOfBernoulli self) => new Dynamic(self);
        public static implicit operator LemniscateOfBernoulli(Dynamic value) => value.As<LemniscateOfBernoulli>();
        public String TypeName => "LemniscateOfBernoulli";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.LemniscateOfBernoulli(this.A);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var447 = this;
            return numPoints.LinearSpace.Map((x) => _var447.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct TrisectrixOfMaclaurin: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number A;
        public TrisectrixOfMaclaurin WithA(Number a) => new TrisectrixOfMaclaurin(a);
        public TrisectrixOfMaclaurin(Number a) => (A) = (a);
        public static TrisectrixOfMaclaurin Default = new TrisectrixOfMaclaurin();
        public static TrisectrixOfMaclaurin New(Number a) => new TrisectrixOfMaclaurin(a);
        public Plato.SinglePrecision.TrisectrixOfMaclaurin ChangePrecision() => (A.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.TrisectrixOfMaclaurin(TrisectrixOfMaclaurin self) => self.ChangePrecision();
        public static implicit operator Number(TrisectrixOfMaclaurin self) => self.A;
        public static implicit operator TrisectrixOfMaclaurin(Number value) => new TrisectrixOfMaclaurin(value);
        public static implicit operator TrisectrixOfMaclaurin(Integer value) => new TrisectrixOfMaclaurin(value);
        public static implicit operator TrisectrixOfMaclaurin(int value) => new Integer(value);
        public static implicit operator TrisectrixOfMaclaurin(double value) => new Number(value);
        public static implicit operator double(TrisectrixOfMaclaurin value) => value.A;
        public override bool Equals(object obj) { if (!(obj is TrisectrixOfMaclaurin)) return false; var other = (TrisectrixOfMaclaurin)obj; return A.Equals(other.A); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A);
        public override string ToString() => $"{{ \"A\" = {A} }}";
        public static implicit operator Dynamic(TrisectrixOfMaclaurin self) => new Dynamic(self);
        public static implicit operator TrisectrixOfMaclaurin(Dynamic value) => value.As<TrisectrixOfMaclaurin>();
        public String TypeName => "TrisectrixOfMaclaurin";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.TrisectrixOfMaclaurin(this.A);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var448 = this;
            return numPoints.LinearSpace.Map((x) => _var448.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ConchoidOfDeSluze: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number A;
        public ConchoidOfDeSluze WithA(Number a) => new ConchoidOfDeSluze(a);
        public ConchoidOfDeSluze(Number a) => (A) = (a);
        public static ConchoidOfDeSluze Default = new ConchoidOfDeSluze();
        public static ConchoidOfDeSluze New(Number a) => new ConchoidOfDeSluze(a);
        public Plato.SinglePrecision.ConchoidOfDeSluze ChangePrecision() => (A.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ConchoidOfDeSluze(ConchoidOfDeSluze self) => self.ChangePrecision();
        public static implicit operator Number(ConchoidOfDeSluze self) => self.A;
        public static implicit operator ConchoidOfDeSluze(Number value) => new ConchoidOfDeSluze(value);
        public static implicit operator ConchoidOfDeSluze(Integer value) => new ConchoidOfDeSluze(value);
        public static implicit operator ConchoidOfDeSluze(int value) => new Integer(value);
        public static implicit operator ConchoidOfDeSluze(double value) => new Number(value);
        public static implicit operator double(ConchoidOfDeSluze value) => value.A;
        public override bool Equals(object obj) { if (!(obj is ConchoidOfDeSluze)) return false; var other = (ConchoidOfDeSluze)obj; return A.Equals(other.A); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A);
        public override string ToString() => $"{{ \"A\" = {A} }}";
        public static implicit operator Dynamic(ConchoidOfDeSluze self) => new Dynamic(self);
        public static implicit operator ConchoidOfDeSluze(Dynamic value) => value.As<ConchoidOfDeSluze>();
        public String TypeName => "ConchoidOfDeSluze";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.ConchoidOfDeSluze(this.A);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var449 = this;
            return numPoints.LinearSpace.Map((x) => _var449.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct TschirnhausenCubic: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number A;
        public TschirnhausenCubic WithA(Number a) => new TschirnhausenCubic(a);
        public TschirnhausenCubic(Number a) => (A) = (a);
        public static TschirnhausenCubic Default = new TschirnhausenCubic();
        public static TschirnhausenCubic New(Number a) => new TschirnhausenCubic(a);
        public Plato.SinglePrecision.TschirnhausenCubic ChangePrecision() => (A.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.TschirnhausenCubic(TschirnhausenCubic self) => self.ChangePrecision();
        public static implicit operator Number(TschirnhausenCubic self) => self.A;
        public static implicit operator TschirnhausenCubic(Number value) => new TschirnhausenCubic(value);
        public static implicit operator TschirnhausenCubic(Integer value) => new TschirnhausenCubic(value);
        public static implicit operator TschirnhausenCubic(int value) => new Integer(value);
        public static implicit operator TschirnhausenCubic(double value) => new Number(value);
        public static implicit operator double(TschirnhausenCubic value) => value.A;
        public override bool Equals(object obj) { if (!(obj is TschirnhausenCubic)) return false; var other = (TschirnhausenCubic)obj; return A.Equals(other.A); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A);
        public override string ToString() => $"{{ \"A\" = {A} }}";
        public static implicit operator Dynamic(TschirnhausenCubic self) => new Dynamic(self);
        public static implicit operator TschirnhausenCubic(Dynamic value) => value.As<TschirnhausenCubic>();
        public String TypeName => "TschirnhausenCubic";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.TschirnhausenCubic(this.A);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var450 = this;
            return numPoints.LinearSpace.Map((x) => _var450.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct SinusoidalSpiral: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number A;
        [DataMember] public readonly Number N;
        public SinusoidalSpiral WithA(Number a) => new SinusoidalSpiral(a, N);
        public SinusoidalSpiral WithN(Number n) => new SinusoidalSpiral(A, n);
        public SinusoidalSpiral(Number a, Number n) => (A, N) = (a, n);
        public static SinusoidalSpiral Default = new SinusoidalSpiral();
        public static SinusoidalSpiral New(Number a, Number n) => new SinusoidalSpiral(a, n);
        public Plato.SinglePrecision.SinusoidalSpiral ChangePrecision() => (A.ChangePrecision(), N.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.SinusoidalSpiral(SinusoidalSpiral self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(SinusoidalSpiral self) => (self.A, self.N);
        public static implicit operator SinusoidalSpiral((Number, Number) value) => new SinusoidalSpiral(value.Item1, value.Item2);
        public void Deconstruct(out Number a, out Number n) { a = A; n = N; }
        public override bool Equals(object obj) { if (!(obj is SinusoidalSpiral)) return false; var other = (SinusoidalSpiral)obj; return A.Equals(other.A) && N.Equals(other.N); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, N);
        public override string ToString() => $"{{ \"A\" = {A}, \"N\" = {N} }}";
        public static implicit operator Dynamic(SinusoidalSpiral self) => new Dynamic(self);
        public static implicit operator SinusoidalSpiral(Dynamic value) => value.As<SinusoidalSpiral>();
        public String TypeName => "SinusoidalSpiral";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"N");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(N));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.SinusoidalSpiral(this.A, this.N);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var451 = this;
            return numPoints.LinearSpace.Map((x) => _var451.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct FermatsSpiral: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number A;
        public FermatsSpiral WithA(Number a) => new FermatsSpiral(a);
        public FermatsSpiral(Number a) => (A) = (a);
        public static FermatsSpiral Default = new FermatsSpiral();
        public static FermatsSpiral New(Number a) => new FermatsSpiral(a);
        public Plato.SinglePrecision.FermatsSpiral ChangePrecision() => (A.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.FermatsSpiral(FermatsSpiral self) => self.ChangePrecision();
        public static implicit operator Number(FermatsSpiral self) => self.A;
        public static implicit operator FermatsSpiral(Number value) => new FermatsSpiral(value);
        public static implicit operator FermatsSpiral(Integer value) => new FermatsSpiral(value);
        public static implicit operator FermatsSpiral(int value) => new Integer(value);
        public static implicit operator FermatsSpiral(double value) => new Number(value);
        public static implicit operator double(FermatsSpiral value) => value.A;
        public override bool Equals(object obj) { if (!(obj is FermatsSpiral)) return false; var other = (FermatsSpiral)obj; return A.Equals(other.A); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A);
        public override string ToString() => $"{{ \"A\" = {A} }}";
        public static implicit operator Dynamic(FermatsSpiral self) => new Dynamic(self);
        public static implicit operator FermatsSpiral(Dynamic value) => value.As<FermatsSpiral>();
        public String TypeName => "FermatsSpiral";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.FermatsSpiral(this.A);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var452 = this;
            return numPoints.LinearSpace.Map((x) => _var452.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct LogarithmicSpiral: IPolarCurve, IOpenShape
    {
        [DataMember] public readonly Number A;
        [DataMember] public readonly Number K;
        public LogarithmicSpiral WithA(Number a) => new LogarithmicSpiral(a, K);
        public LogarithmicSpiral WithK(Number k) => new LogarithmicSpiral(A, k);
        public LogarithmicSpiral(Number a, Number k) => (A, K) = (a, k);
        public static LogarithmicSpiral Default = new LogarithmicSpiral();
        public static LogarithmicSpiral New(Number a, Number k) => new LogarithmicSpiral(a, k);
        public Plato.SinglePrecision.LogarithmicSpiral ChangePrecision() => (A.ChangePrecision(), K.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.LogarithmicSpiral(LogarithmicSpiral self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(LogarithmicSpiral self) => (self.A, self.K);
        public static implicit operator LogarithmicSpiral((Number, Number) value) => new LogarithmicSpiral(value.Item1, value.Item2);
        public void Deconstruct(out Number a, out Number k) { a = A; k = K; }
        public override bool Equals(object obj) { if (!(obj is LogarithmicSpiral)) return false; var other = (LogarithmicSpiral)obj; return A.Equals(other.A) && K.Equals(other.K); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, K);
        public override string ToString() => $"{{ \"A\" = {A}, \"K\" = {K} }}";
        public static implicit operator Dynamic(LogarithmicSpiral self) => new Dynamic(self);
        public static implicit operator LogarithmicSpiral(Dynamic value) => value.As<LogarithmicSpiral>();
        public String TypeName => "LogarithmicSpiral";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"K");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(K));
        // Implemented concept functions and type functions
        public Number GetRadius(Angle t) => t.LogarithmicSpiral(this.A, this.K);
        public PolarCoordinate EvalPolar(Angle t) => this.GetRadius(t).Tuple2(t);
        public Vector2D GetPoint(Angle t) => this.EvalPolar(t);
        public Vector2D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var453 = this;
            return numPoints.LinearSpace.Map((x) => _var453.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct CubicBezier3D: IArray<Vector3D>, IOpenShape
    {
        [DataMember] public readonly Vector3D A;
        [DataMember] public readonly Vector3D B;
        [DataMember] public readonly Vector3D C;
        [DataMember] public readonly Vector3D D;
        public CubicBezier3D WithA(Vector3D a) => new CubicBezier3D(a, B, C, D);
        public CubicBezier3D WithB(Vector3D b) => new CubicBezier3D(A, b, C, D);
        public CubicBezier3D WithC(Vector3D c) => new CubicBezier3D(A, B, c, D);
        public CubicBezier3D WithD(Vector3D d) => new CubicBezier3D(A, B, C, d);
        public CubicBezier3D(Vector3D a, Vector3D b, Vector3D c, Vector3D d) => (A, B, C, D) = (a, b, c, d);
        public static CubicBezier3D Default = new CubicBezier3D();
        public static CubicBezier3D New(Vector3D a, Vector3D b, Vector3D c, Vector3D d) => new CubicBezier3D(a, b, c, d);
        public Plato.SinglePrecision.CubicBezier3D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision(), D.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.CubicBezier3D(CubicBezier3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Vector3D, Vector3D, Vector3D)(CubicBezier3D self) => (self.A, self.B, self.C, self.D);
        public static implicit operator CubicBezier3D((Vector3D, Vector3D, Vector3D, Vector3D) value) => new CubicBezier3D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector3D a, out Vector3D b, out Vector3D c, out Vector3D d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is CubicBezier3D)) return false; var other = (CubicBezier3D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C}, \"D\" = {D} }}";
        public static implicit operator Dynamic(CubicBezier3D self) => new Dynamic(self);
        public static implicit operator CubicBezier3D(Dynamic value) => value.As<CubicBezier3D>();
        public String TypeName => "CubicBezier3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        // Array predefined functions
        public CubicBezier3D(IArray<Vector3D> xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public CubicBezier3D(Vector3D[] xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public static CubicBezier3D New(IArray<Vector3D> xs) => new CubicBezier3D(xs);
        public static CubicBezier3D New(Vector3D[] xs) => new CubicBezier3D(xs);
        public static implicit operator Vector3D[](CubicBezier3D self) => self.ToSystemArray();
        public static implicit operator Array<Vector3D>(CubicBezier3D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector3D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector3D System.Collections.Generic.IReadOnlyList<Vector3D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector3D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Vector3D Eval(Number t) => this.A.QuadraticBezier(this.B, this.C, t);
        public Boolean Closed => ((Boolean)false);
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct QuadraticBezier3D: IArray<Vector3D>, IOpenShape
    {
        [DataMember] public readonly Vector3D A;
        [DataMember] public readonly Vector3D B;
        [DataMember] public readonly Vector3D C;
        public QuadraticBezier3D WithA(Vector3D a) => new QuadraticBezier3D(a, B, C);
        public QuadraticBezier3D WithB(Vector3D b) => new QuadraticBezier3D(A, b, C);
        public QuadraticBezier3D WithC(Vector3D c) => new QuadraticBezier3D(A, B, c);
        public QuadraticBezier3D(Vector3D a, Vector3D b, Vector3D c) => (A, B, C) = (a, b, c);
        public static QuadraticBezier3D Default = new QuadraticBezier3D();
        public static QuadraticBezier3D New(Vector3D a, Vector3D b, Vector3D c) => new QuadraticBezier3D(a, b, c);
        public Plato.SinglePrecision.QuadraticBezier3D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.QuadraticBezier3D(QuadraticBezier3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Vector3D, Vector3D)(QuadraticBezier3D self) => (self.A, self.B, self.C);
        public static implicit operator QuadraticBezier3D((Vector3D, Vector3D, Vector3D) value) => new QuadraticBezier3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D a, out Vector3D b, out Vector3D c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is QuadraticBezier3D)) return false; var other = (QuadraticBezier3D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C} }}";
        public static implicit operator Dynamic(QuadraticBezier3D self) => new Dynamic(self);
        public static implicit operator QuadraticBezier3D(Dynamic value) => value.As<QuadraticBezier3D>();
        public String TypeName => "QuadraticBezier3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        // Array predefined functions
        public QuadraticBezier3D(IArray<Vector3D> xs) : this(xs[0], xs[1], xs[2]) { }
        public QuadraticBezier3D(Vector3D[] xs) : this(xs[0], xs[1], xs[2]) { }
        public static QuadraticBezier3D New(IArray<Vector3D> xs) => new QuadraticBezier3D(xs);
        public static QuadraticBezier3D New(Vector3D[] xs) => new QuadraticBezier3D(xs);
        public static implicit operator Vector3D[](QuadraticBezier3D self) => self.ToSystemArray();
        public static implicit operator Array<Vector3D>(QuadraticBezier3D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector3D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector3D System.Collections.Generic.IReadOnlyList<Vector3D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector3D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Vector3D Eval(Number t) => this.A.QuadraticBezier(this.B, this.C, t);
        public Boolean Closed => ((Boolean)false);
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct TorusKnot: IAngularCurve3D, IClosedShape
    {
        [DataMember] public readonly Integer P;
        [DataMember] public readonly Integer Q;
        [DataMember] public readonly Number Radius;
        public TorusKnot WithP(Integer p) => new TorusKnot(p, Q, Radius);
        public TorusKnot WithQ(Integer q) => new TorusKnot(P, q, Radius);
        public TorusKnot WithRadius(Number radius) => new TorusKnot(P, Q, radius);
        public TorusKnot(Integer p, Integer q, Number radius) => (P, Q, Radius) = (p, q, radius);
        public static TorusKnot Default = new TorusKnot();
        public static TorusKnot New(Integer p, Integer q, Number radius) => new TorusKnot(p, q, radius);
        public Plato.SinglePrecision.TorusKnot ChangePrecision() => (P.ChangePrecision(), Q.ChangePrecision(), Radius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.TorusKnot(TorusKnot self) => self.ChangePrecision();
        public static implicit operator (Integer, Integer, Number)(TorusKnot self) => (self.P, self.Q, self.Radius);
        public static implicit operator TorusKnot((Integer, Integer, Number) value) => new TorusKnot(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Integer p, out Integer q, out Number radius) { p = P; q = Q; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is TorusKnot)) return false; var other = (TorusKnot)obj; return P.Equals(other.P) && Q.Equals(other.Q) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(P, Q, Radius);
        public override string ToString() => $"{{ \"P\" = {P}, \"Q\" = {Q}, \"Radius\" = {Radius} }}";
        public static implicit operator Dynamic(TorusKnot self) => new Dynamic(self);
        public static implicit operator TorusKnot(Dynamic value) => value.As<TorusKnot>();
        public String TypeName => "TorusKnot";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"P", (String)"Q", (String)"Radius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(P), new Dynamic(Q), new Dynamic(Radius));
        // Implemented concept functions and type functions
        public Vector3D GetPoint(Angle t) => t.TorusKnot(this.P, this.Q).Multiply(this.Radius);
        public Vector3D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)true);
        public IArray<Vector3D> Sample(Integer numPoints){
            var _var454 = this;
            return numPoints.LinearSpace.Map((x) => _var454.Eval(x));
        }
        public PolyLine3D ToPolyLine3D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct TrefoilKnot: IAngularCurve3D, IClosedShape
    {
        public static TrefoilKnot Default = new TrefoilKnot();
        public static TrefoilKnot New() => new TrefoilKnot();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(TrefoilKnot self) => new Dynamic(self);
        public static implicit operator TrefoilKnot(Dynamic value) => value.As<TrefoilKnot>();
        public String TypeName => "TrefoilKnot";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        public Vector3D GetPoint(Angle t) => t.TrefoilKnot;
        public Vector3D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)true);
        public IArray<Vector3D> Sample(Integer numPoints){
            var _var455 = this;
            return numPoints.LinearSpace.Map((x) => _var455.Eval(x));
        }
        public PolyLine3D ToPolyLine3D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct FigureEightKnot: IAngularCurve3D, IClosedShape
    {
        public static FigureEightKnot Default = new FigureEightKnot();
        public static FigureEightKnot New() => new FigureEightKnot();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(FigureEightKnot self) => new Dynamic(self);
        public static implicit operator FigureEightKnot(Dynamic value) => value.As<FigureEightKnot>();
        public String TypeName => "FigureEightKnot";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        public Vector3D GetPoint(Angle t) => t.FigureEightKnot;
        public Vector3D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)true);
        public IArray<Vector3D> Sample(Integer numPoints){
            var _var456 = this;
            return numPoints.LinearSpace.Map((x) => _var456.Eval(x));
        }
        public PolyLine3D ToPolyLine3D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Helix: IAngularCurve3D, IOpenShape
    {
        [DataMember] public readonly Number Radius;
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number NumTurns;
        public Helix WithRadius(Number radius) => new Helix(radius, Height, NumTurns);
        public Helix WithHeight(Number height) => new Helix(Radius, height, NumTurns);
        public Helix WithNumTurns(Number numTurns) => new Helix(Radius, Height, numTurns);
        public Helix(Number radius, Number height, Number numTurns) => (Radius, Height, NumTurns) = (radius, height, numTurns);
        public static Helix Default = new Helix();
        public static Helix New(Number radius, Number height, Number numTurns) => new Helix(radius, height, numTurns);
        public Plato.SinglePrecision.Helix ChangePrecision() => (Radius.ChangePrecision(), Height.ChangePrecision(), NumTurns.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Helix(Helix self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number)(Helix self) => (self.Radius, self.Height, self.NumTurns);
        public static implicit operator Helix((Number, Number, Number) value) => new Helix(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number radius, out Number height, out Number numTurns) { radius = Radius; height = Height; numTurns = NumTurns; }
        public override bool Equals(object obj) { if (!(obj is Helix)) return false; var other = (Helix)obj; return Radius.Equals(other.Radius) && Height.Equals(other.Height) && NumTurns.Equals(other.NumTurns); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius, Height, NumTurns);
        public override string ToString() => $"{{ \"Radius\" = {Radius}, \"Height\" = {Height}, \"NumTurns\" = {NumTurns} }}";
        public static implicit operator Dynamic(Helix self) => new Dynamic(self);
        public static implicit operator Helix(Dynamic value) => value.As<Helix>();
        public String TypeName => "Helix";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius", (String)"Height", (String)"NumTurns");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius), new Dynamic(Height), new Dynamic(NumTurns));
        // Implemented concept functions and type functions
        public Vector3D GetPoint(Angle t) => t.Helix(this.NumTurns).Multiply(this.Radius.Tuple3(this.Radius, this.Height));
        public Vector3D Eval(Number t) => this.GetPoint(t.Turns);
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector3D> Sample(Integer numPoints){
            var _var457 = this;
            return numPoints.LinearSpace.Map((x) => _var457.Eval(x));
        }
        public PolyLine3D ToPolyLine3D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Transform2D: IValue<Transform2D>
    {
        [DataMember] public readonly Vector2D Translation;
        [DataMember] public readonly Angle Rotation;
        [DataMember] public readonly Vector2D Scale;
        public Transform2D WithTranslation(Vector2D translation) => new Transform2D(translation, Rotation, Scale);
        public Transform2D WithRotation(Angle rotation) => new Transform2D(Translation, rotation, Scale);
        public Transform2D WithScale(Vector2D scale) => new Transform2D(Translation, Rotation, scale);
        public Transform2D(Vector2D translation, Angle rotation, Vector2D scale) => (Translation, Rotation, Scale) = (translation, rotation, scale);
        public static Transform2D Default = new Transform2D();
        public static Transform2D New(Vector2D translation, Angle rotation, Vector2D scale) => new Transform2D(translation, rotation, scale);
        public Plato.SinglePrecision.Transform2D ChangePrecision() => (Translation.ChangePrecision(), Rotation.ChangePrecision(), Scale.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Transform2D(Transform2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Angle, Vector2D)(Transform2D self) => (self.Translation, self.Rotation, self.Scale);
        public static implicit operator Transform2D((Vector2D, Angle, Vector2D) value) => new Transform2D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D translation, out Angle rotation, out Vector2D scale) { translation = Translation; rotation = Rotation; scale = Scale; }
        public override bool Equals(object obj) { if (!(obj is Transform2D)) return false; var other = (Transform2D)obj; return Translation.Equals(other.Translation) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Translation, Rotation, Scale);
        public override string ToString() => $"{{ \"Translation\" = {Translation}, \"Rotation\" = {Rotation}, \"Scale\" = {Scale} }}";
        public static implicit operator Dynamic(Transform2D self) => new Dynamic(self);
        public static implicit operator Transform2D(Dynamic value) => value.As<Transform2D>();
        public String TypeName => "Transform2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Translation", (String)"Rotation", (String)"Scale");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Translation), new Dynamic(Rotation), new Dynamic(Scale));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Transform2D)b);
        // Implemented concept functions and type functions
        public IArray<Transform2D> Repeat(Integer n){
            var _var458 = this;
            return n.MapRange((i) => _var458);
        }
        public Boolean Equals(Transform2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Transform2D a, Transform2D b) => a.Equals(b);
        public Boolean NotEquals(Transform2D b) => this.Equals(b).Not;
        public static Boolean operator !=(Transform2D a, Transform2D b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Pose2D: IValue<Pose2D>
    {
        [DataMember] public readonly Vector2D Position;
        [DataMember] public readonly Angle Rotation;
        public Pose2D WithPosition(Vector2D position) => new Pose2D(position, Rotation);
        public Pose2D WithRotation(Angle rotation) => new Pose2D(Position, rotation);
        public Pose2D(Vector2D position, Angle rotation) => (Position, Rotation) = (position, rotation);
        public static Pose2D Default = new Pose2D();
        public static Pose2D New(Vector2D position, Angle rotation) => new Pose2D(position, rotation);
        public Plato.SinglePrecision.Pose2D ChangePrecision() => (Position.ChangePrecision(), Rotation.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Pose2D(Pose2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Angle)(Pose2D self) => (self.Position, self.Rotation);
        public static implicit operator Pose2D((Vector2D, Angle) value) => new Pose2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D position, out Angle rotation) { position = Position; rotation = Rotation; }
        public override bool Equals(object obj) { if (!(obj is Pose2D)) return false; var other = (Pose2D)obj; return Position.Equals(other.Position) && Rotation.Equals(other.Rotation); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Position, Rotation);
        public override string ToString() => $"{{ \"Position\" = {Position}, \"Rotation\" = {Rotation} }}";
        public static implicit operator Dynamic(Pose2D self) => new Dynamic(self);
        public static implicit operator Pose2D(Dynamic value) => value.As<Pose2D>();
        public String TypeName => "Pose2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Position", (String)"Rotation");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Position), new Dynamic(Rotation));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Pose2D)b);
        // Implemented concept functions and type functions
        public IArray<Pose2D> Repeat(Integer n){
            var _var459 = this;
            return n.MapRange((i) => _var459);
        }
        public Boolean Equals(Pose2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Pose2D a, Pose2D b) => a.Equals(b);
        public Boolean NotEquals(Pose2D b) => this.Equals(b).Not;
        public static Boolean operator !=(Pose2D a, Pose2D b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Bounds2D: IBounds<Bounds2D, Vector2D>
    {
        [DataMember] public readonly Vector2D Min;
        [DataMember] public readonly Vector2D Max;
        public Bounds2D WithMin(Vector2D min) => new Bounds2D(min, Max);
        public Bounds2D WithMax(Vector2D max) => new Bounds2D(Min, max);
        public Bounds2D(Vector2D min, Vector2D max) => (Min, Max) = (min, max);
        public static Bounds2D Default = new Bounds2D();
        public static Bounds2D New(Vector2D min, Vector2D max) => new Bounds2D(min, max);
        public Plato.SinglePrecision.Bounds2D ChangePrecision() => (Min.ChangePrecision(), Max.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Bounds2D(Bounds2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Vector2D)(Bounds2D self) => (self.Min, self.Max);
        public static implicit operator Bounds2D((Vector2D, Vector2D) value) => new Bounds2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D min, out Vector2D max) { min = Min; max = Max; }
        public override bool Equals(object obj) { if (!(obj is Bounds2D)) return false; var other = (Bounds2D)obj; return Min.Equals(other.Min) && Max.Equals(other.Max); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Min, Max);
        public override string ToString() => $"{{ \"Min\" = {Min}, \"Max\" = {Max} }}";
        public static implicit operator Dynamic(Bounds2D self) => new Dynamic(self);
        public static implicit operator Bounds2D(Dynamic value) => value.As<Bounds2D>();
        public String TypeName => "Bounds2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Min", (String)"Max");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Min), new Dynamic(Max));
        Vector2D IBounds<Bounds2D, Vector2D>.Min => Min;
        Vector2D IBounds<Bounds2D, Vector2D>.Max => Max;
        Vector2D IBounds<Vector2D>.Min => this.Min;
        Vector2D IBounds<Vector2D>.Max => this.Max;
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Bounds2D)b);
        // Implemented concept functions and type functions
        public Bounds3D To3D => this.Min.To3D.Tuple2(this.Max.To3D);
        public Bounds3D Bounds3D => this.To3D;
        public static implicit operator Bounds3D(Bounds2D x) => x.Bounds3D;
        public Vector2D Size => this.Max.Subtract(this.Min);
        public Vector2D Lerp(Number amount) => this.Min.Lerp(this.Max, amount);
        public Vector2D Center => this.Lerp(((Number)0.5));
        public Boolean Contains(Vector2D value) => value.Between(this.Min, this.Max);
        public Boolean Contains(Bounds2D y) => this.Contains(y.Min).And(this.Contains(y.Max));
        public Boolean Overlaps(Bounds2D y) => this.Contains(y.Min).Or(this.Contains(y.Max).Or(y.Contains(this.Min).Or(y.Contains(this.Max))));
        public Bounds2D Recenter(Vector2D c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public Bounds2D Clamp(Bounds2D y) => this.Clamp(y.Min).Tuple2(this.Clamp(y.Max));
        public Vector2D Clamp(Vector2D value) => value.Clamp(this.Min, this.Max);
        public Bounds2D Include(Vector2D value) => this.Min.Min(value).Tuple2(this.Max.Max(value));
        public Bounds2D Include(Bounds2D y) => this.Include(y.Min).Include(y.Max);
        public Boolean Equals(Bounds2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Bounds2D a, Bounds2D b) => a.Equals(b);
        public Boolean NotEquals(Bounds2D b) => this.Equals(b).Not;
        public static Boolean operator !=(Bounds2D a, Bounds2D b) => a.NotEquals(b);
        public IArray<Bounds2D> Repeat(Integer n){
            var _var460 = this;
            return n.MapRange((i) => _var460);
        }
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Ray2D: IValue<Ray2D>
    {
        [DataMember] public readonly Vector2D Origin;
        [DataMember] public readonly Vector2D Direction;
        public Ray2D WithOrigin(Vector2D origin) => new Ray2D(origin, Direction);
        public Ray2D WithDirection(Vector2D direction) => new Ray2D(Origin, direction);
        public Ray2D(Vector2D origin, Vector2D direction) => (Origin, Direction) = (origin, direction);
        public static Ray2D Default = new Ray2D();
        public static Ray2D New(Vector2D origin, Vector2D direction) => new Ray2D(origin, direction);
        public Plato.SinglePrecision.Ray2D ChangePrecision() => (Origin.ChangePrecision(), Direction.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Ray2D(Ray2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Vector2D)(Ray2D self) => (self.Origin, self.Direction);
        public static implicit operator Ray2D((Vector2D, Vector2D) value) => new Ray2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D origin, out Vector2D direction) { origin = Origin; direction = Direction; }
        public override bool Equals(object obj) { if (!(obj is Ray2D)) return false; var other = (Ray2D)obj; return Origin.Equals(other.Origin) && Direction.Equals(other.Direction); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Origin, Direction);
        public override string ToString() => $"{{ \"Origin\" = {Origin}, \"Direction\" = {Direction} }}";
        public static implicit operator Dynamic(Ray2D self) => new Dynamic(self);
        public static implicit operator Ray2D(Dynamic value) => value.As<Ray2D>();
        public String TypeName => "Ray2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Origin", (String)"Direction");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Origin), new Dynamic(Direction));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Ray2D)b);
        // Implemented concept functions and type functions
        public Ray3D To3D => this.Origin.To3D.Tuple2(this.Direction.To3D);
        public Ray3D Ray3D => this.To3D;
        public static implicit operator Ray3D(Ray2D x) => x.Ray3D;
        public IArray<Ray2D> Repeat(Integer n){
            var _var461 = this;
            return n.MapRange((i) => _var461);
        }
        public Boolean Equals(Ray2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Ray2D a, Ray2D b) => a.Equals(b);
        public Boolean NotEquals(Ray2D b) => this.Equals(b).Not;
        public static Boolean operator !=(Ray2D a, Ray2D b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Triangle2D: IPolygon2D
    {
        [DataMember] public readonly Vector2D A;
        [DataMember] public readonly Vector2D B;
        [DataMember] public readonly Vector2D C;
        public Triangle2D WithA(Vector2D a) => new Triangle2D(a, B, C);
        public Triangle2D WithB(Vector2D b) => new Triangle2D(A, b, C);
        public Triangle2D WithC(Vector2D c) => new Triangle2D(A, B, c);
        public Triangle2D(Vector2D a, Vector2D b, Vector2D c) => (A, B, C) = (a, b, c);
        public static Triangle2D Default = new Triangle2D();
        public static Triangle2D New(Vector2D a, Vector2D b, Vector2D c) => new Triangle2D(a, b, c);
        public Plato.SinglePrecision.Triangle2D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Triangle2D(Triangle2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Vector2D, Vector2D)(Triangle2D self) => (self.A, self.B, self.C);
        public static implicit operator Triangle2D((Vector2D, Vector2D, Vector2D) value) => new Triangle2D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D a, out Vector2D b, out Vector2D c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is Triangle2D)) return false; var other = (Triangle2D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C} }}";
        public static implicit operator Dynamic(Triangle2D self) => new Dynamic(self);
        public static implicit operator Triangle2D(Dynamic value) => value.As<Triangle2D>();
        public String TypeName => "Triangle2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        // Array predefined functions
        public Triangle2D(IArray<Vector2D> xs) : this(xs[0], xs[1], xs[2]) { }
        public Triangle2D(Vector2D[] xs) : this(xs[0], xs[1], xs[2]) { }
        public static Triangle2D New(IArray<Vector2D> xs) => new Triangle2D(xs);
        public static Triangle2D New(Vector2D[] xs) => new Triangle2D(xs);
        public static implicit operator Vector2D[](Triangle2D self) => self.ToSystemArray();
        public static implicit operator Array<Vector2D>(Triangle2D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector2D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector2D System.Collections.Generic.IReadOnlyList<Vector2D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector2D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Number Area => this.A.X.Multiply(this.C.Y.Subtract(this.B.Y)).Add(this.B.X.Multiply(this.A.Y.Subtract(this.C.Y)).Add(this.C.X.Multiply(this.B.Y.Subtract(this.A.Y)))).Half;
        public Triangle2D Flip => this.C.Tuple3(this.B, this.A);
        public Vector2D Center => this.A.Add(this.B.Add(this.C)).Divide(((Number)3));
        public Vector2D Barycentric(Vector2D uv) => this.A.Barycentric(this.B, this.C, uv);
        public LineArray2D LineArray2D => LineArray2D.New(this.Lines);
        public static implicit operator LineArray2D(Triangle2D t) => t.LineArray2D;
        public TriangleArray2D TriangleArray2D => TriangleArray2D.New(Intrinsics.MakeArray(this));
        public static implicit operator TriangleArray2D(Triangle2D t) => t.TriangleArray2D;
        public Triangle3D To3D => this.A.To3D.Tuple3(this.B.To3D, this.C.To3D);
        public Triangle3D Triangle3D => this.To3D;
        public static implicit operator Triangle3D(Triangle2D x) => x.Triangle3D;
        public static Triangle2D Unit => ((Number)0.5).Negative.Tuple2(((Number)3).Sqrt.Half.Negative).Tuple3(((Number)0.5).Negative.Tuple2(((Number)3).Sqrt.Half), ((Integer)0).Tuple2(((Integer)1)));
        public IArray<Vector2D> Points => Intrinsics.MakeArray<Vector2D>(this.A, this.B, this.C);
        public IArray<Line2D> Lines => Intrinsics.MakeArray<Line2D>(Line2D.New(this.A, this.B), Line2D.New(this.B, this.C), Line2D.New(this.C, this.A));
        // Ambiguous: could not choose a best function implementation for Closed(Triangle2D):Boolean:Boolean.
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var462 = this;
            return numPoints.LinearSpace.Map((x) => _var462.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
        public Vector2D Eval(Number t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Quad2D: IPolygon2D
    {
        [DataMember] public readonly Vector2D A;
        [DataMember] public readonly Vector2D B;
        [DataMember] public readonly Vector2D C;
        [DataMember] public readonly Vector2D D;
        public Quad2D WithA(Vector2D a) => new Quad2D(a, B, C, D);
        public Quad2D WithB(Vector2D b) => new Quad2D(A, b, C, D);
        public Quad2D WithC(Vector2D c) => new Quad2D(A, B, c, D);
        public Quad2D WithD(Vector2D d) => new Quad2D(A, B, C, d);
        public Quad2D(Vector2D a, Vector2D b, Vector2D c, Vector2D d) => (A, B, C, D) = (a, b, c, d);
        public static Quad2D Default = new Quad2D();
        public static Quad2D New(Vector2D a, Vector2D b, Vector2D c, Vector2D d) => new Quad2D(a, b, c, d);
        public Plato.SinglePrecision.Quad2D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision(), D.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Quad2D(Quad2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Vector2D, Vector2D, Vector2D)(Quad2D self) => (self.A, self.B, self.C, self.D);
        public static implicit operator Quad2D((Vector2D, Vector2D, Vector2D, Vector2D) value) => new Quad2D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector2D a, out Vector2D b, out Vector2D c, out Vector2D d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is Quad2D)) return false; var other = (Quad2D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C}, \"D\" = {D} }}";
        public static implicit operator Dynamic(Quad2D self) => new Dynamic(self);
        public static implicit operator Quad2D(Dynamic value) => value.As<Quad2D>();
        public String TypeName => "Quad2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        // Array predefined functions
        public Quad2D(IArray<Vector2D> xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public Quad2D(Vector2D[] xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public static Quad2D New(IArray<Vector2D> xs) => new Quad2D(xs);
        public static Quad2D New(Vector2D[] xs) => new Quad2D(xs);
        public static implicit operator Vector2D[](Quad2D self) => self.ToSystemArray();
        public static implicit operator Array<Vector2D>(Quad2D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector2D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector2D System.Collections.Generic.IReadOnlyList<Vector2D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector2D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Quad2D Flip => this.D.Tuple4(this.C, this.B, this.A);
        public Vector2D Center => this.A.Add(this.B.Add(this.C.Add(this.D))).Divide(((Number)4));
        public Line2D LineA => this.A.Tuple2(this.B);
        public Line2D LineB => this.B.Tuple2(this.C);
        public Line2D LineC => this.C.Tuple2(this.D);
        public Line2D LineD => this.D.Tuple2(this.A);
        public Triangle2D TriangleA => this.A.Tuple3(this.B, this.C);
        public Triangle2D TriangleB => this.C.Tuple3(this.D, this.A);
        public LineArray2D LineArray2D => LineArray2D.New(this.Lines);
        public static implicit operator LineArray2D(Quad2D q) => q.LineArray2D;
        public TriangleArray2D TriangleArray2D => TriangleArray2D.New(this.Triangles);
        public static implicit operator TriangleArray2D(Quad2D q) => q.TriangleArray2D;
        public Quad3D To3D => this.A.To3D.Tuple4(this.B.To3D, this.C.To3D, this.D.To3D);
        public Quad3D Quad3D => this.To3D;
        public static implicit operator Quad3D(Quad2D x) => x.Quad3D;
        public static Quad2D Unit => ((Integer)0).Tuple2(((Integer)0)).Tuple4(((Integer)1).Tuple2(((Integer)0)), ((Integer)1).Tuple2(((Integer)1)), ((Integer)0).Tuple2(((Integer)1)));
        public IArray<Vector2D> Points => Intrinsics.MakeArray<Vector2D>(this.A, this.B, this.C, this.D);
        public IArray<Line2D> Lines => Intrinsics.MakeArray<Line2D>(Line2D.New(this.A, this.B), Line2D.New(this.B, this.C), Line2D.New(this.C, this.D), Line2D.New(this.D, this.A));
        public IArray<Triangle2D> Triangles => Intrinsics.MakeArray<Triangle2D>(Triangle2D.New(this.A, this.B, this.C), Triangle2D.New(this.C, this.D, this.A));
        // Ambiguous: could not choose a best function implementation for Closed(Quad2D):Boolean:Boolean.
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var463 = this;
            return numPoints.LinearSpace.Map((x) => _var463.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
        public Vector2D Eval(Number t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Line2D: IPolyLine2D, IOpenShape2D, IArray<Vector2D>, IInterval<Line2D, Vector2D>
    {
        [DataMember] public readonly Vector2D A;
        [DataMember] public readonly Vector2D B;
        public Line2D WithA(Vector2D a) => new Line2D(a, B);
        public Line2D WithB(Vector2D b) => new Line2D(A, b);
        public Line2D(Vector2D a, Vector2D b) => (A, B) = (a, b);
        public static Line2D Default = new Line2D();
        public static Line2D New(Vector2D a, Vector2D b) => new Line2D(a, b);
        public Plato.SinglePrecision.Line2D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Line2D(Line2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Vector2D)(Line2D self) => (self.A, self.B);
        public static implicit operator Line2D((Vector2D, Vector2D) value) => new Line2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D a, out Vector2D b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Line2D)) return false; var other = (Line2D)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B} }}";
        public static implicit operator Dynamic(Line2D self) => new Dynamic(self);
        public static implicit operator Line2D(Dynamic value) => value.As<Line2D>();
        public String TypeName => "Line2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        Vector2D IInterval<Vector2D>.Start => this.Start;
        Vector2D IInterval<Vector2D>.End => this.End;
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Line2D)b);
        // Array predefined functions
        public Line2D(IArray<Vector2D> xs) : this(xs[0], xs[1]) { }
        public Line2D(Vector2D[] xs) : this(xs[0], xs[1]) { }
        public static Line2D New(IArray<Vector2D> xs) => new Line2D(xs);
        public static Line2D New(Vector2D[] xs) => new Line2D(xs);
        public static implicit operator Vector2D[](Line2D self) => self.ToSystemArray();
        public static implicit operator Array<Vector2D>(Line2D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector2D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector2D System.Collections.Generic.IReadOnlyList<Vector2D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector2D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Number Length => this.B.Subtract(this.A).Length;
        public Vector2D Direction => this.B.Subtract(this.A);
        public Ray2D Ray2D => this.A.Tuple2(this.Direction);
        public static implicit operator Ray2D(Line2D x) => x.Ray2D;
        public Line2D Reverse => this.B.Tuple2(this.A);
        public Vector2D Start => this.A;
        public Vector2D End => this.B;
        public Vector2D Eval(Number t) => this.A.Lerp(this.B, t);
        public Line3D To3D => this.A.To3D.Tuple2(this.B.To3D);
        public Line3D Line3D => this.To3D;
        public static implicit operator Line3D(Line2D x) => x.Line3D;
        public IArray<Vector2D> Points => Intrinsics.MakeArray<Vector2D>(this.A, this.B);
        public IArray<Line2D> Lines => this.Points.WithNext((a, b) => Line2D.New(a, b), this.Closed);
        // Ambiguous: could not choose a best function implementation for Closed(Line2D):Boolean:Boolean.
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var464 = this;
            return numPoints.LinearSpace.Map((x) => _var464.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        public Vector2D Size => this.End.Subtract(this.Start);
        public Vector2D Lerp(Number amount) => this.Start.Lerp(this.End, amount);
        public Vector2D Center => this.Lerp(((Number)0.5));
        public Boolean Contains(Vector2D value) => value.Between(this.Start, this.End);
        public Boolean Contains(Line2D y) => this.Contains(y.Start).And(this.Contains(y.End));
        public Boolean Overlaps(Line2D y) => this.Contains(y.Start).Or(this.Contains(y.End).Or(y.Contains(this.Start).Or(y.Contains(this.End))));
        public Tuple2<Line2D, Line2D> SplitAt(Number t) => this.Left(t).Tuple2(this.Right(t));
        public Tuple2<Line2D, Line2D> Split => this.SplitAt(((Number)0.5));
        public Line2D Left(Number t) => this.Start.Tuple2(this.Lerp(t));
        public Line2D Right(Number t) => this.Lerp(t).Tuple2(this.End);
        public Line2D MoveTo(Vector2D v) => v.Tuple2(v.Add(this.Size));
        public Line2D LeftHalf => this.Left(((Number)0.5));
        public Line2D RightHalf => this.Right(((Number)0.5));
        public Line2D Recenter(Vector2D c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public Line2D Clamp(Line2D y) => this.Clamp(y.Start).Tuple2(this.Clamp(y.End));
        public Vector2D Clamp(Vector2D value) => value.Clamp(this.Start, this.End);
        public IArray<Vector2D> LinearSpace(Integer count){
            var _var465 = this;
            return count.LinearSpace.Map((x) => _var465.Lerp(x));
        }
        public IArray<Vector2D> LinearSpaceExclusive(Integer count){
            var _var466 = this;
            return count.LinearSpaceExclusive.Map((x) => _var466.Lerp(x));
        }
        public IArray<Vector2D> GeometricSpace(Integer count){
            var _var467 = this;
            return count.GeometricSpace.Map((x) => _var467.Lerp(x));
        }
        public IArray<Vector2D> GeometricSpaceExclusive(Integer count){
            var _var468 = this;
            return count.GeometricSpaceExclusive.Map((x) => _var468.Lerp(x));
        }
        public Line2D Subdivide(Number start, Number end) => this.Lerp(start).Tuple2(this.Lerp(end));
        public Line2D Subdivide(NumberInterval subInterval) => this.Subdivide(subInterval.Start, subInterval.End);
        public IArray<Line2D> Subdivide(Integer count){
            var _var469 = this;
            return count.Intervals.Map((i) => _var469.Subdivide(i));
        }
        public Boolean Equals(Line2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Line2D a, Line2D b) => a.Equals(b);
        public Boolean NotEquals(Line2D b) => this.Equals(b).Not;
        public static Boolean operator !=(Line2D a, Line2D b) => a.NotEquals(b);
        public IArray<Line2D> Repeat(Integer n){
            var _var470 = this;
            return n.MapRange((i) => _var470);
        }
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
        public Integer Count => 2;
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Lens: IClosedShape2D
    {
        [DataMember] public readonly Circle A;
        [DataMember] public readonly Circle B;
        public Lens WithA(Circle a) => new Lens(a, B);
        public Lens WithB(Circle b) => new Lens(A, b);
        public Lens(Circle a, Circle b) => (A, B) = (a, b);
        public static Lens Default = new Lens();
        public static Lens New(Circle a, Circle b) => new Lens(a, b);
        public Plato.SinglePrecision.Lens ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Lens(Lens self) => self.ChangePrecision();
        public static implicit operator (Circle, Circle)(Lens self) => (self.A, self.B);
        public static implicit operator Lens((Circle, Circle) value) => new Lens(value.Item1, value.Item2);
        public void Deconstruct(out Circle a, out Circle b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Lens)) return false; var other = (Lens)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B} }}";
        public static implicit operator Dynamic(Lens self) => new Dynamic(self);
        public static implicit operator Lens(Dynamic value) => value.As<Lens>();
        public String TypeName => "Lens";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Rect2D: IPolygon2D
    {
        [DataMember] public readonly Vector2D Center;
        [DataMember] public readonly Vector2D Size;
        public Rect2D WithCenter(Vector2D center) => new Rect2D(center, Size);
        public Rect2D WithSize(Vector2D size) => new Rect2D(Center, size);
        public Rect2D(Vector2D center, Vector2D size) => (Center, Size) = (center, size);
        public static Rect2D Default = new Rect2D();
        public static Rect2D New(Vector2D center, Vector2D size) => new Rect2D(center, size);
        public Plato.SinglePrecision.Rect2D ChangePrecision() => (Center.ChangePrecision(), Size.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Rect2D(Rect2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Vector2D)(Rect2D self) => (self.Center, self.Size);
        public static implicit operator Rect2D((Vector2D, Vector2D) value) => new Rect2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D center, out Vector2D size) { center = Center; size = Size; }
        public override bool Equals(object obj) { if (!(obj is Rect2D)) return false; var other = (Rect2D)obj; return Center.Equals(other.Center) && Size.Equals(other.Size); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Size);
        public override string ToString() => $"{{ \"Center\" = {Center}, \"Size\" = {Size} }}";
        public static implicit operator Dynamic(Rect2D self) => new Dynamic(self);
        public static implicit operator Rect2D(Dynamic value) => value.As<Rect2D>();
        public String TypeName => "Rect2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Size");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Size));
        // Array predefined functions
        public Rect2D(IArray<Vector2D> xs) : this(xs[0], xs[1]) { }
        public Rect2D(Vector2D[] xs) : this(xs[0], xs[1]) { }
        public static Rect2D New(IArray<Vector2D> xs) => new Rect2D(xs);
        public static Rect2D New(Vector2D[] xs) => new Rect2D(xs);
        public static implicit operator Vector2D[](Rect2D self) => self.ToSystemArray();
        public static implicit operator Array<Vector2D>(Rect2D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector2D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector2D System.Collections.Generic.IReadOnlyList<Vector2D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector2D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Number Width => this.Size.X;
        public Number Height => this.Size.Y;
        public Number Left => this.Center.X.Subtract(this.Width.Half);
        public Number Right => this.Left.Add(this.Width);
        public Number Bottom => this.Center.Y.Subtract(this.Height.Half);
        public Number Top => this.Bottom.Add(this.Height);
        public Vector2D BottomLeft => this.Left.Tuple2(this.Bottom);
        public Vector2D BottomRight => this.Right.Tuple2(this.Bottom);
        public Vector2D TopRight => this.Right.Tuple2(this.Top);
        public Vector2D TopLeft => this.Left.Tuple2(this.Top);
        public Quad2D Quad2D => this.BottomLeft.Tuple4(this.BottomRight, this.TopRight, this.TopLeft);
        public static implicit operator Quad2D(Rect2D x) => x.Quad2D;
        public IArray<Vector2D> Points => this.Quad2D;
        public IArray<Line2D> Lines => this.Points.WithNext((a, b) => Line2D.New(a, b), this.Closed);
        // Ambiguous: could not choose a best function implementation for Closed(Rect2D):Boolean:Boolean.
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var471 = this;
            return numPoints.LinearSpace.Map((x) => _var471.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Integer Count => 2;
        public Vector2D At(Integer n) => n == 0 ? Center : n == 1 ? Size : throw new System.IndexOutOfRangeException();
        public Vector2D this[Integer n] => n == 0 ? Center : n == 1 ? Size : throw new System.IndexOutOfRangeException();
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
        public Vector2D Eval(Number t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Ring: IClosedShape2D
    {
        [DataMember] public readonly Vector2D Center;
        [DataMember] public readonly Number InnerRadius;
        [DataMember] public readonly Number OuterRadius;
        public Ring WithCenter(Vector2D center) => new Ring(center, InnerRadius, OuterRadius);
        public Ring WithInnerRadius(Number innerRadius) => new Ring(Center, innerRadius, OuterRadius);
        public Ring WithOuterRadius(Number outerRadius) => new Ring(Center, InnerRadius, outerRadius);
        public Ring(Vector2D center, Number innerRadius, Number outerRadius) => (Center, InnerRadius, OuterRadius) = (center, innerRadius, outerRadius);
        public static Ring Default = new Ring();
        public static Ring New(Vector2D center, Number innerRadius, Number outerRadius) => new Ring(center, innerRadius, outerRadius);
        public Plato.SinglePrecision.Ring ChangePrecision() => (Center.ChangePrecision(), InnerRadius.ChangePrecision(), OuterRadius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Ring(Ring self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Number, Number)(Ring self) => (self.Center, self.InnerRadius, self.OuterRadius);
        public static implicit operator Ring((Vector2D, Number, Number) value) => new Ring(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D center, out Number innerRadius, out Number outerRadius) { center = Center; innerRadius = InnerRadius; outerRadius = OuterRadius; }
        public override bool Equals(object obj) { if (!(obj is Ring)) return false; var other = (Ring)obj; return Center.Equals(other.Center) && InnerRadius.Equals(other.InnerRadius) && OuterRadius.Equals(other.OuterRadius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, InnerRadius, OuterRadius);
        public override string ToString() => $"{{ \"Center\" = {Center}, \"InnerRadius\" = {InnerRadius}, \"OuterRadius\" = {OuterRadius} }}";
        public static implicit operator Dynamic(Ring self) => new Dynamic(self);
        public static implicit operator Ring(Dynamic value) => value.As<Ring>();
        public String TypeName => "Ring";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"InnerRadius", (String)"OuterRadius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(InnerRadius), new Dynamic(OuterRadius));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Arc: IOpenShape2D
    {
        [DataMember] public readonly AnglePair Angles;
        [DataMember] public readonly Circle Circle;
        public Arc WithAngles(AnglePair angles) => new Arc(angles, Circle);
        public Arc WithCircle(Circle circle) => new Arc(Angles, circle);
        public Arc(AnglePair angles, Circle circle) => (Angles, Circle) = (angles, circle);
        public static Arc Default = new Arc();
        public static Arc New(AnglePair angles, Circle circle) => new Arc(angles, circle);
        public Plato.SinglePrecision.Arc ChangePrecision() => (Angles.ChangePrecision(), Circle.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Arc(Arc self) => self.ChangePrecision();
        public static implicit operator (AnglePair, Circle)(Arc self) => (self.Angles, self.Circle);
        public static implicit operator Arc((AnglePair, Circle) value) => new Arc(value.Item1, value.Item2);
        public void Deconstruct(out AnglePair angles, out Circle circle) { angles = Angles; circle = Circle; }
        public override bool Equals(object obj) { if (!(obj is Arc)) return false; var other = (Arc)obj; return Angles.Equals(other.Angles) && Circle.Equals(other.Circle); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Angles, Circle);
        public override string ToString() => $"{{ \"Angles\" = {Angles}, \"Circle\" = {Circle} }}";
        public static implicit operator Dynamic(Arc self) => new Dynamic(self);
        public static implicit operator Arc(Dynamic value) => value.As<Arc>();
        public String TypeName => "Arc";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Angles", (String)"Circle");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Angles), new Dynamic(Circle));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)false);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Sector: IClosedShape2D
    {
        [DataMember] public readonly Arc Arc;
        public Sector WithArc(Arc arc) => new Sector(arc);
        public Sector(Arc arc) => (Arc) = (arc);
        public static Sector Default = new Sector();
        public static Sector New(Arc arc) => new Sector(arc);
        public Plato.SinglePrecision.Sector ChangePrecision() => (Arc.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Sector(Sector self) => self.ChangePrecision();
        public static implicit operator Arc(Sector self) => self.Arc;
        public static implicit operator Sector(Arc value) => new Sector(value);
        public override bool Equals(object obj) { if (!(obj is Sector)) return false; var other = (Sector)obj; return Arc.Equals(other.Arc); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Arc);
        public override string ToString() => $"{{ \"Arc\" = {Arc} }}";
        public static implicit operator Dynamic(Sector self) => new Dynamic(self);
        public static implicit operator Sector(Dynamic value) => value.As<Sector>();
        public String TypeName => "Sector";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Arc");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Arc));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Chord: IClosedShape2D
    {
        [DataMember] public readonly Arc Arc;
        public Chord WithArc(Arc arc) => new Chord(arc);
        public Chord(Arc arc) => (Arc) = (arc);
        public static Chord Default = new Chord();
        public static Chord New(Arc arc) => new Chord(arc);
        public Plato.SinglePrecision.Chord ChangePrecision() => (Arc.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Chord(Chord self) => self.ChangePrecision();
        public static implicit operator Arc(Chord self) => self.Arc;
        public static implicit operator Chord(Arc value) => new Chord(value);
        public override bool Equals(object obj) { if (!(obj is Chord)) return false; var other = (Chord)obj; return Arc.Equals(other.Arc); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Arc);
        public override string ToString() => $"{{ \"Arc\" = {Arc} }}";
        public static implicit operator Dynamic(Chord self) => new Dynamic(self);
        public static implicit operator Chord(Dynamic value) => value.As<Chord>();
        public String TypeName => "Chord";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Arc");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Arc));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Segment: IClosedShape2D
    {
        [DataMember] public readonly Arc Arc;
        public Segment WithArc(Arc arc) => new Segment(arc);
        public Segment(Arc arc) => (Arc) = (arc);
        public static Segment Default = new Segment();
        public static Segment New(Arc arc) => new Segment(arc);
        public Plato.SinglePrecision.Segment ChangePrecision() => (Arc.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Segment(Segment self) => self.ChangePrecision();
        public static implicit operator Arc(Segment self) => self.Arc;
        public static implicit operator Segment(Arc value) => new Segment(value);
        public override bool Equals(object obj) { if (!(obj is Segment)) return false; var other = (Segment)obj; return Arc.Equals(other.Arc); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Arc);
        public override string ToString() => $"{{ \"Arc\" = {Arc} }}";
        public static implicit operator Dynamic(Segment self) => new Dynamic(self);
        public static implicit operator Segment(Dynamic value) => value.As<Segment>();
        public String TypeName => "Segment";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Arc");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Arc));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct RegularPolygon: IPolygon2D
    {
        [DataMember] public readonly Integer NumPoints;
        public RegularPolygon WithNumPoints(Integer numPoints) => new RegularPolygon(numPoints);
        public RegularPolygon(Integer numPoints) => (NumPoints) = (numPoints);
        public static RegularPolygon Default = new RegularPolygon();
        public static RegularPolygon New(Integer numPoints) => new RegularPolygon(numPoints);
        public Plato.SinglePrecision.RegularPolygon ChangePrecision() => (NumPoints.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.RegularPolygon(RegularPolygon self) => self.ChangePrecision();
        public static implicit operator Integer(RegularPolygon self) => self.NumPoints;
        public static implicit operator RegularPolygon(Integer value) => new RegularPolygon(value);
        public override bool Equals(object obj) { if (!(obj is RegularPolygon)) return false; var other = (RegularPolygon)obj; return NumPoints.Equals(other.NumPoints); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(NumPoints);
        public override string ToString() => $"{{ \"NumPoints\" = {NumPoints} }}";
        public static implicit operator Dynamic(RegularPolygon self) => new Dynamic(self);
        public static implicit operator RegularPolygon(Dynamic value) => value.As<RegularPolygon>();
        public String TypeName => "RegularPolygon";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"NumPoints");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(NumPoints));
        // Array predefined functions
        public static implicit operator Vector2D[](RegularPolygon self) => self.ToSystemArray();
        public static implicit operator Array<Vector2D>(RegularPolygon self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector2D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector2D System.Collections.Generic.IReadOnlyList<Vector2D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector2D>.Count => this.Count;
        // Implemented concept functions and type functions
        public IArray<Vector2D> Points => this.NumPoints.CirclePoints;
        public Vector2D At(Integer n) => n.Number.Divide(this.NumPoints).Turns.UnitCircle;
        public Vector2D this[Integer n] => At(n);
        public Integer Count => this.NumPoints;
        public IArray<Line2D> Lines => this.Points.WithNext((a, b) => Line2D.New(a, b), this.Closed);
        // Ambiguous: could not choose a best function implementation for Closed(RegularPolygon):Boolean:Boolean.
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var472 = this;
            return numPoints.LinearSpace.Map((x) => _var472.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
        public Vector2D Eval(Number t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Box2D: IShape2D
    {
        [DataMember] public readonly Vector2D Center;
        [DataMember] public readonly Angle Rotation;
        [DataMember] public readonly Vector2D Extent;
        public Box2D WithCenter(Vector2D center) => new Box2D(center, Rotation, Extent);
        public Box2D WithRotation(Angle rotation) => new Box2D(Center, rotation, Extent);
        public Box2D WithExtent(Vector2D extent) => new Box2D(Center, Rotation, extent);
        public Box2D(Vector2D center, Angle rotation, Vector2D extent) => (Center, Rotation, Extent) = (center, rotation, extent);
        public static Box2D Default = new Box2D();
        public static Box2D New(Vector2D center, Angle rotation, Vector2D extent) => new Box2D(center, rotation, extent);
        public Plato.SinglePrecision.Box2D ChangePrecision() => (Center.ChangePrecision(), Rotation.ChangePrecision(), Extent.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Box2D(Box2D self) => self.ChangePrecision();
        public static implicit operator (Vector2D, Angle, Vector2D)(Box2D self) => (self.Center, self.Rotation, self.Extent);
        public static implicit operator Box2D((Vector2D, Angle, Vector2D) value) => new Box2D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D center, out Angle rotation, out Vector2D extent) { center = Center; rotation = Rotation; extent = Extent; }
        public override bool Equals(object obj) { if (!(obj is Box2D)) return false; var other = (Box2D)obj; return Center.Equals(other.Center) && Rotation.Equals(other.Rotation) && Extent.Equals(other.Extent); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Rotation, Extent);
        public override string ToString() => $"{{ \"Center\" = {Center}, \"Rotation\" = {Rotation}, \"Extent\" = {Extent} }}";
        public static implicit operator Dynamic(Box2D self) => new Dynamic(self);
        public static implicit operator Box2D(Dynamic value) => value.As<Box2D>();
        public String TypeName => "Box2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Rotation", (String)"Extent");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Rotation), new Dynamic(Extent));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Plane: IValue<Plane>
    {
        [DataMember] public readonly Vector3D Normal;
        [DataMember] public readonly Number D;
        public Plane WithNormal(Vector3D normal) => new Plane(normal, D);
        public Plane WithD(Number d) => new Plane(Normal, d);
        public Plane(Vector3D normal, Number d) => (Normal, D) = (normal, d);
        public static Plane Default = new Plane();
        public static Plane New(Vector3D normal, Number d) => new Plane(normal, d);
        public Plato.SinglePrecision.Plane ChangePrecision() => (Normal.ChangePrecision(), D.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Plane(Plane self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Number)(Plane self) => (self.Normal, self.D);
        public static implicit operator Plane((Vector3D, Number) value) => new Plane(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D normal, out Number d) { normal = Normal; d = D; }
        public override bool Equals(object obj) { if (!(obj is Plane)) return false; var other = (Plane)obj; return Normal.Equals(other.Normal) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Normal, D);
        public override string ToString() => $"{{ \"Normal\" = {Normal}, \"D\" = {D} }}";
        public static implicit operator Dynamic(Plane self) => new Dynamic(self);
        public static implicit operator Plane(Dynamic value) => value.As<Plane>();
        public String TypeName => "Plane";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Normal", (String)"D");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Normal), new Dynamic(D));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Plane)b);
        // Implemented concept functions and type functions
        public IArray<Plane> Repeat(Integer n){
            var _var473 = this;
            return n.MapRange((i) => _var473);
        }
        public Boolean Equals(Plane b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Plane a, Plane b) => a.Equals(b);
        public Boolean NotEquals(Plane b) => this.Equals(b).Not;
        public static Boolean operator !=(Plane a, Plane b) => a.NotEquals(b);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Bounds3D: IBounds<Bounds3D, Vector3D>, IDeformable3D<Bounds3D>
    {
        [DataMember] public readonly Vector3D Min;
        [DataMember] public readonly Vector3D Max;
        public Bounds3D WithMin(Vector3D min) => new Bounds3D(min, Max);
        public Bounds3D WithMax(Vector3D max) => new Bounds3D(Min, max);
        public Bounds3D(Vector3D min, Vector3D max) => (Min, Max) = (min, max);
        public static Bounds3D Default = new Bounds3D();
        public static Bounds3D New(Vector3D min, Vector3D max) => new Bounds3D(min, max);
        public Plato.SinglePrecision.Bounds3D ChangePrecision() => (Min.ChangePrecision(), Max.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Bounds3D(Bounds3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Vector3D)(Bounds3D self) => (self.Min, self.Max);
        public static implicit operator Bounds3D((Vector3D, Vector3D) value) => new Bounds3D(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D min, out Vector3D max) { min = Min; max = Max; }
        public override bool Equals(object obj) { if (!(obj is Bounds3D)) return false; var other = (Bounds3D)obj; return Min.Equals(other.Min) && Max.Equals(other.Max); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Min, Max);
        public override string ToString() => $"{{ \"Min\" = {Min}, \"Max\" = {Max} }}";
        public static implicit operator Dynamic(Bounds3D self) => new Dynamic(self);
        public static implicit operator Bounds3D(Dynamic value) => value.As<Bounds3D>();
        public String TypeName => "Bounds3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Min", (String)"Max");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Min), new Dynamic(Max));
        Vector3D IBounds<Bounds3D, Vector3D>.Min => Min;
        Vector3D IBounds<Bounds3D, Vector3D>.Max => Max;
        Vector3D IBounds<Vector3D>.Min => this.Min;
        Vector3D IBounds<Vector3D>.Max => this.Max;
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Bounds3D)b);
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        // Implemented concept functions and type functions
        public Vector3D Center => this.Min.Add(this.Max).Half;
        public IArray<Vector3D> Corners => Intrinsics.MakeArray<Vector3D>(this.Min.X.Tuple3(this.Min.Y, this.Min.Z), this.Max.X.Tuple3(this.Min.Y, this.Min.Z), this.Min.X.Tuple3(this.Max.Y, this.Min.Z), this.Max.X.Tuple3(this.Max.Y, this.Min.Z), this.Min.X.Tuple3(this.Min.Y, this.Max.Z), this.Max.X.Tuple3(this.Min.Y, this.Max.Z), this.Min.X.Tuple3(this.Max.Y, this.Max.Z), this.Max.X.Tuple3(this.Max.Y, this.Max.Z));
        public static Bounds3D Empty => Vector3D.MaxValue.Tuple2(Vector3D.MinValue);
        public Bounds3D Deform(System.Func<Vector3D, Vector3D> f) => this.Corners.Map(f).Bounds();
        public Vector3D Size => this.Max.Subtract(this.Min);
        public Vector3D Lerp(Number amount) => this.Min.Lerp(this.Max, amount);
        public Boolean Contains(Vector3D value) => value.Between(this.Min, this.Max);
        public Boolean Contains(Bounds3D y) => this.Contains(y.Min).And(this.Contains(y.Max));
        public Boolean Overlaps(Bounds3D y) => this.Contains(y.Min).Or(this.Contains(y.Max).Or(y.Contains(this.Min).Or(y.Contains(this.Max))));
        public Bounds3D Recenter(Vector3D c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public Bounds3D Clamp(Bounds3D y) => this.Clamp(y.Min).Tuple2(this.Clamp(y.Max));
        public Vector3D Clamp(Vector3D value) => value.Clamp(this.Min, this.Max);
        public Bounds3D Include(Vector3D value) => this.Min.Min(value).Tuple2(this.Max.Max(value));
        public Bounds3D Include(Bounds3D y) => this.Include(y.Min).Include(y.Max);
        public Boolean Equals(Bounds3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Bounds3D a, Bounds3D b) => a.Equals(b);
        public Boolean NotEquals(Bounds3D b) => this.Equals(b).Not;
        public static Boolean operator !=(Bounds3D a, Bounds3D b) => a.NotEquals(b);
        public IArray<Bounds3D> Repeat(Integer n){
            var _var474 = this;
            return n.MapRange((i) => _var474);
        }
        public Bounds3D Deform(ITransform3D t){
            var _var475 = t;
            return this.Deform((v) => _var475.Transform(v));
        }
        public Bounds3D Translate(Vector3D v){
            var _var476 = v;
            return this.Deform((p) => p.Add(_var476));
        }
        public Bounds3D Rotate(Quaternion q) => this.Deform(q);
        public Bounds3D Scale(Vector3D v){
            var _var477 = v;
            return this.Deform((p) => p.Multiply(_var477));
        }
        public Bounds3D Scale(Number s){
            var _var478 = s;
            return this.Deform((p) => p.Multiply(_var478));
        }
        public Bounds3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public Bounds3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public Bounds3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public Bounds3D TranslateX(Number s){
            var _var479 = s;
            return this.Deform((p) => p.Add(_var479.Tuple3(((Integer)0), ((Integer)0))));
        }
        public Bounds3D TranslateY(Number s){
            var _var480 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var480, ((Integer)0))));
        }
        public Bounds3D TranslateZ(Number s){
            var _var481 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var481)));
        }
        public Bounds3D ScaleX(Number s){
            var _var482 = s;
            return this.Deform((p) => p.Multiply(_var482.Tuple3(((Integer)1), ((Integer)1))));
        }
        public Bounds3D ScaleY(Number s){
            var _var483 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var483, ((Integer)1))));
        }
        public Bounds3D ScaleZ(Number s){
            var _var484 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var484)));
        }
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Line3D: IPolyLine3D<Line3D>, IOpenShape3D, IDeformable3D<Line3D>, IArray<Vector3D>, IInterval<Line3D, Vector3D>
    {
        [DataMember] public readonly Vector3D A;
        [DataMember] public readonly Vector3D B;
        public Line3D WithA(Vector3D a) => new Line3D(a, B);
        public Line3D WithB(Vector3D b) => new Line3D(A, b);
        public Line3D(Vector3D a, Vector3D b) => (A, B) = (a, b);
        public static Line3D Default = new Line3D();
        public static Line3D New(Vector3D a, Vector3D b) => new Line3D(a, b);
        public Plato.SinglePrecision.Line3D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Line3D(Line3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Vector3D)(Line3D self) => (self.A, self.B);
        public static implicit operator Line3D((Vector3D, Vector3D) value) => new Line3D(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D a, out Vector3D b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Line3D)) return false; var other = (Line3D)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B} }}";
        public static implicit operator Dynamic(Line3D self) => new Dynamic(self);
        public static implicit operator Line3D(Dynamic value) => value.As<Line3D>();
        public String TypeName => "Line3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        Vector3D IInterval<Vector3D>.Start => this.Start;
        Vector3D IInterval<Vector3D>.End => this.End;
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Line3D)b);
        // Array predefined functions
        public Line3D(IArray<Vector3D> xs) : this(xs[0], xs[1]) { }
        public Line3D(Vector3D[] xs) : this(xs[0], xs[1]) { }
        public static Line3D New(IArray<Vector3D> xs) => new Line3D(xs);
        public static Line3D New(Vector3D[] xs) => new Line3D(xs);
        public static implicit operator Vector3D[](Line3D self) => self.ToSystemArray();
        public static implicit operator Array<Vector3D>(Line3D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector3D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector3D System.Collections.Generic.IReadOnlyList<Vector3D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector3D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Number Length => this.B.Subtract(this.A).Length;
        public Vector3D Direction => this.B.Subtract(this.A);
        public Ray3D Ray3D => this.A.Tuple2(this.Direction.Normalize);
        public static implicit operator Ray3D(Line3D x) => x.Ray3D;
        public Line3D Reverse => this.B.Tuple2(this.A);
        public Line3D Deform(System.Func<Vector3D, Vector3D> f) => f.Invoke(this.A).Tuple2(f.Invoke(this.B));
        public Bounds3D Bounds3D => this.A.Min(this.B).Tuple2(this.A.Max(this.B));
        public static implicit operator Bounds3D(Line3D x) => x.Bounds3D;
        public Vector3D Start => this.A;
        public Vector3D End => this.B;
        public Vector3D Eval(Number t) => this.A.Lerp(this.B, t);
        public IArray<Vector3D> Points => Intrinsics.MakeArray<Vector3D>(this.A, this.B);
        public IArray<Line3D> Lines => this.Points.WithNext((a, b) => Line3D.New(a, b), this.Closed);
        public Line3D Deform(ITransform3D t){
            var _var485 = t;
            return this.Deform((v) => _var485.Transform(v));
        }
        public Line3D Translate(Vector3D v){
            var _var486 = v;
            return this.Deform((p) => p.Add(_var486));
        }
        public Line3D Rotate(Quaternion q) => this.Deform(q);
        public Line3D Scale(Vector3D v){
            var _var487 = v;
            return this.Deform((p) => p.Multiply(_var487));
        }
        public Line3D Scale(Number s){
            var _var488 = s;
            return this.Deform((p) => p.Multiply(_var488));
        }
        public Line3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public Line3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public Line3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public Line3D TranslateX(Number s){
            var _var489 = s;
            return this.Deform((p) => p.Add(_var489.Tuple3(((Integer)0), ((Integer)0))));
        }
        public Line3D TranslateY(Number s){
            var _var490 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var490, ((Integer)0))));
        }
        public Line3D TranslateZ(Number s){
            var _var491 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var491)));
        }
        public Line3D ScaleX(Number s){
            var _var492 = s;
            return this.Deform((p) => p.Multiply(_var492.Tuple3(((Integer)1), ((Integer)1))));
        }
        public Line3D ScaleY(Number s){
            var _var493 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var493, ((Integer)1))));
        }
        public Line3D ScaleZ(Number s){
            var _var494 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var494)));
        }
        // Ambiguous: could not choose a best function implementation for Closed(Line3D):Boolean:Boolean.
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector3D> Sample(Integer numPoints){
            var _var495 = this;
            return numPoints.LinearSpace.Map((x) => _var495.Eval(x));
        }
        public PolyLine3D ToPolyLine3D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        public Vector3D Size => this.End.Subtract(this.Start);
        public Vector3D Lerp(Number amount) => this.Start.Lerp(this.End, amount);
        public Vector3D Center => this.Lerp(((Number)0.5));
        public Boolean Contains(Vector3D value) => value.Between(this.Start, this.End);
        public Boolean Contains(Line3D y) => this.Contains(y.Start).And(this.Contains(y.End));
        public Boolean Overlaps(Line3D y) => this.Contains(y.Start).Or(this.Contains(y.End).Or(y.Contains(this.Start).Or(y.Contains(this.End))));
        public Tuple2<Line3D, Line3D> SplitAt(Number t) => this.Left(t).Tuple2(this.Right(t));
        public Tuple2<Line3D, Line3D> Split => this.SplitAt(((Number)0.5));
        public Line3D Left(Number t) => this.Start.Tuple2(this.Lerp(t));
        public Line3D Right(Number t) => this.Lerp(t).Tuple2(this.End);
        public Line3D MoveTo(Vector3D v) => v.Tuple2(v.Add(this.Size));
        public Line3D LeftHalf => this.Left(((Number)0.5));
        public Line3D RightHalf => this.Right(((Number)0.5));
        public Line3D Recenter(Vector3D c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public Line3D Clamp(Line3D y) => this.Clamp(y.Start).Tuple2(this.Clamp(y.End));
        public Vector3D Clamp(Vector3D value) => value.Clamp(this.Start, this.End);
        public IArray<Vector3D> LinearSpace(Integer count){
            var _var496 = this;
            return count.LinearSpace.Map((x) => _var496.Lerp(x));
        }
        public IArray<Vector3D> LinearSpaceExclusive(Integer count){
            var _var497 = this;
            return count.LinearSpaceExclusive.Map((x) => _var497.Lerp(x));
        }
        public IArray<Vector3D> GeometricSpace(Integer count){
            var _var498 = this;
            return count.GeometricSpace.Map((x) => _var498.Lerp(x));
        }
        public IArray<Vector3D> GeometricSpaceExclusive(Integer count){
            var _var499 = this;
            return count.GeometricSpaceExclusive.Map((x) => _var499.Lerp(x));
        }
        public Line3D Subdivide(Number start, Number end) => this.Lerp(start).Tuple2(this.Lerp(end));
        public Line3D Subdivide(NumberInterval subInterval) => this.Subdivide(subInterval.Start, subInterval.End);
        public IArray<Line3D> Subdivide(Integer count){
            var _var500 = this;
            return count.Intervals.Map((i) => _var500.Subdivide(i));
        }
        public Boolean Equals(Line3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Line3D a, Line3D b) => a.Equals(b);
        public Boolean NotEquals(Line3D b) => this.Equals(b).Not;
        public static Boolean operator !=(Line3D a, Line3D b) => a.NotEquals(b);
        public IArray<Line3D> Repeat(Integer n){
            var _var501 = this;
            return n.MapRange((i) => _var501);
        }
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Integer Count => 2;
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Ray3D: IValue<Ray3D>, IDeformable3D<Ray3D>
    {
        [DataMember] public readonly Vector3D Origin;
        [DataMember] public readonly Vector3D Direction;
        public Ray3D WithOrigin(Vector3D origin) => new Ray3D(origin, Direction);
        public Ray3D WithDirection(Vector3D direction) => new Ray3D(Origin, direction);
        public Ray3D(Vector3D origin, Vector3D direction) => (Origin, Direction) = (origin, direction);
        public static Ray3D Default = new Ray3D();
        public static Ray3D New(Vector3D origin, Vector3D direction) => new Ray3D(origin, direction);
        public Plato.SinglePrecision.Ray3D ChangePrecision() => (Origin.ChangePrecision(), Direction.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Ray3D(Ray3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Vector3D)(Ray3D self) => (self.Origin, self.Direction);
        public static implicit operator Ray3D((Vector3D, Vector3D) value) => new Ray3D(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D origin, out Vector3D direction) { origin = Origin; direction = Direction; }
        public override bool Equals(object obj) { if (!(obj is Ray3D)) return false; var other = (Ray3D)obj; return Origin.Equals(other.Origin) && Direction.Equals(other.Direction); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Origin, Direction);
        public override string ToString() => $"{{ \"Origin\" = {Origin}, \"Direction\" = {Direction} }}";
        public static implicit operator Dynamic(Ray3D self) => new Dynamic(self);
        public static implicit operator Ray3D(Dynamic value) => value.As<Ray3D>();
        public String TypeName => "Ray3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Origin", (String)"Direction");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Origin), new Dynamic(Direction));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Ray3D)b);
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        // Implemented concept functions and type functions
        public Angle Angle(Ray3D b) => this.Direction.Angle(b.Direction);
        public Ray3D Deform(System.Func<Vector3D, Vector3D> f) => f.Invoke(this.Origin).Tuple2(f.Invoke(this.Origin.Add(this.Direction)).Normalize);
        public Line3D Line3D => this.Origin.Tuple2(this.Origin.Add(this.Direction));
        public static implicit operator Line3D(Ray3D r) => r.Line3D;
        public Ray3D Reverse => this.Origin.Tuple2(this.Direction.Negative);
        public IArray<Ray3D> Repeat(Integer n){
            var _var502 = this;
            return n.MapRange((i) => _var502);
        }
        public Boolean Equals(Ray3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Ray3D a, Ray3D b) => a.Equals(b);
        public Boolean NotEquals(Ray3D b) => this.Equals(b).Not;
        public static Boolean operator !=(Ray3D a, Ray3D b) => a.NotEquals(b);
        public Ray3D Deform(ITransform3D t){
            var _var503 = t;
            return this.Deform((v) => _var503.Transform(v));
        }
        public Ray3D Translate(Vector3D v){
            var _var504 = v;
            return this.Deform((p) => p.Add(_var504));
        }
        public Ray3D Rotate(Quaternion q) => this.Deform(q);
        public Ray3D Scale(Vector3D v){
            var _var505 = v;
            return this.Deform((p) => p.Multiply(_var505));
        }
        public Ray3D Scale(Number s){
            var _var506 = s;
            return this.Deform((p) => p.Multiply(_var506));
        }
        public Ray3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public Ray3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public Ray3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public Ray3D TranslateX(Number s){
            var _var507 = s;
            return this.Deform((p) => p.Add(_var507.Tuple3(((Integer)0), ((Integer)0))));
        }
        public Ray3D TranslateY(Number s){
            var _var508 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var508, ((Integer)0))));
        }
        public Ray3D TranslateZ(Number s){
            var _var509 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var509)));
        }
        public Ray3D ScaleX(Number s){
            var _var510 = s;
            return this.Deform((p) => p.Multiply(_var510.Tuple3(((Integer)1), ((Integer)1))));
        }
        public Ray3D ScaleY(Number s){
            var _var511 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var511, ((Integer)1))));
        }
        public Ray3D ScaleZ(Number s){
            var _var512 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var512)));
        }
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Triangle3D: IPolygon3D<Triangle3D>, IDeformable3D<Triangle3D>
    {
        [DataMember] public readonly Vector3D A;
        [DataMember] public readonly Vector3D B;
        [DataMember] public readonly Vector3D C;
        public Triangle3D WithA(Vector3D a) => new Triangle3D(a, B, C);
        public Triangle3D WithB(Vector3D b) => new Triangle3D(A, b, C);
        public Triangle3D WithC(Vector3D c) => new Triangle3D(A, B, c);
        public Triangle3D(Vector3D a, Vector3D b, Vector3D c) => (A, B, C) = (a, b, c);
        public static Triangle3D Default = new Triangle3D();
        public static Triangle3D New(Vector3D a, Vector3D b, Vector3D c) => new Triangle3D(a, b, c);
        public Plato.SinglePrecision.Triangle3D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Triangle3D(Triangle3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Vector3D, Vector3D)(Triangle3D self) => (self.A, self.B, self.C);
        public static implicit operator Triangle3D((Vector3D, Vector3D, Vector3D) value) => new Triangle3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D a, out Vector3D b, out Vector3D c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is Triangle3D)) return false; var other = (Triangle3D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C} }}";
        public static implicit operator Dynamic(Triangle3D self) => new Dynamic(self);
        public static implicit operator Triangle3D(Dynamic value) => value.As<Triangle3D>();
        public String TypeName => "Triangle3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        // Array predefined functions
        public Triangle3D(IArray<Vector3D> xs) : this(xs[0], xs[1], xs[2]) { }
        public Triangle3D(Vector3D[] xs) : this(xs[0], xs[1], xs[2]) { }
        public static Triangle3D New(IArray<Vector3D> xs) => new Triangle3D(xs);
        public static Triangle3D New(Vector3D[] xs) => new Triangle3D(xs);
        public static implicit operator Vector3D[](Triangle3D self) => self.ToSystemArray();
        public static implicit operator Array<Vector3D>(Triangle3D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector3D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector3D System.Collections.Generic.IReadOnlyList<Vector3D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector3D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Triangle3D Flip => this.C.Tuple3(this.B, this.A);
        public Vector3D Normal => this.B.Subtract(this.A).Cross(this.C.Subtract(this.A)).Normalize;
        public Vector3D Center => this.A.Add(this.B.Add(this.C)).Divide(((Number)3));
        public Plane Plane => this.Normal.Tuple2(this.Normal.Dot(this.A));
        public static implicit operator Plane(Triangle3D t) => t.Plane;
        // Ambiguous: could not choose a best function implementation for Barycentric(Triangle3D, Vector2D):Vector3D:Vector3D.
        public Vector3D Barycentric(Vector2D uv) => this.A.Barycentric(this.B, this.C, uv);
        public Line3D LineA => this.A.Tuple2(this.B);
        public Line3D LineB => this.B.Tuple2(this.C);
        public Line3D LineC => this.C.Tuple2(this.A);
        public Triangle3D Deform(System.Func<Vector3D, Vector3D> f) => f.Invoke(this.A).Tuple3(f.Invoke(this.B), f.Invoke(this.C));
        public LineArray3D LineArray3D => LineArray3D.New(this.Lines);
        public static implicit operator LineArray3D(Triangle3D t) => t.LineArray3D;
        public TriangleArray3D TriangleArray3D => TriangleArray3D.New(Intrinsics.MakeArray(this));
        public static implicit operator TriangleArray3D(Triangle3D t) => t.TriangleArray3D;
        public TriangleMesh3D TriangleMesh3D => this.TriangleArray3D;
        public static implicit operator TriangleMesh3D(Triangle3D g) => g.TriangleMesh3D;
        public IArray<Vector3D> Points => Intrinsics.MakeArray<Vector3D>(this.A, this.B, this.C);
        public IArray<Line3D> Lines => Intrinsics.MakeArray<Line3D>(Line3D.New(this.A, this.B), Line3D.New(this.B, this.C), Line3D.New(this.C, this.A));
        public Triangle3D Deform(ITransform3D t){
            var _var513 = t;
            return this.Deform((v) => _var513.Transform(v));
        }
        public Triangle3D Translate(Vector3D v){
            var _var514 = v;
            return this.Deform((p) => p.Add(_var514));
        }
        public Triangle3D Rotate(Quaternion q) => this.Deform(q);
        public Triangle3D Scale(Vector3D v){
            var _var515 = v;
            return this.Deform((p) => p.Multiply(_var515));
        }
        public Triangle3D Scale(Number s){
            var _var516 = s;
            return this.Deform((p) => p.Multiply(_var516));
        }
        public Triangle3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public Triangle3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public Triangle3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public Triangle3D TranslateX(Number s){
            var _var517 = s;
            return this.Deform((p) => p.Add(_var517.Tuple3(((Integer)0), ((Integer)0))));
        }
        public Triangle3D TranslateY(Number s){
            var _var518 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var518, ((Integer)0))));
        }
        public Triangle3D TranslateZ(Number s){
            var _var519 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var519)));
        }
        public Triangle3D ScaleX(Number s){
            var _var520 = s;
            return this.Deform((p) => p.Multiply(_var520.Tuple3(((Integer)1), ((Integer)1))));
        }
        public Triangle3D ScaleY(Number s){
            var _var521 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var521, ((Integer)1))));
        }
        public Triangle3D ScaleZ(Number s){
            var _var522 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var522)));
        }
        // Ambiguous: could not choose a best function implementation for Closed(Triangle3D):Boolean:Boolean.
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector3D> Sample(Integer numPoints){
            var _var523 = this;
            return numPoints.LinearSpace.Map((x) => _var523.Eval(x));
        }
        public PolyLine3D ToPolyLine3D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Number t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Quad3D: IPolygon3D<Quad3D>, IDeformable3D<Quad3D>
    {
        [DataMember] public readonly Vector3D A;
        [DataMember] public readonly Vector3D B;
        [DataMember] public readonly Vector3D C;
        [DataMember] public readonly Vector3D D;
        public Quad3D WithA(Vector3D a) => new Quad3D(a, B, C, D);
        public Quad3D WithB(Vector3D b) => new Quad3D(A, b, C, D);
        public Quad3D WithC(Vector3D c) => new Quad3D(A, B, c, D);
        public Quad3D WithD(Vector3D d) => new Quad3D(A, B, C, d);
        public Quad3D(Vector3D a, Vector3D b, Vector3D c, Vector3D d) => (A, B, C, D) = (a, b, c, d);
        public static Quad3D Default = new Quad3D();
        public static Quad3D New(Vector3D a, Vector3D b, Vector3D c, Vector3D d) => new Quad3D(a, b, c, d);
        public Plato.SinglePrecision.Quad3D ChangePrecision() => (A.ChangePrecision(), B.ChangePrecision(), C.ChangePrecision(), D.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Quad3D(Quad3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Vector3D, Vector3D, Vector3D)(Quad3D self) => (self.A, self.B, self.C, self.D);
        public static implicit operator Quad3D((Vector3D, Vector3D, Vector3D, Vector3D) value) => new Quad3D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector3D a, out Vector3D b, out Vector3D c, out Vector3D d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is Quad3D)) return false; var other = (Quad3D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C}, \"D\" = {D} }}";
        public static implicit operator Dynamic(Quad3D self) => new Dynamic(self);
        public static implicit operator Quad3D(Dynamic value) => value.As<Quad3D>();
        public String TypeName => "Quad3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        // Array predefined functions
        public Quad3D(IArray<Vector3D> xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public Quad3D(Vector3D[] xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public static Quad3D New(IArray<Vector3D> xs) => new Quad3D(xs);
        public static Quad3D New(Vector3D[] xs) => new Quad3D(xs);
        public static implicit operator Vector3D[](Quad3D self) => self.ToSystemArray();
        public static implicit operator Array<Vector3D>(Quad3D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector3D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector3D System.Collections.Generic.IReadOnlyList<Vector3D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector3D>.Count => this.Count;
        // Implemented concept functions and type functions
        public Quad3D Flip => this.D.Tuple4(this.C, this.B, this.A);
        public Vector3D Center => this.A.Add(this.B.Add(this.C.Add(this.D))).Divide(((Number)4));
        public Line3D LineA => this.A.Tuple2(this.B);
        public Line3D LineB => this.B.Tuple2(this.C);
        public Line3D LineC => this.C.Tuple2(this.D);
        public Line3D LineD => this.D.Tuple2(this.A);
        public Triangle3D TriangleA => this.A.Tuple3(this.B, this.C);
        public Triangle3D TriangleB => this.C.Tuple3(this.D, this.A);
        public Quad3D Deform(System.Func<Vector3D, Vector3D> f) => f.Invoke(this.A).Tuple4(f.Invoke(this.B), f.Invoke(this.C), f.Invoke(this.D));
        public LineArray3D LineArray3D => LineArray3D.New(this.Lines);
        public static implicit operator LineArray3D(Quad3D q) => q.LineArray3D;
        public TriangleArray3D TriangleArray3D => TriangleArray3D.New(this.Triangles);
        public static implicit operator TriangleArray3D(Quad3D q) => q.TriangleArray3D;
        public QuadArray3D QuadArray3D => QuadArray3D.New(Intrinsics.MakeArray(this));
        public static implicit operator QuadArray3D(Quad3D q) => q.QuadArray3D;
        public TriangleMesh3D TriangleMesh3D => this.TriangleArray3D;
        public static implicit operator TriangleMesh3D(Quad3D g) => g.TriangleMesh3D;
        public IArray<Vector3D> Points => Intrinsics.MakeArray<Vector3D>(this.A, this.B, this.C, this.D);
        public IArray<Line3D> Lines => Intrinsics.MakeArray<Line3D>(Line3D.New(this.A, this.B), Line3D.New(this.B, this.C), Line3D.New(this.C, this.D), Line3D.New(this.D, this.A));
        public IArray<Triangle3D> Triangles => Intrinsics.MakeArray<Triangle3D>(Triangle3D.New(this.A, this.B, this.C), Triangle3D.New(this.C, this.D, this.A));
        public Quad3D Deform(ITransform3D t){
            var _var524 = t;
            return this.Deform((v) => _var524.Transform(v));
        }
        public Quad3D Translate(Vector3D v){
            var _var525 = v;
            return this.Deform((p) => p.Add(_var525));
        }
        public Quad3D Rotate(Quaternion q) => this.Deform(q);
        public Quad3D Scale(Vector3D v){
            var _var526 = v;
            return this.Deform((p) => p.Multiply(_var526));
        }
        public Quad3D Scale(Number s){
            var _var527 = s;
            return this.Deform((p) => p.Multiply(_var527));
        }
        public Quad3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public Quad3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public Quad3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public Quad3D TranslateX(Number s){
            var _var528 = s;
            return this.Deform((p) => p.Add(_var528.Tuple3(((Integer)0), ((Integer)0))));
        }
        public Quad3D TranslateY(Number s){
            var _var529 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var529, ((Integer)0))));
        }
        public Quad3D TranslateZ(Number s){
            var _var530 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var530)));
        }
        public Quad3D ScaleX(Number s){
            var _var531 = s;
            return this.Deform((p) => p.Multiply(_var531.Tuple3(((Integer)1), ((Integer)1))));
        }
        public Quad3D ScaleY(Number s){
            var _var532 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var532, ((Integer)1))));
        }
        public Quad3D ScaleZ(Number s){
            var _var533 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var533)));
        }
        // Ambiguous: could not choose a best function implementation for Closed(Quad3D):Boolean:Boolean.
        public Boolean Closed => ((Boolean)false);
        public IArray<Vector3D> Sample(Integer numPoints){
            var _var534 = this;
            return numPoints.LinearSpace.Map((x) => _var534.Eval(x));
        }
        public PolyLine3D ToPolyLine3D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Number t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct LineMesh3D: ILineMesh3D<LineMesh3D>
    {
        [DataMember] public readonly IArray<Vector3D> Points;
        [DataMember] public readonly IArray<Integer> Indices;
        public LineMesh3D WithPoints(IArray<Vector3D> points) => new LineMesh3D(points, Indices);
        public LineMesh3D WithIndices(IArray<Integer> indices) => new LineMesh3D(Points, indices);
        public LineMesh3D(IArray<Vector3D> points, IArray<Integer> indices) => (Points, Indices) = (points, indices);
        public static LineMesh3D Default = new LineMesh3D();
        public static LineMesh3D New(IArray<Vector3D> points, IArray<Integer> indices) => new LineMesh3D(points, indices);
        public static implicit operator (IArray<Vector3D>, IArray<Integer>)(LineMesh3D self) => (self.Points, self.Indices);
        public static implicit operator LineMesh3D((IArray<Vector3D>, IArray<Integer>) value) => new LineMesh3D(value.Item1, value.Item2);
        public void Deconstruct(out IArray<Vector3D> points, out IArray<Integer> indices) { points = Points; indices = Indices; }
        public override bool Equals(object obj) { if (!(obj is LineMesh3D)) return false; var other = (LineMesh3D)obj; return Points.Equals(other.Points) && Indices.Equals(other.Indices); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Points, Indices);
        public override string ToString() => $"{{ \"Points\" = {Points}, \"Indices\" = {Indices} }}";
        public static implicit operator Dynamic(LineMesh3D self) => new Dynamic(self);
        public static implicit operator LineMesh3D(Dynamic value) => value.As<LineMesh3D>();
        public String TypeName => "LineMesh3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Points", (String)"Indices");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Points), new Dynamic(Indices));
        IArray<Integer> IIndexedGeometry.Indices => Indices;
        IArray<Vector3D> IPointGeometry3D<LineMesh3D>.Points => Points;
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        IArray<Line3D> ILineGeometry3D.Lines => this.Lines;
        // Implemented concept functions and type functions
        public LineMesh3D Deform(System.Func<Vector3D, Vector3D> f) => this.Points.Map(f).Tuple2(this.Indices);
        public IArray<Line3D> Lines => this.AllFaceVertices.Map((xs) => Line3D.New(xs.At(((Integer)0)), xs.At(((Integer)1))));
        public IArray<Vector3D> FaceVertices(Integer f){
            var _var535 = this;
            return this.FaceIndices(f).Map((i) => _var535.Vertex(i));
        }
        public Vector3D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector3D> Vertices(IArray<Integer> xs){
            var _var536 = this;
            return xs.Map((i) => _var536.Vertex(i));
        }
        public IArray<IArray<Vector3D>> AllFaceVertices { get {
            var _var537 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var537.Vertices(xs));
        }
         } public IArray<Vector3D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public LineMesh3D Deform(ITransform3D t){
            var _var538 = t;
            return this.Deform((v) => _var538.Transform(v));
        }
        public LineMesh3D Translate(Vector3D v){
            var _var539 = v;
            return this.Deform((p) => p.Add(_var539));
        }
        public LineMesh3D Rotate(Quaternion q) => this.Deform(q);
        public LineMesh3D Scale(Vector3D v){
            var _var540 = v;
            return this.Deform((p) => p.Multiply(_var540));
        }
        public LineMesh3D Scale(Number s){
            var _var541 = s;
            return this.Deform((p) => p.Multiply(_var541));
        }
        public LineMesh3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public LineMesh3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public LineMesh3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public LineMesh3D TranslateX(Number s){
            var _var542 = s;
            return this.Deform((p) => p.Add(_var542.Tuple3(((Integer)0), ((Integer)0))));
        }
        public LineMesh3D TranslateY(Number s){
            var _var543 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var543, ((Integer)0))));
        }
        public LineMesh3D TranslateZ(Number s){
            var _var544 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var544)));
        }
        public LineMesh3D ScaleX(Number s){
            var _var545 = s;
            return this.Deform((p) => p.Multiply(_var545.Tuple3(((Integer)1), ((Integer)1))));
        }
        public LineMesh3D ScaleY(Number s){
            var _var546 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var546, ((Integer)1))));
        }
        public LineMesh3D ScaleZ(Number s){
            var _var547 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var547)));
        }
        public Integer PrimitiveSize => ((Integer)2);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct TriangleMesh3D: ITriangleMesh3D<TriangleMesh3D>
    {
        [DataMember] public readonly IArray<Vector3D> Points;
        [DataMember] public readonly IArray<Integer> Indices;
        public TriangleMesh3D WithPoints(IArray<Vector3D> points) => new TriangleMesh3D(points, Indices);
        public TriangleMesh3D WithIndices(IArray<Integer> indices) => new TriangleMesh3D(Points, indices);
        public TriangleMesh3D(IArray<Vector3D> points, IArray<Integer> indices) => (Points, Indices) = (points, indices);
        public static TriangleMesh3D Default = new TriangleMesh3D();
        public static TriangleMesh3D New(IArray<Vector3D> points, IArray<Integer> indices) => new TriangleMesh3D(points, indices);
        public static implicit operator (IArray<Vector3D>, IArray<Integer>)(TriangleMesh3D self) => (self.Points, self.Indices);
        public static implicit operator TriangleMesh3D((IArray<Vector3D>, IArray<Integer>) value) => new TriangleMesh3D(value.Item1, value.Item2);
        public void Deconstruct(out IArray<Vector3D> points, out IArray<Integer> indices) { points = Points; indices = Indices; }
        public override bool Equals(object obj) { if (!(obj is TriangleMesh3D)) return false; var other = (TriangleMesh3D)obj; return Points.Equals(other.Points) && Indices.Equals(other.Indices); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Points, Indices);
        public override string ToString() => $"{{ \"Points\" = {Points}, \"Indices\" = {Indices} }}";
        public static implicit operator Dynamic(TriangleMesh3D self) => new Dynamic(self);
        public static implicit operator TriangleMesh3D(Dynamic value) => value.As<TriangleMesh3D>();
        public String TypeName => "TriangleMesh3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Points", (String)"Indices");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Points), new Dynamic(Indices));
        IArray<Integer> IIndexedGeometry.Indices => Indices;
        IArray<Vector3D> IPointGeometry3D<TriangleMesh3D>.Points => Points;
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        IArray<Triangle3D> ITriangleGeometry3D.Triangles => this.Triangles;
        // Implemented concept functions and type functions
        public TriangleMesh3D Deform(System.Func<Vector3D, Vector3D> f) => this.Points.Map(f).Tuple2(this.Indices);
        public IArray<Triangle3D> Faces => this.Triangles;
        public IArray<Triangle3D> Triangles => this.AllFaceVertices.Map((xs) => Triangle3D.New(xs.At(((Integer)0)), xs.At(((Integer)1)), xs.At(((Integer)2))));
        public LineMesh3D LineMesh3D => this.Points.Tuple2(this.AllFaceIndices.FlatMap((a) => Intrinsics.MakeArray(a.At(((Integer)0)), a.At(((Integer)1)), a.At(((Integer)2)), a.At(((Integer)0)))));
        public static implicit operator LineMesh3D(TriangleMesh3D g) => g.LineMesh3D;
        public IArray<Vector3D> FaceVertices(Integer f){
            var _var548 = this;
            return this.FaceIndices(f).Map((i) => _var548.Vertex(i));
        }
        public Vector3D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector3D> Vertices(IArray<Integer> xs){
            var _var549 = this;
            return xs.Map((i) => _var549.Vertex(i));
        }
        public IArray<IArray<Vector3D>> AllFaceVertices { get {
            var _var550 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var550.Vertices(xs));
        }
         } public IArray<Vector3D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public TriangleMesh3D Deform(ITransform3D t){
            var _var551 = t;
            return this.Deform((v) => _var551.Transform(v));
        }
        public TriangleMesh3D Translate(Vector3D v){
            var _var552 = v;
            return this.Deform((p) => p.Add(_var552));
        }
        public TriangleMesh3D Rotate(Quaternion q) => this.Deform(q);
        public TriangleMesh3D Scale(Vector3D v){
            var _var553 = v;
            return this.Deform((p) => p.Multiply(_var553));
        }
        public TriangleMesh3D Scale(Number s){
            var _var554 = s;
            return this.Deform((p) => p.Multiply(_var554));
        }
        public TriangleMesh3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public TriangleMesh3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public TriangleMesh3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public TriangleMesh3D TranslateX(Number s){
            var _var555 = s;
            return this.Deform((p) => p.Add(_var555.Tuple3(((Integer)0), ((Integer)0))));
        }
        public TriangleMesh3D TranslateY(Number s){
            var _var556 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var556, ((Integer)0))));
        }
        public TriangleMesh3D TranslateZ(Number s){
            var _var557 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var557)));
        }
        public TriangleMesh3D ScaleX(Number s){
            var _var558 = s;
            return this.Deform((p) => p.Multiply(_var558.Tuple3(((Integer)1), ((Integer)1))));
        }
        public TriangleMesh3D ScaleY(Number s){
            var _var559 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var559, ((Integer)1))));
        }
        public TriangleMesh3D ScaleZ(Number s){
            var _var560 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var560)));
        }
        public Integer PrimitiveSize => ((Integer)3);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct QuadMesh3D: IQuadMesh3D<QuadMesh3D>
    {
        [DataMember] public readonly IArray<Vector3D> Points;
        [DataMember] public readonly IArray<Integer> Indices;
        public QuadMesh3D WithPoints(IArray<Vector3D> points) => new QuadMesh3D(points, Indices);
        public QuadMesh3D WithIndices(IArray<Integer> indices) => new QuadMesh3D(Points, indices);
        public QuadMesh3D(IArray<Vector3D> points, IArray<Integer> indices) => (Points, Indices) = (points, indices);
        public static QuadMesh3D Default = new QuadMesh3D();
        public static QuadMesh3D New(IArray<Vector3D> points, IArray<Integer> indices) => new QuadMesh3D(points, indices);
        public static implicit operator (IArray<Vector3D>, IArray<Integer>)(QuadMesh3D self) => (self.Points, self.Indices);
        public static implicit operator QuadMesh3D((IArray<Vector3D>, IArray<Integer>) value) => new QuadMesh3D(value.Item1, value.Item2);
        public void Deconstruct(out IArray<Vector3D> points, out IArray<Integer> indices) { points = Points; indices = Indices; }
        public override bool Equals(object obj) { if (!(obj is QuadMesh3D)) return false; var other = (QuadMesh3D)obj; return Points.Equals(other.Points) && Indices.Equals(other.Indices); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Points, Indices);
        public override string ToString() => $"{{ \"Points\" = {Points}, \"Indices\" = {Indices} }}";
        public static implicit operator Dynamic(QuadMesh3D self) => new Dynamic(self);
        public static implicit operator QuadMesh3D(Dynamic value) => value.As<QuadMesh3D>();
        public String TypeName => "QuadMesh3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Points", (String)"Indices");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Points), new Dynamic(Indices));
        IArray<Integer> IIndexedGeometry.Indices => Indices;
        IArray<Vector3D> IPointGeometry3D<QuadMesh3D>.Points => Points;
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        IArray<Quad3D> IQuadGeometry3D.Quads => this.Quads;
        // Implemented concept functions and type functions
        public QuadMesh3D Deform(System.Func<Vector3D, Vector3D> f) => this.Points.Map(f).Tuple2(this.Indices);
        public IArray<Quad3D> Faces => this.Quads;
        public IArray<Quad3D> Quads => this.AllFaceVertices.Map((xs) => Quad3D.New(xs.At(((Integer)0)), xs.At(((Integer)1)), xs.At(((Integer)2)), xs.At(((Integer)3))));
        public LineMesh3D LineMesh3D => this.Points.Tuple2(this.AllFaceIndices.FlatMap((a) => Intrinsics.MakeArray(a.At(((Integer)0)), a.At(((Integer)1)), a.At(((Integer)2)), a.At(((Integer)3)), a.At(((Integer)0)))));
        public static implicit operator LineMesh3D(QuadMesh3D g) => g.LineMesh3D;
        public TriangleMesh3D TriangleMesh3D => this.Points.Tuple2(this.AllFaceIndices.FlatMap((a) => Intrinsics.MakeArray(a.At(((Integer)0)), a.At(((Integer)1)), a.At(((Integer)2)), a.At(((Integer)2)), a.At(((Integer)3)), a.At(((Integer)0)))));
        public static implicit operator TriangleMesh3D(QuadMesh3D g) => g.TriangleMesh3D;
        public IArray<Vector3D> FaceVertices(Integer f){
            var _var561 = this;
            return this.FaceIndices(f).Map((i) => _var561.Vertex(i));
        }
        public Vector3D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector3D> Vertices(IArray<Integer> xs){
            var _var562 = this;
            return xs.Map((i) => _var562.Vertex(i));
        }
        public IArray<IArray<Vector3D>> AllFaceVertices { get {
            var _var563 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var563.Vertices(xs));
        }
         } public IArray<Vector3D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public QuadMesh3D Deform(ITransform3D t){
            var _var564 = t;
            return this.Deform((v) => _var564.Transform(v));
        }
        public QuadMesh3D Translate(Vector3D v){
            var _var565 = v;
            return this.Deform((p) => p.Add(_var565));
        }
        public QuadMesh3D Rotate(Quaternion q) => this.Deform(q);
        public QuadMesh3D Scale(Vector3D v){
            var _var566 = v;
            return this.Deform((p) => p.Multiply(_var566));
        }
        public QuadMesh3D Scale(Number s){
            var _var567 = s;
            return this.Deform((p) => p.Multiply(_var567));
        }
        public QuadMesh3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public QuadMesh3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public QuadMesh3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public QuadMesh3D TranslateX(Number s){
            var _var568 = s;
            return this.Deform((p) => p.Add(_var568.Tuple3(((Integer)0), ((Integer)0))));
        }
        public QuadMesh3D TranslateY(Number s){
            var _var569 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var569, ((Integer)0))));
        }
        public QuadMesh3D TranslateZ(Number s){
            var _var570 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var570)));
        }
        public QuadMesh3D ScaleX(Number s){
            var _var571 = s;
            return this.Deform((p) => p.Multiply(_var571.Tuple3(((Integer)1), ((Integer)1))));
        }
        public QuadMesh3D ScaleY(Number s){
            var _var572 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var572, ((Integer)1))));
        }
        public QuadMesh3D ScaleZ(Number s){
            var _var573 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var573)));
        }
        public Integer PrimitiveSize => ((Integer)4);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct PolyLine2D: IPolyLine2D
    {
        [DataMember] public readonly IArray<Vector2D> Points;
        [DataMember] public readonly Boolean Closed;
        public PolyLine2D WithPoints(IArray<Vector2D> points) => new PolyLine2D(points, Closed);
        public PolyLine2D WithClosed(Boolean closed) => new PolyLine2D(Points, closed);
        public PolyLine2D(IArray<Vector2D> points, Boolean closed) => (Points, Closed) = (points, closed);
        public static PolyLine2D Default = new PolyLine2D();
        public static PolyLine2D New(IArray<Vector2D> points, Boolean closed) => new PolyLine2D(points, closed);
        public static implicit operator (IArray<Vector2D>, Boolean)(PolyLine2D self) => (self.Points, self.Closed);
        public static implicit operator PolyLine2D((IArray<Vector2D>, Boolean) value) => new PolyLine2D(value.Item1, value.Item2);
        public void Deconstruct(out IArray<Vector2D> points, out Boolean closed) { points = Points; closed = Closed; }
        public override bool Equals(object obj) { if (!(obj is PolyLine2D)) return false; var other = (PolyLine2D)obj; return Points.Equals(other.Points) && Closed.Equals(other.Closed); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Points, Closed);
        public override string ToString() => $"{{ \"Points\" = {Points}, \"Closed\" = {Closed} }}";
        public static implicit operator Dynamic(PolyLine2D self) => new Dynamic(self);
        public static implicit operator PolyLine2D(Dynamic value) => value.As<PolyLine2D>();
        public String TypeName => "PolyLine2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Points", (String)"Closed");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Points), new Dynamic(Closed));
        IArray<Vector2D> IPointGeometry2D.Points => Points;
        Boolean IOpenClosedShape.Closed => Closed;
        // Implemented concept functions and type functions
        public PolyLine3D To3D => this.Points.Map((p) => p.To3D).Tuple2(this.Closed);
        public PolyLine3D PolyLine3D => this.To3D;
        public static implicit operator PolyLine3D(PolyLine2D x) => x.PolyLine3D;
        public IArray<Line2D> Lines => this.Points.WithNext((a, b) => Line2D.New(a, b), this.Closed);
        public IArray<Vector2D> Sample(Integer numPoints){
            var _var574 = this;
            return numPoints.LinearSpace.Map((x) => _var574.Eval(x));
        }
        public PolyLine2D ToPolyLine2D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector2D p) => Intrinsics.Distance(this, p);
        public Vector2D Eval(Number t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct PolyLine3D: IPolyLine3D<PolyLine3D>
    {
        [DataMember] public readonly IArray<Vector3D> Points;
        [DataMember] public readonly Boolean Closed;
        public PolyLine3D WithPoints(IArray<Vector3D> points) => new PolyLine3D(points, Closed);
        public PolyLine3D WithClosed(Boolean closed) => new PolyLine3D(Points, closed);
        public PolyLine3D(IArray<Vector3D> points, Boolean closed) => (Points, Closed) = (points, closed);
        public static PolyLine3D Default = new PolyLine3D();
        public static PolyLine3D New(IArray<Vector3D> points, Boolean closed) => new PolyLine3D(points, closed);
        public static implicit operator (IArray<Vector3D>, Boolean)(PolyLine3D self) => (self.Points, self.Closed);
        public static implicit operator PolyLine3D((IArray<Vector3D>, Boolean) value) => new PolyLine3D(value.Item1, value.Item2);
        public void Deconstruct(out IArray<Vector3D> points, out Boolean closed) { points = Points; closed = Closed; }
        public override bool Equals(object obj) { if (!(obj is PolyLine3D)) return false; var other = (PolyLine3D)obj; return Points.Equals(other.Points) && Closed.Equals(other.Closed); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Points, Closed);
        public override string ToString() => $"{{ \"Points\" = {Points}, \"Closed\" = {Closed} }}";
        public static implicit operator Dynamic(PolyLine3D self) => new Dynamic(self);
        public static implicit operator PolyLine3D(Dynamic value) => value.As<PolyLine3D>();
        public String TypeName => "PolyLine3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Points", (String)"Closed");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Points), new Dynamic(Closed));
        IArray<Vector3D> IPointGeometry3D<PolyLine3D>.Points => Points;
        Boolean IOpenClosedShape.Closed => Closed;
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        // Implemented concept functions and type functions
        public PolyLine3D Deform(System.Func<Vector3D, Vector3D> f) => this.Points.Map(f).Tuple2(this.Closed);
        public IArray<Line3D> Lines => this.Points.WithNext((a, b) => Line3D.New(a, b), this.Closed);
        public PolyLine3D Deform(ITransform3D t){
            var _var575 = t;
            return this.Deform((v) => _var575.Transform(v));
        }
        public PolyLine3D Translate(Vector3D v){
            var _var576 = v;
            return this.Deform((p) => p.Add(_var576));
        }
        public PolyLine3D Rotate(Quaternion q) => this.Deform(q);
        public PolyLine3D Scale(Vector3D v){
            var _var577 = v;
            return this.Deform((p) => p.Multiply(_var577));
        }
        public PolyLine3D Scale(Number s){
            var _var578 = s;
            return this.Deform((p) => p.Multiply(_var578));
        }
        public PolyLine3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public PolyLine3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public PolyLine3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public PolyLine3D TranslateX(Number s){
            var _var579 = s;
            return this.Deform((p) => p.Add(_var579.Tuple3(((Integer)0), ((Integer)0))));
        }
        public PolyLine3D TranslateY(Number s){
            var _var580 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var580, ((Integer)0))));
        }
        public PolyLine3D TranslateZ(Number s){
            var _var581 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var581)));
        }
        public PolyLine3D ScaleX(Number s){
            var _var582 = s;
            return this.Deform((p) => p.Multiply(_var582.Tuple3(((Integer)1), ((Integer)1))));
        }
        public PolyLine3D ScaleY(Number s){
            var _var583 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var583, ((Integer)1))));
        }
        public PolyLine3D ScaleZ(Number s){
            var _var584 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var584)));
        }
        public IArray<Vector3D> Sample(Integer numPoints){
            var _var585 = this;
            return numPoints.LinearSpace.Map((x) => _var585.Eval(x));
        }
        public PolyLine3D ToPolyLine3D(Integer numPoints) => this.Sample(numPoints).Tuple2(this.Closed);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Number t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct PointArray2D: IPointArray2D
    {
        [DataMember] public readonly IArray<Vector2D> Points;
        public PointArray2D WithPoints(IArray<Vector2D> points) => new PointArray2D(points);
        public PointArray2D(IArray<Vector2D> points) => (Points) = (points);
        public static PointArray2D Default = new PointArray2D();
        public static PointArray2D New(IArray<Vector2D> points) => new PointArray2D(points);
        public override bool Equals(object obj) { if (!(obj is PointArray2D)) return false; var other = (PointArray2D)obj; return Points.Equals(other.Points); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Points);
        public override string ToString() => $"{{ \"Points\" = {Points} }}";
        public static implicit operator Dynamic(PointArray2D self) => new Dynamic(self);
        public static implicit operator PointArray2D(Dynamic value) => value.As<PointArray2D>();
        public String TypeName => "PointArray2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Points");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Points));
        IArray<Vector2D> IPointGeometry2D.Points => Points;
        // Implemented concept functions and type functions
        public IArray<Integer> Indices => this.Points.Indices();
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct PointArray3D: IPointArray3D<PointArray3D>
    {
        [DataMember] public readonly IArray<Vector3D> Points;
        public PointArray3D WithPoints(IArray<Vector3D> points) => new PointArray3D(points);
        public PointArray3D(IArray<Vector3D> points) => (Points) = (points);
        public static PointArray3D Default = new PointArray3D();
        public static PointArray3D New(IArray<Vector3D> points) => new PointArray3D(points);
        public override bool Equals(object obj) { if (!(obj is PointArray3D)) return false; var other = (PointArray3D)obj; return Points.Equals(other.Points); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Points);
        public override string ToString() => $"{{ \"Points\" = {Points} }}";
        public static implicit operator Dynamic(PointArray3D self) => new Dynamic(self);
        public static implicit operator PointArray3D(Dynamic value) => value.As<PointArray3D>();
        public String TypeName => "PointArray3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Points");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Points));
        IArray<Vector3D> IPointGeometry3D<PointArray3D>.Points => Points;
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        // Implemented concept functions and type functions
        public PointArray3D Deform(System.Func<Vector3D, Vector3D> f) => PointArray3D.New(this.Points.Map(f));
        public IArray<Integer> Indices => this.Points.Indices();
        public PointArray3D Deform(ITransform3D t){
            var _var586 = t;
            return this.Deform((v) => _var586.Transform(v));
        }
        public PointArray3D Translate(Vector3D v){
            var _var587 = v;
            return this.Deform((p) => p.Add(_var587));
        }
        public PointArray3D Rotate(Quaternion q) => this.Deform(q);
        public PointArray3D Scale(Vector3D v){
            var _var588 = v;
            return this.Deform((p) => p.Multiply(_var588));
        }
        public PointArray3D Scale(Number s){
            var _var589 = s;
            return this.Deform((p) => p.Multiply(_var589));
        }
        public PointArray3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public PointArray3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public PointArray3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public PointArray3D TranslateX(Number s){
            var _var590 = s;
            return this.Deform((p) => p.Add(_var590.Tuple3(((Integer)0), ((Integer)0))));
        }
        public PointArray3D TranslateY(Number s){
            var _var591 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var591, ((Integer)0))));
        }
        public PointArray3D TranslateZ(Number s){
            var _var592 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var592)));
        }
        public PointArray3D ScaleX(Number s){
            var _var593 = s;
            return this.Deform((p) => p.Multiply(_var593.Tuple3(((Integer)1), ((Integer)1))));
        }
        public PointArray3D ScaleY(Number s){
            var _var594 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var594, ((Integer)1))));
        }
        public PointArray3D ScaleZ(Number s){
            var _var595 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var595)));
        }
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct LineArray2D: ILineArray2D
    {
        [DataMember] public readonly IArray<Line2D> Lines;
        public LineArray2D WithLines(IArray<Line2D> lines) => new LineArray2D(lines);
        public LineArray2D(IArray<Line2D> lines) => (Lines) = (lines);
        public static LineArray2D Default = new LineArray2D();
        public static LineArray2D New(IArray<Line2D> lines) => new LineArray2D(lines);
        public override bool Equals(object obj) { if (!(obj is LineArray2D)) return false; var other = (LineArray2D)obj; return Lines.Equals(other.Lines); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Lines);
        public override string ToString() => $"{{ \"Lines\" = {Lines} }}";
        public static implicit operator Dynamic(LineArray2D self) => new Dynamic(self);
        public static implicit operator LineArray2D(Dynamic value) => value.As<LineArray2D>();
        public String TypeName => "LineArray2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Lines");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Lines));
        IArray<Line2D> ILineGeometry2D.Lines => Lines;
        // Implemented concept functions and type functions
        public IArray<Integer> Indices => this.Points.Indices();
        public IArray<Vector2D> Points => this.Lines.FlatMap((x) => x.Points);
        public IArray<Vector2D> FaceVertices(Integer f){
            var _var596 = this;
            return this.FaceIndices(f).Map((i) => _var596.Vertex(i));
        }
        public Vector2D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector2D> Vertices(IArray<Integer> xs){
            var _var597 = this;
            return xs.Map((i) => _var597.Vertex(i));
        }
        public IArray<IArray<Vector2D>> AllFaceVertices { get {
            var _var598 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var598.Vertices(xs));
        }
         } public IArray<Vector2D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public Integer PrimitiveSize => ((Integer)2);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct LineArray3D: ILineArray3D<LineArray3D>
    {
        [DataMember] public readonly IArray<Line3D> Lines;
        public LineArray3D WithLines(IArray<Line3D> lines) => new LineArray3D(lines);
        public LineArray3D(IArray<Line3D> lines) => (Lines) = (lines);
        public static LineArray3D Default = new LineArray3D();
        public static LineArray3D New(IArray<Line3D> lines) => new LineArray3D(lines);
        public override bool Equals(object obj) { if (!(obj is LineArray3D)) return false; var other = (LineArray3D)obj; return Lines.Equals(other.Lines); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Lines);
        public override string ToString() => $"{{ \"Lines\" = {Lines} }}";
        public static implicit operator Dynamic(LineArray3D self) => new Dynamic(self);
        public static implicit operator LineArray3D(Dynamic value) => value.As<LineArray3D>();
        public String TypeName => "LineArray3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Lines");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Lines));
        IArray<Line3D> ILineGeometry3D<LineArray3D>.Lines => Lines;
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        IArray<Line3D> ILineGeometry3D.Lines => this.Lines;
        // Implemented concept functions and type functions
        public LineArray3D Deform(System.Func<Vector3D, Vector3D> f){
            var _var599 = f;
            return LineArray3D.New(this.Lines.Map((l) => l.Deform(_var599)));
        }
        public IArray<Integer> Indices => this.Points.Indices();
        public IArray<Vector3D> Points => this.Lines.FlatMap((x) => x.Points);
        public IArray<Vector3D> FaceVertices(Integer f){
            var _var600 = this;
            return this.FaceIndices(f).Map((i) => _var600.Vertex(i));
        }
        public Vector3D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector3D> Vertices(IArray<Integer> xs){
            var _var601 = this;
            return xs.Map((i) => _var601.Vertex(i));
        }
        public IArray<IArray<Vector3D>> AllFaceVertices { get {
            var _var602 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var602.Vertices(xs));
        }
         } public IArray<Vector3D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public LineArray3D Deform(ITransform3D t){
            var _var603 = t;
            return this.Deform((v) => _var603.Transform(v));
        }
        public LineArray3D Translate(Vector3D v){
            var _var604 = v;
            return this.Deform((p) => p.Add(_var604));
        }
        public LineArray3D Rotate(Quaternion q) => this.Deform(q);
        public LineArray3D Scale(Vector3D v){
            var _var605 = v;
            return this.Deform((p) => p.Multiply(_var605));
        }
        public LineArray3D Scale(Number s){
            var _var606 = s;
            return this.Deform((p) => p.Multiply(_var606));
        }
        public LineArray3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public LineArray3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public LineArray3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public LineArray3D TranslateX(Number s){
            var _var607 = s;
            return this.Deform((p) => p.Add(_var607.Tuple3(((Integer)0), ((Integer)0))));
        }
        public LineArray3D TranslateY(Number s){
            var _var608 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var608, ((Integer)0))));
        }
        public LineArray3D TranslateZ(Number s){
            var _var609 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var609)));
        }
        public LineArray3D ScaleX(Number s){
            var _var610 = s;
            return this.Deform((p) => p.Multiply(_var610.Tuple3(((Integer)1), ((Integer)1))));
        }
        public LineArray3D ScaleY(Number s){
            var _var611 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var611, ((Integer)1))));
        }
        public LineArray3D ScaleZ(Number s){
            var _var612 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var612)));
        }
        public Integer PrimitiveSize => ((Integer)2);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct TriangleArray2D: ITriangleArray2D
    {
        [DataMember] public readonly IArray<Triangle2D> Triangles;
        public TriangleArray2D WithTriangles(IArray<Triangle2D> triangles) => new TriangleArray2D(triangles);
        public TriangleArray2D(IArray<Triangle2D> triangles) => (Triangles) = (triangles);
        public static TriangleArray2D Default = new TriangleArray2D();
        public static TriangleArray2D New(IArray<Triangle2D> triangles) => new TriangleArray2D(triangles);
        public override bool Equals(object obj) { if (!(obj is TriangleArray2D)) return false; var other = (TriangleArray2D)obj; return Triangles.Equals(other.Triangles); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Triangles);
        public override string ToString() => $"{{ \"Triangles\" = {Triangles} }}";
        public static implicit operator Dynamic(TriangleArray2D self) => new Dynamic(self);
        public static implicit operator TriangleArray2D(Dynamic value) => value.As<TriangleArray2D>();
        public String TypeName => "TriangleArray2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Triangles");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Triangles));
        IArray<Triangle2D> ITriangleGeometry2D.Triangles => Triangles;
        // Implemented concept functions and type functions
        public IArray<Integer> Indices => this.Points.Indices();
        public IArray<Vector2D> Points => this.Triangles.FlatMap((x) => x.Points);
        public IArray<Line2D> Lines => this.Triangles.FlatMap((x) => x.Lines);
        public IArray<Triangle2D> Faces => this.Triangles;
        public IArray<Vector2D> FaceVertices(Integer f){
            var _var613 = this;
            return this.FaceIndices(f).Map((i) => _var613.Vertex(i));
        }
        public Vector2D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector2D> Vertices(IArray<Integer> xs){
            var _var614 = this;
            return xs.Map((i) => _var614.Vertex(i));
        }
        public IArray<IArray<Vector2D>> AllFaceVertices { get {
            var _var615 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var615.Vertices(xs));
        }
         } public IArray<Vector2D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public Integer PrimitiveSize => ((Integer)3);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct TriangleArray3D: ITriangleArray3D<TriangleArray3D>
    {
        [DataMember] public readonly IArray<Triangle3D> Triangles;
        public TriangleArray3D WithTriangles(IArray<Triangle3D> triangles) => new TriangleArray3D(triangles);
        public TriangleArray3D(IArray<Triangle3D> triangles) => (Triangles) = (triangles);
        public static TriangleArray3D Default = new TriangleArray3D();
        public static TriangleArray3D New(IArray<Triangle3D> triangles) => new TriangleArray3D(triangles);
        public override bool Equals(object obj) { if (!(obj is TriangleArray3D)) return false; var other = (TriangleArray3D)obj; return Triangles.Equals(other.Triangles); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Triangles);
        public override string ToString() => $"{{ \"Triangles\" = {Triangles} }}";
        public static implicit operator Dynamic(TriangleArray3D self) => new Dynamic(self);
        public static implicit operator TriangleArray3D(Dynamic value) => value.As<TriangleArray3D>();
        public String TypeName => "TriangleArray3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Triangles");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Triangles));
        IArray<Triangle3D> ITriangleGeometry3D<TriangleArray3D>.Triangles => Triangles;
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        IArray<Triangle3D> ITriangleGeometry3D.Triangles => this.Triangles;
        // Implemented concept functions and type functions
        public TriangleArray3D Deform(System.Func<Vector3D, Vector3D> f){
            var _var616 = f;
            return TriangleArray3D.New(this.Triangles.Map((t) => t.Deform(_var616)));
        }
        public IArray<Integer> Indices => this.Points.Indices();
        public TriangleMesh3D TriangleMesh3D => this.Points.Tuple2(this.Points.Indices());
        public static implicit operator TriangleMesh3D(TriangleArray3D g) => g.TriangleMesh3D;
        public IArray<Vector3D> Points => this.Triangles.FlatMap((x) => x.Points);
        public IArray<Line3D> Lines => this.Triangles.FlatMap((x) => x.Lines);
        public IArray<Triangle3D> Faces => this.Triangles;
        public LineMesh3D LineMesh3D => this.Points.Tuple2(this.AllFaceIndices.FlatMap((a) => Intrinsics.MakeArray(a.At(((Integer)0)), a.At(((Integer)1)), a.At(((Integer)2)), a.At(((Integer)0)))));
        public static implicit operator LineMesh3D(TriangleArray3D g) => g.LineMesh3D;
        public IArray<Vector3D> FaceVertices(Integer f){
            var _var617 = this;
            return this.FaceIndices(f).Map((i) => _var617.Vertex(i));
        }
        public Vector3D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector3D> Vertices(IArray<Integer> xs){
            var _var618 = this;
            return xs.Map((i) => _var618.Vertex(i));
        }
        public IArray<IArray<Vector3D>> AllFaceVertices { get {
            var _var619 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var619.Vertices(xs));
        }
         } public IArray<Vector3D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public TriangleArray3D Deform(ITransform3D t){
            var _var620 = t;
            return this.Deform((v) => _var620.Transform(v));
        }
        public TriangleArray3D Translate(Vector3D v){
            var _var621 = v;
            return this.Deform((p) => p.Add(_var621));
        }
        public TriangleArray3D Rotate(Quaternion q) => this.Deform(q);
        public TriangleArray3D Scale(Vector3D v){
            var _var622 = v;
            return this.Deform((p) => p.Multiply(_var622));
        }
        public TriangleArray3D Scale(Number s){
            var _var623 = s;
            return this.Deform((p) => p.Multiply(_var623));
        }
        public TriangleArray3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public TriangleArray3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public TriangleArray3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public TriangleArray3D TranslateX(Number s){
            var _var624 = s;
            return this.Deform((p) => p.Add(_var624.Tuple3(((Integer)0), ((Integer)0))));
        }
        public TriangleArray3D TranslateY(Number s){
            var _var625 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var625, ((Integer)0))));
        }
        public TriangleArray3D TranslateZ(Number s){
            var _var626 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var626)));
        }
        public TriangleArray3D ScaleX(Number s){
            var _var627 = s;
            return this.Deform((p) => p.Multiply(_var627.Tuple3(((Integer)1), ((Integer)1))));
        }
        public TriangleArray3D ScaleY(Number s){
            var _var628 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var628, ((Integer)1))));
        }
        public TriangleArray3D ScaleZ(Number s){
            var _var629 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var629)));
        }
        public Integer PrimitiveSize => ((Integer)3);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct QuadArray2D: IQuadArray2D
    {
        [DataMember] public readonly IArray<Quad2D> Quads;
        public QuadArray2D WithQuads(IArray<Quad2D> quads) => new QuadArray2D(quads);
        public QuadArray2D(IArray<Quad2D> quads) => (Quads) = (quads);
        public static QuadArray2D Default = new QuadArray2D();
        public static QuadArray2D New(IArray<Quad2D> quads) => new QuadArray2D(quads);
        public override bool Equals(object obj) { if (!(obj is QuadArray2D)) return false; var other = (QuadArray2D)obj; return Quads.Equals(other.Quads); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Quads);
        public override string ToString() => $"{{ \"Quads\" = {Quads} }}";
        public static implicit operator Dynamic(QuadArray2D self) => new Dynamic(self);
        public static implicit operator QuadArray2D(Dynamic value) => value.As<QuadArray2D>();
        public String TypeName => "QuadArray2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Quads");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Quads));
        IArray<Quad2D> IQuadGeometry2D.Quads => Quads;
        // Implemented concept functions and type functions
        public IArray<Integer> Indices => this.Points.Indices();
        public IArray<Vector2D> Points => this.Quads.FlatMap((x) => x.Points);
        public IArray<Line2D> Lines => this.Quads.FlatMap((x) => x.Lines);
        public IArray<Triangle2D> Triangles => this.Quads.FlatMap((x) => x.Triangles);
        public IArray<Quad2D> Faces => this.Quads;
        public IArray<Vector2D> FaceVertices(Integer f){
            var _var630 = this;
            return this.FaceIndices(f).Map((i) => _var630.Vertex(i));
        }
        public Vector2D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector2D> Vertices(IArray<Integer> xs){
            var _var631 = this;
            return xs.Map((i) => _var631.Vertex(i));
        }
        public IArray<IArray<Vector2D>> AllFaceVertices { get {
            var _var632 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var632.Vertices(xs));
        }
         } public IArray<Vector2D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public Integer PrimitiveSize => ((Integer)4);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct QuadArray3D: IQuadArray3D<QuadArray3D>
    {
        [DataMember] public readonly IArray<Quad3D> Quads;
        public QuadArray3D WithQuads(IArray<Quad3D> quads) => new QuadArray3D(quads);
        public QuadArray3D(IArray<Quad3D> quads) => (Quads) = (quads);
        public static QuadArray3D Default = new QuadArray3D();
        public static QuadArray3D New(IArray<Quad3D> quads) => new QuadArray3D(quads);
        public override bool Equals(object obj) { if (!(obj is QuadArray3D)) return false; var other = (QuadArray3D)obj; return Quads.Equals(other.Quads); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Quads);
        public override string ToString() => $"{{ \"Quads\" = {Quads} }}";
        public static implicit operator Dynamic(QuadArray3D self) => new Dynamic(self);
        public static implicit operator QuadArray3D(Dynamic value) => value.As<QuadArray3D>();
        public String TypeName => "QuadArray3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Quads");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Quads));
        IArray<Quad3D> IQuadGeometry3D<QuadArray3D>.Quads => Quads;
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        IArray<Quad3D> IQuadGeometry3D.Quads => this.Quads;
        // Implemented concept functions and type functions
        public QuadArray3D Deform(System.Func<Vector3D, Vector3D> f){
            var _var633 = f;
            return QuadArray3D.New(this.Quads.Map((q) => q.Deform(_var633)));
        }
        public IArray<Integer> Indices => this.Points.Indices();
        public TriangleArray3D TriangleArray3D => TriangleArray3D.New(this.Triangles);
        public static implicit operator TriangleArray3D(QuadArray3D q) => q.TriangleArray3D;
        public QuadMesh3D QuadMesh3D => this.Points.Tuple2(this.Points.Indices());
        public static implicit operator QuadMesh3D(QuadArray3D self) => self.QuadMesh3D;
        public TriangleMesh3D TriangleMesh3D => this.TriangleArray3D;
        public static implicit operator TriangleMesh3D(QuadArray3D g) => g.TriangleMesh3D;
        public IArray<Vector3D> Points => this.Quads.FlatMap((x) => x.Points);
        public IArray<Line3D> Lines => this.Quads.FlatMap((x) => x.Lines);
        public IArray<Triangle3D> Triangles => this.Quads.FlatMap((x) => x.Triangles);
        public IArray<Quad3D> Faces => this.Quads;
        public LineMesh3D LineMesh3D => this.Points.Tuple2(this.AllFaceIndices.FlatMap((a) => Intrinsics.MakeArray(a.At(((Integer)0)), a.At(((Integer)1)), a.At(((Integer)2)), a.At(((Integer)3)), a.At(((Integer)0)))));
        public static implicit operator LineMesh3D(QuadArray3D g) => g.LineMesh3D;
        public IArray<Vector3D> FaceVertices(Integer f){
            var _var634 = this;
            return this.FaceIndices(f).Map((i) => _var634.Vertex(i));
        }
        public Vector3D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector3D> Vertices(IArray<Integer> xs){
            var _var635 = this;
            return xs.Map((i) => _var635.Vertex(i));
        }
        public IArray<IArray<Vector3D>> AllFaceVertices { get {
            var _var636 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var636.Vertices(xs));
        }
         } public IArray<Vector3D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public QuadArray3D Deform(ITransform3D t){
            var _var637 = t;
            return this.Deform((v) => _var637.Transform(v));
        }
        public QuadArray3D Translate(Vector3D v){
            var _var638 = v;
            return this.Deform((p) => p.Add(_var638));
        }
        public QuadArray3D Rotate(Quaternion q) => this.Deform(q);
        public QuadArray3D Scale(Vector3D v){
            var _var639 = v;
            return this.Deform((p) => p.Multiply(_var639));
        }
        public QuadArray3D Scale(Number s){
            var _var640 = s;
            return this.Deform((p) => p.Multiply(_var640));
        }
        public QuadArray3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public QuadArray3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public QuadArray3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public QuadArray3D TranslateX(Number s){
            var _var641 = s;
            return this.Deform((p) => p.Add(_var641.Tuple3(((Integer)0), ((Integer)0))));
        }
        public QuadArray3D TranslateY(Number s){
            var _var642 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var642, ((Integer)0))));
        }
        public QuadArray3D TranslateZ(Number s){
            var _var643 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var643)));
        }
        public QuadArray3D ScaleX(Number s){
            var _var644 = s;
            return this.Deform((p) => p.Multiply(_var644.Tuple3(((Integer)1), ((Integer)1))));
        }
        public QuadArray3D ScaleY(Number s){
            var _var645 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var645, ((Integer)1))));
        }
        public QuadArray3D ScaleZ(Number s){
            var _var646 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var646)));
        }
        public Integer PrimitiveSize => ((Integer)4);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct QuadGrid3D: IQuadGrid3D<QuadGrid3D>
    {
        [DataMember] public readonly IArray2D<Vector3D> PointGrid;
        [DataMember] public readonly Boolean ClosedX;
        [DataMember] public readonly Boolean ClosedY;
        public QuadGrid3D WithPointGrid(IArray2D<Vector3D> pointGrid) => new QuadGrid3D(pointGrid, ClosedX, ClosedY);
        public QuadGrid3D WithClosedX(Boolean closedX) => new QuadGrid3D(PointGrid, closedX, ClosedY);
        public QuadGrid3D WithClosedY(Boolean closedY) => new QuadGrid3D(PointGrid, ClosedX, closedY);
        public QuadGrid3D(IArray2D<Vector3D> pointGrid, Boolean closedX, Boolean closedY) => (PointGrid, ClosedX, ClosedY) = (pointGrid, closedX, closedY);
        public static QuadGrid3D Default = new QuadGrid3D();
        public static QuadGrid3D New(IArray2D<Vector3D> pointGrid, Boolean closedX, Boolean closedY) => new QuadGrid3D(pointGrid, closedX, closedY);
        public static implicit operator (IArray2D<Vector3D>, Boolean, Boolean)(QuadGrid3D self) => (self.PointGrid, self.ClosedX, self.ClosedY);
        public static implicit operator QuadGrid3D((IArray2D<Vector3D>, Boolean, Boolean) value) => new QuadGrid3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out IArray2D<Vector3D> pointGrid, out Boolean closedX, out Boolean closedY) { pointGrid = PointGrid; closedX = ClosedX; closedY = ClosedY; }
        public override bool Equals(object obj) { if (!(obj is QuadGrid3D)) return false; var other = (QuadGrid3D)obj; return PointGrid.Equals(other.PointGrid) && ClosedX.Equals(other.ClosedX) && ClosedY.Equals(other.ClosedY); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(PointGrid, ClosedX, ClosedY);
        public override string ToString() => $"{{ \"PointGrid\" = {PointGrid}, \"ClosedX\" = {ClosedX}, \"ClosedY\" = {ClosedY} }}";
        public static implicit operator Dynamic(QuadGrid3D self) => new Dynamic(self);
        public static implicit operator QuadGrid3D(Dynamic value) => value.As<QuadGrid3D>();
        public String TypeName => "QuadGrid3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"PointGrid", (String)"ClosedX", (String)"ClosedY");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(PointGrid), new Dynamic(ClosedX), new Dynamic(ClosedY));
        IArray2D<Vector3D> IQuadGrid3D<QuadGrid3D>.PointGrid => PointGrid;
        Boolean IQuadGrid3D<QuadGrid3D>.ClosedX => ClosedX;
        Boolean IQuadGrid3D<QuadGrid3D>.ClosedY => ClosedY;
        IArray2D<Vector3D> IQuadGrid3D.PointGrid => this.PointGrid;
        Boolean IQuadGrid3D.ClosedX => this.ClosedX;
        Boolean IQuadGrid3D.ClosedY => this.ClosedY;
        IArray<Vector3D> IPointGeometry3D.Points => this.Points;
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        IArray<Quad3D> IQuadGeometry3D.Quads => this.Quads;
        // Implemented concept functions and type functions
        public QuadGrid3D Deform(System.Func<Vector3D, Vector3D> f) => QuadGrid3D.New(this.PointGrid.Map(f), this.ClosedX, this.ClosedY);
        public Integer NumColumns => this.PointGrid.NumColumns;
        public Integer NumRows => this.PointGrid.NumRows;
        public IArray<Vector3D> Points => this.PointGrid;
        public IArray<Integer> Indices => this.PointGrid.AllQuadFaceIndices(this.ClosedX, this.ClosedY).FlatMap((x) => x);
        public IArray<Quad3D> Faces => this.Quads;
        public IArray<Quad3D> Quads => this.AllFaceVertices.Map((xs) => Quad3D.New(xs.At(((Integer)0)), xs.At(((Integer)1)), xs.At(((Integer)2)), xs.At(((Integer)3))));
        public LineMesh3D LineMesh3D => this.Points.Tuple2(this.AllFaceIndices.FlatMap((a) => Intrinsics.MakeArray(a.At(((Integer)0)), a.At(((Integer)1)), a.At(((Integer)2)), a.At(((Integer)3)), a.At(((Integer)0)))));
        public static implicit operator LineMesh3D(QuadGrid3D g) => g.LineMesh3D;
        public TriangleMesh3D TriangleMesh3D => this.Points.Tuple2(this.AllFaceIndices.FlatMap((a) => Intrinsics.MakeArray(a.At(((Integer)0)), a.At(((Integer)1)), a.At(((Integer)2)), a.At(((Integer)2)), a.At(((Integer)3)), a.At(((Integer)0)))));
        public static implicit operator TriangleMesh3D(QuadGrid3D g) => g.TriangleMesh3D;
        public IArray<Vector3D> FaceVertices(Integer f){
            var _var647 = this;
            return this.FaceIndices(f).Map((i) => _var647.Vertex(i));
        }
        public Vector3D Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        public IArray<Vector3D> Vertices(IArray<Integer> xs){
            var _var648 = this;
            return xs.Map((i) => _var648.Vertex(i));
        }
        public IArray<IArray<Vector3D>> AllFaceVertices { get {
            var _var649 = this;
            return this.Indices.Slices(this.PrimitiveSize).Map((xs) => _var649.Vertices(xs));
        }
         } public IArray<Vector3D> AllVertices => this.Vertices(this.Indices);
        public Integer NumFaces => this.NumPrimitives;
        public IArray<Integer> FaceIndices(Integer f) => this.Indices.NthSlice(f, this.PrimitiveSize);
        public IArray<IArray<Integer>> AllFaceIndices => this.Indices.Slices(this.PrimitiveSize);
        public Integer NumPrimitives => this.Indices.Count.Divide(this.PrimitiveSize);
        public QuadGrid3D Deform(ITransform3D t){
            var _var650 = t;
            return this.Deform((v) => _var650.Transform(v));
        }
        public QuadGrid3D Translate(Vector3D v){
            var _var651 = v;
            return this.Deform((p) => p.Add(_var651));
        }
        public QuadGrid3D Rotate(Quaternion q) => this.Deform(q);
        public QuadGrid3D Scale(Vector3D v){
            var _var652 = v;
            return this.Deform((p) => p.Multiply(_var652));
        }
        public QuadGrid3D Scale(Number s){
            var _var653 = s;
            return this.Deform((p) => p.Multiply(_var653));
        }
        public QuadGrid3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public QuadGrid3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public QuadGrid3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public QuadGrid3D TranslateX(Number s){
            var _var654 = s;
            return this.Deform((p) => p.Add(_var654.Tuple3(((Integer)0), ((Integer)0))));
        }
        public QuadGrid3D TranslateY(Number s){
            var _var655 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var655, ((Integer)0))));
        }
        public QuadGrid3D TranslateZ(Number s){
            var _var656 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var656)));
        }
        public QuadGrid3D ScaleX(Number s){
            var _var657 = s;
            return this.Deform((p) => p.Multiply(_var657.Tuple3(((Integer)1), ((Integer)1))));
        }
        public QuadGrid3D ScaleY(Number s){
            var _var658 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var658, ((Integer)1))));
        }
        public QuadGrid3D ScaleZ(Number s){
            var _var659 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var659)));
        }
        public Integer PrimitiveSize => ((Integer)4);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Number: IReal<Number>
    {
        [DataMember] public readonly double Value;
        public Number WithValue(double value) => new Number(value);
        public Number(double value) => (Value) = (value);
        public static Number Default = new Number();
        public static Number New(double value) => new Number(value);
        public Plato.SinglePrecision.Number ChangePrecision() => (Value.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Number(Number self) => self.ChangePrecision();
        public static implicit operator double(Number self) => self.Value;
        public static implicit operator Number(double value) => new Number(value);
        public override bool Equals(object obj) { if (!(obj is Number)) return false; var other = (Number)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.ToString(this);
        public static implicit operator Dynamic(Number self) => new Dynamic(self);
        public static implicit operator Number(Dynamic value) => value.As<Number>();
        public String TypeName => "Number";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        Number INumberLike.ToNumber => this.ToNumber;
        INumberLike INumberLike.FromNumber(Number n) => this.FromNumber((Number)n);
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Number)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Number)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Number)b);
        IAdditive IAdditive.Negative => this.Negative;
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Number)y);
        IMultiplicative IMultiplicative.Multiply(IMultiplicative b) => this.Multiply((Number)b);
        IDivisible IDivisible.Divide(IDivisible b) => this.Divide((Number)b);
        IModulo IModulo.Modulo(IModulo b) => this.Modulo((Number)b);
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(Value);
        public Number FromComponents(IArray<Number> numbers) => new Number(numbers[0]);
        // Implemented concept functions and type functions
        public Number OunceToGram => this.Multiply(((Number)28.349523125));
        public Number TroyOunceToGram => this.Multiply(((Number)31.1034768));
        public Number GrainToMilligram => this.Multiply(((Number)64.79891));
        public Number Mole => this.Multiply(((Number)6.02214076E+23));
        public Number Min(Number y) => this.LessThanOrEquals(y) ? this : y;
        public Number Max(Number y) => this.GreaterThanOrEquals(y) ? this : y;
        public Number Inverse => ((Number)1).Divide(this);
        public Number Reciprocal => this.Inverse;
        public Number SquareRoot => this.Pow(((Number)0.5));
        public Number Sqrt => this.SquareRoot;
        public Number InversePow(Number n) => this.Pow(n.Reciprocal);
        // Ambiguous: could not choose a best function implementation for Exp(Number):Number:Number.
        public Number Exp => Constants.E.Pow(this);
        public Number SmoothStep => this.Square.Multiply(((Number)3).Subtract(this.Twice));
        public Number MultiplyEpsilon(Number y) => this.Abs.Greater(y.Abs).Multiply(Constants.Epsilon);
        public Boolean AlmostEqual(Number y) => this.Subtract(y).Abs.LessThanOrEquals(this.MultiplyEpsilon(y));
        public Boolean AlmostZero => this.Abs.LessThan(Constants.Epsilon);
        public Boolean AlmostZeroOrOne => this.AlmostEqual(((Integer)0)).Or(this.AlmostEqual(((Integer)1)));
        public Number Fract => this.Subtract(this.Floor);
        public Angle Turns => this.Multiply(Constants.TwoPi).Radians;
        public Angle Degrees => this.Divide(((Number)360)).Turns;
        public Angle Gradians => this.Divide(((Number)400)).Turns;
        public Angle Radians => this;
        public Number Sin => this.Turns.Sin;
        public Number Cos => this.Turns.Cos;
        public Number Tan => this.Turns.Tan;
        public Number ToNumber => this;
        public Number Linear(Number m, Number b) => m.Multiply(this).Add(b);
        public Number Quadratic(Number a, Number b, Number c) => a.Multiply(this.Sqr).Add(b.Multiply(this).Add(c));
        public Number Cubic(Number a, Number b, Number c, Number d) => a.Multiply(this.Cube).Add(b.Multiply(this.Sqr).Add(c.Multiply(this).Add(d)));
        public Number SineWave(Number amplitude, Number frequency, Number phase) => amplitude.Multiply(frequency.Multiply(this.Turns.Sin).Add(phase));
        public Number StaircaseFloor(Integer steps) => this.Multiply(steps).Floor.Divide(steps);
        public Number StaircaseCeiling(Integer steps) => this.Multiply(steps).Ceiling.Divide(steps);
        public Number StaircaseRound(Integer steps) => this.Multiply(steps).Round.Divide(steps);
        public Vector2D Spiral(Number R, Number r, Number numTurns) => Vector2D.New(this.Turns.Multiply(numTurns).Cos, this.Turns.Multiply(numTurns).Sin).Multiply(r.Lerp(R, this));
        public Angle Acos => Intrinsics.Acos(this);
        public Angle Asin => Intrinsics.Asin(this);
        public Angle Atan => Intrinsics.Atan(this);
        public Angle Atan2(Number x) => Intrinsics.Atan2(this, x);
        public Number Pow(Number y) => Intrinsics.Pow(this, y);
        public Number Log(Number y) => Intrinsics.Log(this, y);
        public Number Ln => Intrinsics.Ln(this);
        public Number Floor => Intrinsics.Floor(this);
        public Number Ceiling => Intrinsics.Ceiling(this);
        public Number Round => Intrinsics.Round(this);
        public Number Truncate => Intrinsics.Truncate(this);
        public Number Add(Number y) => Intrinsics.Add(this, y);
        public static Number operator +(Number x, Number y) => x.Add(y);
        public Number Subtract(Number y) => Intrinsics.Subtract(this, y);
        public static Number operator -(Number x, Number y) => x.Subtract(y);
        public Number Divide(Number y) => Intrinsics.Divide(this, y);
        public static Number operator /(Number x, Number y) => x.Divide(y);
        public Number Multiply(Number y) => Intrinsics.Multiply(this, y);
        public static Number operator *(Number x, Number y) => x.Multiply(y);
        public Number Modulo(Number y) => Intrinsics.Modulo(this, y);
        public static Number operator %(Number x, Number y) => x.Modulo(y);
        public Number Negative => Intrinsics.Negative(this);
        public static Number operator -(Number x) => x.Negative;
        public Boolean LessThanOrEquals(Number y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Number x, Number y) => x.LessThanOrEquals(y);
        public Boolean Equals(Number y) => Intrinsics.Equals(this, y);
        public static Boolean operator ==(Number x, Number y) => x.Equals(y);
        public Number Magnitude => this.Value;
        public Boolean GtZ => this.GreaterThan(this.Zero);
        public Boolean LtZ => this.LessThan(this.Zero);
        public Boolean GtEqZ => this.GreaterThanOrEquals(this.Zero);
        public Boolean LtEqZ => this.LessThanOrEquals(this.Zero);
        public Boolean IsPositive => this.GtEqZ;
        public Boolean IsNegative => this.LtZ;
        public Integer Sign => this.LtZ ? ((Integer)1).Negative : this.GtZ ? ((Integer)1) : ((Integer)0);
        public Number Abs => this.LtZ ? this.Negative : this;
        public Boolean Between(Number min, Number max) => this.GreaterThanOrEquals(min).And(this.LessThanOrEquals(max));
        public Number FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Number b) => this.ToNumber.LessThan(b.ToNumber) ? ((Integer)1).Negative : this.ToNumber.GreaterThan(b.ToNumber) ? ((Integer)1) : ((Integer)0);
        public Number Subract(Number y) => this.FromNumber(this.ToNumber.Subtract(y));
        public Number PlusOne => this.Add(this.One);
        public Number MinusOne => this.Subtract(this.One);
        public Number FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Number MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Number ZipComponents(Number y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Number Zero => this.MapComponents((i) => ((Number)0));
        public Number One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinValue => this.MapComponents((x) => x.MinValue);
        public Number MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Number Clamp(Number a, Number b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Number ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Number Clamp01 => this.ClampZeroOne;
        public IArray<Number> Repeat(Integer n){
            var _var660 = this;
            return n.MapRange((i) => _var660);
        }
        public Boolean NotEquals(Number b) => this.Equals(b).Not;
        public static Boolean operator !=(Number a, Number b) => a.NotEquals(b);
        public Number Half => this.Divide(((Number)2));
        public Number Quarter => this.Divide(((Number)4));
        public Number Eight => this.Divide(((Number)8));
        public Number Sixteenth => this.Divide(((Number)16));
        public Number Tenth => this.Divide(((Number)10));
        public Number Twice => this.Multiply(((Number)2));
        public Number Hundred => this.Multiply(((Number)100));
        public Number Thousand => this.Multiply(((Number)1000));
        public Number Million => this.Thousand.Thousand;
        public Number Billion => this.Thousand.Million;
        public Boolean LessThan(Number b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Number a, Number b) => a.LessThan(b);
        public Boolean GreaterThan(Number b) => b.LessThan(this);
        public static Boolean operator >(Number a, Number b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Number b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Number a, Number b) => a.GreaterThanOrEquals(b);
        public Number Lesser(Number b) => this.LessThanOrEquals(b) ? this : b;
        public Number Greater(Number b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Number b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Number CubicBezier(Number b, Number c, Number d, Number t) => this.Multiply(((Number)1).Subtract(t).Cube).Add(b.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr.Multiply(t))).Add(c.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Multiply(t.Sqr))).Add(d.Multiply(t.Cube))));
        public Number CubicBezierDerivative(Number b, Number c, Number d, Number t) => b.Subtract(this).Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr)).Add(c.Subtract(b).Multiply(((Number)6).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(d.Subtract(c).Multiply(((Number)3).Multiply(t.Sqr))));
        public Number CubicBezierSecondDerivative(Number b, Number c, Number d, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(((Number)1).Subtract(t))).Add(d.Subtract(c.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(t)));
        public Number QuadraticBezier(Number b, Number c, Number t) => this.Multiply(((Number)1).Subtract(t).Sqr).Add(b.Multiply(((Number)2).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(c.Multiply(t.Sqr)));
        public Number QuadraticBezierDerivative(Number b, Number c, Number t) => b.Subtract(b).Multiply(((Number)2).Multiply(((Number)1).Subtract(t))).Add(c.Subtract(b).Multiply(((Number)2).Multiply(t)));
        public Number QuadraticBezierSecondDerivative(Number b, Number c, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this));
        public Number Lerp(Number b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Number Barycentric(Number v2, Number v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        public Number Parabola => this.Sqr;
        public Number Pow2 => this.Multiply(this);
        public Number Pow3 => this.Pow2.Multiply(this);
        public Number Pow4 => this.Pow3.Multiply(this);
        public Number Pow5 => this.Pow4.Multiply(this);
        public Number Square => this.Pow2;
        public Number Sqr => this.Pow2;
        public Number Cube => this.Pow3;
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Integer: IWholeNumber<Integer>
    {
        [DataMember] public readonly int Value;
        public Integer WithValue(int value) => new Integer(value);
        public Integer(int value) => (Value) = (value);
        public static Integer Default = new Integer();
        public static Integer New(int value) => new Integer(value);
        public Plato.SinglePrecision.Integer ChangePrecision() => (Value.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Integer(Integer self) => self.ChangePrecision();
        public static implicit operator int(Integer self) => self.Value;
        public static implicit operator Integer(int value) => new Integer(value);
        public override bool Equals(object obj) { if (!(obj is Integer)) return false; var other = (Integer)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.ToString(this);
        public static implicit operator Dynamic(Integer self) => new Dynamic(self);
        public static implicit operator Integer(Dynamic value) => value.As<Integer>();
        public String TypeName => "Integer";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Integer)b);
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Integer)y);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Integer)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Integer)b);
        IAdditive IAdditive.Negative => this.Negative;
        IMultiplicative IMultiplicative.Multiply(IMultiplicative b) => this.Multiply((Integer)b);
        IDivisible IDivisible.Divide(IDivisible b) => this.Divide((Integer)b);
        IModulo IModulo.Modulo(IModulo b) => this.Modulo((Integer)b);
        // Implemented concept functions and type functions
        public IArray<Integer> Range => this.MapRange((i) => i);
        public Number ToNumber => this.Number;
        public Integer PlusOne => this.Add(((Integer)1));
        public Integer MinusOne => this.Subtract(((Integer)1));
        public Integer FromOne => ((Integer)1).Subtract(this);
        public Number FloatDivision(Integer y) => this.ToNumber.Divide(y.ToNumber);
        public IArray<Number> Fractions { get {
            var _var661 = this;
            return this.Range.Map((i) => i.FloatDivision(_var661.Subtract(((Integer)1))));
        }
         } public IArray<Number> FractionsExclusive { get {
            var _var662 = this;
            return this.Range.Map((i) => i.FloatDivision(_var662));
        }
         } public IArray<Vector2D> CirclePoints => this.Fractions.Map((x) => x.Turns.UnitCircle);
        public Integer4 QuadFaceIndices(Integer row, Integer nCols, Integer nRows){
            var a = row.Multiply(nCols).Add(this);
            var b = row.Multiply(nCols).Add(this.Add(((Integer)1)).Modulo(nCols));
            var c = row.Add(((Integer)1)).Modulo(nRows).Multiply(nCols).Add(this.Add(((Integer)1)).Modulo(nCols));
            var d = row.Add(((Integer)1)).Modulo(nRows).Multiply(nCols).Add(this);
            return a.Tuple4(b, c, d);
        }
        public IArray2D<Integer4> AllQuadFaceIndices(Integer nRows, Boolean closedX, Boolean closedY){
            var _var664 = nRows;
            {
                var _var663 = this;
                {
                    var nx = this.Subtract(closedX ? ((Integer)0) : ((Integer)1));
                    var ny = nRows.Subtract(closedY ? ((Integer)0) : ((Integer)1));
                    return nx.MakeArray2D(ny, (col, row) => col.QuadFaceIndices(row, _var663, _var664));
                }
            }
        }
        public IArray<Number> LinearSpace => this.Fractions;
        public IArray<Number> LinearSpaceExclusive => this.FractionsExclusive;
        public IArray<Number> GeometricSpace => this.LinearSpace.Map((x) => ((Number)1).Pow(x));
        public IArray<Number> GeometricSpaceExclusive => this.LinearSpaceExclusive.Map((x) => ((Number)1).Pow(x));
        public IArray<NumberInterval> Intervals => this.Add(((Integer)1)).LinearSpace.WithNext((a, b) => NumberInterval.New(a, b));
        public Integer Add(Integer y) => Intrinsics.Add(this, y);
        public static Integer operator +(Integer x, Integer y) => x.Add(y);
        public Integer Subtract(Integer y) => Intrinsics.Subtract(this, y);
        public static Integer operator -(Integer x, Integer y) => x.Subtract(y);
        public Integer Divide(Integer y) => Intrinsics.Divide(this, y);
        public static Integer operator /(Integer x, Integer y) => x.Divide(y);
        public Integer Multiply(Integer y) => Intrinsics.Multiply(this, y);
        public static Integer operator *(Integer x, Integer y) => x.Multiply(y);
        public Integer Modulo(Integer y) => Intrinsics.Modulo(this, y);
        public static Integer operator %(Integer x, Integer y) => x.Modulo(y);
        public Integer Negative => Intrinsics.Negative(this);
        public static Integer operator -(Integer x) => x.Negative;
        public Number Number => Intrinsics.Number(this);
        public static implicit operator Number(Integer x) => x.Number;
        public Boolean LessThanOrEquals(Integer y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Integer x, Integer y) => x.LessThanOrEquals(y);
        public Boolean Equals(Integer y) => Intrinsics.Equals(this, y);
        public static Boolean operator ==(Integer x, Integer y) => x.Equals(y);
        public IArray<TR> MapRange<TR>(System.Func<Integer, TR> f) => Intrinsics.MapRange(this, f);
        public IArray2D<TR> MakeArray2D<TR>(Integer rows, System.Func<Integer, Integer, TR> f) => Intrinsics.MakeArray2D(this, rows, f);
        public IArray<Integer> Repeat(Integer n){
            var _var665 = this;
            return n.MapRange((i) => _var665);
        }
        public Boolean NotEquals(Integer b) => this.Equals(b).Not;
        public static Boolean operator !=(Integer a, Integer b) => a.NotEquals(b);
        public Boolean LessThan(Integer b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Integer a, Integer b) => a.LessThan(b);
        public Boolean GreaterThan(Integer b) => b.LessThan(this);
        public static Boolean operator >(Integer a, Integer b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Integer b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Integer a, Integer b) => a.GreaterThanOrEquals(b);
        public Integer Lesser(Integer b) => this.LessThanOrEquals(b) ? this : b;
        public Integer Greater(Integer b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Integer b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Integer Pow2 => this.Multiply(this);
        public Integer Pow3 => this.Pow2.Multiply(this);
        public Integer Pow4 => this.Pow3.Multiply(this);
        public Integer Pow5 => this.Pow4.Multiply(this);
        public Integer Square => this.Pow2;
        public Integer Sqr => this.Pow2;
        public Integer Cube => this.Pow3;
        public Integer Parabola => this.Sqr;
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct String: IValue<String>, IOrderable<String>, IArray<Character>
    {
        [DataMember] public readonly string Value;
        public String WithValue(string value) => new String(value);
        public String(string value) => (Value) = (value);
        public static String Default = new String();
        public static String New(string value) => new String(value);
        public Plato.SinglePrecision.String ChangePrecision() => (Value.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.String(String self) => self.ChangePrecision();
        public static implicit operator string(String self) => self.Value;
        public static implicit operator String(string value) => new String(value);
        public override bool Equals(object obj) { if (!(obj is String)) return false; var other = (String)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.ToString(this);
        public static implicit operator Dynamic(String self) => new Dynamic(self);
        public static implicit operator String(Dynamic value) => value.As<String>();
        public String TypeName => "String";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((String)b);
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((String)y);
        // Array predefined functions
        public static implicit operator Character[](String self) => self.ToSystemArray();
        public static implicit operator Array<Character>(String self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Character> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Character System.Collections.Generic.IReadOnlyList<Character>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Character>.Count => this.Count;
        // Implemented concept functions and type functions
        public Character At(Integer y) => Intrinsics.At(this, y);
        public Character this[Integer y] => At(y);
        public Integer Count => Intrinsics.Count(this);
        public Boolean LessThanOrEquals(String y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(String x, String y) => x.LessThanOrEquals(y);
        public Boolean Equals(String y) => Intrinsics.Equals(this, y);
        public static Boolean operator ==(String x, String y) => x.Equals(y);
        public IArray<String> Repeat(Integer n){
            var _var666 = this;
            return n.MapRange((i) => _var666);
        }
        public Boolean NotEquals(String b) => this.Equals(b).Not;
        public static Boolean operator !=(String a, String b) => a.NotEquals(b);
        public Boolean LessThan(String b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(String a, String b) => a.LessThan(b);
        public Boolean GreaterThan(String b) => b.LessThan(this);
        public static Boolean operator >(String a, String b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(String b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(String a, String b) => a.GreaterThanOrEquals(b);
        public String Lesser(String b) => this.LessThanOrEquals(b) ? this : b;
        public String Greater(String b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(String b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Boolean: IValue<Boolean>, IOrderable<Boolean>, IBoolean<Boolean>
    {
        [DataMember] public readonly bool Value;
        public Boolean WithValue(bool value) => new Boolean(value);
        public Boolean(bool value) => (Value) = (value);
        public static Boolean Default = new Boolean();
        public static Boolean New(bool value) => new Boolean(value);
        public Plato.SinglePrecision.Boolean ChangePrecision() => (Value.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Boolean(Boolean self) => self.ChangePrecision();
        public static implicit operator bool(Boolean self) => self.Value;
        public static implicit operator Boolean(bool value) => new Boolean(value);
        public override bool Equals(object obj) { if (!(obj is Boolean)) return false; var other = (Boolean)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.ToString(this);
        public static implicit operator Dynamic(Boolean self) => new Dynamic(self);
        public static implicit operator Boolean(Dynamic value) => value.As<Boolean>();
        public String TypeName => "Boolean";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Boolean)b);
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Boolean)y);
        IBoolean IBoolean.And(IBoolean b) => this.And((Boolean)b);
        IBoolean IBoolean.Or(IBoolean b) => this.Or((Boolean)b);
        IBoolean IBoolean.Not => this.Not;
        // Implemented concept functions and type functions
        public Boolean And(Boolean y) => Intrinsics.And(this, y);
        public static Boolean operator &(Boolean x, Boolean y) => x.And(y);
        public Boolean Or(Boolean y) => Intrinsics.Or(this, y);
        public static Boolean operator |(Boolean x, Boolean y) => x.Or(y);
        public Boolean Not => Intrinsics.Not(this);
        public static Boolean operator !(Boolean x) => x.Not;
        public Boolean LessThanOrEquals(Boolean y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Boolean x, Boolean y) => x.LessThanOrEquals(y);
        public Boolean Equals(Boolean y) => Intrinsics.Equals(this, y);
        public static Boolean operator ==(Boolean x, Boolean y) => x.Equals(y);
        public IArray<Boolean> Repeat(Integer n){
            var _var667 = this;
            return n.MapRange((i) => _var667);
        }
        public Boolean NotEquals(Boolean b) => this.Equals(b).Not;
        public static Boolean operator !=(Boolean a, Boolean b) => a.NotEquals(b);
        public Boolean LessThan(Boolean b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Boolean a, Boolean b) => a.LessThan(b);
        public Boolean GreaterThan(Boolean b) => b.LessThan(this);
        public static Boolean operator >(Boolean a, Boolean b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Boolean b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Boolean a, Boolean b) => a.GreaterThanOrEquals(b);
        public Boolean Lesser(Boolean b) => this.LessThanOrEquals(b) ? this : b;
        public Boolean Greater(Boolean b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Boolean b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Character: IValue<Character>, IOrderable<Character>
    {
        [DataMember] public readonly char Value;
        public Character WithValue(char value) => new Character(value);
        public Character(char value) => (Value) = (value);
        public static Character Default = new Character();
        public static Character New(char value) => new Character(value);
        public Plato.SinglePrecision.Character ChangePrecision() => (Value.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Character(Character self) => self.ChangePrecision();
        public static implicit operator char(Character self) => self.Value;
        public static implicit operator Character(char value) => new Character(value);
        public override bool Equals(object obj) { if (!(obj is Character)) return false; var other = (Character)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.ToString(this);
        public static implicit operator Dynamic(Character self) => new Dynamic(self);
        public static implicit operator Character(Dynamic value) => value.As<Character>();
        public String TypeName => "Character";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Character)b);
        Boolean IOrderable.LessThanOrEquals(IOrderable y) => this.LessThanOrEquals((Character)y);
        // Implemented concept functions and type functions
        public Number Number => Intrinsics.Number(this);
        public static implicit operator Number(Character x) => x.Number;
        public Integer Integer => Intrinsics.Integer(this);
        public static implicit operator Integer(Character x) => x.Integer;
        public Boolean LessThanOrEquals(Character y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator <=(Character x, Character y) => x.LessThanOrEquals(y);
        public Boolean Equals(Character y) => Intrinsics.Equals(this, y);
        public static Boolean operator ==(Character x, Character y) => x.Equals(y);
        public IArray<Character> Repeat(Integer n){
            var _var668 = this;
            return n.MapRange((i) => _var668);
        }
        public Boolean NotEquals(Character b) => this.Equals(b).Not;
        public static Boolean operator !=(Character a, Character b) => a.NotEquals(b);
        public Boolean LessThan(Character b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator <(Character a, Character b) => a.LessThan(b);
        public Boolean GreaterThan(Character b) => b.LessThan(this);
        public static Boolean operator >(Character a, Character b) => a.GreaterThan(b);
        public Boolean GreaterThanOrEquals(Character b) => b.LessThanOrEquals(this);
        public static Boolean operator >=(Character a, Character b) => a.GreaterThanOrEquals(b);
        public Character Lesser(Character b) => this.LessThanOrEquals(b) ? this : b;
        public Character Greater(Character b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Character b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Type
    {
        [DataMember] public readonly System.Type Value;
        public Type WithValue(System.Type value) => new Type(value);
        public Type(System.Type value) => (Value) = (value);
        public static Type Default = new Type();
        public static Type New(System.Type value) => new Type(value);
        public Plato.SinglePrecision.Type ChangePrecision() => (Value.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Type(Type self) => self.ChangePrecision();
        public static implicit operator System.Type(Type self) => self.Value;
        public static implicit operator Type(System.Type value) => new Type(value);
        public override bool Equals(object obj) { if (!(obj is Type)) return false; var other = (Type)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.ToString(this);
        public static implicit operator Dynamic(Type self) => new Dynamic(self);
        public static implicit operator Type(Dynamic value) => value.As<Type>();
        public String TypeName => "Type";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Implemented concept functions and type functions
        public Dynamic New(IArray<IAny> args) => Intrinsics.New(this, args);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Error
    {
        public static Error Default = new Error();
        public static Error New() => new Error();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Error self) => new Dynamic(self);
        public static implicit operator Error(Dynamic value) => value.As<Error>();
        public String TypeName => "Error";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tuple2<T0, T1>
    {
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;
        public Tuple2<T0, T1> WithX0(T0 x0) => new Tuple2<T0, T1>(x0, X1);
        public Tuple2<T0, T1> WithX1(T1 x1) => new Tuple2<T0, T1>(X0, x1);
        public Tuple2(T0 x0, T1 x1) => (X0, X1) = (x0, x1);
        public static Tuple2<T0, T1> Default = new Tuple2<T0, T1>();
        public static Tuple2<T0, T1> New(T0 x0, T1 x1) => new Tuple2<T0, T1>(x0, x1);
        public static implicit operator (T0, T1)(Tuple2<T0, T1> self) => (self.X0, self.X1);
        public static implicit operator Tuple2<T0, T1>((T0, T1) value) => new Tuple2<T0, T1>(value.Item1, value.Item2);
        public void Deconstruct(out T0 x0, out T1 x1) { x0 = X0; x1 = X1; }
        public override bool Equals(object obj) { if (!(obj is Tuple2<T0, T1>)) return false; var other = (Tuple2<T0, T1>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1);
        public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1} }}";
        public static implicit operator Dynamic(Tuple2<T0, T1> self) => new Dynamic(self);
        public static implicit operator Tuple2<T0, T1>(Dynamic value) => value.As<Tuple2<T0, T1>>();
        public String TypeName => "Tuple2<T0, T1>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tuple3<T0, T1, T2>
    {
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;
        [DataMember] public readonly T2 X2;
        public Tuple3<T0, T1, T2> WithX0(T0 x0) => new Tuple3<T0, T1, T2>(x0, X1, X2);
        public Tuple3<T0, T1, T2> WithX1(T1 x1) => new Tuple3<T0, T1, T2>(X0, x1, X2);
        public Tuple3<T0, T1, T2> WithX2(T2 x2) => new Tuple3<T0, T1, T2>(X0, X1, x2);
        public Tuple3(T0 x0, T1 x1, T2 x2) => (X0, X1, X2) = (x0, x1, x2);
        public static Tuple3<T0, T1, T2> Default = new Tuple3<T0, T1, T2>();
        public static Tuple3<T0, T1, T2> New(T0 x0, T1 x1, T2 x2) => new Tuple3<T0, T1, T2>(x0, x1, x2);
        public static implicit operator (T0, T1, T2)(Tuple3<T0, T1, T2> self) => (self.X0, self.X1, self.X2);
        public static implicit operator Tuple3<T0, T1, T2>((T0, T1, T2) value) => new Tuple3<T0, T1, T2>(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2) { x0 = X0; x1 = X1; x2 = X2; }
        public override bool Equals(object obj) { if (!(obj is Tuple3<T0, T1, T2>)) return false; var other = (Tuple3<T0, T1, T2>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2);
        public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1}, \"X2\" = {X2} }}";
        public static implicit operator Dynamic(Tuple3<T0, T1, T2> self) => new Dynamic(self);
        public static implicit operator Tuple3<T0, T1, T2>(Dynamic value) => value.As<Tuple3<T0, T1, T2>>();
        public String TypeName => "Tuple3<T0, T1, T2>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tuple4<T0, T1, T2, T3>
    {
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;
        [DataMember] public readonly T2 X2;
        [DataMember] public readonly T3 X3;
        public Tuple4<T0, T1, T2, T3> WithX0(T0 x0) => new Tuple4<T0, T1, T2, T3>(x0, X1, X2, X3);
        public Tuple4<T0, T1, T2, T3> WithX1(T1 x1) => new Tuple4<T0, T1, T2, T3>(X0, x1, X2, X3);
        public Tuple4<T0, T1, T2, T3> WithX2(T2 x2) => new Tuple4<T0, T1, T2, T3>(X0, X1, x2, X3);
        public Tuple4<T0, T1, T2, T3> WithX3(T3 x3) => new Tuple4<T0, T1, T2, T3>(X0, X1, X2, x3);
        public Tuple4(T0 x0, T1 x1, T2 x2, T3 x3) => (X0, X1, X2, X3) = (x0, x1, x2, x3);
        public static Tuple4<T0, T1, T2, T3> Default = new Tuple4<T0, T1, T2, T3>();
        public static Tuple4<T0, T1, T2, T3> New(T0 x0, T1 x1, T2 x2, T3 x3) => new Tuple4<T0, T1, T2, T3>(x0, x1, x2, x3);
        public static implicit operator (T0, T1, T2, T3)(Tuple4<T0, T1, T2, T3> self) => (self.X0, self.X1, self.X2, self.X3);
        public static implicit operator Tuple4<T0, T1, T2, T3>((T0, T1, T2, T3) value) => new Tuple4<T0, T1, T2, T3>(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; }
        public override bool Equals(object obj) { if (!(obj is Tuple4<T0, T1, T2, T3>)) return false; var other = (Tuple4<T0, T1, T2, T3>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3);
        public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1}, \"X2\" = {X2}, \"X3\" = {X3} }}";
        public static implicit operator Dynamic(Tuple4<T0, T1, T2, T3> self) => new Dynamic(self);
        public static implicit operator Tuple4<T0, T1, T2, T3>(Dynamic value) => value.As<Tuple4<T0, T1, T2, T3>>();
        public String TypeName => "Tuple4<T0, T1, T2, T3>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tuple5<T0, T1, T2, T3, T4>
    {
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;
        [DataMember] public readonly T2 X2;
        [DataMember] public readonly T3 X3;
        [DataMember] public readonly T4 X4;
        public Tuple5<T0, T1, T2, T3, T4> WithX0(T0 x0) => new Tuple5<T0, T1, T2, T3, T4>(x0, X1, X2, X3, X4);
        public Tuple5<T0, T1, T2, T3, T4> WithX1(T1 x1) => new Tuple5<T0, T1, T2, T3, T4>(X0, x1, X2, X3, X4);
        public Tuple5<T0, T1, T2, T3, T4> WithX2(T2 x2) => new Tuple5<T0, T1, T2, T3, T4>(X0, X1, x2, X3, X4);
        public Tuple5<T0, T1, T2, T3, T4> WithX3(T3 x3) => new Tuple5<T0, T1, T2, T3, T4>(X0, X1, X2, x3, X4);
        public Tuple5<T0, T1, T2, T3, T4> WithX4(T4 x4) => new Tuple5<T0, T1, T2, T3, T4>(X0, X1, X2, X3, x4);
        public Tuple5(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4) => (X0, X1, X2, X3, X4) = (x0, x1, x2, x3, x4);
        public static Tuple5<T0, T1, T2, T3, T4> Default = new Tuple5<T0, T1, T2, T3, T4>();
        public static Tuple5<T0, T1, T2, T3, T4> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4) => new Tuple5<T0, T1, T2, T3, T4>(x0, x1, x2, x3, x4);
        public static implicit operator (T0, T1, T2, T3, T4)(Tuple5<T0, T1, T2, T3, T4> self) => (self.X0, self.X1, self.X2, self.X3, self.X4);
        public static implicit operator Tuple5<T0, T1, T2, T3, T4>((T0, T1, T2, T3, T4) value) => new Tuple5<T0, T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; }
        public override bool Equals(object obj) { if (!(obj is Tuple5<T0, T1, T2, T3, T4>)) return false; var other = (Tuple5<T0, T1, T2, T3, T4>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4);
        public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1}, \"X2\" = {X2}, \"X3\" = {X3}, \"X4\" = {X4} }}";
        public static implicit operator Dynamic(Tuple5<T0, T1, T2, T3, T4> self) => new Dynamic(self);
        public static implicit operator Tuple5<T0, T1, T2, T3, T4>(Dynamic value) => value.As<Tuple5<T0, T1, T2, T3, T4>>();
        public String TypeName => "Tuple5<T0, T1, T2, T3, T4>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tuple6<T0, T1, T2, T3, T4, T5>
    {
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;
        [DataMember] public readonly T2 X2;
        [DataMember] public readonly T3 X3;
        [DataMember] public readonly T4 X4;
        [DataMember] public readonly T5 X5;
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX0(T0 x0) => new Tuple6<T0, T1, T2, T3, T4, T5>(x0, X1, X2, X3, X4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX1(T1 x1) => new Tuple6<T0, T1, T2, T3, T4, T5>(X0, x1, X2, X3, X4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX2(T2 x2) => new Tuple6<T0, T1, T2, T3, T4, T5>(X0, X1, x2, X3, X4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX3(T3 x3) => new Tuple6<T0, T1, T2, T3, T4, T5>(X0, X1, X2, x3, X4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX4(T4 x4) => new Tuple6<T0, T1, T2, T3, T4, T5>(X0, X1, X2, X3, x4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX5(T5 x5) => new Tuple6<T0, T1, T2, T3, T4, T5>(X0, X1, X2, X3, X4, x5);
        public Tuple6(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5) => (X0, X1, X2, X3, X4, X5) = (x0, x1, x2, x3, x4, x5);
        public static Tuple6<T0, T1, T2, T3, T4, T5> Default = new Tuple6<T0, T1, T2, T3, T4, T5>();
        public static Tuple6<T0, T1, T2, T3, T4, T5> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5) => new Tuple6<T0, T1, T2, T3, T4, T5>(x0, x1, x2, x3, x4, x5);
        public static implicit operator (T0, T1, T2, T3, T4, T5)(Tuple6<T0, T1, T2, T3, T4, T5> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5);
        public static implicit operator Tuple6<T0, T1, T2, T3, T4, T5>((T0, T1, T2, T3, T4, T5) value) => new Tuple6<T0, T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; }
        public override bool Equals(object obj) { if (!(obj is Tuple6<T0, T1, T2, T3, T4, T5>)) return false; var other = (Tuple6<T0, T1, T2, T3, T4, T5>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5);
        public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1}, \"X2\" = {X2}, \"X3\" = {X3}, \"X4\" = {X4}, \"X5\" = {X5} }}";
        public static implicit operator Dynamic(Tuple6<T0, T1, T2, T3, T4, T5> self) => new Dynamic(self);
        public static implicit operator Tuple6<T0, T1, T2, T3, T4, T5>(Dynamic value) => value.As<Tuple6<T0, T1, T2, T3, T4, T5>>();
        public String TypeName => "Tuple6<T0, T1, T2, T3, T4, T5>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tuple7<T0, T1, T2, T3, T4, T5, T6>
    {
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;
        [DataMember] public readonly T2 X2;
        [DataMember] public readonly T3 X3;
        [DataMember] public readonly T4 X4;
        [DataMember] public readonly T5 X5;
        [DataMember] public readonly T6 X6;
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX0(T0 x0) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(x0, X1, X2, X3, X4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX1(T1 x1) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(X0, x1, X2, X3, X4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX2(T2 x2) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(X0, X1, x2, X3, X4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX3(T3 x3) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(X0, X1, X2, x3, X4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX4(T4 x4) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(X0, X1, X2, X3, x4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX5(T5 x5) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(X0, X1, X2, X3, X4, x5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX6(T6 x6) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(X0, X1, X2, X3, X4, X5, x6);
        public Tuple7(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6) => (X0, X1, X2, X3, X4, X5, X6) = (x0, x1, x2, x3, x4, x5, x6);
        public static Tuple7<T0, T1, T2, T3, T4, T5, T6> Default = new Tuple7<T0, T1, T2, T3, T4, T5, T6>();
        public static Tuple7<T0, T1, T2, T3, T4, T5, T6> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(x0, x1, x2, x3, x4, x5, x6);
        public static implicit operator (T0, T1, T2, T3, T4, T5, T6)(Tuple7<T0, T1, T2, T3, T4, T5, T6> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5, self.X6);
        public static implicit operator Tuple7<T0, T1, T2, T3, T4, T5, T6>((T0, T1, T2, T3, T4, T5, T6) value) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5, out T6 x6) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; x6 = X6; }
        public override bool Equals(object obj) { if (!(obj is Tuple7<T0, T1, T2, T3, T4, T5, T6>)) return false; var other = (Tuple7<T0, T1, T2, T3, T4, T5, T6>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5) && X6.Equals(other.X6); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5, X6);
        public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1}, \"X2\" = {X2}, \"X3\" = {X3}, \"X4\" = {X4}, \"X5\" = {X5}, \"X6\" = {X6} }}";
        public static implicit operator Dynamic(Tuple7<T0, T1, T2, T3, T4, T5, T6> self) => new Dynamic(self);
        public static implicit operator Tuple7<T0, T1, T2, T3, T4, T5, T6>(Dynamic value) => value.As<Tuple7<T0, T1, T2, T3, T4, T5, T6>>();
        public String TypeName => "Tuple7<T0, T1, T2, T3, T4, T5, T6>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5", (String)"X6");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5), new Dynamic(X6));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>
    {
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;
        [DataMember] public readonly T2 X2;
        [DataMember] public readonly T3 X3;
        [DataMember] public readonly T4 X4;
        [DataMember] public readonly T5 X5;
        [DataMember] public readonly T6 X6;
        [DataMember] public readonly T7 X7;
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX0(T0 x0) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(x0, X1, X2, X3, X4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX1(T1 x1) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(X0, x1, X2, X3, X4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX2(T2 x2) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(X0, X1, x2, X3, X4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX3(T3 x3) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(X0, X1, X2, x3, X4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX4(T4 x4) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(X0, X1, X2, X3, x4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX5(T5 x5) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(X0, X1, X2, X3, X4, x5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX6(T6 x6) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(X0, X1, X2, X3, X4, X5, x6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX7(T7 x7) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(X0, X1, X2, X3, X4, X5, X6, x7);
        public Tuple8(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7) => (X0, X1, X2, X3, X4, X5, X6, X7) = (x0, x1, x2, x3, x4, x5, x6, x7);
        public static Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> Default = new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>();
        public static Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(x0, x1, x2, x3, x4, x5, x6, x7);
        public static implicit operator (T0, T1, T2, T3, T4, T5, T6, T7)(Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5, self.X6, self.X7);
        public static implicit operator Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>((T0, T1, T2, T3, T4, T5, T6, T7) value) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, value.Item8);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5, out T6 x6, out T7 x7) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; x6 = X6; x7 = X7; }
        public override bool Equals(object obj) { if (!(obj is Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>)) return false; var other = (Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5) && X6.Equals(other.X6) && X7.Equals(other.X7); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5, X6, X7);
        public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1}, \"X2\" = {X2}, \"X3\" = {X3}, \"X4\" = {X4}, \"X5\" = {X5}, \"X6\" = {X6}, \"X7\" = {X7} }}";
        public static implicit operator Dynamic(Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> self) => new Dynamic(self);
        public static implicit operator Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(Dynamic value) => value.As<Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>>();
        public String TypeName => "Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5", (String)"X6", (String)"X7");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5), new Dynamic(X6), new Dynamic(X7));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;
        [DataMember] public readonly T2 X2;
        [DataMember] public readonly T3 X3;
        [DataMember] public readonly T4 X4;
        [DataMember] public readonly T5 X5;
        [DataMember] public readonly T6 X6;
        [DataMember] public readonly T7 X7;
        [DataMember] public readonly T8 X8;
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX0(T0 x0) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(x0, X1, X2, X3, X4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX1(T1 x1) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(X0, x1, X2, X3, X4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX2(T2 x2) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(X0, X1, x2, X3, X4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX3(T3 x3) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(X0, X1, X2, x3, X4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX4(T4 x4) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(X0, X1, X2, X3, x4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX5(T5 x5) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(X0, X1, X2, X3, X4, x5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX6(T6 x6) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(X0, X1, X2, X3, X4, X5, x6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX7(T7 x7) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(X0, X1, X2, X3, X4, X5, X6, x7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX8(T8 x8) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(X0, X1, X2, X3, X4, X5, X6, X7, x8);
        public Tuple9(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7, T8 x8) => (X0, X1, X2, X3, X4, X5, X6, X7, X8) = (x0, x1, x2, x3, x4, x5, x6, x7, x8);
        public static Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> Default = new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>();
        public static Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7, T8 x8) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(x0, x1, x2, x3, x4, x5, x6, x7, x8);
        public static implicit operator (T0, T1, T2, T3, T4, T5, T6, T7, T8)(Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5, self.X6, self.X7, self.X8);
        public static implicit operator Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>((T0, T1, T2, T3, T4, T5, T6, T7, T8) value) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, value.Item8, value.Item9);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5, out T6 x6, out T7 x7, out T8 x8) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; x6 = X6; x7 = X7; x8 = X8; }
        public override bool Equals(object obj) { if (!(obj is Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>)) return false; var other = (Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5) && X6.Equals(other.X6) && X7.Equals(other.X7) && X8.Equals(other.X8); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5, X6, X7, X8);
        public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1}, \"X2\" = {X2}, \"X3\" = {X3}, \"X4\" = {X4}, \"X5\" = {X5}, \"X6\" = {X6}, \"X7\" = {X7}, \"X8\" = {X8} }}";
        public static implicit operator Dynamic(Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> self) => new Dynamic(self);
        public static implicit operator Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Dynamic value) => value.As<Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>>();
        public String TypeName => "Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5", (String)"X6", (String)"X7", (String)"X8");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5), new Dynamic(X6), new Dynamic(X7), new Dynamic(X8));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;
        [DataMember] public readonly T2 X2;
        [DataMember] public readonly T3 X3;
        [DataMember] public readonly T4 X4;
        [DataMember] public readonly T5 X5;
        [DataMember] public readonly T6 X6;
        [DataMember] public readonly T7 X7;
        [DataMember] public readonly T8 X8;
        [DataMember] public readonly T9 X9;
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX0(T0 x0) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(x0, X1, X2, X3, X4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX1(T1 x1) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(X0, x1, X2, X3, X4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX2(T2 x2) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(X0, X1, x2, X3, X4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX3(T3 x3) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(X0, X1, X2, x3, X4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX4(T4 x4) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(X0, X1, X2, X3, x4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX5(T5 x5) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(X0, X1, X2, X3, X4, x5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX6(T6 x6) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(X0, X1, X2, X3, X4, X5, x6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX7(T7 x7) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(X0, X1, X2, X3, X4, X5, X6, x7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX8(T8 x8) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(X0, X1, X2, X3, X4, X5, X6, X7, x8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX9(T9 x9) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(X0, X1, X2, X3, X4, X5, X6, X7, X8, x9);
        public Tuple10(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7, T8 x8, T9 x9) => (X0, X1, X2, X3, X4, X5, X6, X7, X8, X9) = (x0, x1, x2, x3, x4, x5, x6, x7, x8, x9);
        public static Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Default = new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        public static Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7, T8 x8, T9 x9) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(x0, x1, x2, x3, x4, x5, x6, x7, x8, x9);
        public static implicit operator (T0, T1, T2, T3, T4, T5, T6, T7, T8, T9)(Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5, self.X6, self.X7, self.X8, self.X9);
        public static implicit operator Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>((T0, T1, T2, T3, T4, T5, T6, T7, T8, T9) value) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, value.Item8, value.Item9, value.Item10);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5, out T6 x6, out T7 x7, out T8 x8, out T9 x9) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; x6 = X6; x7 = X7; x8 = X8; x9 = X9; }
        public override bool Equals(object obj) { if (!(obj is Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>)) return false; var other = (Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5) && X6.Equals(other.X6) && X7.Equals(other.X7) && X8.Equals(other.X8) && X9.Equals(other.X9); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5, X6, X7, X8, X9);
        public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1}, \"X2\" = {X2}, \"X3\" = {X3}, \"X4\" = {X4}, \"X5\" = {X5}, \"X6\" = {X6}, \"X7\" = {X7}, \"X8\" = {X8}, \"X9\" = {X9} }}";
        public static implicit operator Dynamic(Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> self) => new Dynamic(self);
        public static implicit operator Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Dynamic value) => value.As<Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>();
        public String TypeName => "Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5", (String)"X6", (String)"X7", (String)"X8", (String)"X9");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5), new Dynamic(X6), new Dynamic(X7), new Dynamic(X8), new Dynamic(X9));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Function4<T0, T1, T2, T3, TR>
    {
        public static Function4<T0, T1, T2, T3, TR> Default = new Function4<T0, T1, T2, T3, TR>();
        public static Function4<T0, T1, T2, T3, TR> New() => new Function4<T0, T1, T2, T3, TR>();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Function4<T0, T1, T2, T3, TR> self) => new Dynamic(self);
        public static implicit operator Function4<T0, T1, T2, T3, TR>(Dynamic value) => value.As<Function4<T0, T1, T2, T3, TR>>();
        public String TypeName => "Function4<T0, T1, T2, T3, TR>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        public TR Invoke(T0 a0, T1 a1, T2 a2, T3 a3) => Intrinsics.Invoke(this, a0, a1, a2, a3);
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Function5<T0, T1, T2, T3, T4, TR>
    {
        public static Function5<T0, T1, T2, T3, T4, TR> Default = new Function5<T0, T1, T2, T3, T4, TR>();
        public static Function5<T0, T1, T2, T3, T4, TR> New() => new Function5<T0, T1, T2, T3, T4, TR>();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Function5<T0, T1, T2, T3, T4, TR> self) => new Dynamic(self);
        public static implicit operator Function5<T0, T1, T2, T3, T4, TR>(Dynamic value) => value.As<Function5<T0, T1, T2, T3, T4, TR>>();
        public String TypeName => "Function5<T0, T1, T2, T3, T4, TR>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Function6<T0, T1, T2, T3, T4, T5, TR>
    {
        public static Function6<T0, T1, T2, T3, T4, T5, TR> Default = new Function6<T0, T1, T2, T3, T4, T5, TR>();
        public static Function6<T0, T1, T2, T3, T4, T5, TR> New() => new Function6<T0, T1, T2, T3, T4, T5, TR>();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Function6<T0, T1, T2, T3, T4, T5, TR> self) => new Dynamic(self);
        public static implicit operator Function6<T0, T1, T2, T3, T4, T5, TR>(Dynamic value) => value.As<Function6<T0, T1, T2, T3, T4, T5, TR>>();
        public String TypeName => "Function6<T0, T1, T2, T3, T4, T5, TR>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Function7<T0, T1, T2, T3, T4, T5, T6, TR>
    {
        public static Function7<T0, T1, T2, T3, T4, T5, T6, TR> Default = new Function7<T0, T1, T2, T3, T4, T5, T6, TR>();
        public static Function7<T0, T1, T2, T3, T4, T5, T6, TR> New() => new Function7<T0, T1, T2, T3, T4, T5, T6, TR>();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Function7<T0, T1, T2, T3, T4, T5, T6, TR> self) => new Dynamic(self);
        public static implicit operator Function7<T0, T1, T2, T3, T4, T5, T6, TR>(Dynamic value) => value.As<Function7<T0, T1, T2, T3, T4, T5, T6, TR>>();
        public String TypeName => "Function7<T0, T1, T2, T3, T4, T5, T6, TR>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>
    {
        public static Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR> Default = new Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>();
        public static Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR> New() => new Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR> self) => new Dynamic(self);
        public static implicit operator Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>(Dynamic value) => value.As<Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>>();
        public String TypeName => "Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>
    {
        public static Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> Default = new Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>();
        public static Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> New() => new Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> self) => new Dynamic(self);
        public static implicit operator Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>(Dynamic value) => value.As<Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>>();
        public String TypeName => "Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>
    {
        public static Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> Default = new Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>();
        public static Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> New() => new Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> self) => new Dynamic(self);
        public static implicit operator Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>(Dynamic value) => value.As<Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>>();
        public String TypeName => "Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Sphere: ISolid
    {
        [DataMember] public readonly Number Radius;
        public Sphere WithRadius(Number radius) => new Sphere(radius);
        public Sphere(Number radius) => (Radius) = (radius);
        public static Sphere Default = new Sphere();
        public static Sphere New(Number radius) => new Sphere(radius);
        public Plato.SinglePrecision.Sphere ChangePrecision() => (Radius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Sphere(Sphere self) => self.ChangePrecision();
        public static implicit operator Number(Sphere self) => self.Radius;
        public static implicit operator Sphere(Number value) => new Sphere(value);
        public static implicit operator Sphere(Integer value) => new Sphere(value);
        public static implicit operator Sphere(int value) => new Integer(value);
        public static implicit operator Sphere(double value) => new Number(value);
        public static implicit operator double(Sphere value) => value.Radius;
        public override bool Equals(object obj) { if (!(obj is Sphere)) return false; var other = (Sphere)obj; return Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius);
        public override string ToString() => $"{{ \"Radius\" = {Radius} }}";
        public static implicit operator Dynamic(Sphere self) => new Dynamic(self);
        public static implicit operator Sphere(Dynamic value) => value.As<Sphere>();
        public String TypeName => "Sphere";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Cylinder: ISurface
    {
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number Radius;
        public Cylinder WithHeight(Number height) => new Cylinder(height, Radius);
        public Cylinder WithRadius(Number radius) => new Cylinder(Height, radius);
        public Cylinder(Number height, Number radius) => (Height, Radius) = (height, radius);
        public static Cylinder Default = new Cylinder();
        public static Cylinder New(Number height, Number radius) => new Cylinder(height, radius);
        public Plato.SinglePrecision.Cylinder ChangePrecision() => (Height.ChangePrecision(), Radius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Cylinder(Cylinder self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Cylinder self) => (self.Height, self.Radius);
        public static implicit operator Cylinder((Number, Number) value) => new Cylinder(value.Item1, value.Item2);
        public void Deconstruct(out Number height, out Number radius) { height = Height; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is Cylinder)) return false; var other = (Cylinder)obj; return Height.Equals(other.Height) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Height, Radius);
        public override string ToString() => $"{{ \"Height\" = {Height}, \"Radius\" = {Radius} }}";
        public static implicit operator Dynamic(Cylinder self) => new Dynamic(self);
        public static implicit operator Cylinder(Dynamic value) => value.As<Cylinder>();
        public String TypeName => "Cylinder";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Height", (String)"Radius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Height), new Dynamic(Radius));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Capsule: ISolid
    {
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number Radius;
        public Capsule WithHeight(Number height) => new Capsule(height, Radius);
        public Capsule WithRadius(Number radius) => new Capsule(Height, radius);
        public Capsule(Number height, Number radius) => (Height, Radius) = (height, radius);
        public static Capsule Default = new Capsule();
        public static Capsule New(Number height, Number radius) => new Capsule(height, radius);
        public Plato.SinglePrecision.Capsule ChangePrecision() => (Height.ChangePrecision(), Radius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Capsule(Capsule self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Capsule self) => (self.Height, self.Radius);
        public static implicit operator Capsule((Number, Number) value) => new Capsule(value.Item1, value.Item2);
        public void Deconstruct(out Number height, out Number radius) { height = Height; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is Capsule)) return false; var other = (Capsule)obj; return Height.Equals(other.Height) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Height, Radius);
        public override string ToString() => $"{{ \"Height\" = {Height}, \"Radius\" = {Radius} }}";
        public static implicit operator Dynamic(Capsule self) => new Dynamic(self);
        public static implicit operator Capsule(Dynamic value) => value.As<Capsule>();
        public String TypeName => "Capsule";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Height", (String)"Radius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Height), new Dynamic(Radius));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Cone: ISolid
    {
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number Radius;
        public Cone WithHeight(Number height) => new Cone(height, Radius);
        public Cone WithRadius(Number radius) => new Cone(Height, radius);
        public Cone(Number height, Number radius) => (Height, Radius) = (height, radius);
        public static Cone Default = new Cone();
        public static Cone New(Number height, Number radius) => new Cone(height, radius);
        public Plato.SinglePrecision.Cone ChangePrecision() => (Height.ChangePrecision(), Radius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Cone(Cone self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Cone self) => (self.Height, self.Radius);
        public static implicit operator Cone((Number, Number) value) => new Cone(value.Item1, value.Item2);
        public void Deconstruct(out Number height, out Number radius) { height = Height; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is Cone)) return false; var other = (Cone)obj; return Height.Equals(other.Height) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Height, Radius);
        public override string ToString() => $"{{ \"Height\" = {Height}, \"Radius\" = {Radius} }}";
        public static implicit operator Dynamic(Cone self) => new Dynamic(self);
        public static implicit operator Cone(Dynamic value) => value.As<Cone>();
        public String TypeName => "Cone";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Height", (String)"Radius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Height), new Dynamic(Radius));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct ConeSegment: ISolid
    {
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number Radius1;
        [DataMember] public readonly Number Radius2;
        public ConeSegment WithHeight(Number height) => new ConeSegment(height, Radius1, Radius2);
        public ConeSegment WithRadius1(Number radius1) => new ConeSegment(Height, radius1, Radius2);
        public ConeSegment WithRadius2(Number radius2) => new ConeSegment(Height, Radius1, radius2);
        public ConeSegment(Number height, Number radius1, Number radius2) => (Height, Radius1, Radius2) = (height, radius1, radius2);
        public static ConeSegment Default = new ConeSegment();
        public static ConeSegment New(Number height, Number radius1, Number radius2) => new ConeSegment(height, radius1, radius2);
        public Plato.SinglePrecision.ConeSegment ChangePrecision() => (Height.ChangePrecision(), Radius1.ChangePrecision(), Radius2.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.ConeSegment(ConeSegment self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number)(ConeSegment self) => (self.Height, self.Radius1, self.Radius2);
        public static implicit operator ConeSegment((Number, Number, Number) value) => new ConeSegment(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number height, out Number radius1, out Number radius2) { height = Height; radius1 = Radius1; radius2 = Radius2; }
        public override bool Equals(object obj) { if (!(obj is ConeSegment)) return false; var other = (ConeSegment)obj; return Height.Equals(other.Height) && Radius1.Equals(other.Radius1) && Radius2.Equals(other.Radius2); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Height, Radius1, Radius2);
        public override string ToString() => $"{{ \"Height\" = {Height}, \"Radius1\" = {Radius1}, \"Radius2\" = {Radius2} }}";
        public static implicit operator Dynamic(ConeSegment self) => new Dynamic(self);
        public static implicit operator ConeSegment(Dynamic value) => value.As<ConeSegment>();
        public String TypeName => "ConeSegment";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Height", (String)"Radius1", (String)"Radius2");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Height), new Dynamic(Radius1), new Dynamic(Radius2));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Box: ISolid
    {
        [DataMember] public readonly Vector3D Extent;
        public Box WithExtent(Vector3D extent) => new Box(extent);
        public Box(Vector3D extent) => (Extent) = (extent);
        public static Box Default = new Box();
        public static Box New(Vector3D extent) => new Box(extent);
        public Plato.SinglePrecision.Box ChangePrecision() => (Extent.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Box(Box self) => self.ChangePrecision();
        public static implicit operator Vector3D(Box self) => self.Extent;
        public static implicit operator Box(Vector3D value) => new Box(value);
        public override bool Equals(object obj) { if (!(obj is Box)) return false; var other = (Box)obj; return Extent.Equals(other.Extent); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Extent);
        public override string ToString() => $"{{ \"Extent\" = {Extent} }}";
        public static implicit operator Dynamic(Box self) => new Dynamic(self);
        public static implicit operator Box(Dynamic value) => value.As<Box>();
        public String TypeName => "Box";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Extent");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Extent));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Pyramid: ISolid
    {
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number BaseLength;
        public Pyramid WithHeight(Number height) => new Pyramid(height, BaseLength);
        public Pyramid WithBaseLength(Number baseLength) => new Pyramid(Height, baseLength);
        public Pyramid(Number height, Number baseLength) => (Height, BaseLength) = (height, baseLength);
        public static Pyramid Default = new Pyramid();
        public static Pyramid New(Number height, Number baseLength) => new Pyramid(height, baseLength);
        public Plato.SinglePrecision.Pyramid ChangePrecision() => (Height.ChangePrecision(), BaseLength.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Pyramid(Pyramid self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Pyramid self) => (self.Height, self.BaseLength);
        public static implicit operator Pyramid((Number, Number) value) => new Pyramid(value.Item1, value.Item2);
        public void Deconstruct(out Number height, out Number baseLength) { height = Height; baseLength = BaseLength; }
        public override bool Equals(object obj) { if (!(obj is Pyramid)) return false; var other = (Pyramid)obj; return Height.Equals(other.Height) && BaseLength.Equals(other.BaseLength); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Height, BaseLength);
        public override string ToString() => $"{{ \"Height\" = {Height}, \"BaseLength\" = {BaseLength} }}";
        public static implicit operator Dynamic(Pyramid self) => new Dynamic(self);
        public static implicit operator Pyramid(Dynamic value) => value.As<Pyramid>();
        public String TypeName => "Pyramid";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Height", (String)"BaseLength");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Height), new Dynamic(BaseLength));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Torus: ISolid
    {
        [DataMember] public readonly Number MajorRadius;
        [DataMember] public readonly Number MinorRadius;
        public Torus WithMajorRadius(Number majorRadius) => new Torus(majorRadius, MinorRadius);
        public Torus WithMinorRadius(Number minorRadius) => new Torus(MajorRadius, minorRadius);
        public Torus(Number majorRadius, Number minorRadius) => (MajorRadius, MinorRadius) = (majorRadius, minorRadius);
        public static Torus Default = new Torus();
        public static Torus New(Number majorRadius, Number minorRadius) => new Torus(majorRadius, minorRadius);
        public Plato.SinglePrecision.Torus ChangePrecision() => (MajorRadius.ChangePrecision(), MinorRadius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Torus(Torus self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Torus self) => (self.MajorRadius, self.MinorRadius);
        public static implicit operator Torus((Number, Number) value) => new Torus(value.Item1, value.Item2);
        public void Deconstruct(out Number majorRadius, out Number minorRadius) { majorRadius = MajorRadius; minorRadius = MinorRadius; }
        public override bool Equals(object obj) { if (!(obj is Torus)) return false; var other = (Torus)obj; return MajorRadius.Equals(other.MajorRadius) && MinorRadius.Equals(other.MinorRadius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(MajorRadius, MinorRadius);
        public override string ToString() => $"{{ \"MajorRadius\" = {MajorRadius}, \"MinorRadius\" = {MinorRadius} }}";
        public static implicit operator Dynamic(Torus self) => new Dynamic(self);
        public static implicit operator Torus(Dynamic value) => value.As<Torus>();
        public String TypeName => "Torus";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"MajorRadius", (String)"MinorRadius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(MajorRadius), new Dynamic(MinorRadius));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct NPrism: ISolid
    {
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number Radius;
        [DataMember] public readonly Integer NumSides;
        public NPrism WithHeight(Number height) => new NPrism(height, Radius, NumSides);
        public NPrism WithRadius(Number radius) => new NPrism(Height, radius, NumSides);
        public NPrism WithNumSides(Integer numSides) => new NPrism(Height, Radius, numSides);
        public NPrism(Number height, Number radius, Integer numSides) => (Height, Radius, NumSides) = (height, radius, numSides);
        public static NPrism Default = new NPrism();
        public static NPrism New(Number height, Number radius, Integer numSides) => new NPrism(height, radius, numSides);
        public Plato.SinglePrecision.NPrism ChangePrecision() => (Height.ChangePrecision(), Radius.ChangePrecision(), NumSides.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.NPrism(NPrism self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Integer)(NPrism self) => (self.Height, self.Radius, self.NumSides);
        public static implicit operator NPrism((Number, Number, Integer) value) => new NPrism(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number height, out Number radius, out Integer numSides) { height = Height; radius = Radius; numSides = NumSides; }
        public override bool Equals(object obj) { if (!(obj is NPrism)) return false; var other = (NPrism)obj; return Height.Equals(other.Height) && Radius.Equals(other.Radius) && NumSides.Equals(other.NumSides); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Height, Radius, NumSides);
        public override string ToString() => $"{{ \"Height\" = {Height}, \"Radius\" = {Radius}, \"NumSides\" = {NumSides} }}";
        public static implicit operator Dynamic(NPrism self) => new Dynamic(self);
        public static implicit operator NPrism(Dynamic value) => value.As<NPrism>();
        public String TypeName => "NPrism";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Height", (String)"Radius", (String)"NumSides");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Height), new Dynamic(Radius), new Dynamic(NumSides));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Tube: ISolid
    {
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number InnerRadius;
        [DataMember] public readonly Number OuterRadius;
        public Tube WithHeight(Number height) => new Tube(height, InnerRadius, OuterRadius);
        public Tube WithInnerRadius(Number innerRadius) => new Tube(Height, innerRadius, OuterRadius);
        public Tube WithOuterRadius(Number outerRadius) => new Tube(Height, InnerRadius, outerRadius);
        public Tube(Number height, Number innerRadius, Number outerRadius) => (Height, InnerRadius, OuterRadius) = (height, innerRadius, outerRadius);
        public static Tube Default = new Tube();
        public static Tube New(Number height, Number innerRadius, Number outerRadius) => new Tube(height, innerRadius, outerRadius);
        public Plato.SinglePrecision.Tube ChangePrecision() => (Height.ChangePrecision(), InnerRadius.ChangePrecision(), OuterRadius.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Tube(Tube self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number)(Tube self) => (self.Height, self.InnerRadius, self.OuterRadius);
        public static implicit operator Tube((Number, Number, Number) value) => new Tube(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number height, out Number innerRadius, out Number outerRadius) { height = Height; innerRadius = InnerRadius; outerRadius = OuterRadius; }
        public override bool Equals(object obj) { if (!(obj is Tube)) return false; var other = (Tube)obj; return Height.Equals(other.Height) && InnerRadius.Equals(other.InnerRadius) && OuterRadius.Equals(other.OuterRadius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Height, InnerRadius, OuterRadius);
        public override string ToString() => $"{{ \"Height\" = {Height}, \"InnerRadius\" = {InnerRadius}, \"OuterRadius\" = {OuterRadius} }}";
        public static implicit operator Dynamic(Tube self) => new Dynamic(self);
        public static implicit operator Tube(Dynamic value) => value.As<Tube>();
        public String TypeName => "Tube";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Height", (String)"InnerRadius", (String)"OuterRadius");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Height), new Dynamic(InnerRadius), new Dynamic(OuterRadius));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct NPyramid: ISolid
    {
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number Radius;
        [DataMember] public readonly Integer NumSides;
        public NPyramid WithHeight(Number height) => new NPyramid(height, Radius, NumSides);
        public NPyramid WithRadius(Number radius) => new NPyramid(Height, radius, NumSides);
        public NPyramid WithNumSides(Integer numSides) => new NPyramid(Height, Radius, numSides);
        public NPyramid(Number height, Number radius, Integer numSides) => (Height, Radius, NumSides) = (height, radius, numSides);
        public static NPyramid Default = new NPyramid();
        public static NPyramid New(Number height, Number radius, Integer numSides) => new NPyramid(height, radius, numSides);
        public Plato.SinglePrecision.NPyramid ChangePrecision() => (Height.ChangePrecision(), Radius.ChangePrecision(), NumSides.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.NPyramid(NPyramid self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Integer)(NPyramid self) => (self.Height, self.Radius, self.NumSides);
        public static implicit operator NPyramid((Number, Number, Integer) value) => new NPyramid(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number height, out Number radius, out Integer numSides) { height = Height; radius = Radius; numSides = NumSides; }
        public override bool Equals(object obj) { if (!(obj is NPyramid)) return false; var other = (NPyramid)obj; return Height.Equals(other.Height) && Radius.Equals(other.Radius) && NumSides.Equals(other.NumSides); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Height, Radius, NumSides);
        public override string ToString() => $"{{ \"Height\" = {Height}, \"Radius\" = {Radius}, \"NumSides\" = {NumSides} }}";
        public static implicit operator Dynamic(NPyramid self) => new Dynamic(self);
        public static implicit operator NPyramid(Dynamic value) => value.As<NPyramid>();
        public String TypeName => "NPyramid";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Height", (String)"Radius", (String)"NumSides");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Height), new Dynamic(Radius), new Dynamic(NumSides));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Ellipsoid: ISolid
    {
        [DataMember] public readonly Vector3D Radii;
        public Ellipsoid WithRadii(Vector3D radii) => new Ellipsoid(radii);
        public Ellipsoid(Vector3D radii) => (Radii) = (radii);
        public static Ellipsoid Default = new Ellipsoid();
        public static Ellipsoid New(Vector3D radii) => new Ellipsoid(radii);
        public Plato.SinglePrecision.Ellipsoid ChangePrecision() => (Radii.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Ellipsoid(Ellipsoid self) => self.ChangePrecision();
        public static implicit operator Vector3D(Ellipsoid self) => self.Radii;
        public static implicit operator Ellipsoid(Vector3D value) => new Ellipsoid(value);
        public override bool Equals(object obj) { if (!(obj is Ellipsoid)) return false; var other = (Ellipsoid)obj; return Radii.Equals(other.Radii); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radii);
        public override string ToString() => $"{{ \"Radii\" = {Radii} }}";
        public static implicit operator Dynamic(Ellipsoid self) => new Dynamic(self);
        public static implicit operator Ellipsoid(Dynamic value) => value.As<Ellipsoid>();
        public String TypeName => "Ellipsoid";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radii");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radii));
        // Implemented concept functions and type functions
        public Boolean ClosedX => ((Boolean)true);
        public Boolean ClosedY => ((Boolean)true);
        // Unimplemented concept functions
        public Number Distance(Vector3D p) => Intrinsics.Distance(this, p);
        public Vector3D Eval(Vector2D t) => Intrinsics.Eval(this, t);
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Vector2D: IVector<Vector2D>
    {
        [DataMember] public readonly Number X;
        [DataMember] public readonly Number Y;
        public Vector2D WithX(Number x) => new Vector2D(x, Y);
        public Vector2D WithY(Number y) => new Vector2D(X, y);
        public Vector2D(Number x, Number y) => (X, Y) = (x, y);
        public static Vector2D Default = new Vector2D();
        public static Vector2D New(Number x, Number y) => new Vector2D(x, y);
        public Plato.SinglePrecision.Vector2D ChangePrecision() => (X.ChangePrecision(), Y.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Vector2D(Vector2D self) => self.ChangePrecision();
        public static implicit operator (Number, Number)(Vector2D self) => (self.X, self.Y);
        public static implicit operator Vector2D((Number, Number) value) => new Vector2D(value.Item1, value.Item2);
        public void Deconstruct(out Number x, out Number y) { x = X; y = Y; }
        public override bool Equals(object obj) { if (!(obj is Vector2D)) return false; var other = (Vector2D)obj; return X.Equals(other.X) && Y.Equals(other.Y); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X, Y);
        public override string ToString() => $"{{ \"X\" = {X}, \"Y\" = {Y} }}";
        public static implicit operator Dynamic(Vector2D self) => new Dynamic(self);
        public static implicit operator Vector2D(Dynamic value) => value.As<Vector2D>();
        public String TypeName => "Vector2D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X", (String)"Y");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X), new Dynamic(Y));
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Vector2D)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Vector2D)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Vector2D)b);
        IAdditive IAdditive.Negative => this.Negative;
        IMultiplicative IMultiplicative.Multiply(IMultiplicative b) => this.Multiply((Vector2D)b);
        IDivisible IDivisible.Divide(IDivisible b) => this.Divide((Vector2D)b);
        IModulo IModulo.Modulo(IModulo b) => this.Modulo((Vector2D)b);
        // Array predefined functions
        public Vector2D(IArray<Number> xs) : this(xs[0], xs[1]) { }
        public Vector2D(Number[] xs) : this(xs[0], xs[1]) { }
        public static Vector2D New(IArray<Number> xs) => new Vector2D(xs);
        public static Vector2D New(Number[] xs) => new Vector2D(xs);
        public static implicit operator Number[](Vector2D self) => self.ToSystemArray();
        public static implicit operator Array<Number>(Vector2D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Number> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Number System.Collections.Generic.IReadOnlyList<Number>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Number>.Count => this.Count;
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(X, Y);
        public Vector2D FromComponents(IArray<Number> numbers) => new Vector2D(numbers[0], numbers[1]);
        // Implemented concept functions and type functions
        public Vector3D Vector3D => this.X.Tuple3(this.Y, ((Integer)0));
        public static implicit operator Vector3D(Vector2D v) => v.Vector3D;
        public Vector2D MidPoint(Vector2D b) => this.Add(b).Divide(((Number)2));
        public Line2D Line(Vector2D b) => this.Tuple2(b);
        public Ray2D Ray(Vector2D b) => this.Tuple2(b);
        public Ray2D RayTo(Vector2D b) => this.Tuple2(b.Subtract(this));
        public Number Cross(Vector2D b) => this.X.Multiply(b.Y).Subtract(this.Y.Multiply(b.X));
        public Integer Count => ((Integer)2);
        public Number At(Integer n) => n.Equals(((Integer)0)) ? this.X : this.Y;
        public Number this[Integer n] => At(n);
        public Vector3D To3D => this;
        public static Vector2D UnitX => ((Integer)1).Tuple2(((Integer)0));
        public static Vector2D UnitY => ((Integer)0).Tuple2(((Integer)1));
        public Vector2D Multiply(Vector2D y) => this.ZipComponents(y, (a, b) => a.Multiply(b));
        public static Vector2D operator *(Vector2D x, Vector2D y) => x.Multiply(y);
        public Vector2D Divide(Vector2D y) => this.ZipComponents(y, (a, b) => a.Divide(b));
        public static Vector2D operator /(Vector2D x, Vector2D y) => x.Divide(y);
        public Vector2D Modulo(Vector2D y) => this.ZipComponents(y, (a, b) => a.Modulo(b));
        public static Vector2D operator %(Vector2D x, Vector2D y) => x.Modulo(y);
        public Number Length => this.Magnitude;
        public Number LengthSquared => this.MagnitudeSquared;
        public Number Sum => this.Reduce(((Number)0), (a, b) => a.Add(b));
        public Number SumSquares => this.Square.Sum;
        public Number MagnitudeSquared => this.SumSquares;
        public Number Magnitude => this.MagnitudeSquared.SquareRoot;
        public Number Dot(Vector2D v2) => this.Multiply(v2).Sum;
        public Number Average => this.Sum.Divide(this.Count);
        public Vector2D Normalize => this.MagnitudeSquared.GreaterThan(((Integer)0)) ? this.Divide(this.Magnitude) : this.Zero;
        public Vector2D Reflect(Vector2D normal) => this.Subtract(normal.Multiply(this.Dot(normal).Multiply(((Number)2))));
        public Vector2D Project(Vector2D other) => other.Multiply(this.Dot(other));
        public Number Distance(Vector2D b) => b.Subtract(this).Magnitude;
        public Number DistanceSquared(Vector2D b) => b.Subtract(this).Magnitude;
        public Angle Angle(Vector2D b) => this.Dot(b).Divide(this.Magnitude.Multiply(b.Magnitude)).Acos;
        public Vector2D PlusOne => this.Add(this.One);
        public Vector2D MinusOne => this.Subtract(this.One);
        public Vector2D FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Vector2D MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Vector2D ZipComponents(Vector2D y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Vector2D Zero => this.MapComponents((i) => ((Number)0));
        public Vector2D One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Vector2D MinValue => this.MapComponents((x) => x.MinValue);
        public Vector2D MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Vector2D a, Vector2D b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Vector2D Clamp(Vector2D a, Vector2D b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Vector2D ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Vector2D Clamp01 => this.ClampZeroOne;
        public Vector2D Abs => this.MapComponents((i) => i.Abs);
        public Vector2D Min(Vector2D y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Vector2D Max(Vector2D y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Vector2D Floor => this.MapComponents((c) => c.Floor);
        public Vector2D Fract => this.MapComponents((c) => c.Fract);
        public Vector2D Multiply(Number s){
            var _var669 = s;
            return this.MapComponents((i) => i.Multiply(_var669));
        }
        public static Vector2D operator *(Vector2D x, Number s) => x.Multiply(s);
        public Vector2D Divide(Number s){
            var _var670 = s;
            return this.MapComponents((i) => i.Divide(_var670));
        }
        public static Vector2D operator /(Vector2D x, Number s) => x.Divide(s);
        public Vector2D Modulo(Number s){
            var _var671 = s;
            return this.MapComponents((i) => i.Modulo(_var671));
        }
        public static Vector2D operator %(Vector2D x, Number s) => x.Modulo(s);
        public Vector2D Add(Vector2D y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Vector2D operator +(Vector2D x, Vector2D y) => x.Add(y);
        public Vector2D Subtract(Vector2D y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Vector2D operator -(Vector2D x, Vector2D y) => x.Subtract(y);
        public Vector2D Negative => this.MapComponents((a) => a.Negative);
        public static Vector2D operator -(Vector2D x) => x.Negative;
        public IArray<Vector2D> Repeat(Integer n){
            var _var672 = this;
            return n.MapRange((i) => _var672);
        }
        public Boolean Equals(Vector2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Vector2D a, Vector2D b) => a.Equals(b);
        public Boolean NotEquals(Vector2D b) => this.Equals(b).Not;
        public static Boolean operator !=(Vector2D a, Vector2D b) => a.NotEquals(b);
        public Vector2D Half => this.Divide(((Number)2));
        public Vector2D Quarter => this.Divide(((Number)4));
        public Vector2D Eight => this.Divide(((Number)8));
        public Vector2D Sixteenth => this.Divide(((Number)16));
        public Vector2D Tenth => this.Divide(((Number)10));
        public Vector2D Twice => this.Multiply(((Number)2));
        public Vector2D Hundred => this.Multiply(((Number)100));
        public Vector2D Thousand => this.Multiply(((Number)1000));
        public Vector2D Million => this.Thousand.Thousand;
        public Vector2D Billion => this.Thousand.Million;
        public Vector2D Pow2 => this.Multiply(this);
        public Vector2D Pow3 => this.Pow2.Multiply(this);
        public Vector2D Pow4 => this.Pow3.Multiply(this);
        public Vector2D Pow5 => this.Pow4.Multiply(this);
        public Vector2D Square => this.Pow2;
        public Vector2D Sqr => this.Pow2;
        public Vector2D Cube => this.Pow3;
        public Vector2D Parabola => this.Sqr;
        public Vector2D Lerp(Vector2D b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Vector2D Barycentric(Vector2D v2, Vector2D v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        public Vector2D CubicBezier(Vector2D b, Vector2D c, Vector2D d, Number t) => this.Multiply(((Number)1).Subtract(t).Cube).Add(b.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr.Multiply(t))).Add(c.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Multiply(t.Sqr))).Add(d.Multiply(t.Cube))));
        public Vector2D CubicBezierDerivative(Vector2D b, Vector2D c, Vector2D d, Number t) => b.Subtract(this).Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr)).Add(c.Subtract(b).Multiply(((Number)6).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(d.Subtract(c).Multiply(((Number)3).Multiply(t.Sqr))));
        public Vector2D CubicBezierSecondDerivative(Vector2D b, Vector2D c, Vector2D d, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(((Number)1).Subtract(t))).Add(d.Subtract(c.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(t)));
        public Vector2D QuadraticBezier(Vector2D b, Vector2D c, Number t) => this.Multiply(((Number)1).Subtract(t).Sqr).Add(b.Multiply(((Number)2).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(c.Multiply(t.Sqr)));
        public Vector2D QuadraticBezierDerivative(Vector2D b, Vector2D c, Number t) => b.Subtract(b).Multiply(((Number)2).Multiply(((Number)1).Subtract(t))).Add(c.Subtract(b).Multiply(((Number)2).Multiply(t)));
        public Vector2D QuadraticBezierSecondDerivative(Vector2D b, Vector2D c, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this));
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Vector3D: IVector<Vector3D>, IDeformable3D<Vector3D>
    {
        [DataMember] public readonly Number X;
        [DataMember] public readonly Number Y;
        [DataMember] public readonly Number Z;
        public Vector3D WithX(Number x) => new Vector3D(x, Y, Z);
        public Vector3D WithY(Number y) => new Vector3D(X, y, Z);
        public Vector3D WithZ(Number z) => new Vector3D(X, Y, z);
        public Vector3D(Number x, Number y, Number z) => (X, Y, Z) = (x, y, z);
        public static Vector3D Default = new Vector3D();
        public static Vector3D New(Number x, Number y, Number z) => new Vector3D(x, y, z);
        public Plato.SinglePrecision.Vector3D ChangePrecision() => (X.ChangePrecision(), Y.ChangePrecision(), Z.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Vector3D(Vector3D self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number)(Vector3D self) => (self.X, self.Y, self.Z);
        public static implicit operator Vector3D((Number, Number, Number) value) => new Vector3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number x, out Number y, out Number z) { x = X; y = Y; z = Z; }
        public override bool Equals(object obj) { if (!(obj is Vector3D)) return false; var other = (Vector3D)obj; return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X, Y, Z);
        public override string ToString() => $"{{ \"X\" = {X}, \"Y\" = {Y}, \"Z\" = {Z} }}";
        public static implicit operator Dynamic(Vector3D self) => new Dynamic(self);
        public static implicit operator Vector3D(Dynamic value) => value.As<Vector3D>();
        public String TypeName => "Vector3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X", (String)"Y", (String)"Z");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X), new Dynamic(Y), new Dynamic(Z));
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Vector3D)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Vector3D)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Vector3D)b);
        IAdditive IAdditive.Negative => this.Negative;
        IMultiplicative IMultiplicative.Multiply(IMultiplicative b) => this.Multiply((Vector3D)b);
        IDivisible IDivisible.Divide(IDivisible b) => this.Divide((Vector3D)b);
        IModulo IModulo.Modulo(IModulo b) => this.Modulo((Vector3D)b);
        IDeformable3D IDeformable3D.Deform(System.Func<Vector3D, Vector3D> f) => this.Deform((System.Func<Vector3D, Vector3D>)f);
        // Array predefined functions
        public Vector3D(IArray<Number> xs) : this(xs[0], xs[1], xs[2]) { }
        public Vector3D(Number[] xs) : this(xs[0], xs[1], xs[2]) { }
        public static Vector3D New(IArray<Number> xs) => new Vector3D(xs);
        public static Vector3D New(Number[] xs) => new Vector3D(xs);
        public static implicit operator Number[](Vector3D self) => self.ToSystemArray();
        public static implicit operator Array<Number>(Vector3D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Number> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Number System.Collections.Generic.IReadOnlyList<Number>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Number>.Count => this.Count;
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(X, Y, Z);
        public Vector3D FromComponents(IArray<Number> numbers) => new Vector3D(numbers[0], numbers[1], numbers[2]);
        // Implemented concept functions and type functions
        public Vector4D Vector4D => this.ToVector4D(((Integer)0));
        public static implicit operator Vector4D(Vector3D v) => v.Vector4D;
        public Vector4D ToVector4D(Number w) => this.X.Tuple4(this.Y, this.Z, w);
        public Vector3D XZY => this.X.Tuple3(this.Z, this.Y);
        public Vector3D YXZ => this.Y.Tuple3(this.X, this.Z);
        public Vector3D YZX => this.Y.Tuple3(this.Z, this.X);
        public Vector3D ZXY => this.Z.Tuple3(this.X, this.Y);
        public Vector3D ZYX => this.Z.Tuple3(this.Y, this.X);
        public Vector2D XY => this.X.Tuple2(this.Y);
        public Vector2D YX => this.Y.Tuple2(this.X);
        public Vector2D XZ => this.X.Tuple2(this.Z);
        public Vector2D ZX => this.Z.Tuple2(this.X);
        public Vector2D YZ => this.Y.Tuple2(this.Z);
        public Vector2D ZY => this.Z.Tuple2(this.Y);
        public Vector3D MidPoint(Vector3D b) => this.Add(b).Divide(((Number)2));
        public Line3D Line(Vector3D b) => this.Tuple2(b);
        public Ray3D Ray(Vector3D b) => this.Tuple2(b);
        public Ray3D RayTo(Vector3D b) => this.Tuple2(b.Subtract(this));
        public Vector3D Project(Plane p) => this.Subtract(p.Normal.Multiply(p.Normal.Dot(this)));
        public Vector3D Deform(System.Func<Vector3D, Vector3D> f) => f.Invoke(this);
        public Vector2D To2D => this.X.Tuple2(this.Y);
        public Vector3D Cross(Vector3D b) => this.Y.Multiply(b.Z).Subtract(this.Z.Multiply(b.Y)).Tuple3(this.Z.Multiply(b.X).Subtract(this.X.Multiply(b.Z)), this.X.Multiply(b.Y).Subtract(this.Y.Multiply(b.X)));
        public Number MixedProduct(Vector3D b, Vector3D c) => this.Cross(b).Dot(c);
        public Integer Count => ((Integer)3);
        public Number At(Integer n) => n.Equals(((Integer)0)) ? this.X : n.Equals(((Integer)1)) ? this.Y : this.Z;
        public Number this[Integer n] => At(n);
        public Boolean IsParallel(Vector3D b) => this.Dot(b).Abs.GreaterThan(((Number)1).Subtract(((Number)1E-06)));
        public static Vector3D UnitX => ((Integer)1).Tuple3(((Integer)0), ((Integer)0));
        public static Vector3D UnitY => ((Integer)0).Tuple3(((Integer)1), ((Integer)0));
        public static Vector3D UnitZ => ((Integer)0).Tuple3(((Integer)0), ((Integer)1));
        public static Vector3D MinValue => Constants.MinNumber.Tuple3(Constants.MinNumber, Constants.MinNumber);
        public static Vector3D MaxValue => Constants.MaxNumber.Tuple3(Constants.MaxNumber, Constants.MaxNumber);
        public Matrix4x4 Matrix => Matrix4x4.CreateTranslation(this);
        public AxisAngle AxisAngle(Angle a) => this.Tuple2(a);
        public Quaternion Rotation(Angle theta) => this.AxisAngle(theta);
        public Quaternion LookRotation(Vector3D up){
            var forward = this.Normalize;
            var up2 = up.IsParallel(forward) ? Vector3D.UnitX : up;
            var right = up2.Cross(forward).Normalize;
            var correctedUp = forward.Cross(right).Normalize;
            var rotationMatrix = Matrix4x4.New(right.X.Tuple4(correctedUp.X, forward.X, ((Integer)0)), right.Y.Tuple4(correctedUp.Y, forward.Y, ((Integer)0)), right.Z.Tuple4(correctedUp.Z, forward.Z, ((Integer)0)), ((Integer)0).Tuple4(((Integer)0), ((Integer)0), ((Integer)1)));
            return rotationMatrix.QuaternionFromRotationMatrix.Normalize;
        }
        public Vector3D Multiply(Vector3D y) => this.ZipComponents(y, (a, b) => a.Multiply(b));
        public static Vector3D operator *(Vector3D x, Vector3D y) => x.Multiply(y);
        public Vector3D Divide(Vector3D y) => this.ZipComponents(y, (a, b) => a.Divide(b));
        public static Vector3D operator /(Vector3D x, Vector3D y) => x.Divide(y);
        public Vector3D Modulo(Vector3D y) => this.ZipComponents(y, (a, b) => a.Modulo(b));
        public static Vector3D operator %(Vector3D x, Vector3D y) => x.Modulo(y);
        public Number Length => this.Magnitude;
        public Number LengthSquared => this.MagnitudeSquared;
        public Number Sum => this.Reduce(((Number)0), (a, b) => a.Add(b));
        public Number SumSquares => this.Square.Sum;
        public Number MagnitudeSquared => this.SumSquares;
        public Number Magnitude => this.MagnitudeSquared.SquareRoot;
        public Number Dot(Vector3D v2) => this.Multiply(v2).Sum;
        public Number Average => this.Sum.Divide(this.Count);
        public Vector3D Normalize => this.MagnitudeSquared.GreaterThan(((Integer)0)) ? this.Divide(this.Magnitude) : this.Zero;
        public Vector3D Reflect(Vector3D normal) => this.Subtract(normal.Multiply(this.Dot(normal).Multiply(((Number)2))));
        public Vector3D Project(Vector3D other) => other.Multiply(this.Dot(other));
        public Number Distance(Vector3D b) => b.Subtract(this).Magnitude;
        public Number DistanceSquared(Vector3D b) => b.Subtract(this).Magnitude;
        public Angle Angle(Vector3D b) => this.Dot(b).Divide(this.Magnitude.Multiply(b.Magnitude)).Acos;
        public Vector3D PlusOne => this.Add(this.One);
        public Vector3D MinusOne => this.Subtract(this.One);
        public Vector3D FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Vector3D MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Vector3D ZipComponents(Vector3D y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Vector3D Zero => this.MapComponents((i) => ((Number)0));
        public Vector3D One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Vector3D a, Vector3D b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Vector3D Clamp(Vector3D a, Vector3D b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Vector3D ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Vector3D Clamp01 => this.ClampZeroOne;
        public Vector3D Abs => this.MapComponents((i) => i.Abs);
        public Vector3D Min(Vector3D y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Vector3D Max(Vector3D y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Vector3D Floor => this.MapComponents((c) => c.Floor);
        public Vector3D Fract => this.MapComponents((c) => c.Fract);
        public Vector3D Multiply(Number s){
            var _var673 = s;
            return this.MapComponents((i) => i.Multiply(_var673));
        }
        public static Vector3D operator *(Vector3D x, Number s) => x.Multiply(s);
        public Vector3D Divide(Number s){
            var _var674 = s;
            return this.MapComponents((i) => i.Divide(_var674));
        }
        public static Vector3D operator /(Vector3D x, Number s) => x.Divide(s);
        public Vector3D Modulo(Number s){
            var _var675 = s;
            return this.MapComponents((i) => i.Modulo(_var675));
        }
        public static Vector3D operator %(Vector3D x, Number s) => x.Modulo(s);
        public Vector3D Add(Vector3D y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Vector3D operator +(Vector3D x, Vector3D y) => x.Add(y);
        public Vector3D Subtract(Vector3D y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Vector3D operator -(Vector3D x, Vector3D y) => x.Subtract(y);
        public Vector3D Negative => this.MapComponents((a) => a.Negative);
        public static Vector3D operator -(Vector3D x) => x.Negative;
        public IArray<Vector3D> Repeat(Integer n){
            var _var676 = this;
            return n.MapRange((i) => _var676);
        }
        public Boolean Equals(Vector3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Vector3D a, Vector3D b) => a.Equals(b);
        public Boolean NotEquals(Vector3D b) => this.Equals(b).Not;
        public static Boolean operator !=(Vector3D a, Vector3D b) => a.NotEquals(b);
        public Vector3D Half => this.Divide(((Number)2));
        public Vector3D Quarter => this.Divide(((Number)4));
        public Vector3D Eight => this.Divide(((Number)8));
        public Vector3D Sixteenth => this.Divide(((Number)16));
        public Vector3D Tenth => this.Divide(((Number)10));
        public Vector3D Twice => this.Multiply(((Number)2));
        public Vector3D Hundred => this.Multiply(((Number)100));
        public Vector3D Thousand => this.Multiply(((Number)1000));
        public Vector3D Million => this.Thousand.Thousand;
        public Vector3D Billion => this.Thousand.Million;
        public Vector3D Pow2 => this.Multiply(this);
        public Vector3D Pow3 => this.Pow2.Multiply(this);
        public Vector3D Pow4 => this.Pow3.Multiply(this);
        public Vector3D Pow5 => this.Pow4.Multiply(this);
        public Vector3D Square => this.Pow2;
        public Vector3D Sqr => this.Pow2;
        public Vector3D Cube => this.Pow3;
        public Vector3D Parabola => this.Sqr;
        public Vector3D Lerp(Vector3D b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Vector3D Barycentric(Vector3D v2, Vector3D v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        public Vector3D CubicBezier(Vector3D b, Vector3D c, Vector3D d, Number t) => this.Multiply(((Number)1).Subtract(t).Cube).Add(b.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr.Multiply(t))).Add(c.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Multiply(t.Sqr))).Add(d.Multiply(t.Cube))));
        public Vector3D CubicBezierDerivative(Vector3D b, Vector3D c, Vector3D d, Number t) => b.Subtract(this).Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr)).Add(c.Subtract(b).Multiply(((Number)6).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(d.Subtract(c).Multiply(((Number)3).Multiply(t.Sqr))));
        public Vector3D CubicBezierSecondDerivative(Vector3D b, Vector3D c, Vector3D d, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(((Number)1).Subtract(t))).Add(d.Subtract(c.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(t)));
        public Vector3D QuadraticBezier(Vector3D b, Vector3D c, Number t) => this.Multiply(((Number)1).Subtract(t).Sqr).Add(b.Multiply(((Number)2).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(c.Multiply(t.Sqr)));
        public Vector3D QuadraticBezierDerivative(Vector3D b, Vector3D c, Number t) => b.Subtract(b).Multiply(((Number)2).Multiply(((Number)1).Subtract(t))).Add(c.Subtract(b).Multiply(((Number)2).Multiply(t)));
        public Vector3D QuadraticBezierSecondDerivative(Vector3D b, Vector3D c, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this));
        public Vector3D Deform(ITransform3D t){
            var _var677 = t;
            return this.Deform((v) => _var677.Transform(v));
        }
        public Vector3D Translate(Vector3D v){
            var _var678 = v;
            return this.Deform((p) => p.Add(_var678));
        }
        public Vector3D Rotate(Quaternion q) => this.Deform(q);
        public Vector3D Scale(Vector3D v){
            var _var679 = v;
            return this.Deform((p) => p.Multiply(_var679));
        }
        public Vector3D Scale(Number s){
            var _var680 = s;
            return this.Deform((p) => p.Multiply(_var680));
        }
        public Vector3D RotateX(Angle a) => this.Rotate(a.XRotation);
        public Vector3D RotateY(Angle a) => this.Rotate(a.YRotation);
        public Vector3D RotateZ(Angle a) => this.Rotate(a.ZRotation);
        public Vector3D TranslateX(Number s){
            var _var681 = s;
            return this.Deform((p) => p.Add(_var681.Tuple3(((Integer)0), ((Integer)0))));
        }
        public Vector3D TranslateY(Number s){
            var _var682 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(_var682, ((Integer)0))));
        }
        public Vector3D TranslateZ(Number s){
            var _var683 = s;
            return this.Deform((p) => p.Add(((Integer)0).Tuple3(((Integer)0), _var683)));
        }
        public Vector3D ScaleX(Number s){
            var _var684 = s;
            return this.Deform((p) => p.Multiply(_var684.Tuple3(((Integer)1), ((Integer)1))));
        }
        public Vector3D ScaleY(Number s){
            var _var685 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(_var685, ((Integer)1))));
        }
        public Vector3D ScaleZ(Number s){
            var _var686 = s;
            return this.Deform((p) => p.Multiply(((Integer)1).Tuple3(((Integer)1), _var686)));
        }
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Vector4D: IVector<Vector4D>
    {
        [DataMember] public readonly Number X;
        [DataMember] public readonly Number Y;
        [DataMember] public readonly Number Z;
        [DataMember] public readonly Number W;
        public Vector4D WithX(Number x) => new Vector4D(x, Y, Z, W);
        public Vector4D WithY(Number y) => new Vector4D(X, y, Z, W);
        public Vector4D WithZ(Number z) => new Vector4D(X, Y, z, W);
        public Vector4D WithW(Number w) => new Vector4D(X, Y, Z, w);
        public Vector4D(Number x, Number y, Number z, Number w) => (X, Y, Z, W) = (x, y, z, w);
        public static Vector4D Default = new Vector4D();
        public static Vector4D New(Number x, Number y, Number z, Number w) => new Vector4D(x, y, z, w);
        public Plato.SinglePrecision.Vector4D ChangePrecision() => (X.ChangePrecision(), Y.ChangePrecision(), Z.ChangePrecision(), W.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Vector4D(Vector4D self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number, Number)(Vector4D self) => (self.X, self.Y, self.Z, self.W);
        public static implicit operator Vector4D((Number, Number, Number, Number) value) => new Vector4D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Number x, out Number y, out Number z, out Number w) { x = X; y = Y; z = Z; w = W; }
        public override bool Equals(object obj) { if (!(obj is Vector4D)) return false; var other = (Vector4D)obj; return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X, Y, Z, W);
        public override string ToString() => $"{{ \"X\" = {X}, \"Y\" = {Y}, \"Z\" = {Z}, \"W\" = {W} }}";
        public static implicit operator Dynamic(Vector4D self) => new Dynamic(self);
        public static implicit operator Vector4D(Dynamic value) => value.As<Vector4D>();
        public String TypeName => "Vector4D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X", (String)"Y", (String)"Z", (String)"W");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X), new Dynamic(Y), new Dynamic(Z), new Dynamic(W));
        IArray<Number> INumerical.Components => this.Components;
        INumerical INumerical.FromComponents(IArray<Number> xs) => this.FromComponents((IArray<Number>)xs);
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Vector4D)b);
        IScalarArithmetic IScalarArithmetic.Modulo(Number other) => this.Modulo((Number)other);
        IScalarArithmetic IScalarArithmetic.Divide(Number other) => this.Divide((Number)other);
        IScalarArithmetic IScalarArithmetic.Multiply(Number other) => this.Multiply((Number)other);
        IAdditive IAdditive.Add(IAdditive b) => this.Add((Vector4D)b);
        IAdditive IAdditive.Subtract(IAdditive b) => this.Subtract((Vector4D)b);
        IAdditive IAdditive.Negative => this.Negative;
        IMultiplicative IMultiplicative.Multiply(IMultiplicative b) => this.Multiply((Vector4D)b);
        IDivisible IDivisible.Divide(IDivisible b) => this.Divide((Vector4D)b);
        IModulo IModulo.Modulo(IModulo b) => this.Modulo((Vector4D)b);
        // Array predefined functions
        public Vector4D(IArray<Number> xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public Vector4D(Number[] xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public static Vector4D New(IArray<Number> xs) => new Vector4D(xs);
        public static Vector4D New(Number[] xs) => new Vector4D(xs);
        public static implicit operator Number[](Vector4D self) => self.ToSystemArray();
        public static implicit operator Array<Number>(Vector4D self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Number> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Number System.Collections.Generic.IReadOnlyList<Number>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Number>.Count => this.Count;
        // Numerical predefined functions
        public IArray<Number> Components => Intrinsics.MakeArray<Number>(X, Y, Z, W);
        public Vector4D FromComponents(IArray<Number> numbers) => new Vector4D(numbers[0], numbers[1], numbers[2], numbers[3]);
        // Implemented concept functions and type functions
        public Integer Count => ((Integer)4);
        public Number At(Integer n) => n.Equals(((Integer)0)) ? this.X : n.Equals(((Integer)1)) ? this.Y : n.Equals(((Integer)2)) ? this.Z : this.W;
        public Number this[Integer n] => At(n);
        public Vector3D ToVector3D => this.X.Tuple3(this.Y, this.Z);
        public static Vector4D UnitX => ((Integer)1).Tuple4(((Integer)0), ((Integer)0), ((Integer)0));
        public static Vector4D UnitY => ((Integer)0).Tuple4(((Integer)1), ((Integer)0), ((Integer)0));
        public static Vector4D UnitZ => ((Integer)0).Tuple4(((Integer)0), ((Integer)1), ((Integer)0));
        public static Vector4D UnitW => ((Integer)0).Tuple4(((Integer)0), ((Integer)0), ((Integer)1));
        public Quaternion Quaternion => this.X.Tuple4(this.Y, this.Z, this.W);
        public static implicit operator Quaternion(Vector4D v) => v.Quaternion;
        public Vector4D Multiply(Vector4D y) => this.ZipComponents(y, (a, b) => a.Multiply(b));
        public static Vector4D operator *(Vector4D x, Vector4D y) => x.Multiply(y);
        public Vector4D Divide(Vector4D y) => this.ZipComponents(y, (a, b) => a.Divide(b));
        public static Vector4D operator /(Vector4D x, Vector4D y) => x.Divide(y);
        public Vector4D Modulo(Vector4D y) => this.ZipComponents(y, (a, b) => a.Modulo(b));
        public static Vector4D operator %(Vector4D x, Vector4D y) => x.Modulo(y);
        public Number Length => this.Magnitude;
        public Number LengthSquared => this.MagnitudeSquared;
        public Number Sum => this.Reduce(((Number)0), (a, b) => a.Add(b));
        public Number SumSquares => this.Square.Sum;
        public Number MagnitudeSquared => this.SumSquares;
        public Number Magnitude => this.MagnitudeSquared.SquareRoot;
        public Number Dot(Vector4D v2) => this.Multiply(v2).Sum;
        public Number Average => this.Sum.Divide(this.Count);
        public Vector4D Normalize => this.MagnitudeSquared.GreaterThan(((Integer)0)) ? this.Divide(this.Magnitude) : this.Zero;
        public Vector4D Reflect(Vector4D normal) => this.Subtract(normal.Multiply(this.Dot(normal).Multiply(((Number)2))));
        public Vector4D Project(Vector4D other) => other.Multiply(this.Dot(other));
        public Number Distance(Vector4D b) => b.Subtract(this).Magnitude;
        public Number DistanceSquared(Vector4D b) => b.Subtract(this).Magnitude;
        public Angle Angle(Vector4D b) => this.Dot(b).Divide(this.Magnitude.Multiply(b.Magnitude)).Acos;
        public Vector4D PlusOne => this.Add(this.One);
        public Vector4D MinusOne => this.Subtract(this.One);
        public Vector4D FromOne => this.One.Subtract(this);
        public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents => this.Components.Count;
        public Vector4D MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        public Vector4D ZipComponents(Vector4D y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Vector4D Zero => this.MapComponents((i) => ((Number)0));
        public Vector4D One => this.MapComponents((i) => ((Number)1));
        public Number MaxComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Max(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Number MinComponent { get {
            var n = this.NumComponents;
            if (n.Equals(((Integer)0)))
            return ((Integer)0);
            var r = this.Component(((Integer)0));
            {
                var i = ((Integer)1);
                while (i.LessThan(n))
                {
                    r = r.Min(this.Component(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
         } public Vector4D MinValue => this.MapComponents((x) => x.MinValue);
        public Vector4D MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Vector4D a, Vector4D b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Vector4D Clamp(Vector4D a, Vector4D b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Vector4D ClampZeroOne => this.Clamp(this.Zero, this.One);
        public Vector4D Clamp01 => this.ClampZeroOne;
        public Vector4D Abs => this.MapComponents((i) => i.Abs);
        public Vector4D Min(Vector4D y) => this.ZipComponents(y, (a, b) => a.Min(b));
        public Vector4D Max(Vector4D y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Vector4D Floor => this.MapComponents((c) => c.Floor);
        public Vector4D Fract => this.MapComponents((c) => c.Fract);
        public Vector4D Multiply(Number s){
            var _var687 = s;
            return this.MapComponents((i) => i.Multiply(_var687));
        }
        public static Vector4D operator *(Vector4D x, Number s) => x.Multiply(s);
        public Vector4D Divide(Number s){
            var _var688 = s;
            return this.MapComponents((i) => i.Divide(_var688));
        }
        public static Vector4D operator /(Vector4D x, Number s) => x.Divide(s);
        public Vector4D Modulo(Number s){
            var _var689 = s;
            return this.MapComponents((i) => i.Modulo(_var689));
        }
        public static Vector4D operator %(Vector4D x, Number s) => x.Modulo(s);
        public Vector4D Add(Vector4D y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Vector4D operator +(Vector4D x, Vector4D y) => x.Add(y);
        public Vector4D Subtract(Vector4D y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Vector4D operator -(Vector4D x, Vector4D y) => x.Subtract(y);
        public Vector4D Negative => this.MapComponents((a) => a.Negative);
        public static Vector4D operator -(Vector4D x) => x.Negative;
        public IArray<Vector4D> Repeat(Integer n){
            var _var690 = this;
            return n.MapRange((i) => _var690);
        }
        public Boolean Equals(Vector4D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Vector4D a, Vector4D b) => a.Equals(b);
        public Boolean NotEquals(Vector4D b) => this.Equals(b).Not;
        public static Boolean operator !=(Vector4D a, Vector4D b) => a.NotEquals(b);
        public Vector4D Half => this.Divide(((Number)2));
        public Vector4D Quarter => this.Divide(((Number)4));
        public Vector4D Eight => this.Divide(((Number)8));
        public Vector4D Sixteenth => this.Divide(((Number)16));
        public Vector4D Tenth => this.Divide(((Number)10));
        public Vector4D Twice => this.Multiply(((Number)2));
        public Vector4D Hundred => this.Multiply(((Number)100));
        public Vector4D Thousand => this.Multiply(((Number)1000));
        public Vector4D Million => this.Thousand.Thousand;
        public Vector4D Billion => this.Thousand.Million;
        public Vector4D Pow2 => this.Multiply(this);
        public Vector4D Pow3 => this.Pow2.Multiply(this);
        public Vector4D Pow4 => this.Pow3.Multiply(this);
        public Vector4D Pow5 => this.Pow4.Multiply(this);
        public Vector4D Square => this.Pow2;
        public Vector4D Sqr => this.Pow2;
        public Vector4D Cube => this.Pow3;
        public Vector4D Parabola => this.Sqr;
        public Vector4D Lerp(Vector4D b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Vector4D Barycentric(Vector4D v2, Vector4D v3, Vector2D uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        public Vector4D CubicBezier(Vector4D b, Vector4D c, Vector4D d, Number t) => this.Multiply(((Number)1).Subtract(t).Cube).Add(b.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr.Multiply(t))).Add(c.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Multiply(t.Sqr))).Add(d.Multiply(t.Cube))));
        public Vector4D CubicBezierDerivative(Vector4D b, Vector4D c, Vector4D d, Number t) => b.Subtract(this).Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr)).Add(c.Subtract(b).Multiply(((Number)6).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(d.Subtract(c).Multiply(((Number)3).Multiply(t.Sqr))));
        public Vector4D CubicBezierSecondDerivative(Vector4D b, Vector4D c, Vector4D d, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(((Number)1).Subtract(t))).Add(d.Subtract(c.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(t)));
        public Vector4D QuadraticBezier(Vector4D b, Vector4D c, Number t) => this.Multiply(((Number)1).Subtract(t).Sqr).Add(b.Multiply(((Number)2).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(c.Multiply(t.Sqr)));
        public Vector4D QuadraticBezierDerivative(Vector4D b, Vector4D c, Number t) => b.Subtract(b).Multiply(((Number)2).Multiply(((Number)1).Subtract(t))).Add(c.Subtract(b).Multiply(((Number)2).Multiply(t)));
        public Vector4D QuadraticBezierSecondDerivative(Vector4D b, Vector4D c, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this));
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Matrix3x3: IValue<Matrix3x3>, IArray<Vector3D>
    {
        [DataMember] public readonly Vector3D Column1;
        [DataMember] public readonly Vector3D Column2;
        [DataMember] public readonly Vector3D Column3;
        public Matrix3x3 WithColumn1(Vector3D column1) => new Matrix3x3(column1, Column2, Column3);
        public Matrix3x3 WithColumn2(Vector3D column2) => new Matrix3x3(Column1, column2, Column3);
        public Matrix3x3 WithColumn3(Vector3D column3) => new Matrix3x3(Column1, Column2, column3);
        public Matrix3x3(Vector3D column1, Vector3D column2, Vector3D column3) => (Column1, Column2, Column3) = (column1, column2, column3);
        public static Matrix3x3 Default = new Matrix3x3();
        public static Matrix3x3 New(Vector3D column1, Vector3D column2, Vector3D column3) => new Matrix3x3(column1, column2, column3);
        public Plato.SinglePrecision.Matrix3x3 ChangePrecision() => (Column1.ChangePrecision(), Column2.ChangePrecision(), Column3.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Matrix3x3(Matrix3x3 self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Vector3D, Vector3D)(Matrix3x3 self) => (self.Column1, self.Column2, self.Column3);
        public static implicit operator Matrix3x3((Vector3D, Vector3D, Vector3D) value) => new Matrix3x3(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D column1, out Vector3D column2, out Vector3D column3) { column1 = Column1; column2 = Column2; column3 = Column3; }
        public override bool Equals(object obj) { if (!(obj is Matrix3x3)) return false; var other = (Matrix3x3)obj; return Column1.Equals(other.Column1) && Column2.Equals(other.Column2) && Column3.Equals(other.Column3); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Column1, Column2, Column3);
        public override string ToString() => $"{{ \"Column1\" = {Column1}, \"Column2\" = {Column2}, \"Column3\" = {Column3} }}";
        public static implicit operator Dynamic(Matrix3x3 self) => new Dynamic(self);
        public static implicit operator Matrix3x3(Dynamic value) => value.As<Matrix3x3>();
        public String TypeName => "Matrix3x3";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Column1", (String)"Column2", (String)"Column3");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Column1), new Dynamic(Column2), new Dynamic(Column3));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Matrix3x3)b);
        // Array predefined functions
        public Matrix3x3(IArray<Vector3D> xs) : this(xs[0], xs[1], xs[2]) { }
        public Matrix3x3(Vector3D[] xs) : this(xs[0], xs[1], xs[2]) { }
        public static Matrix3x3 New(IArray<Vector3D> xs) => new Matrix3x3(xs);
        public static Matrix3x3 New(Vector3D[] xs) => new Matrix3x3(xs);
        public static implicit operator Vector3D[](Matrix3x3 self) => self.ToSystemArray();
        public static implicit operator Array<Vector3D>(Matrix3x3 self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector3D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector3D System.Collections.Generic.IReadOnlyList<Vector3D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector3D>.Count => this.Count;
        // Implemented concept functions and type functions
        public static Matrix3x3 Identity => ((Integer)1).Tuple3(((Integer)0), ((Integer)0)).Tuple3(((Integer)0).Tuple3(((Integer)1), ((Integer)0)), ((Integer)0).Tuple3(((Integer)0), ((Integer)1)));
        public Matrix4x4 Matrix => Matrix4x4.CreateFromRows(this.Row1, this.Row2, this.Row3, ((Integer)0).Tuple4(((Integer)0), ((Integer)0), ((Integer)1)));
        public Vector3D Row1 => this.Column1.X.Tuple3(this.Column2.X, this.Column3.X);
        public Vector3D Row2 => this.Column1.Y.Tuple3(this.Column2.Y, this.Column3.Y);
        public Vector3D Row3 => this.Column1.Z.Tuple3(this.Column2.Z, this.Column3.Z);
        public Number M11 => Column1.X;
        public Number M12 => Column2.X;
        public Number M13 => Column3.X;
        public Number M21 => Column1.Y;
        public Number M22 => Column2.Y;
        public Number M23 => Column3.Y;
        public Number M31 => Column1.Z;
        public Number M32 => Column2.Z;
        public Number M33 => Column3.Z;
        public Number Determinant => M11.Multiply(M22.Multiply(M33).Subtract(M23.Multiply(M32))).Subtract(M12.Multiply(M21.Multiply(M33).Subtract(M23.Multiply(M31))).Add(M13.Multiply(M21.Multiply(M32).Subtract(M22.Multiply(M31)))));
        public IArray<Matrix3x3> Repeat(Integer n){
            var _var691 = this;
            return n.MapRange((i) => _var691);
        }
        public Boolean Equals(Matrix3x3 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Matrix3x3 a, Matrix3x3 b) => a.Equals(b);
        public Boolean NotEquals(Matrix3x3 b) => this.Equals(b).Not;
        public static Boolean operator !=(Matrix3x3 a, Matrix3x3 b) => a.NotEquals(b);
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector3D At(Integer n) => n == 0 ? Column1 : n == 1 ? Column2 : n == 2 ? Column3 : throw new System.IndexOutOfRangeException();
        public Vector3D this[Integer n] => n == 0 ? Column1 : n == 1 ? Column2 : n == 2 ? Column3 : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Matrix4x4: IValue<Matrix4x4>, IArray<Vector4D>, ITransform3D
    {
        [DataMember] public readonly Vector4D Column1;
        [DataMember] public readonly Vector4D Column2;
        [DataMember] public readonly Vector4D Column3;
        [DataMember] public readonly Vector4D Column4;
        public Matrix4x4 WithColumn1(Vector4D column1) => new Matrix4x4(column1, Column2, Column3, Column4);
        public Matrix4x4 WithColumn2(Vector4D column2) => new Matrix4x4(Column1, column2, Column3, Column4);
        public Matrix4x4 WithColumn3(Vector4D column3) => new Matrix4x4(Column1, Column2, column3, Column4);
        public Matrix4x4 WithColumn4(Vector4D column4) => new Matrix4x4(Column1, Column2, Column3, column4);
        public Matrix4x4(Vector4D column1, Vector4D column2, Vector4D column3, Vector4D column4) => (Column1, Column2, Column3, Column4) = (column1, column2, column3, column4);
        public static Matrix4x4 Default = new Matrix4x4();
        public static Matrix4x4 New(Vector4D column1, Vector4D column2, Vector4D column3, Vector4D column4) => new Matrix4x4(column1, column2, column3, column4);
        public Plato.SinglePrecision.Matrix4x4 ChangePrecision() => (Column1.ChangePrecision(), Column2.ChangePrecision(), Column3.ChangePrecision(), Column4.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Matrix4x4(Matrix4x4 self) => self.ChangePrecision();
        public static implicit operator (Vector4D, Vector4D, Vector4D, Vector4D)(Matrix4x4 self) => (self.Column1, self.Column2, self.Column3, self.Column4);
        public static implicit operator Matrix4x4((Vector4D, Vector4D, Vector4D, Vector4D) value) => new Matrix4x4(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector4D column1, out Vector4D column2, out Vector4D column3, out Vector4D column4) { column1 = Column1; column2 = Column2; column3 = Column3; column4 = Column4; }
        public override bool Equals(object obj) { if (!(obj is Matrix4x4)) return false; var other = (Matrix4x4)obj; return Column1.Equals(other.Column1) && Column2.Equals(other.Column2) && Column3.Equals(other.Column3) && Column4.Equals(other.Column4); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Column1, Column2, Column3, Column4);
        public override string ToString() => $"{{ \"Column1\" = {Column1}, \"Column2\" = {Column2}, \"Column3\" = {Column3}, \"Column4\" = {Column4} }}";
        public static implicit operator Dynamic(Matrix4x4 self) => new Dynamic(self);
        public static implicit operator Matrix4x4(Dynamic value) => value.As<Matrix4x4>();
        public String TypeName => "Matrix4x4";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Column1", (String)"Column2", (String)"Column3", (String)"Column4");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Column1), new Dynamic(Column2), new Dynamic(Column3), new Dynamic(Column4));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Matrix4x4)b);
        // Array predefined functions
        public Matrix4x4(IArray<Vector4D> xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public Matrix4x4(Vector4D[] xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public static Matrix4x4 New(IArray<Vector4D> xs) => new Matrix4x4(xs);
        public static Matrix4x4 New(Vector4D[] xs) => new Matrix4x4(xs);
        public static implicit operator Vector4D[](Matrix4x4 self) => self.ToSystemArray();
        public static implicit operator Array<Vector4D>(Matrix4x4 self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Vector4D> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector4D System.Collections.Generic.IReadOnlyList<Vector4D>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Vector4D>.Count => this.Count;
        // Implemented concept functions and type functions
        public static Matrix4x4 Identity => ((Integer)1).Tuple4(((Integer)0), ((Integer)0), ((Integer)0)).Tuple4(((Integer)0).Tuple4(((Integer)1), ((Integer)0), ((Integer)0)), ((Integer)0).Tuple4(((Integer)0), ((Integer)1), ((Integer)0)), ((Integer)0).Tuple4(((Integer)0), ((Integer)0), ((Integer)1)));
        public Number M11 => this.Column1.X;
        public Number M12 => this.Column2.X;
        public Number M13 => this.Column3.X;
        public Number M14 => this.Column4.X;
        public Number M21 => this.Column1.Y;
        public Number M22 => this.Column2.Y;
        public Number M23 => this.Column3.Y;
        public Number M24 => this.Column4.Y;
        public Number M31 => this.Column1.Z;
        public Number M32 => this.Column2.Z;
        public Number M33 => this.Column3.Z;
        public Number M34 => this.Column4.Z;
        public Number M41 => this.Column1.W;
        public Number M42 => this.Column2.W;
        public Number M43 => this.Column3.W;
        public Number M44 => this.Column4.W;
        public Vector4D Row1 => M11.Tuple4(M12, M13, M14);
        public Vector4D Row2 => M21.Tuple4(M22, M23, M24);
        public Vector4D Row3 => M31.Tuple4(M32, M33, M34);
        public Vector4D Row4 => M41.Tuple4(M42, M43, M44);
        public Vector4D GetRow(Integer row) => row.Equals(((Integer)0)) ? Row1 : row.Equals(((Integer)1)) ? Row2 : row.Equals(((Integer)2)) ? Row3 : Row4;
        public Vector3D Multiply(Vector3D v) => v.X.Multiply(this.M11).Add(v.Y.Multiply(this.M21).Add(v.Z.Multiply(this.M31).Add(this.M41))).Tuple3(v.X.Multiply(this.M12).Add(v.Y.Multiply(this.M22).Add(v.Z.Multiply(this.M32).Add(this.M42))), v.X.Multiply(this.M13).Add(v.Y.Multiply(this.M23).Add(v.Z.Multiply(this.M33).Add(this.M43))));
        public static Vector3D operator *(Matrix4x4 m, Vector3D v) => m.Multiply(v);
        public static Matrix4x4 CreateFromRows(Vector4D row1, Vector4D row2, Vector4D row3, Vector4D row4) => row1.X.Tuple4(row2.X, row3.X, row4.X).Tuple4(row1.Y.Tuple4(row2.Y, row3.Y, row4.Y), row1.Z.Tuple4(row2.Z, row3.Z, row4.Z), row1.W.Tuple4(row2.W, row3.W, row4.W));
        public static Matrix4x4 CreateFromRows(Vector3D row1, Vector3D row2, Vector3D row3) => Matrix4x4.CreateFromRows(row1, row2, row3, ((Integer)0).Tuple4(((Integer)0), ((Integer)0), ((Integer)1)));
        public Matrix4x4 WithTranslation(Vector3D v) => Matrix4x4.CreateFromRows(this.Row1, this.Row2, this.Row3, v.X.Tuple4(v.Y, v.Z, ((Integer)1)));
        public Vector3D Translation => this.M14.Tuple3(this.M24, this.M34);
        public static Matrix4x4 CreateTranslation(Vector3D v) => Identity.WithTranslation(v);
        public static Matrix4x4 CreateScale(Vector3D v) => v.X.Tuple4(((Integer)0), ((Integer)0), ((Integer)0)).Tuple4(((Integer)0).Tuple4(v.Y, ((Integer)0), ((Integer)0)), ((Integer)0).Tuple4(((Integer)0), v.Z, ((Integer)0)), ((Integer)0).Tuple4(((Integer)0), ((Integer)0), ((Integer)1)));
        public Vector3D Transform(Vector3D v) => this.Multiply(v);
        public Vector3D TransformNormal(Vector3D v) => v.X.Multiply(this.M11).Add(v.Y.Multiply(this.M21).Add(v.Z.Multiply(this.M31))).Tuple3(v.X.Multiply(this.M12).Add(v.Y.Multiply(this.M22).Add(v.Z.Multiply(this.M32))), v.X.Multiply(this.M13).Add(v.Y.Multiply(this.M23).Add(v.Z.Multiply(this.M33))));
        public Matrix4x4 Transpose => this.Row1.Tuple4(this.Row2, this.Row3, this.Row4);
        public Matrix4x4 Multiply(Number s) => this.Column1.Multiply(s).Tuple4(this.Column2.Multiply(s), this.Column3.Multiply(s), this.Column4.Multiply(s));
        public static Matrix4x4 operator *(Matrix4x4 m, Number s) => m.Multiply(s);
        public Matrix4x4 Divide(Number s) => this.Column1.Divide(s).Tuple4(this.Column2.Divide(s), this.Column3.Divide(s), this.Column4.Divide(s));
        public static Matrix4x4 operator /(Matrix4x4 m, Number s) => m.Divide(s);
        public Matrix4x4 Multiply(Matrix4x4 b) => Matrix4x4.CreateFromRows(this.M11.Multiply(b.M11).Add(this.M12.Multiply(b.M21).Add(this.M13.Multiply(b.M31).Add(this.M14.Multiply(b.M41)))).Tuple4(this.M11.Multiply(b.M12).Add(this.M12.Multiply(b.M22).Add(this.M13.Multiply(b.M32).Add(this.M14.Multiply(b.M42)))), this.M11.Multiply(b.M13).Add(this.M12.Multiply(b.M23).Add(this.M13.Multiply(b.M33).Add(this.M14.Multiply(b.M43)))), this.M11.Multiply(b.M14).Add(this.M12.Multiply(b.M24).Add(this.M13.Multiply(b.M34).Add(this.M14.Multiply(b.M44))))), this.M21.Multiply(b.M11).Add(this.M22.Multiply(b.M21).Add(this.M23.Multiply(b.M31).Add(this.M24.Multiply(b.M41)))).Tuple4(this.M21.Multiply(b.M12).Add(this.M22.Multiply(b.M22).Add(this.M23.Multiply(b.M32).Add(this.M24.Multiply(b.M42)))), this.M21.Multiply(b.M13).Add(this.M22.Multiply(b.M23).Add(this.M23.Multiply(b.M33).Add(this.M24.Multiply(b.M43)))), this.M21.Multiply(b.M14).Add(this.M22.Multiply(b.M24).Add(this.M23.Multiply(b.M34).Add(this.M24.Multiply(b.M44))))), this.M31.Multiply(b.M11).Add(this.M32.Multiply(b.M21).Add(this.M33.Multiply(b.M31).Add(this.M34.Multiply(b.M41)))).Tuple4(this.M31.Multiply(b.M12).Add(this.M32.Multiply(b.M22).Add(this.M33.Multiply(b.M32).Add(this.M34.Multiply(b.M42)))), this.M31.Multiply(b.M13).Add(this.M32.Multiply(b.M23).Add(this.M33.Multiply(b.M33).Add(this.M34.Multiply(b.M43)))), this.M31.Multiply(b.M14).Add(this.M32.Multiply(b.M24).Add(this.M33.Multiply(b.M34).Add(this.M34.Multiply(b.M44))))), this.M41.Multiply(b.M11).Add(this.M42.Multiply(b.M21).Add(this.M43.Multiply(b.M31).Add(this.M44.Multiply(b.M41)))).Tuple4(this.M41.Multiply(b.M12).Add(this.M42.Multiply(b.M22).Add(this.M43.Multiply(b.M32).Add(this.M44.Multiply(b.M42)))), this.M41.Multiply(b.M13).Add(this.M42.Multiply(b.M23).Add(this.M43.Multiply(b.M33).Add(this.M44.Multiply(b.M43)))), this.M41.Multiply(b.M14).Add(this.M42.Multiply(b.M24).Add(this.M43.Multiply(b.M34).Add(this.M44.Multiply(b.M44))))));
        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b) => a.Multiply(b);
        public Matrix4x4 Matrix => this;
        public Quaternion QuaternionFromRotationMatrix { get {
            var trace = this.M11.Add(this.M22.Add(this.M33));
            if (trace.GreaterThan(((Number)0)))
            {
                var s = trace.Add(((Number)1)).Sqrt;
                var w = s.Multiply(((Number)0.5));
                var s1 = ((Number)0.5).Divide(s);
                return this.M23.Subtract(this.M32).Multiply(s1).Tuple4(this.M31.Subtract(this.M13).Multiply(s1), this.M12.Subtract(this.M21).Multiply(s1), w);
            }
            if (this.M11.GreaterThanOrEquals(this.M22).And(this.M11.GreaterThanOrEquals(this.M33)))
            {
                var s = ((Number)1).Add(this.M11.Subtract(this.M22.Subtract(this.M33))).Sqrt;
                var invS = ((Number)0.5).Divide(s);
                return s.Half.Tuple4(this.M12.Add(this.M21).Multiply(invS), this.M13.Add(this.M31).Multiply(invS), this.M23.Subtract(this.M32).Multiply(invS));
            }
            if (this.M22.GreaterThan(this.M33))
            {
                var s = ((Number)1).Add(this.M22.Subtract(this.M11.Subtract(this.M33))).Sqrt;
                var invS = ((Number)0.5).Divide(s);
                return this.M21.Add(this.M12).Multiply(invS).Tuple4(s.Half, this.M32.Add(this.M23).Multiply(invS), this.M31.Subtract(this.M13).Multiply(invS));
            }
            {
                var s = ((Number)1).Add(this.M33.Subtract(this.M11.Subtract(this.M22))).Sqrt;
                var invS = ((Number)0.5).Divide(s);
                return this.M31.Add(this.M13).Multiply(invS).Tuple4(this.M32.Add(this.M23).Multiply(invS), s.Half, this.M12.Subtract(this.M21).Multiply(invS));
            }
        }
         } public IArray<Matrix4x4> Repeat(Integer n){
            var _var692 = this;
            return n.MapRange((i) => _var692);
        }
        public Boolean Equals(Matrix4x4 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Matrix4x4 a, Matrix4x4 b) => a.Equals(b);
        public Boolean NotEquals(Matrix4x4 b) => this.Equals(b).Not;
        public static Boolean operator !=(Matrix4x4 a, Matrix4x4 b) => a.NotEquals(b);
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector4D At(Integer n) => n == 0 ? Column1 : n == 1 ? Column2 : n == 2 ? Column3 : n == 3 ? Column4 : throw new System.IndexOutOfRangeException();
        public Vector4D this[Integer n] => n == 0 ? Column1 : n == 1 ? Column2 : n == 2 ? Column3 : n == 3 ? Column4 : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct IdentityTransform3D: IValue<IdentityTransform3D>, ITransform3D
    {
        public static IdentityTransform3D Default = new IdentityTransform3D();
        public static IdentityTransform3D New() => new IdentityTransform3D();
        public override bool Equals(object obj) => true;
        public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => $"{{  }}";
        public static implicit operator Dynamic(IdentityTransform3D self) => new Dynamic(self);
        public static implicit operator IdentityTransform3D(Dynamic value) => value.As<IdentityTransform3D>();
        public String TypeName => "IdentityTransform3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>();
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((IdentityTransform3D)b);
        // Implemented concept functions and type functions
        public Vector3D Transform(Vector3D v) => v;
        public Vector3D TransformNormal(Vector3D v) => v;
        public Matrix4x4 Matrix => Matrix4x4.Identity;
        public IArray<IdentityTransform3D> Repeat(Integer n){
            var _var693 = this;
            return n.MapRange((i) => _var693);
        }
        public Boolean Equals(IdentityTransform3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(IdentityTransform3D a, IdentityTransform3D b) => a.Equals(b);
        public Boolean NotEquals(IdentityTransform3D b) => this.Equals(b).Not;
        public static Boolean operator !=(IdentityTransform3D a, IdentityTransform3D b) => a.NotEquals(b);
        public Matrix4x4 Matrix4x4 => this.Matrix;
        public static implicit operator Matrix4x4(IdentityTransform3D t) => t.Matrix4x4;
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Translation3D: ITransform3D
    {
        [DataMember] public readonly Vector3D Translation;
        public Translation3D WithTranslation(Vector3D translation) => new Translation3D(translation);
        public Translation3D(Vector3D translation) => (Translation) = (translation);
        public static Translation3D Default = new Translation3D();
        public static Translation3D New(Vector3D translation) => new Translation3D(translation);
        public Plato.SinglePrecision.Translation3D ChangePrecision() => (Translation.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Translation3D(Translation3D self) => self.ChangePrecision();
        public static implicit operator Vector3D(Translation3D self) => self.Translation;
        public static implicit operator Translation3D(Vector3D value) => new Translation3D(value);
        public override bool Equals(object obj) { if (!(obj is Translation3D)) return false; var other = (Translation3D)obj; return Translation.Equals(other.Translation); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Translation);
        public override string ToString() => $"{{ \"Translation\" = {Translation} }}";
        public static implicit operator Dynamic(Translation3D self) => new Dynamic(self);
        public static implicit operator Translation3D(Dynamic value) => value.As<Translation3D>();
        public String TypeName => "Translation3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Translation");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Translation));
        // Implemented concept functions and type functions
        public Matrix4x4 Matrix => this.Translation.Matrix;
        public Vector3D Transform(Vector3D v) => v.Add(this.Translation);
        public Vector3D TransformNormal(Vector3D v) => v;
        public Matrix4x4 Matrix4x4 => this.Matrix;
        public static implicit operator Matrix4x4(Translation3D t) => t.Matrix4x4;
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Transform3D: IValue<Transform3D>, ITransform3D
    {
        [DataMember] public readonly Vector3D Translation;
        [DataMember] public readonly Quaternion Rotation;
        [DataMember] public readonly Vector3D Scale;
        public Transform3D WithTranslation(Vector3D translation) => new Transform3D(translation, Rotation, Scale);
        public Transform3D WithRotation(Quaternion rotation) => new Transform3D(Translation, rotation, Scale);
        public Transform3D WithScale(Vector3D scale) => new Transform3D(Translation, Rotation, scale);
        public Transform3D(Vector3D translation, Quaternion rotation, Vector3D scale) => (Translation, Rotation, Scale) = (translation, rotation, scale);
        public static Transform3D Default = new Transform3D();
        public static Transform3D New(Vector3D translation, Quaternion rotation, Vector3D scale) => new Transform3D(translation, rotation, scale);
        public Plato.SinglePrecision.Transform3D ChangePrecision() => (Translation.ChangePrecision(), Rotation.ChangePrecision(), Scale.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Transform3D(Transform3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Quaternion, Vector3D)(Transform3D self) => (self.Translation, self.Rotation, self.Scale);
        public static implicit operator Transform3D((Vector3D, Quaternion, Vector3D) value) => new Transform3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D translation, out Quaternion rotation, out Vector3D scale) { translation = Translation; rotation = Rotation; scale = Scale; }
        public override bool Equals(object obj) { if (!(obj is Transform3D)) return false; var other = (Transform3D)obj; return Translation.Equals(other.Translation) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Translation, Rotation, Scale);
        public override string ToString() => $"{{ \"Translation\" = {Translation}, \"Rotation\" = {Rotation}, \"Scale\" = {Scale} }}";
        public static implicit operator Dynamic(Transform3D self) => new Dynamic(self);
        public static implicit operator Transform3D(Dynamic value) => value.As<Transform3D>();
        public String TypeName => "Transform3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Translation", (String)"Rotation", (String)"Scale");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Translation), new Dynamic(Rotation), new Dynamic(Scale));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Transform3D)b);
        // Implemented concept functions and type functions
        public Vector3D Transform(Vector3D v) => this.Rotation.Transform(v).Add(this.Translation).Multiply(this.Scale);
        public Vector3D TransformNormal(Vector3D v) => this.Rotation.TransformNormal(v);
        public Matrix4x4 Matrix => this.Scale.Matrix.Multiply(this.Rotation.Matrix.Multiply(this.Translation.Matrix));
        public IArray<Transform3D> Repeat(Integer n){
            var _var694 = this;
            return n.MapRange((i) => _var694);
        }
        public Boolean Equals(Transform3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Transform3D a, Transform3D b) => a.Equals(b);
        public Boolean NotEquals(Transform3D b) => this.Equals(b).Not;
        public static Boolean operator !=(Transform3D a, Transform3D b) => a.NotEquals(b);
        public Matrix4x4 Matrix4x4 => this.Matrix;
        public static implicit operator Matrix4x4(Transform3D t) => t.Matrix4x4;
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Pose3D: IValue<Pose3D>, ITransform3D
    {
        [DataMember] public readonly Vector3D Position;
        [DataMember] public readonly Rotation3D Rotation;
        public Pose3D WithPosition(Vector3D position) => new Pose3D(position, Rotation);
        public Pose3D WithRotation(Rotation3D rotation) => new Pose3D(Position, rotation);
        public Pose3D(Vector3D position, Rotation3D rotation) => (Position, Rotation) = (position, rotation);
        public static Pose3D Default = new Pose3D();
        public static Pose3D New(Vector3D position, Rotation3D rotation) => new Pose3D(position, rotation);
        public Plato.SinglePrecision.Pose3D ChangePrecision() => (Position.ChangePrecision(), Rotation.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Pose3D(Pose3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Rotation3D)(Pose3D self) => (self.Position, self.Rotation);
        public static implicit operator Pose3D((Vector3D, Rotation3D) value) => new Pose3D(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D position, out Rotation3D rotation) { position = Position; rotation = Rotation; }
        public override bool Equals(object obj) { if (!(obj is Pose3D)) return false; var other = (Pose3D)obj; return Position.Equals(other.Position) && Rotation.Equals(other.Rotation); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Position, Rotation);
        public override string ToString() => $"{{ \"Position\" = {Position}, \"Rotation\" = {Rotation} }}";
        public static implicit operator Dynamic(Pose3D self) => new Dynamic(self);
        public static implicit operator Pose3D(Dynamic value) => value.As<Pose3D>();
        public String TypeName => "Pose3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Position", (String)"Rotation");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Position), new Dynamic(Rotation));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Pose3D)b);
        // Implemented concept functions and type functions
        public Vector3D Transform(Vector3D v) => this.Rotation.Transform(v).Add(this.Position);
        public Vector3D TransformNormal(Vector3D v) => this.Rotation.TransformNormal(v);
        public Matrix4x4 Matrix => this.Rotation.Matrix.Multiply(this.Position.Matrix);
        public IArray<Pose3D> Repeat(Integer n){
            var _var695 = this;
            return n.MapRange((i) => _var695);
        }
        public Boolean Equals(Pose3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Pose3D a, Pose3D b) => a.Equals(b);
        public Boolean NotEquals(Pose3D b) => this.Equals(b).Not;
        public static Boolean operator !=(Pose3D a, Pose3D b) => a.NotEquals(b);
        public Matrix4x4 Matrix4x4 => this.Matrix;
        public static implicit operator Matrix4x4(Pose3D t) => t.Matrix4x4;
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Frame3D: IValue<Frame3D>, ITransform3D
    {
        [DataMember] public readonly Vector3D Forward;
        [DataMember] public readonly Vector3D Up;
        [DataMember] public readonly Vector3D Position;
        public Frame3D WithForward(Vector3D forward) => new Frame3D(forward, Up, Position);
        public Frame3D WithUp(Vector3D up) => new Frame3D(Forward, up, Position);
        public Frame3D WithPosition(Vector3D position) => new Frame3D(Forward, Up, position);
        public Frame3D(Vector3D forward, Vector3D up, Vector3D position) => (Forward, Up, Position) = (forward, up, position);
        public static Frame3D Default = new Frame3D();
        public static Frame3D New(Vector3D forward, Vector3D up, Vector3D position) => new Frame3D(forward, up, position);
        public Plato.SinglePrecision.Frame3D ChangePrecision() => (Forward.ChangePrecision(), Up.ChangePrecision(), Position.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Frame3D(Frame3D self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Vector3D, Vector3D)(Frame3D self) => (self.Forward, self.Up, self.Position);
        public static implicit operator Frame3D((Vector3D, Vector3D, Vector3D) value) => new Frame3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D forward, out Vector3D up, out Vector3D position) { forward = Forward; up = Up; position = Position; }
        public override bool Equals(object obj) { if (!(obj is Frame3D)) return false; var other = (Frame3D)obj; return Forward.Equals(other.Forward) && Up.Equals(other.Up) && Position.Equals(other.Position); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Forward, Up, Position);
        public override string ToString() => $"{{ \"Forward\" = {Forward}, \"Up\" = {Up}, \"Position\" = {Position} }}";
        public static implicit operator Dynamic(Frame3D self) => new Dynamic(self);
        public static implicit operator Frame3D(Dynamic value) => value.As<Frame3D>();
        public String TypeName => "Frame3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Forward", (String)"Up", (String)"Position");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Forward), new Dynamic(Up), new Dynamic(Position));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Frame3D)b);
        // Implemented concept functions and type functions
        public Pose3D Pose3D => this.Position.Tuple2(this.Forward.LookRotation(this.Up));
        public static implicit operator Pose3D(Frame3D f) => f.Pose3D;
        public Vector3D Transform(Vector3D v) => this.Pose3D.Transform(v);
        public Vector3D TransformNormal(Vector3D v) => this.Pose3D.TransformNormal(v);
        public Matrix4x4 Matrix => this.Pose3D.Matrix;
        public IArray<Frame3D> Repeat(Integer n){
            var _var696 = this;
            return n.MapRange((i) => _var696);
        }
        public Boolean Equals(Frame3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Frame3D a, Frame3D b) => a.Equals(b);
        public Boolean NotEquals(Frame3D b) => this.Equals(b).Not;
        public static Boolean operator !=(Frame3D a, Frame3D b) => a.NotEquals(b);
        public Matrix4x4 Matrix4x4 => this.Matrix;
        public static implicit operator Matrix4x4(Frame3D t) => t.Matrix4x4;
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Quaternion: IValue<Quaternion>, IArray<Number>, ITransform3D
    {
        [DataMember] public readonly Number X;
        [DataMember] public readonly Number Y;
        [DataMember] public readonly Number Z;
        [DataMember] public readonly Number W;
        public Quaternion WithX(Number x) => new Quaternion(x, Y, Z, W);
        public Quaternion WithY(Number y) => new Quaternion(X, y, Z, W);
        public Quaternion WithZ(Number z) => new Quaternion(X, Y, z, W);
        public Quaternion WithW(Number w) => new Quaternion(X, Y, Z, w);
        public Quaternion(Number x, Number y, Number z, Number w) => (X, Y, Z, W) = (x, y, z, w);
        public static Quaternion Default = new Quaternion();
        public static Quaternion New(Number x, Number y, Number z, Number w) => new Quaternion(x, y, z, w);
        public Plato.SinglePrecision.Quaternion ChangePrecision() => (X.ChangePrecision(), Y.ChangePrecision(), Z.ChangePrecision(), W.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Quaternion(Quaternion self) => self.ChangePrecision();
        public static implicit operator (Number, Number, Number, Number)(Quaternion self) => (self.X, self.Y, self.Z, self.W);
        public static implicit operator Quaternion((Number, Number, Number, Number) value) => new Quaternion(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Number x, out Number y, out Number z, out Number w) { x = X; y = Y; z = Z; w = W; }
        public override bool Equals(object obj) { if (!(obj is Quaternion)) return false; var other = (Quaternion)obj; return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X, Y, Z, W);
        public override string ToString() => $"{{ \"X\" = {X}, \"Y\" = {Y}, \"Z\" = {Z}, \"W\" = {W} }}";
        public static implicit operator Dynamic(Quaternion self) => new Dynamic(self);
        public static implicit operator Quaternion(Dynamic value) => value.As<Quaternion>();
        public String TypeName => "Quaternion";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"X", (String)"Y", (String)"Z", (String)"W");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X), new Dynamic(Y), new Dynamic(Z), new Dynamic(W));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Quaternion)b);
        // Array predefined functions
        public Quaternion(IArray<Number> xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public Quaternion(Number[] xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        public static Quaternion New(IArray<Number> xs) => new Quaternion(xs);
        public static Quaternion New(Number[] xs) => new Quaternion(xs);
        public static implicit operator Number[](Quaternion self) => self.ToSystemArray();
        public static implicit operator Array<Number>(Quaternion self) => self.ToPrimitiveArray();
        public System.Collections.Generic.IEnumerator<Number> GetEnumerator() { for (var i=0; i < Count; i++) yield return At(i); }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Number System.Collections.Generic.IReadOnlyList<Number>.this[int n] => At(n);
        int System.Collections.Generic.IReadOnlyCollection<Number>.Count => this.Count;
        // Implemented concept functions and type functions
        public static Quaternion Identity => ((Integer)0).Tuple4(((Integer)0), ((Integer)0), ((Integer)1));
        public Vector4D Vector4D => this.X.Tuple4(this.Y, this.Z, this.W);
        public static implicit operator Vector4D(Quaternion q) => q.Vector4D;
        public Number Magnitude => this.Vector4D.Magnitude;
        public Number MagnitudeSquared => this.Vector4D.MagnitudeSquared;
        public Vector3D Transform(Vector3D v){
            var x2 = this.X.Add(this.X);
            var y2 = this.Y.Add(this.Y);
            var z2 = this.Z.Add(this.Z);
            var wx2 = this.W.Multiply(x2);
            var wy2 = this.W.Multiply(y2);
            var wz2 = this.W.Multiply(z2);
            var xx2 = this.X.Multiply(x2);
            var xy2 = this.X.Multiply(y2);
            var xz2 = this.X.Multiply(z2);
            var yy2 = this.Y.Multiply(y2);
            var yz2 = this.Y.Multiply(z2);
            var zz2 = this.Z.Multiply(z2);
            return v.X.Multiply(((Number)1).Subtract(yy2.Subtract(zz2))).Add(v.Y.Multiply(xy2.Subtract(wz2)).Add(v.Z.Multiply(xz2.Add(wy2)))).Tuple3(v.X.Multiply(xy2.Add(wz2)).Add(v.Y.Multiply(((Number)1).Subtract(xx2.Subtract(zz2))).Add(v.Z.Multiply(yz2.Subtract(wx2)))), v.X.Multiply(xz2.Subtract(wy2)).Add(v.Y.Multiply(yz2.Add(wx2)).Add(v.Z.Multiply(((Number)1).Subtract(xx2.Subtract(yy2))))));
        }
        public Vector3D TransformNormal(Vector3D v) => this.Transform(v);
        public Quaternion Concatenate(Quaternion q2) => this.ReverseConcatenate(q2);
        public Quaternion Conjugate => this.X.Negative.Tuple4(this.Y.Negative, this.Z.Negative, this.W);
        public Quaternion Inverse => this.Conjugate.Multiply(this.MagnitudeSquared.Inverse);
        public Quaternion Normalize => this.Vector4D.Normalize;
        public Vector3D XYZ => X.Tuple3(Y, Z);
        public Quaternion ReverseConcatenate(Quaternion q1){
            var av = this.XYZ;
            var bv = q1.XYZ;
            var cv = av.Cross(bv);
            var dot = av.Dot(bv);
            return this.X.Multiply(q1.W).Add(q1.X.Multiply(this.W).Add(cv.X)).Tuple4(this.Y.Multiply(q1.W).Add(q1.Y.Multiply(this.W).Add(cv.Y)), this.Z.Multiply(q1.W).Add(q1.Z.Multiply(this.W).Add(cv.Z)), this.W.Multiply(q1.W).Subtract(dot));
        }
        public Quaternion Multiply(Number scalar) => this.Vector4D.Multiply(scalar);
        public static Quaternion operator *(Quaternion q, Number scalar) => q.Multiply(scalar);
        public Quaternion Divide(Number scalar) => this.Vector4D.Divide(scalar);
        public static Quaternion operator /(Quaternion q, Number scalar) => q.Divide(scalar);
        public Quaternion Add(Quaternion q2) => this.Vector4D.Add(q2.Vector4D);
        public static Quaternion operator +(Quaternion q1, Quaternion q2) => q1.Add(q2);
        public Quaternion Negate => this.Vector4D.Negative;
        public Quaternion Subtract(Quaternion q2) => this.Vector4D.Subtract(q2.Vector4D);
        public static Quaternion operator -(Quaternion q1, Quaternion q2) => q1.Subtract(q2);
        public Number Dot(Quaternion q2) => this.Vector4D.Dot(q2.Vector4D);
        public Quaternion Slerp(Quaternion q2, Number t){
            var cosOmega = this.Dot(q2);
            var flip = cosOmega.LessThan(((Number)0));
            if (cosOmega.Abs.GreaterThan(((Number)1).Subtract(((Number)1E-06))))
            {
                var s1 = ((Number)1).Subtract(t);
                var s2 = flip ? t.Negative : t;
                return this.Multiply(s1).Add(q2.Multiply(s2));
            }
            else
            {
                var omega = cosOmega.Abs.Acos;
                var invSinOmega = omega.Sin.Inverse;
                var s1 = ((Number)1).Subtract(t).Multiply(omega).Sin.Multiply(invSinOmega);
                var s2 = flip ? t.Multiply(omega).Sin.Multiply(invSinOmega).Negative : t.Multiply(omega).Sin.Multiply(invSinOmega);
                return this.Multiply(s1).Add(q2.Multiply(s2));
            }
        }
        public Quaternion Multiply(Quaternion q2){
            var tmp_00 = this.Z.Subtract(this.Y).Multiply(q2.Y.Subtract(q2.Z));
            var tmp_01 = this.W.Add(this.X).Multiply(q2.W.Add(q2.X));
            var tmp_02 = this.W.Subtract(this.X).Multiply(q2.Y.Add(q2.Z));
            var tmp_03 = this.Y.Add(this.Z).Multiply(q2.W.Subtract(q2.X));
            var tmp_04 = this.Z.Subtract(this.X).Multiply(q2.X.Subtract(q2.Y));
            var tmp_05 = this.Z.Add(this.X).Multiply(q2.X.Add(q2.Y));
            var tmp_06 = this.W.Add(this.Y).Multiply(q2.W.Subtract(q2.Z));
            var tmp_07 = this.W.Subtract(this.Y).Multiply(q2.W.Add(q2.Z));
            var tmp_08 = tmp_05.Add(tmp_06.Add(tmp_07));
            var tmp_09 = tmp_04.Add(tmp_08).Multiply(((Number)0.5));
            return tmp_01.Add(tmp_09.Subtract(tmp_08)).Tuple4(tmp_02.Add(tmp_09.Subtract(tmp_07)), tmp_03.Add(tmp_09.Subtract(tmp_06)), tmp_00.Add(tmp_09.Subtract(tmp_05)));
        }
        public static Quaternion operator *(Quaternion q1, Quaternion q2) => q1.Multiply(q2);
        public Matrix4x4 Matrix { get {
            var q1 = this.Normalize;
            var xx = q1.X.Multiply(q1.X);
            var yy = q1.Y.Multiply(q1.Y);
            var zz = q1.Z.Multiply(q1.Z);
            var xy = q1.X.Multiply(q1.Y);
            var wz = q1.Z.Multiply(q1.W);
            var xz = q1.Z.Multiply(q1.X);
            var wy = q1.Y.Multiply(q1.W);
            var yz = q1.Y.Multiply(q1.Z);
            var wx = q1.X.Multiply(q1.W);
            return ((Number)1).Subtract(((Number)2).Multiply(yy.Add(zz))).Tuple4(((Number)2).Multiply(xy.Add(wz)), ((Number)2).Multiply(xz.Subtract(wy)), ((Number)0)).Tuple4(((Number)2).Multiply(xy.Subtract(wz)).Tuple4(((Number)1).Subtract(((Number)2).Multiply(zz.Add(xx))), ((Number)2).Multiply(yz.Add(wx)), ((Number)0)), ((Number)2).Multiply(xz.Add(wy)).Tuple4(((Number)2).Multiply(yz.Subtract(wx)), ((Number)1).Subtract(((Number)2).Multiply(yy.Add(xx))), ((Number)0)), ((Number)0).Tuple4(((Number)0), ((Number)0), ((Number)1)));
        }
         } public IArray<Quaternion> Repeat(Integer n){
            var _var697 = this;
            return n.MapRange((i) => _var697);
        }
        public Boolean Equals(Quaternion b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Quaternion a, Quaternion b) => a.Equals(b);
        public Boolean NotEquals(Quaternion b) => this.Equals(b).Not;
        public static Boolean operator !=(Quaternion a, Quaternion b) => a.NotEquals(b);
        public Matrix4x4 Matrix4x4 => this.Matrix;
        public static implicit operator Matrix4x4(Quaternion t) => t.Matrix4x4;
        // Unimplemented concept functions
        public Integer Count => 4;
        public Number At(Integer n) => n == 0 ? X : n == 1 ? Y : n == 2 ? Z : n == 3 ? W : throw new System.IndexOutOfRangeException();
        public Number this[Integer n] => n == 0 ? X : n == 1 ? Y : n == 2 ? Z : n == 3 ? W : throw new System.IndexOutOfRangeException();
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct AxisAngle: IValue<AxisAngle>, ITransform3D
    {
        [DataMember] public readonly Vector3D Axis;
        [DataMember] public readonly Angle Angle;
        public AxisAngle WithAxis(Vector3D axis) => new AxisAngle(axis, Angle);
        public AxisAngle WithAngle(Angle angle) => new AxisAngle(Axis, angle);
        public AxisAngle(Vector3D axis, Angle angle) => (Axis, Angle) = (axis, angle);
        public static AxisAngle Default = new AxisAngle();
        public static AxisAngle New(Vector3D axis, Angle angle) => new AxisAngle(axis, angle);
        public Plato.SinglePrecision.AxisAngle ChangePrecision() => (Axis.ChangePrecision(), Angle.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.AxisAngle(AxisAngle self) => self.ChangePrecision();
        public static implicit operator (Vector3D, Angle)(AxisAngle self) => (self.Axis, self.Angle);
        public static implicit operator AxisAngle((Vector3D, Angle) value) => new AxisAngle(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D axis, out Angle angle) { axis = Axis; angle = Angle; }
        public override bool Equals(object obj) { if (!(obj is AxisAngle)) return false; var other = (AxisAngle)obj; return Axis.Equals(other.Axis) && Angle.Equals(other.Angle); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Axis, Angle);
        public override string ToString() => $"{{ \"Axis\" = {Axis}, \"Angle\" = {Angle} }}";
        public static implicit operator Dynamic(AxisAngle self) => new Dynamic(self);
        public static implicit operator AxisAngle(Dynamic value) => value.As<AxisAngle>();
        public String TypeName => "AxisAngle";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Axis", (String)"Angle");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Axis), new Dynamic(Angle));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((AxisAngle)b);
        // Implemented concept functions and type functions
        public Vector3D Transform(Vector3D v) => this.Quaternion.Transform(v);
        public Vector3D TransformNormal(Vector3D v) => this.Transform(v);
        public Quaternion Quaternion { get {
            var axis = this.Axis.Normalize;
            var sinHalfAngle = this.Angle.Half.Sin;
            var cosHalfAngle = this.Angle.Half.Cos;
            return axis.X.Multiply(sinHalfAngle).Tuple4(axis.Y.Multiply(sinHalfAngle), axis.Z.Multiply(sinHalfAngle), cosHalfAngle);
        }
         } public static implicit operator Quaternion(AxisAngle aa) => aa.Quaternion;
        public Matrix4x4 Matrix => this.Quaternion.Matrix;
        public IArray<AxisAngle> Repeat(Integer n){
            var _var698 = this;
            return n.MapRange((i) => _var698);
        }
        public Boolean Equals(AxisAngle b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(AxisAngle a, AxisAngle b) => a.Equals(b);
        public Boolean NotEquals(AxisAngle b) => this.Equals(b).Not;
        public static Boolean operator !=(AxisAngle a, AxisAngle b) => a.NotEquals(b);
        public Matrix4x4 Matrix4x4 => this.Matrix;
        public static implicit operator Matrix4x4(AxisAngle t) => t.Matrix4x4;
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct EulerAngles: IValue<EulerAngles>, ITransform3D
    {
        [DataMember] public readonly Angle Yaw;
        [DataMember] public readonly Angle Pitch;
        [DataMember] public readonly Angle Roll;
        public EulerAngles WithYaw(Angle yaw) => new EulerAngles(yaw, Pitch, Roll);
        public EulerAngles WithPitch(Angle pitch) => new EulerAngles(Yaw, pitch, Roll);
        public EulerAngles WithRoll(Angle roll) => new EulerAngles(Yaw, Pitch, roll);
        public EulerAngles(Angle yaw, Angle pitch, Angle roll) => (Yaw, Pitch, Roll) = (yaw, pitch, roll);
        public static EulerAngles Default = new EulerAngles();
        public static EulerAngles New(Angle yaw, Angle pitch, Angle roll) => new EulerAngles(yaw, pitch, roll);
        public Plato.SinglePrecision.EulerAngles ChangePrecision() => (Yaw.ChangePrecision(), Pitch.ChangePrecision(), Roll.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.EulerAngles(EulerAngles self) => self.ChangePrecision();
        public static implicit operator (Angle, Angle, Angle)(EulerAngles self) => (self.Yaw, self.Pitch, self.Roll);
        public static implicit operator EulerAngles((Angle, Angle, Angle) value) => new EulerAngles(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Angle yaw, out Angle pitch, out Angle roll) { yaw = Yaw; pitch = Pitch; roll = Roll; }
        public override bool Equals(object obj) { if (!(obj is EulerAngles)) return false; var other = (EulerAngles)obj; return Yaw.Equals(other.Yaw) && Pitch.Equals(other.Pitch) && Roll.Equals(other.Roll); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Yaw, Pitch, Roll);
        public override string ToString() => $"{{ \"Yaw\" = {Yaw}, \"Pitch\" = {Pitch}, \"Roll\" = {Roll} }}";
        public static implicit operator Dynamic(EulerAngles self) => new Dynamic(self);
        public static implicit operator EulerAngles(Dynamic value) => value.As<EulerAngles>();
        public String TypeName => "EulerAngles";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Yaw", (String)"Pitch", (String)"Roll");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Yaw), new Dynamic(Pitch), new Dynamic(Roll));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((EulerAngles)b);
        // Implemented concept functions and type functions
        public Quaternion Quaternion { get {
            var cy = this.Yaw.Half.Cos;
            var sy = this.Yaw.Half.Sin;
            var cp = this.Pitch.Half.Cos;
            var sp = this.Pitch.Half.Sin;
            var cr = this.Roll.Half.Cos;
            var sr = this.Roll.Half.Sin;
            return sr.Multiply(cp.Multiply(cy)).Subtract(cr.Multiply(sp.Multiply(sy))).Tuple4(cr.Multiply(sp.Multiply(cy)).Add(sr.Multiply(cp.Multiply(sy))), cr.Multiply(cp.Multiply(sy)).Subtract(sr.Multiply(sp.Multiply(cy))), cr.Multiply(cp.Multiply(cy)).Add(sr.Multiply(sp.Multiply(sy))));
        }
         } public static implicit operator Quaternion(EulerAngles e) => e.Quaternion;
        public Vector3D Transform(Vector3D v) => this.Quaternion.Transform(v);
        public Vector3D TransformNormal(Vector3D v) => this.Quaternion.TransformNormal(v);
        public Matrix4x4 Matrix => this.Quaternion.Matrix;
        public IArray<EulerAngles> Repeat(Integer n){
            var _var699 = this;
            return n.MapRange((i) => _var699);
        }
        public Boolean Equals(EulerAngles b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(EulerAngles a, EulerAngles b) => a.Equals(b);
        public Boolean NotEquals(EulerAngles b) => this.Equals(b).Not;
        public static Boolean operator !=(EulerAngles a, EulerAngles b) => a.NotEquals(b);
        public Matrix4x4 Matrix4x4 => this.Matrix;
        public static implicit operator Matrix4x4(EulerAngles t) => t.Matrix4x4;
        // Unimplemented concept functions
    }
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public readonly partial struct Rotation3D: IValue<Rotation3D>, ITransform3D
    {
        [DataMember] public readonly Quaternion Quaternion;
        public Rotation3D WithQuaternion(Quaternion quaternion) => new Rotation3D(quaternion);
        public Rotation3D(Quaternion quaternion) => (Quaternion) = (quaternion);
        public static Rotation3D Default = new Rotation3D();
        public static Rotation3D New(Quaternion quaternion) => new Rotation3D(quaternion);
        public Plato.SinglePrecision.Rotation3D ChangePrecision() => (Quaternion.ChangePrecision());
        public static implicit operator Plato.SinglePrecision.Rotation3D(Rotation3D self) => self.ChangePrecision();
        public static implicit operator Quaternion(Rotation3D self) => self.Quaternion;
        public static implicit operator Rotation3D(Quaternion value) => new Rotation3D(value);
        public override bool Equals(object obj) { if (!(obj is Rotation3D)) return false; var other = (Rotation3D)obj; return Quaternion.Equals(other.Quaternion); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Quaternion);
        public override string ToString() => $"{{ \"Quaternion\" = {Quaternion} }}";
        public static implicit operator Dynamic(Rotation3D self) => new Dynamic(self);
        public static implicit operator Rotation3D(Dynamic value) => value.As<Rotation3D>();
        public String TypeName => "Rotation3D";
        public IArray<String> FieldNames => Intrinsics.MakeArray<String>((String)"Quaternion");
        public IArray<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Quaternion));
        Boolean IEquatable.Equals(IEquatable b) => this.Equals((Rotation3D)b);
        // Implemented concept functions and type functions
        public Vector3D Transform(Vector3D v) => this.Quaternion.Transform(v);
        public Vector3D TransformNormal(Vector3D v) => this.Quaternion.TransformNormal(v);
        public Matrix4x4 Matrix => this.Quaternion.Matrix;
        public IArray<Rotation3D> Repeat(Integer n){
            var _var700 = this;
            return n.MapRange((i) => _var700);
        }
        public Boolean Equals(Rotation3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator ==(Rotation3D a, Rotation3D b) => a.Equals(b);
        public Boolean NotEquals(Rotation3D b) => this.Equals(b).Not;
        public static Boolean operator !=(Rotation3D a, Rotation3D b) => a.NotEquals(b);
        public Matrix4x4 Matrix4x4 => this.Matrix;
        public static implicit operator Matrix4x4(Rotation3D t) => t.Matrix4x4;
        // Unimplemented concept functions
    }
    public static class Constants
    {
        public static Vector2D XAxis2D => ((Integer)1).Tuple2(((Integer)0));
        public static Vector2D YAxis2D => ((Integer)0).Tuple2(((Integer)1));
        public static Vector3D XAxis3D => ((Integer)1).Tuple3(((Integer)0), ((Integer)0));
        public static Vector3D YAxis3D => ((Integer)0).Tuple3(((Integer)1), ((Integer)0));
        public static Vector3D ZAxis3D => ((Integer)0).Tuple3(((Integer)0), ((Integer)1));
        public static NumberInterval UnitInterval => ((Number)0).Tuple2(((Number)1));
        public static Number Pi => ((Number)3.1415926535897);
        public static Number E => ((Number)2.718281828459);
        public static Number TwoPi => Constants.Pi.Twice;
        public static Number HalfPi => Constants.Pi.Half;
        public static Number Epsilon => ((Number)1E-15);
        public static Circle UnitCircle => ((Integer)0).Tuple2(((Integer)0)).Tuple2(((Integer)1));
        public static Number FeetPerMeter => ((Number)3.280839895);
        public static Number FeetPerMile => ((Integer)5280);
        public static Number MetersPerLightyear => ((Number)9460730472580000);
        public static Number MetersPerAU => ((Number)149597870691);
        public static Number DaltonPerKilogram => ((Number)1.66053E-27);
        public static Number PoundPerKilogram => ((Number)0.45359237);
        public static Number PoundPerTon => ((Integer)2000);
        public static Number KilogramPerSolarMass => ((Number)1.9889200011446E+30);
        public static Number JulianYearSeconds => ((Integer)31557600);
        public static Number GregorianYearDays => ((Number)365.2425);
        public static Number RadiansPerDegree => Constants.Pi.Divide(((Number)180));
        public static Number DegreesPerRadian => ((Number)180).Divide(Constants.Pi);
        public static Number MinNumber => Intrinsics.MinNumber;
        public static Number MaxNumber => Intrinsics.MaxNumber;
    }
    public static class Extensions
    {
        public static IArray<Integer> Indices<T>(this IArray<T> xs) => xs.Count.Range;
        public static Integer Count<T>(this IArray2D<T> xs) => xs.NumRows.Multiply(xs.NumColumns);
        public static Integer Count<T>(this IArray3D<T> xs) => xs.NumRows.Multiply(xs.NumColumns.Multiply(xs.NumLayers));
        public static T At<T>(this IArray2D<T> xs, Integer i) => xs.At(i.Modulo(xs.NumColumns), i.Divide(xs.NumColumns));
        public static T At<T>(this IArray3D<T> xs, Integer i) => xs.At(i.Modulo(xs.NumColumns), i.Divide(xs.NumColumns), i.Divide(xs.NumLayers));
        public static Boolean IsEmpty<T>(this IArray<T> xs) => xs.Count.Equals(((Integer)0));
        public static T First<T>(this IArray<T> xs) => xs.At(((Integer)0));
        public static T Last<T>(this IArray<T> xs) => xs.At(xs.Count.Subtract(((Integer)1)));
        public static T Middle<T>(this IArray<T> xs, Integer n) => xs.At(xs.Count.Divide(((Integer)2)));
        public static IArray<T> Slice<T>(this IArray<T> xs, Integer from, Integer to) => xs.Subarray(from, to.Subtract(from));
        public static IArray<IArray<T>> Slices<T>(this IArray<T> xs, Integer size){
            var _var702 = size;
            {
                var _var701 = xs;
                return xs.Count.Divide(size).MapRange((i) => _var701.NthSlice(i, _var702));
            }
        }
        public static IArray<T> NthSlice<T>(this IArray<T> xs, Integer n, Integer size) => xs.Subarray(n.Multiply(size), size);
        public static IArray<T> Subarray<T>(this IArray<T> xs, Integer from, Integer count){
            var _var704 = from;
            {
                var _var703 = xs;
                return count.MapRange((i) => _var703.At(i.Add(_var704)));
            }
        }
        public static IArray<T> Skip<T>(this IArray<T> xs, Integer n) => xs.Subarray(n, xs.Count.Subtract(n));
        public static IArray<T> Take<T>(this IArray<T> xs, Integer n) => xs.Subarray(((Integer)0), n);
        public static IArray<T> TakeLast<T>(this IArray<T> xs, Integer n) => xs.Skip(xs.Count.Subtract(((Integer)1)));
        public static IArray<T> Drop<T>(this IArray<T> xs, Integer n) => xs.Take(xs.Count.Subtract(n));
        public static IArray<T> Trim<T>(this IArray<T> xs, Integer first, Integer last) => xs.Skip(first).Drop(last);
        public static IArray<T> Rest<T>(this IArray<T> xs) => xs.Skip(((Integer)1));
        public static TR Reduce<T1, TR>(this IArray<T1> xs, TR acc, System.Func<TR, T1, TR> f){
            var r = acc;
            {
                var i = ((Integer)0);
                while (i.LessThan(xs.Count))
                {
                    r = f.Invoke(r, xs.At(i));
                    i = i.Add(((Integer)1));
                }

            }
            return r;
        }
        public static Boolean All<T0>(this IArray<T0> xs, System.Func<T0, Boolean> f){
            {
                var i = ((Integer)0);
                while (i.LessThan(xs.Count))
                {
                    if (f.Invoke(xs.At(i)).Not)
                    return ((Boolean)false);
                    i = i.Add(((Integer)1));
                }

            }
            return ((Boolean)true);
        }
        public static Boolean Any<T0>(this IArray<T0> xs, System.Func<T0, Boolean> f){
            {
                var i = ((Integer)0);
                while (i.LessThan(xs.Count))
                {
                    if (f.Invoke(xs.At(i)))
                    return ((Boolean)true);
                    i = i.Add(((Integer)1));
                }

            }
            return ((Boolean)false);
        }
        public static IArray<TR> Map<T0, TR>(this IArray<T0> xs, System.Func<T0, TR> f){
            var _var706 = xs;
            {
                var _var705 = f;
                return xs.Count.MapRange((i) => _var705.Invoke(_var706.At(i)));
            }
        }
        public static IArray<TR> Zip<T0, T1, TR>(this IArray<T0> xs, IArray<T1> ys, System.Func<T0, T1, TR> f){
            var _var709 = ys;
            {
                var _var708 = xs;
                {
                    var _var707 = f;
                    return xs.Count.Lesser(ys.Count).MapRange((i) => _var707.Invoke(_var708.At(i), _var709.At(i)));
                }
            }
        }
        public static IArray<TR> Zip<T0, T1, T2, TR>(this IArray<T0> xs, IArray<T1> ys, IArray<T2> zs, System.Func<T0, T1, T2, TR> f){
            var _var713 = zs;
            {
                var _var712 = ys;
                {
                    var _var711 = xs;
                    {
                        var _var710 = f;
                        return xs.Count.Lesser(ys.Count).Lesser(zs.Count).MapRange((i) => _var710.Invoke(_var711.At(i), _var712.At(i), _var713.At(i)));
                    }
                }
            }
        }
        public static T ModuloAt<T>(this IArray<T> xs, Integer n) => xs.At(n.Modulo(xs.Count));
        public static IArray<T> Shift<T>(this IArray<T> xs, Integer n){
            var _var714 = xs;
            return xs.Count.MapRange((i) => _var714.ModuloAt(i));
        }
        public static IArray<TR> WithNext<T1, TR>(this IArray<T1> xs, System.Func<T1, T1, TR> f) => xs.Drop(((Integer)1)).Zip(xs.Skip(((Integer)1)), f);
        public static IArray<TR> WithNextAndBeginning<T1, TR>(this IArray<T1> xs, System.Func<T1, T1, TR> f) => xs.Zip(xs.Shift(((Integer)1)), f);
        public static IArray<TR> WithNext<T1, TR>(this IArray<T1> xs, System.Func<T1, T1, TR> f, Boolean connect) => connect ? xs.WithNextAndBeginning(f) : xs.WithNext(f);
        public static IArray<T> EveryNth<T>(this IArray<T> self, Integer n){
            var _var716 = n;
            {
                var _var715 = self;
                return self.Indices().Map((i) => _var715.ModuloAt(i.Multiply(_var716)));
            }
        }
        public static IArray2D<TR> CartesianProduct<T0, T1, TR>(this IArray<T0> columns, IArray<T1> rows, System.Func<T0, T1, TR> func){
            var _var719 = rows;
            {
                var _var718 = columns;
                {
                    var _var717 = func;
                    return columns.Count.MakeArray2D(rows.Count, (i, j) => _var717.Invoke(_var718.At(i), _var719.At(j)));
                }
            }
        }
        public static IArray<T> Reverse<T>(this IArray<T> self){
            var _var721 = self;
            {
                var _var720 = self;
                return self.Indices().Map((i) => _var720.At(_var721.Count.Subtract(((Integer)1)).Subtract(i)));
            }
        }
        public static IArray<T> Concat<T>(this IArray<T> xs, IArray<T> ys){
            var _var725 = xs;
            {
                var _var724 = ys;
                {
                    var _var723 = xs;
                    {
                        var _var722 = xs;
                        return xs.Count.Add(ys.Count).MapRange((i) => i.LessThan(_var722.Count) ? _var723.At(i) : _var724.At(i.Subtract(_var725.Count)));
                    }
                }
            }
        }
        public static IArray<T> Prepend<T>(this IArray<T> self, T value){
            var _var727 = self;
            {
                var _var726 = value;
                return self.Count.Add(((Integer)1)).MapRange((i) => i.Equals(((Integer)0)) ? _var726 : _var727.At(i.Subtract(((Integer)1))));
            }
        }
        public static IArray<T> Append<T>(this IArray<T> self, T value){
            var _var729 = self;
            {
                var _var728 = value;
                return self.Count.Add(((Integer)1)).MapRange((i) => i.Equals(((Integer)0)) ? _var728 : _var729.At(i.Subtract(((Integer)1))));
            }
        }
        public static IArray<T> PrependAndAppend<T>(this IArray<T> self, T before, T after) => self.Prepend(before).Append(after);
        public static IArray2D<TR> Map<T0, TR>(this IArray2D<T0> xs, System.Func<T0, TR> f){
            var _var731 = xs;
            {
                var _var730 = f;
                return xs.NumColumns.MakeArray2D(xs.NumRows, (a, b) => _var730.Invoke(_var731.At(a, b)));
            }
        }
        public static Bounds3D Bounds(this IArray<Vector3D> xs) => xs.Reduce(Bounds3D.Empty, (a, b) => a.Include(b));
        public static IArray2D<Integer4> AllQuadFaceIndices<T>(this IArray2D<T> xs, Boolean closedX, Boolean closedY) => xs.NumColumns.AllQuadFaceIndices(xs.NumRows, closedX, closedY);
        public static IArray<Vector2D> Points(this IArray<Line2D> xs) => xs.FlatMap((x) => x);
        public static IArray<Vector3D> Points(this IArray<Line3D> xs) => xs.FlatMap((x) => x);
        public static IArray<Vector2D> Points(this IArray<Triangle2D> xs) => xs.FlatMap((x) => x);
        public static IArray<Vector3D> Points(this IArray<Triangle3D> xs) => xs.FlatMap((x) => x);
        public static IArray<Vector2D> Points(this IArray<Quad2D> xs) => xs.FlatMap((x) => x);
        public static IArray<Vector3D> Points(this IArray<Quad3D> xs) => xs.FlatMap((x) => x);
        public static IArray<Line2D> Lines(this IArray<Line2D> xs) => xs;
        public static IArray<Line3D> Lines(this IArray<Line3D> xs) => xs;
        public static IArray<Line2D> Lines(this IArray<Triangle2D> xs) => xs.FlatMap((x) => x.Lines);
        public static IArray<Line3D> Lines(this IArray<Triangle3D> xs) => xs.FlatMap((x) => x.Lines);
        public static IArray<Line2D> Lines(this IArray<Quad2D> xs) => xs.FlatMap((x) => x.Lines);
        public static IArray<Line3D> Lines(this IArray<Quad3D> xs) => xs.FlatMap((x) => x.Lines);
        public static IArray<Triangle2D> Triangles(this IArray<Triangle2D> xs) => xs;
        public static IArray<Triangle3D> Triangles(this IArray<Triangle3D> xs) => xs;
        public static IArray<Triangle2D> Triangles(this IArray<Quad2D> xs) => xs.FlatMap((x) => x.Triangles);
        public static IArray<Triangle3D> Triangles(this IArray<Quad3D> xs) => xs.FlatMap((x) => x.Triangles);
        public static IArray<Line2D> ToLines(this IArray<Vector2D> xs, IArray<Vector2D> ys) => xs.Zip(ys, (a, b) => Line2D.New(a, b));
        public static IArray<Line3D> ToLines(this IArray<Vector3D> xs, IArray<Vector3D> ys) => xs.Zip(ys, (a, b) => Line3D.New(a, b));
        public static IArray<T> FlatMap<T0, T>(this IArray<T0> xs, System.Func<T0, IArray<T>> f) => Intrinsics.FlatMap(xs, f);
    }
}
