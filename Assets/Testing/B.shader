Shader "Unlit/B"
{
    Properties
    {
        _Colour("Totally Rad Colour!", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
			#pragma vertex vertexFunction
			#pragma fragment fragmentFunction
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;

			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float4 _Colour;

			v2f vertexFunction(appdata IN)
			{
				v2f OUT;
				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;
				return OUT;
			}

			fixed4 fragmentFunction(v2f IN) : SV_TARGET
			{
				return _Colour;
			}
            ENDCG
        }
    }
}
