using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpriteRenderer-Specific Implementation of UIAutoColor.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
[ExecuteInEditMode]
public class UIAutoColorSprite : UIAutoColor<SpriteRenderer> {

	protected override void ApplyColor(SpriteRenderer element, Color color) {
		element.color = color;
	}
}
