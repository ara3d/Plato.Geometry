<Details>
<Summary>
# Types
</Summary>

Types in Plato are readonly structs.
## Type Angle

Fields: Radians:ConcreteType:Number.

Implements: IAdditive, IAny, IEquatable, IInterpolatable, IMeasure, INumberLike, INumerical, IOrderable, IScalarArithmetic, IValue.

## Type AnglePair

Fields: Max:ConcreteType:Angle, Min:ConcreteType:Angle.

Implements: IAny, IArray, IEquatable, IInterval, IValue.

## Type Arc

Fields: Angles:ConcreteType:AnglePair, Circle:ConcreteType:Circle.

Implements: IGeometry, IGeometry2D, IOpenClosedShape, IOpenShape2D.

## Type Array

Fields: .

Implements: IArray.

## Type Array2D

Fields: .

Implements: IArray, IArray2D.

## Type Array3D

Fields: .

Implements: IArray, IArray3D.

## Type AxisAngle

Fields: Angle:ConcreteType:Angle, Axis:ConcreteType:Vector3D.

Implements: IAny, IEquatable, IValue.

## Type Boolean

Fields: .

Implements: IAny, IBoolean, IEquatable, IOrderable, IValue.

## Type Bounds2D

Fields: Max:ConcreteType:Vector2D, Min:ConcreteType:Vector2D.

Implements: IAny, IArray, IEquatable, IInterval, IValue.

## Type Bounds3D

Fields: Max:ConcreteType:Vector3D, Min:ConcreteType:Vector3D.

Implements: IAny, IArray, IEquatable, IInterval, IValue.

## Type Box2D

Fields: Center:ConcreteType:Vector2D, Extent:ConcreteType:Size2D, Rotation:ConcreteType:Angle.

Implements: IGeometry, IGeometry2D, IShape2D.

## Type Box3D

Fields: Center:ConcreteType:Vector3D, Extent:ConcreteType:Size3D, Rotation:ConcreteType:Rotation3D.

Implements: IGeometry, IGeometry3D, IShape3D.

## Type Capsule

Fields: Line:ConcreteType:Line3D, Radius:ConcreteType:Number.

Implements: IGeometry, IGeometry3D, IShape3D.

## Type Character

Fields: .

Implements: IAny, IEquatable, IOrderable, IValue.

## Type Chord

Fields: Arc:ConcreteType:Arc.

Implements: IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Circle

Fields: Center:ConcreteType:Vector2D, Radius:ConcreteType:Number.

Implements: IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Color

Fields: A:ConcreteType:Unit, B:ConcreteType:Unit, G:ConcreteType:Unit, R:ConcreteType:Unit.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type ColorHSL

Fields: Hue:ConcreteType:Angle, Luminance:ConcreteType:Unit, Saturation:ConcreteType:Unit.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type ColorHSV

Fields: Hue:ConcreteType:Angle, S:ConcreteType:Unit, V:ConcreteType:Unit.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type ColorLAB

Fields: A:ConcreteType:Number, B:ConcreteType:Number, Lightness:ConcreteType:Unit.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type ColorLCh

Fields: ChromaHue:ConcreteType:PolarCoordinate, Lightness:ConcreteType:Unit.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type ColorLUV

Fields: Lightness:ConcreteType:Unit, U:ConcreteType:Unit, V:ConcreteType:Unit.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type ColorYCbCr

Fields: Cb:ConcreteType:Unit, Cr:ConcreteType:Unit, Y:ConcreteType:Unit.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type Complex

Fields: Imaginary:ConcreteType:Number, IReal:ConcreteType:Number.

Implements: IAdditive, IAny, IArithmetic, IArray, IDivisible, IEquatable, IInterpolatable, IModulo, IMultiplicative, INumerical, IScalarArithmetic, IValue, IVector.

## Type Cone

Fields: Line:ConcreteType:Line3D, Radius:ConcreteType:Number.

Implements: IGeometry, IGeometry3D, IShape3D.

## Type ConeSegment

Fields: Line:ConcreteType:Line3D, Radius1:ConcreteType:Number, Radius2:ConcreteType:Number.

Implements: IGeometry, IGeometry3D, IShape3D.

## Type CubicBezier2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D, D:ConcreteType:Vector2D.

Implements: IArray.

## Type CubicBezier3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D, D:ConcreteType:Vector3D.

Implements: IArray.

## Type Cylinder

Fields: Line:ConcreteType:Line3D, Radius:ConcreteType:Number.

Implements: IGeometry, IGeometry3D, IShape3D.

## Type CylindricalCoordinate

Fields: Azimuth:ConcreteType:Angle, Height:ConcreteType:Number, RadialDistance:ConcreteType:Number.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type DateTime

Fields: Value:ConcreteType:Number.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type Dynamic

Fields: .

Implements: .

## Type Ellipse

