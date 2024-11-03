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

Implements: IGeometry, IGeometry2D, IOpenClosedShape, IOpenShape, IOpenShape2D.

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

Implements: IAny, IEquatable, ITransform3D, IValue.

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

Fields: Center:ConcreteType:Vector2D, Extent:ConcreteType:Vector2D, Rotation:ConcreteType:Angle.

Implements: IGeometry, IGeometry2D, IShape2D.

## Type Box3D

Fields: Extent:ConcreteType:Vector3D.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type ButterflyCurve

Fields: .

Implements: IClosedCurve2D, IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

## Type Capsule

Fields: Height:ConcreteType:Number, Radius:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type Character

Fields: .

Implements: IAny, IEquatable, IOrderable, IValue.

## Type Chord

Fields: Arc:ConcreteType:Arc.

Implements: IClosedShape, IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Circle

Fields: Center:ConcreteType:Vector2D, Radius:ConcreteType:Number.

Implements: IClosedCurve2D, IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

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

Fields: Height:ConcreteType:Number, Radius:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type ConeSegment

Fields: Height:ConcreteType:Number, Radius1:ConcreteType:Number, Radius2:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type Cos

Fields: Amplitude:ConcreteType:Number, Frequency:ConcreteType:Number, Phase:ConcreteType:Number.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

## Type CubicBezier2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D, D:ConcreteType:Vector2D.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IOpenCurve2D, IOpenShape, IOpenShape2D, IPointArray2D, IPointGeometry2D, IProcedural.

## Type CubicBezier3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D, D:ConcreteType:Vector3D.

Implements: ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IPointArray3D, IPointGeometry3D, IProcedural.

## Type CubicFunction2D

Fields: A:ConcreteType:Number, B:ConcreteType:Number, C:ConcreteType:Number, D:ConcreteType:Number.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IOpenCurve2D, IOpenShape, IOpenShape2D, IProcedural.

## Type Cylinder

Fields: Height:ConcreteType:Number, Radius:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, ISurface.

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

Fields: Center:ConcreteType:Vector2D, Size:ConcreteType:Vector2D.

Implements: IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

## Type Ellipsoid

Fields: Radii:ConcreteType:Vector3D.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type Error

Fields: .

Implements: .

## Type EulerAngles

Fields: Pitch:ConcreteType:Angle, Roll:ConcreteType:Angle, Yaw:ConcreteType:Angle.

Implements: IAny, IEquatable, ITransform3D, IValue.

## Type FigureEightKnot

Fields: .

Implements: ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IProcedural.

## Type Fraction

Fields: Denominator:ConcreteType:Number, Numerator:ConcreteType:Number.

Implements: IAny, IEquatable, IValue.

## Type Frame3D

Fields: Forward:ConcreteType:Vector3D, Position:ConcreteType:Vector3D, Up:ConcreteType:Vector3D.

Implements: IAny, IEquatable, ITransform3D, IValue.

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

## Type Helix

Fields: Height:ConcreteType:Number, NumTurns:ConcreteType:Number, Radius:ConcreteType:Number.

Implements: ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IProcedural.

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

Implements: IClosedShape, IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Line2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IGeometry3D, IOpenClosedShape, IOpenShape, IOpenShape3D, IPointGeometry2D, IPolyLine2D, IProcedural.

## Type Line3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D.

Implements: ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IOpenShape, IOpenShape3D, IPointGeometry3D, IPolyLine3D, IProcedural.

## Type LinearFunction2D

Fields: Slope:ConcreteType:Number, YIntercept:ConcreteType:Number.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IOpenCurve2D, IOpenShape, IOpenShape2D, IProcedural.

## Type LineArray2D

Fields: Primitives:Concept:IArray<ConcreteType:Line2D>.

Implements: IGeometry, IGeometry2D, ILineArray2D, ILinePrimitives, IPointGeometry2D, IPrimitiveArray2D, IPrimitiveGeometry, IPrimitiveGeometry2D.

## Type LineArray3D

Fields: Primitives:Concept:IArray<ConcreteType:Line3D>.

Implements: IGeometry, IGeometry3D, ILineArray3D, ILinePrimitives, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D.

## Type LineMesh3D

Fields: Indices:Concept:IArray<ConcreteType:Integer>, Points:Concept:IArray<ConcreteType:Vector3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry, IIndexedGeometry3D, IIndexedPrimitives3D, ILineMesh3D, ILinePrimitives, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D.

## Type Lissajous

Fields: Kx:ConcreteType:Integer, Ky:ConcreteType:Integer.

Implements: IClosedCurve2D, IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

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

## Type NPrism

Fields: Height:ConcreteType:Number, NumSides:ConcreteType:Integer, Radius:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type NPyramid

Fields: Height:ConcreteType:Number, NumSides:ConcreteType:Integer, Radius:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type Number

Fields: .

Implements: IAdditive, IAlgebraic, IAny, IArithmetic, IDivisible, IEquatable, IInterpolatable, IInvertible, IModulo, IMultiplicative, IMultiplicativeWithInverse, INumberLike, INumerical, IOrderable, IReal, IScalarArithmetic, IValue.

## Type NumberInterval

Fields: Max:ConcreteType:Number, Min:ConcreteType:Number.

Implements: IAny, IArray, IEquatable, IInterval, IValue.

## Type Parabola

Fields: .

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IOpenCurve2D, IOpenShape, IOpenShape2D, IProcedural.

## Type Plane

Fields: D:ConcreteType:Number, Normal:ConcreteType:Vector3D.

Implements: IAny, IEquatable, IValue.

## Type PointArray2D

Fields: Points:Concept:IArray<ConcreteType:Vector2D>.

Implements: IGeometry, IGeometry2D, IPointArray2D, IPointGeometry2D.

## Type PointArray3D

Fields: Points:Concept:IArray<ConcreteType:Vector3D>.

Implements: IGeometry, IGeometry3D, IPointArray3D, IPointGeometry3D.

## Type PolarCoordinate

Fields: Angle:ConcreteType:Angle, Radius:ConcreteType:Number.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type PolyLine2D

Fields: Closed:ConcreteType:Boolean, Points:Concept:IArray<ConcreteType:Vector2D>.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IPointGeometry2D, IPolyLine2D, IProcedural.

