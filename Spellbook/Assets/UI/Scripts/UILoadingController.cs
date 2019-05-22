using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingController : MonoBehaviour {

	// Public References
	public GameObject colorableParent;
	public GameObject textParent;

	/// <summary>
	/// Use this method to set the loading icon text.
	/// </summary>
	/// <param name="loadingText">The loading text.</param>
	public void SetText(string loadingText) {
		Text text = textParent.GetComponent<Text>();
		if (text != null) {
			text.text = loadingText;
		}
	}

	/// <summary>
	/// Use this method to set the icon color.
	/// 
	/// </summary>
	/// <param name="newColor">The icon color.</param>
	public void SetColor(Color newColor) {
		SpriteRenderer sprite = textParent.GetComponent<SpriteRenderer>();
		if (sprite != null) {
			sprite.color = newColor;
		}
	}
}
