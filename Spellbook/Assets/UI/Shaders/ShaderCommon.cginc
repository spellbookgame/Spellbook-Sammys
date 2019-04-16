/* Common functions for shader programs.
 * 
 * Written by Malcolm Riley
 * UCSC CMPM 172, Spring 2019
 */

// Overlay blend function. Uses alpha of "first".
fixed4 BlendOverlay(fixed4 first, fixed4 second) {
	fixed4 result = first < .5 ? 2.0 * first * second : 1.0 - 2.0 * (1.0 - first) * (1.0 - second);
	result.a = first.a;
	return result;
}

// Screen blend function. Uses alpha of "first".
fixed4 BlendScreen(fixed4 first, fixed4 second) {
	fixed4 result = 1.0 - (1.0 - first) * (1.0 - second);
	result.a = first.a;
	return result;
}

// Darker Color blend function. Uses alpha of "first".
fixed4 BlendDarkerColor(fixed4 first, fixed4 second) {
	fixed4 result = min(first, second);
	result.a = first.a;
	return result;
}

// Lighter Color blend function. Uses alpha of "first".
fixed4 BlendLighterColor(fixed4 first, fixed4 second) {
	fixed4 result = max(first, second);
	result.a = first.a;
	return result;
}

// Multiply blend function. Uses alpha of "first".
fixed4 BlendMultiply(fixed4 first, fixed4 second) { 
	fixed4 result = first * second;
	result.a = first.a;
	return result;
}

// Emulation of Photoshop "Colorize" function. Uses alpha of "first".
fixed4 BlendColorize(fixed4 first, fixed4 second) {
	float luma = first.a * ((first.r + first.g + first.b) / 3);
	fixed4 result = luma * second;
	result.a = first.a;
	return result;
}