## Type PolyLine3D

Fields: Closed:ConcreteType:Boolean, Points:Concept:IArray<ConcreteType:Vector3D>.

Implements: ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IPointGeometry3D, IPolyLine3D, IProcedural.

## Type Pose2D

Fields: Position:ConcreteType:Vector2D, Rotation:ConcreteType:Angle.

Implements: IAny, IEquatable, IValue.

## Type Pose3D

Fields: Position:ConcreteType:Vector3D, Rotation:ConcreteType:Rotation3D.

Implements: IAny, IEquatable, ITransform3D, IValue.

## Type Probability

Fields: Value:ConcreteType:Number.

Implements: IAdditive, IAny, IEquatable, IInterpolatable, IMeasure, INumberLike, INumerical, IOrderable, IScalarArithmetic, IValue.

## Type Pyramid

Fields: BaseLength:ConcreteType:Number, Height:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type Quad2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D, D:ConcreteType:Vector2D.

Implements: IClosedPolyLine2D, IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IPointGeometry2D, IPolygon2D, IPolyLine2D, IProcedural.

## Type Quad3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D, D:ConcreteType:Vector3D.

Implements: IClosedPolyLine3D, IClosedShape, IClosedShape3D, ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IPointGeometry3D, IPolygon3D, IPolyLine3D, IProcedural.

## Type QuadArray2D

Fields: Primitives:Concept:IArray<ConcreteType:Quad2D>.

Implements: IGeometry, IGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D, IQuadArray2D, IQuadPrimitives.

## Type QuadArray3D

Fields: Primitives:Concept:IArray<ConcreteType:Quad3D>.

Implements: IGeometry, IGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D, IQuadArray3D, IQuadPrimitives.

## Type QuadGrid3D

Fields: ClosedX:ConcreteType:Boolean, ClosedY:ConcreteType:Boolean, PointGrid:Concept:IArray2D<ConcreteType:Vector3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry, IIndexedGeometry3D, IIndexedPrimitives3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D, IQuadGrid3D, IQuadMesh3D, IQuadPrimitives.

## Type QuadMesh3D

Fields: Indices:Concept:IArray<ConcreteType:Integer>, Points:Concept:IArray<ConcreteType:Vector3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry, IIndexedGeometry3D, IIndexedPrimitives3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D, IQuadMesh3D, IQuadPrimitives.

## Type QuadraticBezier2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IOpenCurve2D, IOpenShape, IOpenShape2D, IPointArray2D, IPointGeometry2D, IProcedural.

## Type QuadraticBezier3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D.

Implements: ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IPointArray3D, IPointGeometry3D, IProcedural.

## Type QuadraticFunction2D

Fields: A:ConcreteType:Number, B:ConcreteType:Number, C:ConcreteType:Number.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IOpenCurve2D, IOpenShape, IOpenShape2D, IProcedural.

## Type Quaternion

Fields: W:ConcreteType:Number, X:ConcreteType:Number, Y:ConcreteType:Number, Z:ConcreteType:Number.

Implements: IAny, IArray, IEquatable, ITransform3D, IValue.

## Type Rational

Fields: Denominator:ConcreteType:Integer, Numerator:ConcreteType:Integer.

Implements: IAny, IEquatable, IValue.

## Type Ray2D

Fields: Direction:ConcreteType:Vector2D, Origin:ConcreteType:Vector2D.

Implements: IAny, IEquatable, IValue.

## Type Ray3D

Fields: Direction:ConcreteType:Vector3D, Origin:ConcreteType:Vector3D.

Implements: IAny, IEquatable, IValue.

## Type Rect2D

Fields: Center:ConcreteType:Vector2D, Size:ConcreteType:Vector2D.

Implements: IClosedPolyLine2D, IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IPointGeometry2D, IPolygon2D, IPolyLine2D, IProcedural.

## Type RegularPolygon

Fields: NumPoints:ConcreteType:Integer.

Implements: IClosedPolyLine2D, IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IPointGeometry2D, IPolygon2D, IPolyLine2D, IProcedural.

## Type Ring

Fields: Center:ConcreteType:Vector2D, InnerRadius:ConcreteType:Number, OuterRadius:ConcreteType:Number.

Implements: IClosedShape, IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Rotation3D

Fields: Quaternion:ConcreteType:Quaternion.

Implements: IAny, IEquatable, ITransform3D, IValue.

## Type Sector

Fields: Arc:ConcreteType:Arc.

Implements: IClosedShape, IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Segment

Fields: Arc:ConcreteType:Arc.

Implements: IClosedShape, IClosedShape2D, IGeometry, IGeometry2D, IOpenClosedShape.

## Type Sin

Fields: Amplitude:ConcreteType:Number, Frequency:ConcreteType:Number, Phase:ConcreteType:Number.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

## Type Sphere

Fields: Radius:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type SphericalCoordinate

Fields: Azimuth:ConcreteType:Angle, Polar:ConcreteType:Angle, Radius:ConcreteType:Number.

Implements: IAny, ICoordinate, IEquatable, IValue.

## Type Spiral

Fields: NumTurns:ConcreteType:Number, Radius1:ConcreteType:Number, Radius2:ConcreteType:Number.

Implements: ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

## Type String

Fields: .

Implements: IAny, IArray, IEquatable, IOrderable, IValue.

## Type Temperature

Fields: Celsius:ConcreteType:Number.

Implements: IAdditive, IAny, IEquatable, IInterpolatable, IMeasure, INumberLike, INumerical, IOrderable, IScalarArithmetic, IValue.

## Type Time

Fields: Seconds:ConcreteType:Number.

Implements: IAdditive, IAny, IEquatable, IInterpolatable, IMeasure, INumberLike, INumerical, IOrderable, IScalarArithmetic, IValue.

## Type Torus

Fields: MajorRadius:ConcreteType:Number, MinorRadius:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

## Type TorusKnot

Fields: P:ConcreteType:Integer, Q:ConcreteType:Integer, Radius:ConcreteType:Number.

Implements: ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IProcedural.

## Type Transform2D

