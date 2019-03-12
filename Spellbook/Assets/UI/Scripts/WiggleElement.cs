using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specialized script that causes the attached object to "wiggle" visually when Wiggle() is called.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Winter 2019
/// </summary>
public class WiggleElement : MonoBehaviour {
    // Public Fields
    [Range(0.0F, 360.0F)]
    public float wiggleIntensity = 15F;
    [Range(0.01F, 1.0F)]
    public float wiggleSpeed = 0.02F;
    public int wiggleQuantity = 3;
    public bool wiggleInterrupt = false;

    // Local Fields
    private bool _wiggling;
    private float _progress;
    private float _destination;
    private int _count;

    public void Wiggle() {
        if (!_wiggling || wiggleInterrupt) {
            _wiggling = true;
            SetDestination();
            SetDefaults();
        }
    }

    public void Update() {
        if (_wiggling) {
            _progress += wiggleSpeed;
            if (_progress > 1.0F) {
                _progress = 0.0F;
                _count += 1;
                if (_count < wiggleQuantity) {
                    SetDestination();
                }
                else {
                    _wiggling = false;
                    SetDefaults();
                }
            }
            transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(Vector3.forward * _destination), Mathf.Sin(_progress * Mathf.PI));
        }
    }

    // Internal Methods
    private void SetDefaults() {
        _progress = 0.0F;
        _count = 0;
    }

    private void SetDestination() {
        _destination = wiggleIntensity * Random.Range(-1.0F, 1.0F);
    }
}
