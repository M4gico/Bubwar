Shader "Custom/FieldOfViewSmooth"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlayerPosition ("Player Position", Vector) = (0,0,0,0)
        _MousePosition ("Mouse Position", Vector) = (0,0,0,0)
        _FOVAngle ("Field of View Angle", Float) = 60
        _ViewDistance ("View Distance", Float) = 5
        _CircleRadius ("Circle Radius", Float) = 2
        _Alpha ("Transparency", Range(0,1)) = 0.8
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _PlayerPosition;
            float4 _MousePosition;
            float _FOVAngle;
            float _ViewDistance;
            float _CircleRadius;
            float _Alpha;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 dir = normalize(_MousePosition.xy - _PlayerPosition.xy);
                float2 toPixel = i.uv - _PlayerPosition.xy;

                float angle = degrees(acos(dot(dir, normalize(toPixel))));
                float dist = length(toPixel);

                float visibilityFactor = 1.0;

                
                if (angle < _FOVAngle * 0.5 && dist < _ViewDistance)
                {
                    float edgeFade = smoothstep(_ViewDistance * 0.8, _ViewDistance, dist);
                    return fixed4(0.07450980392156863,0.058823529411764705,0.047058823529411764, edgeFade * _Alpha);
                }
                
                if (dist < _CircleRadius)
                {
                    visibilityFactor = smoothstep(_CircleRadius * 0.8, _CircleRadius, dist);
                    return fixed4(0.07450980392156863,0.058823529411764705,0.047058823529411764, visibilityFactor * _Alpha);
                }

                return fixed4(0.07450980392156863,0.058823529411764705,0.047058823529411764, _Alpha);
            }
            ENDCG
        }
    }
}
