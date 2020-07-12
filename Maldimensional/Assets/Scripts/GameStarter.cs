using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {

    private void Start() {
        AudioManager.instance.Play("Menu");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            AudioManager.instance.Stop("Menu");
            AudioManager.instance.Play("StartGame");
            SceneManager.LoadScene("MainScene");
        }
    }
}
