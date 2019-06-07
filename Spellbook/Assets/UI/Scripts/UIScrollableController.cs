using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// MonoBehavior script for controlling the UI Scrollable prefab.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class UIScrollableController : MonoBehaviour {

	// Public Fields
	public RectTransform scrollField;

	public void AddElement(GameObject newObject) {
		if (newObject != null) {
			newObject.transform.SetParent(scrollField, false);
		}
	}
}