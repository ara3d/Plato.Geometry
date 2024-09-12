using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.ClonerExample
{
    public class ClonerExample : MonoBehaviour
    { 
        public int instanceCount = 100000;
        public GpuInstanceData[] gpuInstanceData;
        public Mesh mesh;
        public Material material;

        private GpuInstanceData cache;

        [Range(0, 1)] public float smoothness = 0.5f;
        [Range(0, 1)] public float metallic = 0.5f;
        [Range(0, 1)] public float alpha = 0.5f;

        public ShadowCastingMode castShadows = ShadowCastingMode.Off;
        public bool receiveShadows = false;
    
        private ComputeBuffer gpuBuffer;
        private ComputeBuffer argsBuffer;

        private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

        public bool recompute = false;

        void Start()
        {
            argsBuffer = new ComputeBuffer(5, sizeof(uint), ComputeBufferType.IndirectArguments);
            UpdateBuffers();
        }

        void Update()
        { 
            // UpdateGpuData starting position buffer
            if (recompute 
                || cache.Smoothness != smoothness 
                || cache.Metallic != metallic
                || cache.Color.w != alpha
                || argsBuffer == null)
            {
                UpdateBuffers();
                recompute = false;
                cache = new GpuInstanceData()
                {
                    Smoothness = smoothness,
                    Metallic = metallic,
                    Color = new Vector4(0, 0, 0, alpha)
                };
            }

            var bounds = new Bounds(Vector3.zero, new Vector3(100.0f, 100.0f, 100.0f));
            Graphics.DrawMeshInstancedIndirect(mesh, 0, material, bounds, 
                argsBuffer, 0, null, castShadows, receiveShadows);
        }

        void UpdateBuffers()
        { 
            if ( instanceCount < 1 ) instanceCount = 1;
            if ( gpuBuffer != null ) gpuBuffer.Release();
            if ( argsBuffer == null) argsBuffer = new ComputeBuffer(5, sizeof(uint), ComputeBufferType.IndirectArguments);

            gpuInstanceData = new GpuInstanceData[instanceCount];
        
            for (var i=0; i < instanceCount; i++)
            {
                var angle = Random.Range(0.0f, Mathf.PI * 2.0f);
                var distance = Random.Range(20.0f, 100.0f);
                var height = Random.Range(-2.0f, 2.0f);
                var pos = new Vector3(Mathf.Sin(angle) * distance, height, Mathf.Cos(angle) * distance);
                var color = new Vector4(Random.value, Random.value, Random.value, alpha);
                var rot = Quaternion.identity;
                var scl = Vector3.one;

                gpuInstanceData[i] = new GpuInstanceData()
                {
                    Pos = pos,
                    Orientation = rot,
                    Scl = scl,
                    Color = color,
                    Smoothness = Random.Range(0f, 1f),
                    Metallic = Random.Range(0f, 1f),
                };
            }

            gpuBuffer = new ComputeBuffer(instanceCount, GpuInstanceData.Size);
            gpuBuffer.SetData(gpuInstanceData);

            material.SetBuffer("instanceBuffer", gpuBuffer);

            // indirect args
            var numIndices = (mesh != null) ? mesh.GetIndexCount(0) : 0;
            args[0] = numIndices;
            args[1] = (uint)instanceCount;
            argsBuffer.SetData(args);
        }

        void OnDisable()
        {
            if (gpuBuffer != null) gpuBuffer.Release();
            gpuBuffer = null;

            if (argsBuffer != null) argsBuffer.Release();
            argsBuffer = null;
        }
    }
}