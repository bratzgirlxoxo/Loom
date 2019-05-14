Shader "Unlit/EndFade_S"
{
    Properties
    {
        _Opacity ("Opacity", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Fade" "Queue"="Transparent"}
        LOD 100

        Pass
        {
            Zwrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Always
        
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
            };

            float _Opacity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = float4(0,0,0,_Opacity);
                return col;
            }
            ENDCG
        }
    }
}
