  using System.Collections.Generic;
using System.Linq;
using ProceduralNoiseProject;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace MarchingCubesProject
{
    [ExecuteAlways]
    public class MarchingDemo : MonoBehaviour
    {
        public int seed = 0;

        public MarchingTetrahedron marching  = new ();
        public bool smoothNormals = false;

        public List<Vector3> verts = new List<Vector3>();
        public List<Vector3> normals = new List<Vector3>();
        public List<int> indices = new List<int>();

        public MarchingTetrahedron Algorithm;
        public float Amplitude = 1f;
        public float Frequency = 1f;
        public int Octaves = 3;
        public float FractalAmplitude = 1f;


        public int width = 20;
        public int height = 20;
        public int depth = 20;

        public VoxelArray voxels;
        public PerlinNoise perlin;
        public FractalNoise fractal;

        public void Update()
        {
            if (perlin == null || perlin.Seed != seed  || perlin.Amplitude != Amplitude)
                perlin = new PerlinNoise(seed, Amplitude);
            if (fractal == null || fractal.Octaves != Octaves || fractal.Frequency != Frequency || FractalAmplitude != Amplitude)
                fractal = new FractalNoise(perlin, Octaves, Frequency, FractalAmplitude);
            if (voxels == null || voxels.Width != width || voxels.Height != height || voxels.Depth != depth)
                voxels = new VoxelArray(width, height, depth);

            //Fill voxels with values. Im using perlin noise but any method to create voxels will work.
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    for (var z = 0; z < depth; z++)
                    {
                        var u = x / (width - 1.0f);
                        var v = y / (height - 1.0f);
                        var w = z / (depth - 1.0f);

                        voxels[x, y, z] = fractal.Sample3D(u, v, w);
                    }
                }
            }

            verts.Clear();
            normals.Clear();

            // /Would need to weld vertices for better quality mesh.
            marching.Generate(voxels.Voxels, verts);

            if (smoothNormals)
            {
                for (var i = 0; i < verts.Count; i++)
                {
                    // Presumes the vertex is in local space where
                    // the min value is 0 and max is width/height/depth.
                    var p = verts[i];
                    var u = p.x / (width - 1.0f);
                    var v = p.y / (height - 1.0f);
                    var w = p.z / (depth - 1.0f);
                    var n = voxels.GetNormal(u, v, w);
                    normals.Add(n);
                }
            }

            var meshFilter = GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                var mesh = meshFilter.sharedMesh;
                mesh.SetVertices(verts);
                var indices = Enumerable.Range(0, verts.Count).ToList();
                mesh.SetIndices(indices, MeshTopology.Triangles, 0);
                if (smoothNormals)
                    mesh.SetNormals(normals);
                else
                    mesh.RecalculateNormals();
            }
        }
    }
}