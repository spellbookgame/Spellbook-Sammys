using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller script for healthbar UI element.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class UIHealthbarController : MonoBehaviour {

	public enum HealthbarAlignment {
		LEFT_TO_RIGHT,
		CENTER_OUT,
		RIGHT_TO_LEFT,
	}

	// Public Fields
	public HealthbarAlignment alignment;
	public Gradient colorGrade;
	public SpriteRenderer overlay;
	public SpriteRenderer underlay;
	[Range(0,1)]
	public float healthPercentage;

	void Update() {
		RectTransform transform = GetComponent<RectTransform>();
		overlay.size = transform.rect.size;
		underlay.size = new Vector2(transform.rect.width * Mathf.Clamp01(healthPercentage), transform.rect.height);
		underlay.color = colorGrade.Evaluate(healthPercentage);
		switch (alignment) {
			case HealthbarAlignment.LEFT_TO_RIGHT:
				underlay.transform.localPosition = new Vector3(-overlay.size.x * 0.5F * (1 - healthPercentage), underlay.transform.localPosition.y, underlay.transform.localPosition.z);
				break;
			case HealthbarAlignment.RIGHT_TO_LEFT:
				underlay.transform.localPosition = new Vector3(overlay.size.x * 0.5F * (1 - healthPercentage), underlay.transform.localPosition.y, underlay.transform.localPosition.z);
				break;
		}
	}
}
