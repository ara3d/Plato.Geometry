using System.Collections.Generic;
using Ara3D.Mathematics;
using Ara3D.UnityBridge;
using Assets.ClonerExample;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Ara3D.ProceduralGeometry.Unity
{
    [ExecuteAlways]
    public class SceneRenderer : MonoBehaviour
    {
        public Material Material;
        public Material TransparentMaterial;
        public UnityMeshScene Scene;
        public ShadowCastingMode ShadowCastingMode = ShadowCastingMode.Off;
        public bool ReceiveShadows = false;
        public float Metallic = 0.5f;
        public float Smoothnness = 0.5f;
        public int NumberOfInstanceSets;
        public int NumberOfInstances;
        public int NumberOfTriangles;

        private List<CloneRenderData> drawers = new List<CloneRenderData>();
        
        public void Update()
        {
            foreach (var drawer in drawers)
            {
                drawer.Render(ShadowCastingMode, ReceiveShadows);
            }
        }

        public CloneRenderData CreateClones(Mesh mesh, Color color, IReadOnlyList<Decomposition> transforms)
        {
            var r = new CloneRenderData();

            // TODO: use transparent material if color alpha is less than 1. 

            var data = new NativeArray<GpuInstanceData>(transforms.Count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
            for (var i = 0; i < transforms.Count; i++)
            {
                var t = transforms[i];
                data[i] = new GpuInstanceData
                {
                    Color = color.ToFloat4(),
                    Id = (uint)i,
                    Pos = t.Translation.ToUnityFromVim(),
                    Orientation = t.Rotation.ToUnity(),
                    Scl = t.Scale.ToUnityScale(), 

                    // TODO: pick this up from the materials in the file
                    Metallic = Metallic,
                    Smoothness = Smoothnness,
                };
            }

            if (color.a < 0.95f)
            {
                r.UpdateGpuData(mesh, data, new Material(TransparentMaterial));
            }
            else
            {
                r.UpdateGpuData(mesh, data, new Material(Material));
            }

            return r;
        }

        public void Init(UnityMeshScene scene)
        {
            drawers.Clear();
            NumberOfInstances = 0;
            NumberOfTriangles = 0;
            foreach (var set in scene.InstanceSets)
            {
                // If there are no instances, skip it
                if (set.Transforms.Count <= 0)
                    continue;

                // If there are no faces, skip it
                if (set.TriMesh.FaceIndices.Count == 0)
                    continue;

                var mesh = new Mesh();
                set.TriMesh.AssignToMesh(mesh);
                drawers.Add(CreateClones(mesh, set.Color, set.Transforms));
                NumberOfInstances += set.Transforms.Count;
                NumberOfTriangles += (set.Transforms.Count) * (mesh.triangles.Length / 3);
            }
            Scene = scene;
            NumberOfInstanceSets = drawers.Count;
        }
    }
}