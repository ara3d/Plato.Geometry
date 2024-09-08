using System;
using Plato;

namespace Plato.Geometry.Adapters
{
    public static class G3SharpAdapters
    { }

    public static class RevitAdapters
    { }

    public static class RhinoAdapters
    { }

    public static class NumericsAdapters
    { }

    public static class AssimpAdapters 
    { }

    public static class HyparAdapters
    {
        public static Elements.Geometry.Vector3 ToHypar(this DoublePrecision.Vector3D v) => new Elements.Geometry.Vector3(v.X, v.Y, v.Z);

        public static DoublePrecision.Vector3D ToPlato(this Elements.Geometry.Vector3 v) => (v.X, v.Y, v.Z);
    }

    public static class NavisworksAdapters
    { }

    public static class Autodesk3dsMaxAdapters
    { }
}
