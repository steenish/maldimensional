using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions : MonoBehaviour {

    public static Vector3 RandomPointInBounds2D(Bounds bounds) {
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x),
                           Random.Range(bounds.min.y, bounds.max.y),
                           0.0f);
    }
}
