// Autogenerated file: DO NOT EDIT
// Created on 2025-03-06 1:31:00 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Triangle2D: IPolygon2D
    {
        // Fields
        [DataMember] public readonly Vector2 A;
        [DataMember] public readonly Vector2 B;
        [DataMember] public readonly Vector2 C;

        // With functions 
        [MethodImpl(AggressiveInlining)] public Triangle2D WithA(Vector2 a) => new Triangle2D(a, B, C);
        [MethodImpl(AggressiveInlining)] public Triangle2D WithB(Vector2 b) => new Triangle2D(A, b, C);
        [MethodImpl(AggressiveInlining)] public Triangle2D WithC(Vector2 c) => new Triangle2D(A, B, c);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public Triangle2D(Vector2 a, Vector2 b, Vector2 c) { A = a; B = b; C = c; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Triangle2D Create(Vector2 a, Vector2 b, Vector2 c) => new Triangle2D(a, b, c);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Vector2, Vector2, Vector2)(Triangle2D self) => (self.A, self.B, self.C);
        [MethodImpl(AggressiveInlining)] public static implicit operator Triangle2D((Vector2, Vector2, Vector2) value) => new(value.Item1, value.Item2, value.Item3);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Vector2 a, out Vector2 b, out Vector2 c) { a = A; b = B; c = C;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Triangle2D other) => A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        [MethodImpl(AggressiveInlining)] public Boolean NotEquals(Triangle2D other) => !A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Triangle2D other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Array predefined functions
        [MethodImpl(AggressiveInlining)] public Triangle2D(IReadOnlyList<Vector2> xs) : this(xs[0], xs[1], xs[2]) { }
        [MethodImpl(AggressiveInlining)] public Triangle2D(Vector2[] xs) : this(xs[0], xs[1], xs[2]) { }
        [MethodImpl(AggressiveInlining)] public static Triangle2D Create(IReadOnlyList<Vector2> xs) => new Triangle2D(xs);
        // Implementation of IReadOnlyList
        [MethodImpl(AggressiveInlining)] public System.Collections.Generic.IEnumerator<Vector2> GetEnumerator() => new ArrayEnumerator<Vector2>(this);
        [MethodImpl(AggressiveInlining)] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector2 System.Collections.Generic.IReadOnlyList<Vector2>.this[int n] { [MethodImpl(AggressiveInlining)] get => At(n); }
        int System.Collections.Generic.IReadOnlyCollection<Vector2>.Count { [MethodImpl(AggressiveInlining)] get => this.Count; }

        // Implemented concept functions and type functions
        public Number Area { [MethodImpl(AggressiveInlining)] get  => this.A.X.Multiply(this.C.Y.Subtract(this.B.Y)).Add(this.B.X.Multiply(this.A.Y.Subtract(this.C.Y)).Add(this.C.X.Multiply(this.B.Y.Subtract(this.A.Y)))).Half; } 
        public Triangle2D Flip { [MethodImpl(AggressiveInlining)] get  => (this.C, this.B, this.A); } 
        public Vector2 Center { [MethodImpl(AggressiveInlining)] get  => this.A.Add(this.B.Add(this.C)).Divide(((Number)3)); } 
        [MethodImpl(AggressiveInlining)]  public Vector2 Barycentric(Vector2 uv) => this.A.Barycentric(this.B, this.C, uv);
        public Triangle3D To3D { [MethodImpl(AggressiveInlining)] get  => (this.A.To3D, this.B.To3D, this.C.To3D); } 
        public Triangle3D Triangle3D { [MethodImpl(AggressiveInlining)] get  => this.To3D; } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator Triangle3D(Triangle2D x) => x.Triangle3D;
        public static Triangle2D Unit { [MethodImpl(AggressiveInlining)] get  => ((((Number)0.5).Negative, ((Number)3).Sqrt.Half.Negative), (((Number)0.5).Negative, ((Number)3).Sqrt.Half), (((Number)0), ((Number)1))); } 
        public LineArray2D LineArray2D { [MethodImpl(AggressiveInlining)] get  => new LineArray2D(this.Lines); } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator LineArray2D(Triangle2D t) => t.LineArray2D;
        public TriangleArray2D TriangleArray2D { [MethodImpl(AggressiveInlining)] get  => new TriangleArray2D(Intrinsics.MakeArray(this)); } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator TriangleArray2D(Triangle2D t) => t.TriangleArray2D;
        public IArray<Vector2> Points { [MethodImpl(AggressiveInlining)] get  => Intrinsics.MakeArray<Vector2>(this.A, this.B, this.C); } 
        public IArray<Line2D> Lines { [MethodImpl(AggressiveInlining)] get  => Intrinsics.MakeArray<Line2D>(new Line2D(this.A, this.B), new Line2D(this.B, this.C), new Line2D(this.C, this.A)); } 
        [MethodImpl(AggressiveInlining)]  public IArray<Vector2> Sample(Integer numPoints){
            var _var87 = this;
            return numPoints.LinearSpace.Map((x) => _var87.Eval(x));
        }

        [MethodImpl(AggressiveInlining)]  public PolyLine2D ToPolyLine2D(Integer numPoints) => (this.Sample(numPoints), this.Closed);
        public Boolean Closed { [MethodImpl(AggressiveInlining)] get  => ((Boolean)true); } 

        // Unimplemented concept functions
        public Integer Count { [MethodImpl(AggressiveInlining)] get => 3; }
        [MethodImpl(AggressiveInlining)]  public Vector2 At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector2 this[Integer n] { [MethodImpl(AggressiveInlining)] get => At(n); }
        [MethodImpl(AggressiveInlining)]  public Number Distance(Vector2 p) => throw new NotImplementedException();
        [MethodImpl(AggressiveInlining)]  public Vector2 Eval(Number t) => throw new NotImplementedException();
    }
}
