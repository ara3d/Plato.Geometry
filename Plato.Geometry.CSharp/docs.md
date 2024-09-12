# Concepts

Concepts are implemented as interfaces. Functions defined on a concept are available on every type that implements the concept.

## Concept AdditiveArithmetic

Inherits: AdditiveArithmetic, AdditiveInverse.

Implemented by: Measure, Probability, Angle, Length, Mass, Temperature, Time.

Functions: Negative.

## Concept AdditiveInverse

Inherits: AdditiveInverse.

Implemented by: Real, Measure, Vector, WholeNumber, AdditiveArithmetic, Arithmetic, Number, Integer, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Functions: .

## Concept Any

Inherits: Any.

Implemented by: Value, Numerical, Real, Measure, Vector, WholeNumber, Coordinate, Interval, Transform2D, Pose2D, Bounds2D, Ray2D, Triangle2D, Quad2D, Sphere, Plane, Transform3D, Pose3D, Bounds3D, Ray3D, Triangle3D, Quad3D, Quaternion, AxisAngle, EulerAngles, Rotation3D, Orientation3D, Line4D, Number, Integer, Character, Unit, Probability, Complex, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Size2D, Size3D, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4.

Functions: .

## Concept Arithmetic

Inherits: AdditiveInverse, Arithmetic, MultiplicativeInverse.

Implemented by: Real, Vector, WholeNumber, Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Functions: Negative, Reciprocal.

## Concept Array

Inherits: Array.

Implemented by: LazyArray, LazyArray2D, LazyArray3D, Vector, Array2D, Array3D, BezierPatch, Grid2D, QuadGrid, Triangle2D, Quad2D, Line2D, Line3D, Triangle3D, Quad3D, CubicBezier2D, CubicBezier3D, QuadraticBezier2D, QuadraticBezier3D, Line4D, String, Complex, Integer2, Integer3, Integer4, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4.

Functions: .

## Concept Array2D

Inherits: Array, Array2D.

Implemented by: LazyArray2D, BezierPatch, Grid2D, QuadGrid.

Functions: At, Count.

## Concept Array3D

Inherits: Array, Array3D.

Implemented by: LazyArray3D.

Functions: At, Count.

## Concept BezierPatch

Inherits: Array, Array2D, BezierPatch, Geometry, Geometry, Geometry3D, Geometry3D, Points3D, Surface.

Implemented by: .

Functions: At, At, ColumnCount, Count, Points, RowCount.

## Concept BooleanOperations

Inherits: BooleanOperations.

Implemented by: Boolean.

Functions: .

## Concept Bounded2D

Inherits: Bounded2D.

Implemented by: .

Functions: .

## Concept Bounded3D

Inherits: Bounded3D.

Implemented by: .

Functions: .

## Concept ClosedPolyLine2D

Inherits: ClosedPolyLine2D, ClosedShape2D, Geometry, Geometry, Geometry2D, Geometry2D, OpenClosedShape, OpenClosedShape, Points2D, PolyLine2D.

Implemented by: .

Functions: Closed, Points.

## Concept ClosedPolyLine3D

Inherits: ClosedPolyLine3D, Geometry, Geometry3D, OpenClosedShape, Points3D, PolyLine3D.

Implemented by: .

Functions: Closed, Points.

## Concept ClosedShape2D

Inherits: ClosedShape2D, Geometry, Geometry2D, OpenClosedShape.

Implemented by: ClosedPolyLine2D, Circle, Lens, Ring, Sector, Chord, Segment.

Functions: Closed.

## Concept ClosedShape3D

Inherits: ClosedShape3D, Geometry, Geometry3D, OpenClosedShape.

Implemented by: .

Functions: Closed.

## Concept Comparable

Inherits: Comparable.

Implemented by: Real, Measure, WholeNumber, Number, Integer, String, Unit, Probability, Angle, Length, Mass, Temperature, Time.

Functions: .

## Concept ConvexPolyhedron

Inherits: ConvexPolyhedron, Geometry, Geometry, Geometry3D, Geometry3D, Points3D, Polyhedron, Surface.

Implemented by: .

Functions: Faces, Points.

## Concept Coordinate

Inherits: Any, Coordinate, Equatable, Value.

Implemented by: Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, DateTime.

Functions: Equals, FieldNames, FieldValues, NotEquals, TypeName.

## Concept Curve

Inherits: Curve, OpenClosedShape, Procedural.

Implemented by: Curve1D, Curve2D, Curve3D, Ellipse.

Functions: Closed, Eval.

