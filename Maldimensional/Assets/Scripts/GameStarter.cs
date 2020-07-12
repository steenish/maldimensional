using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private GameObject logo;
#pragma warning restore

    private void Start() {
        AudioManager.instance.Play("Menu");

        Instantiate(logo);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            AudioManager.instance.Stop("Menu");
            AudioManager.instance.Play("StartGame");
            SceneManager.LoadScene("MainScene");
        }
    }
}
