# Concepts

Concepts are implemented as interfaces. Functions defined on a concept are available on every type that implements the concept.

## Concept Additive

Inherits: .

Implemented by: Number, Integer, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Inherited by: Numerical, NumberLike, Real, WholeNumber, Measure, Vector, Algebraic, Arithmetic.

Functions: Add, Negative, Subtract.

Fields: .

## Concept Algebraic

Inherits: Additive, Invertible, Multiplicative, MultiplicativeWithInverse, ScalarArithmetic.

Implemented by: Number, Unit.

Inherited by: Real.

Functions: .

Fields: .

## Concept Any

Inherits: .

Implemented by: Transform2D, Pose2D, Bounds2D, Ray2D, Triangle2D, Quad2D, Sphere, Plane, Transform3D, Pose3D, Bounds3D, Ray3D, Triangle3D, Quad3D, Quaternion, AxisAngle, EulerAngles, Rotation3D, Orientation3D, Line4D, Number, Integer, String, Boolean, Character, Unit, Probability, Complex, Integer2, Integer3, Integer4, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Size2D, Size3D, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4.

Inherited by: Value, Numerical, NumberLike, Real, WholeNumber, Measure, Vector, Coordinate, Interval.

Functions: FieldNames, FieldValues, TypeName.

Fields: .

## Concept Arithmetic

Inherits: Additive, Divisible, ModuloOperation, Multiplicative.

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: Real, WholeNumber, Vector.

Functions: .

Fields: .

## Concept Array

Inherits: .

Implemented by: Bounds2D, Triangle2D, Quad2D, Line2D, Bounds3D, Line3D, Triangle3D, Quad3D, CubicBezier2D, CubicBezier3D, QuadraticBezier2D, QuadraticBezier3D, Line4D, String, Complex, Integer2, Integer3, Integer4, Size2D, Size3D, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4.

Inherited by: Array2D, Array3D, Vector, Interval, BezierPatch, Grid2D, QuadGrid.

Functions: At, Count.

Fields: .

## Concept Array2D

Inherits: Array.

Implemented by: .

Inherited by: BezierPatch, Grid2D, QuadGrid.

Functions: At, ColumnCount, RowCount.

Fields: .

## Concept Array3D

Inherits: Array.

Implemented by: .

Inherited by: .

Functions: At, ColumnCount, LayerCount, RowCount.

Fields: .

## Concept BezierPatch

Inherits: Array, Array2D, Geometry, Geometry, Geometry3D, Geometry3D, Points3D, Surface.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept BooleanOperations

Inherits: .

Implemented by: Boolean.

Inherited by: .

Functions: And, Not, Or.

Fields: .

## Concept Bounded2D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Bounds.

Fields: .

## Concept Bounded3D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Bounds.

Fields: .

## Concept ClosedPolyLine2D

Inherits: ClosedShape2D, Geometry, Geometry, Geometry2D, Geometry2D, OpenClosedShape, OpenClosedShape, Points2D, PolyLine2D.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept ClosedPolyLine3D

Inherits: Geometry, Geometry3D, OpenClosedShape, Points3D, PolyLine3D.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept ClosedShape2D

Inherits: Geometry, Geometry2D, OpenClosedShape.

Implemented by: Circle, Lens, Ring, Sector, Chord, Segment.

Inherited by: ClosedPolyLine2D.

Functions: .

Fields: .

## Concept ClosedShape3D

Inherits: Geometry, Geometry3D, OpenClosedShape.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept ConvexPolyhedron

Inherits: Geometry, Geometry, Geometry3D, Geometry3D, Points3D, Polyhedron, Surface.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Coordinate

Inherits: Any, Equatable, Value.

Implemented by: Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, DateTime.

Inherited by: .

Functions: .

Fields: .

## Concept Curve

Inherits: OpenClosedShape, Procedural.

Implemented by: Ellipse, ParametricCurve2D, ParametricCurve3D.

Inherited by: Curve1D, Curve2D, Curve3D.

Functions: .

Fields: .

## Concept Curve1D

Inherits: Curve, OpenClosedShape, Procedural.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Curve2D

Inherits: Curve, Geometry, Geometry2D, OpenClosedShape, Procedural.

Implemented by: Ellipse, ParametricCurve2D.

Inherited by: .

Functions: .

Fields: .

## Concept Curve3D

Inherits: Curve, Geometry, Geometry3D, OpenClosedShape, Procedural.

Implemented by: ParametricCurve3D.

Inherited by: .

