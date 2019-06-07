using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script for controlling the FXDamagePixie prefab.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class UIWanderingProjectile : MonoBehaviour {

	// Public Fields
	[Header("Target Parameters")]
	public Transform target;
	public OnHitEvent onHit;
	public OnHitEvent onLaunch;

	[Header("Projectile Parameters")]
	[Range(0.01F, 1.0F)]
	public float speed;
	[Range(0.0F, 1.0F)]
	public float wanderAmplitude = 0.15F;
	[Range(0.01F, 10.0F)]
	public float wanderFrequency = 3.0F;
	public bool resetPositionOnImpact = true;

	// Internal Fields
	private Vector3 _startPosition;
	private float _progress;
	private bool _isLaunched;

	public void Start() {
		_startPosition = this.transform.position;
	}

	public void Update() {
		if (_isLaunched) {
			_progress = Mathf.Clamp01(_progress + speed);
			transform.position = GetPosition(_progress);
			if (_progress >= 1.0F) {
				_isLaunched = false;
				if (resetPositionOnImpact) {
					Reset();
				}
				onHit.Invoke();
			}
		}
	}

	public void Reset() {
		this.transform.position = _startPosition;
		_progress = 0.0F;
		_isLaunched = false;
	}

	public void Launch() {
		Reset();
		_isLaunched = true;
		onLaunch.Invoke();
	}

	// Internal Methods

	private Vector3 GetPosition(float progress) {
		Vector3 newPosition = Vector3.Slerp(_startPosition, target.position, progress);
		newPosition += GetWander();
		return newPosition;
	}

	private Vector3 GetWander() {
		return new Vector3(Get1DNoise(1.0F), Get1DNoise(2.0F), Get1DNoise(3.0F)) * wanderAmplitude;
	}

	private float Get1DNoise(float xCoordinate) {
		return 0.5F - Mathf.PerlinNoise(xCoordinate * wanderFrequency, Time.timeSinceLevelLoad * wanderFrequency);
	}

}

[System.Serializable]
public class OnHitEvent : UnityEvent { }