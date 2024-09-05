using System.Collections.Generic;
using Plato.DoublePrecision;

namespace IfcGeometry
{
    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcsurface.htm
    public class IfcSurface
    { }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcelementarysurface.htm
    public class IfcElementarySurface : IfcSurface
    {
        public Vector3D Position { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcplane.htm
    public class IfcPlane : IfcElementarySurface
    {
        public Vector3D Axis { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcboundedsurface.htm
    public class IfcBoundedSurface : IfcSurface
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifccurveboundedplane.htm
    public class IfcCurveBoundedPlane : IfcBoundedSurface
    {
        public IfcPlane BasisSurface { get; }
        public IfcCurve OuterBoundary { get; }
        public List<IfcCurve> InnerBoundaries { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcrectangulartrimmedsurface.htm
    public class IfcRectangularTrimmedSurface : IfcBoundedSurface
    {
        public IfcPlane BasisSurface { get; }
        public Vector2D UV1 { get; }
        public Vector2D UV2 { get; }
        public bool Usense { get; }
        public bool Vsense { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcsweptsurface.htm
    public class IfcSweptSurface : IfcSurface
    {
        public IfcProfileDef SweptCurve { get; }
        public IfcPlacement Position { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcsurfaceofrevolution.htm
    public class IfcSurfaceOfRevolution : IfcSweptSurface
    {
        public Vector3D AxisPosition { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifcgeometryresource/lexical/ifcsurfaceoflinearextrusion.htm
    public class IfcSurfaceOfLinearExtrusion : IfcSweptSurface
    {
        public Vector3D Direction { get; }
        public double Depth { get; }
    }
}