using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Sprinting")]
    [SerializeField] private float sprintMultiplier = 1.5f;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight;

    [Header("Ground Detection")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;

    [Header("Drag Control")]
    [SerializeField] private float groundDrag = 1.0f;
    [SerializeField] private float airDrag = 0.5f;

    Rigidbody rb;
    Vector3 direction;
    float multiplier = 1.0f;

    private void Start() {
        
        rb = gameObject.GetComponent<Rigidbody>();

    }

    private void Update() {

        if (!Player.instance.canMove) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical   = Input.GetAxisRaw("Vertical");

        direction = transform.forward * vertical + transform.right * horizontal;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded) { rb.drag = groundDrag; multiplier = 1.0f; } else { rb.drag = airDrag; multiplier = 0.3f; }
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) { multiplier = sprintMultiplier; }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {

            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);

        }

    }

    private void FixedUpdate() {

        if (!Player.instance.canMove) return;

        rb.AddForce(direction.normalized * moveSpeed * 100.0f * Time.deltaTime * multiplier, ForceMode.Acceleration);

    }

}