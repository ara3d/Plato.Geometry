using System.Linq;
using Plato.Geometry.Scenes;
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

        public static void SetScene(IScene scene)
        {
            ClearScene();
            AddScene(scene);
        }

        public static void AddScene(IScene scene)
        {
            var po = new GameObject("PlatoSceneRoot");
            po.AddComponent<PlatoSceneRoot>();
            Create(scene.Root, po);
        }

        public static GameObject Create(ISceneNode obj, GameObject parent)
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

            if (obj is LineObject lo)
            {
                var ld = r.AddComponent<PlatoLineDrawer>();
                ld.LineObject = lo;
            }

            // TODO: add a PlatoMeshDrawer
            // TODO: figure out how to represent and render instances efficiently. 
            // TODO: properly map materials. 

            return r;
        }
    }
}