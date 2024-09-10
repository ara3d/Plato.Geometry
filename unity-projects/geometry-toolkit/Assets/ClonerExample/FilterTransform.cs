using Ara3D.UnityBridge;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class FilterTransform : FilterComponent<Ara3D.Mathematics.Vector3, Ara3D.Mathematics.Vector3>
    {
        public Vector3 Translation = Vector3.zero;
        public Vector3 Scaling = Vector3.one;
        public Vector3 Rotation = Vector3.zero;
        
        private Ara3D.Mathematics.Matrix4x4 Matrix; 

        public void Update()
        {
            Matrix = Ara3D.Mathematics.Matrix4x4.CreateTRS(
                Translation.ToAra3D(), 
                Ara3D.Mathematics.Quaternion.CreateFromEulerAngles(Rotation.ToAra3D()), 
                Scaling.ToAra3D());
        }

        public override Ara3D.Mathematics.Vector3 EvalImpl(Ara3D.Mathematics.Vector3 input)
        {
            return input.Transform(Matrix);
        }
    }
}