## Concept Curve1D

Inherits: Curve, Curve1D, OpenClosedShape, Procedural.

Implemented by: .

Functions: Closed, Eval.

## Concept Curve2D

Inherits: Curve, Curve2D, Geometry, Geometry2D, OpenClosedShape, Procedural.

Implemented by: Ellipse.

Functions: Closed, Eval.

## Concept Curve3D

Inherits: Curve, Curve3D, Geometry, Geometry3D, OpenClosedShape, Procedural.

Implemented by: .

Functions: Closed, Eval.

## Concept Deformable2D

Inherits: Deformable2D.

Implemented by: .

Functions: .

## Concept Deformable3D

Inherits: Deformable3D, Transformable3D.

Implemented by: .

Functions: Transform.

## Concept DistanceField

Inherits: DistanceField, Procedural.

Implemented by: .

Functions: Eval.

## Concept DistanceField2D

Inherits: DistanceField2D, Field2D, Geometry, Geometry2D, Procedural, ScalarField2D.

Implemented by: .

Functions: Eval.

## Concept DistanceField3D

Inherits: DistanceField3D, Field3D, Geometry, Geometry3D, Procedural, ScalarField3D.

Implemented by: .

Functions: Eval.

## Concept Equatable

Inherits: Equatable.

Implemented by: Value, Numerical, Real, Measure, Vector, WholeNumber, Coordinate, Interval, Transform2D, Pose2D, Bounds2D, Ray2D, Triangle2D, Quad2D, Sphere, Plane, Transform3D, Pose3D, Bounds3D, Ray3D, Triangle3D, Quad3D, Quaternion, AxisAngle, EulerAngles, Rotation3D, Orientation3D, Line4D, Number, Integer, String, Character, Unit, Probability, Complex, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Size2D, Size3D, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4.

Functions: .

## Concept ExplicitSurface

Inherits: ExplicitSurface, Geometry, Geometry3D, Procedural, Surface.

Implemented by: .

Functions: Eval.

## Concept Field2D

Inherits: Field2D, Geometry, Geometry2D, Procedural.

Implemented by: ScalarField2D, DistanceField2D, Vector3Field2D, Vector4Field2D.

Functions: Eval.

## Concept Field3D

Inherits: Field3D, Geometry, Geometry3D, Procedural.

Implemented by: ScalarField3D, DistanceField3D, Vector2Field3D, Vector3Field3D, Vector4Field3D.

Functions: Eval.

## Concept Geometry

Inherits: Geometry.

Implemented by: Geometry2D, Geometry3D, Shape2D, Shape3D, OpenShape2D, ClosedShape2D, OpenShape3D, ClosedShape3D, Curve2D, Curve3D, Surface, ParametricSurface, ExplicitSurface, Field2D, Field3D, ScalarField2D, ScalarField3D, DistanceField2D, DistanceField3D, Vector3Field2D, Vector4Field2D, Vector2Field3D, Vector3Field3D, Vector4Field3D, ImplicitSurface, ImplicitCurve2D, ImplicitVolume, Points2D, Points3D, BezierPatch, Polyhedron, ConvexPolyhedron, SolidPolyhedron, Mesh, PolyLine2D, PolyLine3D, ClosedPolyLine2D, ClosedPolyLine3D, Polygon2D, Polygon3D, Line2D, Circle, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Box2D, Line3D, Capsule, Cylinder, Cone, Tube, ConeSegment, Box3D, TriMesh, QuadMesh.

Functions: .

## Concept Geometry2D

Inherits: Geometry, Geometry2D.

Implemented by: Shape2D, OpenShape2D, ClosedShape2D, Curve2D, Field2D, ScalarField2D, DistanceField2D, Vector3Field2D, Vector4Field2D, ImplicitCurve2D, Points2D, PolyLine2D, ClosedPolyLine2D, Polygon2D, Line2D, Circle, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Box2D.

Functions: .

## Concept Geometry3D

Inherits: Geometry, Geometry3D.

Implemented by: Shape3D, OpenShape3D, ClosedShape3D, Curve3D, Surface, ParametricSurface, ExplicitSurface, Field3D, ScalarField3D, DistanceField3D, Vector2Field3D, Vector3Field3D, Vector4Field3D, ImplicitSurface, ImplicitVolume, Points3D, BezierPatch, Polyhedron, ConvexPolyhedron, SolidPolyhedron, Mesh, PolyLine3D, ClosedPolyLine3D, Polygon3D, Line3D, Capsule, Cylinder, Cone, Tube, ConeSegment, Box3D, TriMesh, QuadMesh.

