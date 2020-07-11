using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private Rigidbody2D rigidbody;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform feetTransform;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float jumpForceMagnitude = 1.0f;
    [SerializeField]
    private float jetpackSpeed = 1.0f;
    [SerializeField]
    private Slider fuelSlider;
    [SerializeField]
    private float fuelConsumptionRate = 0.1f;
    [SerializeField]
    private float fuelRechargeRate = 0.2f;
    [SerializeField]
    private float movementSmoothing = 0.05f;
    [SerializeField]
    private float feetRadius = 0.1f;
    [SerializeField]
    private PlatformSpawner platformSpawner;
#pragma warning restore

    private bool facingRight = true;
    private bool isGrounded = false;
    private bool jetPacking = false;
    private LayerMask groundMask;
    private Vector3 velocity;

    private void Awake() {
        groundMask = LayerMask.GetMask(new string[] { "Ground" });
    }

    void Update() {
        float move = 0.0f;
        bool jump = false;
        bool holdJump = false;

        move = Input.GetAxis("Horizontal");
        jump = Input.GetKeyDown(KeyCode.Space);
        holdJump = Input.GetKey(KeyCode.Space);

        Move(move, jump, holdJump);

        if (isGrounded) {
            fuelSlider.value += fuelRechargeRate;
        }
    }

    private void FixedUpdate() {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(feetTransform.position, feetRadius, groundMask);
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject != gameObject) {
                isGrounded = true;
                jetPacking = false;
            }
        }
    }

    private void Move(float move, bool jump, bool holdJump) {

        Vector3 targetVelocity = new Vector2(move * speed, rigidbody.velocity.y);
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing);

        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("Boosting", false);
        animator.SetBool("HasFuel", FuelLeft());
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(rigidbody.velocity.x));

        if ((move < 0 && facingRight) || (move > 0 && !facingRight)) {
            Flip();
        }

        if (isGrounded && jump) {
            rigidbody.AddForce(Vector2.up * jumpForceMagnitude);

        } else if (jump) {
            jetPacking = true;
            Scramble();
        }

        if (!isGrounded && jetPacking && holdJump) {
            if (FuelLeft()) {
                fuelSlider.value -= fuelConsumptionRate;

                Vector3 jetpackTargetVelocity = new Vector2(rigidbody.velocity.x, jetpackSpeed);
                rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, jetpackTargetVelocity, ref velocity, movementSmoothing);

                animator.SetBool("Boosting", true);
            }
        }
    }

    private bool FuelLeft() {
        return fuelSlider.value > fuelSlider.minValue;
    }

    private void Scramble() {
        platformSpawner.RespawnPlatforms();
    }

    private void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
}
