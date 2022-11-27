using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D rigidBody2D;
    CapsuleCollider2D bodCapsuleCollider2D;
    BoxCollider2D feetBoxCollider2D;
    Animator animator;
    float gravityScaleAtStart;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        bodCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        feetBoxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        gravityScaleAtStart = rigidBody2D.gravityScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

   

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = playerVelocity;

        bool playerHasSpeedX = Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Epsilon;

        animator.SetBool("Running", playerHasSpeedX);
    }
    private void FlipSprite()
    {
        bool playerHasSpeedX = Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasSpeedX)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody2D.velocity.x), 1f);
        }
    }
    void ClimbLadder()
    {
        if (!feetBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rigidBody2D.gravityScale = gravityScaleAtStart;
            animator.SetBool("Climbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(rigidBody2D.velocity.x, moveInput.y * climbSpeed);
        rigidBody2D.velocity = climbVelocity;
        rigidBody2D.gravityScale = 0f;

        bool playerHasSpeedY = rigidBody2D.velocity.y > Mathf.Epsilon;
        animator.SetBool("Climbing", playerHasSpeedY);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!feetBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if(value.isPressed)
        {
            rigidBody2D.velocity = new Vector2(0f, jumpSpeed);
        }

    }

}