Functions: .

## Concept Grid2D

Inherits: Array, Array2D, Grid2D.

Implemented by: .

Functions: At, At, ColumnCount, Count, RowCount.

## Concept ImplicitCurve2D

Inherits: Geometry, Geometry2D, ImplicitCurve2D, ImplicitProcedural.

Implemented by: .

Functions: Eval.

## Concept ImplicitProcedural

Inherits: ImplicitProcedural.

Implemented by: ImplicitSurface, ImplicitCurve2D, ImplicitVolume.

Functions: .

## Concept ImplicitSurface

Inherits: Geometry, Geometry3D, ImplicitProcedural, ImplicitSurface, Surface.

Implemented by: .

Functions: Eval.

## Concept ImplicitVolume

Inherits: Geometry, Geometry3D, ImplicitProcedural, ImplicitVolume.

Implemented by: .

Functions: Eval.

## Concept Interval

Inherits: Any, Equatable, Equatable, Interval, Value.

Implemented by: Bounds2D, Bounds3D, AnglePair, NumberInterval.

Functions: Equals, FieldNames, FieldValues, NotEquals, TypeName.

## Concept Measure

Inherits: AdditiveArithmetic, AdditiveInverse, Any, Comparable, Equatable, Measure, MultiplicativeArithmetic, Numerical, ScalarArithmetic, Value.

Implemented by: Probability, Angle, Length, Mass, Temperature, Time.

Functions: Add, Compare, Components, Divide, Equals, FieldNames, FieldValues, FromComponents, Modulo, Multiply, Multiply, Negative, NotEquals, Subtract, TypeName.

## Concept Mesh

Inherits: Geometry, Geometry3D, Mesh.

Implemented by: TriMesh, QuadMesh.

Functions: .

## Concept MultiplicativeArithmetic

Inherits: MultiplicativeArithmetic.

Implemented by: Real, Measure, Vector, ScalarArithmetic, Number, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Functions: .

## Concept MultiplicativeInverse

Inherits: MultiplicativeInverse.

Implemented by: Real, Vector, WholeNumber, Arithmetic, Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Functions: .

## Concept Numerical

Inherits: Any, Equatable, Numerical, Value.

Implemented by: Real, Measure, Vector, WholeNumber, Number, Integer, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Functions: Equals, FieldNames, FieldValues, NotEquals, TypeName.

## Concept OpenClosedShape

Inherits: OpenClosedShape.

Implemented by: OpenShape2D, ClosedShape2D, OpenShape3D, ClosedShape3D, Curve, Curve1D, Curve2D, Curve3D, PolyLine2D, PolyLine3D, ClosedPolyLine2D, ClosedPolyLine3D, Polygon2D, Polygon3D, Line2D, Circle, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Line3D.

Functions: .

## Concept OpenShape2D

Inherits: Geometry, Geometry2D, OpenClosedShape, OpenShape2D.

Implemented by: Arc.

Functions: Closed.

## Concept OpenShape3D

Inherits: Geometry, Geometry3D, OpenClosedShape, OpenShape3D.

Implemented by: .

Functions: Closed.

## Concept ParametricSurface

Inherits: Geometry, Geometry3D, ParametricSurface, Procedural, Surface.

Implemented by: .

Functions: Eval.

## Concept Points2D

Inherits: Geometry, Geometry2D, Points2D.

Implemented by: PolyLine2D, ClosedPolyLine2D, Polygon2D, Line2D, Rect2D, RegularPolygon.

Functions: .

## Concept Points3D

Inherits: Geometry, Geometry3D, Points3D.

Implemented by: BezierPatch, Polyhedron, ConvexPolyhedron, SolidPolyhedron, PolyLine3D, ClosedPolyLine3D, Polygon3D, Line3D.

Functions: .

## Concept Polygon2D

Inherits: Geometry, Geometry2D, OpenClosedShape, Points2D, Polygon2D, PolyLine2D.

Implemented by: Rect2D, RegularPolygon.

Functions: Closed, Points.

## Concept Polygon3D

Inherits: Geometry, Geometry3D, OpenClosedShape, Points3D, Polygon3D, PolyLine3D.

Implemented by: .

Functions: Closed, Points.

## Concept PolyhederalFace

Inherits: PolyhederalFace.

Implemented by: .

Functions: .

## Concept Polyhedron

Inherits: Geometry, Geometry, Geometry3D, Geometry3D, Points3D, Polyhedron, Surface.

Implemented by: ConvexPolyhedron, SolidPolyhedron.

