using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundsChecking : MonoBehaviour {

    private ScoreManager scoreManager;

    private void Start() {
        scoreManager = ScoreManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            scoreManager.ResetScore();
            AudioManager.instance.Play("Death");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
