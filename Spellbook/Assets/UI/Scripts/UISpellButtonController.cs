using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MonoBehavior script for controlling the UI Button Spell prefab.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class UISpellButtonController : MonoBehaviour {

	// Public Fields
	public GameObject outlineParent;
	public GameObject imageParent;
	public GameObject titleParent;
	public GameObject textParent;

	/// <summary>
	/// Use this method to set the title field of the UI Button Spell instance.
	/// 
	/// </summary>
	/// <param name="titleString">The string to use.</param>
	public void SetTitle(string titleString) {
		Text component = titleParent.GetComponent<Text>();
		if (component != null) {
			component.text = titleString;
		}
	}

	/// <summary>
	/// Use this method to set the textual field of the UI Button Spell instance.
	/// 
	/// </summary>
	/// <param name="titleString">The string to use.</param>
	public void SetText(string textString) {
		Text component = textParent.GetComponent<Text>();
		if (component != null) {
			component.text = textString;
		}
	}

	/// <summary>
	/// Use this method to set the icon of the UI Button Spell instance.
	/// 
	/// </summary>
	/// <param name="iconImage">The Sprite to use.</param>
	public void SetIcon(Sprite iconImage) {
		Image component = imageParent.GetComponent<Image>();
		if (component != null) {
			component.sprite = iconImage;
		}
	}

	/// <summary>
	/// Use this method to set the color of the Icon images.
	/// 
	/// </summary>
	/// <param name="iconColor">The Color to use.</param>
	public void SetColor(Color iconColor) {
		Image icon = imageParent.GetComponent<Image>();
		if (icon != null) {
			icon.color = iconColor;
		}
		Image outline = outlineParent.GetComponent<Image>();
		if (outline != null) {
			outline.color = iconColor;
		}
	}
}
