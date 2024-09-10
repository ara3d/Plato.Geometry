using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class UtilGenerateColorPalette : MonoBehaviour
    {
        [Range(0, 360)] public float Hue = 180;
        [Range(0, 1)] public float Saturation = 0.8f;

        public Color[] Colors;

        public void OnValidate()
        {
            Update();
        }

        public void Update()
        {
            Colors = GenerateTriadColors(Hue, Saturation);
        }

        public static Color[] GenerateTriadColors(float hueInDegrees, float saturationZeroToOne)
        {
            var hue0 = (hueInDegrees / 360);
            var hue1 = ((hueInDegrees + 120) % 360) / 360;
            var hue2 = ((hueInDegrees + 240) % 360) / 360;
            var sat0 = saturationZeroToOne;
            var sat1 = sat0 - 0.37f;
            var sat2 = sat1 - 0.17f;
            if (sat1 < 0.10) sat1 = 0.10f + (0.10f - sat1);
            if (sat2 < 0.10) sat2 = 0.10f + (0.10f - sat2);
            return new[]
            {
                Color.HSVToRGB(hue0, sat0, 0.92f),
                Color.HSVToRGB(hue1, sat0, 0.92f),
                Color.HSVToRGB(hue2, sat0, 0.92f),
                Color.HSVToRGB(hue0, sat1, 0.59f),
                Color.HSVToRGB(hue1, sat2, 0.42f),
                Color.HSVToRGB(hue2, sat2, 0.42f)
            };
        }
    }
}