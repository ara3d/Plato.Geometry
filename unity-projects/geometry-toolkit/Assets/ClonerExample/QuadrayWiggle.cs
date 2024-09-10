using System.Collections.Generic;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class QuadrayWiggle : MonoBehaviour
    {
        public Mesh Mesh;
        public Material Material;
        public int Count = 5;

        public static IEnumerable<Quadray> Wiggle(Quadray a, Quadray b, int n)
        {
            var r = Quadray.Zero;
            for (var i = 0; i < n; i++)
            {
                yield return r += a;
                yield return r += b;
            }
        }

        public void Update()
        {
            if (Mesh == null) Mesh = QuadrayExtensions.Tetrahedron;
            var points = new List<Quadray>();
            for (var i = 0; i < 4; i++)
            {
                var dir = Quadray.Units[i];
                for (var j = 0; j < 4; j++)
                {
                    if (i == j) continue;
                    var dir2 = Quadray.Units[j];
                    points.AddRange(Wiggle(dir, dir2, Count));
                }
            }

            foreach (var p in points)
                Graphics.DrawMesh(Mesh, p, Quaternion.identity, Material, 0);
        }
    }
}