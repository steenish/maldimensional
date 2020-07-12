using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PortalController : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private AnimationClip playerTeleportClip;
    [SerializeField]
    private int movementFrames = 10;
#pragma warning restore

    private ScoreManager scoreManager;
    private UnityAction onMovedToPortal;

    private void OnEnable() {
        onMovedToPortal += StartAnimationCoroutine;
    }

    private void OnDisable() {
        onMovedToPortal -= StartAnimationCoroutine;
    }

    private void Start() {
        scoreManager = ScoreManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            scoreManager.IncrementScore();
            scoreManager.SaveHighScore();

            AudioManager.instance.Play("Teleport");

            StartCoroutine(MovePlayerToPortal(collision.gameObject));
        }
    }

    IEnumerator MovePlayerToPortal(GameObject playerGameObject) {
        // Handle controls.
        playerGameObject.GetComponent<PlayerController>().enabled = false;
        
        Animator playerAnimator = playerGameObject.GetComponent<Animator>();
        
        // Handle animation and audio.
        playerAnimator.SetBool("Grounded", false);
        playerAnimator.SetBool("Boosting", false);
        AudioManager.instance.Stop("Boosting");

        // Handle physics simulation.
        Rigidbody2D playerRigidbody = playerGameObject.GetComponent<Rigidbody2D>();
        playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
        playerRigidbody.velocity = Vector3.zero;

        // Linearly interpolate position into portal.
        Vector3 oldPlayerPosition = playerRigidbody.position;
        Vector3 newPlayerPosition = GetComponent<SpriteRenderer>().bounds.center;
        float interpolateParameter = 0.0f;
        for (int i = 0; i < movementFrames; ++i) {
            interpolateParameter += 1.0f / movementFrames;
            playerRigidbody.position = Vector3.Lerp(oldPlayerPosition, newPlayerPosition, interpolateParameter);
            yield return null;
        }

        onMovedToPortal();
    }

    private void StartAnimationCoroutine() {
        StartCoroutine(PlayAnimationThenLoad());
    }

    IEnumerator PlayAnimationThenLoad() {
        GameObject.Find("Player").GetComponent<Animator>().SetBool("Teleporting", true);
        yield return new WaitForSeconds(playerTeleportClip.length - 0.1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
