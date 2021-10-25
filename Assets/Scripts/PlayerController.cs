using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public int amoutOfJumps = 1;

    private float movementInputDirecction;
    private bool isFacingRight = true;
    private bool isRunning;
    private bool isGrounded;
    private bool canJump;
    private int jumpsLeft;

    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpsLeft = amoutOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if(isGrounded && rb.velocity.y <= 0)
        {
            jumpsLeft = amoutOfJumps;
        }

        if(jumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    private void CheckInput()
    {
        //Check if A or D press. A = -1 and D = 1.
        movementInputDirecction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsLeft--;
        }
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirecction, rb.velocity.y);
    }

    private void CheckMovementDirection()
    {
        //check direction and face character in that direction
        if(isFacingRight && movementInputDirecction < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirecction > 0)
        {
            Flip();
        }

        double TOLERANCE = 1e-3;
        //check if is running
        if (!(Mathf.Abs(rb.velocity.x) < TOLERANCE))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void Flip()
    {
        //face character to the correct direction
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180.0f, 0);
    }


    private void UpdateAnimations()
    {
        //update animation parameters with our variables at code.
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
