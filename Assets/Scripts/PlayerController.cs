using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public Transform groundCheck;
    public Transform wallCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public int amoutOfJumps = 1;

    private float movementInputDirecction;
    public bool isFacingRight = true;
    private bool isRunning;
    public bool isGrounded;
    private bool canJump;
    private int jumpsLeft;
    private bool crouch;
    public bool isCrouching;


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
        //isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if(isGrounded && rb.velocity.y <= 0.1)
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

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown("w"))
        {
            Jump();
        }

        if (Input.GetKey("s"))
        {
            Crouch();
        }

        if (!Input.GetKey("s"))
        {
            isCrouching = false;
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

    private void Crouch()
    {
        if (isGrounded == true && isRunning == false)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
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
        anim.SetBool("isCrouching", isCrouching);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        //Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    public bool canAttack()
    {
        return movementInputDirecction == 0 && (isGrounded == true);
    }

    //Method to stop player movement.
    public void Stop()
    {
        rb.velocity = new Vector2(0, 0);
    }
}
