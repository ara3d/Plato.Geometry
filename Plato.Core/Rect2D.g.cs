// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Rect2D: IPolygon2D
    {
        // Fields
        [DataMember] public readonly Vector2 Center;
        [DataMember] public readonly Vector2 Size;

        // With functions 
        [MethodImpl(AggressiveInlining)] public Rect2D WithCenter(Vector2 center) => new Rect2D(center, Size);
        [MethodImpl(AggressiveInlining)] public Rect2D WithSize(Vector2 size) => new Rect2D(Center, size);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public Rect2D(Vector2 center, Vector2 size) { Center = center; Size = size; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Rect2D Create(Vector2 center, Vector2 size) => new Rect2D(center, size);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Vector2, Vector2)(Rect2D self) => (self.Center, self.Size);
        [MethodImpl(AggressiveInlining)] public static implicit operator Rect2D((Vector2, Vector2) value) => new(value.Item1, value.Item2);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Vector2 center, out Vector2 size) { center = Center; size = Size;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Rect2D other) => Center.Equals(other.Center) && Size.Equals(other.Size);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Rect2D other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Center, Size);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Center\" = {Center}, \"Size\" = {Size} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Array predefined functions
        [MethodImpl(AggressiveInlining)] public Rect2D(IReadOnlyList<Vector2> xs) : this(xs[0], xs[1]) { }
        [MethodImpl(AggressiveInlining)] public Rect2D(Vector2[] xs) : this(xs[0], xs[1]) { }
        [MethodImpl(AggressiveInlining)] public static Rect2D Create(IReadOnlyList<Vector2> xs) => new Rect2D(xs);
        // Implementation of IReadOnlyList
        [MethodImpl(AggressiveInlining)] public System.Collections.Generic.IEnumerator<Vector2> GetEnumerator() => new ArrayEnumerator<Vector2>(this);
        [MethodImpl(AggressiveInlining)] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector2 System.Collections.Generic.IReadOnlyList<Vector2>.this[int n] { [MethodImpl(AggressiveInlining)] get => At(n); }
        int System.Collections.Generic.IReadOnlyCollection<Vector2>.Count { [MethodImpl(AggressiveInlining)] get => this.Count; }

        // Implemented concept functions and type functions
        public Number Width { [MethodImpl(AggressiveInlining)] get  => this.Size.X; } 
        public Number Height { [MethodImpl(AggressiveInlining)] get  => this.Size.Y; } 
        public Number Left { [MethodImpl(AggressiveInlining)] get  => this.Center.X.Subtract(this.Width.Half); } 
        public Number Right { [MethodImpl(AggressiveInlining)] get  => this.Left.Add(this.Width); } 
        public Number Bottom { [MethodImpl(AggressiveInlining)] get  => this.Center.Y.Subtract(this.Height.Half); } 
        public Number Top { [MethodImpl(AggressiveInlining)] get  => this.Bottom.Add(this.Height); } 
        public Vector2 BottomLeft { [MethodImpl(AggressiveInlining)] get  => (this.Left, this.Bottom); } 
        public Vector2 BottomRight { [MethodImpl(AggressiveInlining)] get  => (this.Right, this.Bottom); } 
        public Vector2 TopRight { [MethodImpl(AggressiveInlining)] get  => (this.Right, this.Top); } 
        public Vector2 TopLeft { [MethodImpl(AggressiveInlining)] get  => (this.Left, this.Top); } 
        public Quad2D Quad2D { [MethodImpl(AggressiveInlining)] get  => (this.BottomLeft, this.BottomRight, this.TopRight, this.TopLeft); } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator Quad2D(Rect2D x) => x.Quad2D;
        public IArray<Vector2> Points { [MethodImpl(AggressiveInlining)] get  => this.Quad2D; } 
        public IArray<Line2D> Lines { [MethodImpl(AggressiveInlining)] get  => this.Points.WithNext((a, b) => new Any(Line2D, a, b), this.Closed); } 
        // Ambiguous: could not choose a best function implementation for Closed(Rect2D):Boolean:Boolean.
        public Boolean Closed { [MethodImpl(AggressiveInlining)] get  => ((Boolean)false); } 
        [MethodImpl(AggressiveInlining)]  public IArray<Vector2> Sample(Integer numPoints){
            var _var109 = this;
            return numPoints.LinearSpace.Map((x) => _var109.Eval(x));
        }

        [MethodImpl(AggressiveInlining)]  public PolyLine2D ToPolyLine2D(Integer numPoints) => (this.Sample(numPoints), this.Closed);

        // Unimplemented concept functions
        public Integer Count { [MethodImpl(AggressiveInlining)] get => 2; }
        [MethodImpl(AggressiveInlining)]  public Vector2 At(Integer n) { [MethodImpl(AggressiveInlining)] get => n == 0 ? Center : n == 1 ? Size : throw new System.IndexOutOfRangeException(); }
        public Vector2 this[Integer n] => n == 0 ? Center : n == 1 ? Size : throw new System.IndexOutOfRangeException();
        [MethodImpl(AggressiveInlining)]  public Number Distance(Vector2 p) => throw new NotImplementedException();
        [MethodImpl(AggressiveInlining)]  public Vector2 Eval(Number t) => throw new NotImplementedException();
    }
}