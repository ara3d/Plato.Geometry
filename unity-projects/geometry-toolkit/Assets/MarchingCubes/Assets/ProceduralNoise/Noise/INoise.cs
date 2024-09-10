using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace ProceduralNoiseProject
{
    public interface INoise 
	{
        float Frequency { get; }
        float Amplitude { get;  }
        Vector3 Offset { get;  }
		float Sample1D(float x);
		float Sample2D(float x, float y);
		float Sample3D(float x, float y, float z);
    }

    public static class INoiseExtensions
    {
        public static float Sample2D(this INoise self, float2 xy)
            => self.Sample2D(xy.x, xy.y);

        public static float Sample3D(this INoise self, float3 xyz)
            => self.Sample3D(xyz.x, xyz.y, xyz.z);
    }
}
