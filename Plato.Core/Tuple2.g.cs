// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Tuple2<T0, T1>
    {
        // Fields
        [DataMember] public readonly T0 X0;
        [DataMember] public readonly T1 X1;

        // With functions 
        [MethodImpl(AggressiveInlining)] public Tuple2<T0, T1> WithX0(T0 x0) => new Tuple2<T0, T1>(x0, X1);
        [MethodImpl(AggressiveInlining)] public Tuple2<T0, T1> WithX1(T1 x1) => new Tuple2<T0, T1>(X0, x1);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public Tuple2(T0 x0, T1 x1) { X0 = x0; X1 = x1; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Tuple2<T0, T1> Create(T0 x0, T1 x1) => new Tuple2<T0, T1>(x0, x1);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (T0, T1)(Tuple2<T0, T1> self) => (self.X0, self.X1);
        [MethodImpl(AggressiveInlining)] public static implicit operator Tuple2<T0, T1>((T0, T1) value) => new(value.Item1, value.Item2);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out T0 x0, out T1 x1) { x0 = X0; x1 = X1;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Tuple2<T0, T1> other) => X0.Equals(other.X0) && X1.Equals(other.X1);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Tuple2<T0, T1> other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(X0, X1);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"X0\" = {X0}, \"X1\" = {X1} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented concept functions and type functions

        // Unimplemented concept functions
    }
}