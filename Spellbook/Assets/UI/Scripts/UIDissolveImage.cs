using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Causes the attached Image to "dissolve" and then destroy its parent GameObject.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
[RequireComponent(typeof(Image))]
public class UIDissolveImage : UIDissolveElement<Image> {

	protected override void SetMaterial(Material passedMaterial) {
		GetComponent<Image>().material = passedMaterial;
	}
}
