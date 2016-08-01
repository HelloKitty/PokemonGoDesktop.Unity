Shader "Custom/PokemonGround" 
{
	Properties 
	{		
		_MainTex ("Main Albedo (RGB)", 2D) = "white" {}
		_RoadTex ("Road Albedo (RGB)", 2D) = "white" {}
		_GroundTex ("Ground Albedo (RGB)", 2D) = "white" {}
		_WaterTex ("Water Albedo (RGB)", 2D) = "white" {}
		_MapCutoff ("Map Cutoff Value", Float) = 1.0

		_EmissionColor ("Emission Color", Color) = (1,1,1, 1)
		_EmissionLM ("Emission (Lightmapper)", Float) = 0
		[Toggle] _DynamicEmissionLM ("Dynamic Emission (Lightmapper)", Int) = 0

		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		//textures
		sampler2D _MainTex;
		sampler2D _RoadTex;
		sampler2D _WaterTex;
		sampler2D _GroundTex;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_RoadTex;
			float2 uv_WaterTex;
			float2 uv_GroundTex;
		};

		half _Glossiness;
		half _Metallic;
		half _MapCutoff;
	
		//colors
		fixed3 _EmissionColor;

		half _ScrollSpeed;
		half _EmissionLM;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed3 mapTextureColor = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 roadTextureColor = tex2D(_RoadTex, IN.uv_RoadTex);
			fixed4 waterTextureColor = tex2D(_WaterTex, IN.uv_WaterTex);
			fixed4 groundTextureColor = tex2D(_GroundTex, IN.uv_GroundTex);

			//Add the emissive roadside layer first
			o.Albedo = fixed3(1,1,0) * step(0.2f, mapTextureColor.r) * abs(step(1, mapTextureColor.r) - 1);

			//selects between the textures using stepwise color
			o.Albedo += (roadTextureColor.rgb * step(1, mapTextureColor.r)) + (waterTextureColor * step(_MapCutoff, mapTextureColor.b));
			half layerSaturation = clamp(0, 1, step(1, mapTextureColor.r) + step(0.2f, mapTextureColor.r)) + step(_MapCutoff, mapTextureColor.b);

			//If the layer isn't saturated we should apply the ground base-layer.
			o.Albedo += lerp(groundTextureColor.rgb, o.Albedo, clamp(0, 1, layerSaturation));

			//Do the same with emission
			//o.Emission = lerp(0, _EmissionColor * _EmissionLM, characterSampledColor.a);

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 1;
		}
		ENDCG
	} 
	FallBack "Diffuse"

}
