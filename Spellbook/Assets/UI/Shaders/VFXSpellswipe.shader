Shader "UI/VFXSpellswipe" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("Noise Texture", 2D) = "white" {}
		_RampTex ("Ramp Texture", 2D) = "white" {}
		_Damping ("Damping", Range(0.0, 0.9)) = 0.5
		_Speed ("Speed", Float) = 1.0
		_NoiseBlend ("Noise Blend", Range(0.0, 1.0)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent+2" }
		
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "ShaderCommon.cginc"

			struct VertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct VertexOutput {
				float2 uv : TEXCOORD0;
				float2 uvNoise : TEXCOORD1;
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;

			sampler2D _RampTex;
			float4 _RampTex_ST;
			
			uniform float _Damping;
			uniform float _Speed;
			uniform float _NoiseBlend;

			VertexOutput vert (VertexInput input) {
				VertexOutput output;
				output.vertex = UnityObjectToClipPos(input.vertex);
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				output.uvNoise = TRANSFORM_TEX(input.uv, _NoiseTex);
				output.color = input.color;
				return output;
			}

			fixed4 frag (VertexOutput input) : SV_Target {
				fixed4 control = tex2D(_MainTex, input.uv);
				
				// fixed4 trace = tex2D(_RampTex, float2((1 + 0.49 * sin(_Time.z + control.r * )) * _Damping, 0.5));
				float trace = clamp(0.5 + 0.5 * sin(_Time.z * _Speed + control.r * 3.14159) * _Damping, 0.0, 1.0);
				fixed innerglow = control.g;
				fixed outerglow = control.b;
				fixed normal = control.a - control.b;
				
				fixed4 noiseColor = tex2D(_NoiseTex, input.uv + _Time.x);
				fixed4 rampcolor = tex2D(_RampTex, float2((1 - _NoiseBlend) * trace + noiseColor.r * _NoiseBlend, 0.5));
				fixed4 glowColor = rampcolor * outerglow * noiseColor;
				
				return fixed4((innerglow * input.color.rgb + rampcolor + glowColor).rgb, control.a);
			}

			ENDCG
		}
	}
}
