using System;
using System.Collections.Generic;
using Assets.ClonerExample;
using Unity.Collections;
using UnityEngine;

namespace Assets
{
    public class Clones
    {
        public NativeArray<GpuInstanceData> GpuInstances;
        public CloneRenderData RenderData;
        private Material CopiedMaterial;

        public void Update(Mesh mesh, Material material, int count, IEnumerable<GpuInstanceData> gpuInstances)
        {
            if (RenderData == null)
                RenderData = new CloneRenderData();
            if (CopiedMaterial == null)
                CopiedMaterial = new Material(material);
            GpuInstances.Resize(count);
            var i = 0;
            foreach (var inst in gpuInstances)
                GpuInstances[i++] = inst;
            if (i != count)
                throw new Exception("Number of gpu instances does not match expected count");
            RenderData.UpdateGpuData(mesh, GpuInstances, CopiedMaterial);
        }

        public void Dispose()
        {
            CopiedMaterial = default;
            GpuInstances.SafeDispose();
            RenderData?.Dispose();
            GpuInstances = default;
            RenderData = default;
        }

        public bool IsValid
            => CopiedMaterial != null && RenderData != null && GpuInstances.IsCreated;
    }
}