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
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		Color color = colorGrade.Evaluate((1 + Mathf.Sin(Time.timeSinceLevelLoad)) / 2);
		foreach (T iteratedElement in managedElements) {
			ApplyColor(iteratedElement, color);
		}
	}

	protected abstract void ApplyColor(T element, Color color);
}
