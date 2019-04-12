Shader "Unlit/DirtShader"
{
    Properties
    {
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _MainColor ("Main Color", Color) = (1, 1, 1, 1)
        _DirtColor ("Dirt Color", Color) = (1, 1, 1, 1)
        _DeformScale ("Deform Scale", Float) = 1
        _DirtScale ("Dirt Scale", Float) = 0
        _ScrollSpeed1 ("Scroll Speed 1", Float) = 1
        _ScrollSpeed2 ("Scroll Speed 2", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 objectPos : TEXCOORD2;
            };

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            float4 _MainColor;
            float _DeformScale;
            float _ScrollSpeed1;
            float _ScrollSpeed2;

            v2f vert (appdata v)
            {
                v2f o;
                
                float noiseVal = tex2Dlod(_NoiseTex, float4(v.uv.x + (_Time[1] * _ScrollSpeed1), v.uv.y, 0, 0)).r;
                noiseVal += tex2Dlod(_NoiseTex, float4(v.uv.x - (_Time[1] * _ScrollSpeed2), v.uv.y, 0, 0)).r;
                float4 newVert = float4(v.vertex.x, v.vertex.y + (noiseVal * _DeformScale), v.vertex.z, 0);
                o.objectPos = newVert;
                o.vertex = UnityObjectToClipPos(newVert);
                o.uv = TRANSFORM_TEX(v.uv, _NoiseTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 _DirtColor;
            float _DirtScale;

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = _MainColor;
                col *= (i.objectPos.y / _DeformScale);
                col += (_DirtColor * _DirtScale);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
