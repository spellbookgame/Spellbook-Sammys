using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Backend MonoBehavior script for UIDissolveSprite and UIDissolveImage.
/// 
/// Pairs with ApplyTextureDissolvable.shader.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public abstract class UIDissolveElement<T> : MonoBehaviour {

	// Public Fields
	public bool reverse = false;
	public bool destroyWhenDone = true;
	[Range(0.0F, 1.0F)]
	public float progress = 0.0F;
	public float speed = 0.01F;
	public Material destructionMaterial;

	public List<GameObject> destroyImmediate;

	// Internal Fields
	private bool isDestroying = false;
	private Material material;

	public void Start() {

	}

	/// <summary>
	/// Call this method to begin the dissolution process.
	/// </summary>
	public void Dissolve() {
        progress = 0.0F;

		isDestroying = true;
		material = Instantiate(destructionMaterial);
		SetMaterial(material);
		foreach (GameObject instance in destroyImmediate) {
			Destroy(instance);
		}
	}

	public void Update() {
		if (isDestroying) {
			progress += speed;
			if (progress > 1.0F && destroyWhenDone) {
				Destroy(gameObject);
			}
			material.SetFloat("_Progress", Mathf.Clamp01(reverse ? progress : 1 - progress));
		}
	}

	protected abstract void SetMaterial(Material passedMaterial);
}
