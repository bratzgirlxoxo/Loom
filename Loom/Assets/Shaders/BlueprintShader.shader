Shader "Unlit/BlueprintShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Opacity ("Opacity", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Fade" "Queue"="Transparent"}
        LOD 100

        Pass
        {
            Zwrite Off
            ZTest Always
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Opacity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float stepVal = step(0.5, col.r);
                col.rgb = (1 - stepVal).xxx;
                col.a = (1 - stepVal) * _Opacity;

                return col;
            }
            ENDCG
        }
    }
}
