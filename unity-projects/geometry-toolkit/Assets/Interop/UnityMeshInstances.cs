using System.Collections.Generic;
using Ara3D.Mathematics;
using UnityEngine;

namespace Ara3D.UnityBridge
{
    public class UnityMeshInstanceSet
    {
        public readonly UnityTriMesh TriMesh;
        public readonly Color Color;
        public readonly List<Decomposition> Transforms = new();

        public UnityMeshInstanceSet(UnityTriMesh triMesh, Color color)
        {
            TriMesh = triMesh;
            Color = color;
        }
    }
}