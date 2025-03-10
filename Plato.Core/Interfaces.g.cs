// Autogenerated file: DO NOT EDIT
// Created on 2025-03-06 1:31:00 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    public interface IAny<Self>
    {
        Boolean Equals(Self b);
    }
    public interface IValue
    {
    }
    public interface IAdditive<Self>
    {
        Self Add(Self b);
        Self Subtract(Self b);
        Self Negative { get; }
    }
    public interface IScalarArithmetic<Self>
    {
        Self Modulo(Number other);
        Self Divide(Number other);
        Self Multiply(Number other);
    }
    public interface IInterpolatable<Self>
    {
        Self Lerp(Self b, Number t);
    }
    public interface IVectorSpace<Self>: IInterpolatable<Self>, IAdditive<Self>, IScalarArithmetic<Self>
    {
        IArray<Number> Components { get; }
    }
    public interface IMultiplicative<Self>
    {
        Self Multiply(Self b);
    }
    public interface IDivisible<Self>
    {
        Self Divide(Self b);
    }
    public interface IModulo<Self>
    {
        Self Modulo(Self b);
    }
    public interface IArithmetic<Self>: IAdditive<Self>, IMultiplicative<Self>, IDivisible<Self>, IModulo<Self>
    {
    }
    public interface INumerical<Self>: IVectorSpace<Self>, IArithmetic<Self>
    {
    }
    public interface IOrderable<Self>
    {
        Boolean LessThanOrEquals(Self y);
    }
    public interface IInvertible<Self>
    {
        Self Inverse { get; }
    }
    public interface IMultiplicativeWithInverse<Self>: IMultiplicative<Self>, IInvertible<Self>
    {
    }
    public interface IRealNumber<Self>: INumerical<Self>, IOrderable<Self>
    {
    }
    public interface IWholeNumber<Self>: IOrderable<Self>, IArithmetic<Self>, IInterpolatable<Self>
    {
    }
    public interface IMeasure<Self>: IOrderable<Self>, IVectorSpace<Self>
    {
    }
    public interface IVector<Self>: IArray<Number>, INumerical<Self>, IArithmetic<Self>
    {
    }
    public interface ICoordinate
    {
    }
    public interface IBoolean<Self>
    {
        Self And(Self b);
        Self Or(Self b);
        Self Not { get; }
    }
    public interface IInterval<T>: IValue, IArray<T>
    {
        T Start { get; }
        T End { get; }
    }
    public interface IBounds<T>: IValue
    {
        T Min { get; }
        T Max { get; }
    }
    public interface IRealFunction
    {
        Number Eval(Number x);
    }
    public interface IAngularCurve2D: ICurve2D
    {
        Vector2 Eval(Angle t);
    }
    public interface IPolarCurve: IAngularCurve2D
    {
        Number GetRadius(Angle t);
    }
    public interface IAngularCurve3D: ICurve3D
    {
        Vector3 Eval(Angle t);
    }
    public interface IBounded2D
    {
        Bounds2D Bounds { get; }
    }
    public interface IBounded3D
    {
        Bounds3D Bounds { get; }
    }
    public interface IDeformable2D<Self>
    {
        Self Deform(System.Func<Vector2, Vector2> f);
    }
    public interface IOpenClosedShape
    {
        Boolean Closed { get; }
    }
    public interface IDeformable3D<Self>
    {
        Self Deform(System.Func<Vector3, Vector3> f);
    }
    public interface IGeometry
    {
    }
    public interface IGeometry2D: IGeometry
    {
    }
    public interface IGeometry3D: IGeometry
    {
    }
    public interface IShape2D: IGeometry2D
    {
    }
    public interface IShape3D: IGeometry3D
    {
    }
    public interface IOpenShape: IOpenClosedShape
    {
    }
    public interface IClosedShape: IOpenClosedShape
    {
    }
    public interface IOpenShape2D: IGeometry2D, IOpenShape
    {
    }
    public interface IClosedShape2D: IGeometry2D, IClosedShape
    {
    }
    public interface IOpenShape3D: IGeometry3D, IOpenShape
    {
    }
    public interface IClosedShape3D: IGeometry3D, IClosedShape
    {
    }
    public interface IProcedural<TDomain, TRange>
    {
        TRange Eval(TDomain t);
    }
    public interface ICurve<TRange>: IProcedural<Number, TRange>, IOpenClosedShape
    {
    }
    public interface IDistanceField2D
    {
        Number Distance(Vector2 p);
    }
    public interface IDistanceField3D
    {
        Number Distance(Vector3 p);
    }
    public interface ICurve1D: ICurve<Number>
    {
    }
    public interface ICurve2D: IGeometry2D, ICurve<Vector2>, IDistanceField2D
    {
    }
    public interface IClosedCurve2D: ICurve2D, IClosedShape2D
    {
    }
    public interface IOpenCurve2D: ICurve2D, IOpenShape2D
    {
    }
    public interface ICurve3D: IGeometry3D, ICurve<Vector3>, IDistanceField3D
    {
    }
    public interface IClosedCurve3D: ICurve3D, IClosedShape3D
    {
    }
    public interface IOpenCurve3D: ICurve3D, IOpenShape3D
    {
    }
    public interface ISurface: IGeometry3D, IDistanceField3D
    {
    }
    public interface IProceduralSurface: IProcedural<Vector2, Vector3>, ISurface
    {
        Boolean ClosedX { get; }
        Boolean ClosedY { get; }
    }
    public interface IExplicitSurface: IProcedural<Vector2, Number>, ISurface
    {
    }
    public interface IImplicitProcedural<TDomain>: IProcedural<TDomain, Boolean>
    {
    }
    public interface IImplicitSurface: ISurface, IImplicitProcedural<Vector3>
    {
    }
    public interface IImplicitCurve2D: IGeometry2D, IImplicitProcedural<Vector2>
    {
    }
    public interface IImplicitVolume: IGeometry3D, IImplicitProcedural<Vector3>
    {
    }
    public interface IPolyLine2D: IPointGeometry2D, IOpenClosedShape, ICurve2D
    {
    }
    public interface IPolyLine3D<Self>: IPointGeometry3D<Self>, IOpenClosedShape, ICurve3D
    {
    }
    public interface IClosedPolyLine2D: IPolyLine2D, IClosedShape2D
    {
    }
    public interface IClosedPolyLine3D<Self>: IPolyLine3D<Self>, IClosedShape3D
    {
    }
    public interface IPolygon2D: IClosedPolyLine2D, IArray<Vector2>
    {
    }
    public interface IPolygon3D<Self>: IClosedPolyLine3D<Self>, IArray<Vector3>
    {
    }
    public interface ISolid: IProceduralSurface
    {
    }
    public interface IPrimitiveGeometry
    {
        Integer PrimitiveSize { get; }
        Integer NumPrimitives { get; }
    }
    public interface IPointPrimitives: IPrimitiveGeometry
    {
    }
    public interface ILinePrimitives: IPrimitiveGeometry
    {
    }
    public interface ITrianglePrimitives: IPrimitiveGeometry
    {
    }
    public interface IQuadPrimitives: IPrimitiveGeometry
    {
    }
    public interface IPointGeometry2D: IGeometry2D
    {
        IArray<Vector2> Points { get; }
    }
    public interface IPointGeometry3D<Self>: IGeometry3D, IDeformable3D<Self>
    {
        IArray<Vector3> Points { get; }
    }
    public interface IPrimitiveGeometry2D: IPointGeometry2D, IPrimitiveGeometry
    {
    }
    public interface IPrimitiveGeometry3D<Self>: IPointGeometry3D<Self>, IPrimitiveGeometry
    {
    }
    public interface ILineGeometry2D: IPrimitiveGeometry2D, ILinePrimitives
    {
        IArray<Line2D> Lines { get; }
    }
    public interface ILineGeometry3D<Self>: IPrimitiveGeometry3D<Self>, ILinePrimitives
    {
        IArray<Line3D> Lines { get; }
    }
    public interface ITriangleGeometry2D: IPrimitiveGeometry2D, ITrianglePrimitives
    {
        IArray<Triangle2D> Triangles { get; }
    }
    public interface ITriangleGeometry3D<Self>: IPrimitiveGeometry3D<Self>, ITrianglePrimitives
    {
        IArray<Triangle3D> Triangles { get; }
    }
    public interface IQuadGeometry2D: IPrimitiveGeometry2D, IQuadPrimitives
    {
        IArray<Quad2D> Quads { get; }
    }
    public interface IQuadGeometry3D<Self>: IPrimitiveGeometry3D<Self>, IQuadPrimitives
    {
        IArray<Quad3D> Quads { get; }
    }
    public interface IIndexedGeometry: IPrimitiveGeometry
    {
        IArray<Integer> Indices { get; }
    }
    public interface IIndexedGeometry2D: IIndexedGeometry, IPrimitiveGeometry2D
    {
    }
    public interface IIndexedGeometry3D<Self>: IIndexedGeometry, IPrimitiveGeometry3D<Self>
    {
    }
    public interface ILineMesh2D: IIndexedGeometry2D, ILineGeometry2D
    {
    }
    public interface ILineMesh3D<Self>: IIndexedGeometry3D<Self>, ILineGeometry3D<Self>
    {
    }
    public interface ITriangleMesh2D: IIndexedGeometry2D, ITriangleGeometry2D
    {
    }
    public interface ITriangleMesh3D<Self>: IIndexedGeometry3D<Self>, ITriangleGeometry3D<Self>
    {
    }
    public interface IQuadMesh2D: IIndexedGeometry2D, IQuadGeometry2D
    {
    }
    public interface IQuadMesh3D<Self>: IIndexedGeometry3D<Self>, IQuadGeometry3D<Self>
    {
    }
    public interface IPointArray2D: IPointGeometry2D
    {
    }
    public interface IPointArray3D<Self>: IPointGeometry3D<Self>
    {
    }
    public interface ILineArray2D: ILineMesh2D
    {
    }
    public interface ILineArray3D<Self>: ILineMesh3D<Self>
    {
    }
    public interface ITriangleArray2D: ITriangleMesh2D
    {
    }
    public interface ITriangleArray3D<Self>: ITriangleMesh3D<Self>
    {
    }
    public interface IQuadArray2D: IQuadMesh2D
    {
    }
    public interface IQuadArray3D<Self>: IQuadMesh3D<Self>
    {
    }
    public interface IQuadGrid3D<Self>: IQuadMesh3D<Self>
    {
        IArray2D<Vector3> PointGrid { get; }
        Boolean ClosedX { get; }
        Boolean ClosedY { get; }
    }
    public interface ITransform3D
    {
        Vector3 Transform(Vector3 v);
        Vector3 TransformNormal(Vector3 v);
        Matrix4x4 Matrix { get; }
    }
    public interface IRigidTransform3D: ITransform3D
    {
        Translation3D Translation { get; }
        Rotation3D Rotation { get; }
    }
    public interface IRotationalTransform3D: IRigidTransform3D
    {
        Quaternion Quaternion { get; }
    }
}