Fields: Rotation:ConcreteType:Angle, Scale:ConcreteType:Vector2D, Translation:ConcreteType:Vector2D.

Implements: IAny, IEquatable, IValue.

## Type Transform3D

Fields: Rotation:ConcreteType:Quaternion, Scale:ConcreteType:Vector3D, Translation:ConcreteType:Vector3D.

Implements: IAny, IEquatable, ITransform3D, IValue.

## Type TrefoilKnot

Fields: .

Implements: ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IProcedural.

## Type Triangle2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D.

Implements: IClosedPolyLine2D, IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry2D, IOpenClosedShape, IPointGeometry2D, IPolygon2D, IPolyLine2D, IProcedural.

## Type Triangle3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D.

Implements: IClosedPolyLine3D, IClosedShape, IClosedShape3D, ICurve, ICurve3D, IGeometry, IGeometry3D, IOpenClosedShape, IPointGeometry3D, IPolygon3D, IPolyLine3D, IProcedural.

## Type TriangleArray2D

Fields: Primitives:Concept:IArray<ConcreteType:Triangle2D>.

Implements: IGeometry, IGeometry2D, IPointGeometry2D, IPrimitiveArray2D, IPrimitiveGeometry, IPrimitiveGeometry2D, ITriangleArray2D, ITrianglePrimitives.

## Type TriangleArray3D

Fields: Primitives:Concept:IArray<ConcreteType:Triangle3D>.

Implements: IGeometry, IGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D, ITriangleArray3D, ITrianglePrimitives.

## Type TriangleMesh3D

Fields: Indices:Concept:IArray<ConcreteType:Integer>, Points:Concept:IArray<ConcreteType:Vector3D>.

Implements: IGeometry, IGeometry3D, IIndexedGeometry, IIndexedGeometry3D, IIndexedPrimitives3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D, ITriangleMesh3D, ITrianglePrimitives.

## Type Tube

Fields: Height:ConcreteType:Number, InnerRadius:ConcreteType:Number, OuterRadius:ConcreteType:Number.

Implements: IDistanceField3D, IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D, IShape3D, ISolid, ISurface.

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

Implemented by: Number, Integer, String, Boolean, Character, Unit, Probability, Complex, Integer2, Integer3, Integer4, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4, Transform2D, Pose2D, Bounds2D, Ray2D, Plane, Bounds3D, Ray3D, Transform3D, Pose3D, Frame3D, Quaternion, AxisAngle, EulerAngles, Rotation3D.

Inherited by: IValue, INumerical, INumberLike, IReal, IWholeNumber, IMeasure, IVector, ICoordinate, IOrderable, IEquatable, IAdditive, IScalarArithmetic, IMultiplicative, IInvertible, IMultiplicativeWithInverse, IAlgebraic, IInterpolatable, IDivisible, IModulo, IArithmetic, IBoolean, IInterval.

Functions: FieldNames, FieldValues, TypeName.

































## IArithmetic

Inherits: IAdditive, IAny, IAny, IAny, IAny, IDivisible, IModulo, IMultiplicative.

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: IReal, IWholeNumber, IVector.

Functions: .
## IArray

Inherits: .

Implemented by: String, Array, Array2D, Array3D, Complex, Integer2, Integer3, Integer4, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4, Bounds2D, Bounds3D, Quaternion.

Inherited by: IArray2D, IArray3D, IVector, IInterval.

Functions: At, Count.









## IArray2D

Inherits: IArray.

Implemented by: Array2D.

Inherited by: .

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






## IClosedCurve2D

Inherits: IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry, IGeometry2D, IGeometry2D, IOpenClosedShape, IOpenClosedShape, IProcedural.

Implemented by: Circle, Lissajous, ButterflyCurve.

Inherited by: .

Functions: .
## IClosedCurve3D

Inherits: IClosedShape, IClosedShape3D, ICurve, ICurve3D, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IOpenClosedShape, IOpenClosedShape, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IClosedPolyLine2D

Inherits: IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry, IGeometry, IGeometry2D, IGeometry2D, IGeometry2D, IOpenClosedShape, IOpenClosedShape, IOpenClosedShape, IPointGeometry2D, IPolyLine2D, IProcedural.

Implemented by: Triangle2D, Quad2D, Rect2D, RegularPolygon.

Inherited by: IPolygon2D.

Functions: .
## IClosedPolyLine3D

Inherits: IClosedShape, IClosedShape3D, ICurve, ICurve3D, IGeometry, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IGeometry3D, IOpenClosedShape, IOpenClosedShape, IOpenClosedShape, IPointGeometry3D, IPolyLine3D, IProcedural.

Implemented by: Triangle3D, Quad3D.

Inherited by: IPolygon3D.

Functions: .
## IClosedShape

Inherits: IOpenClosedShape.

Implemented by: Triangle2D, Quad2D, Lens, Rect2D, Ellipse, Ring, Sector, Chord, Segment, RegularPolygon, Triangle3D, Quad3D, Circle, Lissajous, ButterflyCurve.

Inherited by: IClosedShape2D, IClosedShape3D, IClosedCurve2D, IClosedCurve3D, IClosedPolyLine2D, IClosedPolyLine3D, IPolygon2D, IPolygon3D.

Functions: .
## IClosedShape2D

Inherits: IClosedShape, IGeometry, IGeometry2D, IOpenClosedShape.

Implemented by: Triangle2D, Quad2D, Lens, Rect2D, Ellipse, Ring, Sector, Chord, Segment, RegularPolygon, Circle, Lissajous, ButterflyCurve.

Inherited by: IClosedCurve2D, IClosedPolyLine2D, IPolygon2D.

Functions: .
## IClosedShape3D

Inherits: IClosedShape, IGeometry, IGeometry3D, IOpenClosedShape.

Implemented by: Triangle3D, Quad3D.

Inherited by: IClosedCurve3D, IClosedPolyLine3D, IPolygon3D.

Functions: .
## ICoordinate

Inherits: IAny, IAny, IEquatable, IValue.

Implemented by: Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, DateTime.

Inherited by: .

Functions: .
## ICurve

Inherits: IOpenClosedShape, IProcedural.

