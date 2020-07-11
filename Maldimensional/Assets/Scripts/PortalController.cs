using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour {

    private ScoreManager scoreManager;

    private void Start() {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            scoreManager.IncrementScore();
            scoreManager.SaveHighScore();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
