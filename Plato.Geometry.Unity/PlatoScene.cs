using System.Collections.Generic;
using Plato.DoublePrecision;

namespace Plato.Geometry.Unity
{
    public class PlatoLineData
    {
        public Color Color;
        public double Width;
        public bool Closed;
        public List<Vector3D> Points = new List<Vector3D>();
    }

    public class PlatoMeshData 
    {
        public Color Color;
        public List<Vector3D> Vertices = new List<Vector3D>();
        public List<int> Indices = new List<int>();
    }

    public class PlatoSceneObject
    {
        public string Name;
        public Transform3D Transform = new Transform3D(Vector3D.Default, Rotation3D.Default, (1, 1, 1));
        public List<PlatoSceneObject> Children = new List<PlatoSceneObject>();
        public List<PlatoMeshData> Meshes = new List<PlatoMeshData>();
        public List<PlatoLineData> Lines = new List<PlatoLineData>();
    }

    public class PlatoScene
    {
        public List<PlatoSceneObject> Objects = new List<PlatoSceneObject>();
    }
}
