using Ara3D.Geometry;
using Assets.ClonerExample;
using UnityEngine;

namespace Ara3D.UnityBridge
{
    [ExecuteAlways]
    public abstract class ProceduralGeometryObject : FilterComponent<object, ITriMesh>
    {
        public bool Render = true;
        public Material Material;
        public bool ZUp;
        public bool FlipTriangles;
        public bool DoubleSided; 

        public void Update()
        {
            if (Render)
            {
                var mesh = ComputeGeometry().ToUnity(ZUp, FlipTriangles, DoubleSided);
                var rp = new RenderParams(Material);
                UnityEngine.Graphics.RenderMesh(rp, mesh, 0, transform.localToWorldMatrix);
            }
        }

        public override ITriMesh EvalImpl(object input)
        {
            return ComputeGeometry();
        }

        public abstract ITriMesh ComputeGeometry();
    }
}