Functions: .

Fields: .

## Concept Deformable2D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Deform.

Fields: .

## Concept Deformable3D

Inherits: Transformable3D.

Implemented by: .

Inherited by: .

Functions: Deform.

Fields: .

## Concept DistanceField

Inherits: Procedural.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept DistanceField2D

Inherits: Field2D, Geometry, Geometry2D, Procedural, ScalarField2D.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept DistanceField3D

Inherits: Field3D, Geometry, Geometry3D, Procedural, ScalarField3D.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Divisible

Inherits: .

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: Real, WholeNumber, Vector, Arithmetic.

Functions: Divide.

Fields: .

## Concept Equatable

Inherits: .

Implemented by: Transform2D, Pose2D, Bounds2D, Ray2D, Triangle2D, Quad2D, Sphere, Plane, Transform3D, Pose3D, Bounds3D, Ray3D, Triangle3D, Quad3D, Quaternion, AxisAngle, EulerAngles, Rotation3D, Orientation3D, Line4D, Number, Integer, String, Boolean, Character, Unit, Probability, Complex, Integer2, Integer3, Integer4, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Size2D, Size3D, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4.

Inherited by: Value, Numerical, NumberLike, Real, WholeNumber, Measure, Vector, Coordinate, Orderable, Interval.

Functions: Equals.

Fields: .

## Concept ExplicitSurface

Inherits: Geometry, Geometry3D, Procedural, Surface.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Field2D

Inherits: Geometry, Geometry2D, Procedural.

Implemented by: .

Inherited by: ScalarField2D, DistanceField2D, Vector3Field2D, Vector4Field2D.

Functions: .

Fields: .

## Concept Field3D

Inherits: Geometry, Geometry3D, Procedural.

Implemented by: .

Inherited by: ScalarField3D, DistanceField3D, Vector2Field3D, Vector3Field3D, Vector4Field3D.

Functions: .

Fields: .

## Concept Geometry

Inherits: .

Implemented by: Line2D, Circle, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Box2D, Line3D, Capsule, Cylinder, Cone, Tube, ConeSegment, Box3D, TriMesh, QuadMesh, ParametricCurve2D, ParametricCurve3D, ParametricSurface.

Inherited by: Geometry2D, Geometry3D, Shape2D, Shape3D, OpenShape2D, ClosedShape2D, OpenShape3D, ClosedShape3D, Curve2D, Curve3D, Surface, ProceduralSurface, ExplicitSurface, Field2D, Field3D, ScalarField2D, ScalarField3D, DistanceField2D, DistanceField3D, Vector3Field2D, Vector4Field2D, Vector2Field3D, Vector3Field3D, Vector4Field3D, ImplicitSurface, ImplicitCurve2D, ImplicitVolume, Points2D, Points3D, BezierPatch, Polyhedron, ConvexPolyhedron, SolidPolyhedron, Mesh, PolyLine2D, PolyLine3D, ClosedPolyLine2D, ClosedPolyLine3D, Polygon2D, Polygon3D.

Functions: .

Fields: .

## Concept Geometry2D

Inherits: Geometry.

Implemented by: Line2D, Circle, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Box2D, ParametricCurve2D.

Inherited by: Shape2D, OpenShape2D, ClosedShape2D, Curve2D, Field2D, ScalarField2D, DistanceField2D, Vector3Field2D, Vector4Field2D, ImplicitCurve2D, Points2D, PolyLine2D, ClosedPolyLine2D, Polygon2D.

Functions: .

Fields: .

## Concept Geometry3D

Inherits: Geometry.

Implemented by: Line3D, Capsule, Cylinder, Cone, Tube, ConeSegment, Box3D, TriMesh, QuadMesh, ParametricCurve3D, ParametricSurface.

Inherited by: Shape3D, OpenShape3D, ClosedShape3D, Curve3D, Surface, ProceduralSurface, ExplicitSurface, Field3D, ScalarField3D, DistanceField3D, Vector2Field3D, Vector3Field3D, Vector4Field3D, ImplicitSurface, ImplicitVolume, Points3D, BezierPatch, Polyhedron, ConvexPolyhedron, SolidPolyhedron, Mesh, PolyLine3D, ClosedPolyLine3D, Polygon3D.

Functions: .

Fields: .

## Concept Grid2D

Inherits: Array, Array2D.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept ImplicitCurve2D

Inherits: Geometry, Geometry2D, ImplicitProcedural.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept ImplicitProcedural

Inherits: .

