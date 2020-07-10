using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPhysics : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private Rigidbody2D rigidbody;
    [SerializeField]
    private Transform feetTransform;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float jumpForceMagnitude = 1.0f;
    [SerializeField]
    private float movementSmoothing = 0.05f;
    [SerializeField]
    private float feetRadius = 0.1f;
#pragma warning restore

    private bool isGrounded = false;
    private LayerMask groundMask;
    private Vector3 velocity;

    private void Awake() {
        groundMask = LayerMask.GetMask(new string[] { "Ground" });
    }

    void Start() {
        
    }

    void Update() {
        
    }

    private void FixedUpdate() {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(feetTransform.position, feetRadius, groundMask);
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject != gameObject) {
                isGrounded = true;
            }
        }
    }

    public void Move(float move, bool jump) {

        Vector3 targetVelocity = new Vector2(move * speed, rigidbody.velocity.y);
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (isGrounded && jump) {
            rigidbody.AddForce(Vector2.up * jumpForceMagnitude);
            Debug.Log("jump");
        } else if (jump) {

        }
    }
}
