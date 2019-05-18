using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reference implementation for initializing Spell Button Controllers.
/// </summary>
public class DemoScript : MonoBehaviour {

	// Public Fields;
	public Sprite combatSprite;
	public Sprite nonCombatSprite;

	// Internal Methods
	private int _counter;

	void Start() {
		
	}

	void Update() {
		
	}

	public void ConfigureSpellButton(GameObject instance) {
		UISpellButtonController controller = instance.GetComponent<UISpellButtonController>();
		if (controller != null) {
			controller.SetColor(Random.ColorHSV(0.5F, 1.0F, 0.8F, 1.0F));
			controller.SetIcon(Random.value > 0.5F ? combatSprite : nonCombatSprite);
			controller.SetTitle(string.Format("Spell {0}", _counter));
			controller.SetText(string.Format("Spell Description {0}", _counter));
			_counter += 1;
		}
	}
}
