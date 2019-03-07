using UnityEngine;

/// <summary>
/// Backend Monobehavior for RandomizedImageOpacity and RandomizedSpriteOpacity.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Winter 2019
/// </summary>
public abstract class RandomShiftingOpacity<T> : MonoBehaviour {
	[Range(0.0F, 1.0F)]
	public float minOpacity = 0.5F;
	[Range(0.0F, 1.0F)]
	public float maxOpacity = 1.0F;
	[Range(0.01F, 5.0F)]
	public float timeStretchFactor = 1.0F;
	public float minFrequency = 5.0F;
	public float maxFrequency = 10.0F;
	public float octaveRatio = 0.6F;
	public int octaves = 4;
	public bool initializeOnStart = true;

	// Internal Fields
	private Octave[] _octaves;
	private bool _initialized = false;

	void Start() {
		if (initializeOnStart) {
			Initialize();
		}
	}

	// Internal Methods
	protected void Initialize() {
		if (!_initialized) {
			_octaves = new Octave[octaves];
			float remainder = 1.0F;
			for (int index = octaves - 1; index > 0; index -= 1) {
				float significance = (index == 0) ? remainder : octaveRatio * remainder;
				_octaves[index] = new Octave(significance, Random.Range(minFrequency, maxFrequency));
				remainder -= significance;
			}
			_initialized = true;
		}
	}

	protected float GetAlpha() {
		float time = Time.time * timeStretchFactor;
		float alpha = 0.0F;
		foreach (Octave iteratedOctave in _octaves) {
			alpha += ApplyTerm(iteratedOctave, time);
		}
		alpha = 0.5F * (1.0F + alpha);
		return minOpacity + (maxOpacity - minOpacity) * alpha;
	}

	protected float ApplyTerm(Octave term, float time) {
		return term.AMPLITUDE * Mathf.Sin(time * term.FREQUENCY);
	}

	protected struct Octave {
		public readonly float AMPLITUDE;
		public readonly float FREQUENCY;

		public Octave(float amplitude, float frequency) {
			AMPLITUDE = amplitude;
			FREQUENCY = frequency;
		}
	}
}