Fields: Center:ConcreteType:Vector2D, Size:ConcreteType:Size2D.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

## Type Error

Fields: .

Implements: .

## Type EulerAngles

Fields: Pitch:ConcreteType:Angle, Roll:ConcreteType:Angle, Yaw:ConcreteType:Angle.

Implements: IAny, IEquatable, IValue.

## Type Fraction

Fields: Denominator:ConcreteType:Number, Numerator:ConcreteType:Number.

Implements: IAny, IEquatable, IValue.

## Type Frame3D

Fields: Forward:ConcreteType:Vector3D, Right:ConcreteType:Vector3D, Up:ConcreteType:Vector3D.

Implements: .

## Type Function0

Fields: .

Implements: .

## Type Function1

Fields: .

Implements: .

## Type Function10

Fields: .

Implements: .

## Type Function2

Fields: .

Implements: .

## Type Function3

Fields: .

Implements: .

## Type Function4

Fields: .

Implements: .

## Type Function5

Fields: .

Implements: .

## Type Function6

Fields: .

Implements: .

## Type Function7

Fields: .

Implements: .

## Type Function8

Fields: .

Implements: .

## Type Function9

Fields: .

Implements: .

## Type GeoCoordinate

Fields: Latitude:ConcreteType:Angle, Longitude:ConcreteType:Angle.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type GeoCoordinateWithAltitude

Fields: Altitude:ConcreteType:Number, ICoordinate:ConcreteType:GeoCoordinate.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type HorizontalCoordinate

Fields: Azimuth:ConcreteType:Angle, Height:ConcreteType:Number, Radius:ConcreteType:Number.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type Integer

Fields: .

Implements: IAdditive, IAny, IArithmetic, IDivisible, IEquatable, IModulo, IMultiplicative, IOrderable, IValue, IWholeNumber.

## Type Integer2

Fields: A:ConcreteType:Integer, B:ConcreteType:Integer.

Implements: IAny, IArray, IEquatable, IValue.

## Type Integer3

Fields: A:ConcreteType:Integer, B:ConcreteType:Integer, C:ConcreteType:Integer.

Implements: IAny, IArray, IEquatable, IValue.

## Type Integer4

Fields: A:ConcreteType:Integer, B:ConcreteType:Integer, C:ConcreteType:Integer, D:ConcreteType:Integer.

Implements: IAny, IArray, IEquatable, IValue.

## Type Length

Fields: Meters:ConcreteType:Number.

Implements: IAdditive, IAny, IEquatable, IInterpolatable, IMeasure, INumberLike, INumerical, IOrderable, IScalarArithmetic, IValue.

## Type Lens

Fields: A:ConcreteType:Circle, B:ConcreteType:Circle.

Implements: IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Line2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D.

Implements: IArray, IGeometry, IGeometry2D, IOpenClosedShape, IPoints2D, IPolyLine2D.

## Type Line3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D.

Implements: IArray, IGeometry, IGeometry3D, IOpenClosedShape, IPoints3D, IPolyLine3D.

## Type Line4D

Fields: A:ConcreteType:Vector4D, B:ConcreteType:Vector4D.

Implements: IAny, IArray, IEquatable, IValue.

## Type LineMesh

Fields: Indices:Concept:IArray<ConcreteType:Integer>, Points:Concept:IArray<ConcreteType:Vector3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry3D, ILineMesh, IPoints3D.

## Type Lines

Fields: Primitives:Concept:IArray<ConcreteType:Line3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry3D, ILines, IPoints3D, IPrimitives3D.

## Type LogPolarCoordinate

Fields: Azimuth:ConcreteType:Angle, Rho:ConcreteType:Number.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type Mass

Fields: Kilograms:ConcreteType:Number.

Implements: IAdditive, IAny, IEquatable, IInterpolatable, IMeasure, INumberLike, INumerical, IOrderable, IScalarArithmetic, IValue.

## Type Matrix3x3

Fields: Column1:ConcreteType:Vector3D, Column2:ConcreteType:Vector3D, Column3:ConcreteType:Vector3D.

Implements: IAny, IArray, IEquatable, IValue.

## Type Matrix4x4

Fields: Column1:ConcreteType:Vector4D, Column2:ConcreteType:Vector4D, Column3:ConcreteType:Vector4D, Column4:ConcreteType:Vector4D.

Implements: IAny, IArray, IEquatable, IValue.

## Type Number

Fields: .

Implements: IAdditive, IAlgebraic, IAny, IArithmetic, IDivisible, IEquatable, IInterpolatable, IInvertible, IModulo, IMultiplicative, IMultiplicativeWithInverse, INumberLike, INumerical, IOrderable, IReal, IScalarArithmetic, IValue.

## Type NumberInterval

Fields: Max:ConcreteType:Number, Min:ConcreteType:Number.

Implements: IAny, IArray, IEquatable, IInterval, IValue.

