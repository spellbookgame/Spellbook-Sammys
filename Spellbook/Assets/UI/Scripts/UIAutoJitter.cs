using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Performs random transform jitter to the attached component each tick.
/// 
/// The script assumes that the object does not move in local space otherwise.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Winter 2019
/// </summary>
public class UIAutoJitter : MonoBehaviour {

    // Public fields
    public bool smooth;
    [Range(0.0F, 5.0F)]
    public float speed = 0.25F;
    [Range(0.0F, 20.0F)]
    public float minJitter = 0.0F;
    [Range(0.0F, 50.0F)]
    public float maxJitter = 5.0F;

    // Internal Fields
    private Vector3 _nextDelta;
    private Vector3 _startPosition;
    private float _progress;

    void Start() {
        _nextDelta = Vector3.zero;
        _startPosition = transform.localPosition;
        RandomizeDelta();
    }

    void Update() {
        _progress += speed;
        if (smooth) {
            transform.localPosition = Vector3.Lerp(_startPosition, _nextDelta, _progress);
        }
        if (_progress > 1.0F) {
            transform.localPosition = _nextDelta;
            _progress = 0.0F;
            RandomizeDelta();
            _startPosition = transform.localPosition;
        }
    }

	public void OnDisable() {
		transform.localPosition = _startPosition;
	}

	// Internal Methods
	private void RandomizeDelta() {
        _nextDelta.x = GetRandomizedParameter();
        _nextDelta.y = GetRandomizedParameter();
        _nextDelta.z = 0;
    }

    private float GetRandomizedParameter() {
        return (maxJitter - minJitter) * Random.Range(-1.0F, 1.0F);
    }
}
