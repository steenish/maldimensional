using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PortalController : MonoBehaviour {

    private ScoreManager scoreManager;

    private void Start() {
        scoreManager = ScoreManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            scoreManager.IncrementScore();
            scoreManager.SaveHighScore();
            Debug.Log(scoreManager.score);
            AudioManager.instance.Play("Teleport");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
