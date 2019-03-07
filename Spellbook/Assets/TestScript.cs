using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	public Texture newTexture;

	// Start is called before the first frame update
	void Start() {
		GetComponent<Renderer>().material.SetTexture("_MainTex", newTexture);
	}

	// Update is called once per frame
	void Update() {

	}
}