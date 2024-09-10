using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralNoiseProject
{

    public enum NoiseType {  Perlin, Value, Simplex, Voronoi, Worley }

    [ExecuteAlways]
    public class Example : MonoBehaviour
    {

        public NoiseType noiseType = NoiseType.Perlin;

        public int seed = 0;

        public int octaves = 4;

        public float frequency = 1.0f;

        public int width = 512;

        public int height = 512;

        Texture2D texture;

        void Start()
        {

            texture = new Texture2D(width, height);

            //Create the noise object and use a fractal to apply it.
            //The same noise object will be used for each fractal octave but you can 
            //manually set each individual ocatve like so...
            // fractal.Noises[3] = noise;
            var noise = GetNoise();
            var fractal = new FractalNoise(noise, octaves, frequency);

            var arr = new float[width, height];

            //Sample the 2D noise and add it into a array.
            for(var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var fx = x / (width - 1.0f);
                    var fy = y / (height - 1.0f);

                    arr[x,y] = fractal.Sample2D(fx, fy);
                }
            }

            //Some of the noises range from -1-1 so normalize the data to 0-1 to make it easier to see.
            NormalizeArray(arr);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var n = arr[x, y];
                    texture.SetPixel(x, y, new Color(n, n, n, 1));
                }
            }

            texture.Apply();

        }

        void Update()
        {
            var center = new Vector2(Screen.width / 2, Screen.height / 2);
            var offset = new Vector2(width / 2, height / 2);

            var rect = new Rect();
            rect.min = center - offset;
            rect.max = center + offset;

            GUI.DrawTexture(rect, texture);

        }

        void OnValidate()
        {
            Update();
        }

        private INoise GetNoise()
        {
            switch (noiseType)
            {
                case NoiseType.Perlin:
                    return new PerlinNoise(seed, 20);

                case NoiseType.Value:
                    return new ValueNoise(seed, 20);

                case NoiseType.Simplex:
                    return new SimplexNoise(seed, 20);

                case NoiseType.Voronoi:
                    return new VoronoiNoise(seed, 20);

                case NoiseType.Worley:
                    return new WorleyNoise(seed, 20, 1.0f);

                default:
                    return new PerlinNoise(seed, 20);
            }
        }

        private void NormalizeArray(float[,] arr)
        {

            var min = float.PositiveInfinity;
            var max = float.NegativeInfinity;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {

                    var v = arr[x, y];
                    if (v < min) min = v;
                    if (v > max) max = v;

                }
            }

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var v = arr[x, y];
                    arr[x, y] = (v - min) / (max - min);
                }
            }

        }

    }

}
