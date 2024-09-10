using UnityEngine;
using System.Linq;
using System;
using Ara3D.Collections;
using Ara3D.Geometry;
using Ara3D.Mathematics;
using Matrix4x4 = Ara3D.Mathematics.Matrix4x4;
using Mesh = UnityEngine.Mesh;
using Vector3 = Ara3D.Mathematics.Vector3;

namespace Ara3D.UnityBridge
{
    /// <summary>
    /// A copy of the Topology, UVs, Vertices, Colors, and Normals, of a Unity mesh.
    /// This is done in a form that is serializable
    /// TODO: finish the implementation.
    /// TODO: decide whether this should be immutable or not.
    /// TODO: decide whether this should implement IRenderMesh
    /// TODO: consider uvs1 through 8, tangents, and boneWeights
    /// https://docs.unity3d.com/ScriptReference/BoneWeight.html
    /// </summary>
    public class UnityTriMesh : ITriMesh, IDeformable<UnityTriMesh>
    {
        public UnityEngine.Vector2[] UnityUVs;
        public UnityEngine.Vector3[] UnityVertices;
        public UnityEngine.Vector3[] UnityNormals;
        public IArray<int> Indices => UnityIndices.ToIArray();
        public int[] UnityIndices;
        public Color32[] UnityColors;
        public IArray<Int3> FaceIndices => Indices.SelectTriplets((a, b, c) => new Int3(a, b, c));
        public IArray<Vector3> Points => UnityVertices.ToIArray().Select(v => v.ToAra3D());

        public UnityTriMesh()
        { }

        public UnityTriMesh(UnityTriMesh other)
        {
            CopyFrom(other);
        }

        public UnityTriMesh Clone()
        {
            return new UnityTriMesh(this);
        }

        public void CopyFrom(UnityTriMesh other)
        {
            UnityIndices = other.UnityIndices;
            UnityUVs = other.UnityUVs?.ToArray();
            UnityVertices = other.UnityVertices?.ToArray();
            UnityColors = other.UnityColors?.ToArray();
            UnityNormals = other.UnityNormals?.ToArray();
        }

        public void CopyFrom(Mesh mesh)
        {
            UnityIndices = mesh.triangles;
            UnityUVs = mesh.uv?.ToArray();
            UnityVertices = mesh.vertices?.ToArray();
            UnityColors = mesh.colors32?.ToArray();
            UnityNormals = mesh.normals?.ToArray();
        }

        public UnityTriMesh(Mesh mesh)
        {
            CopyFrom(mesh);
        }

        public void AssignToMesh(Mesh mesh)
        {
            mesh.Clear();
            if (UnityVertices != null) mesh.vertices = UnityVertices;
            if (UnityIndices != null) mesh.triangles = UnityIndices;
            if (UnityUVs != null) mesh.uv = UnityUVs;
            if (UnityColors != null) mesh.colors32 = UnityColors;
            if (UnityNormals != null)
            {
                mesh.normals = UnityNormals;
            }
            else
            {
                mesh.RecalculateNormals();
            }
        }

        public UnityTriMesh Transform(Matrix4x4 mat)
            => Deform(v => v.Transform(mat));

        public UnityTriMesh Deform(Func<Vector3, Vector3> f)
            => new UnityTriMesh(this)
            {
                UnityVertices = UnityVertices
                    .Select(v => f(v.ToAra3D()).ToUnityFromAra3D())
                    .ToArray()
            };

        IGeometry ITransformable<IGeometry>.Transform(Matrix4x4 mat)
            => Transform(mat);
        
        IGeometry IDeformable<IGeometry>.Deform(Func<Vector3, Vector3> f)
            => Deform(f);

        ITriMesh ITransformable<ITriMesh>.Transform(Matrix4x4 mat)
            => Transform(mat);

        ITriMesh IDeformable<ITriMesh>.Deform(Func<Vector3, Vector3> f)
            => Deform(f);
    }
}
