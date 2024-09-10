using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerColorize : ClonerJobComponent
    {
        [Range(0, 1)] public float Hue;
        [Range(0, 1)] public float Saturation;
        public Color[] Colors;
        [Range(0, 1)] public float Strength = 1.0f;
        public bool Randomize = true;
        public ulong RandomSeed = 12345678;

        private NativeArray<float4> cachedColors = new NativeArray<float4>();
        public void OnValidate()
        {
            Update();
        }

        public new void Update()
        {
            Colors = UtilGenerateColorPalette.GenerateTriadColors(Hue * 360f, Saturation);
        }

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            Colors = UtilGenerateColorPalette.GenerateTriadColors(Hue * 360f, Saturation);
            cachedColors.Assign(Colors, ToFloat4);
            Debug.Log($"Clorize sees {previousData.Count}");
            return (previousData, new JobAssignColors()
            {
                Colors = cachedColors,
                Data = previousData,
                Strength = Strength,
                Randomize = Randomize,
                RandomSeed = RandomSeed,
            }
            .Schedule(previousData.Count, batchSize, previousHandle));
        }

        public static float4 ToFloat4(Color c)
            => new float4(c.r, c.g, c.b, c.a);
    }
}