Implemented by: Triangle2D, Quad2D, Line2D, Rect2D, Ellipse, RegularPolygon, Line3D, Triangle3D, Quad3D, CubicBezier2D, QuadraticBezier2D, LinearFunction2D, QuadraticFunction2D, CubicFunction2D, Parabola, Circle, Lissajous, ButterflyCurve, Spiral, Sin, Cos, CubicBezier3D, QuadraticBezier3D, TorusKnot, Helix, TrefoilKnot, FigureEightKnot, PolyLine2D, PolyLine3D.

Inherited by: ICurve1D, ICurve2D, IClosedCurve2D, IOpenCurve2D, ICurve3D, IClosedCurve3D, IOpenCurve3D, IPolyLine2D, IPolyLine3D, IClosedPolyLine2D, IClosedPolyLine3D, IPolygon2D, IPolygon3D.

Functions: .
## ICurve1D

Inherits: ICurve, IOpenClosedShape, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## ICurve2D

Inherits: ICurve, IGeometry, IGeometry2D, IOpenClosedShape, IProcedural.

Implemented by: Triangle2D, Quad2D, Line2D, Rect2D, Ellipse, RegularPolygon, CubicBezier2D, QuadraticBezier2D, LinearFunction2D, QuadraticFunction2D, CubicFunction2D, Parabola, Circle, Lissajous, ButterflyCurve, Spiral, Sin, Cos, PolyLine2D.

Inherited by: IClosedCurve2D, IOpenCurve2D, IPolyLine2D, IClosedPolyLine2D, IPolygon2D.

Functions: .
## ICurve3D

Inherits: ICurve, IGeometry, IGeometry3D, IOpenClosedShape, IProcedural.

Implemented by: Line3D, Triangle3D, Quad3D, CubicBezier3D, QuadraticBezier3D, TorusKnot, Helix, TrefoilKnot, FigureEightKnot, PolyLine3D.

Inherited by: IClosedCurve3D, IOpenCurve3D, IPolyLine3D, IClosedPolyLine3D, IPolygon3D.

Functions: .
## IDeformable2D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Deform.






## IDeformable3D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Deform.






## IDistanceField2D

Inherits: IField2D, IGeometry, IGeometry2D, IProcedural, IScalarField2D.

Implemented by: .

Inherited by: .

Functions: Distance.








## IDistanceField3D

Inherits: IField3D, IGeometry, IGeometry3D, IProcedural, IScalarField3D.

Implemented by: Sphere, Cylinder, Capsule, Cone, ConeSegment, Box3D, Pyramid, Torus, NPrism, Tube, NPyramid, Ellipsoid.

Inherited by: ISurface, IProceduralSurface, IExplicitSurface, IImplicitSurface, ISolid.

Functions: Distance.








## IDivisible

Inherits: IAny.

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: IReal, IWholeNumber, IVector, IArithmetic.

Functions: Divide.






## IEquatable

Inherits: IAny.

Implemented by: Number, Integer, String, Boolean, Character, Unit, Probability, Complex, Integer2, Integer3, Integer4, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4, Transform2D, Pose2D, Bounds2D, Ray2D, Plane, Bounds3D, Ray3D, Transform3D, Pose3D, Frame3D, Quaternion, AxisAngle, EulerAngles, Rotation3D.

Inherited by: IValue, INumerical, INumberLike, IReal, IWholeNumber, IMeasure, IVector, ICoordinate, IOrderable, IInterval.

Functions: Equals.






## IExplicitSurface

Inherits: IDistanceField3D, IField3D, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IProcedural, IProcedural, IScalarField3D, ISurface.

Implemented by: .

Inherited by: .

Functions: .
## IField2D

Inherits: IGeometry, IGeometry2D, IProcedural.

Implemented by: .

Inherited by: IScalarField2D, IDistanceField2D, IVector3Field2D, IVector4Field2D.

Functions: .
## IField3D

Inherits: IGeometry, IGeometry3D, IProcedural.

Implemented by: Sphere, Cylinder, Capsule, Cone, ConeSegment, Box3D, Pyramid, Torus, NPrism, Tube, NPyramid, Ellipsoid.

Inherited by: ISurface, IProceduralSurface, IExplicitSurface, IScalarField3D, IDistanceField3D, IVector2Field3D, IVector3Field3D, IVector4Field3D, IImplicitSurface, ISolid.

Functions: .
## IGeometry

Inherits: .

Implemented by: Triangle2D, Quad2D, Line2D, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Box2D, Line3D, Triangle3D, Quad3D, Sphere, Cylinder, Capsule, Cone, ConeSegment, Box3D, Pyramid, Torus, NPrism, Tube, NPyramid, Ellipsoid, CubicBezier2D, QuadraticBezier2D, LinearFunction2D, QuadraticFunction2D, CubicFunction2D, Parabola, Circle, Lissajous, ButterflyCurve, Spiral, Sin, Cos, CubicBezier3D, QuadraticBezier3D, TorusKnot, Helix, TrefoilKnot, FigureEightKnot, LineMesh3D, TriangleMesh3D, QuadMesh3D, PolyLine2D, PolyLine3D, PointArray2D, PointArray3D, LineArray2D, LineArray3D, TriangleArray2D, TriangleArray3D, QuadArray2D, QuadArray3D, QuadGrid3D.

Inherited by: IGeometry2D, IGeometry3D, IShape2D, IShape3D, IOpenShape2D, IClosedShape2D, IOpenShape3D, IClosedShape3D, ICurve2D, IClosedCurve2D, IOpenCurve2D, ICurve3D, IClosedCurve3D, IOpenCurve3D, ISurface, IProceduralSurface, IExplicitSurface, IField2D, IField3D, IScalarField2D, IDistanceField2D, IScalarField3D, IDistanceField3D, IVector3Field2D, IVector4Field2D, IVector2Field3D, IVector3Field3D, IVector4Field3D, IImplicitSurface, IImplicitCurve2D, IImplicitVolume, IPolyLine2D, IPolyLine3D, IClosedPolyLine2D, IClosedPolyLine3D, IPolygon2D, IPolygon3D, ISolid, IPointGeometry2D, IPointGeometry3D, IPointArray2D, IPointArray3D, ILineArray2D, ILineArray3D, ITriangleArray2D, ITriangleArray3D, IQuadArray2D, IQuadArray3D, IPrimitiveGeometry2D, IPrimitiveGeometry3D, IPrimitiveArray2D, IPrimitiveArray3D, IIndexedGeometry2D, IIndexedGeometry3D, IIndexedPrimitives2D, IIndexedPrimitives3D, ILineMesh2D, ITriangleMesh2D, IQuadMesh2D, ILineMesh3D, ITriangleMesh3D, IQuadMesh3D, IQuadGrid3D.

