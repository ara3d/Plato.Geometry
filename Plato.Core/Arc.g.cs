// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Arc: IOpenShape2D
    {
        // Fields
        [DataMember] public readonly AnglePair Angles;
        [DataMember] public readonly Circle Circle;

        // With functions 
        [MethodImpl(AggressiveInlining)] public Arc WithAngles(AnglePair angles) => new Arc(angles, Circle);
        [MethodImpl(AggressiveInlining)] public Arc WithCircle(Circle circle) => new Arc(Angles, circle);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public Arc(AnglePair angles, Circle circle) { Angles = angles; Circle = circle; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Arc Create(AnglePair angles, Circle circle) => new Arc(angles, circle);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (AnglePair, Circle)(Arc self) => (self.Angles, self.Circle);
        [MethodImpl(AggressiveInlining)] public static implicit operator Arc((AnglePair, Circle) value) => new(value.Item1, value.Item2);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out AnglePair angles, out Circle circle) { angles = Angles; circle = Circle;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Arc other) => Angles.Equals(other.Angles) && Circle.Equals(other.Circle);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Arc other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Angles, Circle);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Angles\" = {Angles}, \"Circle\" = {Circle} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented concept functions and type functions
        public Boolean Closed { [MethodImpl(AggressiveInlining)] get  => ((Boolean)false); } 

        // Unimplemented concept functions
    }
}