using UnityEngine;

/// <summary>
/// Randomizes the opacity of an attached SpriteRenderer per tick.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Winter 2019
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class RandomizedSpriteOpacity : RandomShiftingOpacity<SpriteRenderer> {
	void Update() {
		SpriteRenderer element = GetComponent<SpriteRenderer>();
		Color newColor = new Color(element.color.r, element.color.g, element.color.b, GetAlpha());
		element.color = newColor;
	}
}