## Type Orientation3D

Fields: IValue:ConcreteType:Rotation3D.

Implements: IAny, IEquatable, IValue.

## Type Plane

Fields: D:ConcreteType:Number, Normal:ConcreteType:Vector3D.

Implements: IAny, IEquatable, IValue.

## Type PolarCoordinate

Fields: Angle:ConcreteType:Angle, Radius:ConcreteType:Number.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type Pose2D

Fields: Orientation:ConcreteType:Angle, Position:ConcreteType:Vector2D.

Implements: IAny, IEquatable, IValue.

## Type Pose3D

Fields: Orientation:ConcreteType:Orientation3D, Position:ConcreteType:Vector3D.

Implements: IAny, IEquatable, IValue.

## Type Probability

Fields: Value:ConcreteType:Number.

Implements: IAdditive, IAny, IEquatable, IInterpolatable, IMeasure, INumberLike, INumerical, IOrderable, IScalarArithmetic, IValue.

## Type Quad2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D, D:ConcreteType:Vector2D.

Implements: IAny, IArray, IEquatable, IValue.

## Type Quad3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D, D:ConcreteType:Vector3D.

Implements: IAny, IArray, IEquatable, IValue.

## Type QuadMesh

Fields: Indices:Concept:IArray<ConcreteType:Integer>, Points:Concept:IArray<ConcreteType:Vector3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D, IQuadMesh.

## Type QuadraticBezier2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D.

Implements: IArray.

## Type QuadraticBezier3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D.

Implements: IArray.

## Type Quads

Fields: Primitives:Concept:IArray<ConcreteType:Quad3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D, IPrimitives3D, IQuads.

## Type Quaternion

Fields: W:ConcreteType:Number, X:ConcreteType:Number, Y:ConcreteType:Number, Z:ConcreteType:Number.

Implements: IAny, IEquatable, IValue.

## Type Rational

Fields: Denominator:ConcreteType:Integer, Numerator:ConcreteType:Integer.

Implements: IAny, IEquatable, IValue.

## Type Ray2D

Fields: Direction:ConcreteType:Vector2D, Origin:ConcreteType:Vector2D.

Implements: IAny, IEquatable, IValue.

## Type Ray3D

Fields: Direction:ConcreteType:Vector3D, Position:ConcreteType:Vector3D.

Implements: IAny, IEquatable, IValue.

## Type Rect2D

Fields: Center:ConcreteType:Vector2D, Size:ConcreteType:Size2D.

Implements: IClosedPolyLine2D, IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape, IPoints2D, IPolygon2D, IPolyLine2D.

## Type RegularPolygon

Fields: NumPoints:ConcreteType:Integer.

Implements: IClosedPolyLine2D, IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape, IPoints2D, IPolygon2D, IPolyLine2D.

## Type Ring

Fields: Center:ConcreteType:Vector2D, InnerRadius:ConcreteType:Number, OuterRadius:ConcreteType:Number.

Implements: IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Rotation3D

Fields: Quaternion:ConcreteType:Quaternion.

Implements: IAny, IEquatable, IValue.

## Type Sector

Fields: Arc:ConcreteType:Arc.

Implements: IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Segment

Fields: Arc:ConcreteType:Arc.

Implements: IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Size2D

Fields: Height:ConcreteType:Number, Width:ConcreteType:Number.

Implements: IAny, IArray, IEquatable, IValue.

## Type Size3D

Fields: Depth:ConcreteType:Number, Height:ConcreteType:Number, Width:ConcreteType:Number.

Implements: IAny, IArray, IEquatable, IValue.

## Type Sphere

Fields: Center:ConcreteType:Vector3D, Radius:ConcreteType:Number.

Implements: IAny, IEquatable, IValue.

## Type SphericalCoordinate

Fields: Azimuth:ConcreteType:Angle, Polar:ConcreteType:Angle, Radius:ConcreteType:Number.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type String

Fields: .

Implements: IAny, IArray, IEquatable, IOrderable, IValue.

## Type Temperature

Fields: Celsius:ConcreteType:Number.

Implements: IAdditive, IAny, IEquatable, IInterpolatable, IMeasure, INumberLike, INumerical, IOrderable, IScalarArithmetic, IValue.

## Type Time

Fields: Seconds:ConcreteType:Number.

Implements: IAdditive, IAny, IEquatable, IInterpolatable, IMeasure, INumberLike, INumerical, IOrderable, IScalarArithmetic, IValue.

## Type Transform2D

Fields: Rotation:ConcreteType:Angle, Scale:ConcreteType:Vector2D, Translation:ConcreteType:Vector2D.

Implements: IAny, IEquatable, IValue.

## Type Transform3D

Fields: Rotation:ConcreteType:Rotation3D, Scale:ConcreteType:Vector3D, Translation:ConcreteType:Vector3D.

