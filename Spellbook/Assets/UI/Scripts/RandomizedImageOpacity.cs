using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Randomizes the opacity of an attached Image every tick.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Winter 2019
/// </summary>
[RequireComponent(typeof(Image))]
public class RandomizedImageOpacity : RandomShiftingOpacity<Image> {
	private void Update() {
		Image element = GetComponent<Image>();
		Color newColor = new Color(element.color.r, element.color.g, element.color.b, GetAlpha());
		element.color = newColor;
	}
}