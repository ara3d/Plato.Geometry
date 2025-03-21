// Autogenerated file: DO NOT EDIT
// Created on 2025-03-18 2:15:23 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Chord: IClosedShape2D
    {
        // Fields
        [DataMember] public readonly Arc Arc;

        // With functions 
        [MethodImpl(AggressiveInlining)] public Chord WithArc(Arc arc) => new Chord(arc);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public Chord(Arc arc) { Arc = arc; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Chord Create(Arc arc) => new Chord(arc);

        // Implicit converters to/from single field
        [MethodImpl(AggressiveInlining)] public static implicit operator Arc(Chord self) => self.Arc;
        [MethodImpl(AggressiveInlining)] public static implicit operator Chord(Arc value) => new Chord(value);

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Chord other) => Arc.Equals(other.Arc);
        [MethodImpl(AggressiveInlining)] public Boolean NotEquals(Chord other) => !Arc.Equals(other.Arc);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Chord other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Arc);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Arc\" = {Arc} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented interface functions
        public Boolean Closed { [MethodImpl(AggressiveInlining)] get  => ((Boolean)true); } 

        // Unimplemented concept functions
    }
}
