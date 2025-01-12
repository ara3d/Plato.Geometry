
using System.Collections.Generic;
using Plato.SinglePrecision;

namespace IfcGeometry
{
    public class IfcSolidModel
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifchalfspacesolid.htm
    public class IfcHalfSpaceSolid : IfcSolidModel
    {
        public IfcSurface BaseSurface { get; }
        public bool AgreementFlag { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcboxedhalfspace.htm
    public class IfcBoxedIfcHalfSpace : IfcHalfSpaceSolid
    {
        public IfcBoundingBox Enclosure { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcpolygonalboundedhalfspace.htm
    public class IfcPolygonalBoundedIfcHalfSpace : IfcHalfSpaceSolid
    {
        public IfcPlacement Position { get; }
        public IfcBoundedCurve Boundary { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcsweptareasolid.htm
    public class IfcSweptAreaSolid : IfcSolidModel
    {
        public IfcProfileDef SweptArea { get; }
        public IfcPlacement Position { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcsweptdisksolid.htm
    public class IfcIfcSweptDiskSolid : IfcSweptAreaSolid
    {
        public IfcCurve Directrix { get; }
        public double Radius { get; }
        public double InnerRadius { get; }
        public double StartParam { get; }
        public double EndParam { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcextrudedareasolid.htm
    public class IfcExtrudedAreaSolid : IfcSweptAreaSolid
    {
        public Vector3D Extrusion { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcrevolvedareasolid.htm
    public class IfcRevolvedAreaSolid : IfcSweptAreaSolid
    {
        public Vector3D Axis { get; }
        public double Angle { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcsurfacecurvesweptareasolid.htm
    public class IfcSurfaceCurveIfcSweptAreaSolid : IfcSweptAreaSolid
    {
        public IfcCurve Directrix { get; }
        public IfcCurve StartParam { get; }
        public IfcCurve EndParam { get; }
        public IfcSurface Surface { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcmanifoldsolidbrep.htm
    public class IfcManifoldSolidBrep : IfcSolidModel
    {
        public IfcClosedShell Outer { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcfacetedbrep.htm
    public class IfcFacetedBrep : IfcManifoldSolidBrep
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometricmodelresource/lexical/ifcfacetedbrepwithvoids.htm
    public class IfcFacetedBrepWithVoids : IfcFacetedBrep
    {
        public List<IfcClosedShell> Voids { get; }
    }

}