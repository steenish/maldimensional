using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadingFader : MonoBehaviour {

#pragma warning disable
	[SerializeField]
	private CanvasGroup canvasGroup;
	[SerializeField]
	private float loadTime = 0.25f;
	[SerializeField]
	private int fadeSteps = 20;
#pragma warning restore

	public static UnityAction onLoadIsFinished;

	private void OnEnable() {
		SceneManager.sceneLoaded += FadeLoadingScreen;
	}

	private void OnDisable() {
		SceneManager.sceneLoaded -= FadeLoadingScreen;
	}

	private void FadeLoadingScreen(Scene scene, LoadSceneMode mode) {
		StartCoroutine(Fade());
	}

	IEnumerator Fade() {
		float fadeStepDuration = loadTime / fadeSteps;
		float fadeAmount = 1.0f / fadeSteps;

		for (int i = 0; i < fadeSteps; ++i) {
			canvasGroup.alpha -= fadeAmount;
			yield return new WaitForSecondsRealtime(fadeStepDuration);
		}

		canvasGroup.alpha = 0.0f;
		canvasGroup.interactable = false;

		onLoadIsFinished();
		yield return null;
	}
}