Implements: IAny, IEquatable, IValue.

## Type Triangle2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D.

Implements: IAny, IArray, IEquatable, IValue.

## Type Triangle3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D.

Implements: IAny, IArray, IEquatable, IValue.

## Type TriangleMesh

Fields: Indices:Concept:IArray<ConcreteType:Integer>, Points:Concept:IArray<ConcreteType:Vector3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D, ITriangleMesh.

## Type Triangles

Fields: Primitives:Concept:IArray<ConcreteType:Triangle3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D, IPrimitives3D, ITriangles.

## Type Tube

Fields: InnerRadius:ConcreteType:Number, Line:ConcreteType:Line3D, OuterRadius:ConcreteType:Number.

Implements: IGeometry, IGeometry3D, IShape3D.

## Type Tuple10

Fields: X0:TypeVariable:T0, X1:TypeVariable:T1, X2:TypeVariable:T2, X3:TypeVariable:T3, X4:TypeVariable:T4, X5:TypeVariable:T5, X6:TypeVariable:T6, X7:TypeVariable:T7, X8:TypeVariable:T8, X9:TypeVariable:T9.

Implements: .

## Type Tuple2

Fields: X0:TypeVariable:T0, X1:TypeVariable:T1.

Implements: .

## Type Tuple3

Fields: X0:TypeVariable:T0, X1:TypeVariable:T1, X2:TypeVariable:T2.

Implements: .

## Type Tuple4

Fields: X0:TypeVariable:T0, X1:TypeVariable:T1, X2:TypeVariable:T2, X3:TypeVariable:T3.

Implements: .

## Type Tuple5

Fields: X0:TypeVariable:T0, X1:TypeVariable:T1, X2:TypeVariable:T2, X3:TypeVariable:T3, X4:TypeVariable:T4.

Implements: .

## Type Tuple6

Fields: X0:TypeVariable:T0, X1:TypeVariable:T1, X2:TypeVariable:T2, X3:TypeVariable:T3, X4:TypeVariable:T4, X5:TypeVariable:T5.

Implements: .

## Type Tuple7

Fields: X0:TypeVariable:T0, X1:TypeVariable:T1, X2:TypeVariable:T2, X3:TypeVariable:T3, X4:TypeVariable:T4, X5:TypeVariable:T5, X6:TypeVariable:T6.

Implements: .

## Type Tuple8

Fields: X0:TypeVariable:T0, X1:TypeVariable:T1, X2:TypeVariable:T2, X3:TypeVariable:T3, X4:TypeVariable:T4, X5:TypeVariable:T5, X6:TypeVariable:T6, X7:TypeVariable:T7.

Implements: .

## Type Tuple9

Fields: X0:TypeVariable:T0, X1:TypeVariable:T1, X2:TypeVariable:T2, X3:TypeVariable:T3, X4:TypeVariable:T4, X5:TypeVariable:T5, X6:TypeVariable:T6, X7:TypeVariable:T7, X8:TypeVariable:T8.

Implements: .

## Type Type

Fields: .

Implements: .

## Type Unit

Fields: Value:ConcreteType:Number.

Implements: IAdditive, IAlgebraic, IAny, IArithmetic, IDivisible, IEquatable, IInterpolatable, IInvertible, IModulo, IMultiplicative, IMultiplicativeWithInverse, INumberLike, INumerical, IOrderable, IReal, IScalarArithmetic, IValue.

## Type Vector2D

Fields: X:ConcreteType:Number, Y:ConcreteType:Number.

Implements: IAdditive, IAny, IArithmetic, IArray, IDivisible, IEquatable, IInterpolatable, IModulo, IMultiplicative, INumerical, IScalarArithmetic, IValue, IVector.

## Type Vector3D

Fields: X:ConcreteType:Number, Y:ConcreteType:Number, Z:ConcreteType:Number.

Implements: IAdditive, IAny, IArithmetic, IArray, IDivisible, IEquatable, IInterpolatable, IModulo, IMultiplicative, INumerical, IScalarArithmetic, IValue, IVector.

## Type Vector4D

Fields: W:ConcreteType:Number, X:ConcreteType:Number, Y:ConcreteType:Number, Z:ConcreteType:Number.

Implements: IAdditive, IAny, IArithmetic, IArray, IDivisible, IEquatable, IInterpolatable, IModulo, IMultiplicative, INumerical, IScalarArithmetic, IValue, IVector.

<Details>
<Summary>
# Concepts
</Summary>

Concepts in Plato are interfaces. Functions defined on a concept are available on every type that implements the concept.

## IAdditive

Inherits: IAny.

Implemented by: Number, Integer, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Inherited by: INumerical, INumberLike, IReal, IWholeNumber, IMeasure, IVector, IAlgebraic, IInterpolatable, IArithmetic.

Functions: Add, Negative, Subtract.























## IAlgebraic

