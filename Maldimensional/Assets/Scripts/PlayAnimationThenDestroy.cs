using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationThenDestroy : MonoBehaviour {

    [SerializeField]
    private AnimationClip clip;
    [SerializeField]
    private float clipLengthRatio = 0.9f;

    void Start() {
        StartCoroutine(WaitForAnimationThenDestroy());
    }

    IEnumerator WaitForAnimationThenDestroy() {
        yield return new WaitForSeconds((clip != null) ? clip.length * clipLengthRatio : 1.0f);
        Destroy(gameObject);
    }
}
