using Ara3D.Serialization.VIM;
using Ara3D.UnityBridge;
using UnityEngine;

namespace Ara3D.ProceduralGeometry.Unity
{
    [ExecuteAlways]
    public class VimLoader : MonoBehaviour
    {
        public bool Load = false;
        public Material Material;
        public Material TransparentMaterial;

        public string File =
            @"C:\Users\cdigg\git\3d-format-shootout\data\files\vim\Wolford_Residence.r2023.om_v5.0.0.vim";

        public void Update()
        {
            if (Load)
            {
                Load = false;
                OpenFile();
            }
        }

        public void OpenFile()
        {
            Debug.Log($"Starting to load {File}");
            var vim = Serializer.Deserialize(File);

            var g = vim.Geometry;
            Debug.Log($"number of meshes {g.Meshes.Count}");
            Debug.Log($"number of vertices {g.Vertices.Count}");
            Debug.Log($"number of uvs {g.AllVertexUvs.Count}");
            Debug.Log($"number of sub-meshes {g.SubmeshIndexCount.Count}");
            Debug.Log($"number of faces {g.Indices.Count / 3}");
            Debug.Log($"number of instances {g.InstanceMeshes.Count}");
            Debug.Log($"number of materials {g.Materials.Count}");
            Debug.Log($"Finished loading {File}");

            var go = new GameObject();
            go.name = "VIM Scene Renderer";
            var scene = vim.ToUnity();
            var comp = go.AddComponent<SceneRenderer>();
            comp.Material = new Material(Material);
            comp.TransparentMaterial = new Material(TransparentMaterial);
            comp.Init(scene);
            Debug.Log("Finished initializing scene");
        }

    }
}
