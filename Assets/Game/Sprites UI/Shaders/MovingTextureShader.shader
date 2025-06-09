Shader "Custom/ScrollingMeteors_Seamless"
{
    Properties
    {
        _MainTex ("Meteor Texture", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", Float) = 1.0
        _TileX ("Tiling X", Float) = 1.0
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "IgnoreProjector"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
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
            float _ScrollSpeed;
            float _TileX;
            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv.x = uv.x * _TileX + _Time.y * _ScrollSpeed;
                uv.x = frac(uv.x);

                fixed4 texColor = tex2D(_MainTex, uv);
                clip(texColor.a - 0.01);
                
                fixed4 col = texColor * _Color;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}