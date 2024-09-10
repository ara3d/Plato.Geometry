using System;
using System.Collections.Generic;
using System.Linq;
using Ara3D.Collections;
using Ara3D.Geometry;
using Ara3D.Mathematics;
using Ara3D.UnityBridge;
using Filters;
using UnityEngine;
using IPoints = Ara3D.Geometry.IPoints;
using Matrix4x4 = Ara3D.Mathematics.Matrix4x4;
using Vector2 = Ara3D.Mathematics.Vector2;
using Vector3 = Ara3D.Mathematics.Vector3;

namespace Assets.ClonerExample
{
    // Render:
    // points (cube, sphere, icosahederon, tetrahedron,arrow)
    // lines [polyline/line] (cube, cylinder, arrow),
    // meshes 
    // meshes as (points + lines)
    // voxels
    // bitmaps
    // Functions (distance fields / vector fields / curves / maps) 
    // - suggests we need a sample as  

    [ExecuteAlways]
    public class FilterRender : MonoBehaviour
    {
        public BaseFilterComponent Source;

        public enum LineMeshEnum
        {
            None,
            Box,
            Prism,
            Cylinder, 
            PrismArrow,
            CylinderArrow,
            Custom,
        }

        public enum PointMeshEnum
        {
            None,
            Triangle,
            Square,
            Tetrahedron,
            Cube,
            Octahederon,
            Icosahedron,
            Cylinder,
            Custom,
        }

        public BaseFilterComponent Filter;
        public Material Material;
        public Mesh InstanceMesh;
        public bool ZUp;
        public bool FlipTriangles;
        public bool DoubleSided;

        // 
        public bool RenderInWorldSpace; 
        
        public int NumCurveSamples = 100;
        
        public PointMeshEnum PointMesh;
        public Vector3 PointMeshScale = Vector3.One;
        public Mesh CustomPointMesh;

        public LineMeshEnum LineMesh;
        public Vector3 LineMeshScale = Vector3.One;
        public Mesh CustomLineMesh;

        // public useBoxesOrSpheresForPoints; 

        public class TransformedMesh
        {
            public Mesh Mesh;
            public UnityEngine.Matrix4x4 Matrix;
            public TransformedMesh(Mesh mesh)
                : this (mesh, UnityEngine.Matrix4x4.identity)
            { }
            public TransformedMesh(Mesh mesh, UnityEngine.Matrix4x4 matrix)
            {
                Mesh = mesh;
                Matrix = matrix;
            }
        }

        public void Update()
        {
            var comp = Filter != null ? Filter : this.GetPreviousComponent<BaseFilterComponent>();

            if (comp == null)
            {
                Debug.Log($"No filter to evaluate");
                return;
            }

            object val = comp.Eval();
            var meshes = ToUnityMeshes(val);

            var rp = new RenderParams(Material);
            foreach (var tm in meshes)
            {
                Graphics.RenderMesh(rp, tm.Mesh, 0, tm.Matrix * transform.localToWorldMatrix);
            }
        }


        public TransformedMesh ToUnityMesh(object obj)
        {
            if (obj is ITriMesh m)
                return new TransformedMesh(
                    m.ToUnity(ZUp, FlipTriangles, DoubleSided));
            if (obj is Matrix4x4 mat)
                return new TransformedMesh(InstanceMesh, mat.ToUnityRaw());
            if (obj is Vector2 vec2)
                return new TransformedMesh(InstanceMesh,
                    UnityEngine.Matrix4x4.Translate(ZUp ? vec2.ToVector3().ToUnityFromAra3D() : vec2.ToVector3().ToUnity()));
            if (obj is Vector3 vec)
                return new TransformedMesh(InstanceMesh,
                    UnityEngine.Matrix4x4.Translate(ZUp ? vec.ToUnityFromAra3D() : vec.ToUnity()));
            throw new Exception($"Could not convert {obj} to TransformedMesh");
        }

        public IEnumerable<TransformedMesh> ToUnityMeshes(object obj)
        {
            if (obj is List<object> list)
                return list.SelectMany(ToUnityMeshes);
            if (obj is IPoints points && !(obj is ITriMesh))
                return points.Points.ToEnumerable().Select(p => ToUnityMesh(p));
            if (obj is IPolyLine2D polyLine)
                return polyLine.Points.ToEnumerable().Select(p => ToUnityMesh(p));

            // TODO: handle, lines, points, transforms, strands, curves. 
            return new[] { ToUnityMesh(obj) };
        }
    }
}