Functions: Points.

## Concept PolyLine2D

Inherits: Geometry, Geometry2D, OpenClosedShape, Points2D, PolyLine2D.

Implemented by: ClosedPolyLine2D, Polygon2D, Line2D, Rect2D, RegularPolygon.

Functions: Closed, Points.

## Concept PolyLine3D

Inherits: Geometry, Geometry3D, OpenClosedShape, Points3D, PolyLine3D.

Implemented by: ClosedPolyLine3D, Polygon3D, Line3D.

Functions: Closed, Points.

## Concept Procedural

Inherits: Procedural.

Implemented by: Curve, Curve1D, Curve2D, Curve3D, ParametricSurface, ExplicitSurface, DistanceField, Field2D, Field3D, ScalarField2D, ScalarField3D, DistanceField2D, DistanceField3D, Vector3Field2D, Vector4Field2D, Vector2Field3D, Vector3Field3D, Vector4Field3D, Ellipse.

Functions: .

## Concept QuadGrid

Inherits: Array, Array2D, QuadGrid.

Implemented by: .

Functions: At, At, ColumnCount, Count, RowCount.

## Concept Real

Inherits: AdditiveInverse, Any, Arithmetic, Comparable, Equatable, MultiplicativeArithmetic, MultiplicativeInverse, Numerical, Real, ScalarArithmetic, Value.

Implemented by: Number, Unit.

Functions: Add, Compare, Components, Divide, Divide, Equals, FieldNames, FieldValues, FromComponents, Modulo, Modulo, Multiply, Multiply, Multiply, Negative, NotEquals, Reciprocal, Subtract, TypeName.

## Concept ScalarArithmetic

Inherits: MultiplicativeArithmetic, ScalarArithmetic.

Implemented by: Real, Measure, Vector, Number, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Functions: Divide, Modulo, Multiply, Multiply.

## Concept ScalarField2D

Inherits: Field2D, Geometry, Geometry2D, Procedural, ScalarField2D.

Implemented by: DistanceField2D.

Functions: Eval.

## Concept ScalarField3D

Inherits: Field3D, Geometry, Geometry3D, Procedural, ScalarField3D.

Implemented by: DistanceField3D.

Functions: Eval.

## Concept Shape2D

Inherits: Geometry, Geometry2D, Shape2D.

Implemented by: Box2D.

Functions: .

## Concept Shape3D

Inherits: Geometry, Geometry3D, Shape3D.

Implemented by: Capsule, Cylinder, Cone, Tube, ConeSegment, Box3D.

Functions: .

## Concept SolidPolyhedron

Inherits: Geometry, Geometry, Geometry3D, Geometry3D, Points3D, Polyhedron, SolidPolyhedron, Surface.

Implemented by: .

Functions: Faces, Points.

## Concept Surface

Inherits: Geometry, Geometry3D, Surface.

Implemented by: ParametricSurface, ExplicitSurface, ImplicitSurface, BezierPatch, Polyhedron, ConvexPolyhedron, SolidPolyhedron.

Functions: .

## Concept Transformable2D

Inherits: Transformable2D.

Implemented by: .

Functions: .

## Concept Transformable3D

Inherits: Transformable3D.

Implemented by: Deformable3D.

Functions: .

## Concept Value

Inherits: Any, Equatable, Value.

Implemented by: Numerical, Real, Measure, Vector, WholeNumber, Coordinate, Interval, Transform2D, Pose2D, Bounds2D, Ray2D, Triangle2D, Quad2D, Sphere, Plane, Transform3D, Pose3D, Bounds3D, Ray3D, Triangle3D, Quad3D, Quaternion, AxisAngle, EulerAngles, Rotation3D, Orientation3D, Line4D, Number, Integer, Character, Unit, Probability, Complex, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Size2D, Size3D, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4.

Functions: Equals, FieldNames, FieldValues, NotEquals, TypeName.

## Concept Vector

Inherits: AdditiveInverse, Any, Arithmetic, Array, Equatable, MultiplicativeArithmetic, MultiplicativeInverse, Numerical, ScalarArithmetic, Value, Vector.

Implemented by: Complex, Vector2D, Vector3D, Vector4D.

Functions: Add, At, Components, Count, Divide, Divide, Equals, FieldNames, FieldValues, FromComponents, Modulo, Modulo, Multiply, Multiply, Multiply, Negative, NotEquals, Reciprocal, Subtract, TypeName.

## Concept Vector2Field3D

