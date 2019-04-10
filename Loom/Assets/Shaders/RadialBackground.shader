Shader "Unlit/RadialBackground"
{
    Properties
    {
        _GradientTex ("Gradient Texture", 2D) = "white" {}
        _CenterX ("Center X", Float) = 0.5
        _CenterY ("Center Y", Float) = 0.5
        _Transparency ("Transparency", Float) = 0.5
        _DomeHeight ("Dome Height", Float) = 280.0

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}

        Zwrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

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
                float3 worldPos : TEXCOORD2;
                UNITY_FOG_COORDS(1)
            };
            
            
            float remap(float value, float low1, float high1, float low2, float high2) {
                return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
            sampler2D _GradientTex;
            
            float _CenterX;
            float _CenterY;
            float _Transparency;
            float _DomeHeight;

            fixed4 frag (v2f i) : SV_Target
            {
                
                float xDist = i.uv.x - _CenterX;
                float yDist = i.uv.y - _CenterY;
                float trueDist = sqrt(pow(xDist, 2) + pow(yDist, 2))/2;
                float relativeDist = saturate(trueDist);
                
                float heightScale = remap(i.worldPos.y, 0, _DomeHeight, 0, 1);
                
                float transparency = 1 - heightScale;
                
                float4 col = float4(tex2D(_GradientTex, float2(transparency, 0.5)).rgb * 0.75, 1);
                
                UNITY_APPLY_FOG(i.fogCoord, col);
                
                return col;
            }
            ENDCG
        }
    }
}