Inherits: IAdditive, IAny, IAny, IAny, IAny, IInterpolatable, IInvertible, IMultiplicative, IMultiplicativeWithInverse, IScalarArithmetic.

Implemented by: Number, Unit.

Inherited by: IReal.

Functions: .
## IAny

Inherits: .

Implemented by: Number, Integer, String, Boolean, Character, Unit, Probability, Complex, Integer2, Integer3, Integer4, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Size2D, Size3D, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4, Transform2D, Pose2D, Bounds2D, Ray2D, Triangle2D, Quad2D, Sphere, Plane, Transform3D, Pose3D, Bounds3D, Ray3D, Triangle3D, Quad3D, Quaternion, AxisAngle, EulerAngles, Rotation3D, Orientation3D, Line4D.

Inherited by: IValue, INumerical, INumberLike, IReal, IWholeNumber, IMeasure, IVector, ICoordinate, IOrderable, IEquatable, IAdditive, IScalarArithmetic, IMultiplicative, IInvertible, IMultiplicativeWithInverse, IAlgebraic, IInterpolatable, IDivisible, IModulo, IArithmetic, IBoolean, IInterval.

Functions: FieldNames, FieldValues, TypeName.

































## IArithmetic

Inherits: IAdditive, IAny, IAny, IAny, IAny, IDivisible, IModulo, IMultiplicative.

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: IReal, IWholeNumber, IVector.

Functions: .
## IArray

Inherits: .

Implemented by: String, Array, Array2D, Array3D, Complex, Integer2, Integer3, Integer4, Size2D, Size3D, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4, Bounds2D, Triangle2D, Quad2D, Line2D, Bounds3D, Line3D, Triangle3D, Quad3D, CubicBezier2D, CubicBezier3D, QuadraticBezier2D, QuadraticBezier3D, Line4D.

Inherited by: IArray2D, IArray3D, IVector, IInterval, IGrid2D, IQuadGrid.

Functions: At, Count.









## IArray2D

Inherits: IArray.

Implemented by: Array2D.

Inherited by: IGrid2D, IQuadGrid.

Functions: At, ColumnCount, RowCount.

























## IArray3D

Inherits: IArray.

Implemented by: Array3D.

Inherited by: .

Functions: At, ColumnCount, LayerCount, RowCount.





































## IBoolean

Inherits: IAny.

Implemented by: Boolean.

Inherited by: .

Functions: And, Not, Or.












## IBounded2D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Bounds.






## IBounded3D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Bounds.






## IClosedPolyLine2D

Inherits: IClosedShape2D, IGeometry, IGeometry, IGeometry2D, IGeometry2D, IOpenClosedShape, IOpenClosedShape, IPoints2D, IPolyLine2D.

Implemented by: Rect2D, RegularPolygon.

Inherited by: IPolygon2D.

Functions: .
## IClosedPolyLine3D

Inherits: IClosedShape3D, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IOpenClosedShape, IOpenClosedShape, IPoints3D, IPolyLine3D.

Implemented by: .

Inherited by: IPolygon3D.

Functions: .
## IClosedShape2D

Inherits: IGeometry, IGeometry2D, IOpenClosedShape.

Implemented by: Circle, Lens, Rect2D, Ring, Sector, Chord, Segment, RegularPolygon.

Inherited by: IClosedPolyLine2D, IPolygon2D.

Functions: .
## IClosedShape3D

Inherits: IGeometry, IGeometry3D, IOpenClosedShape.

Implemented by: .

Inherited by: IClosedPolyLine3D, IPolygon3D.

Functions: .
## ICoordinate

Inherits: IAny, IAny, IEquatable, IValue.

Implemented by: Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, DateTime.

Inherited by: .

Functions: .
## ICurve

Inherits: IOpenClosedShape, IProcedural.

Implemented by: Ellipse.

Inherited by: ICurve1D, ICurve2D, ICurve3D.

Functions: .
## ICurve1D

Inherits: ICurve, IOpenClosedShape, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## ICurve2D

Inherits: ICurve, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

Implemented by: Ellipse.

Inherited by: .

Functions: .
## ICurve3D

Inherits: ICurve, IGeometry, IGeometry3D, IOpenClosedShape, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IDeformable2D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Deform.






## IDeformable3D

Inherits: ITransformable3D.

Implemented by: .

Inherited by: .

Functions: Deform.






## IDistanceField

Inherits: IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IDivisible

Inherits: IAny.

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: IReal, IWholeNumber, IVector, IArithmetic.

Functions: Divide.






## IEquatable

Inherits: IAny.

Implemented by: Number, Integer, String, Boolean, Character, Unit, Probability, Complex, Integer2, Integer3, Integer4, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Size2D, Size3D, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4, Transform2D, Pose2D, Bounds2D, Ray2D, Triangle2D, Quad2D, Sphere, Plane, Transform3D, Pose3D, Bounds3D, Ray3D, Triangle3D, Quad3D, Quaternion, AxisAngle, EulerAngles, Rotation3D, Orientation3D, Line4D.

