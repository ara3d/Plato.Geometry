namespace Plato.DoublePrecision
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
        public static Number MaxNumber => double.MinValue;

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
        public static Array<T> Map<T>(this Integer x, System.Func<Integer, T> f) => new LazyArray<T>(x, f);
        public static Array<T> MakeArray<T>(params T[] args) => new PrimitiveArray<T>(args);

        public static Boolean And(Boolean x, Boolean y) => x.Value && y.Value;
        public static Boolean Or(Boolean x, Boolean y) => x.Value || y.Value;
        public static Boolean Not(Boolean x) => !x.Value;

        public static Number ToNumber(Integer x) => x.Value;

        public static Character At(String x, Integer n) => x.Value[n];
        public static Integer Count(String x) => x.Value.Length;

        public static string MakeString(string typeName, Array<String> fieldNames, Array<Dynamic> fieldValues)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append($"{{ _type=\"{typeName}\" ");
            for (var i = 0; i < fieldNames.Count; i++)
                sb.Append(", ").Append(fieldNames.At(i).Value).Append(" = ").Append(fieldValues.At(i).Value);
            sb.Append(" }");
            return sb.ToString();
        }

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

        // TODO: I need to implemnt this. 
        public static Dynamic New(Type type, Array<Any> args) 
            => throw new System.NotImplementedException("");

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
    }
    
    public readonly partial struct Number
    {
    }

    public readonly struct LazyArray<T> : Array<T>
    {
        private readonly System.Func<Integer, T> _func;
        public Integer Count { get; }
        public T At(Integer n) => _func(n);
        public T this[Integer n] => _func(n);
        public LazyArray(Integer count, System.Func<Integer, T> func)
        {
            Count = count;
            _func = func;
        }
    }

   public readonly struct LazyArray2D<T> : Array2D<T>
    {
        private readonly System.Func<Integer, Integer, T> _func;
        public Integer Count => ColumnCount * RowCount;
        public Integer ColumnCount { get; }
        public Integer RowCount { get; }
        public T At(Integer n) => At(n % ColumnCount, n / ColumnCount);
        public T this[Integer n] => At(n % ColumnCount, n / ColumnCount);
        public T At(Integer col, Integer row) => _func(col, row);
        public T this[Integer col, Integer row] => _func(col, row);
        public LazyArray2D(Integer numCols, Integer numRows, System.Func<Integer, Integer, T> func)
        {
            ColumnCount = numCols;
            RowCount = numRows;
            _func = func;
        }
    }

    public readonly struct PrimitiveArray<T> : Array<T>
    {
        private readonly T[] _data;
        public Integer Count => _data.Length;
        public T At(Integer n) => _data[n];
        public T this[Integer n] => _data[n];
        public PrimitiveArray(T[] data) => _data = data;
        public static Array<T> Default = new PrimitiveArray<T>(System.Array.Empty<T>());
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
         public static implicit operator Number(Integer self) => self.Value;
    }

    public readonly partial struct Character
    {
        public static implicit operator Number(Character self) => self.Value;
    }

    public readonly partial struct Dynamic
    {
        public readonly object Value;
        public Dynamic WithValue(object value) => new Dynamic(value);
        public Dynamic(object value) => (Value) = (value);
        public static Dynamic Default = new Dynamic();
        public static Dynamic New(object value) => new Dynamic(value);
        public String TypeName => "Dynamic";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>("Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
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

    public interface Array<T>
    {
        Integer Count { get; }
        T At(Integer n);
        T this[Integer n] { get; }
    }
    public interface Array2D<T>: Array<T>
    {
        Integer RowCount { get; }
        Integer ColumnCount { get; }
        T At(Integer column, Integer row);
        T this[Integer column, Integer row] { get; }
    }
    public interface Array3D<T>: Array<T>
    {
        Integer RowCount { get; }
        Integer ColumnCount { get; }
        Integer LayerCount { get; }
        T At(Integer column, Integer row, Integer layer);
        T this[Integer column, Integer row, Integer layer] { get; }
    }
    public interface Any
    {
        Array<String> FieldNames { get; }
        Array<Dynamic> FieldValues { get; }
        String TypeName { get; }
    }
    public interface Value<Self>: Any, Equatable<Self>
    {
    }
    public interface Numerical<Self>: Value<Self>, ScalarArithmetic<Self>, Additive<Self>
    {
        Array<Number> Components { get; }
        Self FromComponents(Array<Number> xs);
    }
    public interface NumberLike<Self>: Numerical<Self>, Orderable<Self>
    {
        Number ToNumber { get; }
        Self FromNumber(Number n);
    }
    public interface Real<Self>: NumberLike<Self>, Algebraic<Self>, Arithmetic<Self>
    {
    }
    public interface WholeNumber<Self>: Value<Self>, Orderable<Self>, Arithmetic<Self>
    {
    }
    public interface Measure<Self>: NumberLike<Self>, Additive<Self>
    {
    }
    public interface Vector<Self>: Numerical<Self>, Arithmetic<Self>, Array<Number>
    {
    }
    public interface Coordinate<Self>: Value<Self>
    {
    }
    public interface Orderable<Self>: Equatable<Self>
    {
        Boolean LessThanOrEquals(Self y);
    }
    public interface Equatable<Self>
    {
        Boolean Equals(Self b);
    }
    public interface Additive<Self>
    {
        Self Add(Self b);
        Self Subtract(Self b);
        Self Negative { get; }
    }
    public interface ScalarArithmetic<Self>
    {
        Self Modulo(Number other);
        Self Divide(Number other);
        Self Multiply(Number other);
    }
    public interface Multiplicative<Self>
    {
        Self Multiply(Self b);
    }
    public interface Invertible<Self>
    {
        Self Inverse { get; }
    }
    public interface MultiplicativeWithInverse<Self>: Multiplicative<Self>, Invertible<Self>
    {
    }
    public interface Algebraic<Self>: Additive<Self>, MultiplicativeWithInverse<Self>, ScalarArithmetic<Self>
    {
    }
    public interface Divisible<Self>
    {
        Self Divide(Self b);
    }
    public interface ModuloOperation<Self>
    {
        Self Modulo(Self b);
    }
    public interface Arithmetic<Self>: Additive<Self>, Multiplicative<Self>, Divisible<Self>, ModuloOperation<Self>
    {
    }
    public interface BooleanOperations<Self>
    {
        Self And(Self b);
        Self Or(Self b);
        Self Not { get; }
    }
    public interface Interval<Self, T>: Equatable<Self>, Value<Self>, Array<T>
    {
        T Min { get; }
        T Max { get; }
        T Size { get; }
    }
    public interface Bounded2D
    {
        Bounds2D Bounds { get; }
    }
    public interface Bounded3D
    {
        Bounds3D Bounds { get; }
    }
    public interface Transformable2D<Self>
    {
        Self Transform(Matrix3x3 matrix);
    }
    public interface Transformable3D<Self>
    {
        Self Transform(Matrix4x4 matrix);
    }
    public interface Deformable2D<Self>
    {
        Self Deform(System.Func<Vector2D, Vector2D> f);
    }
    public interface OpenClosedShape
    {
        Boolean Closed { get; }
    }
    public interface Deformable3D<Self>: Transformable3D<Self>
    {
        Self Deform(System.Func<Vector3D, Vector3D> f);
    }
    public interface Geometry
    {
    }
    public interface Geometry2D: Geometry
    {
    }
    public interface Geometry3D: Geometry
    {
    }
    public interface Shape2D: Geometry2D
    {
    }
    public interface Shape3D: Geometry3D
    {
    }
    public interface OpenShape2D: Geometry2D, OpenClosedShape
    {
    }
    public interface ClosedShape2D: Geometry2D, OpenClosedShape
    {
    }
    public interface OpenShape3D: Geometry3D, OpenClosedShape
    {
    }
    public interface ClosedShape3D: Geometry3D, OpenClosedShape
    {
    }
    public interface Procedural<TDomain, TRange>
    {
        TRange Eval(TDomain amount);
    }
    public interface Curve<TRange>: Procedural<Number, TRange>, OpenClosedShape
    {
    }
    public interface Curve1D: Curve<Number>
    {
    }
    public interface Curve2D: Geometry2D, Curve<Vector2D>
    {
    }
    public interface Curve3D: Geometry3D, Curve<Vector3D>
    {
    }
    public interface Surface: Geometry3D
    {
    }
    public interface ProceduralSurface: Procedural<Vector2D, Vector3D>, Surface
    {
        Boolean PeriodicX { get; }
        Boolean PeriodicY { get; }
    }
    public interface ExplicitSurface: Procedural<Vector2D, Number>, Surface
    {
    }
    public interface DistanceField<TDomain>: Procedural<TDomain, Number>
    {
    }
    public interface Field2D<T>: Geometry2D, Procedural<Vector2D, T>
    {
    }
    public interface Field3D<T>: Geometry3D, Procedural<Vector3D, T>
    {
    }
    public interface ScalarField2D: Field2D<Number>
    {
    }
    public interface ScalarField3D: Field3D<Number>
    {
    }
    public interface DistanceField2D: ScalarField2D
    {
    }
    public interface DistanceField3D: ScalarField3D
    {
    }
    public interface Vector3Field2D: Field2D<Vector3D>
    {
    }
    public interface Vector4Field2D: Field2D<Vector4D>
    {
    }
    public interface Vector2Field3D: Field3D<Vector2D>
    {
    }
    public interface Vector3Field3D: Field3D<Vector3D>
    {
    }
    public interface Vector4Field3D: Field3D<Vector4D>
    {
    }
    public interface ImplicitProcedural<TDomain>
    {
        Boolean Eval(TDomain amount, TDomain epsilon);
    }
    public interface ImplicitSurface: Surface, ImplicitProcedural<Vector3D>
    {
    }
    public interface ImplicitCurve2D: Geometry2D, ImplicitProcedural<Vector2D>
    {
    }
    public interface ImplicitVolume: Geometry3D, ImplicitProcedural<Vector3D>
    {
    }
    public interface Points2D: Geometry2D
    {
        Array<Vector2D> Points { get; }
    }
    public interface Points3D: Geometry3D
    {
        Array<Vector3D> Points { get; }
    }
    public interface BezierPatch: Points3D, Surface, Array2D<Vector3D>
    {
    }
    public interface PolyhederalFace
    {
        Integer FaceIndex { get; }
        Array<Integer> VertexIndices { get; }
        Polyhedron Polyhedron { get; }
    }
    public interface Polyhedron: Surface, Points3D
    {
        Array<PolyhederalFace> Faces { get; }
    }
    public interface ConvexPolyhedron: Polyhedron
    {
    }
    public interface SolidPolyhedron: Polyhedron
    {
    }
    public interface Mesh<FaceType, VertexType>: Geometry3D
    {
        Array<FaceType> Faces { get; }
        Array<VertexType> Vertices { get; }
    }
    public interface Grid2D: Array2D<Vector2D>
    {
    }
    public interface QuadGrid: Array2D<Vector3D>
    {
        Boolean ClosedX { get; }
        Boolean ClosedY { get; }
    }
    public interface PolyLine2D: Points2D, OpenClosedShape
    {
    }
    public interface PolyLine3D: Points3D, OpenClosedShape
    {
    }
    public interface ClosedPolyLine2D: PolyLine2D, ClosedShape2D
    {
    }
    public interface ClosedPolyLine3D: PolyLine3D
    {
    }
    public interface Polygon2D: PolyLine2D
    {
    }
    public interface Polygon3D: PolyLine3D
    {
    }
    public readonly partial struct Transform2D: Value<Transform2D>
    {
        public readonly Vector2D Translation;
        public readonly Angle Rotation;
        public readonly Vector2D Scale;
        public Transform2D WithTranslation(Vector2D translation) => (translation, Rotation, Scale);
        public Transform2D WithRotation(Angle rotation) => (Translation, rotation, Scale);
        public Transform2D WithScale(Vector2D scale) => (Translation, Rotation, scale);
        public Transform2D(Vector2D translation, Angle rotation, Vector2D scale) => (Translation, Rotation, Scale) = (translation, rotation, scale);
        public static Transform2D Default = new Transform2D();
        public static Transform2D New(Vector2D translation, Angle rotation, Vector2D scale) => new Transform2D(translation, rotation, scale);
        public static implicit operator (Vector2D, Angle, Vector2D)(Transform2D self) => (self.Translation, self.Rotation, self.Scale);
        public static implicit operator Transform2D((Vector2D, Angle, Vector2D) value) => new Transform2D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D translation, out Angle rotation, out Vector2D scale) { translation = Translation; rotation = Rotation; scale = Scale; }
        public override bool Equals(object obj) { if (!(obj is Transform2D)) return false; var other = (Transform2D)obj; return Translation.Equals(other.Translation) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Translation, Rotation, Scale);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Transform2D self) => new Dynamic(self);
        public static implicit operator Transform2D(Dynamic value) => value.As<Transform2D>();
        public String TypeName => "Transform2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Translation", (String)"Rotation", (String)"Scale");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Translation), new Dynamic(Rotation), new Dynamic(Scale));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Transform2D a, Transform2D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Transform2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Transform2D a, Transform2D b) => a.Equals(b).Not;
        public Boolean NotEquals(Transform2D b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Pose2D: Value<Pose2D>
    {
        public readonly Vector2D Position;
        public readonly Angle Orientation;
        public Pose2D WithPosition(Vector2D position) => (position, Orientation);
        public Pose2D WithOrientation(Angle orientation) => (Position, orientation);
        public Pose2D(Vector2D position, Angle orientation) => (Position, Orientation) = (position, orientation);
        public static Pose2D Default = new Pose2D();
        public static Pose2D New(Vector2D position, Angle orientation) => new Pose2D(position, orientation);
        public static implicit operator (Vector2D, Angle)(Pose2D self) => (self.Position, self.Orientation);
        public static implicit operator Pose2D((Vector2D, Angle) value) => new Pose2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D position, out Angle orientation) { position = Position; orientation = Orientation; }
        public override bool Equals(object obj) { if (!(obj is Pose2D)) return false; var other = (Pose2D)obj; return Position.Equals(other.Position) && Orientation.Equals(other.Orientation); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Position, Orientation);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Pose2D self) => new Dynamic(self);
        public static implicit operator Pose2D(Dynamic value) => value.As<Pose2D>();
        public String TypeName => "Pose2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Position", (String)"Orientation");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Position), new Dynamic(Orientation));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Pose2D a, Pose2D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Pose2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Pose2D a, Pose2D b) => a.Equals(b).Not;
        public Boolean NotEquals(Pose2D b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Bounds2D: Interval<Bounds2D, Vector2D>
    {
        public readonly Vector2D Min;
        public readonly Vector2D Max;
        public Bounds2D WithMin(Vector2D min) => (min, Max);
        public Bounds2D WithMax(Vector2D max) => (Min, max);
        public Bounds2D(Vector2D min, Vector2D max) => (Min, Max) = (min, max);
        public static Bounds2D Default = new Bounds2D();
        public static Bounds2D New(Vector2D min, Vector2D max) => new Bounds2D(min, max);
        public static implicit operator (Vector2D, Vector2D)(Bounds2D self) => (self.Min, self.Max);
        public static implicit operator Bounds2D((Vector2D, Vector2D) value) => new Bounds2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D min, out Vector2D max) { min = Min; max = Max; }
        public override bool Equals(object obj) { if (!(obj is Bounds2D)) return false; var other = (Bounds2D)obj; return Min.Equals(other.Min) && Max.Equals(other.Max); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Min, Max);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Bounds2D self) => new Dynamic(self);
        public static implicit operator Bounds2D(Dynamic value) => value.As<Bounds2D>();
        public String TypeName => "Bounds2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Min", (String)"Max");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Min), new Dynamic(Max));
        Vector2D Interval<Bounds2D, Vector2D>.Min => Min;
        Vector2D Interval<Bounds2D, Vector2D>.Max => Max;
        // Implemented concept functions and type functions
        public Vector2D Size => this.Max.Subtract(this.Min);
        public Vector2D Lerp(Number amount) => this.Min.Lerp(this.Max, amount);
        public Bounds2D Reverse => this.Max.Tuple2(this.Min);
        public Vector2D Center => this.Lerp(((Number)0.5));
        public Boolean Contains(Vector2D value) => value.Between(this.Min, this.Max);
        public Boolean Contains(Bounds2D y) => this.Contains(y.Min).And(this.Contains(y.Max));
        public Boolean Overlaps(Bounds2D y) => this.Contains(y.Min).Or(this.Contains(y.Max).Or(y.Contains(this.Min).Or(y.Contains(this.Max))));
        public Tuple2<Bounds2D, Bounds2D> SplitAt(Number t) => this.Left(t).Tuple2(this.Right(t));
        public Tuple2<Bounds2D, Bounds2D> Split => this.SplitAt(((Number)0.5));
        public Bounds2D Left(Number t) => this.Min.Tuple2(this.Lerp(t));
        public Bounds2D Right(Number t) => this.Lerp(t).Tuple2(this.Max);
        public Bounds2D MoveTo(Vector2D v) => v.Tuple2(v.Add(this.Size));
        public Bounds2D LeftHalf => this.Left(((Number)0.5));
        public Bounds2D RightHalf => this.Right(((Number)0.5));
        public Bounds2D Recenter(Vector2D c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public Bounds2D Clamp(Bounds2D y) => this.Clamp(y.Min).Tuple2(this.Clamp(y.Max));
        public Vector2D Clamp(Vector2D value) => value.Clamp(this.Min, this.Max);
        public static Boolean operator ==(Bounds2D a, Bounds2D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Bounds2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Bounds2D a, Bounds2D b) => a.Equals(b).Not;
        public Boolean NotEquals(Bounds2D b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 2;
        public Vector2D this[Integer n] => n == 0 ? Min : n == 1 ? Max : throw new System.IndexOutOfRangeException();
        public Vector2D At(Integer n) => n == 0 ? Min : n == 1 ? Max : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Ray2D: Value<Ray2D>
    {
        public readonly Vector2D Direction;
        public readonly Vector2D Origin;
        public Ray2D WithDirection(Vector2D direction) => (direction, Origin);
        public Ray2D WithOrigin(Vector2D origin) => (Direction, origin);
        public Ray2D(Vector2D direction, Vector2D origin) => (Direction, Origin) = (direction, origin);
        public static Ray2D Default = new Ray2D();
        public static Ray2D New(Vector2D direction, Vector2D origin) => new Ray2D(direction, origin);
        public static implicit operator (Vector2D, Vector2D)(Ray2D self) => (self.Direction, self.Origin);
        public static implicit operator Ray2D((Vector2D, Vector2D) value) => new Ray2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D direction, out Vector2D origin) { direction = Direction; origin = Origin; }
        public override bool Equals(object obj) { if (!(obj is Ray2D)) return false; var other = (Ray2D)obj; return Direction.Equals(other.Direction) && Origin.Equals(other.Origin); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Direction, Origin);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Ray2D self) => new Dynamic(self);
        public static implicit operator Ray2D(Dynamic value) => value.As<Ray2D>();
        public String TypeName => "Ray2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Direction", (String)"Origin");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Direction), new Dynamic(Origin));
        // Implemented concept functions and type functions
        public static implicit operator Ray3D(Ray2D ray) => ray.Origin.Tuple2(ray.Direction);
        public Ray3D Ray3D => this.Origin.Tuple2(this.Direction);
        public static Boolean operator ==(Ray2D a, Ray2D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Ray2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Ray2D a, Ray2D b) => a.Equals(b).Not;
        public Boolean NotEquals(Ray2D b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Triangle2D: Value<Triangle2D>, Array<Vector2D>
    {
        public readonly Vector2D A;
        public readonly Vector2D B;
        public readonly Vector2D C;
        public Triangle2D WithA(Vector2D a) => (a, B, C);
        public Triangle2D WithB(Vector2D b) => (A, b, C);
        public Triangle2D WithC(Vector2D c) => (A, B, c);
        public Triangle2D(Vector2D a, Vector2D b, Vector2D c) => (A, B, C) = (a, b, c);
        public static Triangle2D Default = new Triangle2D();
        public static Triangle2D New(Vector2D a, Vector2D b, Vector2D c) => new Triangle2D(a, b, c);
        public static implicit operator (Vector2D, Vector2D, Vector2D)(Triangle2D self) => (self.A, self.B, self.C);
        public static implicit operator Triangle2D((Vector2D, Vector2D, Vector2D) value) => new Triangle2D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D a, out Vector2D b, out Vector2D c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is Triangle2D)) return false; var other = (Triangle2D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Triangle2D self) => new Dynamic(self);
        public static implicit operator Triangle2D(Dynamic value) => value.As<Triangle2D>();
        public String TypeName => "Triangle2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        // Implemented concept functions and type functions
        public Array<Vector2D> Points => Intrinsics.MakeArray(this.A, this.B, this.C);
        public Number Area => this.A.X.Multiply(this.C.Y.Subtract(this.B.Y)).Add(this.B.X.Multiply(this.A.Y.Subtract(this.C.Y)).Add(this.C.X.Multiply(this.B.Y.Subtract(this.A.Y)))).Half;
        public Triangle2D Flip => this.C.Tuple3(this.B, this.A);
        public Vector2D Center => this.A.Add(this.B.Add(this.C)).Divide(((Number)3));
        public Vector2D Barycentric(Vector2D uv) => this.A.Barycentric(this.B, this.C, uv);
        public static Boolean operator ==(Triangle2D a, Triangle2D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Triangle2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Triangle2D a, Triangle2D b) => a.Equals(b).Not;
        public Boolean NotEquals(Triangle2D b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Quad2D: Value<Quad2D>, Array<Vector2D>
    {
        public readonly Vector2D A;
        public readonly Vector2D B;
        public readonly Vector2D C;
        public readonly Vector2D D;
        public Quad2D WithA(Vector2D a) => (a, B, C, D);
        public Quad2D WithB(Vector2D b) => (A, b, C, D);
        public Quad2D WithC(Vector2D c) => (A, B, c, D);
        public Quad2D WithD(Vector2D d) => (A, B, C, d);
        public Quad2D(Vector2D a, Vector2D b, Vector2D c, Vector2D d) => (A, B, C, D) = (a, b, c, d);
        public static Quad2D Default = new Quad2D();
        public static Quad2D New(Vector2D a, Vector2D b, Vector2D c, Vector2D d) => new Quad2D(a, b, c, d);
        public static implicit operator (Vector2D, Vector2D, Vector2D, Vector2D)(Quad2D self) => (self.A, self.B, self.C, self.D);
        public static implicit operator Quad2D((Vector2D, Vector2D, Vector2D, Vector2D) value) => new Quad2D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector2D a, out Vector2D b, out Vector2D c, out Vector2D d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is Quad2D)) return false; var other = (Quad2D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Quad2D self) => new Dynamic(self);
        public static implicit operator Quad2D(Dynamic value) => value.As<Quad2D>();
        public String TypeName => "Quad2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Quad2D a, Quad2D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Quad2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Quad2D a, Quad2D b) => a.Equals(b).Not;
        public Boolean NotEquals(Quad2D b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Line2D: PolyLine2D, Array<Vector2D>
    {
        public readonly Vector2D A;
        public readonly Vector2D B;
        public Line2D WithA(Vector2D a) => (a, B);
        public Line2D WithB(Vector2D b) => (A, b);
        public Line2D(Vector2D a, Vector2D b) => (A, B) = (a, b);
        public static Line2D Default = new Line2D();
        public static Line2D New(Vector2D a, Vector2D b) => new Line2D(a, b);
        public static implicit operator (Vector2D, Vector2D)(Line2D self) => (self.A, self.B);
        public static implicit operator Line2D((Vector2D, Vector2D) value) => new Line2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D a, out Vector2D b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Line2D)) return false; var other = (Line2D)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Line2D self) => new Dynamic(self);
        public static implicit operator Line2D(Dynamic value) => value.As<Line2D>();
        public String TypeName => "Line2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)false);
        public Array<Vector2D> Points => this;
        public Number Length => this.B.Subtract(this.A).Length;
        public Vector2D Direction => this.B.Subtract(this.A);
        public static implicit operator Ray2D(Line2D x) => x.A.Tuple2(x.Direction);
        public Ray2D Ray2D => this.A.Tuple2(this.Direction);
        public static implicit operator Line3D(Line2D x) => x.A.Tuple2(x.B);
        public Line3D Line3D => this.A.Tuple2(this.B);
        // Unimplemented concept functions
        public Integer Count => 2;
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Circle: ClosedShape2D
    {
        public readonly Vector2D Center;
        public readonly Number Radius;
        public Circle WithCenter(Vector2D center) => (center, Radius);
        public Circle WithRadius(Number radius) => (Center, radius);
        public Circle(Vector2D center, Number radius) => (Center, Radius) = (center, radius);
        public static Circle Default = new Circle();
        public static Circle New(Vector2D center, Number radius) => new Circle(center, radius);
        public static implicit operator (Vector2D, Number)(Circle self) => (self.Center, self.Radius);
        public static implicit operator Circle((Vector2D, Number) value) => new Circle(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D center, out Number radius) { center = Center; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is Circle)) return false; var other = (Circle)obj; return Center.Equals(other.Center) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Radius);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Circle self) => new Dynamic(self);
        public static implicit operator Circle(Dynamic value) => value.As<Circle>();
        public String TypeName => "Circle";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Radius");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Radius));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    public readonly partial struct Lens: ClosedShape2D
    {
        public readonly Circle A;
        public readonly Circle B;
        public Lens WithA(Circle a) => (a, B);
        public Lens WithB(Circle b) => (A, b);
        public Lens(Circle a, Circle b) => (A, B) = (a, b);
        public static Lens Default = new Lens();
        public static Lens New(Circle a, Circle b) => new Lens(a, b);
        public static implicit operator (Circle, Circle)(Lens self) => (self.A, self.B);
        public static implicit operator Lens((Circle, Circle) value) => new Lens(value.Item1, value.Item2);
        public void Deconstruct(out Circle a, out Circle b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Lens)) return false; var other = (Lens)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Lens self) => new Dynamic(self);
        public static implicit operator Lens(Dynamic value) => value.As<Lens>();
        public String TypeName => "Lens";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    public readonly partial struct Rect2D: Polygon2D
    {
        public readonly Vector2D Center;
        public readonly Size2D Size;
        public Rect2D WithCenter(Vector2D center) => (center, Size);
        public Rect2D WithSize(Size2D size) => (Center, size);
        public Rect2D(Vector2D center, Size2D size) => (Center, Size) = (center, size);
        public static Rect2D Default = new Rect2D();
        public static Rect2D New(Vector2D center, Size2D size) => new Rect2D(center, size);
        public static implicit operator (Vector2D, Size2D)(Rect2D self) => (self.Center, self.Size);
        public static implicit operator Rect2D((Vector2D, Size2D) value) => new Rect2D(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D center, out Size2D size) { center = Center; size = Size; }
        public override bool Equals(object obj) { if (!(obj is Rect2D)) return false; var other = (Rect2D)obj; return Center.Equals(other.Center) && Size.Equals(other.Size); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Size);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Rect2D self) => new Dynamic(self);
        public static implicit operator Rect2D(Dynamic value) => value.As<Rect2D>();
        public String TypeName => "Rect2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Size");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Size));
        // Implemented concept functions and type functions
        public Number Width => this.Size.Width;
        public Number Height => this.Size.Height;
        public Number HalfWidth => this.Width.Half;
        public Number HalfHeight => this.Height.Half;
        public Number Top => this.Center.Y.Add(HalfHeight);
        public Number Bottom => this.Top.Add(this.Height);
        public Number Left => this.Center.X.Subtract(HalfWidth);
        public Number Right => this.Left.Add(this.Width);
        public Vector2D TopLeft => this.Left.Tuple2(this.Top);
        public Vector2D TopRight => this.Right.Tuple2(this.Top);
        public Vector2D BottomRight => this.Right.Tuple2(this.Bottom);
        public Vector2D BottomLeft => this.Left.Tuple2(this.Bottom);
        public Array<Vector2D> Points => Intrinsics.MakeArray(this.TopLeft, this.TopRight, this.BottomRight, this.BottomLeft);
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    public readonly partial struct Ellipse: Curve2D
    {
        public readonly Vector2D Center;
        public readonly Size2D Size;
        public Ellipse WithCenter(Vector2D center) => (center, Size);
        public Ellipse WithSize(Size2D size) => (Center, size);
        public Ellipse(Vector2D center, Size2D size) => (Center, Size) = (center, size);
        public static Ellipse Default = new Ellipse();
        public static Ellipse New(Vector2D center, Size2D size) => new Ellipse(center, size);
        public static implicit operator (Vector2D, Size2D)(Ellipse self) => (self.Center, self.Size);
        public static implicit operator Ellipse((Vector2D, Size2D) value) => new Ellipse(value.Item1, value.Item2);
        public void Deconstruct(out Vector2D center, out Size2D size) { center = Center; size = Size; }
        public override bool Equals(object obj) { if (!(obj is Ellipse)) return false; var other = (Ellipse)obj; return Center.Equals(other.Center) && Size.Equals(other.Size); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Size);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Ellipse self) => new Dynamic(self);
        public static implicit operator Ellipse(Dynamic value) => value.As<Ellipse>();
        public String TypeName => "Ellipse";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Size");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Size));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        public Vector2D Eval(Number t) => t.CircleFunction.Multiply(this.Size).Add(this.Center);
        // Unimplemented concept functions
    }
    public readonly partial struct Ring: ClosedShape2D
    {
        public readonly Vector2D Center;
        public readonly Number InnerRadius;
        public readonly Number OuterRadius;
        public Ring WithCenter(Vector2D center) => (center, InnerRadius, OuterRadius);
        public Ring WithInnerRadius(Number innerRadius) => (Center, innerRadius, OuterRadius);
        public Ring WithOuterRadius(Number outerRadius) => (Center, InnerRadius, outerRadius);
        public Ring(Vector2D center, Number innerRadius, Number outerRadius) => (Center, InnerRadius, OuterRadius) = (center, innerRadius, outerRadius);
        public static Ring Default = new Ring();
        public static Ring New(Vector2D center, Number innerRadius, Number outerRadius) => new Ring(center, innerRadius, outerRadius);
        public static implicit operator (Vector2D, Number, Number)(Ring self) => (self.Center, self.InnerRadius, self.OuterRadius);
        public static implicit operator Ring((Vector2D, Number, Number) value) => new Ring(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D center, out Number innerRadius, out Number outerRadius) { center = Center; innerRadius = InnerRadius; outerRadius = OuterRadius; }
        public override bool Equals(object obj) { if (!(obj is Ring)) return false; var other = (Ring)obj; return Center.Equals(other.Center) && InnerRadius.Equals(other.InnerRadius) && OuterRadius.Equals(other.OuterRadius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, InnerRadius, OuterRadius);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Ring self) => new Dynamic(self);
        public static implicit operator Ring(Dynamic value) => value.As<Ring>();
        public String TypeName => "Ring";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"InnerRadius", (String)"OuterRadius");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(InnerRadius), new Dynamic(OuterRadius));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    public readonly partial struct Arc: OpenShape2D
    {
        public readonly AnglePair Angles;
        public readonly Circle Circle;
        public Arc WithAngles(AnglePair angles) => (angles, Circle);
        public Arc WithCircle(Circle circle) => (Angles, circle);
        public Arc(AnglePair angles, Circle circle) => (Angles, Circle) = (angles, circle);
        public static Arc Default = new Arc();
        public static Arc New(AnglePair angles, Circle circle) => new Arc(angles, circle);
        public static implicit operator (AnglePair, Circle)(Arc self) => (self.Angles, self.Circle);
        public static implicit operator Arc((AnglePair, Circle) value) => new Arc(value.Item1, value.Item2);
        public void Deconstruct(out AnglePair angles, out Circle circle) { angles = Angles; circle = Circle; }
        public override bool Equals(object obj) { if (!(obj is Arc)) return false; var other = (Arc)obj; return Angles.Equals(other.Angles) && Circle.Equals(other.Circle); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Angles, Circle);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Arc self) => new Dynamic(self);
        public static implicit operator Arc(Dynamic value) => value.As<Arc>();
        public String TypeName => "Arc";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Angles", (String)"Circle");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Angles), new Dynamic(Circle));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)false);
        // Unimplemented concept functions
    }
    public readonly partial struct Sector: ClosedShape2D
    {
        public readonly Arc Arc;
        public Sector WithArc(Arc arc) => (arc);
        public Sector(Arc arc) => (Arc) = (arc);
        public static Sector Default = new Sector();
        public static Sector New(Arc arc) => new Sector(arc);
        public static implicit operator Arc(Sector self) => self.Arc;
        public static implicit operator Sector(Arc value) => new Sector(value);
        public override bool Equals(object obj) { if (!(obj is Sector)) return false; var other = (Sector)obj; return Arc.Equals(other.Arc); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Arc);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Sector self) => new Dynamic(self);
        public static implicit operator Sector(Dynamic value) => value.As<Sector>();
        public String TypeName => "Sector";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Arc");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Arc));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    public readonly partial struct Chord: ClosedShape2D
    {
        public readonly Arc Arc;
        public Chord WithArc(Arc arc) => (arc);
        public Chord(Arc arc) => (Arc) = (arc);
        public static Chord Default = new Chord();
        public static Chord New(Arc arc) => new Chord(arc);
        public static implicit operator Arc(Chord self) => self.Arc;
        public static implicit operator Chord(Arc value) => new Chord(value);
        public override bool Equals(object obj) { if (!(obj is Chord)) return false; var other = (Chord)obj; return Arc.Equals(other.Arc); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Arc);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Chord self) => new Dynamic(self);
        public static implicit operator Chord(Dynamic value) => value.As<Chord>();
        public String TypeName => "Chord";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Arc");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Arc));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    public readonly partial struct Segment: ClosedShape2D
    {
        public readonly Arc Arc;
        public Segment WithArc(Arc arc) => (arc);
        public Segment(Arc arc) => (Arc) = (arc);
        public static Segment Default = new Segment();
        public static Segment New(Arc arc) => new Segment(arc);
        public static implicit operator Arc(Segment self) => self.Arc;
        public static implicit operator Segment(Arc value) => new Segment(value);
        public override bool Equals(object obj) { if (!(obj is Segment)) return false; var other = (Segment)obj; return Arc.Equals(other.Arc); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Arc);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Segment self) => new Dynamic(self);
        public static implicit operator Segment(Dynamic value) => value.As<Segment>();
        public String TypeName => "Segment";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Arc");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Arc));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    public readonly partial struct RegularPolygon: Polygon2D
    {
        public readonly Integer NumPoints;
        public RegularPolygon WithNumPoints(Integer numPoints) => (numPoints);
        public RegularPolygon(Integer numPoints) => (NumPoints) = (numPoints);
        public static RegularPolygon Default = new RegularPolygon();
        public static RegularPolygon New(Integer numPoints) => new RegularPolygon(numPoints);
        public static implicit operator Integer(RegularPolygon self) => self.NumPoints;
        public static implicit operator RegularPolygon(Integer value) => new RegularPolygon(value);
        public override bool Equals(object obj) { if (!(obj is RegularPolygon)) return false; var other = (RegularPolygon)obj; return NumPoints.Equals(other.NumPoints); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(NumPoints);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(RegularPolygon self) => new Dynamic(self);
        public static implicit operator RegularPolygon(Dynamic value) => value.As<RegularPolygon>();
        public String TypeName => "RegularPolygon";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"NumPoints");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(NumPoints));
        // Implemented concept functions and type functions
        public Array<Vector2D> Points => this.NumPoints.CirclePoints;
        public Boolean Closed => ((Boolean)true);
        // Unimplemented concept functions
    }
    public readonly partial struct Box2D: Shape2D
    {
        public readonly Vector2D Center;
        public readonly Angle Rotation;
        public readonly Size2D Extent;
        public Box2D WithCenter(Vector2D center) => (center, Rotation, Extent);
        public Box2D WithRotation(Angle rotation) => (Center, rotation, Extent);
        public Box2D WithExtent(Size2D extent) => (Center, Rotation, extent);
        public Box2D(Vector2D center, Angle rotation, Size2D extent) => (Center, Rotation, Extent) = (center, rotation, extent);
        public static Box2D Default = new Box2D();
        public static Box2D New(Vector2D center, Angle rotation, Size2D extent) => new Box2D(center, rotation, extent);
        public static implicit operator (Vector2D, Angle, Size2D)(Box2D self) => (self.Center, self.Rotation, self.Extent);
        public static implicit operator Box2D((Vector2D, Angle, Size2D) value) => new Box2D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D center, out Angle rotation, out Size2D extent) { center = Center; rotation = Rotation; extent = Extent; }
        public override bool Equals(object obj) { if (!(obj is Box2D)) return false; var other = (Box2D)obj; return Center.Equals(other.Center) && Rotation.Equals(other.Rotation) && Extent.Equals(other.Extent); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Rotation, Extent);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Box2D self) => new Dynamic(self);
        public static implicit operator Box2D(Dynamic value) => value.As<Box2D>();
        public String TypeName => "Box2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Rotation", (String)"Extent");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Rotation), new Dynamic(Extent));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Sphere: Value<Sphere>
    {
        public readonly Vector3D Center;
        public readonly Number Radius;
        public Sphere WithCenter(Vector3D center) => (center, Radius);
        public Sphere WithRadius(Number radius) => (Center, radius);
        public Sphere(Vector3D center, Number radius) => (Center, Radius) = (center, radius);
        public static Sphere Default = new Sphere();
        public static Sphere New(Vector3D center, Number radius) => new Sphere(center, radius);
        public static implicit operator (Vector3D, Number)(Sphere self) => (self.Center, self.Radius);
        public static implicit operator Sphere((Vector3D, Number) value) => new Sphere(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D center, out Number radius) { center = Center; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is Sphere)) return false; var other = (Sphere)obj; return Center.Equals(other.Center) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Radius);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Sphere self) => new Dynamic(self);
        public static implicit operator Sphere(Dynamic value) => value.As<Sphere>();
        public String TypeName => "Sphere";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Radius");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Radius));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Sphere a, Sphere b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Sphere b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Sphere a, Sphere b) => a.Equals(b).Not;
        public Boolean NotEquals(Sphere b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Plane: Value<Plane>
    {
        public readonly Vector3D Normal;
        public readonly Number D;
        public Plane WithNormal(Vector3D normal) => (normal, D);
        public Plane WithD(Number d) => (Normal, d);
        public Plane(Vector3D normal, Number d) => (Normal, D) = (normal, d);
        public static Plane Default = new Plane();
        public static Plane New(Vector3D normal, Number d) => new Plane(normal, d);
        public static implicit operator (Vector3D, Number)(Plane self) => (self.Normal, self.D);
        public static implicit operator Plane((Vector3D, Number) value) => new Plane(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D normal, out Number d) { normal = Normal; d = D; }
        public override bool Equals(object obj) { if (!(obj is Plane)) return false; var other = (Plane)obj; return Normal.Equals(other.Normal) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Normal, D);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Plane self) => new Dynamic(self);
        public static implicit operator Plane(Dynamic value) => value.As<Plane>();
        public String TypeName => "Plane";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Normal", (String)"D");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Normal), new Dynamic(D));
        // Implemented concept functions and type functions
        public Vector3D Project(Vector3D v) => v.Subtract(this.Normal.Multiply(this.Normal.Dot(v)));
        public static Boolean operator ==(Plane a, Plane b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Plane b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Plane a, Plane b) => a.Equals(b).Not;
        public Boolean NotEquals(Plane b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Transform3D: Value<Transform3D>
    {
        public readonly Vector3D Translation;
        public readonly Rotation3D Rotation;
        public readonly Vector3D Scale;
        public Transform3D WithTranslation(Vector3D translation) => (translation, Rotation, Scale);
        public Transform3D WithRotation(Rotation3D rotation) => (Translation, rotation, Scale);
        public Transform3D WithScale(Vector3D scale) => (Translation, Rotation, scale);
        public Transform3D(Vector3D translation, Rotation3D rotation, Vector3D scale) => (Translation, Rotation, Scale) = (translation, rotation, scale);
        public static Transform3D Default = new Transform3D();
        public static Transform3D New(Vector3D translation, Rotation3D rotation, Vector3D scale) => new Transform3D(translation, rotation, scale);
        public static implicit operator (Vector3D, Rotation3D, Vector3D)(Transform3D self) => (self.Translation, self.Rotation, self.Scale);
        public static implicit operator Transform3D((Vector3D, Rotation3D, Vector3D) value) => new Transform3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D translation, out Rotation3D rotation, out Vector3D scale) { translation = Translation; rotation = Rotation; scale = Scale; }
        public override bool Equals(object obj) { if (!(obj is Transform3D)) return false; var other = (Transform3D)obj; return Translation.Equals(other.Translation) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Translation, Rotation, Scale);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Transform3D self) => new Dynamic(self);
        public static implicit operator Transform3D(Dynamic value) => value.As<Transform3D>();
        public String TypeName => "Transform3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Translation", (String)"Rotation", (String)"Scale");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Translation), new Dynamic(Rotation), new Dynamic(Scale));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Transform3D a, Transform3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Transform3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Transform3D a, Transform3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Transform3D b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Pose3D: Value<Pose3D>
    {
        public readonly Vector3D Position;
        public readonly Orientation3D Orientation;
        public Pose3D WithPosition(Vector3D position) => (position, Orientation);
        public Pose3D WithOrientation(Orientation3D orientation) => (Position, orientation);
        public Pose3D(Vector3D position, Orientation3D orientation) => (Position, Orientation) = (position, orientation);
        public static Pose3D Default = new Pose3D();
        public static Pose3D New(Vector3D position, Orientation3D orientation) => new Pose3D(position, orientation);
        public static implicit operator (Vector3D, Orientation3D)(Pose3D self) => (self.Position, self.Orientation);
        public static implicit operator Pose3D((Vector3D, Orientation3D) value) => new Pose3D(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D position, out Orientation3D orientation) { position = Position; orientation = Orientation; }
        public override bool Equals(object obj) { if (!(obj is Pose3D)) return false; var other = (Pose3D)obj; return Position.Equals(other.Position) && Orientation.Equals(other.Orientation); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Position, Orientation);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Pose3D self) => new Dynamic(self);
        public static implicit operator Pose3D(Dynamic value) => value.As<Pose3D>();
        public String TypeName => "Pose3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Position", (String)"Orientation");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Position), new Dynamic(Orientation));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Pose3D a, Pose3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Pose3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Pose3D a, Pose3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Pose3D b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Frame3D
    {
        public readonly Vector3D Forward;
        public readonly Vector3D Up;
        public readonly Vector3D Right;
        public Frame3D WithForward(Vector3D forward) => (forward, Up, Right);
        public Frame3D WithUp(Vector3D up) => (Forward, up, Right);
        public Frame3D WithRight(Vector3D right) => (Forward, Up, right);
        public Frame3D(Vector3D forward, Vector3D up, Vector3D right) => (Forward, Up, Right) = (forward, up, right);
        public static Frame3D Default = new Frame3D();
        public static Frame3D New(Vector3D forward, Vector3D up, Vector3D right) => new Frame3D(forward, up, right);
        public static implicit operator (Vector3D, Vector3D, Vector3D)(Frame3D self) => (self.Forward, self.Up, self.Right);
        public static implicit operator Frame3D((Vector3D, Vector3D, Vector3D) value) => new Frame3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D forward, out Vector3D up, out Vector3D right) { forward = Forward; up = Up; right = Right; }
        public override bool Equals(object obj) { if (!(obj is Frame3D)) return false; var other = (Frame3D)obj; return Forward.Equals(other.Forward) && Up.Equals(other.Up) && Right.Equals(other.Right); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Forward, Up, Right);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Frame3D self) => new Dynamic(self);
        public static implicit operator Frame3D(Dynamic value) => value.As<Frame3D>();
        public String TypeName => "Frame3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Forward", (String)"Up", (String)"Right");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Forward), new Dynamic(Up), new Dynamic(Right));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Bounds3D: Interval<Bounds3D, Vector3D>
    {
        public readonly Vector3D Min;
        public readonly Vector3D Max;
        public Bounds3D WithMin(Vector3D min) => (min, Max);
        public Bounds3D WithMax(Vector3D max) => (Min, max);
        public Bounds3D(Vector3D min, Vector3D max) => (Min, Max) = (min, max);
        public static Bounds3D Default = new Bounds3D();
        public static Bounds3D New(Vector3D min, Vector3D max) => new Bounds3D(min, max);
        public static implicit operator (Vector3D, Vector3D)(Bounds3D self) => (self.Min, self.Max);
        public static implicit operator Bounds3D((Vector3D, Vector3D) value) => new Bounds3D(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D min, out Vector3D max) { min = Min; max = Max; }
        public override bool Equals(object obj) { if (!(obj is Bounds3D)) return false; var other = (Bounds3D)obj; return Min.Equals(other.Min) && Max.Equals(other.Max); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Min, Max);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Bounds3D self) => new Dynamic(self);
        public static implicit operator Bounds3D(Dynamic value) => value.As<Bounds3D>();
        public String TypeName => "Bounds3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Min", (String)"Max");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Min), new Dynamic(Max));
        Vector3D Interval<Bounds3D, Vector3D>.Min => Min;
        Vector3D Interval<Bounds3D, Vector3D>.Max => Max;
        // Implemented concept functions and type functions
        public Vector3D Size => this.Max.Subtract(this.Min);
        public Vector3D Lerp(Number amount) => this.Min.Lerp(this.Max, amount);
        public Bounds3D Reverse => this.Max.Tuple2(this.Min);
        public Vector3D Center => this.Lerp(((Number)0.5));
        public Boolean Contains(Vector3D value) => value.Between(this.Min, this.Max);
        public Boolean Contains(Bounds3D y) => this.Contains(y.Min).And(this.Contains(y.Max));
        public Boolean Overlaps(Bounds3D y) => this.Contains(y.Min).Or(this.Contains(y.Max).Or(y.Contains(this.Min).Or(y.Contains(this.Max))));
        public Tuple2<Bounds3D, Bounds3D> SplitAt(Number t) => this.Left(t).Tuple2(this.Right(t));
        public Tuple2<Bounds3D, Bounds3D> Split => this.SplitAt(((Number)0.5));
        public Bounds3D Left(Number t) => this.Min.Tuple2(this.Lerp(t));
        public Bounds3D Right(Number t) => this.Lerp(t).Tuple2(this.Max);
        public Bounds3D MoveTo(Vector3D v) => v.Tuple2(v.Add(this.Size));
        public Bounds3D LeftHalf => this.Left(((Number)0.5));
        public Bounds3D RightHalf => this.Right(((Number)0.5));
        public Bounds3D Recenter(Vector3D c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public Bounds3D Clamp(Bounds3D y) => this.Clamp(y.Min).Tuple2(this.Clamp(y.Max));
        public Vector3D Clamp(Vector3D value) => value.Clamp(this.Min, this.Max);
        public static Boolean operator ==(Bounds3D a, Bounds3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Bounds3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Bounds3D a, Bounds3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Bounds3D b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 2;
        public Vector3D this[Integer n] => n == 0 ? Min : n == 1 ? Max : throw new System.IndexOutOfRangeException();
        public Vector3D At(Integer n) => n == 0 ? Min : n == 1 ? Max : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Line3D: PolyLine3D, Array<Vector3D>
    {
        public readonly Vector3D A;
        public readonly Vector3D B;
        public Line3D WithA(Vector3D a) => (a, B);
        public Line3D WithB(Vector3D b) => (A, b);
        public Line3D(Vector3D a, Vector3D b) => (A, B) = (a, b);
        public static Line3D Default = new Line3D();
        public static Line3D New(Vector3D a, Vector3D b) => new Line3D(a, b);
        public static implicit operator (Vector3D, Vector3D)(Line3D self) => (self.A, self.B);
        public static implicit operator Line3D((Vector3D, Vector3D) value) => new Line3D(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D a, out Vector3D b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Line3D)) return false; var other = (Line3D)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Line3D self) => new Dynamic(self);
        public static implicit operator Line3D(Dynamic value) => value.As<Line3D>();
        public String TypeName => "Line3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public Boolean Closed => ((Boolean)false);
        public Array<Vector3D> Points => this;
        public Number Length => this.B.Subtract(this.A).Length;
        public Vector3D Direction => this.B.Subtract(this.A);
        public static implicit operator Ray3D(Line3D x) => x.A.Tuple2(x.Direction);
        public Ray3D Ray3D => this.A.Tuple2(this.Direction);
        // Unimplemented concept functions
        public Integer Count => 2;
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Ray3D: Value<Ray3D>
    {
        public readonly Vector3D Direction;
        public readonly Vector3D Position;
        public Ray3D WithDirection(Vector3D direction) => (direction, Position);
        public Ray3D WithPosition(Vector3D position) => (Direction, position);
        public Ray3D(Vector3D direction, Vector3D position) => (Direction, Position) = (direction, position);
        public static Ray3D Default = new Ray3D();
        public static Ray3D New(Vector3D direction, Vector3D position) => new Ray3D(direction, position);
        public static implicit operator (Vector3D, Vector3D)(Ray3D self) => (self.Direction, self.Position);
        public static implicit operator Ray3D((Vector3D, Vector3D) value) => new Ray3D(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D direction, out Vector3D position) { direction = Direction; position = Position; }
        public override bool Equals(object obj) { if (!(obj is Ray3D)) return false; var other = (Ray3D)obj; return Direction.Equals(other.Direction) && Position.Equals(other.Position); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Direction, Position);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Ray3D self) => new Dynamic(self);
        public static implicit operator Ray3D(Dynamic value) => value.As<Ray3D>();
        public String TypeName => "Ray3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Direction", (String)"Position");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Direction), new Dynamic(Position));
        // Implemented concept functions and type functions
        public Angle Angle(Ray3D b) => this.Direction.Angle(b.Direction);
        public static Boolean operator ==(Ray3D a, Ray3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Ray3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Ray3D a, Ray3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Ray3D b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Triangle3D: Value<Triangle3D>, Array<Vector3D>
    {
        public readonly Vector3D A;
        public readonly Vector3D B;
        public readonly Vector3D C;
        public Triangle3D WithA(Vector3D a) => (a, B, C);
        public Triangle3D WithB(Vector3D b) => (A, b, C);
        public Triangle3D WithC(Vector3D c) => (A, B, c);
        public Triangle3D(Vector3D a, Vector3D b, Vector3D c) => (A, B, C) = (a, b, c);
        public static Triangle3D Default = new Triangle3D();
        public static Triangle3D New(Vector3D a, Vector3D b, Vector3D c) => new Triangle3D(a, b, c);
        public static implicit operator (Vector3D, Vector3D, Vector3D)(Triangle3D self) => (self.A, self.B, self.C);
        public static implicit operator Triangle3D((Vector3D, Vector3D, Vector3D) value) => new Triangle3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D a, out Vector3D b, out Vector3D c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is Triangle3D)) return false; var other = (Triangle3D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Triangle3D self) => new Dynamic(self);
        public static implicit operator Triangle3D(Dynamic value) => value.As<Triangle3D>();
        public String TypeName => "Triangle3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        // Implemented concept functions and type functions
        public Triangle3D Flip => this.C.Tuple3(this.B, this.A);
        public Vector3D Normal => this.B.Subtract(this.A).Cross(this.C.Subtract(this.A)).Normalize;
        public Vector3D Center => this.A.Add(this.B.Add(this.C)).Divide(((Number)3));
        public static implicit operator Plane(Triangle3D t) => t.Normal.Tuple2(t.Normal.Dot(t.A));
        public Plane Plane => this.Normal.Tuple2(this.Normal.Dot(this.A));
        public Vector3D Barycentric(Vector2D uv) => this.A.Barycentric(this.B, this.C, uv);
        public static Boolean operator ==(Triangle3D a, Triangle3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Triangle3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Triangle3D a, Triangle3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Triangle3D b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Quad3D: Value<Quad3D>, Array<Vector3D>
    {
        public readonly Vector3D A;
        public readonly Vector3D B;
        public readonly Vector3D C;
        public readonly Vector3D D;
        public Quad3D WithA(Vector3D a) => (a, B, C, D);
        public Quad3D WithB(Vector3D b) => (A, b, C, D);
        public Quad3D WithC(Vector3D c) => (A, B, c, D);
        public Quad3D WithD(Vector3D d) => (A, B, C, d);
        public Quad3D(Vector3D a, Vector3D b, Vector3D c, Vector3D d) => (A, B, C, D) = (a, b, c, d);
        public static Quad3D Default = new Quad3D();
        public static Quad3D New(Vector3D a, Vector3D b, Vector3D c, Vector3D d) => new Quad3D(a, b, c, d);
        public static implicit operator (Vector3D, Vector3D, Vector3D, Vector3D)(Quad3D self) => (self.A, self.B, self.C, self.D);
        public static implicit operator Quad3D((Vector3D, Vector3D, Vector3D, Vector3D) value) => new Quad3D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector3D a, out Vector3D b, out Vector3D c, out Vector3D d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is Quad3D)) return false; var other = (Quad3D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Quad3D self) => new Dynamic(self);
        public static implicit operator Quad3D(Dynamic value) => value.As<Quad3D>();
        public String TypeName => "Quad3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Quad3D a, Quad3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Quad3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Quad3D a, Quad3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Quad3D b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Capsule: Shape3D
    {
        public readonly Line3D Line;
        public readonly Number Radius;
        public Capsule WithLine(Line3D line) => (line, Radius);
        public Capsule WithRadius(Number radius) => (Line, radius);
        public Capsule(Line3D line, Number radius) => (Line, Radius) = (line, radius);
        public static Capsule Default = new Capsule();
        public static Capsule New(Line3D line, Number radius) => new Capsule(line, radius);
        public static implicit operator (Line3D, Number)(Capsule self) => (self.Line, self.Radius);
        public static implicit operator Capsule((Line3D, Number) value) => new Capsule(value.Item1, value.Item2);
        public void Deconstruct(out Line3D line, out Number radius) { line = Line; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is Capsule)) return false; var other = (Capsule)obj; return Line.Equals(other.Line) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Line, Radius);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Capsule self) => new Dynamic(self);
        public static implicit operator Capsule(Dynamic value) => value.As<Capsule>();
        public String TypeName => "Capsule";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Line", (String)"Radius");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Line), new Dynamic(Radius));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Cylinder: Shape3D
    {
        public readonly Line3D Line;
        public readonly Number Radius;
        public Cylinder WithLine(Line3D line) => (line, Radius);
        public Cylinder WithRadius(Number radius) => (Line, radius);
        public Cylinder(Line3D line, Number radius) => (Line, Radius) = (line, radius);
        public static Cylinder Default = new Cylinder();
        public static Cylinder New(Line3D line, Number radius) => new Cylinder(line, radius);
        public static implicit operator (Line3D, Number)(Cylinder self) => (self.Line, self.Radius);
        public static implicit operator Cylinder((Line3D, Number) value) => new Cylinder(value.Item1, value.Item2);
        public void Deconstruct(out Line3D line, out Number radius) { line = Line; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is Cylinder)) return false; var other = (Cylinder)obj; return Line.Equals(other.Line) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Line, Radius);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Cylinder self) => new Dynamic(self);
        public static implicit operator Cylinder(Dynamic value) => value.As<Cylinder>();
        public String TypeName => "Cylinder";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Line", (String)"Radius");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Line), new Dynamic(Radius));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Cone: Shape3D
    {
        public readonly Line3D Line;
        public readonly Number Radius;
        public Cone WithLine(Line3D line) => (line, Radius);
        public Cone WithRadius(Number radius) => (Line, radius);
        public Cone(Line3D line, Number radius) => (Line, Radius) = (line, radius);
        public static Cone Default = new Cone();
        public static Cone New(Line3D line, Number radius) => new Cone(line, radius);
        public static implicit operator (Line3D, Number)(Cone self) => (self.Line, self.Radius);
        public static implicit operator Cone((Line3D, Number) value) => new Cone(value.Item1, value.Item2);
        public void Deconstruct(out Line3D line, out Number radius) { line = Line; radius = Radius; }
        public override bool Equals(object obj) { if (!(obj is Cone)) return false; var other = (Cone)obj; return Line.Equals(other.Line) && Radius.Equals(other.Radius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Line, Radius);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Cone self) => new Dynamic(self);
        public static implicit operator Cone(Dynamic value) => value.As<Cone>();
        public String TypeName => "Cone";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Line", (String)"Radius");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Line), new Dynamic(Radius));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tube: Shape3D
    {
        public readonly Line3D Line;
        public readonly Number InnerRadius;
        public readonly Number OuterRadius;
        public Tube WithLine(Line3D line) => (line, InnerRadius, OuterRadius);
        public Tube WithInnerRadius(Number innerRadius) => (Line, innerRadius, OuterRadius);
        public Tube WithOuterRadius(Number outerRadius) => (Line, InnerRadius, outerRadius);
        public Tube(Line3D line, Number innerRadius, Number outerRadius) => (Line, InnerRadius, OuterRadius) = (line, innerRadius, outerRadius);
        public static Tube Default = new Tube();
        public static Tube New(Line3D line, Number innerRadius, Number outerRadius) => new Tube(line, innerRadius, outerRadius);
        public static implicit operator (Line3D, Number, Number)(Tube self) => (self.Line, self.InnerRadius, self.OuterRadius);
        public static implicit operator Tube((Line3D, Number, Number) value) => new Tube(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Line3D line, out Number innerRadius, out Number outerRadius) { line = Line; innerRadius = InnerRadius; outerRadius = OuterRadius; }
        public override bool Equals(object obj) { if (!(obj is Tube)) return false; var other = (Tube)obj; return Line.Equals(other.Line) && InnerRadius.Equals(other.InnerRadius) && OuterRadius.Equals(other.OuterRadius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Line, InnerRadius, OuterRadius);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tube self) => new Dynamic(self);
        public static implicit operator Tube(Dynamic value) => value.As<Tube>();
        public String TypeName => "Tube";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Line", (String)"InnerRadius", (String)"OuterRadius");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Line), new Dynamic(InnerRadius), new Dynamic(OuterRadius));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct ConeSegment: Shape3D
    {
        public readonly Line3D Line;
        public readonly Number Radius1;
        public readonly Number Radius2;
        public ConeSegment WithLine(Line3D line) => (line, Radius1, Radius2);
        public ConeSegment WithRadius1(Number radius1) => (Line, radius1, Radius2);
        public ConeSegment WithRadius2(Number radius2) => (Line, Radius1, radius2);
        public ConeSegment(Line3D line, Number radius1, Number radius2) => (Line, Radius1, Radius2) = (line, radius1, radius2);
        public static ConeSegment Default = new ConeSegment();
        public static ConeSegment New(Line3D line, Number radius1, Number radius2) => new ConeSegment(line, radius1, radius2);
        public static implicit operator (Line3D, Number, Number)(ConeSegment self) => (self.Line, self.Radius1, self.Radius2);
        public static implicit operator ConeSegment((Line3D, Number, Number) value) => new ConeSegment(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Line3D line, out Number radius1, out Number radius2) { line = Line; radius1 = Radius1; radius2 = Radius2; }
        public override bool Equals(object obj) { if (!(obj is ConeSegment)) return false; var other = (ConeSegment)obj; return Line.Equals(other.Line) && Radius1.Equals(other.Radius1) && Radius2.Equals(other.Radius2); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Line, Radius1, Radius2);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ConeSegment self) => new Dynamic(self);
        public static implicit operator ConeSegment(Dynamic value) => value.As<ConeSegment>();
        public String TypeName => "ConeSegment";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Line", (String)"Radius1", (String)"Radius2");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Line), new Dynamic(Radius1), new Dynamic(Radius2));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Box3D: Shape3D
    {
        public readonly Vector3D Center;
        public readonly Rotation3D Rotation;
        public readonly Size3D Extent;
        public Box3D WithCenter(Vector3D center) => (center, Rotation, Extent);
        public Box3D WithRotation(Rotation3D rotation) => (Center, rotation, Extent);
        public Box3D WithExtent(Size3D extent) => (Center, Rotation, extent);
        public Box3D(Vector3D center, Rotation3D rotation, Size3D extent) => (Center, Rotation, Extent) = (center, rotation, extent);
        public static Box3D Default = new Box3D();
        public static Box3D New(Vector3D center, Rotation3D rotation, Size3D extent) => new Box3D(center, rotation, extent);
        public static implicit operator (Vector3D, Rotation3D, Size3D)(Box3D self) => (self.Center, self.Rotation, self.Extent);
        public static implicit operator Box3D((Vector3D, Rotation3D, Size3D) value) => new Box3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D center, out Rotation3D rotation, out Size3D extent) { center = Center; rotation = Rotation; extent = Extent; }
        public override bool Equals(object obj) { if (!(obj is Box3D)) return false; var other = (Box3D)obj; return Center.Equals(other.Center) && Rotation.Equals(other.Rotation) && Extent.Equals(other.Extent); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Rotation, Extent);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Box3D self) => new Dynamic(self);
        public static implicit operator Box3D(Dynamic value) => value.As<Box3D>();
        public String TypeName => "Box3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Center", (String)"Rotation", (String)"Extent");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Center), new Dynamic(Rotation), new Dynamic(Extent));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct CubicBezier2D: Array<Vector2D>
    {
        public readonly Vector2D A;
        public readonly Vector2D B;
        public readonly Vector2D C;
        public readonly Vector2D D;
        public CubicBezier2D WithA(Vector2D a) => (a, B, C, D);
        public CubicBezier2D WithB(Vector2D b) => (A, b, C, D);
        public CubicBezier2D WithC(Vector2D c) => (A, B, c, D);
        public CubicBezier2D WithD(Vector2D d) => (A, B, C, d);
        public CubicBezier2D(Vector2D a, Vector2D b, Vector2D c, Vector2D d) => (A, B, C, D) = (a, b, c, d);
        public static CubicBezier2D Default = new CubicBezier2D();
        public static CubicBezier2D New(Vector2D a, Vector2D b, Vector2D c, Vector2D d) => new CubicBezier2D(a, b, c, d);
        public static implicit operator (Vector2D, Vector2D, Vector2D, Vector2D)(CubicBezier2D self) => (self.A, self.B, self.C, self.D);
        public static implicit operator CubicBezier2D((Vector2D, Vector2D, Vector2D, Vector2D) value) => new CubicBezier2D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector2D a, out Vector2D b, out Vector2D c, out Vector2D d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is CubicBezier2D)) return false; var other = (CubicBezier2D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(CubicBezier2D self) => new Dynamic(self);
        public static implicit operator CubicBezier2D(Dynamic value) => value.As<CubicBezier2D>();
        public String TypeName => "CubicBezier2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct CubicBezier3D: Array<Vector3D>
    {
        public readonly Vector3D A;
        public readonly Vector3D B;
        public readonly Vector3D C;
        public readonly Vector3D D;
        public CubicBezier3D WithA(Vector3D a) => (a, B, C, D);
        public CubicBezier3D WithB(Vector3D b) => (A, b, C, D);
        public CubicBezier3D WithC(Vector3D c) => (A, B, c, D);
        public CubicBezier3D WithD(Vector3D d) => (A, B, C, d);
        public CubicBezier3D(Vector3D a, Vector3D b, Vector3D c, Vector3D d) => (A, B, C, D) = (a, b, c, d);
        public static CubicBezier3D Default = new CubicBezier3D();
        public static CubicBezier3D New(Vector3D a, Vector3D b, Vector3D c, Vector3D d) => new CubicBezier3D(a, b, c, d);
        public static implicit operator (Vector3D, Vector3D, Vector3D, Vector3D)(CubicBezier3D self) => (self.A, self.B, self.C, self.D);
        public static implicit operator CubicBezier3D((Vector3D, Vector3D, Vector3D, Vector3D) value) => new CubicBezier3D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector3D a, out Vector3D b, out Vector3D c, out Vector3D d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is CubicBezier3D)) return false; var other = (CubicBezier3D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(CubicBezier3D self) => new Dynamic(self);
        public static implicit operator CubicBezier3D(Dynamic value) => value.As<CubicBezier3D>();
        public String TypeName => "CubicBezier3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct QuadraticBezier2D: Array<Vector2D>
    {
        public readonly Vector2D A;
        public readonly Vector2D B;
        public readonly Vector2D C;
        public QuadraticBezier2D WithA(Vector2D a) => (a, B, C);
        public QuadraticBezier2D WithB(Vector2D b) => (A, b, C);
        public QuadraticBezier2D WithC(Vector2D c) => (A, B, c);
        public QuadraticBezier2D(Vector2D a, Vector2D b, Vector2D c) => (A, B, C) = (a, b, c);
        public static QuadraticBezier2D Default = new QuadraticBezier2D();
        public static QuadraticBezier2D New(Vector2D a, Vector2D b, Vector2D c) => new QuadraticBezier2D(a, b, c);
        public static implicit operator (Vector2D, Vector2D, Vector2D)(QuadraticBezier2D self) => (self.A, self.B, self.C);
        public static implicit operator QuadraticBezier2D((Vector2D, Vector2D, Vector2D) value) => new QuadraticBezier2D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector2D a, out Vector2D b, out Vector2D c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is QuadraticBezier2D)) return false; var other = (QuadraticBezier2D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(QuadraticBezier2D self) => new Dynamic(self);
        public static implicit operator QuadraticBezier2D(Dynamic value) => value.As<QuadraticBezier2D>();
        public String TypeName => "QuadraticBezier2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector2D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector2D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct QuadraticBezier3D: Array<Vector3D>
    {
        public readonly Vector3D A;
        public readonly Vector3D B;
        public readonly Vector3D C;
        public QuadraticBezier3D WithA(Vector3D a) => (a, B, C);
        public QuadraticBezier3D WithB(Vector3D b) => (A, b, C);
        public QuadraticBezier3D WithC(Vector3D c) => (A, B, c);
        public QuadraticBezier3D(Vector3D a, Vector3D b, Vector3D c) => (A, B, C) = (a, b, c);
        public static QuadraticBezier3D Default = new QuadraticBezier3D();
        public static QuadraticBezier3D New(Vector3D a, Vector3D b, Vector3D c) => new QuadraticBezier3D(a, b, c);
        public static implicit operator (Vector3D, Vector3D, Vector3D)(QuadraticBezier3D self) => (self.A, self.B, self.C);
        public static implicit operator QuadraticBezier3D((Vector3D, Vector3D, Vector3D) value) => new QuadraticBezier3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D a, out Vector3D b, out Vector3D c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is QuadraticBezier3D)) return false; var other = (QuadraticBezier3D)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(QuadraticBezier3D self) => new Dynamic(self);
        public static implicit operator QuadraticBezier3D(Dynamic value) => value.As<QuadraticBezier3D>();
        public String TypeName => "QuadraticBezier3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector3D this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector3D At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Quaternion: Value<Quaternion>
    {
        public readonly Number X;
        public readonly Number Y;
        public readonly Number Z;
        public readonly Number W;
        public Quaternion WithX(Number x) => (x, Y, Z, W);
        public Quaternion WithY(Number y) => (X, y, Z, W);
        public Quaternion WithZ(Number z) => (X, Y, z, W);
        public Quaternion WithW(Number w) => (X, Y, Z, w);
        public Quaternion(Number x, Number y, Number z, Number w) => (X, Y, Z, W) = (x, y, z, w);
        public static Quaternion Default = new Quaternion();
        public static Quaternion New(Number x, Number y, Number z, Number w) => new Quaternion(x, y, z, w);
        public static implicit operator (Number, Number, Number, Number)(Quaternion self) => (self.X, self.Y, self.Z, self.W);
        public static implicit operator Quaternion((Number, Number, Number, Number) value) => new Quaternion(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Number x, out Number y, out Number z, out Number w) { x = X; y = Y; z = Z; w = W; }
        public override bool Equals(object obj) { if (!(obj is Quaternion)) return false; var other = (Quaternion)obj; return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X, Y, Z, W);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Quaternion self) => new Dynamic(self);
        public static implicit operator Quaternion(Dynamic value) => value.As<Quaternion>();
        public String TypeName => "Quaternion";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X", (String)"Y", (String)"Z", (String)"W");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X), new Dynamic(Y), new Dynamic(Z), new Dynamic(W));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Quaternion a, Quaternion b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Quaternion b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Quaternion a, Quaternion b) => a.Equals(b).Not;
        public Boolean NotEquals(Quaternion b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct AxisAngle: Value<AxisAngle>
    {
        public readonly Vector3D Axis;
        public readonly Angle Angle;
        public AxisAngle WithAxis(Vector3D axis) => (axis, Angle);
        public AxisAngle WithAngle(Angle angle) => (Axis, angle);
        public AxisAngle(Vector3D axis, Angle angle) => (Axis, Angle) = (axis, angle);
        public static AxisAngle Default = new AxisAngle();
        public static AxisAngle New(Vector3D axis, Angle angle) => new AxisAngle(axis, angle);
        public static implicit operator (Vector3D, Angle)(AxisAngle self) => (self.Axis, self.Angle);
        public static implicit operator AxisAngle((Vector3D, Angle) value) => new AxisAngle(value.Item1, value.Item2);
        public void Deconstruct(out Vector3D axis, out Angle angle) { axis = Axis; angle = Angle; }
        public override bool Equals(object obj) { if (!(obj is AxisAngle)) return false; var other = (AxisAngle)obj; return Axis.Equals(other.Axis) && Angle.Equals(other.Angle); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Axis, Angle);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(AxisAngle self) => new Dynamic(self);
        public static implicit operator AxisAngle(Dynamic value) => value.As<AxisAngle>();
        public String TypeName => "AxisAngle";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Axis", (String)"Angle");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Axis), new Dynamic(Angle));
        // Implemented concept functions and type functions
        public static Boolean operator ==(AxisAngle a, AxisAngle b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(AxisAngle b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(AxisAngle a, AxisAngle b) => a.Equals(b).Not;
        public Boolean NotEquals(AxisAngle b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct EulerAngles: Value<EulerAngles>
    {
        public readonly Angle Yaw;
        public readonly Angle Pitch;
        public readonly Angle Roll;
        public EulerAngles WithYaw(Angle yaw) => (yaw, Pitch, Roll);
        public EulerAngles WithPitch(Angle pitch) => (Yaw, pitch, Roll);
        public EulerAngles WithRoll(Angle roll) => (Yaw, Pitch, roll);
        public EulerAngles(Angle yaw, Angle pitch, Angle roll) => (Yaw, Pitch, Roll) = (yaw, pitch, roll);
        public static EulerAngles Default = new EulerAngles();
        public static EulerAngles New(Angle yaw, Angle pitch, Angle roll) => new EulerAngles(yaw, pitch, roll);
        public static implicit operator (Angle, Angle, Angle)(EulerAngles self) => (self.Yaw, self.Pitch, self.Roll);
        public static implicit operator EulerAngles((Angle, Angle, Angle) value) => new EulerAngles(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Angle yaw, out Angle pitch, out Angle roll) { yaw = Yaw; pitch = Pitch; roll = Roll; }
        public override bool Equals(object obj) { if (!(obj is EulerAngles)) return false; var other = (EulerAngles)obj; return Yaw.Equals(other.Yaw) && Pitch.Equals(other.Pitch) && Roll.Equals(other.Roll); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Yaw, Pitch, Roll);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(EulerAngles self) => new Dynamic(self);
        public static implicit operator EulerAngles(Dynamic value) => value.As<EulerAngles>();
        public String TypeName => "EulerAngles";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Yaw", (String)"Pitch", (String)"Roll");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Yaw), new Dynamic(Pitch), new Dynamic(Roll));
        // Implemented concept functions and type functions
        public static Boolean operator ==(EulerAngles a, EulerAngles b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(EulerAngles b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(EulerAngles a, EulerAngles b) => a.Equals(b).Not;
        public Boolean NotEquals(EulerAngles b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Rotation3D: Value<Rotation3D>
    {
        public readonly Quaternion Quaternion;
        public Rotation3D WithQuaternion(Quaternion quaternion) => (quaternion);
        public Rotation3D(Quaternion quaternion) => (Quaternion) = (quaternion);
        public static Rotation3D Default = new Rotation3D();
        public static Rotation3D New(Quaternion quaternion) => new Rotation3D(quaternion);
        public static implicit operator Quaternion(Rotation3D self) => self.Quaternion;
        public static implicit operator Rotation3D(Quaternion value) => new Rotation3D(value);
        public override bool Equals(object obj) { if (!(obj is Rotation3D)) return false; var other = (Rotation3D)obj; return Quaternion.Equals(other.Quaternion); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Quaternion);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Rotation3D self) => new Dynamic(self);
        public static implicit operator Rotation3D(Dynamic value) => value.As<Rotation3D>();
        public String TypeName => "Rotation3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Quaternion");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Quaternion));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Rotation3D a, Rotation3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Rotation3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Rotation3D a, Rotation3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Rotation3D b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Orientation3D: Value<Orientation3D>
    {
        public readonly Rotation3D Value;
        public Orientation3D WithValue(Rotation3D value) => (value);
        public Orientation3D(Rotation3D value) => (Value) = (value);
        public static Orientation3D Default = new Orientation3D();
        public static Orientation3D New(Rotation3D value) => new Orientation3D(value);
        public static implicit operator Rotation3D(Orientation3D self) => self.Value;
        public static implicit operator Orientation3D(Rotation3D value) => new Orientation3D(value);
        public override bool Equals(object obj) { if (!(obj is Orientation3D)) return false; var other = (Orientation3D)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Orientation3D self) => new Dynamic(self);
        public static implicit operator Orientation3D(Dynamic value) => value.As<Orientation3D>();
        public String TypeName => "Orientation3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Orientation3D a, Orientation3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Orientation3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Orientation3D a, Orientation3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Orientation3D b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Line4D: Value<Line4D>, Array<Vector4D>
    {
        public readonly Vector4D A;
        public readonly Vector4D B;
        public Line4D WithA(Vector4D a) => (a, B);
        public Line4D WithB(Vector4D b) => (A, b);
        public Line4D(Vector4D a, Vector4D b) => (A, B) = (a, b);
        public static Line4D Default = new Line4D();
        public static Line4D New(Vector4D a, Vector4D b) => new Line4D(a, b);
        public static implicit operator (Vector4D, Vector4D)(Line4D self) => (self.A, self.B);
        public static implicit operator Line4D((Vector4D, Vector4D) value) => new Line4D(value.Item1, value.Item2);
        public void Deconstruct(out Vector4D a, out Vector4D b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Line4D)) return false; var other = (Line4D)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Line4D self) => new Dynamic(self);
        public static implicit operator Line4D(Dynamic value) => value.As<Line4D>();
        public String TypeName => "Line4D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Line4D a, Line4D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Line4D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Line4D a, Line4D b) => a.Equals(b).Not;
        public Boolean NotEquals(Line4D b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 2;
        public Vector4D this[Integer n] => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
        public Vector4D At(Integer n) => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Vertex
    {
        public readonly Vector3D Position;
        public readonly Vector3D Normal;
        public readonly Vector2D UV;
        public Vertex WithPosition(Vector3D position) => (position, Normal, UV);
        public Vertex WithNormal(Vector3D normal) => (Position, normal, UV);
        public Vertex WithUV(Vector2D uV) => (Position, Normal, uV);
        public Vertex(Vector3D position, Vector3D normal, Vector2D uV) => (Position, Normal, UV) = (position, normal, uV);
        public static Vertex Default = new Vertex();
        public static Vertex New(Vector3D position, Vector3D normal, Vector2D uV) => new Vertex(position, normal, uV);
        public static implicit operator (Vector3D, Vector3D, Vector2D)(Vertex self) => (self.Position, self.Normal, self.UV);
        public static implicit operator Vertex((Vector3D, Vector3D, Vector2D) value) => new Vertex(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D position, out Vector3D normal, out Vector2D uV) { position = Position; normal = Normal; uV = UV; }
        public override bool Equals(object obj) { if (!(obj is Vertex)) return false; var other = (Vertex)obj; return Position.Equals(other.Position) && Normal.Equals(other.Normal) && UV.Equals(other.UV); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Position, Normal, UV);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Vertex self) => new Dynamic(self);
        public static implicit operator Vertex(Dynamic value) => value.As<Vertex>();
        public String TypeName => "Vertex";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Position", (String)"Normal", (String)"UV");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Position), new Dynamic(Normal), new Dynamic(UV));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct TriMesh: Mesh<Integer3, Vertex>
    {
        public readonly Array<Vertex> Vertices;
        public readonly Array<Integer3> Faces;
        public TriMesh WithVertices(Array<Vertex> vertices) => (vertices, Faces);
        public TriMesh WithFaces(Array<Integer3> faces) => (Vertices, faces);
        public TriMesh(Array<Vertex> vertices, Array<Integer3> faces) => (Vertices, Faces) = (vertices, faces);
        public static TriMesh Default = new TriMesh();
        public static TriMesh New(Array<Vertex> vertices, Array<Integer3> faces) => new TriMesh(vertices, faces);
        public static implicit operator (Array<Vertex>, Array<Integer3>)(TriMesh self) => (self.Vertices, self.Faces);
        public static implicit operator TriMesh((Array<Vertex>, Array<Integer3>) value) => new TriMesh(value.Item1, value.Item2);
        public void Deconstruct(out Array<Vertex> vertices, out Array<Integer3> faces) { vertices = Vertices; faces = Faces; }
        public override bool Equals(object obj) { if (!(obj is TriMesh)) return false; var other = (TriMesh)obj; return Vertices.Equals(other.Vertices) && Faces.Equals(other.Faces); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Vertices, Faces);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(TriMesh self) => new Dynamic(self);
        public static implicit operator TriMesh(Dynamic value) => value.As<TriMesh>();
        public String TypeName => "TriMesh";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Vertices", (String)"Faces");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Vertices), new Dynamic(Faces));
        Array<Integer3> Mesh<Integer3, Vertex>.Faces => Faces;
        Array<Vertex> Mesh<Integer3, Vertex>.Vertices => Vertices;
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct QuadMesh: Mesh<Integer4, Vertex>
    {
        public readonly Array<Vertex> Vertices;
        public readonly Array<Integer4> Faces;
        public QuadMesh WithVertices(Array<Vertex> vertices) => (vertices, Faces);
        public QuadMesh WithFaces(Array<Integer4> faces) => (Vertices, faces);
        public QuadMesh(Array<Vertex> vertices, Array<Integer4> faces) => (Vertices, Faces) = (vertices, faces);
        public static QuadMesh Default = new QuadMesh();
        public static QuadMesh New(Array<Vertex> vertices, Array<Integer4> faces) => new QuadMesh(vertices, faces);
        public static implicit operator (Array<Vertex>, Array<Integer4>)(QuadMesh self) => (self.Vertices, self.Faces);
        public static implicit operator QuadMesh((Array<Vertex>, Array<Integer4>) value) => new QuadMesh(value.Item1, value.Item2);
        public void Deconstruct(out Array<Vertex> vertices, out Array<Integer4> faces) { vertices = Vertices; faces = Faces; }
        public override bool Equals(object obj) { if (!(obj is QuadMesh)) return false; var other = (QuadMesh)obj; return Vertices.Equals(other.Vertices) && Faces.Equals(other.Faces); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Vertices, Faces);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(QuadMesh self) => new Dynamic(self);
        public static implicit operator QuadMesh(Dynamic value) => value.As<QuadMesh>();
        public String TypeName => "QuadMesh";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Vertices", (String)"Faces");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Vertices), new Dynamic(Faces));
        Array<Integer4> Mesh<Integer4, Vertex>.Faces => Faces;
        Array<Vertex> Mesh<Integer4, Vertex>.Vertices => Vertices;
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct ParametricCurve2D: Curve2D
    {
        public readonly Function1<Number, Vector2D> Evaluator;
        public ParametricCurve2D WithEvaluator(Function1<Number, Vector2D> evaluator) => (evaluator);
        public ParametricCurve2D(Function1<Number, Vector2D> evaluator) => (Evaluator) = (evaluator);
        public static ParametricCurve2D Default = new ParametricCurve2D();
        public static ParametricCurve2D New(Function1<Number, Vector2D> evaluator) => new ParametricCurve2D(evaluator);
        public static implicit operator Function1<Number, Vector2D>(ParametricCurve2D self) => self.Evaluator;
        public static implicit operator ParametricCurve2D(Function1<Number, Vector2D> value) => new ParametricCurve2D(value);
        public override bool Equals(object obj) { if (!(obj is ParametricCurve2D)) return false; var other = (ParametricCurve2D)obj; return Evaluator.Equals(other.Evaluator); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Evaluator);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ParametricCurve2D self) => new Dynamic(self);
        public static implicit operator ParametricCurve2D(Dynamic value) => value.As<ParametricCurve2D>();
        public String TypeName => "ParametricCurve2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Evaluator");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Evaluator));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
        public Boolean Closed => throw new System.NotImplementedException();
        public Vector2D Eval(Number amount) => throw new System.NotImplementedException();
    }
    public readonly partial struct ParametricCurve3D: Curve3D
    {
        public readonly Function1<Number, Vector3D> Evaluator;
        public ParametricCurve3D WithEvaluator(Function1<Number, Vector3D> evaluator) => (evaluator);
        public ParametricCurve3D(Function1<Number, Vector3D> evaluator) => (Evaluator) = (evaluator);
        public static ParametricCurve3D Default = new ParametricCurve3D();
        public static ParametricCurve3D New(Function1<Number, Vector3D> evaluator) => new ParametricCurve3D(evaluator);
        public static implicit operator Function1<Number, Vector3D>(ParametricCurve3D self) => self.Evaluator;
        public static implicit operator ParametricCurve3D(Function1<Number, Vector3D> value) => new ParametricCurve3D(value);
        public override bool Equals(object obj) { if (!(obj is ParametricCurve3D)) return false; var other = (ParametricCurve3D)obj; return Evaluator.Equals(other.Evaluator); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Evaluator);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ParametricCurve3D self) => new Dynamic(self);
        public static implicit operator ParametricCurve3D(Dynamic value) => value.As<ParametricCurve3D>();
        public String TypeName => "ParametricCurve3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Evaluator");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Evaluator));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
        public Boolean Closed => throw new System.NotImplementedException();
        public Vector3D Eval(Number amount) => throw new System.NotImplementedException();
    }
    public readonly partial struct ParametricSurface: ProceduralSurface
    {
        public readonly Function1<Number, Vector3D> Evaluator;
        public readonly Boolean PeriodicX;
        public readonly Boolean PeriodicY;
        public ParametricSurface WithEvaluator(Function1<Number, Vector3D> evaluator) => (evaluator, PeriodicX, PeriodicY);
        public ParametricSurface WithPeriodicX(Boolean periodicX) => (Evaluator, periodicX, PeriodicY);
        public ParametricSurface WithPeriodicY(Boolean periodicY) => (Evaluator, PeriodicX, periodicY);
        public ParametricSurface(Function1<Number, Vector3D> evaluator, Boolean periodicX, Boolean periodicY) => (Evaluator, PeriodicX, PeriodicY) = (evaluator, periodicX, periodicY);
        public static ParametricSurface Default = new ParametricSurface();
        public static ParametricSurface New(Function1<Number, Vector3D> evaluator, Boolean periodicX, Boolean periodicY) => new ParametricSurface(evaluator, periodicX, periodicY);
        public static implicit operator (Function1<Number, Vector3D>, Boolean, Boolean)(ParametricSurface self) => (self.Evaluator, self.PeriodicX, self.PeriodicY);
        public static implicit operator ParametricSurface((Function1<Number, Vector3D>, Boolean, Boolean) value) => new ParametricSurface(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Function1<Number, Vector3D> evaluator, out Boolean periodicX, out Boolean periodicY) { evaluator = Evaluator; periodicX = PeriodicX; periodicY = PeriodicY; }
        public override bool Equals(object obj) { if (!(obj is ParametricSurface)) return false; var other = (ParametricSurface)obj; return Evaluator.Equals(other.Evaluator) && PeriodicX.Equals(other.PeriodicX) && PeriodicY.Equals(other.PeriodicY); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Evaluator, PeriodicX, PeriodicY);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ParametricSurface self) => new Dynamic(self);
        public static implicit operator ParametricSurface(Dynamic value) => value.As<ParametricSurface>();
        public String TypeName => "ParametricSurface";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Evaluator", (String)"PeriodicX", (String)"PeriodicY");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Evaluator), new Dynamic(PeriodicX), new Dynamic(PeriodicY));
        Boolean ProceduralSurface.PeriodicX => PeriodicX;
        Boolean ProceduralSurface.PeriodicY => PeriodicY;
        // Implemented concept functions and type functions
        // Unimplemented concept functions
        public Vector3D Eval(Vector2D amount) => throw new System.NotImplementedException();
    }
    public readonly partial struct Number: Real<Number>
    {
        public readonly double Value;
        public Number WithValue(double value) => (value);
        public Number(double value) => (Value) = (value);
        public static Number Default = new Number();
        public static Number New(double value) => new Number(value);
        public static implicit operator double(Number self) => self.Value;
        public static implicit operator Number(double value) => new Number(value);
        public override bool Equals(object obj) { if (!(obj is Number)) return false; var other = (Number)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Number self) => new Dynamic(self);
        public static implicit operator Number(Dynamic value) => value.As<Number>();
        public String TypeName => "Number";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(Value);
        public Number FromComponents(Array<Number> numbers) => new Number(numbers[0]);
        // Implemented concept functions and type functions
        public Number StaircaseFloorFunction(Integer steps) => this.Multiply(steps).Floor.Divide(steps);
        public Number StaircaseCeilingFunction(Integer steps) => this.Multiply(steps).Ceiling.Divide(steps);
        public Number StaircaseRoundFunction(Integer steps) => this.Multiply(steps).Round.Divide(steps);
        public Vector2D CircleFunction => this.Turns.CircleFunction;
        public Vector2D LissajousFunction(Number kx, Number ky) => this.Turns.LissajousFunction(kx, ky);
        public Vector2D ButterflyCurveFunction => this.Turns.Divide(((Number)6)).ButterflyCurveFunction;
        public Vector2D ParabolaFunction2D => this.Tuple2(this.ParabolaFunction);
        public Vector2D LineFunction2D(Number m, Number b) => this.Tuple2(this.LinearFunction(m, b));
        public Vector2D SinFunction2D => this.Tuple2(this.Turns.Sin);
        public Vector2D CosFunction2D => this.Tuple2(this.Turns.Cos);
        public Vector2D TanFunction2D => this.Tuple2(this.Turns.Tan);
        public Vector3D HelixFunction(Number revs) => this.Multiply(revs).Turns.Sin.Tuple3(this.Multiply(revs).Turns.Cos, this);
        public Angle Acos => Intrinsics.Acos(this);
        public Angle Asin => Intrinsics.Asin(this);
        public Angle Atan => Intrinsics.Atan(this);
        public Number Pow(Number y) => Intrinsics.Pow(this, y);
        public Number Log(Number y) => Intrinsics.Log(this, y);
        public Number Ln => Intrinsics.Ln(this);
        public Number Exp => Intrinsics.Exp(this);
        public Number Floor => Intrinsics.Floor(this);
        public Number Ceiling => Intrinsics.Ceiling(this);
        public Number Round => Intrinsics.Round(this);
        public Number Truncate => Intrinsics.Truncate(this);
        public static Number operator +(Number x, Number y) => x.Add(y);
        public Number Add(Number y) => Intrinsics.Add(this, y);
        public static Number operator -(Number x, Number y) => x.Subtract(y);
        public Number Subtract(Number y) => Intrinsics.Subtract(this, y);
        public static Number operator /(Number x, Number y) => x.Divide(y);
        public Number Divide(Number y) => Intrinsics.Divide(this, y);
        public static Number operator *(Number x, Number y) => x.Multiply(y);
        public Number Multiply(Number y) => Intrinsics.Multiply(this, y);
        public static Number operator %(Number x, Number y) => x.Modulo(y);
        public Number Modulo(Number y) => Intrinsics.Modulo(this, y);
        public static Number operator -(Number x) => x.Negative;
        public Number Negative => Intrinsics.Negative(this);
        public static Boolean operator <=(Number x, Number y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Number y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator ==(Number x, Number y) => x.Equals(y);
        public Boolean Equals(Number y) => Intrinsics.Equals(this, y);
        public Number OunceToGram => this.Multiply(((Number)28.349523125));
        public Number TroyOunceToGram => this.Multiply(((Number)31.1034768));
        public Number GrainToMilligram => this.Multiply(((Number)64.79891));
        public Number Mole => this.Multiply(((Number)6.02214076E+23));
        public Number Hundred => this.Multiply(((Integer)100));
        public Number Thousand => this.Multiply(((Integer)1000));
        public Number Million => this.Thousand.Thousand;
        public Number Billion => this.Thousand.Million;
        public Number Inverse => ((Number)1).Divide(this);
        public Number Reciprocal => this.Inverse;
        public Number SquareRoot => this.Pow(((Number)0.5));
        public Number Sqrt => this.SquareRoot;
        public Number SmoothStep => this.Square.Multiply(((Number)3).Subtract(this.Twice));
        public Number MultiplyEpsilon(Number y) => this.Abs.Greater(y.Abs).Multiply(Constants.Epsilon);
        public Boolean AlmostEqual(Number y) => this.Subtract(y).Abs.LessThanOrEquals(this.MultiplyEpsilon(y));
        public Boolean AlmostZero => this.Abs.LessThan(Constants.Epsilon);
        public Boolean AlmostZeroOrOne => this.AlmostEqual(((Integer)0)).Or(this.AlmostEqual(((Integer)1)));
        public Boolean Between(Number min, Number max) => this.GreaterThanOrEquals(min).And(this.LessThanOrEquals(max));
        public Angle Turns => this.Multiply(Constants.TwoPi).Radians;
        public Angle Degrees => this.Divide(((Number)360)).Turns;
        public Angle Gradians => this.Divide(((Number)400)).Turns;
        public Angle Radians => this;
        public Number Magnitude => this.Value;
        public Boolean GtZ => this.GreaterThan(this.Zero);
        public Boolean LtZ => this.LessThan(this.Zero);
        public Boolean GtEqZ => this.GreaterThanOrEquals(this.Zero);
        public Boolean LtEqZ => this.LessThanOrEquals(this.Zero);
        public Boolean IsPositive => this.GtEqZ;
        public Boolean IsNegative => this.LtZ;
        public Integer Sign => this.LtZ ? ((Integer)1).Negative : this.GtZ ? ((Integer)1) : ((Integer)0);
        public Number Abs => this.LtZ ? this.Negative : this;
        public Number ToNumber => this.Component(((Integer)0));
        public Number FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Number b) => this.ToNumber.Compare(b.ToNumber);
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
        public Number MinValue => this.MapComponents((x) => x.MinValue);
        public Number MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Number Clamp(Number a, Number b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Number ClampOne => this.Clamp(this.Zero, this.One);
        public static Boolean operator !=(Number a, Number b) => a.Equals(b).Not;
        public Boolean NotEquals(Number b) => this.Equals(b).Not;
        public Number Half => this.Divide(((Number)2));
        public Number Quarter => this.Divide(((Number)4));
        public Number Tenth => this.Divide(((Number)10));
        public Number Twice => this.Multiply(((Number)2));
        public Number Lerp(Number b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Number Barycentric(Number v2, Number v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public static Boolean operator <(Number a, Number b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Number b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Number a, Number b) => b.LessThan(a);
        public Boolean GreaterThan(Number b) => b.LessThan(this);
        public static Boolean operator >=(Number a, Number b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Number b) => b.LessThanOrEquals(this);
        public Number Lesser(Number b) => this.LessThanOrEquals(b) ? this : b;
        public Number Greater(Number b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Number b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Number LinearFunction(Number m, Number b) => m.Multiply(this).Add(b);
        public Number QuadraticFunction(Number a, Number b, Number c) => a.Multiply(this.Square).Add(b.Multiply(this).Add(c));
        public Number CubicFunction(Number a, Number b, Number c, Number d) => a.Multiply(this.Cube).Add(b.Multiply(this.Square).Add(c.Multiply(this).Add(d)));
        public Number ParabolaFunction => this.Square;
        public Number Pow2 => this.Multiply(this);
        public Number Pow3 => this.Pow2.Multiply(this);
        public Number Pow4 => this.Pow3.Multiply(this);
        public Number Pow5 => this.Pow4.Multiply(this);
        public Number Square => this.Pow2;
        public Number Cube => this.Pow3;
        // Unimplemented concept functions
    }
    public readonly partial struct Integer: WholeNumber<Integer>
    {
        public readonly int Value;
        public Integer WithValue(int value) => (value);
        public Integer(int value) => (Value) = (value);
        public static Integer Default = new Integer();
        public static Integer New(int value) => new Integer(value);
        public static implicit operator int(Integer self) => self.Value;
        public static implicit operator Integer(int value) => new Integer(value);
        public override bool Equals(object obj) { if (!(obj is Integer)) return false; var other = (Integer)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Integer self) => new Dynamic(self);
        public static implicit operator Integer(Dynamic value) => value.As<Integer>();
        public String TypeName => "Integer";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Implemented concept functions and type functions
        public Array<Number> Numbers
        {
            get
            {
                var _var0 = this;
                return this.Map((i) => i.ToNumber.Divide(_var0.ToNumber));
            }
        }
        public Array<Integer> Range => this.Map((i) => i);
        public Array<Vector2D> CirclePoints => this.Fractions.Map((x) => x.Turns.CircleFunction);
        public static Integer operator +(Integer x, Integer y) => x.Add(y);
        public Integer Add(Integer y) => Intrinsics.Add(this, y);
        public static Integer operator -(Integer x, Integer y) => x.Subtract(y);
        public Integer Subtract(Integer y) => Intrinsics.Subtract(this, y);
        public static Integer operator /(Integer x, Integer y) => x.Divide(y);
        public Integer Divide(Integer y) => Intrinsics.Divide(this, y);
        public static Integer operator *(Integer x, Integer y) => x.Multiply(y);
        public Integer Multiply(Integer y) => Intrinsics.Multiply(this, y);
        public static Integer operator %(Integer x, Integer y) => x.Modulo(y);
        public Integer Modulo(Integer y) => Intrinsics.Modulo(this, y);
        public static Integer operator -(Integer x) => x.Negative;
        public Integer Negative => Intrinsics.Negative(this);
        public Number ToNumber => Intrinsics.ToNumber(this);
        public Array<TR> Map<TR>(System.Func<Integer, TR> f) => Intrinsics.Map(this, f);
        public static Boolean operator <=(Integer x, Integer y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Integer y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator ==(Integer x, Integer y) => x.Equals(y);
        public Boolean Equals(Integer y) => Intrinsics.Equals(this, y);
        public Number FloatDivision(Integer y) => this.ToNumber.Divide(y.ToNumber);
        public Array<Number> Fractions
        {
            get
            {
                var _var1 = this;
                return this.Range.Map((i) => i.FloatDivision(_var1));
            }
        }
        public static Boolean operator !=(Integer a, Integer b) => a.Equals(b).Not;
        public Boolean NotEquals(Integer b) => this.Equals(b).Not;
        public static Boolean operator <(Integer a, Integer b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Integer b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Integer a, Integer b) => b.LessThan(a);
        public Boolean GreaterThan(Integer b) => b.LessThan(this);
        public static Boolean operator >=(Integer a, Integer b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Integer b) => b.LessThanOrEquals(this);
        public Integer Lesser(Integer b) => this.LessThanOrEquals(b) ? this : b;
        public Integer Greater(Integer b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Integer b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Integer Pow2 => this.Multiply(this);
        public Integer Pow3 => this.Pow2.Multiply(this);
        public Integer Pow4 => this.Pow3.Multiply(this);
        public Integer Pow5 => this.Pow4.Multiply(this);
        public Integer Square => this.Pow2;
        public Integer Cube => this.Pow3;
        public Integer ParabolaFunction => this.Square;
        // Unimplemented concept functions
    }
    public readonly partial struct String: Value<String>, Orderable<String>, Array<Character>
    {
        public readonly string Value;
        public String WithValue(string value) => (value);
        public String(string value) => (Value) = (value);
        public static String Default = new String();
        public static String New(string value) => new String(value);
        public static implicit operator string(String self) => self.Value;
        public static implicit operator String(string value) => new String(value);
        public override bool Equals(object obj) { if (!(obj is String)) return false; var other = (String)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(String self) => new Dynamic(self);
        public static implicit operator String(Dynamic value) => value.As<String>();
        public String TypeName => "String";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Implemented concept functions and type functions
        public Character this[Integer y] => Intrinsics.At(this, y);
        public Character At(Integer y) => Intrinsics.At(this, y);
        public Integer Count => Intrinsics.Count(this);
        public static Boolean operator <=(String x, String y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(String y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator ==(String x, String y) => x.Equals(y);
        public Boolean Equals(String y) => Intrinsics.Equals(this, y);
        public static Boolean operator !=(String a, String b) => a.Equals(b).Not;
        public Boolean NotEquals(String b) => this.Equals(b).Not;
        public static Boolean operator <(String a, String b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(String b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(String a, String b) => b.LessThan(a);
        public Boolean GreaterThan(String b) => b.LessThan(this);
        public static Boolean operator >=(String a, String b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(String b) => b.LessThanOrEquals(this);
        public String Lesser(String b) => this.LessThanOrEquals(b) ? this : b;
        public String Greater(String b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(String b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
    }
    public readonly partial struct Boolean: Value<Boolean>, Orderable<Boolean>, BooleanOperations<Boolean>
    {
        public readonly bool Value;
        public Boolean WithValue(bool value) => (value);
        public Boolean(bool value) => (Value) = (value);
        public static Boolean Default = new Boolean();
        public static Boolean New(bool value) => new Boolean(value);
        public static implicit operator bool(Boolean self) => self.Value;
        public static implicit operator Boolean(bool value) => new Boolean(value);
        public override bool Equals(object obj) { if (!(obj is Boolean)) return false; var other = (Boolean)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Boolean self) => new Dynamic(self);
        public static implicit operator Boolean(Dynamic value) => value.As<Boolean>();
        public String TypeName => "Boolean";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Implemented concept functions and type functions
        public static Boolean operator &(Boolean x, Boolean y) => x.And(y);
        public Boolean And(Boolean y) => Intrinsics.And(this, y);
        public static Boolean operator |(Boolean x, Boolean y) => x.Or(y);
        public Boolean Or(Boolean y) => Intrinsics.Or(this, y);
        public static Boolean operator !(Boolean x) => x.Not;
        public Boolean Not => Intrinsics.Not(this);
        public static Boolean operator <=(Boolean x, Boolean y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Boolean y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator ==(Boolean x, Boolean y) => x.Equals(y);
        public Boolean Equals(Boolean y) => Intrinsics.Equals(this, y);
        public static Boolean operator !=(Boolean a, Boolean b) => a.Equals(b).Not;
        public Boolean NotEquals(Boolean b) => this.Equals(b).Not;
        public static Boolean operator <(Boolean a, Boolean b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Boolean b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Boolean a, Boolean b) => b.LessThan(a);
        public Boolean GreaterThan(Boolean b) => b.LessThan(this);
        public static Boolean operator >=(Boolean a, Boolean b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Boolean b) => b.LessThanOrEquals(this);
        public Boolean Lesser(Boolean b) => this.LessThanOrEquals(b) ? this : b;
        public Boolean Greater(Boolean b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Boolean b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
    }
    public readonly partial struct Character: Value<Character>, Orderable<Character>
    {
        public readonly char Value;
        public Character WithValue(char value) => (value);
        public Character(char value) => (Value) = (value);
        public static Character Default = new Character();
        public static Character New(char value) => new Character(value);
        public static implicit operator char(Character self) => self.Value;
        public static implicit operator Character(char value) => new Character(value);
        public override bool Equals(object obj) { if (!(obj is Character)) return false; var other = (Character)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Character self) => new Dynamic(self);
        public static implicit operator Character(Dynamic value) => value.As<Character>();
        public String TypeName => "Character";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Implemented concept functions and type functions
        public static Boolean operator <=(Character x, Character y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Character y) => Intrinsics.LessThanOrEquals(this, y);
        public static Boolean operator ==(Character x, Character y) => x.Equals(y);
        public Boolean Equals(Character y) => Intrinsics.Equals(this, y);
        public static Boolean operator !=(Character a, Character b) => a.Equals(b).Not;
        public Boolean NotEquals(Character b) => this.Equals(b).Not;
        public static Boolean operator <(Character a, Character b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Character b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Character a, Character b) => b.LessThan(a);
        public Boolean GreaterThan(Character b) => b.LessThan(this);
        public static Boolean operator >=(Character a, Character b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Character b) => b.LessThanOrEquals(this);
        public Character Lesser(Character b) => this.LessThanOrEquals(b) ? this : b;
        public Character Greater(Character b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Character b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
    }
    public readonly partial struct Type
    {
        public readonly System.Type Value;
        public Type WithValue(System.Type value) => (value);
        public Type(System.Type value) => (Value) = (value);
        public static Type Default = new Type();
        public static Type New(System.Type value) => new Type(value);
        public static implicit operator System.Type(Type self) => self.Value;
        public static implicit operator Type(System.Type value) => new Type(value);
        public override bool Equals(object obj) { if (!(obj is Type)) return false; var other = (Type)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Type self) => new Dynamic(self);
        public static implicit operator Type(Dynamic value) => value.As<Type>();
        public String TypeName => "Type";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Implemented concept functions and type functions
        public Dynamic New(Array<Any> args) => Intrinsics.New(this, args);
        // Unimplemented concept functions
    }
    public readonly partial struct Error
    {
        public static Error Default = new Error();
        public static Error New() => new Error();
        public override bool Equals(object obj) => true;public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Error self) => new Dynamic(self);
        public static implicit operator Error(Dynamic value) => value.As<Error>();
        public String TypeName => "Error";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>();
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tuple2<T0, T1>
    {
        public readonly T0 X0;
        public readonly T1 X1;
        public Tuple2<T0, T1> WithX0(T0 x0) => (x0, X1);
        public Tuple2<T0, T1> WithX1(T1 x1) => (X0, x1);
        public Tuple2(T0 x0, T1 x1) => (X0, X1) = (x0, x1);
        public static Tuple2<T0, T1> Default = new Tuple2<T0, T1>();
        public static Tuple2<T0, T1> New(T0 x0, T1 x1) => new Tuple2<T0, T1>(x0, x1);
        public static implicit operator (T0, T1)(Tuple2<T0, T1> self) => (self.X0, self.X1);
        public static implicit operator Tuple2<T0, T1>((T0, T1) value) => new Tuple2<T0, T1>(value.Item1, value.Item2);
        public void Deconstruct(out T0 x0, out T1 x1) { x0 = X0; x1 = X1; }
        public override bool Equals(object obj) { if (!(obj is Tuple2<T0, T1>)) return false; var other = (Tuple2<T0, T1>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tuple2<T0, T1> self) => new Dynamic(self);
        public static implicit operator Tuple2<T0, T1>(Dynamic value) => value.As<Tuple2<T0, T1>>();
        public String TypeName => "Tuple2<T0, T1>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tuple3<T0, T1, T2>
    {
        public readonly T0 X0;
        public readonly T1 X1;
        public readonly T2 X2;
        public Tuple3<T0, T1, T2> WithX0(T0 x0) => (x0, X1, X2);
        public Tuple3<T0, T1, T2> WithX1(T1 x1) => (X0, x1, X2);
        public Tuple3<T0, T1, T2> WithX2(T2 x2) => (X0, X1, x2);
        public Tuple3(T0 x0, T1 x1, T2 x2) => (X0, X1, X2) = (x0, x1, x2);
        public static Tuple3<T0, T1, T2> Default = new Tuple3<T0, T1, T2>();
        public static Tuple3<T0, T1, T2> New(T0 x0, T1 x1, T2 x2) => new Tuple3<T0, T1, T2>(x0, x1, x2);
        public static implicit operator (T0, T1, T2)(Tuple3<T0, T1, T2> self) => (self.X0, self.X1, self.X2);
        public static implicit operator Tuple3<T0, T1, T2>((T0, T1, T2) value) => new Tuple3<T0, T1, T2>(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2) { x0 = X0; x1 = X1; x2 = X2; }
        public override bool Equals(object obj) { if (!(obj is Tuple3<T0, T1, T2>)) return false; var other = (Tuple3<T0, T1, T2>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tuple3<T0, T1, T2> self) => new Dynamic(self);
        public static implicit operator Tuple3<T0, T1, T2>(Dynamic value) => value.As<Tuple3<T0, T1, T2>>();
        public String TypeName => "Tuple3<T0, T1, T2>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tuple4<T0, T1, T2, T3>
    {
        public readonly T0 X0;
        public readonly T1 X1;
        public readonly T2 X2;
        public readonly T3 X3;
        public Tuple4<T0, T1, T2, T3> WithX0(T0 x0) => (x0, X1, X2, X3);
        public Tuple4<T0, T1, T2, T3> WithX1(T1 x1) => (X0, x1, X2, X3);
        public Tuple4<T0, T1, T2, T3> WithX2(T2 x2) => (X0, X1, x2, X3);
        public Tuple4<T0, T1, T2, T3> WithX3(T3 x3) => (X0, X1, X2, x3);
        public Tuple4(T0 x0, T1 x1, T2 x2, T3 x3) => (X0, X1, X2, X3) = (x0, x1, x2, x3);
        public static Tuple4<T0, T1, T2, T3> Default = new Tuple4<T0, T1, T2, T3>();
        public static Tuple4<T0, T1, T2, T3> New(T0 x0, T1 x1, T2 x2, T3 x3) => new Tuple4<T0, T1, T2, T3>(x0, x1, x2, x3);
        public static implicit operator (T0, T1, T2, T3)(Tuple4<T0, T1, T2, T3> self) => (self.X0, self.X1, self.X2, self.X3);
        public static implicit operator Tuple4<T0, T1, T2, T3>((T0, T1, T2, T3) value) => new Tuple4<T0, T1, T2, T3>(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; }
        public override bool Equals(object obj) { if (!(obj is Tuple4<T0, T1, T2, T3>)) return false; var other = (Tuple4<T0, T1, T2, T3>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tuple4<T0, T1, T2, T3> self) => new Dynamic(self);
        public static implicit operator Tuple4<T0, T1, T2, T3>(Dynamic value) => value.As<Tuple4<T0, T1, T2, T3>>();
        public String TypeName => "Tuple4<T0, T1, T2, T3>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tuple5<T0, T1, T2, T3, T4>
    {
        public readonly T0 X0;
        public readonly T1 X1;
        public readonly T2 X2;
        public readonly T3 X3;
        public readonly T4 X4;
        public Tuple5<T0, T1, T2, T3, T4> WithX0(T0 x0) => (x0, X1, X2, X3, X4);
        public Tuple5<T0, T1, T2, T3, T4> WithX1(T1 x1) => (X0, x1, X2, X3, X4);
        public Tuple5<T0, T1, T2, T3, T4> WithX2(T2 x2) => (X0, X1, x2, X3, X4);
        public Tuple5<T0, T1, T2, T3, T4> WithX3(T3 x3) => (X0, X1, X2, x3, X4);
        public Tuple5<T0, T1, T2, T3, T4> WithX4(T4 x4) => (X0, X1, X2, X3, x4);
        public Tuple5(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4) => (X0, X1, X2, X3, X4) = (x0, x1, x2, x3, x4);
        public static Tuple5<T0, T1, T2, T3, T4> Default = new Tuple5<T0, T1, T2, T3, T4>();
        public static Tuple5<T0, T1, T2, T3, T4> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4) => new Tuple5<T0, T1, T2, T3, T4>(x0, x1, x2, x3, x4);
        public static implicit operator (T0, T1, T2, T3, T4)(Tuple5<T0, T1, T2, T3, T4> self) => (self.X0, self.X1, self.X2, self.X3, self.X4);
        public static implicit operator Tuple5<T0, T1, T2, T3, T4>((T0, T1, T2, T3, T4) value) => new Tuple5<T0, T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; }
        public override bool Equals(object obj) { if (!(obj is Tuple5<T0, T1, T2, T3, T4>)) return false; var other = (Tuple5<T0, T1, T2, T3, T4>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tuple5<T0, T1, T2, T3, T4> self) => new Dynamic(self);
        public static implicit operator Tuple5<T0, T1, T2, T3, T4>(Dynamic value) => value.As<Tuple5<T0, T1, T2, T3, T4>>();
        public String TypeName => "Tuple5<T0, T1, T2, T3, T4>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tuple6<T0, T1, T2, T3, T4, T5>
    {
        public readonly T0 X0;
        public readonly T1 X1;
        public readonly T2 X2;
        public readonly T3 X3;
        public readonly T4 X4;
        public readonly T5 X5;
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX0(T0 x0) => (x0, X1, X2, X3, X4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX1(T1 x1) => (X0, x1, X2, X3, X4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX2(T2 x2) => (X0, X1, x2, X3, X4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX3(T3 x3) => (X0, X1, X2, x3, X4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX4(T4 x4) => (X0, X1, X2, X3, x4, X5);
        public Tuple6<T0, T1, T2, T3, T4, T5> WithX5(T5 x5) => (X0, X1, X2, X3, X4, x5);
        public Tuple6(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5) => (X0, X1, X2, X3, X4, X5) = (x0, x1, x2, x3, x4, x5);
        public static Tuple6<T0, T1, T2, T3, T4, T5> Default = new Tuple6<T0, T1, T2, T3, T4, T5>();
        public static Tuple6<T0, T1, T2, T3, T4, T5> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5) => new Tuple6<T0, T1, T2, T3, T4, T5>(x0, x1, x2, x3, x4, x5);
        public static implicit operator (T0, T1, T2, T3, T4, T5)(Tuple6<T0, T1, T2, T3, T4, T5> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5);
        public static implicit operator Tuple6<T0, T1, T2, T3, T4, T5>((T0, T1, T2, T3, T4, T5) value) => new Tuple6<T0, T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; }
        public override bool Equals(object obj) { if (!(obj is Tuple6<T0, T1, T2, T3, T4, T5>)) return false; var other = (Tuple6<T0, T1, T2, T3, T4, T5>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tuple6<T0, T1, T2, T3, T4, T5> self) => new Dynamic(self);
        public static implicit operator Tuple6<T0, T1, T2, T3, T4, T5>(Dynamic value) => value.As<Tuple6<T0, T1, T2, T3, T4, T5>>();
        public String TypeName => "Tuple6<T0, T1, T2, T3, T4, T5>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tuple7<T0, T1, T2, T3, T4, T5, T6>
    {
        public readonly T0 X0;
        public readonly T1 X1;
        public readonly T2 X2;
        public readonly T3 X3;
        public readonly T4 X4;
        public readonly T5 X5;
        public readonly T6 X6;
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX0(T0 x0) => (x0, X1, X2, X3, X4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX1(T1 x1) => (X0, x1, X2, X3, X4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX2(T2 x2) => (X0, X1, x2, X3, X4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX3(T3 x3) => (X0, X1, X2, x3, X4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX4(T4 x4) => (X0, X1, X2, X3, x4, X5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX5(T5 x5) => (X0, X1, X2, X3, X4, x5, X6);
        public Tuple7<T0, T1, T2, T3, T4, T5, T6> WithX6(T6 x6) => (X0, X1, X2, X3, X4, X5, x6);
        public Tuple7(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6) => (X0, X1, X2, X3, X4, X5, X6) = (x0, x1, x2, x3, x4, x5, x6);
        public static Tuple7<T0, T1, T2, T3, T4, T5, T6> Default = new Tuple7<T0, T1, T2, T3, T4, T5, T6>();
        public static Tuple7<T0, T1, T2, T3, T4, T5, T6> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(x0, x1, x2, x3, x4, x5, x6);
        public static implicit operator (T0, T1, T2, T3, T4, T5, T6)(Tuple7<T0, T1, T2, T3, T4, T5, T6> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5, self.X6);
        public static implicit operator Tuple7<T0, T1, T2, T3, T4, T5, T6>((T0, T1, T2, T3, T4, T5, T6) value) => new Tuple7<T0, T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5, out T6 x6) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; x6 = X6; }
        public override bool Equals(object obj) { if (!(obj is Tuple7<T0, T1, T2, T3, T4, T5, T6>)) return false; var other = (Tuple7<T0, T1, T2, T3, T4, T5, T6>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5) && X6.Equals(other.X6); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5, X6);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tuple7<T0, T1, T2, T3, T4, T5, T6> self) => new Dynamic(self);
        public static implicit operator Tuple7<T0, T1, T2, T3, T4, T5, T6>(Dynamic value) => value.As<Tuple7<T0, T1, T2, T3, T4, T5, T6>>();
        public String TypeName => "Tuple7<T0, T1, T2, T3, T4, T5, T6>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5", (String)"X6");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5), new Dynamic(X6));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>
    {
        public readonly T0 X0;
        public readonly T1 X1;
        public readonly T2 X2;
        public readonly T3 X3;
        public readonly T4 X4;
        public readonly T5 X5;
        public readonly T6 X6;
        public readonly T7 X7;
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX0(T0 x0) => (x0, X1, X2, X3, X4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX1(T1 x1) => (X0, x1, X2, X3, X4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX2(T2 x2) => (X0, X1, x2, X3, X4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX3(T3 x3) => (X0, X1, X2, x3, X4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX4(T4 x4) => (X0, X1, X2, X3, x4, X5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX5(T5 x5) => (X0, X1, X2, X3, X4, x5, X6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX6(T6 x6) => (X0, X1, X2, X3, X4, X5, x6, X7);
        public Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> WithX7(T7 x7) => (X0, X1, X2, X3, X4, X5, X6, x7);
        public Tuple8(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7) => (X0, X1, X2, X3, X4, X5, X6, X7) = (x0, x1, x2, x3, x4, x5, x6, x7);
        public static Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> Default = new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>();
        public static Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(x0, x1, x2, x3, x4, x5, x6, x7);
        public static implicit operator (T0, T1, T2, T3, T4, T5, T6, T7)(Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5, self.X6, self.X7);
        public static implicit operator Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>((T0, T1, T2, T3, T4, T5, T6, T7) value) => new Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, value.Item8);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5, out T6 x6, out T7 x7) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; x6 = X6; x7 = X7; }
        public override bool Equals(object obj) { if (!(obj is Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>)) return false; var other = (Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5) && X6.Equals(other.X6) && X7.Equals(other.X7); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5, X6, X7);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tuple8<T0, T1, T2, T3, T4, T5, T6, T7> self) => new Dynamic(self);
        public static implicit operator Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>(Dynamic value) => value.As<Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>>();
        public String TypeName => "Tuple8<T0, T1, T2, T3, T4, T5, T6, T7>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5", (String)"X6", (String)"X7");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5), new Dynamic(X6), new Dynamic(X7));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        public readonly T0 X0;
        public readonly T1 X1;
        public readonly T2 X2;
        public readonly T3 X3;
        public readonly T4 X4;
        public readonly T5 X5;
        public readonly T6 X6;
        public readonly T7 X7;
        public readonly T8 X8;
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX0(T0 x0) => (x0, X1, X2, X3, X4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX1(T1 x1) => (X0, x1, X2, X3, X4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX2(T2 x2) => (X0, X1, x2, X3, X4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX3(T3 x3) => (X0, X1, X2, x3, X4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX4(T4 x4) => (X0, X1, X2, X3, x4, X5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX5(T5 x5) => (X0, X1, X2, X3, X4, x5, X6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX6(T6 x6) => (X0, X1, X2, X3, X4, X5, x6, X7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX7(T7 x7) => (X0, X1, X2, X3, X4, X5, X6, x7, X8);
        public Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> WithX8(T8 x8) => (X0, X1, X2, X3, X4, X5, X6, X7, x8);
        public Tuple9(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7, T8 x8) => (X0, X1, X2, X3, X4, X5, X6, X7, X8) = (x0, x1, x2, x3, x4, x5, x6, x7, x8);
        public static Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> Default = new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>();
        public static Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7, T8 x8) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(x0, x1, x2, x3, x4, x5, x6, x7, x8);
        public static implicit operator (T0, T1, T2, T3, T4, T5, T6, T7, T8)(Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5, self.X6, self.X7, self.X8);
        public static implicit operator Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>((T0, T1, T2, T3, T4, T5, T6, T7, T8) value) => new Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, value.Item8, value.Item9);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5, out T6 x6, out T7 x7, out T8 x8) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; x6 = X6; x7 = X7; x8 = X8; }
        public override bool Equals(object obj) { if (!(obj is Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>)) return false; var other = (Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5) && X6.Equals(other.X6) && X7.Equals(other.X7) && X8.Equals(other.X8); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5, X6, X7, X8);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8> self) => new Dynamic(self);
        public static implicit operator Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Dynamic value) => value.As<Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>>();
        public String TypeName => "Tuple9<T0, T1, T2, T3, T4, T5, T6, T7, T8>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5", (String)"X6", (String)"X7", (String)"X8");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5), new Dynamic(X6), new Dynamic(X7), new Dynamic(X8));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public readonly T0 X0;
        public readonly T1 X1;
        public readonly T2 X2;
        public readonly T3 X3;
        public readonly T4 X4;
        public readonly T5 X5;
        public readonly T6 X6;
        public readonly T7 X7;
        public readonly T8 X8;
        public readonly T9 X9;
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX0(T0 x0) => (x0, X1, X2, X3, X4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX1(T1 x1) => (X0, x1, X2, X3, X4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX2(T2 x2) => (X0, X1, x2, X3, X4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX3(T3 x3) => (X0, X1, X2, x3, X4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX4(T4 x4) => (X0, X1, X2, X3, x4, X5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX5(T5 x5) => (X0, X1, X2, X3, X4, x5, X6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX6(T6 x6) => (X0, X1, X2, X3, X4, X5, x6, X7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX7(T7 x7) => (X0, X1, X2, X3, X4, X5, X6, x7, X8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX8(T8 x8) => (X0, X1, X2, X3, X4, X5, X6, X7, x8, X9);
        public Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WithX9(T9 x9) => (X0, X1, X2, X3, X4, X5, X6, X7, X8, x9);
        public Tuple10(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7, T8 x8, T9 x9) => (X0, X1, X2, X3, X4, X5, X6, X7, X8, X9) = (x0, x1, x2, x3, x4, x5, x6, x7, x8, x9);
        public static Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Default = new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        public static Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> New(T0 x0, T1 x1, T2 x2, T3 x3, T4 x4, T5 x5, T6 x6, T7 x7, T8 x8, T9 x9) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(x0, x1, x2, x3, x4, x5, x6, x7, x8, x9);
        public static implicit operator (T0, T1, T2, T3, T4, T5, T6, T7, T8, T9)(Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> self) => (self.X0, self.X1, self.X2, self.X3, self.X4, self.X5, self.X6, self.X7, self.X8, self.X9);
        public static implicit operator Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>((T0, T1, T2, T3, T4, T5, T6, T7, T8, T9) value) => new Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, value.Item8, value.Item9, value.Item10);
        public void Deconstruct(out T0 x0, out T1 x1, out T2 x2, out T3 x3, out T4 x4, out T5 x5, out T6 x6, out T7 x7, out T8 x8, out T9 x9) { x0 = X0; x1 = X1; x2 = X2; x3 = X3; x4 = X4; x5 = X5; x6 = X6; x7 = X7; x8 = X8; x9 = X9; }
        public override bool Equals(object obj) { if (!(obj is Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>)) return false; var other = (Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>)obj; return X0.Equals(other.X0) && X1.Equals(other.X1) && X2.Equals(other.X2) && X3.Equals(other.X3) && X4.Equals(other.X4) && X5.Equals(other.X5) && X6.Equals(other.X6) && X7.Equals(other.X7) && X8.Equals(other.X8) && X9.Equals(other.X9); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1, X2, X3, X4, X5, X6, X7, X8, X9);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> self) => new Dynamic(self);
        public static implicit operator Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Dynamic value) => value.As<Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>();
        public String TypeName => "Tuple10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X0", (String)"X1", (String)"X2", (String)"X3", (String)"X4", (String)"X5", (String)"X6", (String)"X7", (String)"X8", (String)"X9");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X0), new Dynamic(X1), new Dynamic(X2), new Dynamic(X3), new Dynamic(X4), new Dynamic(X5), new Dynamic(X6), new Dynamic(X7), new Dynamic(X8), new Dynamic(X9));
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Function4<T0, T1, T2, T3, TR>
    {
        public static Function4<T0, T1, T2, T3, TR> Default = new Function4<T0, T1, T2, T3, TR>();
        public static Function4<T0, T1, T2, T3, TR> New() => new Function4<T0, T1, T2, T3, TR>();
        public override bool Equals(object obj) => true;public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Function4<T0, T1, T2, T3, TR> self) => new Dynamic(self);
        public static implicit operator Function4<T0, T1, T2, T3, TR>(Dynamic value) => value.As<Function4<T0, T1, T2, T3, TR>>();
        public String TypeName => "Function4<T0, T1, T2, T3, TR>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>();
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        public TR Invoke(T0 a0, T1 a1, T2 a2, T3 a3) => Intrinsics.Invoke(this, a0, a1, a2, a3);
        // Unimplemented concept functions
    }
    public readonly partial struct Function5<T0, T1, T2, T3, T4, TR>
    {
        public static Function5<T0, T1, T2, T3, T4, TR> Default = new Function5<T0, T1, T2, T3, T4, TR>();
        public static Function5<T0, T1, T2, T3, T4, TR> New() => new Function5<T0, T1, T2, T3, T4, TR>();
        public override bool Equals(object obj) => true;public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Function5<T0, T1, T2, T3, T4, TR> self) => new Dynamic(self);
        public static implicit operator Function5<T0, T1, T2, T3, T4, TR>(Dynamic value) => value.As<Function5<T0, T1, T2, T3, T4, TR>>();
        public String TypeName => "Function5<T0, T1, T2, T3, T4, TR>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>();
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Function6<T0, T1, T2, T3, T4, T5, TR>
    {
        public static Function6<T0, T1, T2, T3, T4, T5, TR> Default = new Function6<T0, T1, T2, T3, T4, T5, TR>();
        public static Function6<T0, T1, T2, T3, T4, T5, TR> New() => new Function6<T0, T1, T2, T3, T4, T5, TR>();
        public override bool Equals(object obj) => true;public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Function6<T0, T1, T2, T3, T4, T5, TR> self) => new Dynamic(self);
        public static implicit operator Function6<T0, T1, T2, T3, T4, T5, TR>(Dynamic value) => value.As<Function6<T0, T1, T2, T3, T4, T5, TR>>();
        public String TypeName => "Function6<T0, T1, T2, T3, T4, T5, TR>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>();
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Function7<T0, T1, T2, T3, T4, T5, T6, TR>
    {
        public static Function7<T0, T1, T2, T3, T4, T5, T6, TR> Default = new Function7<T0, T1, T2, T3, T4, T5, T6, TR>();
        public static Function7<T0, T1, T2, T3, T4, T5, T6, TR> New() => new Function7<T0, T1, T2, T3, T4, T5, T6, TR>();
        public override bool Equals(object obj) => true;public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Function7<T0, T1, T2, T3, T4, T5, T6, TR> self) => new Dynamic(self);
        public static implicit operator Function7<T0, T1, T2, T3, T4, T5, T6, TR>(Dynamic value) => value.As<Function7<T0, T1, T2, T3, T4, T5, T6, TR>>();
        public String TypeName => "Function7<T0, T1, T2, T3, T4, T5, T6, TR>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>();
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>
    {
        public static Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR> Default = new Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>();
        public static Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR> New() => new Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>();
        public override bool Equals(object obj) => true;public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR> self) => new Dynamic(self);
        public static implicit operator Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>(Dynamic value) => value.As<Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>>();
        public String TypeName => "Function8<T0, T1, T2, T3, T4, T5, T6, T7, TR>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>();
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>
    {
        public static Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> Default = new Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>();
        public static Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> New() => new Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>();
        public override bool Equals(object obj) => true;public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR> self) => new Dynamic(self);
        public static implicit operator Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>(Dynamic value) => value.As<Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>>();
        public String TypeName => "Function9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TR>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>();
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>
    {
        public static Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> Default = new Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>();
        public static Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> New() => new Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>();
        public override bool Equals(object obj) => true;public override int GetHashCode() => Intrinsics.CombineHashCodes();
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> self) => new Dynamic(self);
        public static implicit operator Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>(Dynamic value) => value.As<Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>>();
        public String TypeName => "Function10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>();
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>();
        // Implemented concept functions and type functions
        // Unimplemented concept functions
    }
    public readonly partial struct Unit: Real<Unit>
    {
        public readonly Number Value;
        public Unit WithValue(Number value) => (value);
        public Unit(Number value) => (Value) = (value);
        public static Unit Default = new Unit();
        public static Unit New(Number value) => new Unit(value);
        public static implicit operator Number(Unit self) => self.Value;
        public static implicit operator Unit(Number value) => new Unit(value);
        public static implicit operator Unit(Integer value) => new Unit(value);
        public static implicit operator Unit(int value) => new Integer(value);
        public static implicit operator Unit(double value) => new Number(value);
        public static implicit operator double(Unit value) => value.Value;
        public override bool Equals(object obj) { if (!(obj is Unit)) return false; var other = (Unit)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Unit self) => new Dynamic(self);
        public static implicit operator Unit(Dynamic value) => value.As<Unit>();
        public String TypeName => "Unit";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(Value);
        public Unit FromComponents(Array<Number> numbers) => new Unit(numbers[0]);
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
        public static Unit operator *(Unit x, Unit y) => x.FromNumber(x.ToNumber.Multiply(y.ToNumber));
        public Unit Multiply(Unit y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Unit operator /(Unit x, Unit y) => x.FromNumber(x.ToNumber.Divide(y.ToNumber));
        public Unit Divide(Unit y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Unit operator %(Unit x, Unit y) => x.FromNumber(x.ToNumber.Modulo(y.ToNumber));
        public Unit Modulo(Unit y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public Number ToNumber => this.Component(((Integer)0));
        public Unit FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Unit b) => this.ToNumber.Compare(b.ToNumber);
        public static Unit operator +(Unit x, Number y) => x.FromNumber(x.ToNumber.Add(y));
        public Unit Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
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
        public Unit MinValue => this.MapComponents((x) => x.MinValue);
        public Unit MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Unit a, Unit b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Unit Clamp(Unit a, Unit b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Unit ClampOne => this.Clamp(this.Zero, this.One);
        public static Unit operator *(Unit x, Number s)
        {
            var _var2 = s;
            return x.MapComponents((i) => i.Multiply(_var2));
        }
        public Unit Multiply(Number s)
        {
            var _var3 = s;
            return this.MapComponents((i) => i.Multiply(_var3));
        }
        public static Unit operator /(Unit x, Number s)
        {
            var _var4 = s;
            return x.MapComponents((i) => i.Divide(_var4));
        }
        public Unit Divide(Number s)
        {
            var _var5 = s;
            return this.MapComponents((i) => i.Divide(_var5));
        }
        public static Unit operator %(Unit x, Number s)
        {
            var _var6 = s;
            return x.MapComponents((i) => i.Modulo(_var6));
        }
        public Unit Modulo(Number s)
        {
            var _var7 = s;
            return this.MapComponents((i) => i.Modulo(_var7));
        }
        public static Unit operator +(Unit x, Unit y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Unit Add(Unit y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Unit operator -(Unit x, Unit y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Unit Subtract(Unit y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Unit operator -(Unit x) => x.MapComponents((a) => a.Negative);
        public Unit Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Unit a, Unit b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Unit b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Unit a, Unit b) => a.Equals(b).Not;
        public Boolean NotEquals(Unit b) => this.Equals(b).Not;
        public Unit Half => this.Divide(((Number)2));
        public Unit Quarter => this.Divide(((Number)4));
        public Unit Tenth => this.Divide(((Number)10));
        public Unit Twice => this.Multiply(((Number)2));
        public Unit Lerp(Unit b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Unit Barycentric(Unit v2, Unit v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public static Boolean operator <(Unit a, Unit b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Unit b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Unit a, Unit b) => b.LessThan(a);
        public Boolean GreaterThan(Unit b) => b.LessThan(this);
        public static Boolean operator >=(Unit a, Unit b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Unit b) => b.LessThanOrEquals(this);
        public Unit Lesser(Unit b) => this.LessThanOrEquals(b) ? this : b;
        public Unit Greater(Unit b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Unit b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        public Unit LinearFunction(Number m, Number b) => m.Multiply(this).Add(b);
        public Unit QuadraticFunction(Number a, Number b, Number c) => a.Multiply(this.Square).Add(b.Multiply(this).Add(c));
        public Unit CubicFunction(Number a, Number b, Number c, Number d) => a.Multiply(this.Cube).Add(b.Multiply(this.Square).Add(c.Multiply(this).Add(d)));
        public Unit ParabolaFunction => this.Square;
        public Unit Pow2 => this.Multiply(this);
        public Unit Pow3 => this.Pow2.Multiply(this);
        public Unit Pow4 => this.Pow3.Multiply(this);
        public Unit Pow5 => this.Pow4.Multiply(this);
        public Unit Square => this.Pow2;
        public Unit Cube => this.Pow3;
        // Unimplemented concept functions
        public static Unit Multiply(Number other, Unit self) => self.Multiply(other);
        public static Unit operator *(Number other, Unit self) => Multiply(other, self);
        public Unit Inverse => throw new System.NotImplementedException();
        public static Boolean operator <=(Unit x, Unit y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Unit y) => throw new System.NotImplementedException();
    }
    public readonly partial struct Probability: Measure<Probability>
    {
        public readonly Number Value;
        public Probability WithValue(Number value) => (value);
        public Probability(Number value) => (Value) = (value);
        public static Probability Default = new Probability();
        public static Probability New(Number value) => new Probability(value);
        public static implicit operator Number(Probability self) => self.Value;
        public static implicit operator Probability(Number value) => new Probability(value);
        public static implicit operator Probability(Integer value) => new Probability(value);
        public static implicit operator Probability(int value) => new Integer(value);
        public static implicit operator Probability(double value) => new Number(value);
        public static implicit operator double(Probability value) => value.Value;
        public override bool Equals(object obj) { if (!(obj is Probability)) return false; var other = (Probability)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Probability self) => new Dynamic(self);
        public static implicit operator Probability(Dynamic value) => value.As<Probability>();
        public String TypeName => "Probability";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(Value);
        public Probability FromComponents(Array<Number> numbers) => new Probability(numbers[0]);
        // Implemented concept functions and type functions
        public static Probability operator *(Probability x, Probability y) => x.FromNumber(x.ToNumber.Multiply(y.ToNumber));
        public Probability Multiply(Probability y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Probability operator /(Probability x, Probability y) => x.FromNumber(x.ToNumber.Divide(y.ToNumber));
        public Probability Divide(Probability y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Probability operator %(Probability x, Probability y) => x.FromNumber(x.ToNumber.Modulo(y.ToNumber));
        public Probability Modulo(Probability y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public Number ToNumber => this.Component(((Integer)0));
        public Probability FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Probability b) => this.ToNumber.Compare(b.ToNumber);
        public static Probability operator +(Probability x, Number y) => x.FromNumber(x.ToNumber.Add(y));
        public Probability Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
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
        public Probability MinValue => this.MapComponents((x) => x.MinValue);
        public Probability MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Probability a, Probability b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Probability Clamp(Probability a, Probability b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Probability ClampOne => this.Clamp(this.Zero, this.One);
        public static Probability operator *(Probability x, Number s)
        {
            var _var8 = s;
            return x.MapComponents((i) => i.Multiply(_var8));
        }
        public Probability Multiply(Number s)
        {
            var _var9 = s;
            return this.MapComponents((i) => i.Multiply(_var9));
        }
        public static Probability operator /(Probability x, Number s)
        {
            var _var10 = s;
            return x.MapComponents((i) => i.Divide(_var10));
        }
        public Probability Divide(Number s)
        {
            var _var11 = s;
            return this.MapComponents((i) => i.Divide(_var11));
        }
        public static Probability operator %(Probability x, Number s)
        {
            var _var12 = s;
            return x.MapComponents((i) => i.Modulo(_var12));
        }
        public Probability Modulo(Number s)
        {
            var _var13 = s;
            return this.MapComponents((i) => i.Modulo(_var13));
        }
        public static Probability operator +(Probability x, Probability y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Probability Add(Probability y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Probability operator -(Probability x, Probability y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Probability Subtract(Probability y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Probability operator -(Probability x) => x.MapComponents((a) => a.Negative);
        public Probability Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Probability a, Probability b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Probability b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Probability a, Probability b) => a.Equals(b).Not;
        public Boolean NotEquals(Probability b) => this.Equals(b).Not;
        public Probability Half => this.Divide(((Number)2));
        public Probability Quarter => this.Divide(((Number)4));
        public Probability Tenth => this.Divide(((Number)10));
        public Probability Twice => this.Multiply(((Number)2));
        public Probability Lerp(Probability b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Probability Barycentric(Probability v2, Probability v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public static Boolean operator <(Probability a, Probability b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Probability b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Probability a, Probability b) => b.LessThan(a);
        public Boolean GreaterThan(Probability b) => b.LessThan(this);
        public static Boolean operator >=(Probability a, Probability b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Probability b) => b.LessThanOrEquals(this);
        public Probability Lesser(Probability b) => this.LessThanOrEquals(b) ? this : b;
        public Probability Greater(Probability b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Probability b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
        public static Boolean operator <=(Probability x, Probability y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Probability y) => throw new System.NotImplementedException();
        public static Probability Multiply(Number other, Probability self) => self.Multiply(other);
        public static Probability operator *(Number other, Probability self) => Multiply(other, self);
    }
    public readonly partial struct Complex: Vector<Complex>
    {
        public readonly Number Real;
        public readonly Number Imaginary;
        public Complex WithReal(Number real) => (real, Imaginary);
        public Complex WithImaginary(Number imaginary) => (Real, imaginary);
        public Complex(Number real, Number imaginary) => (Real, Imaginary) = (real, imaginary);
        public static Complex Default = new Complex();
        public static Complex New(Number real, Number imaginary) => new Complex(real, imaginary);
        public static implicit operator (Number, Number)(Complex self) => (self.Real, self.Imaginary);
        public static implicit operator Complex((Number, Number) value) => new Complex(value.Item1, value.Item2);
        public void Deconstruct(out Number real, out Number imaginary) { real = Real; imaginary = Imaginary; }
        public override bool Equals(object obj) { if (!(obj is Complex)) return false; var other = (Complex)obj; return Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Real, Imaginary);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Complex self) => new Dynamic(self);
        public static implicit operator Complex(Dynamic value) => value.As<Complex>();
        public String TypeName => "Complex";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Real", (String)"Imaginary");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Real), new Dynamic(Imaginary));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(Real, Imaginary);
        public Complex FromComponents(Array<Number> numbers) => new Complex(numbers[0], numbers[1]);
        // Implemented concept functions and type functions
        public Integer Count => ((Integer)2);
        public Number this[Integer n] => n.Equals(((Integer)0)) ? this.Real : this.Imaginary;
        public Number At(Integer n) => n.Equals(((Integer)0)) ? this.Real : this.Imaginary;
        public static Complex operator *(Complex x, Complex y) => x.ZipComponents(y, (a, b) => a.Multiply(b));
        public Complex Multiply(Complex y) => this.ZipComponents(y, (a, b) => a.Multiply(b));
        public static Complex operator /(Complex x, Complex y) => x.ZipComponents(y, (a, b) => a.Divide(b));
        public Complex Divide(Complex y) => this.ZipComponents(y, (a, b) => a.Divide(b));
        public static Complex operator %(Complex x, Complex y) => x.ZipComponents(y, (a, b) => a.Modulo(b));
        public Complex Modulo(Complex y) => this.ZipComponents(y, (a, b) => a.Modulo(b));
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
        public Complex MinValue => this.MapComponents((x) => x.MinValue);
        public Complex MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Complex a, Complex b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Complex Clamp(Complex a, Complex b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Complex ClampOne => this.Clamp(this.Zero, this.One);
        public static Complex operator *(Complex x, Number s)
        {
            var _var14 = s;
            return x.MapComponents((i) => i.Multiply(_var14));
        }
        public Complex Multiply(Number s)
        {
            var _var15 = s;
            return this.MapComponents((i) => i.Multiply(_var15));
        }
        public static Complex operator /(Complex x, Number s)
        {
            var _var16 = s;
            return x.MapComponents((i) => i.Divide(_var16));
        }
        public Complex Divide(Number s)
        {
            var _var17 = s;
            return this.MapComponents((i) => i.Divide(_var17));
        }
        public static Complex operator %(Complex x, Number s)
        {
            var _var18 = s;
            return x.MapComponents((i) => i.Modulo(_var18));
        }
        public Complex Modulo(Number s)
        {
            var _var19 = s;
            return this.MapComponents((i) => i.Modulo(_var19));
        }
        public static Complex operator +(Complex x, Complex y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Complex Add(Complex y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Complex operator -(Complex x, Complex y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Complex Subtract(Complex y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Complex operator -(Complex x) => x.MapComponents((a) => a.Negative);
        public Complex Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Complex a, Complex b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Complex b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Complex a, Complex b) => a.Equals(b).Not;
        public Boolean NotEquals(Complex b) => this.Equals(b).Not;
        public Complex Half => this.Divide(((Number)2));
        public Complex Quarter => this.Divide(((Number)4));
        public Complex Tenth => this.Divide(((Number)10));
        public Complex Twice => this.Multiply(((Number)2));
        public Complex Lerp(Complex b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Complex Barycentric(Complex v2, Complex v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public Complex Pow2 => this.Multiply(this);
        public Complex Pow3 => this.Pow2.Multiply(this);
        public Complex Pow4 => this.Pow3.Multiply(this);
        public Complex Pow5 => this.Pow4.Multiply(this);
        public Complex Square => this.Pow2;
        public Complex Cube => this.Pow3;
        public Complex ParabolaFunction => this.Square;
        // Unimplemented concept functions
        public static Complex Multiply(Number other, Complex self) => self.Multiply(other);
        public static Complex operator *(Number other, Complex self) => Multiply(other, self);
    }
    public readonly partial struct Integer2: Value<Integer2>, Array<Integer>
    {
        public readonly Integer A;
        public readonly Integer B;
        public Integer2 WithA(Integer a) => (a, B);
        public Integer2 WithB(Integer b) => (A, b);
        public Integer2(Integer a, Integer b) => (A, B) = (a, b);
        public static Integer2 Default = new Integer2();
        public static Integer2 New(Integer a, Integer b) => new Integer2(a, b);
        public static implicit operator (Integer, Integer)(Integer2 self) => (self.A, self.B);
        public static implicit operator Integer2((Integer, Integer) value) => new Integer2(value.Item1, value.Item2);
        public void Deconstruct(out Integer a, out Integer b) { a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is Integer2)) return false; var other = (Integer2)obj; return A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Integer2 self) => new Dynamic(self);
        public static implicit operator Integer2(Dynamic value) => value.As<Integer2>();
        public String TypeName => "Integer2";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Integer2 a, Integer2 b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Integer2 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Integer2 a, Integer2 b) => a.Equals(b).Not;
        public Boolean NotEquals(Integer2 b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 2;
        public Integer this[Integer n] => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
        public Integer At(Integer n) => n == 0 ? A : n == 1 ? B : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Integer3: Value<Integer3>, Array<Integer>
    {
        public readonly Integer A;
        public readonly Integer B;
        public readonly Integer C;
        public Integer3 WithA(Integer a) => (a, B, C);
        public Integer3 WithB(Integer b) => (A, b, C);
        public Integer3 WithC(Integer c) => (A, B, c);
        public Integer3(Integer a, Integer b, Integer c) => (A, B, C) = (a, b, c);
        public static Integer3 Default = new Integer3();
        public static Integer3 New(Integer a, Integer b, Integer c) => new Integer3(a, b, c);
        public static implicit operator (Integer, Integer, Integer)(Integer3 self) => (self.A, self.B, self.C);
        public static implicit operator Integer3((Integer, Integer, Integer) value) => new Integer3(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Integer a, out Integer b, out Integer c) { a = A; b = B; c = C; }
        public override bool Equals(object obj) { if (!(obj is Integer3)) return false; var other = (Integer3)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Integer3 self) => new Dynamic(self);
        public static implicit operator Integer3(Dynamic value) => value.As<Integer3>();
        public String TypeName => "Integer3";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Integer3 a, Integer3 b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Integer3 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Integer3 a, Integer3 b) => a.Equals(b).Not;
        public Boolean NotEquals(Integer3 b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 3;
        public Integer this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Integer At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Integer4: Value<Integer4>, Array<Integer>
    {
        public readonly Integer A;
        public readonly Integer B;
        public readonly Integer C;
        public readonly Integer D;
        public Integer4 WithA(Integer a) => (a, B, C, D);
        public Integer4 WithB(Integer b) => (A, b, C, D);
        public Integer4 WithC(Integer c) => (A, B, c, D);
        public Integer4 WithD(Integer d) => (A, B, C, d);
        public Integer4(Integer a, Integer b, Integer c, Integer d) => (A, B, C, D) = (a, b, c, d);
        public static Integer4 Default = new Integer4();
        public static Integer4 New(Integer a, Integer b, Integer c, Integer d) => new Integer4(a, b, c, d);
        public static implicit operator (Integer, Integer, Integer, Integer)(Integer4 self) => (self.A, self.B, self.C, self.D);
        public static implicit operator Integer4((Integer, Integer, Integer, Integer) value) => new Integer4(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Integer a, out Integer b, out Integer c, out Integer d) { a = A; b = B; c = C; d = D; }
        public override bool Equals(object obj) { if (!(obj is Integer4)) return false; var other = (Integer4)obj; return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C, D);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Integer4 self) => new Dynamic(self);
        public static implicit operator Integer4(Dynamic value) => value.As<Integer4>();
        public String TypeName => "Integer4";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"A", (String)"B", (String)"C", (String)"D");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(A), new Dynamic(B), new Dynamic(C), new Dynamic(D));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Integer4 a, Integer4 b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Integer4 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Integer4 a, Integer4 b) => a.Equals(b).Not;
        public Boolean NotEquals(Integer4 b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 4;
        public Integer this[Integer n] => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
        public Integer At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : n == 3 ? D : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Color: Coordinate<Color>
    {
        public readonly Unit R;
        public readonly Unit G;
        public readonly Unit B;
        public readonly Unit A;
        public Color WithR(Unit r) => (r, G, B, A);
        public Color WithG(Unit g) => (R, g, B, A);
        public Color WithB(Unit b) => (R, G, b, A);
        public Color WithA(Unit a) => (R, G, B, a);
        public Color(Unit r, Unit g, Unit b, Unit a) => (R, G, B, A) = (r, g, b, a);
        public static Color Default = new Color();
        public static Color New(Unit r, Unit g, Unit b, Unit a) => new Color(r, g, b, a);
        public static implicit operator (Unit, Unit, Unit, Unit)(Color self) => (self.R, self.G, self.B, self.A);
        public static implicit operator Color((Unit, Unit, Unit, Unit) value) => new Color(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Unit r, out Unit g, out Unit b, out Unit a) { r = R; g = G; b = B; a = A; }
        public override bool Equals(object obj) { if (!(obj is Color)) return false; var other = (Color)obj; return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(R, G, B, A);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Color self) => new Dynamic(self);
        public static implicit operator Color(Dynamic value) => value.As<Color>();
        public String TypeName => "Color";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"R", (String)"G", (String)"B", (String)"A");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(R), new Dynamic(G), new Dynamic(B), new Dynamic(A));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Color a, Color b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Color b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Color a, Color b) => a.Equals(b).Not;
        public Boolean NotEquals(Color b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct ColorLUV: Coordinate<ColorLUV>
    {
        public readonly Unit Lightness;
        public readonly Unit U;
        public readonly Unit V;
        public ColorLUV WithLightness(Unit lightness) => (lightness, U, V);
        public ColorLUV WithU(Unit u) => (Lightness, u, V);
        public ColorLUV WithV(Unit v) => (Lightness, U, v);
        public ColorLUV(Unit lightness, Unit u, Unit v) => (Lightness, U, V) = (lightness, u, v);
        public static ColorLUV Default = new ColorLUV();
        public static ColorLUV New(Unit lightness, Unit u, Unit v) => new ColorLUV(lightness, u, v);
        public static implicit operator (Unit, Unit, Unit)(ColorLUV self) => (self.Lightness, self.U, self.V);
        public static implicit operator ColorLUV((Unit, Unit, Unit) value) => new ColorLUV(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Unit lightness, out Unit u, out Unit v) { lightness = Lightness; u = U; v = V; }
        public override bool Equals(object obj) { if (!(obj is ColorLUV)) return false; var other = (ColorLUV)obj; return Lightness.Equals(other.Lightness) && U.Equals(other.U) && V.Equals(other.V); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Lightness, U, V);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ColorLUV self) => new Dynamic(self);
        public static implicit operator ColorLUV(Dynamic value) => value.As<ColorLUV>();
        public String TypeName => "ColorLUV";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Lightness", (String)"U", (String)"V");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Lightness), new Dynamic(U), new Dynamic(V));
        // Implemented concept functions and type functions
        public static Boolean operator ==(ColorLUV a, ColorLUV b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(ColorLUV b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(ColorLUV a, ColorLUV b) => a.Equals(b).Not;
        public Boolean NotEquals(ColorLUV b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct ColorLAB: Coordinate<ColorLAB>
    {
        public readonly Unit Lightness;
        public readonly Number A;
        public readonly Number B;
        public ColorLAB WithLightness(Unit lightness) => (lightness, A, B);
        public ColorLAB WithA(Number a) => (Lightness, a, B);
        public ColorLAB WithB(Number b) => (Lightness, A, b);
        public ColorLAB(Unit lightness, Number a, Number b) => (Lightness, A, B) = (lightness, a, b);
        public static ColorLAB Default = new ColorLAB();
        public static ColorLAB New(Unit lightness, Number a, Number b) => new ColorLAB(lightness, a, b);
        public static implicit operator (Unit, Number, Number)(ColorLAB self) => (self.Lightness, self.A, self.B);
        public static implicit operator ColorLAB((Unit, Number, Number) value) => new ColorLAB(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Unit lightness, out Number a, out Number b) { lightness = Lightness; a = A; b = B; }
        public override bool Equals(object obj) { if (!(obj is ColorLAB)) return false; var other = (ColorLAB)obj; return Lightness.Equals(other.Lightness) && A.Equals(other.A) && B.Equals(other.B); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Lightness, A, B);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ColorLAB self) => new Dynamic(self);
        public static implicit operator ColorLAB(Dynamic value) => value.As<ColorLAB>();
        public String TypeName => "ColorLAB";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Lightness", (String)"A", (String)"B");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Lightness), new Dynamic(A), new Dynamic(B));
        // Implemented concept functions and type functions
        public static Boolean operator ==(ColorLAB a, ColorLAB b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(ColorLAB b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(ColorLAB a, ColorLAB b) => a.Equals(b).Not;
        public Boolean NotEquals(ColorLAB b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct ColorLCh: Coordinate<ColorLCh>
    {
        public readonly Unit Lightness;
        public readonly PolarCoordinate ChromaHue;
        public ColorLCh WithLightness(Unit lightness) => (lightness, ChromaHue);
        public ColorLCh WithChromaHue(PolarCoordinate chromaHue) => (Lightness, chromaHue);
        public ColorLCh(Unit lightness, PolarCoordinate chromaHue) => (Lightness, ChromaHue) = (lightness, chromaHue);
        public static ColorLCh Default = new ColorLCh();
        public static ColorLCh New(Unit lightness, PolarCoordinate chromaHue) => new ColorLCh(lightness, chromaHue);
        public static implicit operator (Unit, PolarCoordinate)(ColorLCh self) => (self.Lightness, self.ChromaHue);
        public static implicit operator ColorLCh((Unit, PolarCoordinate) value) => new ColorLCh(value.Item1, value.Item2);
        public void Deconstruct(out Unit lightness, out PolarCoordinate chromaHue) { lightness = Lightness; chromaHue = ChromaHue; }
        public override bool Equals(object obj) { if (!(obj is ColorLCh)) return false; var other = (ColorLCh)obj; return Lightness.Equals(other.Lightness) && ChromaHue.Equals(other.ChromaHue); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Lightness, ChromaHue);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ColorLCh self) => new Dynamic(self);
        public static implicit operator ColorLCh(Dynamic value) => value.As<ColorLCh>();
        public String TypeName => "ColorLCh";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Lightness", (String)"ChromaHue");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Lightness), new Dynamic(ChromaHue));
        // Implemented concept functions and type functions
        public static Boolean operator ==(ColorLCh a, ColorLCh b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(ColorLCh b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(ColorLCh a, ColorLCh b) => a.Equals(b).Not;
        public Boolean NotEquals(ColorLCh b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct ColorHSV: Coordinate<ColorHSV>
    {
        public readonly Angle Hue;
        public readonly Unit S;
        public readonly Unit V;
        public ColorHSV WithHue(Angle hue) => (hue, S, V);
        public ColorHSV WithS(Unit s) => (Hue, s, V);
        public ColorHSV WithV(Unit v) => (Hue, S, v);
        public ColorHSV(Angle hue, Unit s, Unit v) => (Hue, S, V) = (hue, s, v);
        public static ColorHSV Default = new ColorHSV();
        public static ColorHSV New(Angle hue, Unit s, Unit v) => new ColorHSV(hue, s, v);
        public static implicit operator (Angle, Unit, Unit)(ColorHSV self) => (self.Hue, self.S, self.V);
        public static implicit operator ColorHSV((Angle, Unit, Unit) value) => new ColorHSV(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Angle hue, out Unit s, out Unit v) { hue = Hue; s = S; v = V; }
        public override bool Equals(object obj) { if (!(obj is ColorHSV)) return false; var other = (ColorHSV)obj; return Hue.Equals(other.Hue) && S.Equals(other.S) && V.Equals(other.V); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Hue, S, V);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ColorHSV self) => new Dynamic(self);
        public static implicit operator ColorHSV(Dynamic value) => value.As<ColorHSV>();
        public String TypeName => "ColorHSV";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Hue", (String)"S", (String)"V");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Hue), new Dynamic(S), new Dynamic(V));
        // Implemented concept functions and type functions
        public static Boolean operator ==(ColorHSV a, ColorHSV b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(ColorHSV b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(ColorHSV a, ColorHSV b) => a.Equals(b).Not;
        public Boolean NotEquals(ColorHSV b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct ColorHSL: Coordinate<ColorHSL>
    {
        public readonly Angle Hue;
        public readonly Unit Saturation;
        public readonly Unit Luminance;
        public ColorHSL WithHue(Angle hue) => (hue, Saturation, Luminance);
        public ColorHSL WithSaturation(Unit saturation) => (Hue, saturation, Luminance);
        public ColorHSL WithLuminance(Unit luminance) => (Hue, Saturation, luminance);
        public ColorHSL(Angle hue, Unit saturation, Unit luminance) => (Hue, Saturation, Luminance) = (hue, saturation, luminance);
        public static ColorHSL Default = new ColorHSL();
        public static ColorHSL New(Angle hue, Unit saturation, Unit luminance) => new ColorHSL(hue, saturation, luminance);
        public static implicit operator (Angle, Unit, Unit)(ColorHSL self) => (self.Hue, self.Saturation, self.Luminance);
        public static implicit operator ColorHSL((Angle, Unit, Unit) value) => new ColorHSL(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Angle hue, out Unit saturation, out Unit luminance) { hue = Hue; saturation = Saturation; luminance = Luminance; }
        public override bool Equals(object obj) { if (!(obj is ColorHSL)) return false; var other = (ColorHSL)obj; return Hue.Equals(other.Hue) && Saturation.Equals(other.Saturation) && Luminance.Equals(other.Luminance); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Hue, Saturation, Luminance);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ColorHSL self) => new Dynamic(self);
        public static implicit operator ColorHSL(Dynamic value) => value.As<ColorHSL>();
        public String TypeName => "ColorHSL";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Hue", (String)"Saturation", (String)"Luminance");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Hue), new Dynamic(Saturation), new Dynamic(Luminance));
        // Implemented concept functions and type functions
        public static Boolean operator ==(ColorHSL a, ColorHSL b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(ColorHSL b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(ColorHSL a, ColorHSL b) => a.Equals(b).Not;
        public Boolean NotEquals(ColorHSL b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct ColorYCbCr: Coordinate<ColorYCbCr>
    {
        public readonly Unit Y;
        public readonly Unit Cb;
        public readonly Unit Cr;
        public ColorYCbCr WithY(Unit y) => (y, Cb, Cr);
        public ColorYCbCr WithCb(Unit cb) => (Y, cb, Cr);
        public ColorYCbCr WithCr(Unit cr) => (Y, Cb, cr);
        public ColorYCbCr(Unit y, Unit cb, Unit cr) => (Y, Cb, Cr) = (y, cb, cr);
        public static ColorYCbCr Default = new ColorYCbCr();
        public static ColorYCbCr New(Unit y, Unit cb, Unit cr) => new ColorYCbCr(y, cb, cr);
        public static implicit operator (Unit, Unit, Unit)(ColorYCbCr self) => (self.Y, self.Cb, self.Cr);
        public static implicit operator ColorYCbCr((Unit, Unit, Unit) value) => new ColorYCbCr(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Unit y, out Unit cb, out Unit cr) { y = Y; cb = Cb; cr = Cr; }
        public override bool Equals(object obj) { if (!(obj is ColorYCbCr)) return false; var other = (ColorYCbCr)obj; return Y.Equals(other.Y) && Cb.Equals(other.Cb) && Cr.Equals(other.Cr); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Y, Cb, Cr);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(ColorYCbCr self) => new Dynamic(self);
        public static implicit operator ColorYCbCr(Dynamic value) => value.As<ColorYCbCr>();
        public String TypeName => "ColorYCbCr";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Y", (String)"Cb", (String)"Cr");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Y), new Dynamic(Cb), new Dynamic(Cr));
        // Implemented concept functions and type functions
        public static Boolean operator ==(ColorYCbCr a, ColorYCbCr b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(ColorYCbCr b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(ColorYCbCr a, ColorYCbCr b) => a.Equals(b).Not;
        public Boolean NotEquals(ColorYCbCr b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct SphericalCoordinate: Coordinate<SphericalCoordinate>
    {
        public readonly Number Radius;
        public readonly Angle Azimuth;
        public readonly Angle Polar;
        public SphericalCoordinate WithRadius(Number radius) => (radius, Azimuth, Polar);
        public SphericalCoordinate WithAzimuth(Angle azimuth) => (Radius, azimuth, Polar);
        public SphericalCoordinate WithPolar(Angle polar) => (Radius, Azimuth, polar);
        public SphericalCoordinate(Number radius, Angle azimuth, Angle polar) => (Radius, Azimuth, Polar) = (radius, azimuth, polar);
        public static SphericalCoordinate Default = new SphericalCoordinate();
        public static SphericalCoordinate New(Number radius, Angle azimuth, Angle polar) => new SphericalCoordinate(radius, azimuth, polar);
        public static implicit operator (Number, Angle, Angle)(SphericalCoordinate self) => (self.Radius, self.Azimuth, self.Polar);
        public static implicit operator SphericalCoordinate((Number, Angle, Angle) value) => new SphericalCoordinate(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number radius, out Angle azimuth, out Angle polar) { radius = Radius; azimuth = Azimuth; polar = Polar; }
        public override bool Equals(object obj) { if (!(obj is SphericalCoordinate)) return false; var other = (SphericalCoordinate)obj; return Radius.Equals(other.Radius) && Azimuth.Equals(other.Azimuth) && Polar.Equals(other.Polar); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius, Azimuth, Polar);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(SphericalCoordinate self) => new Dynamic(self);
        public static implicit operator SphericalCoordinate(Dynamic value) => value.As<SphericalCoordinate>();
        public String TypeName => "SphericalCoordinate";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius", (String)"Azimuth", (String)"Polar");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius), new Dynamic(Azimuth), new Dynamic(Polar));
        // Implemented concept functions and type functions
        public static Boolean operator ==(SphericalCoordinate a, SphericalCoordinate b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(SphericalCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(SphericalCoordinate a, SphericalCoordinate b) => a.Equals(b).Not;
        public Boolean NotEquals(SphericalCoordinate b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct PolarCoordinate: Coordinate<PolarCoordinate>
    {
        public readonly Number Radius;
        public readonly Angle Angle;
        public PolarCoordinate WithRadius(Number radius) => (radius, Angle);
        public PolarCoordinate WithAngle(Angle angle) => (Radius, angle);
        public PolarCoordinate(Number radius, Angle angle) => (Radius, Angle) = (radius, angle);
        public static PolarCoordinate Default = new PolarCoordinate();
        public static PolarCoordinate New(Number radius, Angle angle) => new PolarCoordinate(radius, angle);
        public static implicit operator (Number, Angle)(PolarCoordinate self) => (self.Radius, self.Angle);
        public static implicit operator PolarCoordinate((Number, Angle) value) => new PolarCoordinate(value.Item1, value.Item2);
        public void Deconstruct(out Number radius, out Angle angle) { radius = Radius; angle = Angle; }
        public override bool Equals(object obj) { if (!(obj is PolarCoordinate)) return false; var other = (PolarCoordinate)obj; return Radius.Equals(other.Radius) && Angle.Equals(other.Angle); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius, Angle);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(PolarCoordinate self) => new Dynamic(self);
        public static implicit operator PolarCoordinate(Dynamic value) => value.As<PolarCoordinate>();
        public String TypeName => "PolarCoordinate";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius", (String)"Angle");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius), new Dynamic(Angle));
        // Implemented concept functions and type functions
        public static Boolean operator ==(PolarCoordinate a, PolarCoordinate b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(PolarCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(PolarCoordinate a, PolarCoordinate b) => a.Equals(b).Not;
        public Boolean NotEquals(PolarCoordinate b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct LogPolarCoordinate: Coordinate<LogPolarCoordinate>
    {
        public readonly Number Rho;
        public readonly Angle Azimuth;
        public LogPolarCoordinate WithRho(Number rho) => (rho, Azimuth);
        public LogPolarCoordinate WithAzimuth(Angle azimuth) => (Rho, azimuth);
        public LogPolarCoordinate(Number rho, Angle azimuth) => (Rho, Azimuth) = (rho, azimuth);
        public static LogPolarCoordinate Default = new LogPolarCoordinate();
        public static LogPolarCoordinate New(Number rho, Angle azimuth) => new LogPolarCoordinate(rho, azimuth);
        public static implicit operator (Number, Angle)(LogPolarCoordinate self) => (self.Rho, self.Azimuth);
        public static implicit operator LogPolarCoordinate((Number, Angle) value) => new LogPolarCoordinate(value.Item1, value.Item2);
        public void Deconstruct(out Number rho, out Angle azimuth) { rho = Rho; azimuth = Azimuth; }
        public override bool Equals(object obj) { if (!(obj is LogPolarCoordinate)) return false; var other = (LogPolarCoordinate)obj; return Rho.Equals(other.Rho) && Azimuth.Equals(other.Azimuth); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Rho, Azimuth);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(LogPolarCoordinate self) => new Dynamic(self);
        public static implicit operator LogPolarCoordinate(Dynamic value) => value.As<LogPolarCoordinate>();
        public String TypeName => "LogPolarCoordinate";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Rho", (String)"Azimuth");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Rho), new Dynamic(Azimuth));
        // Implemented concept functions and type functions
        public static Boolean operator ==(LogPolarCoordinate a, LogPolarCoordinate b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(LogPolarCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(LogPolarCoordinate a, LogPolarCoordinate b) => a.Equals(b).Not;
        public Boolean NotEquals(LogPolarCoordinate b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct CylindricalCoordinate: Coordinate<CylindricalCoordinate>
    {
        public readonly Number RadialDistance;
        public readonly Angle Azimuth;
        public readonly Number Height;
        public CylindricalCoordinate WithRadialDistance(Number radialDistance) => (radialDistance, Azimuth, Height);
        public CylindricalCoordinate WithAzimuth(Angle azimuth) => (RadialDistance, azimuth, Height);
        public CylindricalCoordinate WithHeight(Number height) => (RadialDistance, Azimuth, height);
        public CylindricalCoordinate(Number radialDistance, Angle azimuth, Number height) => (RadialDistance, Azimuth, Height) = (radialDistance, azimuth, height);
        public static CylindricalCoordinate Default = new CylindricalCoordinate();
        public static CylindricalCoordinate New(Number radialDistance, Angle azimuth, Number height) => new CylindricalCoordinate(radialDistance, azimuth, height);
        public static implicit operator (Number, Angle, Number)(CylindricalCoordinate self) => (self.RadialDistance, self.Azimuth, self.Height);
        public static implicit operator CylindricalCoordinate((Number, Angle, Number) value) => new CylindricalCoordinate(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number radialDistance, out Angle azimuth, out Number height) { radialDistance = RadialDistance; azimuth = Azimuth; height = Height; }
        public override bool Equals(object obj) { if (!(obj is CylindricalCoordinate)) return false; var other = (CylindricalCoordinate)obj; return RadialDistance.Equals(other.RadialDistance) && Azimuth.Equals(other.Azimuth) && Height.Equals(other.Height); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(RadialDistance, Azimuth, Height);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(CylindricalCoordinate self) => new Dynamic(self);
        public static implicit operator CylindricalCoordinate(Dynamic value) => value.As<CylindricalCoordinate>();
        public String TypeName => "CylindricalCoordinate";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"RadialDistance", (String)"Azimuth", (String)"Height");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(RadialDistance), new Dynamic(Azimuth), new Dynamic(Height));
        // Implemented concept functions and type functions
        public static Boolean operator ==(CylindricalCoordinate a, CylindricalCoordinate b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(CylindricalCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(CylindricalCoordinate a, CylindricalCoordinate b) => a.Equals(b).Not;
        public Boolean NotEquals(CylindricalCoordinate b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct HorizontalCoordinate: Coordinate<HorizontalCoordinate>
    {
        public readonly Number Radius;
        public readonly Angle Azimuth;
        public readonly Number Height;
        public HorizontalCoordinate WithRadius(Number radius) => (radius, Azimuth, Height);
        public HorizontalCoordinate WithAzimuth(Angle azimuth) => (Radius, azimuth, Height);
        public HorizontalCoordinate WithHeight(Number height) => (Radius, Azimuth, height);
        public HorizontalCoordinate(Number radius, Angle azimuth, Number height) => (Radius, Azimuth, Height) = (radius, azimuth, height);
        public static HorizontalCoordinate Default = new HorizontalCoordinate();
        public static HorizontalCoordinate New(Number radius, Angle azimuth, Number height) => new HorizontalCoordinate(radius, azimuth, height);
        public static implicit operator (Number, Angle, Number)(HorizontalCoordinate self) => (self.Radius, self.Azimuth, self.Height);
        public static implicit operator HorizontalCoordinate((Number, Angle, Number) value) => new HorizontalCoordinate(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number radius, out Angle azimuth, out Number height) { radius = Radius; azimuth = Azimuth; height = Height; }
        public override bool Equals(object obj) { if (!(obj is HorizontalCoordinate)) return false; var other = (HorizontalCoordinate)obj; return Radius.Equals(other.Radius) && Azimuth.Equals(other.Azimuth) && Height.Equals(other.Height); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radius, Azimuth, Height);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(HorizontalCoordinate self) => new Dynamic(self);
        public static implicit operator HorizontalCoordinate(Dynamic value) => value.As<HorizontalCoordinate>();
        public String TypeName => "HorizontalCoordinate";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radius", (String)"Azimuth", (String)"Height");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radius), new Dynamic(Azimuth), new Dynamic(Height));
        // Implemented concept functions and type functions
        public static Boolean operator ==(HorizontalCoordinate a, HorizontalCoordinate b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(HorizontalCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(HorizontalCoordinate a, HorizontalCoordinate b) => a.Equals(b).Not;
        public Boolean NotEquals(HorizontalCoordinate b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct GeoCoordinate: Coordinate<GeoCoordinate>
    {
        public readonly Angle Latitude;
        public readonly Angle Longitude;
        public GeoCoordinate WithLatitude(Angle latitude) => (latitude, Longitude);
        public GeoCoordinate WithLongitude(Angle longitude) => (Latitude, longitude);
        public GeoCoordinate(Angle latitude, Angle longitude) => (Latitude, Longitude) = (latitude, longitude);
        public static GeoCoordinate Default = new GeoCoordinate();
        public static GeoCoordinate New(Angle latitude, Angle longitude) => new GeoCoordinate(latitude, longitude);
        public static implicit operator (Angle, Angle)(GeoCoordinate self) => (self.Latitude, self.Longitude);
        public static implicit operator GeoCoordinate((Angle, Angle) value) => new GeoCoordinate(value.Item1, value.Item2);
        public void Deconstruct(out Angle latitude, out Angle longitude) { latitude = Latitude; longitude = Longitude; }
        public override bool Equals(object obj) { if (!(obj is GeoCoordinate)) return false; var other = (GeoCoordinate)obj; return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Latitude, Longitude);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(GeoCoordinate self) => new Dynamic(self);
        public static implicit operator GeoCoordinate(Dynamic value) => value.As<GeoCoordinate>();
        public String TypeName => "GeoCoordinate";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Latitude", (String)"Longitude");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Latitude), new Dynamic(Longitude));
        // Implemented concept functions and type functions
        public static Boolean operator ==(GeoCoordinate a, GeoCoordinate b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(GeoCoordinate b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(GeoCoordinate a, GeoCoordinate b) => a.Equals(b).Not;
        public Boolean NotEquals(GeoCoordinate b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct GeoCoordinateWithAltitude: Coordinate<GeoCoordinateWithAltitude>
    {
        public readonly GeoCoordinate Coordinate;
        public readonly Number Altitude;
        public GeoCoordinateWithAltitude WithCoordinate(GeoCoordinate coordinate) => (coordinate, Altitude);
        public GeoCoordinateWithAltitude WithAltitude(Number altitude) => (Coordinate, altitude);
        public GeoCoordinateWithAltitude(GeoCoordinate coordinate, Number altitude) => (Coordinate, Altitude) = (coordinate, altitude);
        public static GeoCoordinateWithAltitude Default = new GeoCoordinateWithAltitude();
        public static GeoCoordinateWithAltitude New(GeoCoordinate coordinate, Number altitude) => new GeoCoordinateWithAltitude(coordinate, altitude);
        public static implicit operator (GeoCoordinate, Number)(GeoCoordinateWithAltitude self) => (self.Coordinate, self.Altitude);
        public static implicit operator GeoCoordinateWithAltitude((GeoCoordinate, Number) value) => new GeoCoordinateWithAltitude(value.Item1, value.Item2);
        public void Deconstruct(out GeoCoordinate coordinate, out Number altitude) { coordinate = Coordinate; altitude = Altitude; }
        public override bool Equals(object obj) { if (!(obj is GeoCoordinateWithAltitude)) return false; var other = (GeoCoordinateWithAltitude)obj; return Coordinate.Equals(other.Coordinate) && Altitude.Equals(other.Altitude); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Coordinate, Altitude);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(GeoCoordinateWithAltitude self) => new Dynamic(self);
        public static implicit operator GeoCoordinateWithAltitude(Dynamic value) => value.As<GeoCoordinateWithAltitude>();
        public String TypeName => "GeoCoordinateWithAltitude";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Coordinate", (String)"Altitude");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Coordinate), new Dynamic(Altitude));
        // Implemented concept functions and type functions
        public static Boolean operator ==(GeoCoordinateWithAltitude a, GeoCoordinateWithAltitude b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(GeoCoordinateWithAltitude b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(GeoCoordinateWithAltitude a, GeoCoordinateWithAltitude b) => a.Equals(b).Not;
        public Boolean NotEquals(GeoCoordinateWithAltitude b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Size2D: Value<Size2D>, Array<Number>
    {
        public readonly Number Width;
        public readonly Number Height;
        public Size2D WithWidth(Number width) => (width, Height);
        public Size2D WithHeight(Number height) => (Width, height);
        public Size2D(Number width, Number height) => (Width, Height) = (width, height);
        public static Size2D Default = new Size2D();
        public static Size2D New(Number width, Number height) => new Size2D(width, height);
        public static implicit operator (Number, Number)(Size2D self) => (self.Width, self.Height);
        public static implicit operator Size2D((Number, Number) value) => new Size2D(value.Item1, value.Item2);
        public void Deconstruct(out Number width, out Number height) { width = Width; height = Height; }
        public override bool Equals(object obj) { if (!(obj is Size2D)) return false; var other = (Size2D)obj; return Width.Equals(other.Width) && Height.Equals(other.Height); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Width, Height);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Size2D self) => new Dynamic(self);
        public static implicit operator Size2D(Dynamic value) => value.As<Size2D>();
        public String TypeName => "Size2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Width", (String)"Height");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Width), new Dynamic(Height));
        // Implemented concept functions and type functions
        public static implicit operator Vector2D(Size2D a) => a.Width.Tuple2(a.Height);
        public Vector2D Vector2D => this.Width.Tuple2(this.Height);
        public static Boolean operator ==(Size2D a, Size2D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Size2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Size2D a, Size2D b) => a.Equals(b).Not;
        public Boolean NotEquals(Size2D b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 2;
        public Number this[Integer n] => n == 0 ? Width : n == 1 ? Height : throw new System.IndexOutOfRangeException();
        public Number At(Integer n) => n == 0 ? Width : n == 1 ? Height : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Size3D: Value<Size3D>, Array<Number>
    {
        public readonly Number Width;
        public readonly Number Height;
        public readonly Number Depth;
        public Size3D WithWidth(Number width) => (width, Height, Depth);
        public Size3D WithHeight(Number height) => (Width, height, Depth);
        public Size3D WithDepth(Number depth) => (Width, Height, depth);
        public Size3D(Number width, Number height, Number depth) => (Width, Height, Depth) = (width, height, depth);
        public static Size3D Default = new Size3D();
        public static Size3D New(Number width, Number height, Number depth) => new Size3D(width, height, depth);
        public static implicit operator (Number, Number, Number)(Size3D self) => (self.Width, self.Height, self.Depth);
        public static implicit operator Size3D((Number, Number, Number) value) => new Size3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number width, out Number height, out Number depth) { width = Width; height = Height; depth = Depth; }
        public override bool Equals(object obj) { if (!(obj is Size3D)) return false; var other = (Size3D)obj; return Width.Equals(other.Width) && Height.Equals(other.Height) && Depth.Equals(other.Depth); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Width, Height, Depth);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Size3D self) => new Dynamic(self);
        public static implicit operator Size3D(Dynamic value) => value.As<Size3D>();
        public String TypeName => "Size3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Width", (String)"Height", (String)"Depth");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Width), new Dynamic(Height), new Dynamic(Depth));
        // Implemented concept functions and type functions
        public static implicit operator Vector3D(Size3D a) => a.Width.Tuple3(a.Height, a.Depth);
        public Vector3D Vector3D => this.Width.Tuple3(this.Height, this.Depth);
        public static Boolean operator ==(Size3D a, Size3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Size3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Size3D a, Size3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Size3D b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 3;
        public Number this[Integer n] => n == 0 ? Width : n == 1 ? Height : n == 2 ? Depth : throw new System.IndexOutOfRangeException();
        public Number At(Integer n) => n == 0 ? Width : n == 1 ? Height : n == 2 ? Depth : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Rational: Value<Rational>
    {
        public readonly Integer Numerator;
        public readonly Integer Denominator;
        public Rational WithNumerator(Integer numerator) => (numerator, Denominator);
        public Rational WithDenominator(Integer denominator) => (Numerator, denominator);
        public Rational(Integer numerator, Integer denominator) => (Numerator, Denominator) = (numerator, denominator);
        public static Rational Default = new Rational();
        public static Rational New(Integer numerator, Integer denominator) => new Rational(numerator, denominator);
        public static implicit operator (Integer, Integer)(Rational self) => (self.Numerator, self.Denominator);
        public static implicit operator Rational((Integer, Integer) value) => new Rational(value.Item1, value.Item2);
        public void Deconstruct(out Integer numerator, out Integer denominator) { numerator = Numerator; denominator = Denominator; }
        public override bool Equals(object obj) { if (!(obj is Rational)) return false; var other = (Rational)obj; return Numerator.Equals(other.Numerator) && Denominator.Equals(other.Denominator); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Numerator, Denominator);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Rational self) => new Dynamic(self);
        public static implicit operator Rational(Dynamic value) => value.As<Rational>();
        public String TypeName => "Rational";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Numerator", (String)"Denominator");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Numerator), new Dynamic(Denominator));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Rational a, Rational b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Rational b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Rational a, Rational b) => a.Equals(b).Not;
        public Boolean NotEquals(Rational b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Fraction: Value<Fraction>
    {
        public readonly Number Numerator;
        public readonly Number Denominator;
        public Fraction WithNumerator(Number numerator) => (numerator, Denominator);
        public Fraction WithDenominator(Number denominator) => (Numerator, denominator);
        public Fraction(Number numerator, Number denominator) => (Numerator, Denominator) = (numerator, denominator);
        public static Fraction Default = new Fraction();
        public static Fraction New(Number numerator, Number denominator) => new Fraction(numerator, denominator);
        public static implicit operator (Number, Number)(Fraction self) => (self.Numerator, self.Denominator);
        public static implicit operator Fraction((Number, Number) value) => new Fraction(value.Item1, value.Item2);
        public void Deconstruct(out Number numerator, out Number denominator) { numerator = Numerator; denominator = Denominator; }
        public override bool Equals(object obj) { if (!(obj is Fraction)) return false; var other = (Fraction)obj; return Numerator.Equals(other.Numerator) && Denominator.Equals(other.Denominator); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Numerator, Denominator);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Fraction self) => new Dynamic(self);
        public static implicit operator Fraction(Dynamic value) => value.As<Fraction>();
        public String TypeName => "Fraction";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Numerator", (String)"Denominator");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Numerator), new Dynamic(Denominator));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Fraction a, Fraction b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Fraction b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Fraction a, Fraction b) => a.Equals(b).Not;
        public Boolean NotEquals(Fraction b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct Angle: Measure<Angle>
    {
        public readonly Number Radians;
        public Angle WithRadians(Number radians) => (radians);
        public Angle(Number radians) => (Radians) = (radians);
        public static Angle Default = new Angle();
        public static Angle New(Number radians) => new Angle(radians);
        public static implicit operator Number(Angle self) => self.Radians;
        public static implicit operator Angle(Number value) => new Angle(value);
        public static implicit operator Angle(Integer value) => new Angle(value);
        public static implicit operator Angle(int value) => new Integer(value);
        public static implicit operator Angle(double value) => new Number(value);
        public static implicit operator double(Angle value) => value.Radians;
        public override bool Equals(object obj) { if (!(obj is Angle)) return false; var other = (Angle)obj; return Radians.Equals(other.Radians); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Radians);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Angle self) => new Dynamic(self);
        public static implicit operator Angle(Dynamic value) => value.As<Angle>();
        public String TypeName => "Angle";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Radians");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Radians));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(Radians);
        public Angle FromComponents(Array<Number> numbers) => new Angle(numbers[0]);
        // Implemented concept functions and type functions
        public Vector2D CircleFunction => this.Cos.Tuple2(this.Sin);
        public Vector2D LissajousFunction(Number kx, Number ky) => this.Multiply(kx).Cos.Tuple2(this.Multiply(ky).Sin);
        public Vector2D ButterflyCurveFunction => this.Multiply(this.Cos.Exp.Subtract(((Number)2).Multiply(this.Multiply(((Number)4)).Cos).Subtract(this.Divide(((Number)12)).Sin.Pow(((Number)5))))).Sin.Tuple2(this.Multiply(this.Cos.Exp.Subtract(((Number)2).Multiply(this.Multiply(((Number)4)).Cos).Subtract(this.Divide(((Number)12)).Sin.Pow(((Number)5))))).Cos);
        public Vector3D TorusKnotFunction(Number p, Number q)
        {
            var r = this.Multiply(q).Cos.Add(((Number)2));
            var x = r.Multiply(this.Multiply(p).Cos);
            var y = r.Multiply(this.Multiply(p).Sin);
            var z = this.Multiply(q).Sin.Negative;
            return x.Tuple3(y, z);
        }
        public Vector3D TrefoilKnotFunction => this.Sin.Add(this.Multiply(((Number)2)).Sin.Multiply(((Number)2))).Tuple3(this.Cos.Add(this.Multiply(((Number)2)).Cos.Multiply(((Number)2))), this.Multiply(((Number)3)).Sin.Negative);
        public Vector3D FigureEightKnotFunction => ((Number)2).Add(this.Multiply(((Number)2)).Cos).Multiply(this.Multiply(((Number)3)).Cos).Tuple3(((Number)2).Add(this.Multiply(((Number)2)).Cos).Multiply(this.Multiply(((Number)3)).Sin), this.Multiply(((Number)4)).Sin);
        public Number Cos => Intrinsics.Cos(this);
        public Number Sin => Intrinsics.Sin(this);
        public Number Tan => Intrinsics.Tan(this);
        public Number Turns => this.Radians.Divide(Constants.TwoPi);
        public Number Degrees => this.Turns.Multiply(((Number)360));
        public Number Gradians => this.Turns.Multiply(((Number)400));
        public static Angle operator *(Angle x, Angle y) => x.FromNumber(x.ToNumber.Multiply(y.ToNumber));
        public Angle Multiply(Angle y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Angle operator /(Angle x, Angle y) => x.FromNumber(x.ToNumber.Divide(y.ToNumber));
        public Angle Divide(Angle y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Angle operator %(Angle x, Angle y) => x.FromNumber(x.ToNumber.Modulo(y.ToNumber));
        public Angle Modulo(Angle y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public Number ToNumber => this.Component(((Integer)0));
        public Angle FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Angle b) => this.ToNumber.Compare(b.ToNumber);
        public static Angle operator +(Angle x, Number y) => x.FromNumber(x.ToNumber.Add(y));
        public Angle Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
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
        public Angle MinValue => this.MapComponents((x) => x.MinValue);
        public Angle MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Angle a, Angle b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Angle Clamp(Angle a, Angle b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Angle ClampOne => this.Clamp(this.Zero, this.One);
        public static Angle operator *(Angle x, Number s)
        {
            var _var20 = s;
            return x.MapComponents((i) => i.Multiply(_var20));
        }
        public Angle Multiply(Number s)
        {
            var _var21 = s;
            return this.MapComponents((i) => i.Multiply(_var21));
        }
        public static Angle operator /(Angle x, Number s)
        {
            var _var22 = s;
            return x.MapComponents((i) => i.Divide(_var22));
        }
        public Angle Divide(Number s)
        {
            var _var23 = s;
            return this.MapComponents((i) => i.Divide(_var23));
        }
        public static Angle operator %(Angle x, Number s)
        {
            var _var24 = s;
            return x.MapComponents((i) => i.Modulo(_var24));
        }
        public Angle Modulo(Number s)
        {
            var _var25 = s;
            return this.MapComponents((i) => i.Modulo(_var25));
        }
        public static Angle operator +(Angle x, Angle y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Angle Add(Angle y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Angle operator -(Angle x, Angle y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Angle Subtract(Angle y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Angle operator -(Angle x) => x.MapComponents((a) => a.Negative);
        public Angle Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Angle a, Angle b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Angle b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Angle a, Angle b) => a.Equals(b).Not;
        public Boolean NotEquals(Angle b) => this.Equals(b).Not;
        public Angle Half => this.Divide(((Number)2));
        public Angle Quarter => this.Divide(((Number)4));
        public Angle Tenth => this.Divide(((Number)10));
        public Angle Twice => this.Multiply(((Number)2));
        public Angle Lerp(Angle b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Angle Barycentric(Angle v2, Angle v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public static Boolean operator <(Angle a, Angle b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Angle b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Angle a, Angle b) => b.LessThan(a);
        public Boolean GreaterThan(Angle b) => b.LessThan(this);
        public static Boolean operator >=(Angle a, Angle b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Angle b) => b.LessThanOrEquals(this);
        public Angle Lesser(Angle b) => this.LessThanOrEquals(b) ? this : b;
        public Angle Greater(Angle b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Angle b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
        public static Boolean operator <=(Angle x, Angle y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Angle y) => throw new System.NotImplementedException();
        public static Angle Multiply(Number other, Angle self) => self.Multiply(other);
        public static Angle operator *(Number other, Angle self) => Multiply(other, self);
    }
    public readonly partial struct Length: Measure<Length>
    {
        public readonly Number Meters;
        public Length WithMeters(Number meters) => (meters);
        public Length(Number meters) => (Meters) = (meters);
        public static Length Default = new Length();
        public static Length New(Number meters) => new Length(meters);
        public static implicit operator Number(Length self) => self.Meters;
        public static implicit operator Length(Number value) => new Length(value);
        public static implicit operator Length(Integer value) => new Length(value);
        public static implicit operator Length(int value) => new Integer(value);
        public static implicit operator Length(double value) => new Number(value);
        public static implicit operator double(Length value) => value.Meters;
        public override bool Equals(object obj) { if (!(obj is Length)) return false; var other = (Length)obj; return Meters.Equals(other.Meters); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Meters);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Length self) => new Dynamic(self);
        public static implicit operator Length(Dynamic value) => value.As<Length>();
        public String TypeName => "Length";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Meters");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Meters));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(Meters);
        public Length FromComponents(Array<Number> numbers) => new Length(numbers[0]);
        // Implemented concept functions and type functions
        public static Length operator *(Length x, Length y) => x.FromNumber(x.ToNumber.Multiply(y.ToNumber));
        public Length Multiply(Length y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Length operator /(Length x, Length y) => x.FromNumber(x.ToNumber.Divide(y.ToNumber));
        public Length Divide(Length y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Length operator %(Length x, Length y) => x.FromNumber(x.ToNumber.Modulo(y.ToNumber));
        public Length Modulo(Length y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public Number ToNumber => this.Component(((Integer)0));
        public Length FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Length b) => this.ToNumber.Compare(b.ToNumber);
        public static Length operator +(Length x, Number y) => x.FromNumber(x.ToNumber.Add(y));
        public Length Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
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
        public Length MinValue => this.MapComponents((x) => x.MinValue);
        public Length MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Length a, Length b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Length Clamp(Length a, Length b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Length ClampOne => this.Clamp(this.Zero, this.One);
        public static Length operator *(Length x, Number s)
        {
            var _var26 = s;
            return x.MapComponents((i) => i.Multiply(_var26));
        }
        public Length Multiply(Number s)
        {
            var _var27 = s;
            return this.MapComponents((i) => i.Multiply(_var27));
        }
        public static Length operator /(Length x, Number s)
        {
            var _var28 = s;
            return x.MapComponents((i) => i.Divide(_var28));
        }
        public Length Divide(Number s)
        {
            var _var29 = s;
            return this.MapComponents((i) => i.Divide(_var29));
        }
        public static Length operator %(Length x, Number s)
        {
            var _var30 = s;
            return x.MapComponents((i) => i.Modulo(_var30));
        }
        public Length Modulo(Number s)
        {
            var _var31 = s;
            return this.MapComponents((i) => i.Modulo(_var31));
        }
        public static Length operator +(Length x, Length y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Length Add(Length y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Length operator -(Length x, Length y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Length Subtract(Length y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Length operator -(Length x) => x.MapComponents((a) => a.Negative);
        public Length Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Length a, Length b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Length b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Length a, Length b) => a.Equals(b).Not;
        public Boolean NotEquals(Length b) => this.Equals(b).Not;
        public Length Half => this.Divide(((Number)2));
        public Length Quarter => this.Divide(((Number)4));
        public Length Tenth => this.Divide(((Number)10));
        public Length Twice => this.Multiply(((Number)2));
        public Length Lerp(Length b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Length Barycentric(Length v2, Length v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public static Boolean operator <(Length a, Length b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Length b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Length a, Length b) => b.LessThan(a);
        public Boolean GreaterThan(Length b) => b.LessThan(this);
        public static Boolean operator >=(Length a, Length b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Length b) => b.LessThanOrEquals(this);
        public Length Lesser(Length b) => this.LessThanOrEquals(b) ? this : b;
        public Length Greater(Length b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Length b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
        public static Boolean operator <=(Length x, Length y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Length y) => throw new System.NotImplementedException();
        public static Length Multiply(Number other, Length self) => self.Multiply(other);
        public static Length operator *(Number other, Length self) => Multiply(other, self);
    }
    public readonly partial struct Mass: Measure<Mass>
    {
        public readonly Number Kilograms;
        public Mass WithKilograms(Number kilograms) => (kilograms);
        public Mass(Number kilograms) => (Kilograms) = (kilograms);
        public static Mass Default = new Mass();
        public static Mass New(Number kilograms) => new Mass(kilograms);
        public static implicit operator Number(Mass self) => self.Kilograms;
        public static implicit operator Mass(Number value) => new Mass(value);
        public static implicit operator Mass(Integer value) => new Mass(value);
        public static implicit operator Mass(int value) => new Integer(value);
        public static implicit operator Mass(double value) => new Number(value);
        public static implicit operator double(Mass value) => value.Kilograms;
        public override bool Equals(object obj) { if (!(obj is Mass)) return false; var other = (Mass)obj; return Kilograms.Equals(other.Kilograms); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Kilograms);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Mass self) => new Dynamic(self);
        public static implicit operator Mass(Dynamic value) => value.As<Mass>();
        public String TypeName => "Mass";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Kilograms");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Kilograms));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(Kilograms);
        public Mass FromComponents(Array<Number> numbers) => new Mass(numbers[0]);
        // Implemented concept functions and type functions
        public static Mass operator *(Mass x, Mass y) => x.FromNumber(x.ToNumber.Multiply(y.ToNumber));
        public Mass Multiply(Mass y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Mass operator /(Mass x, Mass y) => x.FromNumber(x.ToNumber.Divide(y.ToNumber));
        public Mass Divide(Mass y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Mass operator %(Mass x, Mass y) => x.FromNumber(x.ToNumber.Modulo(y.ToNumber));
        public Mass Modulo(Mass y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public Number ToNumber => this.Component(((Integer)0));
        public Mass FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Mass b) => this.ToNumber.Compare(b.ToNumber);
        public static Mass operator +(Mass x, Number y) => x.FromNumber(x.ToNumber.Add(y));
        public Mass Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
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
        public Mass MinValue => this.MapComponents((x) => x.MinValue);
        public Mass MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Mass a, Mass b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Mass Clamp(Mass a, Mass b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Mass ClampOne => this.Clamp(this.Zero, this.One);
        public static Mass operator *(Mass x, Number s)
        {
            var _var32 = s;
            return x.MapComponents((i) => i.Multiply(_var32));
        }
        public Mass Multiply(Number s)
        {
            var _var33 = s;
            return this.MapComponents((i) => i.Multiply(_var33));
        }
        public static Mass operator /(Mass x, Number s)
        {
            var _var34 = s;
            return x.MapComponents((i) => i.Divide(_var34));
        }
        public Mass Divide(Number s)
        {
            var _var35 = s;
            return this.MapComponents((i) => i.Divide(_var35));
        }
        public static Mass operator %(Mass x, Number s)
        {
            var _var36 = s;
            return x.MapComponents((i) => i.Modulo(_var36));
        }
        public Mass Modulo(Number s)
        {
            var _var37 = s;
            return this.MapComponents((i) => i.Modulo(_var37));
        }
        public static Mass operator +(Mass x, Mass y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Mass Add(Mass y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Mass operator -(Mass x, Mass y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Mass Subtract(Mass y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Mass operator -(Mass x) => x.MapComponents((a) => a.Negative);
        public Mass Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Mass a, Mass b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Mass b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Mass a, Mass b) => a.Equals(b).Not;
        public Boolean NotEquals(Mass b) => this.Equals(b).Not;
        public Mass Half => this.Divide(((Number)2));
        public Mass Quarter => this.Divide(((Number)4));
        public Mass Tenth => this.Divide(((Number)10));
        public Mass Twice => this.Multiply(((Number)2));
        public Mass Lerp(Mass b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Mass Barycentric(Mass v2, Mass v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public static Boolean operator <(Mass a, Mass b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Mass b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Mass a, Mass b) => b.LessThan(a);
        public Boolean GreaterThan(Mass b) => b.LessThan(this);
        public static Boolean operator >=(Mass a, Mass b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Mass b) => b.LessThanOrEquals(this);
        public Mass Lesser(Mass b) => this.LessThanOrEquals(b) ? this : b;
        public Mass Greater(Mass b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Mass b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
        public static Boolean operator <=(Mass x, Mass y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Mass y) => throw new System.NotImplementedException();
        public static Mass Multiply(Number other, Mass self) => self.Multiply(other);
        public static Mass operator *(Number other, Mass self) => Multiply(other, self);
    }
    public readonly partial struct Temperature: Measure<Temperature>
    {
        public readonly Number Celsius;
        public Temperature WithCelsius(Number celsius) => (celsius);
        public Temperature(Number celsius) => (Celsius) = (celsius);
        public static Temperature Default = new Temperature();
        public static Temperature New(Number celsius) => new Temperature(celsius);
        public static implicit operator Number(Temperature self) => self.Celsius;
        public static implicit operator Temperature(Number value) => new Temperature(value);
        public static implicit operator Temperature(Integer value) => new Temperature(value);
        public static implicit operator Temperature(int value) => new Integer(value);
        public static implicit operator Temperature(double value) => new Number(value);
        public static implicit operator double(Temperature value) => value.Celsius;
        public override bool Equals(object obj) { if (!(obj is Temperature)) return false; var other = (Temperature)obj; return Celsius.Equals(other.Celsius); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Celsius);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Temperature self) => new Dynamic(self);
        public static implicit operator Temperature(Dynamic value) => value.As<Temperature>();
        public String TypeName => "Temperature";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Celsius");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Celsius));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(Celsius);
        public Temperature FromComponents(Array<Number> numbers) => new Temperature(numbers[0]);
        // Implemented concept functions and type functions
        public static Temperature operator *(Temperature x, Temperature y) => x.FromNumber(x.ToNumber.Multiply(y.ToNumber));
        public Temperature Multiply(Temperature y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Temperature operator /(Temperature x, Temperature y) => x.FromNumber(x.ToNumber.Divide(y.ToNumber));
        public Temperature Divide(Temperature y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Temperature operator %(Temperature x, Temperature y) => x.FromNumber(x.ToNumber.Modulo(y.ToNumber));
        public Temperature Modulo(Temperature y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public Number ToNumber => this.Component(((Integer)0));
        public Temperature FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Temperature b) => this.ToNumber.Compare(b.ToNumber);
        public static Temperature operator +(Temperature x, Number y) => x.FromNumber(x.ToNumber.Add(y));
        public Temperature Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
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
        public Temperature MinValue => this.MapComponents((x) => x.MinValue);
        public Temperature MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Temperature a, Temperature b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Temperature Clamp(Temperature a, Temperature b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Temperature ClampOne => this.Clamp(this.Zero, this.One);
        public static Temperature operator *(Temperature x, Number s)
        {
            var _var38 = s;
            return x.MapComponents((i) => i.Multiply(_var38));
        }
        public Temperature Multiply(Number s)
        {
            var _var39 = s;
            return this.MapComponents((i) => i.Multiply(_var39));
        }
        public static Temperature operator /(Temperature x, Number s)
        {
            var _var40 = s;
            return x.MapComponents((i) => i.Divide(_var40));
        }
        public Temperature Divide(Number s)
        {
            var _var41 = s;
            return this.MapComponents((i) => i.Divide(_var41));
        }
        public static Temperature operator %(Temperature x, Number s)
        {
            var _var42 = s;
            return x.MapComponents((i) => i.Modulo(_var42));
        }
        public Temperature Modulo(Number s)
        {
            var _var43 = s;
            return this.MapComponents((i) => i.Modulo(_var43));
        }
        public static Temperature operator +(Temperature x, Temperature y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Temperature Add(Temperature y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Temperature operator -(Temperature x, Temperature y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Temperature Subtract(Temperature y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Temperature operator -(Temperature x) => x.MapComponents((a) => a.Negative);
        public Temperature Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Temperature a, Temperature b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Temperature b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Temperature a, Temperature b) => a.Equals(b).Not;
        public Boolean NotEquals(Temperature b) => this.Equals(b).Not;
        public Temperature Half => this.Divide(((Number)2));
        public Temperature Quarter => this.Divide(((Number)4));
        public Temperature Tenth => this.Divide(((Number)10));
        public Temperature Twice => this.Multiply(((Number)2));
        public Temperature Lerp(Temperature b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Temperature Barycentric(Temperature v2, Temperature v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public static Boolean operator <(Temperature a, Temperature b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Temperature b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Temperature a, Temperature b) => b.LessThan(a);
        public Boolean GreaterThan(Temperature b) => b.LessThan(this);
        public static Boolean operator >=(Temperature a, Temperature b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Temperature b) => b.LessThanOrEquals(this);
        public Temperature Lesser(Temperature b) => this.LessThanOrEquals(b) ? this : b;
        public Temperature Greater(Temperature b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Temperature b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
        public static Boolean operator <=(Temperature x, Temperature y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Temperature y) => throw new System.NotImplementedException();
        public static Temperature Multiply(Number other, Temperature self) => self.Multiply(other);
        public static Temperature operator *(Number other, Temperature self) => Multiply(other, self);
    }
    public readonly partial struct Time: Measure<Time>
    {
        public readonly Number Seconds;
        public Time WithSeconds(Number seconds) => (seconds);
        public Time(Number seconds) => (Seconds) = (seconds);
        public static Time Default = new Time();
        public static Time New(Number seconds) => new Time(seconds);
        public static implicit operator Number(Time self) => self.Seconds;
        public static implicit operator Time(Number value) => new Time(value);
        public static implicit operator Time(Integer value) => new Time(value);
        public static implicit operator Time(int value) => new Integer(value);
        public static implicit operator Time(double value) => new Number(value);
        public static implicit operator double(Time value) => value.Seconds;
        public override bool Equals(object obj) { if (!(obj is Time)) return false; var other = (Time)obj; return Seconds.Equals(other.Seconds); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Seconds);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Time self) => new Dynamic(self);
        public static implicit operator Time(Dynamic value) => value.As<Time>();
        public String TypeName => "Time";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Seconds");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Seconds));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(Seconds);
        public Time FromComponents(Array<Number> numbers) => new Time(numbers[0]);
        // Implemented concept functions and type functions
        public static Time operator *(Time x, Time y) => x.FromNumber(x.ToNumber.Multiply(y.ToNumber));
        public Time Multiply(Time y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        public static Time operator /(Time x, Time y) => x.FromNumber(x.ToNumber.Divide(y.ToNumber));
        public Time Divide(Time y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        public static Time operator %(Time x, Time y) => x.FromNumber(x.ToNumber.Modulo(y.ToNumber));
        public Time Modulo(Time y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        public Number ToNumber => this.Component(((Integer)0));
        public Time FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        public Integer Compare(Time b) => this.ToNumber.Compare(b.ToNumber);
        public static Time operator +(Time x, Number y) => x.FromNumber(x.ToNumber.Add(y));
        public Time Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
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
        public Time MinValue => this.MapComponents((x) => x.MinValue);
        public Time MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Time a, Time b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Time Clamp(Time a, Time b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Time ClampOne => this.Clamp(this.Zero, this.One);
        public static Time operator *(Time x, Number s)
        {
            var _var44 = s;
            return x.MapComponents((i) => i.Multiply(_var44));
        }
        public Time Multiply(Number s)
        {
            var _var45 = s;
            return this.MapComponents((i) => i.Multiply(_var45));
        }
        public static Time operator /(Time x, Number s)
        {
            var _var46 = s;
            return x.MapComponents((i) => i.Divide(_var46));
        }
        public Time Divide(Number s)
        {
            var _var47 = s;
            return this.MapComponents((i) => i.Divide(_var47));
        }
        public static Time operator %(Time x, Number s)
        {
            var _var48 = s;
            return x.MapComponents((i) => i.Modulo(_var48));
        }
        public Time Modulo(Number s)
        {
            var _var49 = s;
            return this.MapComponents((i) => i.Modulo(_var49));
        }
        public static Time operator +(Time x, Time y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Time Add(Time y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Time operator -(Time x, Time y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Time Subtract(Time y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Time operator -(Time x) => x.MapComponents((a) => a.Negative);
        public Time Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Time a, Time b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Time b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Time a, Time b) => a.Equals(b).Not;
        public Boolean NotEquals(Time b) => this.Equals(b).Not;
        public Time Half => this.Divide(((Number)2));
        public Time Quarter => this.Divide(((Number)4));
        public Time Tenth => this.Divide(((Number)10));
        public Time Twice => this.Multiply(((Number)2));
        public Time Lerp(Time b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Time Barycentric(Time v2, Time v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public static Boolean operator <(Time a, Time b) => a.LessThanOrEquals(b).And(a.NotEquals(b));
        public Boolean LessThan(Time b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        public static Boolean operator >(Time a, Time b) => b.LessThan(a);
        public Boolean GreaterThan(Time b) => b.LessThan(this);
        public static Boolean operator >=(Time a, Time b) => b.LessThanOrEquals(a);
        public Boolean GreaterThanOrEquals(Time b) => b.LessThanOrEquals(this);
        public Time Lesser(Time b) => this.LessThanOrEquals(b) ? this : b;
        public Time Greater(Time b) => this.GreaterThanOrEquals(b) ? this : b;
        public Integer CompareTo(Time b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        // Unimplemented concept functions
        public static Boolean operator <=(Time x, Time y) => x.LessThanOrEquals(y);
        public Boolean LessThanOrEquals(Time y) => throw new System.NotImplementedException();
        public static Time Multiply(Number other, Time self) => self.Multiply(other);
        public static Time operator *(Number other, Time self) => Multiply(other, self);
    }
    public readonly partial struct DateTime: Coordinate<DateTime>
    {
        public readonly Number Value;
        public DateTime WithValue(Number value) => (value);
        public DateTime(Number value) => (Value) = (value);
        public static DateTime Default = new DateTime();
        public static DateTime New(Number value) => new DateTime(value);
        public static implicit operator Number(DateTime self) => self.Value;
        public static implicit operator DateTime(Number value) => new DateTime(value);
        public static implicit operator DateTime(Integer value) => new DateTime(value);
        public static implicit operator DateTime(int value) => new Integer(value);
        public static implicit operator DateTime(double value) => new Number(value);
        public static implicit operator double(DateTime value) => value.Value;
        public override bool Equals(object obj) { if (!(obj is DateTime)) return false; var other = (DateTime)obj; return Value.Equals(other.Value); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(DateTime self) => new Dynamic(self);
        public static implicit operator DateTime(Dynamic value) => value.As<DateTime>();
        public String TypeName => "DateTime";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Value");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Value));
        // Implemented concept functions and type functions
        public static Boolean operator ==(DateTime a, DateTime b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(DateTime b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(DateTime a, DateTime b) => a.Equals(b).Not;
        public Boolean NotEquals(DateTime b) => this.Equals(b).Not;
        // Unimplemented concept functions
    }
    public readonly partial struct AnglePair: Interval<AnglePair, Angle>
    {
        public readonly Angle Min;
        public readonly Angle Max;
        public AnglePair WithMin(Angle min) => (min, Max);
        public AnglePair WithMax(Angle max) => (Min, max);
        public AnglePair(Angle min, Angle max) => (Min, Max) = (min, max);
        public static AnglePair Default = new AnglePair();
        public static AnglePair New(Angle min, Angle max) => new AnglePair(min, max);
        public static implicit operator (Angle, Angle)(AnglePair self) => (self.Min, self.Max);
        public static implicit operator AnglePair((Angle, Angle) value) => new AnglePair(value.Item1, value.Item2);
        public void Deconstruct(out Angle min, out Angle max) { min = Min; max = Max; }
        public override bool Equals(object obj) { if (!(obj is AnglePair)) return false; var other = (AnglePair)obj; return Min.Equals(other.Min) && Max.Equals(other.Max); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Min, Max);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(AnglePair self) => new Dynamic(self);
        public static implicit operator AnglePair(Dynamic value) => value.As<AnglePair>();
        public String TypeName => "AnglePair";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Min", (String)"Max");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Min), new Dynamic(Max));
        Angle Interval<AnglePair, Angle>.Min => Min;
        Angle Interval<AnglePair, Angle>.Max => Max;
        // Implemented concept functions and type functions
        public Angle Size => this.Max.Subtract(this.Min);
        public Angle Lerp(Number amount) => this.Min.Lerp(this.Max, amount);
        public AnglePair Reverse => this.Max.Tuple2(this.Min);
        public Angle Center => this.Lerp(((Number)0.5));
        public Boolean Contains(Angle value) => value.Between(this.Min, this.Max);
        public Boolean Contains(AnglePair y) => this.Contains(y.Min).And(this.Contains(y.Max));
        public Boolean Overlaps(AnglePair y) => this.Contains(y.Min).Or(this.Contains(y.Max).Or(y.Contains(this.Min).Or(y.Contains(this.Max))));
        public Tuple2<AnglePair, AnglePair> SplitAt(Number t) => this.Left(t).Tuple2(this.Right(t));
        public Tuple2<AnglePair, AnglePair> Split => this.SplitAt(((Number)0.5));
        public AnglePair Left(Number t) => this.Min.Tuple2(this.Lerp(t));
        public AnglePair Right(Number t) => this.Lerp(t).Tuple2(this.Max);
        public AnglePair MoveTo(Angle v) => v.Tuple2(v.Add(this.Size));
        public AnglePair LeftHalf => this.Left(((Number)0.5));
        public AnglePair RightHalf => this.Right(((Number)0.5));
        public AnglePair Recenter(Angle c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public AnglePair Clamp(AnglePair y) => this.Clamp(y.Min).Tuple2(this.Clamp(y.Max));
        public Angle Clamp(Angle value) => value.Clamp(this.Min, this.Max);
        public static Boolean operator ==(AnglePair a, AnglePair b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(AnglePair b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(AnglePair a, AnglePair b) => a.Equals(b).Not;
        public Boolean NotEquals(AnglePair b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 2;
        public Angle this[Integer n] => n == 0 ? Min : n == 1 ? Max : throw new System.IndexOutOfRangeException();
        public Angle At(Integer n) => n == 0 ? Min : n == 1 ? Max : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct NumberInterval: Interval<NumberInterval, Number>
    {
        public readonly Number Min;
        public readonly Number Max;
        public NumberInterval WithMin(Number min) => (min, Max);
        public NumberInterval WithMax(Number max) => (Min, max);
        public NumberInterval(Number min, Number max) => (Min, Max) = (min, max);
        public static NumberInterval Default = new NumberInterval();
        public static NumberInterval New(Number min, Number max) => new NumberInterval(min, max);
        public static implicit operator (Number, Number)(NumberInterval self) => (self.Min, self.Max);
        public static implicit operator NumberInterval((Number, Number) value) => new NumberInterval(value.Item1, value.Item2);
        public void Deconstruct(out Number min, out Number max) { min = Min; max = Max; }
        public override bool Equals(object obj) { if (!(obj is NumberInterval)) return false; var other = (NumberInterval)obj; return Min.Equals(other.Min) && Max.Equals(other.Max); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Min, Max);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(NumberInterval self) => new Dynamic(self);
        public static implicit operator NumberInterval(Dynamic value) => value.As<NumberInterval>();
        public String TypeName => "NumberInterval";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Min", (String)"Max");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Min), new Dynamic(Max));
        Number Interval<NumberInterval, Number>.Min => Min;
        Number Interval<NumberInterval, Number>.Max => Max;
        // Implemented concept functions and type functions
        public Number Size => this.Max.Subtract(this.Min);
        public Number Lerp(Number amount) => this.Min.Lerp(this.Max, amount);
        public NumberInterval Reverse => this.Max.Tuple2(this.Min);
        public Number Center => this.Lerp(((Number)0.5));
        public Boolean Contains(Number value) => value.Between(this.Min, this.Max);
        public Boolean Contains(NumberInterval y) => this.Contains(y.Min).And(this.Contains(y.Max));
        public Boolean Overlaps(NumberInterval y) => this.Contains(y.Min).Or(this.Contains(y.Max).Or(y.Contains(this.Min).Or(y.Contains(this.Max))));
        public Tuple2<NumberInterval, NumberInterval> SplitAt(Number t) => this.Left(t).Tuple2(this.Right(t));
        public Tuple2<NumberInterval, NumberInterval> Split => this.SplitAt(((Number)0.5));
        public NumberInterval Left(Number t) => this.Min.Tuple2(this.Lerp(t));
        public NumberInterval Right(Number t) => this.Lerp(t).Tuple2(this.Max);
        public NumberInterval MoveTo(Number v) => v.Tuple2(v.Add(this.Size));
        public NumberInterval LeftHalf => this.Left(((Number)0.5));
        public NumberInterval RightHalf => this.Right(((Number)0.5));
        public NumberInterval Recenter(Number c) => c.Subtract(this.Size.Half).Tuple2(c.Add(this.Size.Half));
        public NumberInterval Clamp(NumberInterval y) => this.Clamp(y.Min).Tuple2(this.Clamp(y.Max));
        public Number Clamp(Number value) => value.Clamp(this.Min, this.Max);
        public static Boolean operator ==(NumberInterval a, NumberInterval b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(NumberInterval b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(NumberInterval a, NumberInterval b) => a.Equals(b).Not;
        public Boolean NotEquals(NumberInterval b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 2;
        public Number this[Integer n] => n == 0 ? Min : n == 1 ? Max : throw new System.IndexOutOfRangeException();
        public Number At(Integer n) => n == 0 ? Min : n == 1 ? Max : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Vector2D: Vector<Vector2D>
    {
        public readonly Number X;
        public readonly Number Y;
        public Vector2D WithX(Number x) => (x, Y);
        public Vector2D WithY(Number y) => (X, y);
        public Vector2D(Number x, Number y) => (X, Y) = (x, y);
        public static Vector2D Default = new Vector2D();
        public static Vector2D New(Number x, Number y) => new Vector2D(x, y);
        public static implicit operator (Number, Number)(Vector2D self) => (self.X, self.Y);
        public static implicit operator Vector2D((Number, Number) value) => new Vector2D(value.Item1, value.Item2);
        public void Deconstruct(out Number x, out Number y) { x = X; y = Y; }
        public override bool Equals(object obj) { if (!(obj is Vector2D)) return false; var other = (Vector2D)obj; return X.Equals(other.X) && Y.Equals(other.Y); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X, Y);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Vector2D self) => new Dynamic(self);
        public static implicit operator Vector2D(Dynamic value) => value.As<Vector2D>();
        public String TypeName => "Vector2D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X", (String)"Y");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X), new Dynamic(Y));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(X, Y);
        public Vector2D FromComponents(Array<Number> numbers) => new Vector2D(numbers[0], numbers[1]);
        // Implemented concept functions and type functions
        public static implicit operator Vector3D(Vector2D v) => v.X.Tuple3(v.Y, ((Integer)0));
        public Vector3D Vector3D => this.X.Tuple3(this.Y, ((Integer)0));
        public Integer Count => ((Integer)2);
        public Number this[Integer n] => n.Equals(((Integer)0)) ? this.X : this.Y;
        public Number At(Integer n) => n.Equals(((Integer)0)) ? this.X : this.Y;
        public Number Cross(Vector2D b) => this.X.Multiply(b.Y).Subtract(this.Y.Multiply(b.X));
        public static Vector2D operator *(Vector2D x, Vector2D y) => x.ZipComponents(y, (a, b) => a.Multiply(b));
        public Vector2D Multiply(Vector2D y) => this.ZipComponents(y, (a, b) => a.Multiply(b));
        public static Vector2D operator /(Vector2D x, Vector2D y) => x.ZipComponents(y, (a, b) => a.Divide(b));
        public Vector2D Divide(Vector2D y) => this.ZipComponents(y, (a, b) => a.Divide(b));
        public static Vector2D operator %(Vector2D x, Vector2D y) => x.ZipComponents(y, (a, b) => a.Modulo(b));
        public Vector2D Modulo(Vector2D y) => this.ZipComponents(y, (a, b) => a.Modulo(b));
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
        public Vector2D MinValue => this.MapComponents((x) => x.MinValue);
        public Vector2D MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Vector2D a, Vector2D b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Vector2D Clamp(Vector2D a, Vector2D b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Vector2D ClampOne => this.Clamp(this.Zero, this.One);
        public static Vector2D operator *(Vector2D x, Number s)
        {
            var _var50 = s;
            return x.MapComponents((i) => i.Multiply(_var50));
        }
        public Vector2D Multiply(Number s)
        {
            var _var51 = s;
            return this.MapComponents((i) => i.Multiply(_var51));
        }
        public static Vector2D operator /(Vector2D x, Number s)
        {
            var _var52 = s;
            return x.MapComponents((i) => i.Divide(_var52));
        }
        public Vector2D Divide(Number s)
        {
            var _var53 = s;
            return this.MapComponents((i) => i.Divide(_var53));
        }
        public static Vector2D operator %(Vector2D x, Number s)
        {
            var _var54 = s;
            return x.MapComponents((i) => i.Modulo(_var54));
        }
        public Vector2D Modulo(Number s)
        {
            var _var55 = s;
            return this.MapComponents((i) => i.Modulo(_var55));
        }
        public static Vector2D operator +(Vector2D x, Vector2D y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Vector2D Add(Vector2D y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Vector2D operator -(Vector2D x, Vector2D y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Vector2D Subtract(Vector2D y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Vector2D operator -(Vector2D x) => x.MapComponents((a) => a.Negative);
        public Vector2D Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Vector2D a, Vector2D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Vector2D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Vector2D a, Vector2D b) => a.Equals(b).Not;
        public Boolean NotEquals(Vector2D b) => this.Equals(b).Not;
        public Vector2D Half => this.Divide(((Number)2));
        public Vector2D Quarter => this.Divide(((Number)4));
        public Vector2D Tenth => this.Divide(((Number)10));
        public Vector2D Twice => this.Multiply(((Number)2));
        public Vector2D Lerp(Vector2D b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Vector2D Barycentric(Vector2D v2, Vector2D v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public Vector2D Pow2 => this.Multiply(this);
        public Vector2D Pow3 => this.Pow2.Multiply(this);
        public Vector2D Pow4 => this.Pow3.Multiply(this);
        public Vector2D Pow5 => this.Pow4.Multiply(this);
        public Vector2D Square => this.Pow2;
        public Vector2D Cube => this.Pow3;
        public Vector2D ParabolaFunction => this.Square;
        // Unimplemented concept functions
        public static Vector2D Multiply(Number other, Vector2D self) => self.Multiply(other);
        public static Vector2D operator *(Number other, Vector2D self) => Multiply(other, self);
    }
    public readonly partial struct Vector3D: Vector<Vector3D>
    {
        public readonly Number X;
        public readonly Number Y;
        public readonly Number Z;
        public Vector3D WithX(Number x) => (x, Y, Z);
        public Vector3D WithY(Number y) => (X, y, Z);
        public Vector3D WithZ(Number z) => (X, Y, z);
        public Vector3D(Number x, Number y, Number z) => (X, Y, Z) = (x, y, z);
        public static Vector3D Default = new Vector3D();
        public static Vector3D New(Number x, Number y, Number z) => new Vector3D(x, y, z);
        public static implicit operator (Number, Number, Number)(Vector3D self) => (self.X, self.Y, self.Z);
        public static implicit operator Vector3D((Number, Number, Number) value) => new Vector3D(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Number x, out Number y, out Number z) { x = X; y = Y; z = Z; }
        public override bool Equals(object obj) { if (!(obj is Vector3D)) return false; var other = (Vector3D)obj; return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X, Y, Z);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Vector3D self) => new Dynamic(self);
        public static implicit operator Vector3D(Dynamic value) => value.As<Vector3D>();
        public String TypeName => "Vector3D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X", (String)"Y", (String)"Z");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X), new Dynamic(Y), new Dynamic(Z));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(X, Y, Z);
        public Vector3D FromComponents(Array<Number> numbers) => new Vector3D(numbers[0], numbers[1], numbers[2]);
        // Implemented concept functions and type functions
        public static implicit operator Vector4D(Vector3D v) => v.X.Tuple4(v.Y, v.Z, ((Integer)0));
        public Vector4D Vector4D => this.X.Tuple4(this.Y, this.Z, ((Integer)0));
        public Vector2D To2D => this.X.Tuple2(this.Y);
        public Integer Count => ((Integer)3);
        public Number this[Integer n] => n.Equals(((Integer)0)) ? this.X : n.Equals(((Integer)1)) ? this.Y : this.Z;
        public Number At(Integer n) => n.Equals(((Integer)0)) ? this.X : n.Equals(((Integer)1)) ? this.Y : this.Z;
        public Vector3D Cross(Vector3D b) => this.Y.Multiply(b.Z).Subtract(this.Z.Multiply(b.Y)).Tuple3(this.Z.Multiply(b.X).Subtract(this.X.Multiply(b.Z)), this.X.Multiply(b.Y).Subtract(this.Y.Multiply(b.X)));
        public Number MixedProduct(Vector3D b, Vector3D c) => this.Cross(b).Dot(c);
        public static Vector3D operator *(Vector3D x, Vector3D y) => x.ZipComponents(y, (a, b) => a.Multiply(b));
        public Vector3D Multiply(Vector3D y) => this.ZipComponents(y, (a, b) => a.Multiply(b));
        public static Vector3D operator /(Vector3D x, Vector3D y) => x.ZipComponents(y, (a, b) => a.Divide(b));
        public Vector3D Divide(Vector3D y) => this.ZipComponents(y, (a, b) => a.Divide(b));
        public static Vector3D operator %(Vector3D x, Vector3D y) => x.ZipComponents(y, (a, b) => a.Modulo(b));
        public Vector3D Modulo(Vector3D y) => this.ZipComponents(y, (a, b) => a.Modulo(b));
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
        public Vector3D MinValue => this.MapComponents((x) => x.MinValue);
        public Vector3D MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Vector3D a, Vector3D b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Vector3D Clamp(Vector3D a, Vector3D b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Vector3D ClampOne => this.Clamp(this.Zero, this.One);
        public static Vector3D operator *(Vector3D x, Number s)
        {
            var _var56 = s;
            return x.MapComponents((i) => i.Multiply(_var56));
        }
        public Vector3D Multiply(Number s)
        {
            var _var57 = s;
            return this.MapComponents((i) => i.Multiply(_var57));
        }
        public static Vector3D operator /(Vector3D x, Number s)
        {
            var _var58 = s;
            return x.MapComponents((i) => i.Divide(_var58));
        }
        public Vector3D Divide(Number s)
        {
            var _var59 = s;
            return this.MapComponents((i) => i.Divide(_var59));
        }
        public static Vector3D operator %(Vector3D x, Number s)
        {
            var _var60 = s;
            return x.MapComponents((i) => i.Modulo(_var60));
        }
        public Vector3D Modulo(Number s)
        {
            var _var61 = s;
            return this.MapComponents((i) => i.Modulo(_var61));
        }
        public static Vector3D operator +(Vector3D x, Vector3D y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Vector3D Add(Vector3D y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Vector3D operator -(Vector3D x, Vector3D y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Vector3D Subtract(Vector3D y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Vector3D operator -(Vector3D x) => x.MapComponents((a) => a.Negative);
        public Vector3D Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Vector3D a, Vector3D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Vector3D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Vector3D a, Vector3D b) => a.Equals(b).Not;
        public Boolean NotEquals(Vector3D b) => this.Equals(b).Not;
        public Vector3D Half => this.Divide(((Number)2));
        public Vector3D Quarter => this.Divide(((Number)4));
        public Vector3D Tenth => this.Divide(((Number)10));
        public Vector3D Twice => this.Multiply(((Number)2));
        public Vector3D Lerp(Vector3D b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Vector3D Barycentric(Vector3D v2, Vector3D v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public Vector3D Pow2 => this.Multiply(this);
        public Vector3D Pow3 => this.Pow2.Multiply(this);
        public Vector3D Pow4 => this.Pow3.Multiply(this);
        public Vector3D Pow5 => this.Pow4.Multiply(this);
        public Vector3D Square => this.Pow2;
        public Vector3D Cube => this.Pow3;
        public Vector3D ParabolaFunction => this.Square;
        // Unimplemented concept functions
        public static Vector3D Multiply(Number other, Vector3D self) => self.Multiply(other);
        public static Vector3D operator *(Number other, Vector3D self) => Multiply(other, self);
    }
    public readonly partial struct Vector4D: Vector<Vector4D>
    {
        public readonly Number X;
        public readonly Number Y;
        public readonly Number Z;
        public readonly Number W;
        public Vector4D WithX(Number x) => (x, Y, Z, W);
        public Vector4D WithY(Number y) => (X, y, Z, W);
        public Vector4D WithZ(Number z) => (X, Y, z, W);
        public Vector4D WithW(Number w) => (X, Y, Z, w);
        public Vector4D(Number x, Number y, Number z, Number w) => (X, Y, Z, W) = (x, y, z, w);
        public static Vector4D Default = new Vector4D();
        public static Vector4D New(Number x, Number y, Number z, Number w) => new Vector4D(x, y, z, w);
        public static implicit operator (Number, Number, Number, Number)(Vector4D self) => (self.X, self.Y, self.Z, self.W);
        public static implicit operator Vector4D((Number, Number, Number, Number) value) => new Vector4D(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Number x, out Number y, out Number z, out Number w) { x = X; y = Y; z = Z; w = W; }
        public override bool Equals(object obj) { if (!(obj is Vector4D)) return false; var other = (Vector4D)obj; return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(X, Y, Z, W);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Vector4D self) => new Dynamic(self);
        public static implicit operator Vector4D(Dynamic value) => value.As<Vector4D>();
        public String TypeName => "Vector4D";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"X", (String)"Y", (String)"Z", (String)"W");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(X), new Dynamic(Y), new Dynamic(Z), new Dynamic(W));
        // Numerical predefined functions
        public Array<Number> Components => Intrinsics.MakeArray<Number>(X, Y, Z, W);
        public Vector4D FromComponents(Array<Number> numbers) => new Vector4D(numbers[0], numbers[1], numbers[2], numbers[3]);
        // Implemented concept functions and type functions
        public Integer Count => ((Integer)4);
        public Number this[Integer n] => n.Equals(((Integer)0)) ? this.X : n.Equals(((Integer)1)) ? this.Y : n.Equals(((Integer)2)) ? this.Z : this.W;
        public Number At(Integer n) => n.Equals(((Integer)0)) ? this.X : n.Equals(((Integer)1)) ? this.Y : n.Equals(((Integer)2)) ? this.Z : this.W;
        public static Vector4D operator *(Vector4D x, Vector4D y) => x.ZipComponents(y, (a, b) => a.Multiply(b));
        public Vector4D Multiply(Vector4D y) => this.ZipComponents(y, (a, b) => a.Multiply(b));
        public static Vector4D operator /(Vector4D x, Vector4D y) => x.ZipComponents(y, (a, b) => a.Divide(b));
        public Vector4D Divide(Vector4D y) => this.ZipComponents(y, (a, b) => a.Divide(b));
        public static Vector4D operator %(Vector4D x, Vector4D y) => x.ZipComponents(y, (a, b) => a.Modulo(b));
        public Vector4D Modulo(Vector4D y) => this.ZipComponents(y, (a, b) => a.Modulo(b));
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
        public Vector4D MinValue => this.MapComponents((x) => x.MinValue);
        public Vector4D MaxValue => this.MapComponents((x) => x.MaxValue);
        public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean Between(Vector4D a, Vector4D b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x) => x);
        public Boolean BetweenZeroOne => this.Between(this.Zero, this.One);
        public Vector4D Clamp(Vector4D a, Vector4D b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Vector4D ClampOne => this.Clamp(this.Zero, this.One);
        public static Vector4D operator *(Vector4D x, Number s)
        {
            var _var62 = s;
            return x.MapComponents((i) => i.Multiply(_var62));
        }
        public Vector4D Multiply(Number s)
        {
            var _var63 = s;
            return this.MapComponents((i) => i.Multiply(_var63));
        }
        public static Vector4D operator /(Vector4D x, Number s)
        {
            var _var64 = s;
            return x.MapComponents((i) => i.Divide(_var64));
        }
        public Vector4D Divide(Number s)
        {
            var _var65 = s;
            return this.MapComponents((i) => i.Divide(_var65));
        }
        public static Vector4D operator %(Vector4D x, Number s)
        {
            var _var66 = s;
            return x.MapComponents((i) => i.Modulo(_var66));
        }
        public Vector4D Modulo(Number s)
        {
            var _var67 = s;
            return this.MapComponents((i) => i.Modulo(_var67));
        }
        public static Vector4D operator +(Vector4D x, Vector4D y) => x.ZipComponents(y, (a, b) => a.Add(b));
        public Vector4D Add(Vector4D y) => this.ZipComponents(y, (a, b) => a.Add(b));
        public static Vector4D operator -(Vector4D x, Vector4D y) => x.ZipComponents(y, (a, b) => a.Subtract(b));
        public Vector4D Subtract(Vector4D y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        public static Vector4D operator -(Vector4D x) => x.MapComponents((a) => a.Negative);
        public Vector4D Negative => this.MapComponents((a) => a.Negative);
        public static Boolean operator ==(Vector4D a, Vector4D b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Vector4D b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Vector4D a, Vector4D b) => a.Equals(b).Not;
        public Boolean NotEquals(Vector4D b) => this.Equals(b).Not;
        public Vector4D Half => this.Divide(((Number)2));
        public Vector4D Quarter => this.Divide(((Number)4));
        public Vector4D Tenth => this.Divide(((Number)10));
        public Vector4D Twice => this.Multiply(((Number)2));
        public Vector4D Lerp(Vector4D b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        public Vector4D Barycentric(Vector4D v2, Vector4D v3, Vector2D uv) => this.Add(v2.Subtract(this).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y)));
        public Vector4D Pow2 => this.Multiply(this);
        public Vector4D Pow3 => this.Pow2.Multiply(this);
        public Vector4D Pow4 => this.Pow3.Multiply(this);
        public Vector4D Pow5 => this.Pow4.Multiply(this);
        public Vector4D Square => this.Pow2;
        public Vector4D Cube => this.Pow3;
        public Vector4D ParabolaFunction => this.Square;
        // Unimplemented concept functions
        public static Vector4D Multiply(Number other, Vector4D self) => self.Multiply(other);
        public static Vector4D operator *(Number other, Vector4D self) => Multiply(other, self);
    }
    public readonly partial struct Matrix3x3: Value<Matrix3x3>, Array<Vector3D>
    {
        public readonly Vector3D Column1;
        public readonly Vector3D Column2;
        public readonly Vector3D Column3;
        public Matrix3x3 WithColumn1(Vector3D column1) => (column1, Column2, Column3);
        public Matrix3x3 WithColumn2(Vector3D column2) => (Column1, column2, Column3);
        public Matrix3x3 WithColumn3(Vector3D column3) => (Column1, Column2, column3);
        public Matrix3x3(Vector3D column1, Vector3D column2, Vector3D column3) => (Column1, Column2, Column3) = (column1, column2, column3);
        public static Matrix3x3 Default = new Matrix3x3();
        public static Matrix3x3 New(Vector3D column1, Vector3D column2, Vector3D column3) => new Matrix3x3(column1, column2, column3);
        public static implicit operator (Vector3D, Vector3D, Vector3D)(Matrix3x3 self) => (self.Column1, self.Column2, self.Column3);
        public static implicit operator Matrix3x3((Vector3D, Vector3D, Vector3D) value) => new Matrix3x3(value.Item1, value.Item2, value.Item3);
        public void Deconstruct(out Vector3D column1, out Vector3D column2, out Vector3D column3) { column1 = Column1; column2 = Column2; column3 = Column3; }
        public override bool Equals(object obj) { if (!(obj is Matrix3x3)) return false; var other = (Matrix3x3)obj; return Column1.Equals(other.Column1) && Column2.Equals(other.Column2) && Column3.Equals(other.Column3); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Column1, Column2, Column3);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Matrix3x3 self) => new Dynamic(self);
        public static implicit operator Matrix3x3(Dynamic value) => value.As<Matrix3x3>();
        public String TypeName => "Matrix3x3";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Column1", (String)"Column2", (String)"Column3");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Column1), new Dynamic(Column2), new Dynamic(Column3));
        // Implemented concept functions and type functions
        public static Boolean operator ==(Matrix3x3 a, Matrix3x3 b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Matrix3x3 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Matrix3x3 a, Matrix3x3 b) => a.Equals(b).Not;
        public Boolean NotEquals(Matrix3x3 b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 3;
        public Vector3D this[Integer n] => n == 0 ? Column1 : n == 1 ? Column2 : n == 2 ? Column3 : throw new System.IndexOutOfRangeException();
        public Vector3D At(Integer n) => n == 0 ? Column1 : n == 1 ? Column2 : n == 2 ? Column3 : throw new System.IndexOutOfRangeException();
    }
    public readonly partial struct Matrix4x4: Value<Matrix4x4>, Array<Vector4D>
    {
        public readonly Vector4D Column1;
        public readonly Vector4D Column2;
        public readonly Vector4D Column3;
        public readonly Vector4D Column4;
        public Matrix4x4 WithColumn1(Vector4D column1) => (column1, Column2, Column3, Column4);
        public Matrix4x4 WithColumn2(Vector4D column2) => (Column1, column2, Column3, Column4);
        public Matrix4x4 WithColumn3(Vector4D column3) => (Column1, Column2, column3, Column4);
        public Matrix4x4 WithColumn4(Vector4D column4) => (Column1, Column2, Column3, column4);
        public Matrix4x4(Vector4D column1, Vector4D column2, Vector4D column3, Vector4D column4) => (Column1, Column2, Column3, Column4) = (column1, column2, column3, column4);
        public static Matrix4x4 Default = new Matrix4x4();
        public static Matrix4x4 New(Vector4D column1, Vector4D column2, Vector4D column3, Vector4D column4) => new Matrix4x4(column1, column2, column3, column4);
        public static implicit operator (Vector4D, Vector4D, Vector4D, Vector4D)(Matrix4x4 self) => (self.Column1, self.Column2, self.Column3, self.Column4);
        public static implicit operator Matrix4x4((Vector4D, Vector4D, Vector4D, Vector4D) value) => new Matrix4x4(value.Item1, value.Item2, value.Item3, value.Item4);
        public void Deconstruct(out Vector4D column1, out Vector4D column2, out Vector4D column3, out Vector4D column4) { column1 = Column1; column2 = Column2; column3 = Column3; column4 = Column4; }
        public override bool Equals(object obj) { if (!(obj is Matrix4x4)) return false; var other = (Matrix4x4)obj; return Column1.Equals(other.Column1) && Column2.Equals(other.Column2) && Column3.Equals(other.Column3) && Column4.Equals(other.Column4); }
        public override int GetHashCode() => Intrinsics.CombineHashCodes(Column1, Column2, Column3, Column4);
        public override string ToString() => Intrinsics.MakeString(TypeName, FieldNames, FieldValues);
        public static implicit operator Dynamic(Matrix4x4 self) => new Dynamic(self);
        public static implicit operator Matrix4x4(Dynamic value) => value.As<Matrix4x4>();
        public String TypeName => "Matrix4x4";
        public Array<String> FieldNames => Intrinsics.MakeArray<String>((String)"Column1", (String)"Column2", (String)"Column3", (String)"Column4");
        public Array<Dynamic> FieldValues => Intrinsics.MakeArray<Dynamic>(new Dynamic(Column1), new Dynamic(Column2), new Dynamic(Column3), new Dynamic(Column4));
        // Implemented concept functions and type functions
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
        public static Vector3D operator *(Matrix4x4 m, Vector3D v) => v.X.Multiply(m.M11).Add(v.Y.Multiply(m.M21).Add(v.Z.Multiply(m.M31).Add(m.M41))).Tuple3(v.X.Multiply(m.M12).Add(v.Y.Multiply(m.M22).Add(v.Z.Multiply(m.M32).Add(m.M42))), v.X.Multiply(m.M13).Add(v.Y.Multiply(m.M23).Add(v.Z.Multiply(m.M33).Add(m.M43))));
        public Vector3D Multiply(Vector3D v) => v.X.Multiply(this.M11).Add(v.Y.Multiply(this.M21).Add(v.Z.Multiply(this.M31).Add(this.M41))).Tuple3(v.X.Multiply(this.M12).Add(v.Y.Multiply(this.M22).Add(v.Z.Multiply(this.M32).Add(this.M42))), v.X.Multiply(this.M13).Add(v.Y.Multiply(this.M23).Add(v.Z.Multiply(this.M33).Add(this.M43))));
        public static Boolean operator ==(Matrix4x4 a, Matrix4x4 b) => a.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public Boolean Equals(Matrix4x4 b) => this.FieldValues.Zip(b.FieldValues, (a0, b0) => a0.Equals(b0)).All((x) => x);
        public static Boolean operator !=(Matrix4x4 a, Matrix4x4 b) => a.Equals(b).Not;
        public Boolean NotEquals(Matrix4x4 b) => this.Equals(b).Not;
        // Unimplemented concept functions
        public Integer Count => 4;
        public Vector4D this[Integer n] => n == 0 ? Column1 : n == 1 ? Column2 : n == 2 ? Column3 : n == 3 ? Column4 : throw new System.IndexOutOfRangeException();
        public Vector4D At(Integer n) => n == 0 ? Column1 : n == 1 ? Column2 : n == 2 ? Column3 : n == 3 ? Column4 : throw new System.IndexOutOfRangeException();
    }
    public static class Constants
    {
        public static Number MinNumber => Intrinsics.MinNumber;
        public static Number MaxNumber => Intrinsics.MaxNumber;
        public static Number Pi => ((Number)3.1415926535897);
        public static Number TwoPi => Constants.Pi.Twice;
        public static Number HalfPi => Constants.Pi.Half;
        public static Number Epsilon => ((Number)1E-15);
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
    }
}
