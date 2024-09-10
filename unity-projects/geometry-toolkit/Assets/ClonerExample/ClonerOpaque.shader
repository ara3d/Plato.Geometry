Shader "GeometryToolkit/ClonerOpaque" 
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader
	{
		Tags 
		{ 
			"RenderType" = "Opaque"
		}
		LOD 200

		CGPROGRAM

#pragma surface surf Standard addshadow // vertex:vert
#pragma multi_compile_instancing
#pragma instancing_options procedural:setup

#include "ClonerCommon.cginc"
		
		ENDCG
	}
	FallBack "Diffuse"
}