Inherited by: IValue, INumerical, INumberLike, IReal, IWholeNumber, IMeasure, IVector, ICoordinate, IOrderable, IInterval.

Functions: Equals.






## IExplicitSurface

Inherits: IGeometry, IGeometry3D, IProcedural, ISurface.

Implemented by: .

Inherited by: .

Functions: .
## IField2D

Inherits: IGeometry, IGeometry2D, IProcedural.

Implemented by: .

Inherited by: IScalarField2D, IVector3Field2D, IVector4Field2D.

Functions: .
## IField3D

Inherits: IGeometry, IGeometry3D, IProcedural.

Implemented by: .

Inherited by: IScalarField3D, IVector2Field3D, IVector3Field3D, IVector4Field3D.

Functions: .
## IGeometry

Inherits: .

Implemented by: Line2D, Circle, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Box2D, Line3D, Capsule, Cylinder, Cone, Tube, ConeSegment, Box3D, LineMesh, TriangleMesh, QuadMesh, Lines, Triangles, Quads.

Inherited by: IGeometry2D, IGeometry3D, IShape2D, IShape3D, IOpenShape2D, IClosedShape2D, IOpenShape3D, IClosedShape3D, ICurve2D, ICurve3D, ISurface, IProceduralSurface, IExplicitSurface, IField2D, IField3D, IScalarField2D, IScalarField3D, IVector3Field2D, IVector4Field2D, IVector2Field3D, IVector3Field3D, IVector4Field3D, IImplicitSurface, IImplicitCurve2D, IImplicitVolume, IPoints2D, IPoints3D, IPolyLine2D, IPolyLine3D, IClosedPolyLine2D, IClosedPolyLine3D, IPolygon2D, IPolygon3D, IIndexedGeometry3D, ILineMesh, ITriangleMesh, IQuadMesh, IPrimitives3D, ILines, ITriangles, IQuads.

Functions: .
## IGeometry2D

Inherits: IGeometry.

Implemented by: Line2D, Circle, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Box2D.

Inherited by: IShape2D, IOpenShape2D, IClosedShape2D, ICurve2D, IField2D, IScalarField2D, IVector3Field2D, IVector4Field2D, IImplicitCurve2D, IPoints2D, IPolyLine2D, IClosedPolyLine2D, IPolygon2D.

Functions: .
## IGeometry3D

Inherits: IGeometry.

Implemented by: Line3D, Capsule, Cylinder, Cone, Tube, ConeSegment, Box3D, LineMesh, TriangleMesh, QuadMesh, Lines, Triangles, Quads.

Inherited by: IShape3D, IOpenShape3D, IClosedShape3D, ICurve3D, ISurface, IProceduralSurface, IExplicitSurface, IField3D, IScalarField3D, IVector2Field3D, IVector3Field3D, IVector4Field3D, IImplicitSurface, IImplicitVolume, IPoints3D, IPolyLine3D, IClosedPolyLine3D, IPolygon3D, IIndexedGeometry3D, ILineMesh, ITriangleMesh, IQuadMesh, IPrimitives3D, ILines, ITriangles, IQuads.

Functions: .
## IGrid2D

Inherits: IArray, IArray2D.

Implemented by: .

Inherited by: .

Functions: .
## IImplicitCurve2D

Inherits: IGeometry, IGeometry2D, IImplicitProcedural, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IImplicitProcedural

Inherits: IProcedural.

Implemented by: .

Inherited by: IImplicitSurface, IImplicitCurve2D, IImplicitVolume.

Functions: .
## IImplicitSurface

Inherits: IGeometry, IGeometry3D, IImplicitProcedural, IProcedural, ISurface.

Implemented by: .

Inherited by: .

Functions: .
## IImplicitVolume

Inherits: IGeometry, IGeometry3D, IImplicitProcedural, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IIndexedGeometry3D

Inherits: IGeometry, IGeometry3D, IPoints3D.

Implemented by: LineMesh, TriangleMesh, QuadMesh, Lines, Triangles, Quads.

Inherited by: ILineMesh, ITriangleMesh, IQuadMesh, IPrimitives3D, ILines, ITriangles, IQuads.

Functions: Indices, PrimitiveSize.






















## IInterpolatable

Inherits: IAdditive, IAny, IAny, IScalarArithmetic.

Implemented by: Number, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Inherited by: IReal, IMeasure, IVector, IAlgebraic.

Functions: .
## IInterval

Inherits: IAny, IAny, IAny, IArray, IEquatable, IEquatable, IValue.

Implemented by: AnglePair, NumberInterval, Bounds2D, Bounds3D.

Inherited by: .

Functions: Max, Min.








## IInvertible

