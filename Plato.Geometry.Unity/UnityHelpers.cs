using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Plato.Geometry.Unity
{
    public static class UnityHelpers
    {
        public static Material CreateStandardMaterial(Color color, float metallic = 0.5f, float glossiness = 0.7f)
        {
            // Find the Standard Shader
            var standardShader = Shader.Find("Standard");
            if (standardShader == null)
            {
                Debug.LogError("Standard Shader not found. Make sure it is included in the project.");
                return null;
            }

            var newMaterial = new Material(standardShader);
            newMaterial.SetColor("_Color", color);
            newMaterial.SetFloat("_Metallic", metallic);
            newMaterial.SetFloat("_Glossiness", glossiness);

            // If alpha less than one, we will make the material transparent
            if (color.a < 1f)
            {
                SetMaterialToTransparent(newMaterial);
            }

            return newMaterial;
        }

        public static void SetMaterialToOpaque(Material material)
        {
            material.SetFloat("_Mode", 0); // Opaque
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }

        public static void SetMaterialToTransparent(Material material)
        {
            material.SetFloat("_Mode", 3); // Transparent
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }

    }
}
