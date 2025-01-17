// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Vector3: IVector<Vector3>
    {
        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Vector3 Create(Number x, Number y, Number z) => new Vector3(x, y, z);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Number, Number, Number)(Vector3 self) => (self.X, self.Y, self.Z);
        [MethodImpl(AggressiveInlining)] public static implicit operator Vector3((Number, Number, Number) value) => new(value.Item1, value.Item2, value.Item3);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Number x, out Number y, out Number z) { x = X; y = Y; z = Z;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Vector3 other) => Value.Equals(other.Value);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Vector3 other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override string ToString() => Value.ToString();
        [MethodImpl(AggressiveInlining)] public static Boolean operator==(Vector3 a, Vector3 b) => a.Equals(b);
        [MethodImpl(AggressiveInlining)] public static Boolean operator!=(Vector3 a, Vector3 b) => !a.Equals(b);

        // Explicit implementation of interfaces by forwarding properties to fields

        // Array predefined functions
        [MethodImpl(AggressiveInlining)] public Vector3(IReadOnlyList<Number> xs) : this(xs[0], xs[1], xs[2]) { }
        [MethodImpl(AggressiveInlining)] public Vector3(Number[] xs) : this(xs[0], xs[1], xs[2]) { }
        [MethodImpl(AggressiveInlining)] public static Vector3 Create(IReadOnlyList<Number> xs) => new Vector3(xs);
        // Implementation of IReadOnlyList
        [MethodImpl(AggressiveInlining)] public System.Collections.Generic.IEnumerator<Number> GetEnumerator() => new ArrayEnumerator<Number>(this);
        [MethodImpl(AggressiveInlining)] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Number System.Collections.Generic.IReadOnlyList<Number>.this[int n] { [MethodImpl(AggressiveInlining)] get => At(n); }
        int System.Collections.Generic.IReadOnlyCollection<Number>.Count { [MethodImpl(AggressiveInlining)] get => this.Count; }

        // Numerical predefined functions
        public IArray<Number> Components { [MethodImpl(AggressiveInlining)] get => Intrinsics.MakeArray<Number>(X, Y, Z); }
        [MethodImpl(AggressiveInlining)] public Vector3 FromComponents(IArray<Number> numbers) => new Vector3(numbers[0], numbers[1], numbers[2]);

        // Implemented concept functions and type functions
        public static Vector3 UnitX { [MethodImpl(AggressiveInlining)] get  => (((Number)1), ((Number)0), ((Number)0)); } 
        public static Vector3 UnitY { [MethodImpl(AggressiveInlining)] get  => (((Number)0), ((Number)1), ((Number)0)); } 
        public static Vector3 UnitZ { [MethodImpl(AggressiveInlining)] get  => (((Number)0), ((Number)0), ((Number)1)); } 
        public static Vector3 MinValue { [MethodImpl(AggressiveInlining)] get  => (Constants.MinNumber, Constants.MinNumber, Constants.MinNumber); } 
        public static Vector3 MaxValue { [MethodImpl(AggressiveInlining)] get  => (Constants.MaxNumber, Constants.MaxNumber, Constants.MaxNumber); } 
        [MethodImpl(AggressiveInlining)]  public Vector3 Deform(System.Func<Vector3, Vector3> f) => f.Invoke(this);
        public Vector3 XZY { [MethodImpl(AggressiveInlining)] get  => (this.X, this.Z, this.Y); } 
        public Vector3 YXZ { [MethodImpl(AggressiveInlining)] get  => (this.Y, this.X, this.Z); } 
        public Vector3 YZX { [MethodImpl(AggressiveInlining)] get  => (this.Y, this.Z, this.X); } 
        public Vector3 ZXY { [MethodImpl(AggressiveInlining)] get  => (this.Z, this.X, this.Y); } 
        public Vector3 ZYX { [MethodImpl(AggressiveInlining)] get  => (this.Z, this.Y, this.X); } 
        public Vector2 XY { [MethodImpl(AggressiveInlining)] get  => (this.X, this.Y); } 
        public Vector2 YX { [MethodImpl(AggressiveInlining)] get  => (this.Y, this.X); } 
        public Vector2 XZ { [MethodImpl(AggressiveInlining)] get  => (this.X, this.Z); } 
        public Vector2 ZX { [MethodImpl(AggressiveInlining)] get  => (this.Z, this.X); } 
        public Vector2 YZ { [MethodImpl(AggressiveInlining)] get  => (this.Y, this.Z); } 
        public Vector2 ZY { [MethodImpl(AggressiveInlining)] get  => (this.Z, this.Y); } 
        [MethodImpl(AggressiveInlining)]  public Line3D Line(Vector3 b) => (this, b);
        [MethodImpl(AggressiveInlining)]  public Ray3D Ray(Vector3 b) => (this, b);
        [MethodImpl(AggressiveInlining)]  public Ray3D RayTo(Vector3 b) => (this, b.Subtract(this));
        [MethodImpl(AggressiveInlining)]  public Vector3 Project(Plane p) => this.Subtract(p.Normal.Multiply(p.Normal.Dot(this)));
        public Vector2 To2D { [MethodImpl(AggressiveInlining)] get  => (this.X, this.Y); } 
        [MethodImpl(AggressiveInlining)]  public Number MixedProduct(Vector3 b, Vector3 c) => this.Cross(b).Dot(c);
        public Vector4 Vector4 { [MethodImpl(AggressiveInlining)] get  => this.Vector4(((Integer)0)); } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator Vector4(Vector3 v) => v.Vector4;
        [MethodImpl(AggressiveInlining)]  public Vector4 Vector4(Number w) => (this.X, this.Y, this.Z, w);
        [MethodImpl(AggressiveInlining)]  public Vector3 Modulo(Vector3 y) => this.ZipComponents(y, (a, b) => a.Modulo(b));
        [MethodImpl(AggressiveInlining)]  public Vector2 MidPoint(Vector3 b) => this.Add(b).Divide(((Number)2));
        public Number Sum { [MethodImpl(AggressiveInlining)] get  => this.Reduce(((Number)0), (a, b) => a.Add(b)); } 
        public Number SumSquares { [MethodImpl(AggressiveInlining)] get  => this.Square.Sum; } 
        public Number MagnitudeSquared { [MethodImpl(AggressiveInlining)] get  => this.SumSquares; } 
        public Number Magnitude { [MethodImpl(AggressiveInlining)] get  => this.MagnitudeSquared.SquareRoot; } 
        [MethodImpl(AggressiveInlining)]  public Vector3 Project(Vector3 other) => other.Multiply(this.Dot(other));
        [MethodImpl(AggressiveInlining)]  public Angle Angle(Vector3 b) => this.Dot(b).Divide(this.Magnitude.Multiply(b.Magnitude)).Acos;
        [MethodImpl(AggressiveInlining)]  public Boolean IsParallel(Vector3 b) => this.Dot(b).Abs.GreaterThan(((Number)1).Subtract(Constants.Epsilon));
        public Vector3 PlusOne { [MethodImpl(AggressiveInlining)] get  => this.Add(this.One); } 
        public Vector3 MinusOne { [MethodImpl(AggressiveInlining)] get  => this.Subtract(this.One); } 
        public Vector3 FromOne { [MethodImpl(AggressiveInlining)] get  => this.One.Subtract(this); } 
        [MethodImpl(AggressiveInlining)]  public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents { [MethodImpl(AggressiveInlining)] get  => this.Components.Count; } 
        [MethodImpl(AggressiveInlining)]  public Vector3 MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        [MethodImpl(AggressiveInlining)]  public Vector3 ZipComponents(Vector3 y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Vector3 Zero { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((i) => ((Number)0)); } 
        public Vector3 One { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((i) => ((Number)1)); } 
        public Number MaxComponent { [MethodImpl(AggressiveInlining)] get {
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
         } 
        public Number MinComponent { [MethodImpl(AggressiveInlining)] get {
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
         } 
        [MethodImpl(AggressiveInlining)]  public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        [MethodImpl(AggressiveInlining)]  public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        [MethodImpl(AggressiveInlining)]  public Boolean Between(Vector3 a, Vector3 b) => this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Between(a0, b0)).All((x0) => x0);
        public Boolean BetweenZeroOne { [MethodImpl(AggressiveInlining)] get  => this.Between(this.Zero, this.One); } 
        public Vector3 ClampZeroOne { [MethodImpl(AggressiveInlining)] get  => this.Clamp(this.Zero, this.One); } 
        public Vector3 Clamp01 { [MethodImpl(AggressiveInlining)] get  => this.ClampZeroOne; } 
        public Vector3 Fract { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((c) => c.Fract); } 
        [MethodImpl(AggressiveInlining)]  public Vector3 Modulo(Number s){
            var _var312 = s;
            return this.MapComponents((i) => i.Modulo(_var312));
        }

        [MethodImpl(AggressiveInlining)]  public IArray<Vector3> Repeat(Integer n){
            var _var313 = this;
            return n.MapRange((i) => _var313);
        }

        public Vector3 Half { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)2)); } 
        public Vector3 Quarter { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)4)); } 
        public Vector3 Eight { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)8)); } 
        public Vector3 Sixteenth { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)16)); } 
        public Vector3 Tenth { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)10)); } 
        public Vector3 Twice { [MethodImpl(AggressiveInlining)] get  => this.Multiply(((Number)2)); } 
        public Vector3 Hundred { [MethodImpl(AggressiveInlining)] get  => this.Multiply(((Number)100)); } 
        public Vector3 Thousand { [MethodImpl(AggressiveInlining)] get  => this.Multiply(((Number)1000)); } 
        public Vector3 Million { [MethodImpl(AggressiveInlining)] get  => this.Thousand.Thousand; } 
        public Vector3 Billion { [MethodImpl(AggressiveInlining)] get  => this.Thousand.Million; } 
        public Vector3 Pow2 { [MethodImpl(AggressiveInlining)] get  => this.Multiply(this); } 
        public Vector3 Pow3 { [MethodImpl(AggressiveInlining)] get  => this.Pow2.Multiply(this); } 
        public Vector3 Pow4 { [MethodImpl(AggressiveInlining)] get  => this.Pow3.Multiply(this); } 
        public Vector3 Pow5 { [MethodImpl(AggressiveInlining)] get  => this.Pow4.Multiply(this); } 
        public Vector3 Square { [MethodImpl(AggressiveInlining)] get  => this.Pow2; } 
        public Vector3 Sqr { [MethodImpl(AggressiveInlining)] get  => this.Pow2; } 
        public Vector3 Cube { [MethodImpl(AggressiveInlining)] get  => this.Pow3; } 
        public Vector3 Parabola { [MethodImpl(AggressiveInlining)] get  => this.Sqr; } 
        [MethodImpl(AggressiveInlining)]  public Vector3 Lerp(Vector3 b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        [MethodImpl(AggressiveInlining)]  public Vector3 Barycentric(Vector3 v2, Vector3 v3, Vector2 uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        [MethodImpl(AggressiveInlining)]  public Vector3 CubicBezier(Vector3 b, Vector3 c, Vector3 d, Number t) => this.Multiply(((Number)1).Subtract(t).Cube).Add(b.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr.Multiply(t))).Add(c.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Multiply(t.Sqr))).Add(d.Multiply(t.Cube))));
        [MethodImpl(AggressiveInlining)]  public Vector3 CubicBezierDerivative(Vector3 b, Vector3 c, Vector3 d, Number t) => b.Subtract(this).Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr)).Add(c.Subtract(b).Multiply(((Number)6).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(d.Subtract(c).Multiply(((Number)3).Multiply(t.Sqr))));
        [MethodImpl(AggressiveInlining)]  public Vector3 CubicBezierSecondDerivative(Vector3 b, Vector3 c, Vector3 d, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(((Number)1).Subtract(t))).Add(d.Subtract(c.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(t)));
        [MethodImpl(AggressiveInlining)]  public Vector3 QuadraticBezier(Vector3 b, Vector3 c, Number t) => this.Multiply(((Number)1).Subtract(t).Sqr).Add(b.Multiply(((Number)2).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(c.Multiply(t.Sqr)));
        [MethodImpl(AggressiveInlining)]  public Vector3 QuadraticBezierDerivative(Vector3 b, Vector3 c, Number t) => b.Subtract(b).Multiply(((Number)2).Multiply(((Number)1).Subtract(t))).Add(c.Subtract(b).Multiply(((Number)2).Multiply(t)));
        [MethodImpl(AggressiveInlining)]  public Vector3 QuadraticBezierSecondDerivative(Vector3 b, Vector3 c, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this));

        // Unimplemented concept functions
        public Integer Count { [MethodImpl(AggressiveInlining)] get => 3; }
        [MethodImpl(AggressiveInlining)]  public Number At(Integer n) { [MethodImpl(AggressiveInlining)] get => n == 0 ? X : n == 1 ? Y : n == 2 ? Z : throw new System.IndexOutOfRangeException(); }
        public Number this[Integer n] => n == 0 ? X : n == 1 ? Y : n == 2 ? Z : throw new System.IndexOutOfRangeException();
    }
}
