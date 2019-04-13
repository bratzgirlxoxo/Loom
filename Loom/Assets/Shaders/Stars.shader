Shader "Unlit/Stars"
{
    Properties
    {
        [HDR] _MainColor ("Main Color", Color) = (1, 1, 1, 1)
        _WorldMaxHeight ("Max World Height", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}

        Pass
        {
            Zwrite Off
            Blend SrcAlpha OneMinusSrcAlpha 
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            float remap(float value, float low1, float high1, float low2, float high2) {
                return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
            }

            float4 _MainColor;
            float _WorldMaxHeight;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float brightnessVal = remap(i.worldPos.y, 0.0, _WorldMaxHeight, 0.1, 1.0);
                float4 col = float4(_MainColor.rgb * brightnessVal, brightnessVal);
                return col;
            }
            ENDCG
        }
    }
}
