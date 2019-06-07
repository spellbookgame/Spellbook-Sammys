using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for causing an object to hover.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class UIAutoHover : MonoBehaviour {

	// Public Fields
	[Tooltip("Optional field - if set, the shadow will be scaled automatically as this object hovers.")]
	public GameObject shadow;
	[Tooltip("How quickly should the object bob?")]
	public float hoverSpeed = 1.0F;
	[Tooltip("How high should the object hover?")]
	public float hoverIntensity = 1.0F;
	[Tooltip("Should the vertical starting position be randomized?")]
	public bool randomize;

	// Internal Fields
	private float _randomOffset = 0.0F;
	private Vector3 _shadowScale;
    public Vector3 _startPosition;

    public void Start() {
		_startPosition = transform.localPosition;
		if (randomize) {
			_randomOffset = Random.Range(-1.0F, 1.0F);
		}
		if (shadow) {
			_shadowScale = shadow.transform.localScale;
		}
	}

	public void OnDisable() {
		transform.localPosition = _startPosition;
	}

	public void Update() {
		float sine = Mathf.Sin(_randomOffset + Time.realtimeSinceStartup * hoverSpeed);
		float hoverOffset = hoverIntensity * (1 + sine);
		transform.localPosition = new Vector3(transform.localPosition.x, _startPosition.y + hoverOffset, transform.localPosition.z);
		if (shadow != null) {
			shadow.transform.localScale = new Vector3(ShadowScaleFactor(_shadowScale.x, sine), ShadowScaleFactor(_shadowScale.y, sine), ShadowScaleFactor(_shadowScale.z, sine));
		}
	}

	private float ShadowScaleFactor(float passedValue, float sine) {
		return passedValue + passedValue * -sine * 0.08F;
	}
}
