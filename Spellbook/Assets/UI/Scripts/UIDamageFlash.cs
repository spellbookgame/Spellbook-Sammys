using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Backend MonoBehavior script for UIDamageFlashSprite and UIDamageFlashImage.
/// 
/// Applies a "damage flash" to a renderable over time.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public abstract class UIDamageFlash<T> : MonoBehaviour {

	// Public Fields
	public T flashingElement;
	public Gradient flashGradient;
	public float flashTime = 1.0F;

	// Internal Fields
	private bool _isFlashing;
	private float _progress;
	private Color _startColor;

	public void Start() {
		_startColor = GetColorFrom(flashingElement);
	}

	public void DoFlash() {
		_isFlashing = true;
	}

	private void ResetFlash() {
		_isFlashing = false;
		_progress = 0.0F;
		ApplyColor(flashingElement, _startColor);
	}

	public void Update() {
		if (_isFlashing) {
			_progress = Mathf.Clamp01(_progress + (Time.smoothDeltaTime / flashTime));
			ApplyColor(flashingElement, flashGradient.Evaluate(Mathf.Cos(_progress * Mathf.PI)));
			if (_progress >= 1.0F) {
				ResetFlash();
			}
		}
	}

	// Abstract Methods
	protected abstract void ApplyColor(T element, Color color);

	protected abstract Color GetColorFrom(T element);
}
