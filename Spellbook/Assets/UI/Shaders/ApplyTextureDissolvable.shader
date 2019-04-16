Shader "UI/ApplyTextureDestructable" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_OverlayTex ("Overlay", 2D) = "white" {}
		_DissolveTex ("Dissolve", 2D) = "white" {}
		_RampTex ("Ramp", 2D) = "white" {}
		_Progress ("Progress", Range(0.0, 1.0)) = 0.0
	}
	
	SubShader {
		Tags {"Queue"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass {
			CGPROGRAM
			#pragma vertex ShaderVertex
			#pragma fragment ShaderFragment
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "ShaderCommon.cginc"

			struct VertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 tint : COLOR; 
			};

			struct VertexOutput {
				float2 uv : TEXCOORD0;
				fixed4 tint : COLOR;
			};

			sampler2D _MainTex;
			sampler2D _OverlayTex;
			sampler2D _DissolveTex;
			sampler2D _RampTex;
			float _Progress;
			float4 _MainTex_ST;

			VertexOutput ShaderVertex (VertexInput input, out float4 vertex : SV_POSITION) {
				VertexOutput output;
				vertex = UnityObjectToClipPos(input.vertex);
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				output.tint = input.tint;
				return output;
			}

			fixed4 ShaderFragment (VertexOutput input, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target {
				float2 fragPos = float2(screenPos.x / _ScreenParams.x, screenPos.y / _ScreenParams.y);
				fixed4 overlayColor = tex2D(_OverlayTex, fragPos);
				fixed4 textureColor = BlendMultiply(BlendOverlay(tex2D(_MainTex, input.uv), overlayColor), input.tint);
				fixed4 useColor = textureColor;
				float dissolve = tex2D(_DissolveTex, fragPos);
				if (_Progress > 0.0) {
					if (dissolve < _Progress) {
						return fixed4(0.0, 0.0, 0.0, 0.0);
					}
					if (dissolve - _Progress < 0.15) {
						float burnProgress = (dissolve - _Progress) / 0.15;
						useColor = (textureColor * burnProgress) + (tex2D(_RampTex, float2(burnProgress, 1.0)) * (1 - burnProgress));
						useColor.a = textureColor.a;
					}
				}
				return useColor;
			}
			ENDCG
		}
	}
}
