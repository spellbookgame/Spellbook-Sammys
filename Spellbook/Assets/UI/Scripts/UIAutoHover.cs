using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private float _startPosition;
	private Vector3 _shadowScale;

	public void Start() {
		_startPosition = transform.localPosition.y;
		if (randomize) {
			_randomOffset = Random.Range(-1.0F, 1.0F);
		}
		if (shadow) {
			_shadowScale = shadow.transform.localScale;
		}
	}

	public void Update() {
		float sine = Mathf.Sin(_randomOffset + Time.realtimeSinceStartup * hoverSpeed);
		float hoverOffset = hoverIntensity * (1 + sine);
		transform.localPosition = new Vector3(transform.localPosition.x, _startPosition + hoverOffset, transform.localPosition.z);
		if (!(shadow is null)) {
			shadow.transform.localScale = new Vector3(ShadowScaleFactor(_shadowScale.x, sine), ShadowScaleFactor(_shadowScale.y, sine), ShadowScaleFactor(_shadowScale.z, sine));
		}
	}

	private float ShadowScaleFactor(float passedValue, float sine) {
		return passedValue + passedValue * -sine * 0.08F;
	}
}
