using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Monobehavior that automatically calls the associated UnityEvents on Start().
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class AutoStart : MonoBehaviour {

	// Public Fields
	public OnStartEvent onStart;

	void Start() {
		onStart.Invoke();
	}
}

[System.Serializable]
public class OnStartEvent : UnityEvent { }
