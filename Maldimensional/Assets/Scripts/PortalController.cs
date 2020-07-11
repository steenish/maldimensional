using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private ScoreManager scoreManager;
#pragma warning restore

    private void OnTriggerEnter(Collider other) {
        scoreManager.IncrementScore();
        scoreManager.SaveHighScore();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
