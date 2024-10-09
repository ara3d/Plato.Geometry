using System;
using System.Linq;
using Plato.Geometry.Scenes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Plato.Geometry.Unity
{
    public static class UnityApi
    {
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

            var id = 0;
            var rootGameObject = Create(scene.Root, po, ref id);
            rootGameObject.name = "Scene";

            // Rotation to align Z-up to Y-up
            po.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            // Adjust for the right-handed to left-handed coordinate system
            po.transform.localScale = new Vector3(1, 1, -1);
        }

        public static GameObject Create(ISceneNode node, GameObject parent, ref int id)
        {
            var r = new GameObject
            {
                name = node.Name
            };

            r.transform.SetParent(parent.transform);

            // TODO: this does not support nested transforms! 
            // In Plato, we assume all transforms are in world space. 
            if (!node.Transform.IsIdentity())
                node.Transform.Matrix.ApplyLocalTransformToUnity(r.transform);

            foreach (var obj in node.Objects)
            {
                Create(obj, r, ref id);
            }

            foreach (var child in node.Children)
            {
                Create(child, r, ref id);
            }

            return r;
        }

        public static GameObject Create(ISceneObject obj, GameObject parent, ref int id)
        {
            var r = new GameObject();

            r.transform.SetParent(parent.transform);

            if (obj is SceneLine lo)
            {
                r.name = $"Line {id++}";
                var ld = r.AddComponent<PlatoLineDrawer>();
                ld.LineObject = lo;
            }

            if (obj is SceneMesh sm)
            {
                r.name = $"Mesh {id++}";
                var md = r.AddComponent<PlatoMeshDrawer>();
                md.MeshObject = sm;
            }

            return r;
        }
    }
}