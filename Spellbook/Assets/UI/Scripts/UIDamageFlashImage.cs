using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Image-Specific Implementation of UIDamageFlash.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class UIDamageFlashImage : UIDamageFlash<Image> {

	protected override void ApplyColor(Image element, Color color) {
		element.color = color;
	}

	protected override Color GetColorFrom(Image element) {
		return element.color;
	}
}