Inherits: IAny.

Implemented by: Number, Unit.

Inherited by: IReal, IMultiplicativeWithInverse, IAlgebraic.

Functions: Inverse.







## ILineMesh

Inherits: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D.

Implemented by: LineMesh.

Inherited by: .

Functions: .
## ILines

Inherits: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D, IPrimitives3D.

Implemented by: Lines.

Inherited by: .

Functions: .
## IMeasure

Inherits: IAdditive, IAdditive, IAdditive, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IEquatable, IEquatable, IInterpolatable, INumberLike, INumerical, IOrderable, IScalarArithmetic, IScalarArithmetic, IValue.

Implemented by: Probability, Angle, Length, Mass, Temperature, Time.

Inherited by: .

Functions: .
## IModulo

Inherits: IAny.

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: IReal, IWholeNumber, IVector, IArithmetic.

Functions: Modulo.






## IMultiplicative

Inherits: IAny.

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: IReal, IWholeNumber, IVector, IMultiplicativeWithInverse, IAlgebraic, IArithmetic.

Functions: Multiply.








## IMultiplicativeWithInverse

Inherits: IAny, IAny, IInvertible, IMultiplicative.

Implemented by: Number, Unit.

Inherited by: IReal, IAlgebraic.

Functions: .
## INumberLike

Inherits: IAdditive, IAny, IAny, IAny, IAny, IAny, IEquatable, IEquatable, INumerical, IOrderable, IScalarArithmetic, IValue.

Implemented by: Number, Unit, Probability, Angle, Length, Mass, Temperature, Time.

Inherited by: IReal, IMeasure.

Functions: FromNumber, ToNumber.




















## INumerical

Inherits: IAdditive, IAny, IAny, IAny, IAny, IEquatable, IScalarArithmetic, IValue.

Implemented by: Number, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Inherited by: INumberLike, IReal, IMeasure, IVector.

Functions: Components, FromComponents.


























## IOpenClosedShape

Inherits: .

Implemented by: Line2D, Circle, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Line3D.

Inherited by: IOpenShape2D, IClosedShape2D, IOpenShape3D, IClosedShape3D, ICurve, ICurve1D, ICurve2D, ICurve3D, IPolyLine2D, IPolyLine3D, IClosedPolyLine2D, IClosedPolyLine3D, IPolygon2D, IPolygon3D.

Functions: Closed.






## IOpenShape2D

Inherits: IGeometry, IGeometry2D, IOpenClosedShape.

Implemented by: Arc.

Inherited by: .

Functions: .
## IOpenShape3D

Inherits: IGeometry, IGeometry3D, IOpenClosedShape.

Implemented by: .

Inherited by: .

Functions: .
## IOrderable

Inherits: IAny, IEquatable.

Implemented by: Number, Integer, String, Boolean, Character, Unit, Probability, Angle, Length, Mass, Temperature, Time.

Inherited by: INumberLike, IReal, IWholeNumber, IMeasure.

Functions: LessThanOrEquals.
















## IPoints2D

Inherits: IGeometry, IGeometry2D.

Implemented by: Line2D, Rect2D, RegularPolygon.

Inherited by: IPolyLine2D, IClosedPolyLine2D, IPolygon2D.

Functions: Points.






## IPoints3D

Inherits: IGeometry, IGeometry3D.

Implemented by: Line3D, LineMesh, TriangleMesh, QuadMesh, Lines, Triangles, Quads.

Inherited by: IPolyLine3D, IClosedPolyLine3D, IPolygon3D, IIndexedGeometry3D, ILineMesh, ITriangleMesh, IQuadMesh, IPrimitives3D, ILines, ITriangles, IQuads.

Functions: Points.






## IPolygon2D

Inherits: IClosedPolyLine2D, IClosedShape2D, IGeometry, IGeometry, IGeometry2D, IGeometry2D, IOpenClosedShape, IOpenClosedShape, IPoints2D, IPolyLine2D.

Implemented by: Rect2D, RegularPolygon.

Inherited by: .

Functions: .
## IPolygon3D

Inherits: IClosedPolyLine3D, IClosedShape3D, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IOpenClosedShape, IOpenClosedShape, IPoints3D, IPolyLine3D.

Implemented by: .

Inherited by: .

Functions: .
## IPolyLine2D

Inherits: IGeometry, IGeometry2D, IOpenClosedShape, IPoints2D.

Implemented by: Line2D, Rect2D, RegularPolygon.

Inherited by: IClosedPolyLine2D, IPolygon2D.

Functions: .
## IPolyLine3D

Inherits: IGeometry, IGeometry3D, IOpenClosedShape, IPoints3D.

Implemented by: Line3D.

Inherited by: IClosedPolyLine3D, IPolygon3D.

Functions: .
## IPrimitives3D

Inherits: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D.

Implemented by: Lines, Triangles, Quads.

