using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehavior script automatically modulating the tilt of a number of renderable elements.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class UIAutoTilt : MonoBehaviour {

	// Public References
	[Range(0.0F, 2.0F)]
	public float speed;
	public bool randomizeSeed;
	public bool randomizeAxes;
	public Vector3 axialTilt;

	// Internal References
	private Quaternion _startRotation;
	private float _seed;

	public void Start() {
		_seed = (randomizeSeed) ? Random.Range(0.0F, 100.0F) : 0.0F;
		_startRotation = transform.localRotation;
	}

	public void Update() {
		transform.localRotation = _startRotation * RandomRotation();
	}

	public void OnDisable() {
		transform.localRotation = _startRotation;
	}

	private Quaternion RandomRotation() {
		float x = axialTilt.x * GetRandomAxisRotation(0.0F);
		float y = axialTilt.y * GetRandomAxisRotation(randomizeAxes ? 3.0F : 0.0F);
		float z = axialTilt.z * GetRandomAxisRotation(randomizeAxes ? 6.0F : 0.0F);
		return Quaternion.Euler(x, y, z);
	}

	private float GetRandomAxisRotation(float offset) {
		return 0.5F - Mathf.PerlinNoise(Time.timeSinceLevelLoad * speed, offset + _seed);
	}
}
