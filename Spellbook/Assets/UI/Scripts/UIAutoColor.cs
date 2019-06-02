using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehavior script automatically modulating the color of a number of renderable elements.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
[ExecuteInEditMode]
public abstract class UIAutoColor<T> : MonoBehaviour {

	// Public Fields
	public List<T> managedElements;
	public Gradient colorGrade;

	// Start is called before the first frame update
	public void Start() {
		
	}

	public void OnDisable() {
		ApplyColorToAll(colorGrade.Evaluate(0.0F));
	}

	// Update is called once per frame
	public void Update() {
		Color color = colorGrade.Evaluate((1 + Mathf.Sin(Time.timeSinceLevelLoad)) / 2);
		ApplyColorToAll(color);
	}

	protected abstract void ApplyColor(T element, Color color);

	// Internal Methods
	protected void ApplyColorToAll(Color newColor) {
		foreach (T iteratedElement in managedElements) {
			ApplyColor(iteratedElement, newColor);
		}
	}
}
