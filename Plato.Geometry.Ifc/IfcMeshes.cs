using System.Collections.Generic;
using Plato.DoublePrecision;

namespace IfcGeometry
{

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcconnectedfaceset.htm
    public class IfcConnectedFaceSet
    {
        public List<IfcFace> Faces { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcclosedshell.htm
    public class IfcClosedShell : IfcConnectedFaceSet
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcopenshell.htm
    public class IfcOpenShell : IfcConnectedFaceSet
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcface.htm
    public class IfcFace
    {
        public List<IfcFaceBound> Bounds { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcfacesurface.htm
    public class IfcFaceSurface : IfcFace
    {
        public IfcSurface Surface { get; }
        public bool SameSense { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcfacebound.htm
    public class IfcFaceBound
    {
        public IfcLoop Bound { get; }
        public bool Orientation { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcloop.htm
    public class IfcLoop
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcpolyloop.htm
    public class IfcPolyLoop : IfcLoop
    {
        public List<Vector3D> Polygon { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcvertexloop.htm
    public class IfcVertexLoop : IfcLoop
    {

    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcvertex.htm
    public class Vertex
    {
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcedgeloop.htm
    public class EdgeLoop : IfcLoop
    {
        public List<OrientedEdge> Edges { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcorientededge.htm
    public class OrientedEdge : Edge
    {
        public Edge Edge { get; }
        public bool Orientation { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcedge.htm
    public class Edge
    {
        public Vertex Start { get; }
        public Vertex End { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcedgecurve.htm
    public class EdgeCurve : Edge
    {
        public IfcCurve EdgeGeometry { get; }
        public bool SameSense { get; }
    }

    // https://standards.buildingsmart.org/IFC/RELEASE/IFC2x3/TC1/HTML/ifctopologyresource/lexical/ifcsubedge.htm
    public class SubEdge : Edge
    {
        public Edge Parent { get; }
    }
}