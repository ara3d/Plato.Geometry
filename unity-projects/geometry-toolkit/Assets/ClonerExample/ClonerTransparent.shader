Shader "GeometryToolkit/ClonerTransparent" 
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader
	{
		Tags 
		{ 
			"RenderType" = "Transparent"
			"Queue" = "Transparent" 
		}
		LOD 200
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha		
		
		CGPROGRAM

#pragma surface surf Standard addshadow keepalpha // vertex:vert
#pragma multi_compile_instancing
#pragma instancing_options procedural:setup

#include "ClonerCommon.cginc"

	ENDCG
	}
	FallBack "Diffuse"
}
