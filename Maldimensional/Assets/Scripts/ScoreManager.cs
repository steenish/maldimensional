using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private Sprite beatDevsSprite;
    [SerializeField]
    private int devHighScore;
#pragma warning restore

    public int score { get; private set; } = 0;

    public static ScoreManager instance;

    private TMP_Text scoreText;
    private TMP_Text highScoreText;

    private void OnEnable() {
        SceneManager.sceneLoaded += NewSceneDisplayScores;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= NewSceneDisplayScores;
    }

    void Awake() {
        // Handle score manager instancing between scene loads.
        // If there is no instance, let this be the new instance, otherwise, destroy this object.
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void IncrementScore() {
        ++score;
    }

    public void ResetScore() {
        score = 0;
    }

    public void SaveHighScore() {
        int currentHighScore = PlayerPrefs.GetInt("HighScore");
        if (score > currentHighScore) {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    private void NewSceneDisplayScores(Scene scene, LoadSceneMode mode) {
        switch (scene.name) {
            case "MainScene":
                scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
                highScoreText = GameObject.Find("HighScoreText").GetComponent<TMP_Text>();
                GameObject scoreboard = GameObject.Find("Scoreboard");

                if (PlayerPrefs.GetInt("HighScore") > devHighScore) {
                    scoreboard.GetComponent<SpriteRenderer>().sprite = beatDevsSprite;
                    // Maybe change position etc.
                }

                scoreText.text = FormatScoreText(score);
                highScoreText.text = FormatScoreText(PlayerPrefs.GetInt("HighScore"));
                break;
            case "StartScene":
                break;
        }
    }

    private string FormatScoreText(int scoreToFormat) {
        scoreToFormat = Mathf.Clamp(scoreToFormat, 0, 99);
        return ((scoreToFormat < 10) ? "0" : "") + scoreToFormat;
    }
}
