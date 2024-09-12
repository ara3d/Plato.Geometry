using Ara3D.UnityBridge;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class Clamp : FilterComponent<Vector3, Vector3>
    {
        public Bounds Bounds;

        public override Vector3 EvalImpl(Vector3 v)
            => Bounds.ToAra3D().Clamp(v.ToAra3D()).ToUnity();
    }
}