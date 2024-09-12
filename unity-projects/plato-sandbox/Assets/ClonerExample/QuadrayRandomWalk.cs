using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class QuadrayRandomWalk : MonoBehaviour
    {
        public Mesh Mesh;
        public Material Material;
        public int Length = 20;
        public int Walks = 20;
        public int Seed = 1;
        
        public static IEnumerable<Quadray> Wiggle(Quadray q, int n)
        {
            for (var i = 0; i < n; i++)
            {
                var index = Random.Range(0, 4);
                var dir = Quadray.Units[index];
                yield return q += dir;
            }
        }

        public void Update()
        {
            if (Mesh == null) Mesh = QuadrayExtensions.Tetrahedron;
            var points = new List<Quadray>();
            var seed = Seed; 
            for (var i = 0; i < Walks; i++)
            {
                Random.InitState(Seed + i);
                points.AddRange(Wiggle(Quadray.Zero, Length));
            }

            foreach (var p in points)
                Graphics.DrawMesh(Mesh, p, Quaternion.identity, Material, 0);
        }
    }
}