Implemented by: .

Inherited by: ImplicitSurface, ImplicitCurve2D, ImplicitVolume.

Functions: Eval.

Fields: .

## Concept ImplicitSurface

Inherits: Geometry, Geometry3D, ImplicitProcedural, Surface.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept ImplicitVolume

Inherits: Geometry, Geometry3D, ImplicitProcedural.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Interval

Inherits: Any, Array, Equatable, Equatable, Value.

Implemented by: Bounds2D, Bounds3D, AnglePair, NumberInterval.

Inherited by: .

Functions: Max, Min, Size.

Fields: .

## Concept Invertible

Inherits: .

Implemented by: Number, Unit.

Inherited by: Real, MultiplicativeWithInverse, Algebraic.

Functions: Inverse.

Fields: .

## Concept Measure

Inherits: Additive, Additive, Any, Equatable, Equatable, NumberLike, Numerical, Orderable, ScalarArithmetic, Value.

Implemented by: Probability, Angle, Length, Mass, Temperature, Time.

Inherited by: .

Functions: .

Fields: .

## Concept Mesh

Inherits: Geometry, Geometry3D.

Implemented by: TriMesh, QuadMesh.

Inherited by: .

Functions: Faces, Vertices.

Fields: .

## Concept ModuloOperation

Inherits: .

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: Real, WholeNumber, Vector, Arithmetic.

Functions: Modulo.

Fields: .

## Concept Multiplicative

Inherits: .

Implemented by: Number, Integer, Unit, Complex, Vector2D, Vector3D, Vector4D.

Inherited by: Real, WholeNumber, Vector, MultiplicativeWithInverse, Algebraic, Arithmetic.

Functions: Multiply.

Fields: .

## Concept MultiplicativeWithInverse

Inherits: Invertible, Multiplicative.

Implemented by: Number, Unit.

Inherited by: Real, Algebraic.

Functions: .

Fields: .

## Concept NumberLike

Inherits: Additive, Any, Equatable, Equatable, Numerical, Orderable, ScalarArithmetic, Value.

Implemented by: Number, Unit, Probability, Angle, Length, Mass, Temperature, Time.

Inherited by: Real, Measure.

Functions: FromNumber, ToNumber.

Fields: .

## Concept Numerical

Inherits: Additive, Any, Equatable, ScalarArithmetic, Value.

Implemented by: Number, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Inherited by: NumberLike, Real, Measure, Vector.

Functions: Components, FromComponents.

Fields: .

## Concept OpenClosedShape

Inherits: .

Implemented by: Line2D, Circle, Lens, Rect2D, Ellipse, Ring, Arc, Sector, Chord, Segment, RegularPolygon, Line3D, ParametricCurve2D, ParametricCurve3D.

Inherited by: OpenShape2D, ClosedShape2D, OpenShape3D, ClosedShape3D, Curve, Curve1D, Curve2D, Curve3D, PolyLine2D, PolyLine3D, ClosedPolyLine2D, ClosedPolyLine3D, Polygon2D, Polygon3D.

Functions: Closed.

Fields: .

## Concept OpenShape2D

Inherits: Geometry, Geometry2D, OpenClosedShape.

Implemented by: Arc.

Inherited by: .

Functions: .

Fields: .

## Concept OpenShape3D

Inherits: Geometry, Geometry3D, OpenClosedShape.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Orderable

Inherits: Equatable.

Implemented by: Number, Integer, String, Boolean, Character, Unit, Probability, Angle, Length, Mass, Temperature, Time.

Inherited by: NumberLike, Real, WholeNumber, Measure.

Functions: LessThanOrEquals.

Fields: .

## Concept Points2D

Inherits: Geometry, Geometry2D.

Implemented by: Line2D, Rect2D, RegularPolygon.

Inherited by: PolyLine2D, ClosedPolyLine2D, Polygon2D.

Functions: Points.

Fields: .

## Concept Points3D

Inherits: Geometry, Geometry3D.

Implemented by: Line3D.

Inherited by: BezierPatch, Polyhedron, ConvexPolyhedron, SolidPolyhedron, PolyLine3D, ClosedPolyLine3D, Polygon3D.

Functions: Points.

Fields: .

## Concept Polygon2D

Inherits: Geometry, Geometry2D, OpenClosedShape, Points2D, PolyLine2D.

Implemented by: Rect2D, RegularPolygon.

Inherited by: .

Functions: .

Fields: .

## Concept Polygon3D