Inherits: Field3D, Geometry, Geometry3D, Procedural, Vector2Field3D.

Implemented by: .

Functions: Eval.

## Concept Vector3Field2D

Inherits: Field2D, Geometry, Geometry2D, Procedural, Vector3Field2D.

Implemented by: .

Functions: Eval.

## Concept Vector3Field3D

Inherits: Field3D, Geometry, Geometry3D, Procedural, Vector3Field3D.

Implemented by: .

Functions: Eval.

## Concept Vector4Field2D

Inherits: Field2D, Geometry, Geometry2D, Procedural, Vector4Field2D.

Implemented by: .

Functions: Eval.

## Concept Vector4Field3D

Inherits: Field3D, Geometry, Geometry3D, Procedural, Vector4Field3D.

Implemented by: .

Functions: Eval.

## Concept WholeNumber

Inherits: AdditiveInverse, Any, Arithmetic, Comparable, Equatable, MultiplicativeInverse, Numerical, Value, WholeNumber.

Implemented by: Integer.

Functions: Add, Compare, Components, Divide, Equals, FieldNames, FieldValues, FromComponents, Modulo, Multiply, Negative, NotEquals, Reciprocal, Subtract, TypeName.

# Types

Types are implemented as structs.
## Type Angle

Fields: Radians:ConcreteType:Number.

Implements: AdditiveArithmetic, AdditiveInverse, Any, Comparable, Equatable, Measure, MultiplicativeArithmetic, Numerical, ScalarArithmetic, Value.

## Type AnglePair

Fields: Max:ConcreteType:Angle, Min:ConcreteType:Angle.

Implements: Any, Equatable, Interval, Value.

## Type Arc

Fields: Angles:ConcreteType:AnglePair, Circle:ConcreteType:Circle.

Implements: Geometry, Geometry2D, OpenClosedShape, OpenShape2D.

## Type AxisAngle

Fields: Angle:ConcreteType:Angle, Axis:ConcreteType:Vector3D.

Implements: Any, Equatable, Value.

## Type Boolean

Fields: .

Implements: BooleanOperations.

## Type Bounds2D

Fields: Max:ConcreteType:Vector2D, Min:ConcreteType:Vector2D.

Implements: Any, Equatable, Interval, Value.

## Type Bounds3D

Fields: Max:ConcreteType:Vector3D, Min:ConcreteType:Vector3D.

Implements: Any, Equatable, Interval, Value.

## Type Box2D

Fields: Center:ConcreteType:Vector2D, Extent:ConcreteType:Size2D, Rotation:ConcreteType:Angle.

Implements: Geometry, Geometry2D, Shape2D.

## Type Box3D

Fields: Center:ConcreteType:Vector3D, Extent:ConcreteType:Size3D, Rotation:ConcreteType:Rotation3D.

Implements: Geometry, Geometry3D, Shape3D.

## Type Capsule

Fields: Line:ConcreteType:Line3D, Radius:ConcreteType:Number.

Implements: Geometry, Geometry3D, Shape3D.

## Type Character

Fields: .

Implements: Any, Equatable, Value.

## Type Chord

Fields: Arc:ConcreteType:Arc.

Implements: ClosedShape2D, Geometry, Geometry2D, OpenClosedShape.

## Type Circle

Fields: Center:ConcreteType:Vector2D, Radius:ConcreteType:Number.

Implements: ClosedShape2D, Geometry, Geometry2D, OpenClosedShape.

## Type Color

Fields: A:ConcreteType:Unit, B:ConcreteType:Unit, G:ConcreteType:Unit, R:ConcreteType:Unit.

Implements: Any, Coordinate, Equatable, Value.

## Type ColorHSL

Fields: Hue:ConcreteType:Angle, Luminance:ConcreteType:Unit, Saturation:ConcreteType:Unit.

Implements: Any, Coordinate, Equatable, Value.

## Type ColorHSV

Fields: Hue:ConcreteType:Angle, S:ConcreteType:Unit, V:ConcreteType:Unit.

Implements: Any, Coordinate, Equatable, Value.

## Type ColorLAB

Fields: A:ConcreteType:Number, B:ConcreteType:Number, Lightness:ConcreteType:Unit.

Implements: Any, Coordinate, Equatable, Value.

## Type ColorLCh

Fields: ChromaHue:ConcreteType:PolarCoordinate, Lightness:ConcreteType:Unit.

Implements: Any, Coordinate, Equatable, Value.

## Type ColorLUV

Fields: Lightness:ConcreteType:Unit, U:ConcreteType:Unit, V:ConcreteType:Unit.

