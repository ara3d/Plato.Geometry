using System.Collections.Generic;

namespace IfcGeometry
{
    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcprofiletypeenum.htm
    public enum IfcProfileType
    {
        Curve,
        Area
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcprofiledef.htm
    public class IfcProfileDef
    {
        public IfcProfileType IfcProfileType { get; }
        public string Name { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcparameterizedprofiledef.htm
    public class IfcParameterizedProfileDef : IfcProfileDef
    {
        public IfcPlacement Position { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcishapeprofiledef.htm
    public class IfcIShapeProfileDef : IfcParameterizedProfileDef
    {
        public double OverallWidth { get; }
        public double OverallDepth { get; }
        public double WebThickness { get; }
        public double FlangeThickness { get; }
        public double? FilletRadius { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcasymmetricishapeprofiledef.htm
    public class IfcAsymmetricIShapeProfileDef : IfcIShapeProfileDef
    {
        public double TopFlangeWidth { get; }
        public double? TopFlangeThickness { get; }
        public double? TopFlangeFilletRadius { get; }
        public double? CentreOfGravityInY { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifclshapeprofiledef.htm
    public class IfcLShapeProfileDef : IfcParameterizedProfileDef
    {
        public double Depth { get; }
        public double? Width { get; }
        public double Thickness { get; }
        public double? FilletRadius { get; }
        public double?EdgeRadius { get; }
        public double? LegSlope { get; }
        public double? CentreOfGravityInX { get; }
        public double? CentreOfGravityInY { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcushapeprofiledef.htm
    public class IfcUShapeProfileDef : IfcParameterizedProfileDef
    {
        public double Depth { get; }
        public double FlangeWidth { get; }
        public double WebThickness { get; }
        public double FlangeThickness { get; }
        public double? FilletRadius { get; }
        public double? EdgeRadius { get; }
        public double? FlangeSlope { get; }
        public double? CentreOfGravityInX { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifccshapeprofiledef.htm
    public class IfcCShapeProfileDef : IfcParameterizedProfileDef
    {
        public double Depth { get; }
        public double Width { get; }
        public double WallThickness { get; }
        public double Girth { get; }
        public double? InternalFilletRadius { get; }
        public double? CentreOfGravityInX { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifczshapeprofiledef.htm
    public class IfcZShapeProfileDef : IfcParameterizedProfileDef
    {
        public double Depth { get; }
        public double FlangeWidth { get; }
        public double WebThickness { get; }
        public double FlangeThickness { get; }
        public double? FilletRadius { get; }
        public double? EdgeRadius { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifctshapeprofiledef.htm
    public class IfcTShapeProfileDef : IfcParameterizedProfileDef
    {
        public double Depth { get; }
        public double FlangeWidth { get; }
        public double WebThickness { get; }
        public double FlangeThickness { get; }
        public double? FilletRadius { get; }
        public double? FlangeEdgeRadius { get; }
        public double? WebEdgeRadius { get; }
        public double? WebSlope { get; }
        public double? FlangeSlope { get; }
        public double? CentreOfGravityInY { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifccranerailfshapeprofiledef.htm
    public class IfcCraneRailFShapeProfileDef : IfcParameterizedProfileDef
    {
        public double OverallHeight { get; }
        public double HeadWidth { get; }
        public double? Radius { get; }
        public double HeadDepth2 { get; }
        public double HeadDepth3 { get; }
        public double WebThickness { get; }
        public double BaseDepth1 { get; }
        public double BaseDepth2 { get; }
        public double? CentreOfGravityInY { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifccranerailashapeprofiledef.htm
    public class IfcCraneRailAShapeProfileDef : IfcParameterizedProfileDef
    {
        public double OverallHeight { get; }
        public double BaseWidth2 { get; }
        public double? Radius { get; }
        public double HeadWidth { get; }
        public double HeadDepth2 { get; }
        public double HeadDepth3 { get; }
        public double WebThickness { get; }
        public double BaseWidth4 { get; }
        public double BaseDepth1 { get; }
        public double BaseDepth2 { get; }
        public double BaseDepth3 { get; }
        public double? CentreOfGravityInY { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifccircleprofiledef.htm
    public class IfcCircleProfileDef : IfcParameterizedProfileDef
    {
        public double Radius { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifccirclehollowprofiledef.htm
    public class IfcCircleHollowProfileDef : IfcCircleProfileDef
    {
        public double WallThickness { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcellipseprofiledef.htm
    public class IfcEllipseProfileDef : IfcParameterizedProfileDef
    {
        public double SemiAxis1 { get; }
        public double SemiAxis2 { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcrectangleprofiledef.htm
    public class IfcRectangleProfileDef : IfcParameterizedProfileDef
    {
        public double X { get; }
        public double Y { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifctrapeziumprofiledef.htm
    public class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
    {
        public double BottomX { get; }
        public double TopX { get; }
        public double Y { get; }
        public double TopXOffset { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcarbitraryopenprofiledef.htm
    public class IfcArbitraryOpenProfileDef : IfcParameterizedProfileDef
    {
        public IfcBoundedCurve Curve { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifccenterlineprofiledef.htm
    public class IfcCenterLineProfileDef : IfcArbitraryOpenProfileDef
    {
        public double Thickness { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcarbitraryclosedprofiledef.htm
    public class IfcArbitraryClosedProfileDef : IfcArbitraryOpenProfileDef
    {
        public IfcBoundedCurve OuterCurve { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcarbitraryprofiledefwithvoids.htm
    public class IfcArbitraryProfileDefWithVoids : IfcArbitraryClosedProfileDef
    {
        public List<IfcBoundedCurve> InnerCurves { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifccompositeprofiledef.htm
    public class IfcCompositeProfileDef : IfcProfileDef
    {
        public List<IfcProfileDef> Profiles { get; }
        public string Label { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcprofileresource/lexical/ifcderivedprofiledef.htm
    public class IfcDerivedProfileDef : IfcProfileDef
    {
        public IfcProfileDef ParentProfile { get; }
        public IfcTransform2D IfcTransform { get; }
        public string Label { get; }
    }
}

