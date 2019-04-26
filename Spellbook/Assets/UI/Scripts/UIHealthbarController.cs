using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class UIHealthbarController : MonoBehaviour {

	// Public Fields
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
	}
}