Functions: .
## IGeometry2D

Inherits: IGeometry.

Implemented by: Triangle2D, Quad2D, Line2D, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Box2D, CubicBezier2D, QuadraticBezier2D, LinearFunction2D, QuadraticFunction2D, CubicFunction2D, Parabola, Circle, Lissajous, ButterflyCurve, Spiral, Sin, Cos, PolyLine2D, PointArray2D, LineArray2D, TriangleArray2D.

Inherited by: IShape2D, IOpenShape2D, IClosedShape2D, ICurve2D, IClosedCurve2D, IOpenCurve2D, IField2D, IScalarField2D, IDistanceField2D, IVector3Field2D, IVector4Field2D, IImplicitCurve2D, IPolyLine2D, IClosedPolyLine2D, IPolygon2D, IPointGeometry2D, IPointArray2D, ILineArray2D, ITriangleArray2D, IPrimitiveGeometry2D, IPrimitiveArray2D, IIndexedGeometry2D, IIndexedPrimitives2D, ILineMesh2D, ITriangleMesh2D, IQuadMesh2D.

Functions: .
## IGeometry3D

Inherits: IGeometry.

Implemented by: Line2D, Line3D, Triangle3D, Quad3D, Sphere, Cylinder, Capsule, Cone, ConeSegment, Box3D, Pyramid, Torus, NPrism, Tube, NPyramid, Ellipsoid, CubicBezier3D, QuadraticBezier3D, TorusKnot, Helix, TrefoilKnot, FigureEightKnot, LineMesh3D, TriangleMesh3D, QuadMesh3D, PolyLine3D, PointArray3D, LineArray3D, TriangleArray3D, QuadArray2D, QuadArray3D, QuadGrid3D.

Inherited by: IShape3D, IOpenShape3D, IClosedShape3D, ICurve3D, IClosedCurve3D, IOpenCurve3D, ISurface, IProceduralSurface, IExplicitSurface, IField3D, IScalarField3D, IDistanceField3D, IVector2Field3D, IVector3Field3D, IVector4Field3D, IImplicitSurface, IImplicitVolume, IPolyLine3D, IClosedPolyLine3D, IPolygon3D, ISolid, IPointGeometry3D, IPointArray3D, ILineArray3D, ITriangleArray3D, IQuadArray2D, IQuadArray3D, IPrimitiveGeometry3D, IPrimitiveArray3D, IIndexedGeometry3D, IIndexedPrimitives3D, ILineMesh3D, ITriangleMesh3D, IQuadMesh3D, IQuadGrid3D.

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

Inherits: IDistanceField3D, IField3D, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IImplicitProcedural, IProcedural, IProcedural, IScalarField3D, ISurface.

Implemented by: .

Inherited by: .

Functions: .
## IImplicitVolume

Inherits: IGeometry, IGeometry3D, IImplicitProcedural, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IIndexedGeometry

Inherits: .

Implemented by: LineMesh3D, TriangleMesh3D, QuadMesh3D, QuadGrid3D.

Inherited by: IIndexedGeometry2D, IIndexedGeometry3D, IIndexedPrimitives2D, IIndexedPrimitives3D, ILineMesh2D, ITriangleMesh2D, IQuadMesh2D, ILineMesh3D, ITriangleMesh3D, IQuadMesh3D, IQuadGrid3D.

Functions: Indices.







## IIndexedGeometry2D

Inherits: IGeometry, IGeometry2D, IIndexedGeometry, IPointGeometry2D, IPrimitiveGeometry, IPrimitiveGeometry2D.

Implemented by: .

Inherited by: IIndexedPrimitives2D, ILineMesh2D, ITriangleMesh2D, IQuadMesh2D.

Functions: .
## IIndexedGeometry3D

Inherits: IGeometry, IGeometry3D, IIndexedGeometry, IPointGeometry3D, IPrimitiveGeometry, IPrimitiveGeometry3D.

Implemented by: LineMesh3D, TriangleMesh3D, QuadMesh3D, QuadGrid3D.

Inherited by: IIndexedPrimitives3D, ILineMesh3D, ITriangleMesh3D, IQuadMesh3D, IQuadGrid3D.

Functions: .
## IIndexedPrimitives2D

Inherits: IGeometry, IGeometry, IGeometry2D, IGeometry2D, IIndexedGeometry, IIndexedGeometry2D, IPointGeometry2D, IPointGeometry2D, IPrimitiveArray2D, IPrimitiveGeometry, IPrimitiveGeometry, IPrimitiveGeometry2D, IPrimitiveGeometry2D.

Implemented by: .

Inherited by: ILineMesh2D, ITriangleMesh2D, IQuadMesh2D.

Functions: .
## IIndexedPrimitives3D

Inherits: IGeometry, IGeometry, IGeometry3D, IGeometry3D, IIndexedGeometry, IIndexedGeometry3D, IPointGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry, IPrimitiveGeometry3D, IPrimitiveGeometry3D.

Implemented by: LineMesh3D, TriangleMesh3D, QuadMesh3D, QuadGrid3D.

Inherited by: ILineMesh3D, ITriangleMesh3D, IQuadMesh3D, IQuadGrid3D.

Functions: .
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







## ILineArray2D

Inherits: IGeometry, IGeometry2D, ILinePrimitives, IPointGeometry2D, IPrimitiveArray2D, IPrimitiveGeometry, IPrimitiveGeometry2D.

Implemented by: LineArray2D.

Inherited by: .

Functions: .
## ILineArray3D

Inherits: IGeometry, IGeometry3D, ILinePrimitives, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D.

Implemented by: LineArray3D.

Inherited by: .

Functions: .
## ILineMesh2D

