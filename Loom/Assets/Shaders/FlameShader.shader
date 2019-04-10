Shader "Unlit/FlameShader"
{
    Properties
    {
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _NoiseTex2 ("Noise Texture 2", 2D) = "white" {}
        _FlameMask ("Flame Mask", 2D) = "white" {}
        _OuterColor ("Outer Color", Color) = (1, 1, 1, 1)
        _InnerColor ("Inner Color", Color) = (1, 1, 1, 1)
        _ScrollSpeed1 ("Scroll Speed 1", Float) = 1
        _ScrollSpeed2 ("Scroll Speed 2", Float) = 1
        _OpacityCutoff ("Opacity Cutoff", Range(0,1)) = 0.5
        _FlameCutoff ("Flame Cutoff", Range(0,1)) = 0.5
        _InnerOpacity ("Inner Opacity", Range(0,1)) = 0.
        _OuterOpacity ("Outer Opacity", Range(0,1)) = 0.9
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Zwrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 noiseUV : TEXCOORD0;
                float2 noiseUV2 : TEXCOORD2;
                float2 flameUV : TEXCOORD3;
            };

            struct v2f
            {
                float2 noiseUV : TEXCOORD0;
                float2 noiseUV2 : TEXCOORD2;
                float2 flameUV : TEXCOORD3;
                float4 vertex : SV_POSITION;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            sampler2D _NoiseTex2;
            float4 _NoiseTex2_ST;
            sampler2D _FlameMask;
            float4 _FlameMask_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.noiseUV = TRANSFORM_TEX(v.noiseUV, _NoiseTex);
                o.noiseUV2 = TRANSFORM_TEX(v.noiseUV2, _NoiseTex2);
                o.flameUV = TRANSFORM_TEX(v.flameUV, _FlameMask);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float _ScrollSpeed1;
            float _ScrollSpeed2;
            float _OpacityCutoff;
            float _FlameCutoff;
            
            float4 _InnerColor;
            float4 _OuterColor;
            float _InnerOpacity;
            float _OuterOpacity;

            fixed4 frag (v2f i) : SV_Target
            {
                float4 noise1 = tex2D(_NoiseTex, float2(i.noiseUV.x, i.noiseUV.y + _Time[1] * _ScrollSpeed1));
                float4 noise2 = tex2D(_NoiseTex2, float2(i.noiseUV2.x, i.noiseUV2.y + _Time[1] * _ScrollSpeed2));
                float4 flameMask = tex2D(_FlameMask, i.flameUV);
                float4 combinedNoise = noise1 * noise2;
                
                combinedNoise = (combinedNoise + flameMask) * flameMask;
                
                float opacity = step(_OpacityCutoff, combinedNoise.r);
                float flameStep = step(_FlameCutoff, combinedNoise.r);
                
                float4 innerColor = _InnerColor * flameStep;
                float4 outerColor = _OuterColor * (1 - flameStep);
                
                if (flameStep == 0.0) {
                    opacity *= _InnerOpacity; 
                } else {
                    opacity *= _OuterOpacity;
                }
                
                float4 output = float4(innerColor.rgb + outerColor.rgb, opacity);
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, output);
                return output;
            }
            ENDCG
        }
    }
}
