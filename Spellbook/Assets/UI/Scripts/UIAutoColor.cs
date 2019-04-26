using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAutoColor : MonoBehaviour {

	// Public Fields
	public List<SpriteRenderer> managedSprites;
	public Gradient colorGrade;

	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		Color color = colorGrade.Evaluate((1 + Mathf.Sin(Time.timeSinceLevelLoad)) / 2);
		foreach (SpriteRenderer iteratedSprite in managedSprites) {
			iteratedSprite.color = color;
		}
	}
}
