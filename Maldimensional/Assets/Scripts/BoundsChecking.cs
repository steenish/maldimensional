using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundsChecking : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private ScoreManager scoreManager;
#pragma warning restore

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("portal collision");
        if (collision.CompareTag("Player")) {
            scoreManager.ResetScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
