using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationThenDestroy : MonoBehaviour {

    [SerializeField]
    private AnimationClip clip;

    void Start() {
        StartCoroutine(WaitForAnimationThenDestroy());
    }

    IEnumerator WaitForAnimationThenDestroy() {
        yield return new WaitForSeconds((clip != null) ? clip.length : 1.0f);
        Destroy(gameObject);
    }
}
