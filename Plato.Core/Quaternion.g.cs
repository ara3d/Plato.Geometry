// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Quaternion: IValue<Quaternion>, IArray<Number>
    {
        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Quaternion Create(Number x, Number y, Number z, Number w) => new Quaternion(x, y, z, w);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Number, Number, Number, Number)(Quaternion self) => (self.X, self.Y, self.Z, self.W);
        [MethodImpl(AggressiveInlining)] public static implicit operator Quaternion((Number, Number, Number, Number) value) => new(value.Item1, value.Item2, value.Item3, value.Item4);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Number x, out Number y, out Number z, out Number w) { x = X; y = Y; z = Z; w = W;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Quaternion other) => Value.Equals(other.Value);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Quaternion other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override string ToString() => Value.ToString();
        [MethodImpl(AggressiveInlining)] public static Boolean operator==(Quaternion a, Quaternion b) => a.Equals(b);
        [MethodImpl(AggressiveInlining)] public static Boolean operator!=(Quaternion a, Quaternion b) => !a.Equals(b);

        // Explicit implementation of interfaces by forwarding properties to fields

        // Array predefined functions
        [MethodImpl(AggressiveInlining)] public Quaternion(IReadOnlyList<Number> xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        [MethodImpl(AggressiveInlining)] public Quaternion(Number[] xs) : this(xs[0], xs[1], xs[2], xs[3]) { }
        [MethodImpl(AggressiveInlining)] public static Quaternion Create(IReadOnlyList<Number> xs) => new Quaternion(xs);
        // Implementation of IReadOnlyList
        [MethodImpl(AggressiveInlining)] public System.Collections.Generic.IEnumerator<Number> GetEnumerator() => new ArrayEnumerator<Number>(this);
        [MethodImpl(AggressiveInlining)] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Number System.Collections.Generic.IReadOnlyList<Number>.this[int n] { [MethodImpl(AggressiveInlining)] get => At(n); }
        int System.Collections.Generic.IReadOnlyCollection<Number>.Count { [MethodImpl(AggressiveInlining)] get => this.Count; }

        // Implemented concept functions and type functions
        public static Quaternion Identity { [MethodImpl(AggressiveInlining)] get  => (((Number)0), ((Number)0), ((Number)0), ((Number)1)); } 
        [MethodImpl(AggressiveInlining)]  public Vector3 Transform(Vector3 v) => v.Transform(this);
        [MethodImpl(AggressiveInlining)]  public Vector3 Multiply(Vector3 v) => this.Transform(v);
        public Matrix4x4 Matrix4x4 { [MethodImpl(AggressiveInlining)] get  => Matrix4x4.CreateFromQuaternion(this); } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator Matrix4x4(Quaternion q) => q.Matrix4x4;
        [MethodImpl(AggressiveInlining)]  public IArray<Quaternion> Repeat(Integer n){
            var _var320 = this;
            return n.MapRange((i) => _var320);
        }


        // Unimplemented concept functions
        public Integer Count { [MethodImpl(AggressiveInlining)] get => 4; }
        [MethodImpl(AggressiveInlining)]  public Number At(Integer n) { [MethodImpl(AggressiveInlining)] get => n == 0 ? X : n == 1 ? Y : n == 2 ? Z : n == 3 ? W : throw new System.IndexOutOfRangeException(); }
        public Number this[Integer n] => n == 0 ? X : n == 1 ? Y : n == 2 ? Z : n == 3 ? W : throw new System.IndexOutOfRangeException();
    }
}