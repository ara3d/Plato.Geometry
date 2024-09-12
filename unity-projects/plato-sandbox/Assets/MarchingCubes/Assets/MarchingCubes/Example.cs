using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

using ProceduralNoiseProject;
using Common.Unity.Drawing;

namespace MarchingCubesProject
{

    public enum MarchingMode {  CUBES, TETRAHEDRON };

    public class Example : MonoBehaviour
    {

        public Material material;

        public MarchingMode mode = MarchingMode.CUBES;

        public int seed = 0;

        public bool smoothNormals = false;

        public bool drawNormals = false;

        private List<GameObject> meshes = new List<GameObject>();

        private NormalRenderer normalRenderer;

        void Start()
        {

            INoise perlin = new PerlinNoise(seed, 1.0f);
            var fractal = new FractalNoise(perlin, 3, 1.0f);

            //Set the mode used to create the mesh.
            //Cubes is faster and creates less verts, tetrahedrons is slower and creates more verts but better represents the mesh surface.
            
            //Marching marching = null;
            //if(mode == MarchingMode.TETRAHEDRON)
                var marching = new MarchingTetrahedron();
//            else
  //              marching = new MarchingCubes();

            //Surface is the value that represents the surface of mesh
            //For example the perlin noise has a range of -1 to 1 so the mid point is where we want the surface to cut through.
            //The target value does not have to be the mid point it can be any value with in the range.
            //marching.Surface = 0.0f;

            //The size of voxel array.
            var width = 32;
            var height = 32;
            var depth = 32;

            var voxels = new VoxelArray(width, height, depth);

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

                        voxels[x,y,z] = fractal.Sample3D(u, v, w);
                    }
                }
            }

            var verts = new List<Vector3>();
            var normals = new List<Vector3>();

            //The mesh produced is not optimal. There is one vert for each index.
            //Would need to weld vertices for better quality mesh.
            marching.Generate(voxels.Voxels, verts);

            var indices = Enumerable.Range(0, verts.Count).ToList();
            //Create the normals from the voxel.

            if (smoothNormals)
            {
                for (var i = 0; i < verts.Count; i++)
                {
                    //Presumes the vertex is in local space where
                    //the min value is 0 and max is width/height/depth.
                    var p = verts[i];

                    var u = p.x / (width - 1.0f);
                    var v = p.y / (height - 1.0f);
                    var w = p.z / (depth - 1.0f);

                    var n = voxels.GetNormal(u, v, w);

                    normals.Add(n);
                }

                normalRenderer = new NormalRenderer();
                normalRenderer.DefaultColor = Color.red;
                normalRenderer.Length = 0.25f;
                normalRenderer.Load(verts, normals);
            }

            var position = new Vector3(-width / 2, -height / 2, -depth / 2);

            CreateMesh32(verts, normals, indices, position);

        }

        private void CreateMesh32(List<Vector3> verts, List<Vector3> normals, List<int> indices, Vector3 position)
        {
            var mesh = new Mesh();
            mesh.indexFormat = IndexFormat.UInt32;
            mesh.SetVertices(verts);
            mesh.SetTriangles(indices, 0);

            if (normals.Count > 0)
                mesh.SetNormals(normals);
            else
                mesh.RecalculateNormals();

            mesh.RecalculateBounds();

            var go = new GameObject("Mesh");
            go.transform.parent = transform;
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            go.GetComponent<Renderer>().material = material;
            go.GetComponent<MeshFilter>().mesh = mesh;
            go.transform.localPosition = position;

            meshes.Add(go);
        }

        private void Update()
        {
            //transform.Rotate(Vector3.up, 10.0f * Time.deltaTime);
        }

        private void OnRenderObject()
        {
            if(normalRenderer != null && meshes.Count > 0 && drawNormals)
            {
                var m = meshes[0].transform.localToWorldMatrix;

                normalRenderer.LocalToWorld = m;
                normalRenderer.Draw();
            }
            
        }

    }

}
