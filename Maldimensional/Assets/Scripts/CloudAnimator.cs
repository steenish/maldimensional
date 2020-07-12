using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
