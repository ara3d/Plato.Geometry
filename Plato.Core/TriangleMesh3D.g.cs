// Autogenerated file: DO NOT EDIT
// Created on 2025-03-06 1:31:00 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [DataContract, StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct TriangleMesh3D: ITriangleMesh3D<TriangleMesh3D>
    {
        // Fields
        [DataMember] public readonly IArray<Vector3> Points;
        [DataMember] public readonly IArray<Integer> Indices;

        // With functions 
        [MethodImpl(AggressiveInlining)] public TriangleMesh3D WithPoints(IArray<Vector3> points) => new TriangleMesh3D(points, Indices);
        [MethodImpl(AggressiveInlining)] public TriangleMesh3D WithIndices(IArray<Integer> indices) => new TriangleMesh3D(Points, indices);

        // Regular Constructor
        [MethodImpl(AggressiveInlining)] public TriangleMesh3D(IArray<Vector3> points, IArray<Integer> indices) { Points = points; Indices = indices; }

        // Static factory function
        [MethodImpl(AggressiveInlining)] public static TriangleMesh3D Create(IArray<Vector3> points, IArray<Integer> indices) => new TriangleMesh3D(points, indices);

        // Implicit converters to/from value-tuples and deconstructor
        [MethodImpl(AggressiveInlining)] public static implicit operator (IArray<Vector3>, IArray<Integer>)(TriangleMesh3D self) => (self.Points, self.Indices);
        [MethodImpl(AggressiveInlining)] public static implicit operator TriangleMesh3D((IArray<Vector3>, IArray<Integer>) value) => new(value.Item1, value.Item2);
        [MethodImpl(AggressiveInlining)] public void Deconstruct(out IArray<Vector3> points, out IArray<Integer> indices) { points = Points; indices = Indices;  }

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(TriangleMesh3D other) => Points.Equals(other.Points) && Indices.Equals(other.Indices);
        [MethodImpl(AggressiveInlining)] public Boolean NotEquals(TriangleMesh3D other) => !Points.Equals(other.Points) && Indices.Equals(other.Indices);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is TriangleMesh3D other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override int GetHashCode() => Intrinsics.CombineHashCodes(Points, Indices);
        [MethodImpl(AggressiveInlining)] public override string ToString() => $"{{ \"Points\" = {Points}, \"Indices\" = {Indices} }}";

        // Explicit implementation of interfaces by forwarding properties to fields
        IArray<Integer> IIndexedGeometry.Indices { [MethodImpl(AggressiveInlining)] get => Indices; }
        IArray<Vector3> IPointGeometry3D<TriangleMesh3D>.Points { [MethodImpl(AggressiveInlining)] get => Points; }

        // Implemented concept functions and type functions
        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(System.Func<Vector3, Vector3> f) => (this.Points.Map(f), this.Indices);
        public LineMesh3D LineMesh3D { [MethodImpl(AggressiveInlining)] get  => (this.Points, this.FaceIndices.FlatMap((a) => Intrinsics.MakeArray(a.At(((Integer)0)), a.At(((Integer)1)), a.At(((Integer)1)), a.At(((Integer)2)), a.At(((Integer)2)), a.At(((Integer)0))))); } 
        [MethodImpl(AggressiveInlining)]  public static implicit operator LineMesh3D(TriangleMesh3D g) => g.LineMesh3D;
        public IArray<Triangle3D> Faces { [MethodImpl(AggressiveInlining)] get  => this.Triangles; } 
        public IArray<Triangle3D> Triangles { [MethodImpl(AggressiveInlining)] get  => this.AllFaceVertices.Map((xs) => new Triangle3D(xs.At(((Integer)0)), xs.At(((Integer)1)), xs.At(((Integer)2)))); } 
        [MethodImpl(AggressiveInlining)]  public Vector3 Vertex(Integer n) => this.Points.At(this.Indices.At(n));
        [MethodImpl(AggressiveInlining)]  public IArray<Vector3> FaceVertices(Integer f){
            var _var240 = this;
            return this.FaceIndices.At(f).Map((i) => _var240.Vertex(i));
        }

        public IArray2D<Vector3> AllFaceVertices { [MethodImpl(AggressiveInlining)] get {
            var _var241 = this;
            return this.AllFaceIndices.Map((x) => _var241.Vertex(x));
        }
         } 
        [MethodImpl(AggressiveInlining)]  public IArray<Vector3> Vertices(IArray<Integer> xs){
            var _var242 = this;
            return xs.Map((i) => _var242.Vertex(i));
        }

        public IArray<Vector3> AllVertices { [MethodImpl(AggressiveInlining)] get  => this.Vertices(this.Indices); } 
        public Integer NumPrimitives { [MethodImpl(AggressiveInlining)] get  => this.Indices.Count.Divide(this.PrimitiveSize); } 
        public Integer NumFaces { [MethodImpl(AggressiveInlining)] get  => this.NumPrimitives; } 
        public IArray2D<Integer> AllFaceIndices { [MethodImpl(AggressiveInlining)] get  => this.Indices.Slices(this.PrimitiveSize); } 
        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(IdentityTransform3D t){
            var _var243 = t;
            return this.Deform((v) => _var243.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(MatrixTransform3D t){
            var _var244 = t;
            return this.Deform((v) => _var244.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(Translation3D t){
            var _var245 = t;
            return this.Deform((v) => _var245.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(Rotation3D t){
            var _var246 = t;
            return this.Deform((v) => _var246.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(LookRotation t){
            var _var247 = t;
            return this.Deform((v) => _var247.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(Scaling3D t){
            var _var248 = t;
            return this.Deform((v) => _var248.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(TRSTransform3D t){
            var _var249 = t;
            return this.Deform((v) => _var249.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(Pose3D t){
            var _var250 = t;
            return this.Deform((v) => _var250.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(AxisAngle t){
            var _var251 = t;
            return this.Deform((v) => _var251.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(EulerAngles t){
            var _var252 = t;
            return this.Deform((v) => _var252.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(Perspective3D t){
            var _var253 = t;
            return this.Deform((v) => _var253.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(Orthographic3D t){
            var _var254 = t;
            return this.Deform((v) => _var254.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Deform(PlaneProjection t){
            var _var255 = t;
            return this.Deform((v) => _var255.Transform(v));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Translate(Vector3 v){
            var _var256 = v;
            return this.Deform((p) => p.Add(_var256));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Rotate(Quaternion q) => this.Deform(q);
        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Scale(Vector3 v){
            var _var257 = v;
            return this.Deform((p) => p.Multiply(_var257));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D Scale(Number s){
            var _var258 = s;
            return this.Deform((p) => p.Multiply(_var258));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D RotateX(Angle a) => this.Rotate(a.RotateX);
        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D RotateY(Angle a) => this.Rotate(a.RotateY);
        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D RotateZ(Angle a) => this.Rotate(a.RotateZ);
        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D TranslateX(Number s){
            var _var259 = s;
            return this.Deform((p) => p.Add((_var259, ((Integer)0), ((Integer)0))));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D TranslateY(Number s){
            var _var260 = s;
            return this.Deform((p) => p.Add((((Integer)0), _var260, ((Integer)0))));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D TranslateZ(Number s){
            var _var261 = s;
            return this.Deform((p) => p.Add((((Integer)0), ((Integer)0), _var261)));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D ScaleX(Number s){
            var _var262 = s;
            return this.Deform((p) => p.Multiply((_var262, ((Integer)1), ((Integer)1))));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D ScaleY(Number s){
            var _var263 = s;
            return this.Deform((p) => p.Multiply((((Integer)1), _var263, ((Integer)1))));
        }

        [MethodImpl(AggressiveInlining)]  public TriangleMesh3D ScaleZ(Number s){
            var _var264 = s;
            return this.Deform((p) => p.Multiply((((Integer)1), ((Integer)1), _var264)));
        }

        public Integer PrimitiveSize { [MethodImpl(AggressiveInlining)] get  => ((Integer)3); } 

        // Unimplemented concept functions
    }
}