Inherits: Geometry, Geometry3D, OpenClosedShape, Points3D, PolyLine3D.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept PolyhederalFace

Inherits: .

Implemented by: .

Inherited by: .

Functions: FaceIndex, Polyhedron, VertexIndices.

Fields: .

## Concept Polyhedron

Inherits: Geometry, Geometry, Geometry3D, Geometry3D, Points3D, Surface.

Implemented by: .

Inherited by: ConvexPolyhedron, SolidPolyhedron.

Functions: Faces.

Fields: .

## Concept PolyLine2D

Inherits: Geometry, Geometry2D, OpenClosedShape, Points2D.

Implemented by: Line2D, Rect2D, RegularPolygon.

Inherited by: ClosedPolyLine2D, Polygon2D.

Functions: .

Fields: .

## Concept PolyLine3D

Inherits: Geometry, Geometry3D, OpenClosedShape, Points3D.

Implemented by: Line3D.

Inherited by: ClosedPolyLine3D, Polygon3D.

Functions: .

Fields: .

## Concept Procedural

Inherits: .

Implemented by: Ellipse, ParametricCurve2D, ParametricCurve3D, ParametricSurface.

Inherited by: Curve, Curve1D, Curve2D, Curve3D, ProceduralSurface, ExplicitSurface, DistanceField, Field2D, Field3D, ScalarField2D, ScalarField3D, DistanceField2D, DistanceField3D, Vector3Field2D, Vector4Field2D, Vector2Field3D, Vector3Field3D, Vector4Field3D.

Functions: Eval.

Fields: .

## Concept ProceduralSurface

Inherits: Geometry, Geometry3D, Procedural, Surface.

Implemented by: ParametricSurface.

Inherited by: .

Functions: PeriodicX, PeriodicY.

Fields: .

## Concept QuadGrid

Inherits: Array, Array2D.

Implemented by: .

Inherited by: .

Functions: ClosedX, ClosedY.

Fields: .

## Concept Real

Inherits: Additive, Additive, Additive, Algebraic, Any, Arithmetic, Divisible, Equatable, Equatable, Invertible, ModuloOperation, Multiplicative, Multiplicative, MultiplicativeWithInverse, NumberLike, Numerical, Orderable, ScalarArithmetic, ScalarArithmetic, Value.

Implemented by: Number, Unit.

Inherited by: .

Functions: .

Fields: .

## Concept ScalarArithmetic

Inherits: .

Implemented by: Number, Unit, Probability, Complex, Angle, Length, Mass, Temperature, Time, Vector2D, Vector3D, Vector4D.

Inherited by: Numerical, NumberLike, Real, Measure, Vector, Algebraic.

Functions: Divide, Modulo, Multiply, Multiply.

Fields: .

## Concept ScalarField2D

Inherits: Field2D, Geometry, Geometry2D, Procedural.

Implemented by: .

Inherited by: DistanceField2D.

Functions: .

Fields: .

## Concept ScalarField3D

Inherits: Field3D, Geometry, Geometry3D, Procedural.

Implemented by: .

Inherited by: DistanceField3D.

Functions: .

Fields: .

## Concept Shape2D

Inherits: Geometry, Geometry2D.

Implemented by: Box2D.

Inherited by: .

Functions: .

Fields: .

## Concept Shape3D

Inherits: Geometry, Geometry3D.

Implemented by: Capsule, Cylinder, Cone, Tube, ConeSegment, Box3D.

Inherited by: .

Functions: .

Fields: .

## Concept SolidPolyhedron

Inherits: Geometry, Geometry, Geometry3D, Geometry3D, Points3D, Polyhedron, Surface.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Surface

Inherits: Geometry, Geometry3D.

Implemented by: ParametricSurface.

Inherited by: ProceduralSurface, ExplicitSurface, ImplicitSurface, BezierPatch, Polyhedron, ConvexPolyhedron, SolidPolyhedron.

Functions: .

Fields: .

## Concept Transformable2D

Inherits: .

Implemented by: .

Inherited by: .

Functions: Transform.

Fields: .

## Concept Transformable3D

Inherits: .

Implemented by: .

Inherited by: Deformable3D.

Functions: Transform.

Fields: .

## Concept Value

Inherits: Any, Equatable.

