using System;
using Unity.Mathematics;

namespace Assets.ClonerExample
{
    public static class Rng
    {
        public static ulong GetNth(ulong seed, ulong index) 
            => Hasher.Hash(seed + index);
        
        public static float HashToFloat(ulong hash) 
            => ((uint)hash) / (float)uint.MaxValue;
        
        public static float GetNthFloat(ulong seed, ulong index) 
            => HashToFloat(GetNth(seed, index));
        
        public static float GetNthFloat(ulong seed, ulong index, float min, float max) 
            => math.lerp(min, max, GetNthFloat(seed, index));

        public static int GetNthInt(ulong seed, ulong index, int minValue, int maxValue)
            => GetNthInt(seed, index, (maxValue - minValue)) + minValue;

        public static int GetNthInt(ulong seed, ulong index, int maxValue = int.MaxValue) 
            => Math.Abs((int)GetNth(seed, (ulong)index) % maxValue);

        public static quaternion GetNthQuaternion(ulong seed, ulong index)
            => GetNthQuaternion(seed, index, -math.PI, +math.PI);

        public static quaternion GetNthQuaternion(ulong seed, ulong index, float min, float max) 
            => quaternion.EulerXYZ(GetNthFloat3(seed, index, min, max));
        
        public static float3 GetNthFloat3(ulong seed, ulong index) 
            =>new(GetNthFloat(seed, index * 3),
                GetNthFloat(seed, index * 3 + 1),
                GetNthFloat(seed, index * 3 + 2));

        public static float3 GetNthFloat3(ulong seed, ulong index, float3 min, float3 max)
            => GetNthFloat3(seed, index) * (max - min) + min;

        public static float3x3 GetNthFloat3x3(ulong seed, ulong index, float3 min, float3 max)
            => new(
                GetNthFloat3(seed, index * 3, min, max),
                GetNthFloat3(seed, index * 3 + 1, min, max),
                GetNthFloat3(seed, index * 3 + 2, min, max));
    }
}