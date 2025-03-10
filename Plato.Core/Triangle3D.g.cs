// Autogenerated file: DO NOT EDIT
// Created on 2025-03-06 1:31:00 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Triangle3D: IPolygon3D<Triangle3D>, IDeformable3D<Triangle3D>
    {
        // Fields
        [DataMember] public readonly Vector3 A;
        [DataMember] public readonly Vector3 B;
        [DataMember] public readonly Vector3 C;

        // With functions 
        [MethodImpl(AggressiveInlining)] public Triangle3D WithA(Vector3 a) => new Triangle3D(a, B, C);
        [MethodImpl(AggressiveInlining)] public Triangle3D WithB(Vector3 b) => new Triangle3D(A, b, C);
        [MethodImpl(AggressiveInlining)] public Triangle3D WithC(Vector3 c) => new Triangle3D(A, B, c);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public Triangle3D(Vector3 a, Vector3 b, Vector3 c) { A = a; B = b; C = c; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Triangle3D Create(Vector3 a, Vector3 b, Vector3 c) => new Triangle3D(a, b, c);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (Vector3, Vector3, Vector3)(Triangle3D self) => (self.A, self.B, self.C);
        [MethodImpl(AggressiveInlining)] public static implicit operator Triangle3D((Vector3, Vector3, Vector3) value) => new(value.Item1, value.Item2, value.Item3);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out Vector3 a, out Vector3 b, out Vector3 c) { a = A; b = B; c = C;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Triangle3D other) => A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        [MethodImpl(AggressiveInlining)] public Boolean NotEquals(Triangle3D other) => !A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Triangle3D other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(A, B, C);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"A\" = {A}, \"B\" = {B}, \"C\" = {C} }}";

        // Explicit implementation of interfaces by forwarding properties to fields

        // Array predefined functions
        [MethodImpl(AggressiveInlining)] public Triangle3D(IReadOnlyList<Vector3> xs) : this(xs[0], xs[1], xs[2]) { }
        [MethodImpl(AggressiveInlining)] public Triangle3D(Vector3[] xs) : this(xs[0], xs[1], xs[2]) { }
        [MethodImpl(AggressiveInlining)] public static Triangle3D Create(IReadOnlyList<Vector3> xs) => new Triangle3D(xs);
        // Implementation of IReadOnlyList
        [MethodImpl(AggressiveInlining)] public System.Collections.Generic.IEnumerator<Vector3> GetEnumerator() => new ArrayEnumerator<Vector3>(this);
        [MethodImpl(AggressiveInlining)] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        Vector3 System.Collections.Generic.IReadOnlyList<Vector3>.this[int n] { [MethodImpl(AggressiveInlining)] get => At(n); }
        int System.Collections.Generic.IReadOnlyCollection<Vector3>.Count { [MethodImpl(AggressiveInlining)] get => this.Count; }

        // Implemented concept functions and type functions
        public Triangle3D Flip { [MethodImpl(AggressiveInlining)] get  => (this.C, this.B, this.A); } 
        public Vector3 Normal { [MethodImpl(AggressiveInlining)] get  => this.B.Subtract(this.A).Cross(this.C.Subtract(this.A)).Normalize; } 
        public Vector3 Center { [MethodImpl(AggressiveInlining)] get  => this.A.Add(this.B.Add(this.C)).Divide(((Number)3)); } 
        public Plane Plane { [MethodImpl(AggressiveInlining)] get  => (this.Normal, this.Normal.Dot(this.A)); } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator Plane(Triangle3D t) => t.Plane;
        public Line3D LineA { [MethodImpl(AggressiveInlining)] get  => (this.A, this.B); } 
        public Line3D LineB { [MethodImpl(AggressiveInlining)] get  => (this.B, this.C); } 
        public Line3D LineC { [MethodImpl(AggressiveInlining)] get  => (this.C, this.A); } 
        [MethodImpl(AggressiveInlining)]  public Vector3 Barycentric(Vector2 uv) => this.A.Barycentric(this.B, this.C, uv);
        public LineArray3D LineArray3D { [MethodImpl(AggressiveInlining)] get  => new LineArray3D(this.Lines); } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator LineArray3D(Triangle3D t) => t.LineArray3D;
        public TriangleArray3D TriangleArray3D { [MethodImpl(AggressiveInlining)] get  => new TriangleArray3D(Intrinsics.MakeArray(this)); } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator TriangleArray3D(Triangle3D t) => t.TriangleArray3D;
        public TriangleMesh3D TriangleMesh3D { [MethodImpl(AggressiveInlining)] get  => this.TriangleArray3D; } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator TriangleMesh3D(Triangle3D g) => g.TriangleMesh3D;
        public IArray<Vector3> Points { [MethodImpl(AggressiveInlining)] get  => Intrinsics.MakeArray<Vector3>(this.A, this.B, this.C); } 
        public IArray<Line3D> Lines { [MethodImpl(AggressiveInlining)] get  => Intrinsics.MakeArray<Line3D>(new Line3D(this.A, this.B), new Line3D(this.B, this.C), new Line3D(this.C, this.A)); } 
        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(System.Func<Vector3, Vector3> f) => (f.Invoke(this.A), f.Invoke(this.B), f.Invoke(this.C));
        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(IdentityTransform3D t){
            var _var169 = t;
            return this.Deform((v) => _var169.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(MatrixTransform3D t){
            var _var170 = t;
            return this.Deform((v) => _var170.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(Translation3D t){
            var _var171 = t;
            return this.Deform((v) => _var171.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(Rotation3D t){
            var _var172 = t;
            return this.Deform((v) => _var172.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(LookRotation t){
            var _var173 = t;
            return this.Deform((v) => _var173.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(Scaling3D t){
            var _var174 = t;
            return this.Deform((v) => _var174.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(TRSTransform3D t){
            var _var175 = t;
            return this.Deform((v) => _var175.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(Pose3D t){
            var _var176 = t;
            return this.Deform((v) => _var176.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(AxisAngle t){
            var _var177 = t;
            return this.Deform((v) => _var177.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(EulerAngles t){
            var _var178 = t;
            return this.Deform((v) => _var178.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(Perspective3D t){
            var _var179 = t;
            return this.Deform((v) => _var179.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(Orthographic3D t){
            var _var180 = t;
            return this.Deform((v) => _var180.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Deform(PlaneProjection t){
            var _var181 = t;
            return this.Deform((v) => _var181.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Translate(Vector3 v){
            var _var182 = v;
            return this.Deform((p) => p.Add(_var182));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Rotate(Quaternion q) => this.Deform(q);
        [MethodImpl(AggressiveInlining)]  public Triangle3D Scale(Vector3 v){
            var _var183 = v;
            return this.Deform((p) => p.Multiply(_var183));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D Scale(Number s){
            var _var184 = s;
            return this.Deform((p) => p.Multiply(_var184));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D RotateX(Angle a) => this.Rotate(a.RotateX);
        [MethodImpl(AggressiveInlining)]  public Triangle3D RotateY(Angle a) => this.Rotate(a.RotateY);
        [MethodImpl(AggressiveInlining)]  public Triangle3D RotateZ(Angle a) => this.Rotate(a.RotateZ);
        [MethodImpl(AggressiveInlining)]  public Triangle3D TranslateX(Number s){
            var _var185 = s;
            return this.Deform((p) => p.Add((_var185, ((Integer)0), ((Integer)0))));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D TranslateY(Number s){
            var _var186 = s;
            return this.Deform((p) => p.Add((((Integer)0), _var186, ((Integer)0))));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D TranslateZ(Number s){
            var _var187 = s;
            return this.Deform((p) => p.Add((((Integer)0), ((Integer)0), _var187)));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D ScaleX(Number s){
            var _var188 = s;
            return this.Deform((p) => p.Multiply((_var188, ((Integer)1), ((Integer)1))));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D ScaleY(Number s){
            var _var189 = s;
            return this.Deform((p) => p.Multiply((((Integer)1), _var189, ((Integer)1))));
        }

        [MethodImpl(AggressiveInlining)]  public Triangle3D ScaleZ(Number s){
            var _var190 = s;
            return this.Deform((p) => p.Multiply((((Integer)1), ((Integer)1), _var190)));
        }

        [MethodImpl(AggressiveInlining)]  public IArray<Vector3> Sample(Integer numPoints){
            var _var191 = this;
            return numPoints.LinearSpace.Map((x) => _var191.Eval(x));
        }

        [MethodImpl(AggressiveInlining)]  public PolyLine3D ToPolyLine3D(Integer numPoints) => (this.Sample(numPoints), this.Closed);
        public Boolean Closed { [MethodImpl(AggressiveInlining)] get  => ((Boolean)true); } 

        // Unimplemented concept functions
        public Integer Count { [MethodImpl(AggressiveInlining)] get => 3; }
        [MethodImpl(AggressiveInlining)]  public Vector3 At(Integer n) => n == 0 ? A : n == 1 ? B : n == 2 ? C : throw new System.IndexOutOfRangeException();
        public Vector3 this[Integer n] { [MethodImpl(AggressiveInlining)] get => At(n); }
        [MethodImpl(AggressiveInlining)]  public Number Distance(Vector3 p) => throw new NotImplementedException();
        [MethodImpl(AggressiveInlining)]  public Vector3 Eval(Number t) => throw new NotImplementedException();
    }
}
