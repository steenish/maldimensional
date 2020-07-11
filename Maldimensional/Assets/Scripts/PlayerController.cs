using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

#pragma warning disable
    [SerializeField]
    private Rigidbody2D rigidbody;
    [SerializeField]
    private Transform spawnPoint;
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
    private float feetExtentY;
    [SerializeField]
    private float feetExtentShrinkX;
    [SerializeField]
    private PlatformSpawner platformSpawner;
    [SerializeField]
    private ScrambleBlinker blinker;
#pragma warning restore

    private AudioManager audioManager;
    private bool facingRight = true;
    private bool isGrounded = true;
    private bool jetPacking = false;
    private bool paused = true;
    private LayerMask groundMask;
    private Vector3 feetExtent;
    private Vector3 velocity;

    private void OnEnable() {
        LoadingFader.onLoadIsFinished += UnPause;
    }

    private void OnDisable() {
        LoadingFader.onLoadIsFinished -= UnPause;
    }

    private void UnPause() {
        paused = false;
    }

    private void Awake() {
        groundMask = LayerMask.GetMask(new string[] { "Ground" });
        feetExtent = GetComponent<Collider2D>().bounds.size - Vector3.right * feetExtentShrinkX;
        feetExtent.y = feetExtentY;
    }

    private void Start() {
        audioManager = AudioManager.instance;

        transform.position = spawnPoint.position;
    }

    void Update() {
        if (paused) return;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            audioManager.Play("BackToMenu");
            SceneManager.LoadScene("StartScene");
        }

        float move = 0.0f;
        bool jump = false;
        bool holdJump = false;

        move = Input.GetAxis("Horizontal");
        jump = Input.GetKeyDown(KeyCode.Space);
        holdJump = Input.GetKey(KeyCode.Space);

        Move(move, jump, holdJump);

        if (isGrounded) {
            fuelSlider.value += fuelRechargeRate * Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        if (paused) return;

        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(feetTransform.position, feetExtent, 0.0f, groundMask);
        foreach (Collider2D collider in colliders) {
            if (collider.gameObject != gameObject) {
                isGrounded = true;
                jetPacking = false;

                if (!wasGrounded) {
                    audioManager.Play("Landing");
                    Debug.Log("play landing");
                }
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
            audioManager.Play("Jump");
            rigidbody.AddForce(Vector2.up * jumpForceMagnitude);
        } else if (jump) {
            jetPacking = true;
            Scramble();
        }

        if (!isGrounded && jetPacking && holdJump) {
            if (FuelLeft()) {
                fuelSlider.value -= fuelConsumptionRate * Time.deltaTime;

                Vector3 jetpackTargetVelocity = new Vector2(rigidbody.velocity.x, jetpackSpeed);
                rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, jetpackTargetVelocity, ref velocity, movementSmoothing);

                animator.SetBool("Boosting", true);
                audioManager.Play("Boosting");
            } else {
                audioManager.Stop("Boosting");
            }
        } else {
            audioManager.Stop("Boosting");
        }
    }

    private bool FuelLeft() {
        return fuelSlider.value > fuelSlider.minValue;
    }

    private void Scramble() {
        platformSpawner.RespawnPlatforms();
        blinker.ScrambleBlink();

        audioManager.Play("Scramble");
    }

    private void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
}
