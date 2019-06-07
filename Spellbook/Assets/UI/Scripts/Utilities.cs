using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpellbookExtensions {

	/// <summary>
	/// Collection of extension methods for Color manipulation.
	/// 
	/// Written by Malcolm Riley
	/// CMPS 17X, Spring 2019
	/// </summary>
	public static class ColorUtilities {

		public const float COLOR_EPSILON = 0.001F;

		public static Color SetHue(this Color color, float newHue) {
			Color.RGBToHSV(color, out float hue, out float saturation, out float value);
			Color newColor = Color.HSVToRGB(newHue, saturation, value);
			if (color.a.CheckEpsilon(newColor.a, COLOR_EPSILON)) {
				return new Color(newColor.r, newColor.g, newColor.b, color.a);
			}
			return newColor;
		}

		public static Color SetSaturation(this Color color, float newSaturation) {
			Color.RGBToHSV(color, out float hue, out float saturation, out float value);
			Color newColor = Color.HSVToRGB(hue, newSaturation, value);
			if (color.a.CheckEpsilon(newColor.a, COLOR_EPSILON)) {
				return new Color(newColor.r, newColor.g, newColor.b, color.a);
			}
			return newColor;
		}

		public static Color SetValue(this Color color, float newValue) {
			Color.RGBToHSV(color, out float hue, out float saturation, out float value);
			Color newColor = Color.HSVToRGB(hue, saturation, newValue);
			if (color.a.CheckEpsilon(newColor.a, COLOR_EPSILON)) {
				return new Color(newColor.r, newColor.g, newColor.b, color.a);
			}
			return newColor;
		}

		public static float Hue(this Color color) {
			Color.RGBToHSV(color, out float hue, out float saturation, out float value);
			return hue;
		}

		public static float Saturation(this Color color) {
			Color.RGBToHSV(color, out float hue, out float saturation, out float value);
			return saturation;
		}

		public static float Value(this Color color) {
			Color.RGBToHSV(color, out float hue, out float saturation, out float value);
			return value;
		}
	}

	/// <summary>
	/// Collection of Math-related extension methods.
	/// 
	/// Written by Malcolm Riley
	/// CMPS 17X, Spring 2019
	/// </summary>
	public static class MathUtilities {
		public static bool CheckEpsilon(this float value, float other, float epsilon) {
			return Mathf.Abs(value - other) > epsilon;
		}
	}

}
