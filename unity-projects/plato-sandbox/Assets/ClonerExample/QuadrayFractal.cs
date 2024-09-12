using System.Collections.Generic;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class QuadrayFractal : MonoBehaviour
    {
        public Mesh Mesh;
        public Material Material;
        public float MeshScaling = 0.1f;
        public int InitialDistance = 10;
        public float ScaleFactor = 0.8f;
        public int Iterations = 4;

        public void AddPoints(List<Quadray> list, Quadray pos, float scale, int curIteration)
        {
            list.Add(pos);
            if (curIteration == Iterations) 
                return;
            foreach (var v in Quadray.Units)
                AddPoints(list, pos + v * scale, scale * ScaleFactor, curIteration + 1);
        }

        public void Update()
        {
            var scaledMesh = Mesh.Transform(Matrix4x4.Scale(Vector3.one * MeshScaling));
            var list = new List<Quadray>();
            AddPoints(list, Quadray.Zero, InitialDistance, 0);
            Debug.Log($"{list.Count} points");
            foreach (var p in list)
            {
                Graphics.DrawMesh(scaledMesh, p.Vector3, Quaternion.identity, Material, 0);
            }
        }
    }
}