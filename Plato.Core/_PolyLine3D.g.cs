// Autogenerated file: DO NOT EDIT
// Created on 2025-04-03 1:53:19 AM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct PolyLine3D: IPolyLine3D
    {
        // Fields
        [DataMember] public readonly IArray<Vector3> Points;
        [DataMember] public readonly Boolean Closed;

        // With functions 
        [MethodImpl(AggressiveInlining)] public PolyLine3D WithPoints(IArray<Vector3> points) => new PolyLine3D(points, Closed);
        [MethodImpl(AggressiveInlining)] public PolyLine3D WithClosed(Boolean closed) => new PolyLine3D(Points, closed);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public PolyLine3D(IArray<Vector3> points, Boolean closed) { Points = points; Closed = closed; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static PolyLine3D Create(IArray<Vector3> points, Boolean closed) => new PolyLine3D(points, closed);

        // Static default implementation
        public static readonly PolyLine3D Default = default;

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (IArray<Vector3>, Boolean)(PolyLine3D self) => (self.Points, self.Closed);
        [MethodImpl(AggressiveInlining)] public static implicit operator PolyLine3D((IArray<Vector3>, Boolean) value) => new(value.Item1, value.Item2);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out IArray<Vector3> points, out Boolean closed) { points = Points; closed = Closed;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(PolyLine3D other) => Points.Equals(other.Points) && Closed.Equals(other.Closed);
        [MethodImpl(AggressiveInlining)] public Boolean NotEquals(PolyLine3D other) => !Points.Equals(other.Points) && Closed.Equals(other.Closed);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is PolyLine3D other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Points, Closed);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Points\" = {Points}, \"Closed\" = {Closed} }}";

        // Explicit implementation of interfaces by forwarding properties to fields
        Boolean IOpenClosedShape.Closed { [MethodImpl(AggressiveInlining)] get => Closed; }
        IArray<Vector3> IPointGeometry3D.Points { [MethodImpl(AggressiveInlining)] get => Points; }

        // Implemented interface functions
        [MethodImpl(AggressiveInlining)]  public PolyLine3D Deform(System.Func<Vector3, Vector3> f) => (this.Points.Map(f), this.Closed);
public IArray<Line3D> Lines { [MethodImpl(AggressiveInlining)] get  => this.Points.WithNext((a, b)  => new Line3D(a, b), this.Closed); } 
[MethodImpl(AggressiveInlining)]  public IArray<Vector3> Sample(Integer numPoints){
            var _var162 = this;
            return numPoints.LinearSpace.Map((x)  => _var162.Eval(x));
        }

[MethodImpl(AggressiveInlining)]  public PolyLine3D ToPolyLine3D(Integer numPoints) => (this.Sample(numPoints), this.Closed);

        // Unimplemented interface functions
        public Integer PrimitiveSize => throw new NotImplementedException();
public Integer NumPrimitives => throw new NotImplementedException();
public IArray<Vector3> Corners => throw new NotImplementedException();
[MethodImpl(AggressiveInlining)]  public Vector3 Eval(Number t) => throw new NotImplementedException();
}
}
