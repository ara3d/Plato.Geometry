using System.Linq;
using UnityEngine;

namespace Plato.Geometry.Unity
{
    public static class UnityApi
    {
        public static Material LineMaterial;

        public static void ClearScene()
        {
            var objects = Object.FindObjectsOfType(typeof(PlatoSceneRoot));
            foreach (var obj in objects.Cast<PlatoSceneRoot>())
                Object.DestroyImmediate(obj.gameObject);
        }

        public static void SetScene(PlatoScene scene)
        {
            ClearScene();
            AddScene(scene);
        }

        public static void AddScene(PlatoScene scene)
        {
            var po = new GameObject("PlatoSceneRoot");
            po.AddComponent<PlatoSceneRoot>();
            foreach (var so in scene.Objects)
            {
                Create(so, po);
            }
        }

        public static GameObject Create(PlatoSceneObject obj, GameObject parent)
        {
            var r = new GameObject();
            r.name = obj.Name;

            r.transform.SetParent(parent.transform);
            r.transform.localPosition = obj.Transform.Translation.ToUnity();
            r.transform.localRotation = obj.Transform.Rotation.ToUnity();
            r.transform.localScale = obj.Transform.Scale.ToUnity();

            foreach (var child in obj.Children)
            {
                Create(child, r);
            }

            foreach (var line in obj.Lines)
            {
                var ld = r.AddComponent<PlatoLineDrawer>();
                ld.LineData = line;
            }

            return r;
        }
    }
}