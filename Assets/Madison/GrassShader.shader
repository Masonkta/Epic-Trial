
Shader "Custom/GrassShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white"
        _WindDirection("Wind Direction", Vector) = (0, 0, 1)
        _WindStrength("Wind Strength", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Cull Off // Disable culling to render both sides
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        sampler2D _MainTex;
        float3 _WindDirection;
        float _WindStrength;

        void vert(inout appdata_full v)
        {
            float windFactor = dot(normalize(_WindDirection), normalize(v.vertex.xyz));
            float windOffset = sin(windFactor * _WindStrength * _Time.y);
            v.vertex.y += windOffset;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = texColor.rgb;
            o.Alpha = texColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}