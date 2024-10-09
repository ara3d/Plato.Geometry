using Plato.Geometry.Scenes;
using UnityEngine;

namespace Plato.Geometry.Unity
{
    [ExecuteAlways]
    public class PlatoSceneComponent : MonoBehaviour
    {
        public PlatoSceneRoot Root { get; private set; }

        public PlatoSceneRoot FindRoot()
        {
            var root = transform;
            while (root != null)
            {
                var platoRoot = root.gameObject.GetComponent<PlatoSceneRoot>();
                if (platoRoot != null)
                    return platoRoot;
                root = root.parent;
            }
            return null;
        }

        public void Awake()
        {
            Root = FindRoot();
        }
    }

    /// <summary>
    /// Identifies the root object in the scene 
    /// </summary>
    public class PlatoSceneRoot : PlatoSceneComponent
    {
    }

    /// <summary>
    /// Used for drawing lines
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class PlatoLineDrawer : PlatoSceneComponent
    {
        public SceneLine LineObject;

        public void Start()
        {
            var lr = gameObject.GetComponent<LineRenderer>();
            lr.startWidth = (float)LineObject.Width;
            lr.endWidth = lr.startWidth;
            lr.useWorldSpace = false;
            lr.startColor = LineObject.Material.Color.ToUnity();
            lr.endColor = lr.startColor;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            UpdatePositions();
        }

        public void UpdatePositions()
        {
            var lr = GetComponent<LineRenderer>();
            lr.positionCount = LineObject.Points.Count;
            var i = 0;
            foreach (var point in LineObject.Points)
            {
                lr.SetPosition(i++, point.ToUnity());
            }

            if (LineObject.Closed)
            {
                lr.loop = true;
            }
        }
    }

    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class PlatoMeshDrawer : PlatoSceneComponent
    {
        public SceneMesh MeshObject;

        public void Start()
        {
            var mf = gameObject.GetComponent<MeshFilter>();
            var mr = gameObject.GetComponent<MeshRenderer>();
            mf.sharedMesh = MeshObject.Mesh.ToUnity();
            mr.sharedMaterial = new Material(Shader.Find("Standard")) { color = MeshObject.Material.Color.ToUnity() };
        }
    }
}