using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellbookExtensions;

/// <summary>
/// Script for easy control of the Warp background prefab.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
[ExecuteInEditMode]
public class UIWarpController : MonoBehaviour {

	// Public Fields
	public SpriteRenderer first;
	public SpriteRenderer second;
	public Color color = Color.red;
	[Range(0.0F, 1.0F)]
	public float darken = 0.5F;

	public void Update() {
		SetColors(color);
	}

	// Internal Methods
	private void SetColors(Color passedColor) {
		Color.RGBToHSV(passedColor, out float hue, out float saturation, out float value);
		first.color = new Color(passedColor.r, passedColor.g, passedColor.b, 0.5F);
		second.color = Color.HSVToRGB(hue, saturation, Mathf.Clamp01(value - darken));
	}
}
