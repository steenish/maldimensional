using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private PlayerPhysics playerPhysics;
#pragma warning restore

    void Start() {
        
    }

    void Update() {
        float move = 0.0f;
        bool jump = false;

        move = Input.GetAxis("Horizontal");
        jump = Input.GetKeyDown(KeyCode.Space);

        playerPhysics.Move(move, jump);
    }
}
