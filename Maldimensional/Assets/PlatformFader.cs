using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFader : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private int fadeFrames = 20;
    [SerializeField]
    private Color fadeFromColor;
#pragma warning restore

    void Start() {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn() {
        float fadeAmountPerFrame = 1.0f / fadeFrames;
        float amountFaded = 0.0f;
        Color fadeToColor = Color.white;

        for (int i = 0; i < fadeFrames; ++i) {
            amountFaded += fadeAmountPerFrame;
            renderer.color = Color.Lerp(fadeFromColor, fadeToColor, amountFaded);
            yield return new WaitForEndOfFrame();
        }
        renderer.color = fadeToColor;
    }
}