Implements: Any, Coordinate, Equatable, Value.

## Type ColorYCbCr

Fields: Cb:ConcreteType:Unit, Cr:ConcreteType:Unit, Y:ConcreteType:Unit.

Implements: Any, Coordinate, Equatable, Value.

## Type Complex

Fields: Imaginary:ConcreteType:Number, Real:ConcreteType:Number.

Implements: AdditiveInverse, Any, Arithmetic, Array, Equatable, MultiplicativeArithmetic, MultiplicativeInverse, Numerical, ScalarArithmetic, Value, Vector.

## Type Cone

Fields: Line:ConcreteType:Line3D, Radius:ConcreteType:Number.

Implements: Geometry, Geometry3D, Shape3D.

## Type ConeSegment

Fields: Line:ConcreteType:Line3D, Radius1:ConcreteType:Number, Radius2:ConcreteType:Number.

Implements: Geometry, Geometry3D, Shape3D.

## Type CubicBezier2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D, D:ConcreteType:Vector2D.

Implements: Array.

## Type CubicBezier3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D, D:ConcreteType:Vector3D.

Implements: Array.

## Type Cylinder

Fields: Line:ConcreteType:Line3D, Radius:ConcreteType:Number.

Implements: Geometry, Geometry3D, Shape3D.

## Type CylindricalCoordinate

Fields: Azimuth:ConcreteType:Angle, Height:ConcreteType:Number, RadialDistance:ConcreteType:Number.

Implements: Any, Coordinate, Equatable, Value.

## Type DateTime

Fields: Value:ConcreteType:Number.

Implements: Any, Coordinate, Equatable, Value.

## Type Dynamic

Fields: .

Implements: .

## Type Ellipse

Fields: Center:ConcreteType:Vector2D, Size:ConcreteType:Size2D.

Implements: Curve, Curve2D, Geometry, Geometry2D, OpenClosedShape, Procedural.

## Type Error

Fields: .

Implements: .

## Type EulerAngles

Fields: Pitch:ConcreteType:Angle, Roll:ConcreteType:Angle, Yaw:ConcreteType:Angle.

Implements: Any, Equatable, Value.

## Type Fraction

Fields: Denominator:ConcreteType:Number, Numerator:ConcreteType:Number.

Implements: Any, Equatable, Value.

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

Implements: Any, Coordinate, Equatable, Value.

## Type GeoCoordinateWithAltitude

Fields: Altitude:ConcreteType:Number, Coordinate:ConcreteType:GeoCoordinate.

Implements: Any, Coordinate, Equatable, Value.

## Type HorizontalCoordinate

Fields: Azimuth:ConcreteType:Angle, Height:ConcreteType:Number, Radius:ConcreteType:Number.

Implements: Any, Coordinate, Equatable, Value.

## Type Integer

Fields: .

Implements: AdditiveInverse, Any, Arithmetic, Comparable, Equatable, MultiplicativeInverse, Numerical, Value, WholeNumber.

## Type Integer2

Fields: A:ConcreteType:Integer, B:ConcreteType:Integer.

Implements: Array.

## Type Integer3

Fields: A:ConcreteType:Integer, B:ConcreteType:Integer, C:ConcreteType:Integer.

Implements: Array.

## Type Integer4

Fields: A:ConcreteType:Integer, B:ConcreteType:Integer, C:ConcreteType:Integer, D:ConcreteType:Integer.

Implements: Array.

## Type LazyArray

Fields: Count:ConcreteType:Integer, Function:ConcreteType:Function1<ConcreteType:Integer,TypeVariable:T>.

Implements: Array.

## Type LazyArray2D

Fields: ColumnCount:ConcreteType:Integer, Function:ConcreteType:Function2<ConcreteType:Integer,ConcreteType:Integer,TypeVariable:T>, RowCount:ConcreteType:Integer.

Implements: Array, Array2D.

## Type LazyArray3D

Fields: ColumnCount:ConcreteType:Integer, Function:ConcreteType:Function3<ConcreteType:Integer,ConcreteType:Integer,ConcreteType:Integer,TypeVariable:T>, LayerCount:ConcreteType:Integer, RowCount:ConcreteType:Integer.

Implements: Array, Array3D.

## Type Length

Fields: Meters:ConcreteType:Number.

Implements: AdditiveArithmetic, AdditiveInverse, Any, Comparable, Equatable, Measure, MultiplicativeArithmetic, Numerical, ScalarArithmetic, Value.

## Type Lens

