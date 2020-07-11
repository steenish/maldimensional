using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredefinedPlatform : MonoBehaviour {

    [SerializeField]
    private float maxYPerturbance = 1.0f;

    void Awake() {
        Debug.Log("old position: " + transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-maxYPerturbance, maxYPerturbance), transform.position.z);
        Debug.Log("new position: " + transform.position);
    }
}
