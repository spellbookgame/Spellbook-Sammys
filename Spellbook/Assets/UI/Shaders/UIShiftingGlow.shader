Shader "UI/UIShiftingGlow" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("Noise Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType" = "Transparent" "Queue"="Transparent-1" }
		
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off

		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "ShaderCommon.cginc"

			struct VertexInput {
				float2 uv : TEXCOORD0;
				float2 uv_noise : TEXCOORD1;
				float4 vertex : POSITION;
				fixed4 color : COLOR;
			};

			struct VertexOutput {
				float2 uv : TEXCOORD0;
				float2 uv_noise : TEXCOORD1;
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			float4 _MainTex_ST;

			VertexOutput vert (VertexInput input) {
				VertexOutput output;
				output.vertex = UnityObjectToClipPos(input.vertex);
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				output.uv_noise = TRANSFORM_TEX(input.uv_noise, _NoiseTex) + _Time.x;
				output.color = input.color;
				return output;
			}

			fixed4 frag (VertexOutput input) : SV_Target {
				fixed4 color = tex2D(_MainTex, input.uv);
				fixed4 noiseColor = tex2D(_NoiseTex, input.uv_noise);
				color.a = color.a * Luma(noiseColor);
				return color * input.color;
			}

			ENDCG
		}
	}
}
