Shader "Unlit/Footprint"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor ("Color", Color) = (1, 1, 1, 1)
        _StairDepthMax("Max Stair Depth Diff", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"  "Queue"="Transparent"}

        Pass
        {
            Zwrite Off
            Blend SrcAlpha OneMinusSrcAlpha
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float4 _MainTex_ST;
            float _StairDepthMax;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 _MainColor;

            fixed4 frag (v2f i) : SV_Target
            {
                //float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)).r;
                //float existingDepthLinear = LinearEyeDepth(existingDepth01);
                //float depthDifference = existingDepth01 - i.screenPos.w;
            
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                
                col = float4(_MainColor.rgb, col.r);
                //col.a *= step(_StairDepthMax, depthDifference);
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
