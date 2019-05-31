using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Image-Specific Implementation of UIAutoColor.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
[ExecuteInEditMode]
public class UIAutoColorImage : UIAutoColor<Image> {

	protected override void ApplyColor(Image element, Color color) {
		element.color = color;
	}
}