Inherits: IGeometry, IGeometry, IGeometry2D, IGeometry2D, IIndexedGeometry, IIndexedGeometry2D, IIndexedPrimitives2D, ILinePrimitives, IPointGeometry2D, IPointGeometry2D, IPrimitiveArray2D, IPrimitiveGeometry, IPrimitiveGeometry, IPrimitiveGeometry2D, IPrimitiveGeometry2D.

Implemented by: .

Inherited by: .

Functions: .
## ILineMesh3D

Inherits: IGeometry, IGeometry, IGeometry3D, IGeometry3D, IIndexedGeometry, IIndexedGeometry3D, IIndexedPrimitives3D, ILinePrimitives, IPointGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry, IPrimitiveGeometry3D, IPrimitiveGeometry3D.

Implemented by: LineMesh3D.

Inherited by: .

Functions: .
## ILinePrimitives

Inherits: .

Implemented by: LineMesh3D, LineArray2D, LineArray3D.

Inherited by: ILineArray2D, ILineArray3D, ILineMesh2D, ILineMesh3D.

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

Implemented by: Triangle2D, Quad2D, Line2D, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Line3D, Triangle3D, Quad3D, CubicBezier2D, QuadraticBezier2D, LinearFunction2D, QuadraticFunction2D, CubicFunction2D, Parabola, Circle, Lissajous, ButterflyCurve, Spiral, Sin, Cos, CubicBezier3D, QuadraticBezier3D, TorusKnot, Helix, TrefoilKnot, FigureEightKnot, PolyLine2D, PolyLine3D.

Inherited by: IOpenShape, IClosedShape, IOpenShape2D, IClosedShape2D, IOpenShape3D, IClosedShape3D, ICurve, ICurve1D, ICurve2D, IClosedCurve2D, IOpenCurve2D, ICurve3D, IClosedCurve3D, IOpenCurve3D, IPolyLine2D, IPolyLine3D, IClosedPolyLine2D, IClosedPolyLine3D, IPolygon2D, IPolygon3D.

Functions: Closed.






## IOpenCurve2D

Inherits: ICurve, ICurve2D, IGeometry, IGeometry, IGeometry2D, IGeometry2D, IOpenClosedShape, IOpenClosedShape, IOpenShape, IOpenShape2D, IProcedural.

Implemented by: CubicBezier2D, QuadraticBezier2D, LinearFunction2D, QuadraticFunction2D, CubicFunction2D, Parabola.

Inherited by: .

Functions: .
## IOpenCurve3D

Inherits: ICurve, ICurve3D, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IOpenClosedShape, IOpenClosedShape, IOpenShape, IOpenShape3D, IProcedural.

Implemented by: .

Inherited by: .

Functions: .
## IOpenShape

Inherits: IOpenClosedShape.

Implemented by: Line2D, Arc, Line3D, CubicBezier2D, QuadraticBezier2D, LinearFunction2D, QuadraticFunction2D, CubicFunction2D, Parabola.

Inherited by: IOpenShape2D, IOpenShape3D, IOpenCurve2D, IOpenCurve3D.

Functions: .
## IOpenShape2D

Inherits: IGeometry, IGeometry2D, IOpenClosedShape, IOpenShape.

Implemented by: Arc, CubicBezier2D, QuadraticBezier2D, LinearFunction2D, QuadraticFunction2D, CubicFunction2D, Parabola.

Inherited by: IOpenCurve2D.

Functions: .
## IOpenShape3D

Inherits: IGeometry, IGeometry3D, IOpenClosedShape, IOpenShape.

Implemented by: Line2D, Line3D.

Inherited by: IOpenCurve3D.

Functions: .
## IOrderable

Inherits: IAny, IEquatable.

Implemented by: Number, Integer, String, Boolean, Character, Unit, Probability, Angle, Length, Mass, Temperature, Time.

Inherited by: INumberLike, IReal, IWholeNumber, IMeasure.

Functions: LessThanOrEquals.
















## IPointArray2D

Inherits: IGeometry, IGeometry2D, IPointGeometry2D.

Implemented by: CubicBezier2D, QuadraticBezier2D, PointArray2D.

Inherited by: .

Functions: .
## IPointArray3D

Inherits: IGeometry, IGeometry3D, IPointGeometry3D.

Implemented by: CubicBezier3D, QuadraticBezier3D, PointArray3D.

Inherited by: .

Functions: .
## IPointGeometry2D

Inherits: IGeometry, IGeometry2D.

Implemented by: Triangle2D, Quad2D, Line2D, Rect2D, RegularPolygon, CubicBezier2D, QuadraticBezier2D, PolyLine2D, PointArray2D, LineArray2D, TriangleArray2D.

Inherited by: IPolyLine2D, IClosedPolyLine2D, IPolygon2D, IPointArray2D, ILineArray2D, ITriangleArray2D, IPrimitiveGeometry2D, IPrimitiveArray2D, IIndexedGeometry2D, IIndexedPrimitives2D, ILineMesh2D, ITriangleMesh2D, IQuadMesh2D.

Functions: Points.






## IPointGeometry3D

Inherits: IGeometry, IGeometry3D.

Implemented by: Line3D, Triangle3D, Quad3D, CubicBezier3D, QuadraticBezier3D, LineMesh3D, TriangleMesh3D, QuadMesh3D, PolyLine3D, PointArray3D, LineArray3D, TriangleArray3D, QuadArray2D, QuadArray3D, QuadGrid3D.

Inherited by: IPolyLine3D, IClosedPolyLine3D, IPolygon3D, IPointArray3D, ILineArray3D, ITriangleArray3D, IQuadArray2D, IQuadArray3D, IPrimitiveGeometry3D, IPrimitiveArray3D, IIndexedGeometry3D, IIndexedPrimitives3D, ILineMesh3D, ITriangleMesh3D, IQuadMesh3D, IQuadGrid3D.

Functions: Points.






## IPolygon2D

Inherits: IClosedPolyLine2D, IClosedShape, IClosedShape2D, ICurve, ICurve2D, IGeometry, IGeometry, IGeometry, IGeometry2D, IGeometry2D, IGeometry2D, IOpenClosedShape, IOpenClosedShape, IOpenClosedShape, IPointGeometry2D, IPolyLine2D, IProcedural.

