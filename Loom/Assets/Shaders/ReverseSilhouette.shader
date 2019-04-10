Shader "Unlit/ReverseSihlouette"
{
	Properties
	{
		_Color ("Color", Color) = (1, 1, 1, 0.5)
		_Intensity ("Intensity", Float) = 1
	}
	SubShader
	{
	Tags { "Queue"="Transparent" }
		LOD 100

		Pass
		{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			uniform float4 _Color;
			uniform float _Intensity;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float3 normal : TEXCOORD;
				float3 viewDir : TEXCOORD1;
				UNITY_FOG_COORDS(2)
			};

			
			vertexOutput vert (vertexInput i)
			{
				vertexOutput output;

				float4x4 modelMatrix = unity_ObjectToWorld;
				float4x4 modelMatrixInverse = unity_WorldToObject;

				output.normal = normalize(mul(float4(i.normal, 0), modelMatrixInverse).xyz);
				//output.viewDir = normalize(_WorldSpaceCameraPos - mul(modelMatrix, i.vertex).xyz);
				output.viewDir = normalize(WorldSpaceViewDir(i.vertex)); // using unity helper functions
				output.pos = UnityObjectToClipPos(i.vertex);

                UNITY_TRANSFER_FOG(output,output.pos);

				return output;
			}
			
			fixed4 frag (vertexOutput i) : SV_Target
			{
				float3 normalDirection = normalize(i.normal);
				float3 viewDirection = normalize(i.viewDir);

				float dotProduct = pow(abs(dot(viewDirection, normalDirection)), _Intensity); 
				float newOpacity = min(1.0, _Color.a * dotProduct);
				
				float4 output = float4(_Color.rgb, newOpacity);
				
				UNITY_APPLY_FOG(i.fogCoord, output);
				
				return output;
			}
			ENDCG
		}
	}
}
