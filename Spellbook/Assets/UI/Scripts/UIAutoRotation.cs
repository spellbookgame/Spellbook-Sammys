using UnityEngine;

/// <summary>
/// Automatically rotates the attached RectTransform object.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UIAutoRotation : MonoBehaviour {

	// Public Fields
	[Range(-360.0F, 360.0F)]
	public float rotationSpeed = 50.0F;
	public bool spinning = true;

	void Update() {
		if (spinning) {
			RectTransform component = GetComponent<RectTransform>();
			component.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
		}
	}
}