Inherited by: ILines, ITriangles, IQuads.

Functions: Primitives.










## IProcedural

Inherits: .

Implemented by: Ellipse.

Inherited by: ICurve, ICurve1D, ICurve2D, ICurve3D, IProceduralSurface, IExplicitSurface, IDistanceField, IField2D, IField3D, IScalarField2D, IScalarField3D, IVector3Field2D, IVector4Field2D, IVector2Field3D, IVector3Field3D, IVector4Field3D, IImplicitProcedural, IImplicitSurface, IImplicitCurve2D, IImplicitVolume.

Functions: Eval.




## IProceduralSurface

Inherits: IGeometry, IGeometry3D, IProcedural, ISurface.

Implemented by: .

Inherited by: .

Functions: PeriodicX, PeriodicY.




















## IQuadGrid

Inherits: IArray, IArray2D.

Implemented by: .

Inherited by: .

Functions: ClosedX, ClosedY.
















## IQuadMesh

Inherits: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D.

Implemented by: QuadMesh.

Inherited by: .

Functions: .
## IQuads

Inherits: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D, IPrimitives3D.

Implemented by: Quads.

Inherited by: .

Functions: .
## IReal

Inherits: IAdditive, IAdditive, IAdditive, IAlgebraic, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IArithmetic, IDivisible, IEquatable, IEquatable, IInterpolatable, IInvertible, IModulo, IMultiplicative, IMultiplicative, IMultiplicativeWithInverse, INumberLike, INumerical, IOrderable, IScalarArithmetic, IScalarArithmetic, IValue.

Implemented by: Number, Unit.

Inherited by: .

Functions: .
## IScalarArithmetic

Inherits: IAny.

Implemented by: Number, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Inherited by: INumerical, INumberLike, IReal, IMeasure, IVector, IAlgebraic, IInterpolatable.

Functions: Divide, Modulo, Multiply.
























## IScalarField2D

Inherits: IField2D, IGeometry, IGeometry2D, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IScalarField3D

Inherits: IField3D, IGeometry, IGeometry3D, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IShape2D

Inherits: IGeometry, IGeometry2D.

Implemented by: Box2D.

Inherited by: .

Functions: .
## IShape3D

Inherits: IGeometry, IGeometry3D.

Implemented by: Capsule, Cylinder, Cone, Tube, ConeSegment, Box3D.

Inherited by: .

Functions: .
## ISurface

Inherits: IGeometry, IGeometry3D.

Implemented by: .

Inherited by: IProceduralSurface, IExplicitSurface, IImplicitSurface.

Functions: .
## ITransformable2D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Transform.









## ITransformable3D

Inherits: .

Implemented by: .

Inherited by: IDeformable3D.

Functions: Transform.









## ITriangleMesh

Inherits: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D.

Implemented by: TriangleMesh.

Inherited by: .

Functions: .
## ITriangles

Inherits: IGeometry, IGeometry3D, IIndexedGeometry3D, IPoints3D, IPrimitives3D.

Implemented by: Triangles.

Inherited by: .

Functions: .
## IValue

Inherits: IAny, IAny, IEquatable.

Implemented by: Number, Integer, String, Boolean, Character, Unit, Probability, Complex, Integer2, Integer3, Integer4, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Size2D, Size3D, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4, Transform2D, Pose2D, Bounds2D, Ray2D, Triangle2D, Quad2D, Sphere, Plane, Transform3D, Pose3D, Bounds3D, Ray3D, Triangle3D, Quad3D, Quaternion, AxisAngle, EulerAngles, Rotation3D, Orientation3D, Line4D.

Inherited by: INumerical, INumberLike, IReal, IWholeNumber, IMeasure, IVector, ICoordinate, IInterval.

Functions: .
## IVector

Inherits: IAdditive, IAdditive, IAdditive, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IArithmetic, IArray, IDivisible, IEquatable, IInterpolatable, IModulo, IMultiplicative, INumerical, IScalarArithmetic, IScalarArithmetic, IValue.

Implemented by: Complex, Vector2D, Vector3D, Vector4D.

Inherited by: .

Functions: .
## IVector2Field3D

Inherits: IField3D, IGeometry, IGeometry3D, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IVector3Field2D

Inherits: IField2D, IGeometry, IGeometry2D, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IVector3Field3D

Inherits: IField3D, IGeometry, IGeometry3D, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IVector4Field2D

Inherits: IField2D, IGeometry, IGeometry2D, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IVector4Field3D

Inherits: IField3D, IGeometry, IGeometry3D, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IWholeNumber

Inherits: IAdditive, IAny, IAny, IAny, IAny, IAny, IAny, IAny, IArithmetic, IDivisible, IEquatable, IEquatable, IModulo, IMultiplicative, IOrderable, IValue.

Implemented by: Integer.

Inherited by: .

Functions: .
</Details>
