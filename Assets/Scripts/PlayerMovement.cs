using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 8f;
    public float gravity = -20f;
    public float airControlMultiplier = 0.5f;

    [Header("Jumping")]
    public float jumpCooldown = 0.25f;
    private bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;
    private bool grounded;

    [Header("References")]
    public Transform orientation;

    private CharacterController controller;
    private Vector3 velocity;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check using sphere
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        MyInput();
        ApplyGravity();
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        float controlMultiplier = grounded ? 1f : airControlMultiplier;

        controller.Move(moveDirection.normalized * moveSpeed * controlMultiplier * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime); // Apply gravity/jump force
    }

    private void ApplyGravity()
    {
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f; // Stick to ground
        }

        velocity.y += gravity * Time.deltaTime;
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        readyToJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}