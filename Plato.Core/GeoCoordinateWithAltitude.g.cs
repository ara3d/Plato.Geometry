// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct GeoCoordinateWithAltitude: ICoordinate<GeoCoordinateWithAltitude>
    {
        // Fields
        [DataMember] public readonly GeoCoordinate ICoordinate;
        [DataMember] public readonly Number Altitude;

        // With functions 
        [MethodImpl(AggressiveInlining)] public GeoCoordinateWithAltitude WithICoordinate(GeoCoordinate iCoordinate) => new GeoCoordinateWithAltitude(iCoordinate, Altitude);
        [MethodImpl(AggressiveInlining)] public GeoCoordinateWithAltitude WithAltitude(Number altitude) => new GeoCoordinateWithAltitude(ICoordinate, altitude);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public GeoCoordinateWithAltitude(GeoCoordinate iCoordinate, Number altitude) { ICoordinate = iCoordinate; Altitude = altitude; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static GeoCoordinateWithAltitude Create(GeoCoordinate iCoordinate, Number altitude) => new GeoCoordinateWithAltitude(iCoordinate, altitude);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (GeoCoordinate, Number)(GeoCoordinateWithAltitude self) => (self.ICoordinate, self.Altitude);
        [MethodImpl(AggressiveInlining)] public static implicit operator GeoCoordinateWithAltitude((GeoCoordinate, Number) value) => new(value.Item1, value.Item2);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out GeoCoordinate iCoordinate, out Number altitude) { iCoordinate = ICoordinate; altitude = Altitude;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(GeoCoordinateWithAltitude other) => ICoordinate.Equals(other.ICoordinate) && Altitude.Equals(other.Altitude);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is GeoCoordinateWithAltitude other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(ICoordinate, Altitude);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"ICoordinate\" = {ICoordinate}, \"Altitude\" = {Altitude} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented concept functions and type functions
        [MethodImpl(AggressiveInlining)]  public IArray<GeoCoordinateWithAltitude> Repeat(Integer n){
            var _var44 = this;
            return n.MapRange((i) => _var44);
        }


        // Unimplemented concept functions
    }
}