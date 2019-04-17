Shader "UI/ApplyTexture" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_OverlayTex ("Overlay", 2D) = "white" {}
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
			float4 _MainTex_ST;

			VertexOutput ShaderVertex (VertexInput input, out float4 vertex : SV_POSITION) {
				VertexOutput output;
				vertex = UnityObjectToClipPos(input.vertex);
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				output.tint = input.tint;
				return output;
			}

			fixed4 ShaderFragment (VertexOutput input, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target {
				fixed4 textureColor = tex2D(_MainTex, input.uv);
				fixed4 overlayColor = tex2D(_OverlayTex, float2(screenPos.x / _ScreenParams.x, screenPos.y / _ScreenParams.y));
				return BlendMultiply(BlendOverlay(textureColor, overlayColor), input.tint);
			}
			ENDCG
		}
	}
}
