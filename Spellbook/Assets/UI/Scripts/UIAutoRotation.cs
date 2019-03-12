using UnityEngine;

/// <summary>
/// Automatically rotates the attached transform object.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Winter 2019
/// </summary>
public class UIAutoRotation : MonoBehaviour {

	// Public Fields
	[Range(-360.0F, 360.0F)]
	public float rotationSpeed = 50.0F;
	public bool spinning = true;

	void Update() {
		if (spinning) {
			transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
		}
	}
}
