using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrambleBlinker : MonoBehaviour {

#pragma warning disable
	[SerializeField]
	private CanvasGroup canvasGroup;
	[SerializeField]
	private float blinkTime = 0.05f;
	[SerializeField]
	private int fadeSteps = 20;
#pragma warning restore

	public void ScrambleBlink() {
		StartCoroutine(Blink());
	}

	IEnumerator Blink() {
		float fadeStepDuration = blinkTime / fadeSteps;
		float fadeAmount = 2.0f / fadeSteps;

		for (int i = 0; i < fadeSteps / 2; ++i) {
			canvasGroup.alpha += fadeAmount;
			yield return new WaitForSecondsRealtime(fadeStepDuration);
		}

		canvasGroup.alpha = 1.0f;

		for (int i = 0; i < fadeSteps / 2; ++i) {
			canvasGroup.alpha -= fadeAmount;
			yield return new WaitForSecondsRealtime(fadeStepDuration);
		}

		canvasGroup.alpha = 0.0f;
		canvasGroup.interactable = false;

		yield return null;
	}
}
