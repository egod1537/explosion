Shader "Custom/RingBlur"
{
    Properties
    {
		_BlurTex("BlurTexture", 2D) = "black"{}

		_radius("Radius", Range(0, 1)) = 0.0
		_line("line", Range(0, 1)) = 0.0

		_pos("Position", Vector) = (0, 0, 0, 0)
    }

    SubShader
    {
		Tags 
		{ 
			"RenderType" = "Transparent"
			"Queue" = "Transparent" 
		}

		GrabPass{ "_ScreenTex" }

		Pass{

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

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
				float2 Smokeuv : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _BlurTex;
			sampler2D _ScreenTex;
			float4 _ScreenTex_ST;
			float4 _BlurTex_ST;

			float _radius;
			float _line;
			float _scaleX;
			float _scaleY;
			float4 _vector;
			float4 _pos;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _ScreenTex);
				o.Smokeuv = TRANSFORM_TEX(v.uv, _BlurTex);
				return o;
			}

			float4 frag(v2f i) : SV_Target{
				
				half NoiseValue = 0;

				float backLine = 1 - _line;

				//타원의 방정식 (x - a)^2 / q^2 + (y - b)^2 / p^2 = 1
				//float _distance = (pow(i.uv.x - 0.5, 2) / pow(_radius, 2)) + (pow(i.uv.y - 0.5, 2) / pow(_radius / 9 * 16, 2));
				float _distance = (pow(i.uv.x - _pos.x, 2) / pow(_radius, 2)) + (pow(i.uv.y - _pos.y, 2) / pow(_radius / 16 * 9, 2));

				float4 col = tex2D(_ScreenTex, i.uv - NoiseValue * tex2D(_BlurTex, i.uv));

				if (backLine <= _distance && _distance <= 1) {

					NoiseValue = abs(backLine - _distance);

					col = tex2D(_ScreenTex, i.uv - NoiseValue * tex2D(_BlurTex, i.uv));

					col.r = col.r + 0.5;

					return col;

				}

				return col;

			}

			ENDCG

		}

    }
    FallBack "Diffuse"
}
