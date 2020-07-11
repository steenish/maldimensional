using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrambleBlinker : MonoBehaviour {

#pragma warning disable
	[SerializeField]
	private CanvasGroup canvasGroup;
	[SerializeField]
	private int blinkFrames = 20;
#pragma warning restore

	public void ScrambleBlink() {
		StartCoroutine(Blink());
	}

	IEnumerator Blink() {
		float fadeAmountPerFrame = 1.0f / blinkFrames;

		for (int i = 0; i < blinkFrames / 2; ++i) {
			canvasGroup.alpha += fadeAmountPerFrame;
			yield return new WaitForEndOfFrame();
		}

		canvasGroup.alpha = 1.0f;

		for (int i = 0; i < blinkFrames / 2; ++i) {
			canvasGroup.alpha -= fadeAmountPerFrame;
			yield return new WaitForEndOfFrame();
		}

		canvasGroup.alpha = 0.0f;
		canvasGroup.interactable = false;

		yield return null;
	}
}
