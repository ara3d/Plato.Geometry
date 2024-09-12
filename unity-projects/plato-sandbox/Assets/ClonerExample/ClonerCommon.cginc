		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
		};

		struct InstanceData 
		{
			float3 pos;
			float4 rot;
			float3 scl;
			float4 col;
			float smoothness;
			float metallic;
			uint id;
		};

#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
		StructuredBuffer<InstanceData> instanceBuffer;
#endif

		// NOTE: not currently used, but demonstrates how we could deform vertices as well. 
		void vert (inout appdata_full v) 
		{
			v.vertex.xyz += v.normal.xyz * 0.5f;
		}

		float4x4 QuatToMatrix(float4 q)
		{
			float4x4 rotMat = float4x4
			(
				float4(1 - 2 * q.y * q.y - 2 * q.z * q.z, 2 * q.x * q.y + 2 * q.w * q.z, 2 * q.x * q.z - 2 * q.w * q.y, 0),
				float4(2 * q.x * q.y - 2 * q.w * q.z, 1 - 2 * q.x * q.x - 2 * q.z * q.z, 2 * q.y * q.z + 2 * q.w * q.x, 0),
				float4(2 * q.x * q.z + 2 * q.w * q.y, 2 * q.y * q.z - 2 * q.w * q.x, 1 - 2 * q.x * q.x - 2 * q.y * q.y, 0),
				float4(0, 0, 0, 1)
			);
			return rotMat;
		}
 
		float4x4 MakeTRSMatrix(float3 pos, float4 rotQuat, float3 scale)
		{
			float4x4 rotPart = QuatToMatrix(rotQuat);
			float4x4 sclPart = float4x4(float4(scale.x, 0, 0, 0), float4(0, scale.y, 0, 0), float4(0, 0, scale.z, 0), float4(0, 0, 0, 1));
			float4x4 trPart = float4x4(float4(1, 0, 0, 0), float4(0, 1, 0, 0), float4(0, 0, 1, 0), float4(pos, 1));
			return mul(mul(transpose(trPart), transpose(rotPart)), transpose(sclPart));
		}
 
		void setup()
		{
#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
			InstanceData inst = instanceBuffer[unity_InstanceID];	
			float4x4 mat = (inst.col.a > 0) ? MakeTRSMatrix(inst.pos, inst.rot, inst.scl) : 0;
			unity_ObjectToWorld = mat;
#endif
		}

		void surf(Input IN, inout SurfaceOutputStandard o) 
		{
#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
			InstanceData inst = instanceBuffer[unity_InstanceID];
			float4 col = inst.col;
			o.Smoothness = inst.smoothness;
			o.Metallic = inst.metallic;
#else
			float4 col = float4(0, 0, 1, 1);
#endif

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * col;
			o.Albedo = c.rgb;
			o.Alpha = col.w;
		}