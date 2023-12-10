using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private SpriteRenderer sprite;
    private Animator animation;

    [SerializeField] private LayerMask jumpableGround;

    private float move = 0f;
    [SerializeField] public float moveSpeed = 7f;
    [SerializeField] public float jumpForce = 14f;

    private enum MovementState {spoczynek, bieg, skok}

    [SerializeField] private AudioSource jumpSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
    }

    private void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        AnimationUpdate();
    }

    private void AnimationUpdate()
    {
        MovementState state;

        if (move > 0f)
        {
            state = MovementState.bieg;
            sprite.flipX = false;
        }
        else if (move < 0f)
        {
            state = MovementState.bieg;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.spoczynek;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.skok;
        }

        animation.SetInteger("State", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
