// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Cylinder: ISurface
    {
        // Fields
        [DataMember] public readonly Number Height;
        [DataMember] public readonly Number Radius;

        // With functions 
        [MethodImpl(AggressiveInlining)] public Cylinder WithHeight(Number height) => new Cylinder(height, Radius);
        [MethodImpl(AggressiveInlining)] public Cylinder WithRadius(Number radius) => new Cylinder(Height, radius);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public Cylinder(Number height, Number radius) { Height = height; Radius = radius; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Cylinder Create(Number height, Number radius) => new Cylinder(height, radius);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Number, Number)(Cylinder self) => (self.Height, self.Radius);
        [MethodImpl(AggressiveInlining)] public static implicit operator Cylinder((Number, Number) value) => new(value.Item1, value.Item2);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Number height, out Number radius) { height = Height; radius = Radius;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Cylinder other) => Height.Equals(other.Height) && Radius.Equals(other.Radius);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Cylinder other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Height, Radius);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Height\" = {Height}, \"Radius\" = {Radius} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented concept functions and type functions

        // Unimplemented concept functions
        [MethodImpl(AggressiveInlining)]  public Number Distance(Vector3 p) => throw new NotImplementedException();
    }
}