using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateSetter : MonoBehaviour {

    private static FrameRateSetter instance;

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

        Application.targetFrameRate = 60;
    }
}
