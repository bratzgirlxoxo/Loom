Shader "Custom/WaterEruption"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        _DissolveAmt ("Dissolve Amount", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _DissolveTex;
        fixed4 _Color;
        float _DissolveAmt;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = _Color;
            
            float clipAmt = tex2D(_DissolveTex, IN.uv_MainTex).r - _DissolveAmt;
            clip(clipAmt);
            
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
