using System.Diagnostics;
using Plato.SinglePrecision;

namespace Plato.Geometry.Graphics
{
    public class Material
    {
        public string Name;
        public Color Color;
        public float Transmission;
        public float Metallic;
        public float Roughness;
        public float Sheen;
        public float SheenTint;
        public bool UseSpecularReflectance = false;
        public float SpecularReflectance = 0.4;
        public float IndexOfRefraction;
        public float SpecularTint;
        public float SubsurfaceScattering;
        public Color SubsurfaceColor;

        public Material(Color color)
            => Color = color;

        public Material(
            int red, 
            int green, 
            int blue, 
            int alpha,            
            float transmission, 
            float metallic, 
            float roughness, 
            float sheen, 
            float sheenTint, 
            bool useSpecularReflectance, 
            float specularReflectance, 
            float indexOfRefraction, 
            float specularTint, 
            float subsurfaceScattering, 
            int subsurfaceRed,
            int subsurfaceGreen,
            int subsurfaceBlue,
            [System.Runtime.CompilerServices.CallerMemberName] string name = "")
        {
            Debug.Assert(alpha == 255, "Alpha channel is not currently supported");
            Color = new Color(red / 255.0f, green / 255.0f, blue / 255.0f, 1.0f - transmission);
            Metallic = metallic;
            Roughness = roughness;
            Sheen = sheen;
            SheenTint = sheenTint;
            UseSpecularReflectance = useSpecularReflectance;
            SpecularReflectance = specularReflectance;
            SpecularTint = specularTint;
            IndexOfRefraction = indexOfRefraction;
            SubsurfaceColor = new Color(subsurfaceRed / 255.0f, subsurfaceGreen / 255.0f, subsurfaceBlue / 255.0f, 1.0f);
            SubsurfaceScattering = subsurfaceScattering;
            Name = name;
        }

        public SpecularSettings GetSpecularSettings()
            => SpecularSettings.FromPBR(Color, Metallic, Roughness);

        public bool UseSpecular 
            => Metallic != 0 || Roughness != 0;

        public static implicit operator Material(Color color) => new Material(color);
    }

    public struct SpecularSettings
    {
        public readonly Color Color;
        public readonly float Power;
        public SpecularSettings(Color color, float power)
        {
            Color = color;
            Power = power;
        }

        public static Color DielectricSpecular = new Color(0.04f, 0.04f, 0.04f, 1.0f);

        public static SpecularSettings FromPBR(Color color, float metallic, float roughness)
            => new SpecularSettings(DielectricSpecular.Lerp(color, metallic), ((Number)1).Lerp(100, 1 - roughness));
    }
}