Implemented by: Transform2D, Pose2D, Bounds2D, Ray2D, Triangle2D, Quad2D, Sphere, Plane, Transform3D, Pose3D, Bounds3D, Ray3D, Triangle3D, Quad3D, Quaternion, AxisAngle, EulerAngles, Rotation3D, Orientation3D, Line4D, Number, Integer, String, Boolean, Character, Unit, Probability, Complex, Integer2, Integer3, Integer4, Color, ColorLUV, ColorLAB, ColorLCh, ColorHSV, ColorHSL, ColorYCbCr, SphericalCoordinate, PolarCoordinate, LogPolarCoordinate, CylindricalCoordinate, HorizontalCoordinate, GeoCoordinate, GeoCoordinateWithAltitude, Size2D, Size3D, Rational, Fraction, Angle, Length, Mass, Temperature, Time, DateTime, AnglePair, NumberInterval, Vector2D, Vector3D, Vector4D, Matrix3x3, Matrix4x4.

Inherited by: Numerical, NumberLike, Real, WholeNumber, Measure, Vector, Coordinate, Interval.

Functions: .

Fields: .

## Concept Vector

Inherits: Additive, Additive, Any, Arithmetic, Array, Divisible, Equatable, ModuloOperation, Multiplicative, Numerical, ScalarArithmetic, Value.

Implemented by: Complex, Vector2D, Vector3D, Vector4D.

Inherited by: .

Functions: .

Fields: .

## Concept Vector2Field3D

Inherits: Field3D, Geometry, Geometry3D, Procedural.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Vector3Field2D

Inherits: Field2D, Geometry, Geometry2D, Procedural.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Vector3Field3D

Inherits: Field3D, Geometry, Geometry3D, Procedural.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Vector4Field2D

Inherits: Field2D, Geometry, Geometry2D, Procedural.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept Vector4Field3D

Inherits: Field3D, Geometry, Geometry3D, Procedural.

Implemented by: .

Inherited by: .

Functions: .

Fields: .

## Concept WholeNumber

Inherits: Additive, Any, Arithmetic, Divisible, Equatable, Equatable, ModuloOperation, Multiplicative, Orderable, Value.

Implemented by: Integer.

Inherited by: .

Functions: .

Fields: .

# Types

Types are implemented as structs.
## Type Angle

Fields: Radians:ConcreteType:Number.

Implements: Additive, Any, Equatable, Measure, NumberLike, Numerical, Orderable, ScalarArithmetic, Value.

## Type AnglePair

Fields: Max:ConcreteType:Angle, Min:ConcreteType:Angle.

Implements: Any, Array, Equatable, Interval, Value.

## Type Arc

Fields: Angles:ConcreteType:AnglePair, Circle:ConcreteType:Circle.

Implements: Geometry, Geometry2D, OpenClosedShape, OpenShape2D.

## Type AxisAngle

Fields: Angle:ConcreteType:Angle, Axis:ConcreteType:Vector3D.

Implements: Any, Equatable, Value.

## Type Boolean

Fields: .

Implements: Any, BooleanOperations, Equatable, Orderable, Value.

## Type Bounds2D

Fields: Max:ConcreteType:Vector2D, Min:ConcreteType:Vector2D.

Implements: Any, Array, Equatable, Interval, Value.

## Type Bounds3D

Fields: Max:ConcreteType:Vector3D, Min:ConcreteType:Vector3D.

Implements: Any, Array, Equatable, Interval, Value.

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

Implements: Any, Equatable, Orderable, Value.

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

Implements: Additive, Any, Arithmetic, Array, Divisible, Equatable, ModuloOperation, Multiplicative, Numerical, ScalarArithmetic, Value, Vector.

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

Implements: Additive, Any, Arithmetic, Divisible, Equatable, ModuloOperation, Multiplicative, Orderable, Value, WholeNumber.

## Type Integer2

Fields: A:ConcreteType:Integer, B:ConcreteType:Integer.

Implements: Any, Array, Equatable, Value.

## Type Integer3

Fields: A:ConcreteType:Integer, B:ConcreteType:Integer, C:ConcreteType:Integer.

Implements: Any, Array, Equatable, Value.

## Type Integer4

Fields: A:ConcreteType:Integer, B:ConcreteType:Integer, C:ConcreteType:Integer, D:ConcreteType:Integer.

Implements: Any, Array, Equatable, Value.

## Type Length

Fields: Meters:ConcreteType:Number.

Implements: Additive, Any, Equatable, Measure, NumberLike, Numerical, Orderable, ScalarArithmetic, Value.

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

Implements: Additive, Any, Equatable, Measure, NumberLike, Numerical, Orderable, ScalarArithmetic, Value.

## Type Matrix3x3

