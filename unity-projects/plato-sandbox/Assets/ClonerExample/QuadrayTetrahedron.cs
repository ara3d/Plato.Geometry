using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class QuadrayTetrahedron : MonoBehaviour
    {
        public Mesh EdgeMesh;
        public Mesh PointMesh;
        public Material EdgeMaterial;
        public Material PointMaterial;
        public bool Reflect; 
        public bool DrawPoints = true;
        public bool DrawEdges = true;
        public float XYScale = 0.1f;
        public bool DrawMesh = true;

        public Transform CartesianTarget;
        public Vector3 CartesianPoint;
        public Vector4 Simplical;
        public Vector3 SimplicalToCartesian;

        public void Update()
        {
            var m = GetComponent<Transform>().localToWorldMatrix;

            var points = Quadray.Units.Select(q => m.MultiplyPoint(q.Vector3)).ToList();
            var edges = ComputeTetrahedralEdges(points);
            DrawWireframeMesh(points, edges);

            if (Reflect)
            {
                points = Quadray.Units.Select(q => m.MultiplyPoint(-q.Vector3)).ToList();
                edges = ComputeTetrahedralEdges(points);
                DrawWireframeMesh(points, edges);
            }

            if (CartesianTarget != null)
            {
                CartesianPoint = CartesianTarget.position;
                Simplical = Quadray.ToQuadray(CartesianPoint);
                SimplicalToCartesian = ((Quadray)Simplical).Vector3;
            }
        }

        public IReadOnlyList<(Vector3, Vector3)> ComputeTetrahedralEdges(IReadOnlyList<Vector3> points)
            => new[]
            {
                (points[0], points[1]),
                (points[0], points[2]),
                (points[0], points[3]),
                (points[1], points[2]),
                (points[1], points[3]),
                (points[2], points[3]),
            };

        public void DrawWireframeMesh(IReadOnlyList<Vector3> points, IReadOnlyList<(Vector3, Vector3)> edges)
        {
            if (DrawPoints)
                foreach (var p in points)
                    Graphics.DrawMesh(PointMesh, Matrix4x4.Translate(p), PointMaterial, 0);
            
            if (DrawEdges)
                foreach (var e in edges)
                    Graphics.DrawMesh(EdgeMesh, UnityExtensions.GetAlignmentMatrix(EdgeMesh.bounds, e.Item1, e.Item2, XYScale), EdgeMaterial, 0);

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