using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// MonoBehavior script for automatically instantiating a number of prefabs as new GameObjects.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class AutoPrefabInstantiator : MonoBehaviour {

	// Public Fields
	public GameObject prefab;
	public int quantity;
	public bool instantiateOnStart;
	public OnInstantiationEvent onInstantiation;

	public void Start() {
		if (instantiateOnStart) {
			GenerateObjects();
		}
	}

	public void GenerateObjects() {
		for (int count = 0; count < quantity; count += 1) {
			GameObject newInstance = Instantiate(prefab);
			onInstantiation.Invoke(newInstance);
		}
	}
}

[System.Serializable]
public class OnInstantiationEvent : UnityEvent<GameObject> { }