Fields: Column1:ConcreteType:Vector3D, Column2:ConcreteType:Vector3D, Column3:ConcreteType:Vector3D.

Implements: Any, Array, Equatable, Value.

## Type Matrix4x4

Fields: Column1:ConcreteType:Vector4D, Column2:ConcreteType:Vector4D, Column3:ConcreteType:Vector4D, Column4:ConcreteType:Vector4D.

Implements: Any, Array, Equatable, Value.

## Type Number

Fields: .

Implements: Additive, Algebraic, Any, Arithmetic, Divisible, Equatable, Invertible, ModuloOperation, Multiplicative, MultiplicativeWithInverse, NumberLike, Numerical, Orderable, Real, ScalarArithmetic, Value.

## Type NumberInterval

Fields: Max:ConcreteType:Number, Min:ConcreteType:Number.

Implements: Any, Array, Equatable, Interval, Value.

## Type Orientation3D

Fields: Value:ConcreteType:Rotation3D.

Implements: Any, Equatable, Value.

## Type ParametricCurve2D

Fields: Evaluator:ConcreteType:Function1<ConcreteType:Number,ConcreteType:Vector2D>.

Implements: Curve, Curve2D, Geometry, Geometry2D, OpenClosedShape, Procedural.

## Type ParametricCurve3D

Fields: Evaluator:ConcreteType:Function1<ConcreteType:Number,ConcreteType:Vector3D>.

Implements: Curve, Curve3D, Geometry, Geometry3D, OpenClosedShape, Procedural.

## Type ParametricSurface

Fields: Evaluator:ConcreteType:Function1<ConcreteType:Number,ConcreteType:Vector3D>, PeriodicX:ConcreteType:Boolean, PeriodicY:ConcreteType:Boolean.

Implements: Geometry, Geometry3D, Procedural, ProceduralSurface, Surface.

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

Implements: Additive, Any, Equatable, Measure, NumberLike, Numerical, Orderable, ScalarArithmetic, Value.

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

Fields: Direction:ConcreteType:Vector2D, Origin:ConcreteType:Vector2D.

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

Implements: Any, Array, Equatable, Value.

## Type Size3D

Fields: Depth:ConcreteType:Number, Height:ConcreteType:Number, Width:ConcreteType:Number.

Implements: Any, Array, Equatable, Value.

## Type Sphere

Fields: Center:ConcreteType:Vector3D, Radius:ConcreteType:Number.

Implements: Any, Equatable, Value.

## Type SphericalCoordinate

Fields: Azimuth:ConcreteType:Angle, Polar:ConcreteType:Angle, Radius:ConcreteType:Number.

Implements: Any, Coordinate, Equatable, Value.

## Type String

Fields: .

Implements: Any, Array, Equatable, Orderable, Value.

## Type Temperature

Fields: Celsius:ConcreteType:Number.

Implements: Additive, Any, Equatable, Measure, NumberLike, Numerical, Orderable, ScalarArithmetic, Value.

## Type Time

Fields: Seconds:ConcreteType:Number.

Implements: Additive, Any, Equatable, Measure, NumberLike, Numerical, Orderable, ScalarArithmetic, Value.

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

Implements: Additive, Algebraic, Any, Arithmetic, Divisible, Equatable, Invertible, ModuloOperation, Multiplicative, MultiplicativeWithInverse, NumberLike, Numerical, Orderable, Real, ScalarArithmetic, Value.

## Type Vector2D

Fields: X:ConcreteType:Number, Y:ConcreteType:Number.

Implements: Additive, Any, Arithmetic, Array, Divisible, Equatable, ModuloOperation, Multiplicative, Numerical, ScalarArithmetic, Value, Vector.

## Type Vector3D

Fields: X:ConcreteType:Number, Y:ConcreteType:Number, Z:ConcreteType:Number.

Implements: Additive, Any, Arithmetic, Array, Divisible, Equatable, ModuloOperation, Multiplicative, Numerical, ScalarArithmetic, Value, Vector.

## Type Vector4D

Fields: W:ConcreteType:Number, X:ConcreteType:Number, Y:ConcreteType:Number, Z:ConcreteType:Number.

Implements: Additive, Any, Arithmetic, Array, Divisible, Equatable, ModuloOperation, Multiplicative, Numerical, ScalarArithmetic, Value, Vector.

## Type Vertex

Fields: Normal:ConcreteType:Vector3D, Position:ConcreteType:Vector3D, UV:ConcreteType:Vector2D.

Implements: .

