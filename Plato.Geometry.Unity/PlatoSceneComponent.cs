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
        // Rotation to align Z-up to Y-up
        private Quaternion targetRotation = Quaternion.Euler(-90f, 0f, 0f);

        public Material LineMaterial;

        public void Awake()
        {
            if (LineMaterial == null)
                LineMaterial = new Material(Shader.Find("Sprites/Default"));

            // Apply rotation to make Z-up
            transform.rotation = targetRotation * transform.rotation;
        }
    }

    /// <summary>
    /// Used for drawing lines
    /// </summary>
    public class PlatoLineDrawer : PlatoSceneComponent
    {
        public SceneLine LineObject;

        public void Start()
        {
            var lr = gameObject.AddComponent<LineRenderer>();
            lr.startWidth = (float)LineObject.Width;
            lr.endWidth = lr.startWidth;
            lr.useWorldSpace = false;
            lr.startColor = LineObject.Material.Color.ToUnity();
            lr.endColor = lr.startColor;
            lr.material = Root.LineMaterial;
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
}