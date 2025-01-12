using Plato.SinglePrecision;

namespace IfcGeometry
{
    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifccsgprimitive3d.htm
    public class IfcCsgPrimitive3D
    {
        public IfcPlacement Position { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcblock.htm
    public class IfcBlock : IfcCsgPrimitive3D
    {
        public Vector3D Dimensions { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcsphere.htm
    public class IfcSphere : IfcCsgPrimitive3D
    {
        public double Radius { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcrectangularpyramid.htm
    public class IfcRectangularPyramid : IfcCsgPrimitive3D
    {
        public double XLength { get; }
        public double YLength { get; }
        public double Height { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcrightcircularcylinder.htm
    public class IfcRightCircularCylinder : IfcCsgPrimitive3D
    {
        public double Height { get; }
        public double Radius { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcrightcircularcone.htm
    public class IfcRightCircularCone : IfcCsgPrimitive3D
    {
        public double Height { get; }
        public double BottomRadius { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcbooleanoperand.htm
    public class IfcBooleanOperand : IfcSelect
    {
        public IfcSolidModel SolidModel;
        public IfcHalfSpaceSolid IfcHalfSpaceSolid;
        public IfcBooleanResult BooleanResult;
        public IfcCsgPrimitive3D CsgPrimitive3D;
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcbooleanoperator.htm
    public enum IfcBooleanOperator
    {
        Union,
        Intersection,
        Difference,
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcbooleanresult.htm
    public class IfcBooleanResult
    {
        public IfcBooleanOperator Operator;
        public IfcBooleanOperand FirstOperand;
        public IfcBooleanOperand SecondOperand;
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcbooleanclippingresult.htm
    public class IfcBooleanClippingResult : IfcBooleanResult
    { }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifccsgsolid.htm
    public class CsgSolid : IfcSolidModel
    {
        public IfcCsgPrimitive3D Primitive { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifccsgselect.htm
    public class CsgSelect : IfcSelect
    {
        public IfcBooleanResult BooleanResult { get; }
        public IfcCsgPrimitive3D CsgPrimitive3D { get; }
    }
}