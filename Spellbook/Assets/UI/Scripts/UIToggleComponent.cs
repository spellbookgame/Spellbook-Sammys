using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple OnClick listener that toggles a specific component.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Winter 2019
/// </summary>
public class UIToggleComponent : MonoBehaviour {

    // Public Fields
    public GameObject component;

    public void OnClick() {
        component.SetActive(!component.activeInHierarchy);
    }
}
