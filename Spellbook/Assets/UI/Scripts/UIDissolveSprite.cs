using UnityEngine;

/// <summary>
/// Causes the attached SpriteRenderer to "dissolve" and then destroy its parent GameObject.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class UIDissolveSprite : UIDissolveElement<SpriteRenderer> {

	protected override void SetMaterial(Material passedMaterial) {
		GetComponent<SpriteRenderer>().material = passedMaterial;
	}
}