Fields: A:ConcreteType:Circle, B:ConcreteType:Circle.

Implements: ClosedShape2D, Geometry, Geometry2D, OpenClosedShape.

## Type Line2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D.

Implements: Array, Geometry, Geometry2D, OpenClosedShape, Points2D, PolyLine2D.

## Type Line3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D.

Implements: Array, Geometry, Geometry3D, OpenClosedShape, Points3D, PolyLine3D.

## Type Line4D

Fields: A:ConcreteType:Vector4D, B:ConcreteType:Vector4D.

Implements: Any, Array, Equatable, Value.

## Type LogPolarCoordinate

Fields: Azimuth:ConcreteType:Angle, Rho:ConcreteType:Number.

Implements: Any, Coordinate, Equatable, Value.

## Type Mass

Fields: Kilograms:ConcreteType:Number.

Implements: AdditiveArithmetic, AdditiveInverse, Any, Comparable, Equatable, Measure, MultiplicativeArithmetic, Numerical, ScalarArithmetic, Value.

## Type Matrix3x3

Fields: Column1:ConcreteType:Vector3D, Column2:ConcreteType:Vector3D, Column3:ConcreteType:Vector3D.

Implements: Any, Array, Equatable, Value.

## Type Matrix4x4

Fields: Column1:ConcreteType:Vector4D, Column2:ConcreteType:Vector4D, Column3:ConcreteType:Vector4D, Column4:ConcreteType:Vector4D.

Implements: Any, Array, Equatable, Value.

## Type Number

Fields: .

Implements: AdditiveInverse, Any, Arithmetic, Comparable, Equatable, MultiplicativeArithmetic, MultiplicativeInverse, Numerical, Real, ScalarArithmetic, Value.

## Type NumberInterval

Fields: Max:ConcreteType:Number, Min:ConcreteType:Number.

Implements: Any, Equatable, Interval, Value.

## Type Orientation3D

Fields: Value:ConcreteType:Rotation3D.

Implements: Any, Equatable, Value.

## Type Plane

Fields: D:ConcreteType:Number, Normal:ConcreteType:Vector3D.

Implements: Any, Equatable, Value.

## Type PolarCoordinate

Fields: Angle:ConcreteType:Angle, Radius:ConcreteType:Number.

Implements: Any, Coordinate, Equatable, Value.

## Type Pose2D

Fields: Orientation:ConcreteType:Angle, Position:ConcreteType:Vector2D.

Implements: Any, Equatable, Value.

## Type Pose3D

Fields: Orientation:ConcreteType:Orientation3D, Position:ConcreteType:Vector3D.

Implements: Any, Equatable, Value.

## Type Probability

Fields: Value:ConcreteType:Number.

Implements: AdditiveArithmetic, AdditiveInverse, Any, Comparable, Equatable, Measure, MultiplicativeArithmetic, Numerical, ScalarArithmetic, Value.

## Type Quad2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D, D:ConcreteType:Vector2D.

Implements: Any, Array, Equatable, Value.

## Type Quad3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D, D:ConcreteType:Vector3D.

Implements: Any, Array, Equatable, Value.

## Type QuadMesh

Fields: Faces:Concept:Array<ConcreteType:Integer4>, Vertices:Concept:Array<ConcreteType:Vertex>.

Implements: Geometry, Geometry3D, Mesh.

## Type QuadraticBezier2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D.

Implements: Array.

## Type QuadraticBezier3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D.

Implements: Array.

## Type Quaternion

Fields: W:ConcreteType:Number, X:ConcreteType:Number, Y:ConcreteType:Number, Z:ConcreteType:Number.

Implements: Any, Equatable, Value.

## Type Rational

Fields: Denominator:ConcreteType:Integer, Numerator:ConcreteType:Integer.

Implements: Any, Equatable, Value.

## Type Ray2D

Fields: Direction:ConcreteType:Vector2D, Position:ConcreteType:Vector2D.

Implements: Any, Equatable, Value.

## Type Ray3D

Fields: Direction:ConcreteType:Vector3D, Position:ConcreteType:Vector3D.

Implements: Any, Equatable, Value.

## Type Rect2D

Fields: Center:ConcreteType:Vector2D, Size:ConcreteType:Size2D.

Implements: Geometry, Geometry2D, OpenClosedShape, Points2D, Polygon2D, PolyLine2D.

## Type RegularPolygon

Fields: NumPoints:ConcreteType:Integer.

Implements: Geometry, Geometry2D, OpenClosedShape, Points2D, Polygon2D, PolyLine2D.

