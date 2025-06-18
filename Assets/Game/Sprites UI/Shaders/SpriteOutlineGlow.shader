Shader "Custom/SpriteOutlineGlow"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Sprite Color", Color) = (1,1,1,1)

        _OutlineColor ("Outline Color", Color) = (0, 1, 1, 1)
        _OutlineThickness ("Outline Thickness", Range(0, 0.1)) = 0.03

        _GlowColor ("Glow Color", Color) = (0, 1, 1, 1)
        _GlowIntensity ("Glow Intensity", Range(0, 10)) = 5
        _GlowSpeed ("Glow Speed", Range(0, 10)) = 2
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }
        Lighting Off
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                float2 uv       : TEXCOORD1; // for custom usage
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            fixed4 _OutlineColor;
            float _OutlineThickness;

            fixed4 _GlowColor;
            float _GlowIntensity;
            float _GlowSpeed;
            float _time;

            // Vertex shader
            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);
                OUT.color = IN.color * _Color;

                OUT.uv = OUT.texcoord;
                return OUT;
            }

            // Function to sample alpha multiple times around pixel for outline
            // We'll sample 8 neighbors around current uv with offset of _OutlineThickness
            fixed3 outlineSample(float2 uv, float2 offset)
            {
                fixed3 result = tex2D(_MainTex, uv + offset).rgb;
                return result;
            }

            // Fragment shader
            fixed4 frag(v2f IN) : SV_Target
            {
                // Sample the main texture alpha at current uv
                fixed4 mainCol = tex2D(_MainTex, IN.texcoord) * IN.color;

                // If fully transparent, try to draw outline
                // We'll check the alpha in neighboring pixels; if any neighbor is opaque, we draw outline

                float alpha = mainCol.a;

                if (alpha == 0)
                {
                    // Offsets to check in UV space
                    float2 texelSize = float2(_OutlineThickness, _OutlineThickness);

                    // Sample neighbors alpha
                    float alphaUp     = tex2D(_MainTex, IN.texcoord + float2(0, texelSize.y)).a;
                    float alphaDown   = tex2D(_MainTex, IN.texcoord + float2(0, -texelSize.y)).a;
                    float alphaLeft   = tex2D(_MainTex, IN.texcoord + float2(-texelSize.x, 0)).a;
                    float alphaRight  = tex2D(_MainTex, IN.texcoord + float2(texelSize.x, 0)).a;

                    float alphaUpLeft = tex2D(_MainTex, IN.texcoord + float2(-texelSize.x, texelSize.y)).a;
                    float alphaUpRight= tex2D(_MainTex, IN.texcoord + float2(texelSize.x, texelSize.y)).a;
                    float alphaDownLeft=tex2D(_MainTex, IN.texcoord + float2(-texelSize.x, -texelSize.y)).a;
                    float alphaDownRight=tex2D(_MainTex, IN.texcoord + float2(texelSize.x, -texelSize.y)).a;

                    float maxNeighborAlpha = max(max(max(alphaUp, alphaDown), max(alphaLeft, alphaRight)),
                                                  max(max(alphaUpLeft, alphaUpRight), max(alphaDownLeft, alphaDownRight)));

                    if (maxNeighborAlpha > 0.1)
                    {
                        // Draw outline color
                        return fixed4(_OutlineColor.rgb, _OutlineColor.a * maxNeighborAlpha);
                    }
                    else
                    {
                        // Fully transparent
                        return fixed4(0,0,0,0);
                    }
                }

                // For glowing effect, we add emission based on a smooth pulse

                // Compute glow factor (pulsating)
                float glowPulse = (sin(_time * _GlowSpeed * 6.2831) + 1) * 0.5; // oscillates 0..1

                // Glow intensity modulated by glowPulse
                fixed3 emissive = _GlowColor.rgb * _GlowIntensity * glowPulse * mainCol.a;

                // Final color adds glow emissive light to main color
                fixed3 finalColor = mainCol.rgb + emissive;

                // Clamp to maximum 1
                finalColor = saturate(finalColor);

                return fixed4(finalColor, mainCol.a);
            }
            ENDCG
        }
    }
    FallBack "Sprites/Default"
}
