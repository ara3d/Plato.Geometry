using Ara3D.Geometry;
using Ara3D.UnityBridge;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class FilterMesh : FilterComponent<Object, ITriMesh>
    {   
        public Mesh Mesh;

        public override ITriMesh EvalImpl(Object _)
            => (Mesh != null ? Mesh : GetComponent<MeshFilter>().mesh).ToAra3D();
    }
}