Implemented by: Triangle2D, Quad2D, Rect2D, RegularPolygon.

Inherited by: .

Functions: .
## IPolygon3D

Inherits: IClosedPolyLine3D, IClosedShape, IClosedShape3D, ICurve, ICurve3D, IGeometry, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IGeometry3D, IOpenClosedShape, IOpenClosedShape, IOpenClosedShape, IPointGeometry3D, IPolyLine3D, IProcedural.

Implemented by: Triangle3D, Quad3D.

Inherited by: .

Functions: .
## IPolyLine2D

Inherits: ICurve, ICurve2D, IGeometry, IGeometry, IGeometry2D, IGeometry2D, IOpenClosedShape, IOpenClosedShape, IPointGeometry2D, IProcedural.

Implemented by: Triangle2D, Quad2D, Line2D, Rect2D, RegularPolygon, PolyLine2D.

Inherited by: IClosedPolyLine2D, IPolygon2D.

Functions: .
## IPolyLine3D

Inherits: ICurve, ICurve3D, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IOpenClosedShape, IOpenClosedShape, IPointGeometry3D, IProcedural.

Implemented by: Line3D, Triangle3D, Quad3D, PolyLine3D.

Inherited by: IClosedPolyLine3D, IPolygon3D.

Functions: .
## IPrimitiveArray2D

Inherits: IGeometry, IGeometry2D, IPointGeometry2D, IPrimitiveGeometry, IPrimitiveGeometry2D.

Implemented by: LineArray2D, TriangleArray2D.

Inherited by: ILineArray2D, ITriangleArray2D, IIndexedPrimitives2D, ILineMesh2D, ITriangleMesh2D, IQuadMesh2D.

Functions: Primitives.










## IPrimitiveArray3D

Inherits: IGeometry, IGeometry3D, IPointGeometry3D, IPrimitiveGeometry, IPrimitiveGeometry3D.

Implemented by: LineMesh3D, TriangleMesh3D, QuadMesh3D, LineArray3D, TriangleArray3D, QuadArray2D, QuadArray3D, QuadGrid3D.

Inherited by: ILineArray3D, ITriangleArray3D, IQuadArray2D, IQuadArray3D, IIndexedPrimitives3D, ILineMesh3D, ITriangleMesh3D, IQuadMesh3D, IQuadGrid3D.

Functions: Primitives.










## IPrimitiveGeometry

Inherits: .

Implemented by: LineMesh3D, TriangleMesh3D, QuadMesh3D, LineArray2D, LineArray3D, TriangleArray2D, TriangleArray3D, QuadArray2D, QuadArray3D, QuadGrid3D.

Inherited by: ILineArray2D, ILineArray3D, ITriangleArray2D, ITriangleArray3D, IQuadArray2D, IQuadArray3D, IPrimitiveGeometry2D, IPrimitiveGeometry3D, IPrimitiveArray2D, IPrimitiveArray3D, IIndexedGeometry2D, IIndexedGeometry3D, IIndexedPrimitives2D, IIndexedPrimitives3D, ILineMesh2D, ITriangleMesh2D, IQuadMesh2D, ILineMesh3D, ITriangleMesh3D, IQuadMesh3D, IQuadGrid3D.

Functions: PrimitiveSize.













## IPrimitiveGeometry2D

Inherits: IGeometry, IGeometry2D, IPointGeometry2D, IPrimitiveGeometry.

Implemented by: LineArray2D, TriangleArray2D.

Inherited by: ILineArray2D, ITriangleArray2D, IPrimitiveArray2D, IIndexedGeometry2D, IIndexedPrimitives2D, ILineMesh2D, ITriangleMesh2D, IQuadMesh2D.

Functions: .
## IPrimitiveGeometry3D

Inherits: IGeometry, IGeometry3D, IPointGeometry3D, IPrimitiveGeometry.

Implemented by: LineMesh3D, TriangleMesh3D, QuadMesh3D, LineArray3D, TriangleArray3D, QuadArray2D, QuadArray3D, QuadGrid3D.

Inherited by: ILineArray3D, ITriangleArray3D, IQuadArray2D, IQuadArray3D, IPrimitiveArray3D, IIndexedGeometry3D, IIndexedPrimitives3D, ILineMesh3D, ITriangleMesh3D, IQuadMesh3D, IQuadGrid3D.

Functions: .
## IProcedural

Inherits: .

Implemented by: Triangle2D, Quad2D, Line2D, Rect2D, Ellipse, RegularPolygon, Line3D, Triangle3D, Quad3D, Sphere, Cylinder, Capsule, Cone, ConeSegment, Box3D, Pyramid, Torus, NPrism, Tube, NPyramid, Ellipsoid, CubicBezier2D, QuadraticBezier2D, LinearFunction2D, QuadraticFunction2D, CubicFunction2D, Parabola, Circle, Lissajous, ButterflyCurve, Spiral, Sin, Cos, CubicBezier3D, QuadraticBezier3D, TorusKnot, Helix, TrefoilKnot, FigureEightKnot, PolyLine2D, PolyLine3D.

Inherited by: ICurve, ICurve1D, ICurve2D, IClosedCurve2D, IOpenCurve2D, ICurve3D, IClosedCurve3D, IOpenCurve3D, ISurface, IProceduralSurface, IExplicitSurface, IField2D, IField3D, IScalarField2D, IDistanceField2D, IScalarField3D, IDistanceField3D, IVector3Field2D, IVector4Field2D, IVector2Field3D, IVector3Field3D, IVector4Field3D, IImplicitProcedural, IImplicitSurface, IImplicitCurve2D, IImplicitVolume, IPolyLine2D, IPolyLine3D, IClosedPolyLine2D, IClosedPolyLine3D, IPolygon2D, IPolygon3D, ISolid.

Functions: Eval.




## IProceduralSurface

Inherits: IDistanceField3D, IField3D, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IProcedural, IProcedural, IScalarField3D, ISurface.

Implemented by: .

Inherited by: .

Functions: PeriodicX, PeriodicY.




















## IQuadArray2D

Inherits: IGeometry, IGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D, IQuadPrimitives.

