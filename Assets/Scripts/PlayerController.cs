using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;

    private float movementInputDirecction;
    private bool isFacingRight = true;
    private bool isRunning;

    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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

        //check if is running
        if(rb.velocity.x != 0)
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
        //update isRunning parameter animation with our isRunning at code.
        anim.SetBool("isRunning", isRunning);
    }

}
