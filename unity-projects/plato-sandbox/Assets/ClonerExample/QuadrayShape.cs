using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Graphics = UnityEngine.Graphics;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class QuadrayShape : MonoBehaviour
    {
        public Mesh EdgeMesh;
        public Mesh PointMesh;
        public Material EdgeMaterial;
        public Material PointMaterial;
        public bool Reflect;
        public bool DrawPoints = true;
        public bool DrawEdges = true;
        public float XYEdgeScale = 0.1f;
        public float ScalePoint = 0.2f;
        public bool DrawMesh = true;
        public float Length1 = 3.0f;
        public float Length2 = 1.0f;
        public bool UseSpread = true;
        public bool IncludeTips = false;

        public float SpreadLength1  => UseSpread ? Length1 * Length1 : Length1;
        public float SpreadLength2 => UseSpread ? Length2 * Length2 : Length2;

        public List<List<Vector3>> PointListList = new List<List<Vector3>>();
        public List<(Vector3, Vector3)> Edges = new List<(Vector3, Vector3)>();
        public List<Vector3> Points = new List<Vector3>();

        public void Update()
        {
            PointListList.Clear();
            var m = GetComponent<Transform>().localToWorldMatrix;
            Edges.Clear();
            Points.Clear();

            var vecs = Quadray.Units;
            for (var i = 0; i < vecs.Length; i++)
            {
                var vec = vecs[i] * SpreadLength1;
                var pts = new List<Vector3>();

                var edgePts = new List<Vector3>();

                for (var j = 0; j < vecs.Length; j++)
                {
                    var vec2 = vec + (vecs[j] * SpreadLength2);
                    pts.Add(vec2);

                    if (!IncludeTips && i == j)
                        continue;
                    edgePts.Add(vec2);
                    Points.Add(vec2);
                }

                PointListList.Add(pts);

                var edges = ComputeEdges(edgePts);
                
                Edges.AddRange(edges);
            }

            var edges2 = new List<(Vector3, Vector3)>();
            for (var i = 0; i < PointListList.Count; i++)
            {
                var pointsA = PointListList[i];
                var pointsB = PointListList[(i + 1) % 4];

                var pA = pointsA[(i + 1) % 4];
                var pB = pointsB[(i) % 4];
                edges2.Add((pA, pB));

                var pointsC = PointListList[(i + 2) % 4];
                
                var pA1 = pointsA[(i + 2) % 4];
                var pC = pointsC[(i + 4) % 4];
                edges2.Add((pA1, pC));
            }

            DrawPointMeshes(Points);
            DrawEdgeMeshes(Edges);
            DrawEdgeMeshes(edges2);
        }

        public void DrawEdgeMeshes(IReadOnlyList<(Vector3, Vector3)> edges)
        {
            if (DrawEdges)
                foreach (var e in edges)
                    Graphics.DrawMesh(EdgeMesh, UnityExtensions.GetAlignmentMatrix(EdgeMesh.bounds, e.Item1, e.Item2, XYEdgeScale), EdgeMaterial, 0);
        }

        public IReadOnlyList<(Vector3, Vector3)> ComputeEdges(IReadOnlyList<Vector3> points)
        {
            if (points.Count == 4)
            {
                return new[]
                {
                    (points[0], points[1]),
                    (points[0], points[2]),
                    (points[0], points[3]),
                    (points[1], points[2]),
                    (points[1], points[3]),
                    (points[2], points[3]),
                };
            }

            if (points.Count == 3)
            {
                return new[]
                {
                    (points[0], points[1]),
                    (points[0], points[2]),
                    (points[1], points[2]),
                };
            }

            if (points.Count == 2)
            {
                return new[]
                {
                    (points[0], points[1]),
                };
            }

            throw new Exception($"Invalid number of points {points.Count}");
        }

        public void DrawPointMeshes(IReadOnlyList<Vector3> points)
        {
            if (DrawPoints)
                foreach (var p in points)
                    Graphics.DrawMesh(PointMesh, Matrix4x4.TRS(p, Quaternion.identity, Vector3.one * ScalePoint), PointMaterial, 0);
        }

        public void DrawWireframeMesh(IReadOnlyList<Vector3> points, IReadOnlyList<(Vector3, Vector3)> edges)
        {
            DrawPointMeshes(points);
            DrawEdgeMeshes(edges);

            if (DrawMesh)
            {
                var indices = new[]
                {
                    2, 1, 0,
                    0, 1, 3,
                    3, 2, 0,
                    1, 2, 3,
                };
                var mesh = new Mesh()
                {
                    vertices = points.ToArray(),
                    triangles = indices, // .Concat(indices.Reverse()).ToArray()
                };
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                Graphics.DrawMesh(mesh, Matrix4x4.identity, PointMaterial, 0);
            }
        }
    }
}