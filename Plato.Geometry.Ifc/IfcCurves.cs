using Ara3D.Mathematics;

namespace IfcGeometry
{
    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifccurve.htm
    public class IfcCurve
    {
    }
    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcboundedcurve.htm
    public class IfcBoundedCurve : IfcCurve
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifccompositecurve.htm
    public class IfcCompositeCurve : IfcBoundedCurve
    {
        public List<IfcCompositeCurveSegment> Segments { get; }
        public bool SelfIntersect { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifccompositecurvesegment.htm
    public class IfcCompositeCurveSegment
    {
        public IfcTransitionCode Transition { get; }
        public IfcCurve ParentCurve { get; }
        public bool SameSense { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifctransitioncode.htm
    public enum IfcTransitionCode
    {
        Continuous,
        Discontinuous,
        SameGradient,
        SameGradientSameCurvature,
        //SameGradientDifferentCurvature,
        //DifferentGradient,
        //Other
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifc2dcompositecurve.htm
    [IfcDeprecated]
    public class IfcCompositeCurve2D : IfcCompositeCurve
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcpolyline.htm
    public class IfcPolyline : IfcBoundedCurve
    {
        public List<Vector3> Points { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifctrimmedcurve.htm
    public class IfcTrimmedCurve : IfcBoundedCurve
    {
        public IfcCurve Basis { get; }
        public List<IfcTrimmingSelect> Trim1 { get; }
        public List<IfcTrimmingSelect> Trim2 { get; }
        public bool SenseAgreement { get; }
        public IfcTrimType MasterRepresentation { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifctrimmingpreference.htm
    public enum IfcTrimType
    {
        Cartesian,
        Parameter,
        Unspecified
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifctrimmingselect.htm
    public class IfcTrimmingSelect : IfcSelect
    {
        public Vector3 Point { get; }
        public double Parameter { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcbsplinecurve.htm
    public class IfcBSplineCurve : IfcBoundedCurve
    {
        public int Degree { get; }
        public List<Vector3> ControlPoints { get; }
        public List<double> Weights { get; }
        public IfcBSplineCurveForm Form { get; }
        public bool Closed { get; }
        public bool SelfIntersect { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcbsplinecurveform.htm
    public enum IfcBSplineCurveForm
    {
        PolylineForm,
        CircularArc,
        EllipticArc,
        ParabolicArc,
        HyperbolicArc,
        Unspecified
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcbeziercurve.htm
    public class IfcBezierCurve : IfcBSplineCurve
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcrationalbeziercurve.htm
    public class RationalBezierCurve : IfcBezierCurve
    {
        public List<double> Weights { get; }
    }

}