## Type Ring

Fields: Center:ConcreteType:Vector2D, InnerRadius:ConcreteType:Number, OuterRadius:ConcreteType:Number.

Implements: ClosedShape2D, Geometry, Geometry2D, OpenClosedShape.

## Type Rotation3D

Fields: Quaternion:ConcreteType:Quaternion.

Implements: Any, Equatable, Value.

## Type Sector

Fields: Arc:ConcreteType:Arc.

Implements: ClosedShape2D, Geometry, Geometry2D, OpenClosedShape.

## Type Segment

Fields: Arc:ConcreteType:Arc.

Implements: ClosedShape2D, Geometry, Geometry2D, OpenClosedShape.

## Type Size2D

Fields: Height:ConcreteType:Number, Width:ConcreteType:Number.

Implements: Any, Equatable, Value.

## Type Size3D

Fields: Depth:ConcreteType:Number, Height:ConcreteType:Number, Width:ConcreteType:Number.

Implements: Any, Equatable, Value.

## Type Sphere

Fields: Center:ConcreteType:Vector3D, Radius:ConcreteType:Number.

Implements: Any, Equatable, Value.

## Type SphericalCoordinate

Fields: Azimuth:ConcreteType:Angle, Polar:ConcreteType:Angle, Radius:ConcreteType:Number.

Implements: Any, Coordinate, Equatable, Value.

## Type String

Fields: .

Implements: Array, Comparable, Equatable.

## Type Temperature

Fields: Celsius:ConcreteType:Number.

Implements: AdditiveArithmetic, AdditiveInverse, Any, Comparable, Equatable, Measure, MultiplicativeArithmetic, Numerical, ScalarArithmetic, Value.

## Type Time

Fields: Seconds:ConcreteType:Number.

Implements: AdditiveArithmetic, AdditiveInverse, Any, Comparable, Equatable, Measure, MultiplicativeArithmetic, Numerical, ScalarArithmetic, Value.

## Type Transform2D

Fields: Rotation:ConcreteType:Angle, Scale:ConcreteType:Vector2D, Translation:ConcreteType:Vector2D.

Implements: Any, Equatable, Value.

## Type Transform3D

Fields: Rotation:ConcreteType:Rotation3D, Scale:ConcreteType:Vector3D, Translation:ConcreteType:Vector3D.

Implements: Any, Equatable, Value.

## Type Triangle2D

Fields: A:ConcreteType:Vector2D, B:ConcreteType:Vector2D, C:ConcreteType:Vector2D.

Implements: Any, Array, Equatable, Value.

## Type Triangle3D

Fields: A:ConcreteType:Vector3D, B:ConcreteType:Vector3D, C:ConcreteType:Vector3D.

Implements: Any, Array, Equatable, Value.

## Type TriMesh

Fields: Faces:Concept:Array<ConcreteType:Integer3>, Vertices:Concept:Array<ConcreteType:Vertex>.

Implements: Geometry, Geometry3D, Mesh.

## Type Tube

Fields: InnerRadius:ConcreteType:Number, Line:ConcreteType:Line3D, OuterRadius:ConcreteType:Number.

Implements: Geometry, Geometry3D, Shape3D.

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

Implements: AdditiveInverse, Any, Arithmetic, Comparable, Equatable, MultiplicativeArithmetic, MultiplicativeInverse, Numerical, Real, ScalarArithmetic, Value.

## Type Vector2D

Fields: X:ConcreteType:Number, Y:ConcreteType:Number.

Implements: AdditiveInverse, Any, Arithmetic, Array, Equatable, MultiplicativeArithmetic, MultiplicativeInverse, Numerical, ScalarArithmetic, Value, Vector.

## Type Vector3D

Fields: X:ConcreteType:Number, Y:ConcreteType:Number, Z:ConcreteType:Number.

Implements: AdditiveInverse, Any, Arithmetic, Array, Equatable, MultiplicativeArithmetic, MultiplicativeInverse, Numerical, ScalarArithmetic, Value, Vector.

## Type Vector4D

Fields: W:ConcreteType:Number, X:ConcreteType:Number, Y:ConcreteType:Number, Z:ConcreteType:Number.

Implements: AdditiveInverse, Any, Arithmetic, Array, Equatable, MultiplicativeArithmetic, MultiplicativeInverse, Numerical, ScalarArithmetic, Value, Vector.

## Type Vertex

Fields: Normal:ConcreteType:Vector3D, Position:ConcreteType:Vector3D, UV:ConcreteType:Vector2D.

Implements: .

