// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct ColorLAB: ICoordinate<ColorLAB>
    {
        // Fields
        [DataMember] public readonly Unit Lightness;
        [DataMember] public readonly Number A;
        [DataMember] public readonly Number B;

        // With functions 
        [MethodImpl(AggressiveInlining)] public ColorLAB WithLightness(Unit lightness) => new ColorLAB(lightness, A, B);
        [MethodImpl(AggressiveInlining)] public ColorLAB WithA(Number a) => new ColorLAB(Lightness, a, B);
        [MethodImpl(AggressiveInlining)] public ColorLAB WithB(Number b) => new ColorLAB(Lightness, A, b);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public ColorLAB(Unit lightness, Number a, Number b) { Lightness = lightness; A = a; B = b; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static ColorLAB Create(Unit lightness, Number a, Number b) => new ColorLAB(lightness, a, b);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Unit, Number, Number)(ColorLAB self) => (self.Lightness, self.A, self.B);
        [MethodImpl(AggressiveInlining)] public static implicit operator ColorLAB((Unit, Number, Number) value) => new(value.Item1, value.Item2, value.Item3);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Unit lightness, out Number a, out Number b) { lightness = Lightness; a = A; b = B;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(ColorLAB other) => Lightness.Equals(other.Lightness) && A.Equals(other.A) && B.Equals(other.B);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is ColorLAB other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Lightness, A, B);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Lightness\" = {Lightness}, \"A\" = {A}, \"B\" = {B} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented concept functions and type functions
        [MethodImpl(AggressiveInlining)]  public IArray<ColorLAB> Repeat(Integer n){
            var _var33 = this;
            return n.MapRange((i) => _var33);
        }


        // Unimplemented concept functions
    }
}