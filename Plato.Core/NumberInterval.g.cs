// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct NumberInterval: IInterval<NumberInterval, Number>
    {
        // Fields
        [DataMember] public readonly Number Start;
        [DataMember] public readonly Number End;

        // With functions 
        [MethodImpl(AggressiveInlining)] public NumberInterval WithStart(Number start) => new NumberInterval(start, End);
        [MethodImpl(AggressiveInlining)] public NumberInterval WithEnd(Number end) => new NumberInterval(Start, end);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public NumberInterval(Number start, Number end) { Start = start; End = end; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static NumberInterval Create(Number start, Number end) => new NumberInterval(start, end);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Number, Number)(NumberInterval self) => (self.Start, self.End);
        [MethodImpl(AggressiveInlining)] public static implicit operator NumberInterval((Number, Number) value) => new(value.Item1, value.Item2);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Number start, out Number end) { start = Start; end = End;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(NumberInterval other) => Start.Equals(other.Start) && End.Equals(other.End);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is NumberInterval other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Start, End);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Start\" = {Start}, \"End\" = {End} }}";

        // Explicit implementation of interfaces by forwarding properties to fields
        Number IInterval<NumberInterval, Number>.Start { [MethodImpl(AggressiveInlining)] get => Start; }
        Number IInterval<NumberInterval, Number>.End { [MethodImpl(AggressiveInlining)] get => End; }

        // Array predefined functions
        [MethodImpl(AggressiveInlining)] public NumberInterval(IReadOnlyList<Number> xs) : this(xs[0], xs[1]) { }
        [MethodImpl(AggressiveInlining)] public NumberInterval(Number[] xs) : this(xs[0], xs[1]) { }
        [MethodImpl(AggressiveInlining)] public static NumberInterval Create(IReadOnlyList<Number> xs) => new NumberInterval(xs);
        // Implementation of IReadOnlyList
        [MethodImpl(AggressiveInlining)] public System.Collections.Generic.IEnumerator<Number> GetEnumerator() => new ArrayEnumerator<Number>(this);
        [MethodImpl(AggressiveInlining)] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Number System.Collections.Generic.IReadOnlyList<Number>.this[int n] { [MethodImpl(AggressiveInlining)] get => At(n); }
        int System.Collections.Generic.IReadOnlyCollection<Number>.Count { [MethodImpl(AggressiveInlining)] get => this.Count; }

        // Implemented concept functions and type functions
        public Number Size { [MethodImpl(AggressiveInlining)] get  => this.End.Subtract(this.Start); } 
        [MethodImpl(AggressiveInlining)]  public Number Lerp(Number amount) => this.Start.Lerp(this.End, amount);
        public NumberInterval Reverse { [MethodImpl(AggressiveInlining)] get  => (this.End, this.Start); } 
        public Number Center { [MethodImpl(AggressiveInlining)] get  => this.Lerp(((Number)0.5)); } 
        [MethodImpl(AggressiveInlining)]  public Boolean Contains(Number value) => value.Between(this.Start, this.End);
        [MethodImpl(AggressiveInlining)]  public Boolean Contains(NumberInterval y) => this.Contains(y.Start).And(this.Contains(y.End));
        [MethodImpl(AggressiveInlining)]  public Boolean Overlaps(NumberInterval y) => this.Contains(y.Start).Or(this.Contains(y.End).Or(y.Contains(this.Start).Or(y.Contains(this.End))));
        [MethodImpl(AggressiveInlining)]  public Tuple2<NumberInterval, NumberInterval> SplitAt(Number t) => (this.Left(t), this.Right(t));
        public Tuple2<NumberInterval, NumberInterval> Split { [MethodImpl(AggressiveInlining)] get  => this.SplitAt(((Number)0.5)); } 
        [MethodImpl(AggressiveInlining)]  public NumberInterval Left(Number t) => (this.Start, this.Lerp(t));
        [MethodImpl(AggressiveInlining)]  public NumberInterval Right(Number t) => (this.Lerp(t), this.End);
        [MethodImpl(AggressiveInlining)]  public NumberInterval MoveTo(Number v) => (v, v.Add(this.Size));
        public NumberInterval LeftHalf { [MethodImpl(AggressiveInlining)] get  => this.Left(((Number)0.5)); } 
        public NumberInterval RightHalf { [MethodImpl(AggressiveInlining)] get  => this.Right(((Number)0.5)); } 
        [MethodImpl(AggressiveInlining)]  public NumberInterval Recenter(Number c) => (c.Subtract(this.Size.Half), c.Add(this.Size.Half));
        [MethodImpl(AggressiveInlining)]  public NumberInterval Clamp(NumberInterval y) => (this.Clamp(y.Start), this.Clamp(y.End));
        [MethodImpl(AggressiveInlining)]  public Number Clamp(Number value) => value.Clamp(this.Start, this.End);
        [MethodImpl(AggressiveInlining)]  public IArray<Number> LinearSpace(Integer count){
            var _var62 = this;
            return count.LinearSpace.Map((x) => _var62.Lerp(x));
        }

        [MethodImpl(AggressiveInlining)]  public IArray<Number> LinearSpaceExclusive(Integer count){
            var _var63 = this;
            return count.LinearSpaceExclusive.Map((x) => _var63.Lerp(x));
        }

        [MethodImpl(AggressiveInlining)]  public IArray<Number> GeometricSpace(Integer count){
            var _var64 = this;
            return count.GeometricSpace.Map((x) => _var64.Lerp(x));
        }

        [MethodImpl(AggressiveInlining)]  public IArray<Number> GeometricSpaceExclusive(Integer count){
            var _var65 = this;
            return count.GeometricSpaceExclusive.Map((x) => _var65.Lerp(x));
        }

        [MethodImpl(AggressiveInlining)]  public NumberInterval Subdivide(Number start, Number end) => (this.Lerp(start), this.Lerp(end));
        [MethodImpl(AggressiveInlining)]  public NumberInterval Subdivide(NumberInterval subInterval) => this.Subdivide(subInterval.Start, subInterval.End);
        [MethodImpl(AggressiveInlining)]  public IArray<NumberInterval> Subdivide(Integer count){
            var _var66 = this;
            return count.Intervals.Map((i) => _var66.Subdivide(i));
        }

        [MethodImpl(AggressiveInlining)]  public IArray<NumberInterval> Repeat(Integer n){
            var _var67 = this;
            return n.MapRange((i) => _var67);
        }


        // Unimplemented concept functions
        public Integer Count { [MethodImpl(AggressiveInlining)] get => 2; }
        [MethodImpl(AggressiveInlining)]  public Number At(Integer n) { [MethodImpl(AggressiveInlining)] get => n == 0 ? Start : n == 1 ? End : throw new System.IndexOutOfRangeException(); }
        public Number this[Integer n] => n == 0 ? Start : n == 1 ? End : throw new System.IndexOutOfRangeException();
    }
}