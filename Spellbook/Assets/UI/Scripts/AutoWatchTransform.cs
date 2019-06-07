using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// Script that calls an event when the watched RectTransform component first overlaps or first leaves its own.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class AutoWatchTransform : MonoBehaviour {

	// Public Fields
	[Header("Configuration")]
	public RectTransform watchedTransform;
	public bool invokeEveryTick;

	[Header("Events")]
	public TransformWatchEvent OnTransformEnter;
	public TransformWatchEvent OnTransformLeave;

	// Internal Fields
	private RectTransform _thisTransform;
	private bool _onEnterCalled;
	private bool _onLeaveCalled;

	public void Start() {
		_thisTransform = GetComponent<RectTransform>();
	}

	public void Update() {
		if (IsOverlapping(watchedTransform)) {
			TryInvoke(OnTransformEnter, ref _onEnterCalled);
			_onLeaveCalled = false;
		}
		else {
			TryInvoke(OnTransformLeave, ref _onLeaveCalled);
			_onEnterCalled = false;
		}
	}

	// Internal Methods
	private void TryInvoke(TransformWatchEvent watchEvent, ref bool invocationFlag) {
		if (ShouldInvoke(invocationFlag)) {
			watchEvent.Invoke();
			invocationFlag = true;
		}
	}

	private bool ShouldInvoke(bool flag) {
		return invokeEveryTick || !flag;
	}

	private bool IsOverlapping(RectTransform otherTransform) {
		Rect first = WorldRectFromLocal(_thisTransform);
		Rect second = WorldRectFromLocal(otherTransform);
		return first.Overlaps(second);
	}

	private Rect WorldRectFromLocal(RectTransform rectTransform) {
		float scaledWidth = rectTransform.sizeDelta.x * rectTransform.lossyScale.x;
		float scaledHeight = rectTransform.sizeDelta.y * rectTransform.lossyScale.y;

		return new Rect(rectTransform.position.x - scaledWidth / 2, rectTransform.position.y - scaledHeight / 2, scaledWidth, scaledHeight);
	}
}

[System.Serializable]
public class TransformWatchEvent : UnityEvent { }