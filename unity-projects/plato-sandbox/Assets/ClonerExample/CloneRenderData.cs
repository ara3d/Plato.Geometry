using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.ClonerExample
{
    /// <summary>
    /// Manages important information for rendering instance data efficiently.
    /// Contains an array of CpuInstanceData, and GpuInstanceData. 
    /// </summary>
    public class CloneRenderData : IDisposable
    {
        public uint NumIndices => args[0];
        public uint NumInstances => args[1];
        public Mesh Mesh;
        public Material Material;
        public Bounds bounds;

        private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };
        private ComputeBuffer argsBuffer;
        private ComputeBuffer gpuBuffer;

        public void UpdateGpuData(Mesh mesh, NativeArray<GpuInstanceData> gpuInstances, Material material)
        {
            Mesh = mesh;
            Material = material;
            if (!gpuInstances.IsCreated)
                return;

            if (argsBuffer == null) argsBuffer =
                new ComputeBuffer(5, sizeof(uint), ComputeBufferType.IndirectArguments);

            var numIndices = (Mesh != null) ? Mesh.GetIndexCount(0) : 0u;
            var numInstances = (uint)gpuInstances.Length;

            // Only reallocate the gpuBuffer is the number of instances has changed 
            if (numInstances != NumInstances || gpuBuffer == null)
            {
                if (gpuBuffer != null)
                    gpuBuffer.Release();
                gpuBuffer = null;
                if (gpuInstances.Length != 0)
                {
                    gpuBuffer = new ComputeBuffer(gpuInstances.Length, GpuInstanceData.Size);
                    Material.SetBuffer("instanceBuffer", gpuBuffer);
                }
            }

            if (gpuBuffer != null)
            {
                // Copy the data into the ComputeBuffer (should be fast)
                gpuBuffer.SetData(gpuInstances);
            }

            if (numIndices != NumIndices || numInstances != NumInstances)
            {
                args[0] = numIndices;
                args[1] = numInstances;
                argsBuffer.SetData(args);
            }
        }

        public void Render(ShadowCastingMode shadowCasting, bool receiveShadows)
        {
            if (Material == null)
            {
                Debug.Log("No material present");
                return;
            }
            
            if (NumInstances == 0)
            {
                Debug.Log("No instances present");
                return;
            }   
            
            if (NumIndices == 0)
            {
                Debug.Log("No mesh Indices present");
                return;
            }

            bounds = new Bounds(Vector3.zero, new Vector3(10000.0f, 10000.0f, 10000.0f));

            Graphics.DrawMeshInstancedIndirect(Mesh, 0, Material, bounds,
                argsBuffer, 0, null, shadowCasting, receiveShadows);
        }

        public void Dispose()
        {
            if (gpuBuffer != null) gpuBuffer.Dispose();
            gpuBuffer = null;
            if (argsBuffer != null) argsBuffer.Dispose();
            argsBuffer = null;
        }
    }
}