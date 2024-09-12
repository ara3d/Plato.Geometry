using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class UtilMoleculeLoader : MonoBehaviour
    {
        public CloneRenderData RenderData;
        public Material Material;
        public Mesh Mesh;
        public CloneAppearance Appearance;
        public string FileName = @"C:\Users\cdigg\git\3d-format-shootout\data\files\cif\sarscov2_1.cif";
        public float Scale = 1f;
        public ShadowCastingMode ShadowCastingMode = ShadowCastingMode.On;
        public bool ReceiveShadows = true;
        public bool Recompute;

        public bool Reload;
        public NativeArray<GpuInstanceData> GpuInstances;

        public Dictionary<string, Color> IdToColor = new Dictionary<string, Color>();
        private List<Vector3> points = new List<Vector3>();
        private List<string> Ids = new List<string>();

        [Range(0, 1)] public float Metallic = 0.5f;
        [Range(0, 1)] public float Smoothness = 0.5f;
        [Range(0, 1)] public float Saturation = 0.5f;
        [Range(0, 1)] public float Brightness = 0.5f;

        public JobHandle Schedule(ICloneJob previous)
        {
            return default;
        }

        public void OnValidate()
        {
            Recompute = true;
        }
        
        public void Update()
        {
            if (RenderData == null)
                RenderData = new CloneRenderData();

            if (Reload)
            {
                Reload = false;
                Recompute = true;

                var lines = File.ReadAllLines(FileName);
                points.Clear();
                foreach (var line in lines)
                {
                    if (line.StartsWith("HETATM"))
                    {
                        var entries = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        var x = float.Parse(entries[10]);
                        var y = float.Parse(entries[11]);
                        var z = float.Parse(entries[12]);
                        points.Add(new Vector3(x, y, z));
                        var id = $"{entries[2]}:{entries[3]}";
                        IdToColor[id] = Color.black;
                        Ids.Add(id);
                    }
                }

                var hueDelta = 1f / IdToColor.Count;
                var hue = 0f;
                foreach (var key in IdToColor.Keys.ToList())
                {
                    IdToColor[key] = Color.HSVToRGB(hue, Saturation, Brightness);
                    hue += hueDelta;
                }

                GpuInstances.Resize(points.Count);
            }

            if (Recompute)
            {
                Recompute = false;

                for (var i = 0; i < points.Count; i++)
                {
                    var id = Ids[i];
                    var color = IdToColor[id].ToFloat4();
                    GpuInstances[i] = new GpuInstanceData()
                    {
                        Color = color,
                        Id = (uint)i,
                        Metallic = Appearance.Metallic,
                        Orientation = quaternion.identity,
                        Pos = points[i],
                        Scl = new float3(Scale, Scale, Scale),
                        Smoothness = Appearance.Smoothness,
                    };
                }
                RenderData.UpdateGpuData(Mesh, GpuInstances, Material);
            }
            RenderData.Render(ShadowCastingMode, ReceiveShadows);
        }
        
        private void OnDisable()
        {
            RenderData.Dispose();
        }

    }
    
}