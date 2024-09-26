using System;
using System.Collections.Generic;
using System.Text;
using Plato.DoublePrecision;

namespace Plato.Geometry.Graphics
{
    public class Material
    {
        public Color Color;
        public double Transmission;
        public double Metallic;
        public double Roughness;
        public double Sheen;
        public double SheenTint;
        public bool UseSpecularReflectance = false;
        public double SpecularReflectance = 0.4;
        public double IndexOfRefraction;
        public double SpecularTint;
        public double SubsurfaceScattering;
        public Color SubsurfaceColor;

        public Material(Color color)
            => Color = color;

        public Material(int red, 
            int green, 
            int blue, 
            int alpha,            
            double transmission, 
            double metallic, 
            double roughness, 
            double sheen, 
            double sheenTint, 
            bool useSpecularReflectance, 
            double specularReflectance, 
            double indexOfRefraction, 
            double specularTint, 
            double subsurfaceScattering, 
            int subsurfaceRed,
            int subsurfaceGreen,
            int subsurfaceBlue)
        {
            Color = new Color(red / 255.0, green / 255.0, blue / 255.0, 1.0 - transmission);
            Metallic = metallic;
            Roughness = roughness;
            Sheen = sheen;
            SheenTint = sheenTint;
            UseSpecularReflectance = useSpecularReflectance;
            SpecularReflectance = specularReflectance;
            SpecularTint = specularTint;
            IndexOfRefraction = indexOfRefraction;
            SubsurfaceColor = new Color(subsurfaceRed / 255.0, subsurfaceGreen / 255.0, subsurfaceBlue / 255.0, 1.0);
            SubsurfaceScattering = subsurfaceScattering;
        }

        public static implicit operator Material(Color color) => new Material(color);
    }
}
