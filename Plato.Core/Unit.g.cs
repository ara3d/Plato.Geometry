// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Unit: IReal<Unit>
    {
        // Fields
        [DataMember] public readonly Number Value;

        // With functions 
        [MethodImpl(AggressiveInlining)] public Unit WithValue(Number value) => new Unit(value);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public Unit(Number value) { Value = value; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Unit Create(Number value) => new Unit(value);

        // Implicit converters to/from single field
        [MethodImpl(AggressiveInlining)] public static implicit operator Number(Unit self) => self.Value;
        [MethodImpl(AggressiveInlining)] public static implicit operator Unit(Number value) => new Unit(value);
        [MethodImpl(AggressiveInlining)] public static implicit operator Unit(Integer value) => new Unit(value);
        [MethodImpl(AggressiveInlining)] public static implicit operator Unit(int value) => new Integer(value);
        [MethodImpl(AggressiveInlining)] public static implicit operator Unit(float value) => new Number(value);
        [MethodImpl(AggressiveInlining)] public static implicit operator float(Unit value) => value.Value;

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Unit other) => Value.Equals(other.Value);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Unit other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Value);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Value\" = {Value} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Numerical predefined functions
        public IArray<Number> Components { [MethodImpl(AggressiveInlining)] get => Intrinsics.MakeArray<Number>(Value); }
        [MethodImpl(AggressiveInlining)] public Unit FromComponents(IArray<Number> numbers) => new Unit(numbers[0]);

        // Implemented concept functions and type functions
        public Number Magnitude { [MethodImpl(AggressiveInlining)] get  => this.Value; } 
        public Boolean GtZ { [MethodImpl(AggressiveInlining)] get  => this.GreaterThan(this.Zero); } 
        public Boolean LtZ { [MethodImpl(AggressiveInlining)] get  => this.LessThan(this.Zero); } 
        public Boolean GtEqZ { [MethodImpl(AggressiveInlining)] get  => this.GreaterThanOrEquals(this.Zero); } 
        public Boolean LtEqZ { [MethodImpl(AggressiveInlining)] get  => this.LessThanOrEquals(this.Zero); } 
        public Boolean IsPositive { [MethodImpl(AggressiveInlining)] get  => this.GtEqZ; } 
        public Boolean IsNegative { [MethodImpl(AggressiveInlining)] get  => this.LtZ; } 
        public Integer Sign { [MethodImpl(AggressiveInlining)] get  => this.LtZ ? ((Integer)1).Negative : this.GtZ ? ((Integer)1) : ((Integer)0); } 
        public Unit Abs { [MethodImpl(AggressiveInlining)] get  => this.LtZ ? this.Negative : this; } 
        public Unit Inverse { [MethodImpl(AggressiveInlining)] get  => this.One.Divide(this); } 
        [MethodImpl(AggressiveInlining)]  public Boolean Between(Unit min, Unit max) => this.GreaterThanOrEquals(min).And(this.LessThanOrEquals(max));
        [MethodImpl(AggressiveInlining)]  public Unit Multiply(Unit y) => this.FromNumber(this.ToNumber.Multiply(y.ToNumber));
        [MethodImpl(AggressiveInlining)]  public static Unit operator *(Unit x, Unit y) => x.Multiply(y);
        [MethodImpl(AggressiveInlining)]  public Unit Divide(Unit y) => this.FromNumber(this.ToNumber.Divide(y.ToNumber));
        [MethodImpl(AggressiveInlining)]  public static Unit operator /(Unit x, Unit y) => x.Divide(y);
        [MethodImpl(AggressiveInlining)]  public Unit Modulo(Unit y) => this.FromNumber(this.ToNumber.Modulo(y.ToNumber));
        [MethodImpl(AggressiveInlining)]  public static Unit operator %(Unit x, Unit y) => x.Modulo(y);
        public Number Number { [MethodImpl(AggressiveInlining)] get  => this.ToNumber; } 
        public Number ToNumber { [MethodImpl(AggressiveInlining)] get  => this.Component(((Integer)0)); } 
        [MethodImpl(AggressiveInlining)]  public Unit FromNumber(Number n) => this.FromComponents(Intrinsics.MakeArray(n));
        [MethodImpl(AggressiveInlining)]  public Integer Compare(Unit b) => this.ToNumber.LessThan(b.ToNumber) ? ((Integer)1).Negative : this.ToNumber.GreaterThan(b.ToNumber) ? ((Integer)1) : ((Integer)0);
        [MethodImpl(AggressiveInlining)]  public Unit Add(Number y) => this.FromNumber(this.ToNumber.Add(y));
        [MethodImpl(AggressiveInlining)]  public static Unit operator +(Unit x, Number y) => x.Add(y);
        [MethodImpl(AggressiveInlining)]  public Unit Subract(Number y) => this.FromNumber(this.ToNumber.Subtract(y));
        public Unit PlusOne { [MethodImpl(AggressiveInlining)] get  => this.Add(this.One); } 
        public Unit MinusOne { [MethodImpl(AggressiveInlining)] get  => this.Subtract(this.One); } 
        public Unit FromOne { [MethodImpl(AggressiveInlining)] get  => this.One.Subtract(this); } 
        [MethodImpl(AggressiveInlining)]  public Number Component(Integer n) => this.Components.At(n);
        public Integer NumComponents { [MethodImpl(AggressiveInlining)] get  => this.Components.Count; } 
        [MethodImpl(AggressiveInlining)]  public Unit MapComponents(System.Func<Number, Number> f) => this.FromComponents(this.Components.Map(f));
        [MethodImpl(AggressiveInlining)]  public Unit ZipComponents(Unit y, System.Func<Number, Number, Number> f) => this.FromComponents(this.Components.Zip(y.Components, f));
        public Unit Zero { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((i) => ((Number)0)); } 
        public Unit One { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((i) => ((Number)1)); } 
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
        public Unit MinValue { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((x) => x.MinValue); } 
        public Unit MaxValue { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((x) => x.MaxValue); } 
        [MethodImpl(AggressiveInlining)]  public Boolean AllComponents(System.Func<Number, Boolean> predicate) => this.Components.All(predicate);
        [MethodImpl(AggressiveInlining)]  public Boolean AnyComponent(System.Func<Number, Boolean> predicate) => this.Components.Any(predicate);
        public Boolean BetweenZeroOne { [MethodImpl(AggressiveInlining)] get  => this.Between(this.Zero, this.One); } 
        [MethodImpl(AggressiveInlining)]  public Unit Clamp(Unit a, Unit b) => this.FromComponents(this.Components.Zip(a.Components, b.Components, (x0, a0, b0) => x0.Clamp(a0, b0)));
        public Unit ClampZeroOne { [MethodImpl(AggressiveInlining)] get  => this.Clamp(this.Zero, this.One); } 
        public Unit Clamp01 { [MethodImpl(AggressiveInlining)] get  => this.ClampZeroOne; } 
        [MethodImpl(AggressiveInlining)]  public Unit Min(Unit y) => this.ZipComponents(y, (a, b) => a.Min(b));
        [MethodImpl(AggressiveInlining)]  public Unit Max(Unit y) => this.ZipComponents(y, (a, b) => a.Max(b));
        public Unit Floor { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((c) => c.Floor); } 
        public Unit Fract { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((c) => c.Fract); } 
        [MethodImpl(AggressiveInlining)]  public Unit Multiply(Number s){
            var _var45 = s;
            return this.MapComponents((i) => i.Multiply(_var45));
        }

        [MethodImpl(AggressiveInlining)]  public static Unit operator *(Unit x, Number s) => x.Multiply(s);
        [MethodImpl(AggressiveInlining)]  public Unit Divide(Number s){
            var _var46 = s;
            return this.MapComponents((i) => i.Divide(_var46));
        }

        [MethodImpl(AggressiveInlining)]  public static Unit operator /(Unit x, Number s) => x.Divide(s);
        [MethodImpl(AggressiveInlining)]  public Unit Modulo(Number s){
            var _var47 = s;
            return this.MapComponents((i) => i.Modulo(_var47));
        }

        [MethodImpl(AggressiveInlining)]  public static Unit operator %(Unit x, Number s) => x.Modulo(s);
        [MethodImpl(AggressiveInlining)]  public Unit Add(Unit y) => this.ZipComponents(y, (a, b) => a.Add(b));
        [MethodImpl(AggressiveInlining)]  public static Unit operator +(Unit x, Unit y) => x.Add(y);
        [MethodImpl(AggressiveInlining)]  public Unit Subtract(Unit y) => this.ZipComponents(y, (a, b) => a.Subtract(b));
        [MethodImpl(AggressiveInlining)]  public static Unit operator -(Unit x, Unit y) => x.Subtract(y);
        public Unit Negative { [MethodImpl(AggressiveInlining)] get  => this.MapComponents((a) => a.Negative); } 
        [MethodImpl(AggressiveInlining)]  public static Unit operator -(Unit x) => x.Negative;
        [MethodImpl(AggressiveInlining)]  public IArray<Unit> Repeat(Integer n){
            var _var48 = this;
            return n.MapRange((i) => _var48);
        }

        public Unit Half { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)2)); } 
        public Unit Quarter { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)4)); } 
        public Unit Eight { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)8)); } 
        public Unit Sixteenth { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)16)); } 
        public Unit Tenth { [MethodImpl(AggressiveInlining)] get  => this.Divide(((Number)10)); } 
        public Unit Twice { [MethodImpl(AggressiveInlining)] get  => this.Multiply(((Number)2)); } 
        public Unit Hundred { [MethodImpl(AggressiveInlining)] get  => this.Multiply(((Number)100)); } 
        public Unit Thousand { [MethodImpl(AggressiveInlining)] get  => this.Multiply(((Number)1000)); } 
        public Unit Million { [MethodImpl(AggressiveInlining)] get  => this.Thousand.Thousand; } 
        public Unit Billion { [MethodImpl(AggressiveInlining)] get  => this.Thousand.Million; } 
        [MethodImpl(AggressiveInlining)]  public Boolean LessThan(Unit b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        [MethodImpl(AggressiveInlining)]  public static Boolean operator <(Unit a, Unit b) => a.LessThan(b);
        [MethodImpl(AggressiveInlining)]  public Boolean GreaterThan(Unit b) => b.LessThan(this);
        [MethodImpl(AggressiveInlining)]  public static Boolean operator >(Unit a, Unit b) => a.GreaterThan(b);
        [MethodImpl(AggressiveInlining)]  public Boolean GreaterThanOrEquals(Unit b) => b.LessThanOrEquals(this);
        [MethodImpl(AggressiveInlining)]  public static Boolean operator >=(Unit a, Unit b) => a.GreaterThanOrEquals(b);
        [MethodImpl(AggressiveInlining)]  public Unit Lesser(Unit b) => this.LessThanOrEquals(b) ? this : b;
        [MethodImpl(AggressiveInlining)]  public Unit Greater(Unit b) => this.GreaterThanOrEquals(b) ? this : b;
        [MethodImpl(AggressiveInlining)]  public Integer CompareTo(Unit b) => this.LessThanOrEquals(b) ? this.Equals(b) ? ((Integer)0) : ((Integer)1).Negative : ((Integer)1);
        [MethodImpl(AggressiveInlining)]  public Unit CubicBezier(Unit b, Unit c, Unit d, Number t) => this.Multiply(((Number)1).Subtract(t).Cube).Add(b.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr.Multiply(t))).Add(c.Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Multiply(t.Sqr))).Add(d.Multiply(t.Cube))));
        [MethodImpl(AggressiveInlining)]  public Unit CubicBezierDerivative(Unit b, Unit c, Unit d, Number t) => b.Subtract(this).Multiply(((Number)3).Multiply(((Number)1).Subtract(t).Sqr)).Add(c.Subtract(b).Multiply(((Number)6).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(d.Subtract(c).Multiply(((Number)3).Multiply(t.Sqr))));
        [MethodImpl(AggressiveInlining)]  public Unit CubicBezierSecondDerivative(Unit b, Unit c, Unit d, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(((Number)1).Subtract(t))).Add(d.Subtract(c.Multiply(((Number)2)).Add(this)).Multiply(((Number)6).Multiply(t)));
        [MethodImpl(AggressiveInlining)]  public Unit QuadraticBezier(Unit b, Unit c, Number t) => this.Multiply(((Number)1).Subtract(t).Sqr).Add(b.Multiply(((Number)2).Multiply(((Number)1).Subtract(t).Multiply(t))).Add(c.Multiply(t.Sqr)));
        [MethodImpl(AggressiveInlining)]  public Unit QuadraticBezierDerivative(Unit b, Unit c, Number t) => b.Subtract(b).Multiply(((Number)2).Multiply(((Number)1).Subtract(t))).Add(c.Subtract(b).Multiply(((Number)2).Multiply(t)));
        [MethodImpl(AggressiveInlining)]  public Unit QuadraticBezierSecondDerivative(Unit b, Unit c, Number t) => c.Subtract(b.Multiply(((Number)2)).Add(this));
        [MethodImpl(AggressiveInlining)]  public Unit Lerp(Unit b, Number t) => this.Multiply(t.FromOne).Add(b.Multiply(t));
        [MethodImpl(AggressiveInlining)]  public Unit Barycentric(Unit v2, Unit v3, Vector2 uv) => this.Add(v2.Subtract(this)).Multiply(uv.X).Add(v3.Subtract(this).Multiply(uv.Y));
        public Unit Parabola { [MethodImpl(AggressiveInlining)] get  => this.Sqr; } 
        public Unit Pow2 { [MethodImpl(AggressiveInlining)] get  => this.Multiply(this); } 
        public Unit Pow3 { [MethodImpl(AggressiveInlining)] get  => this.Pow2.Multiply(this); } 
        public Unit Pow4 { [MethodImpl(AggressiveInlining)] get  => this.Pow3.Multiply(this); } 
        public Unit Pow5 { [MethodImpl(AggressiveInlining)] get  => this.Pow4.Multiply(this); } 
        public Unit Square { [MethodImpl(AggressiveInlining)] get  => this.Pow2; } 
        public Unit Sqr { [MethodImpl(AggressiveInlining)] get  => this.Pow2; } 
        public Unit Cube { [MethodImpl(AggressiveInlining)] get  => this.Pow3; } 

        // Unimplemented concept functions
        [MethodImpl(AggressiveInlining)]  public Boolean LessThanOrEquals(Unit y) => throw new NotImplementedException();
        [MethodImpl(AggressiveInlining)]  public static Boolean operator <=(Unit x, Unit y) => x.LessThanOrEquals(y);
    }
}