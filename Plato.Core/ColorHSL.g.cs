// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct ColorHSL: ICoordinate<ColorHSL>
    {
        // Fields
        [DataMember] public readonly Angle Hue;
        [DataMember] public readonly Unit Saturation;
        [DataMember] public readonly Unit Luminance;

        // With functions 
        [MethodImpl(AggressiveInlining)] public ColorHSL WithHue(Angle hue) => new ColorHSL(hue, Saturation, Luminance);
        [MethodImpl(AggressiveInlining)] public ColorHSL WithSaturation(Unit saturation) => new ColorHSL(Hue, saturation, Luminance);
        [MethodImpl(AggressiveInlining)] public ColorHSL WithLuminance(Unit luminance) => new ColorHSL(Hue, Saturation, luminance);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public ColorHSL(Angle hue, Unit saturation, Unit luminance) { Hue = hue; Saturation = saturation; Luminance = luminance; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static ColorHSL Create(Angle hue, Unit saturation, Unit luminance) => new ColorHSL(hue, saturation, luminance);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Angle, Unit, Unit)(ColorHSL self) => (self.Hue, self.Saturation, self.Luminance);
        [MethodImpl(AggressiveInlining)] public static implicit operator ColorHSL((Angle, Unit, Unit) value) => new(value.Item1, value.Item2, value.Item3);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Angle hue, out Unit saturation, out Unit luminance) { hue = Hue; saturation = Saturation; luminance = Luminance;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(ColorHSL other) => Hue.Equals(other.Hue) && Saturation.Equals(other.Saturation) && Luminance.Equals(other.Luminance);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is ColorHSL other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Hue, Saturation, Luminance);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Hue\" = {Hue}, \"Saturation\" = {Saturation}, \"Luminance\" = {Luminance} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented concept functions and type functions
        [MethodImpl(AggressiveInlining)]  public IArray<ColorHSL> Repeat(Integer n){
            var _var36 = this;
            return n.MapRange((i) => _var36);
        }


        // Unimplemented concept functions
    }
}