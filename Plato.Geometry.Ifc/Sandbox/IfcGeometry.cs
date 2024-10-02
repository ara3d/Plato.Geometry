using System;
using Plato.DoublePrecision;

namespace IfcGeometry
{
    public class IfcDeprecatedAttribute : Attribute
    {
    }

    public class IfcPlacement
    {
        public Vector3D Location { get; }
        public Vector3D Axis { get; }
        public Vector3D RefDirection { get; }
    }

    public class IfcTransform2D
    {

        public Vector3D? Axis { get; }
        public Vector3D? Axis2 { get; }
        public Vector3D LocalOrigin { get; }
        public double Scale { get; }
    }


    public class IfcBoundingBox
    {
        public Vector3D Min { get; }
        public Vector3D Max { get; }
    }

    public class IfcSelect
    { }
}