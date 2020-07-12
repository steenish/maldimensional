using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PortalController : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private AnimationClip playerTeleportClip;
#pragma warning restore

    private ScoreManager scoreManager;

    private void Start() {
        scoreManager = ScoreManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            scoreManager.IncrementScore();
            scoreManager.SaveHighScore();

            AudioManager.instance.Play("Teleport");

            GameObject player = collision.gameObject;

            collision.GetComponent<PlayerController>().enabled = false;

            StartCoroutine(WaitForAnimation(player));
        }
    }

    IEnumerator WaitForAnimation(GameObject playerGameObject) {
        Transform playerTransform = playerGameObject.transform;
        Vector3 oldPlayerPosition = playerGameObject.transform.position;
        Vector3 newPlayerPosition = GetComponent<Renderer>().bounds.center;
        float interpolateParameter = 0.0f;
        for (int i = 0; i < 10; ++i) {
            interpolateParameter += 1 / 10;
            playerTransform.position = Vector3.Lerp(oldPlayerPosition, newPlayerPosition, interpolateParameter);
        }

        playerGameObject.GetComponent<Animator>().SetBool("Teleporting", true);
        yield return new WaitForSeconds(playerTeleportClip.length - 0.1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
