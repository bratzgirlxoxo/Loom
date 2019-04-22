Shader "Unlit/FireballBirth"
{
    Properties
    {
        _AlphaTex ("Alpha Texture", 2D) = "white" {}
        [HDR] _MainColor ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Fade" "Queue"="Transparent"}

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _AlphaTex;
            float4 _AlphaTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _AlphaTex);
                return o;
            }

            float4 _MainColor;

            fixed4 frag (v2f i) : SV_Target
            {
                float alphaMask = tex2D(_AlphaTex, i.uv).r;
                
                if (alphaMask != 1) {
                    alphaMask *= 0.85;
                }
                
                float4 col = float4(_MainColor.rgb, 1 - alphaMask);

                return col;
            }
            ENDCG
        }
    }
}
