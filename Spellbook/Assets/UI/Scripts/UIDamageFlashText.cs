using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Text-Specific Implementation of UIDamageFlash.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class UIDamageFlashText : UIDamageFlash<Text> {

	protected override void ApplyColor(Text element, Color color) {
		element.color = color;
	}

	protected override Color GetColorFrom(Text element) {
		return element.color;
	}
}