Implemented by: QuadArray2D.

Inherited by: .

Functions: .
## IQuadArray3D

Inherits: IGeometry, IGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D, IQuadPrimitives.

Implemented by: QuadArray3D.

Inherited by: .

Functions: .
## IQuadGrid3D

Inherits: IGeometry, IGeometry, IGeometry3D, IGeometry3D, IIndexedGeometry, IIndexedGeometry3D, IIndexedPrimitives3D, IPointGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry, IPrimitiveGeometry3D, IPrimitiveGeometry3D, IQuadMesh3D, IQuadPrimitives.

Implemented by: QuadGrid3D.

Inherited by: .

Functions: ClosedX, ClosedY, PointGrid.



























## IQuadMesh2D

Inherits: IGeometry, IGeometry, IGeometry2D, IGeometry2D, IIndexedGeometry, IIndexedGeometry2D, IIndexedPrimitives2D, IPointGeometry2D, IPointGeometry2D, IPrimitiveArray2D, IPrimitiveGeometry, IPrimitiveGeometry, IPrimitiveGeometry2D, IPrimitiveGeometry2D, IQuadPrimitives.

Implemented by: .

Inherited by: .

Functions: .
## IQuadMesh3D

Inherits: IGeometry, IGeometry, IGeometry3D, IGeometry3D, IIndexedGeometry, IIndexedGeometry3D, IIndexedPrimitives3D, IPointGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry, IPrimitiveGeometry3D, IPrimitiveGeometry3D, IQuadPrimitives.

Implemented by: QuadMesh3D, QuadGrid3D.

Inherited by: IQuadGrid3D.

Functions: .
## IQuadPrimitives

Inherits: .

Implemented by: QuadMesh3D, QuadArray2D, QuadArray3D, QuadGrid3D.

Inherited by: IQuadArray2D, IQuadArray3D, IQuadMesh2D, IQuadMesh3D, IQuadGrid3D.

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

Inherited by: IDistanceField2D.

Functions: .
## IScalarField3D

Inherits: IField3D, IGeometry, IGeometry3D, IProcedural.

Implemented by: Sphere, Cylinder, Capsule, Cone, ConeSegment, Box3D, Pyramid, Torus, NPrism, Tube, NPyramid, Ellipsoid.

Inherited by: ISurface, IProceduralSurface, IExplicitSurface, IDistanceField3D, IImplicitSurface, ISolid.

Functions: .
## IShape2D

Inherits: IGeometry, IGeometry2D.

Implemented by: Box2D.

Inherited by: .

Functions: .
## IShape3D

Inherits: IGeometry, IGeometry3D.

Implemented by: Sphere, Capsule, Cone, ConeSegment, Box3D, Pyramid, Torus, NPrism, Tube, NPyramid, Ellipsoid.

Inherited by: ISolid.

Functions: .
## ISolid

Inherits: IDistanceField3D, IDistanceField3D, IField3D, IField3D, IGeometry, IGeometry, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IGeometry3D, IGeometry3D, IProcedural, IProcedural, IScalarField3D, IScalarField3D, IShape3D, ISurface.

Implemented by: Sphere, Capsule, Cone, ConeSegment, Box3D, Pyramid, Torus, NPrism, Tube, NPyramid, Ellipsoid.

Inherited by: .

Functions: .
## ISurface

Inherits: IDistanceField3D, IField3D, IGeometry, IGeometry, IGeometry3D, IGeometry3D, IProcedural, IScalarField3D.

Implemented by: Sphere, Cylinder, Capsule, Cone, ConeSegment, Box3D, Pyramid, Torus, NPrism, Tube, NPyramid, Ellipsoid.

Inherited by: IProceduralSurface, IExplicitSurface, IImplicitSurface, ISolid.

Functions: .
## ITransform3D

Inherits: .

Implemented by: Transform3D, Pose3D, Frame3D, Quaternion, AxisAngle, EulerAngles, Rotation3D.

Inherited by: .

Functions: Inverse, Transform, TransformNormal.



































## ITriangleArray2D

Inherits: IGeometry, IGeometry2D, IPointGeometry2D, IPrimitiveArray2D, IPrimitiveGeometry, IPrimitiveGeometry2D, ITrianglePrimitives.

Implemented by: TriangleArray2D.

Inherited by: .

Functions: .
## ITriangleArray3D

Inherits: IGeometry, IGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry3D, ITrianglePrimitives.

Implemented by: TriangleArray3D.

Inherited by: .

Functions: .
## ITriangleMesh2D

Inherits: IGeometry, IGeometry, IGeometry2D, IGeometry2D, IIndexedGeometry, IIndexedGeometry2D, IIndexedPrimitives2D, IPointGeometry2D, IPointGeometry2D, IPrimitiveArray2D, IPrimitiveGeometry, IPrimitiveGeometry, IPrimitiveGeometry2D, IPrimitiveGeometry2D, ITrianglePrimitives.

Implemented by: .

Inherited by: .

Functions: .
## ITriangleMesh3D

Inherits: IGeometry, IGeometry, IGeometry3D, IGeometry3D, IIndexedGeometry, IIndexedGeometry3D, IIndexedPrimitives3D, IPointGeometry3D, IPointGeometry3D, IPrimitiveArray3D, IPrimitiveGeometry, IPrimitiveGeometry, IPrimitiveGeometry3D, IPrimitiveGeometry3D, ITrianglePrimitives.

Implemented by: TriangleMesh3D.

Inherited by: .

Functions: .
## ITrianglePrimitives

Inherits: .

Implemented by: TriangleMesh3D, TriangleArray2D, TriangleArray3D.

Inherited by: ITriangleArray2D, ITriangleArray3D, ITriangleMesh2D, ITriangleMesh3D.

Functions: .
## IValue

Inherits: IAny, IAny, IEquatable.

Implemented by: Number, Integer, String, Boolean, Character, Unit, Probability, Complex, Integer2, Integer3, Integer4, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4, Transform2D, Pose2D, Bounds2D, Ray2D, Plane, Bounds3D, Ray3D, Transform3D, Pose3D, Frame3D, Quaternion, AxisAngle, EulerAngles, Rotation3D.

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
