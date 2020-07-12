using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloudAnimator : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private GameObject initialLeftClouds;
    [SerializeField]
    private GameObject initialCenterClouds;
    [SerializeField]
    private GameObject initialRightClouds;
    [SerializeField]
    private float deltaMovement;
#pragma warning restore

    private GameObject leftClouds;
    private GameObject centerClouds;
    private GameObject rightClouds;

    private static CloudAnimator instance;

    private void OnEnable() {
        SceneManager.sceneLoaded += DeleteOnWrongScene;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= DeleteOnWrongScene;
    }

    void Awake() {
        // Handle audio manager instancing between scene loads.
        // If there is no instance, let this be the new instance, otherwise, destroy this object.
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        // If this object was set as the instance, make sure it is not destroyed on scene loads.
        DontDestroyOnLoad(gameObject);
    }

    private void DeleteOnWrongScene(Scene scene, LoadSceneMode mode) {
        if (!scene.name.Equals("MainScene")) {
            Destroy(gameObject);
        }
    }

    void Start() {
        leftClouds = initialLeftClouds;
        centerClouds = initialCenterClouds;
        rightClouds = initialRightClouds;
    }

    void Update() {
        leftClouds.transform.position += Vector3.left * deltaMovement * Time.deltaTime;
        centerClouds.transform.position += Vector3.left * deltaMovement * Time.deltaTime;
        rightClouds.transform.position += Vector3.left * deltaMovement * Time.deltaTime;

        if (centerClouds.transform.position.x <= 0.0f) {
            leftClouds.transform.position = rightClouds.transform.position + Vector3.right * 20.0f;

            GameObject tempOldLeftClouds = leftClouds;
            leftClouds = centerClouds;
            centerClouds = rightClouds;
            rightClouds = tempOldLeftClouds;
        }
    }
}
