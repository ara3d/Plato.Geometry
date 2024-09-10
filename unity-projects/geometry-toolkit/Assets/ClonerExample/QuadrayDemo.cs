using System.Linq;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class QuadrayDemo : MonoBehaviour
    {
        public Mesh Mesh;
        public Material Material;
        public float Scale = 1;
        public float A = 0;
        public float B = 1;
        public float C = 1;
        public float D = 2;

        public bool DrawMesh;
        private Mesh _createdMesh;

        public void Update()
        {
            var points = new[] { A, B, C, D }.Permutations().Select(p => (Vector3)(Quadray)p).ToArray(); 
            if (Mesh == null) Mesh = QuadrayExtensions.Tetrahedron;
            foreach (var point in points)
            {
                Graphics.DrawMesh(Mesh, point * Scale, Quaternion.identity, Material, 0);
            }

            if (DrawMesh)
            {
                var indices = 3
                    .ChooseFrom(points.Length)
                    .SelectMany(p => p.Reverse())
                    .ToArray();

                _createdMesh = new Mesh()
                {
                    vertices = points,
                    triangles = indices,
                };
                _createdMesh.RecalculateNormals();
                Graphics.DrawMesh(_createdMesh, Vector3.zero, Quaternion.identity, Material, 0);
            }
        }
    }
}