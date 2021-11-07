using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public bool facingRight = true;
    public LayerMask whatIsGround;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isIdle = true;
    public bool isFalling = false;

    public float jumpForceX = 2f;
    public float jumpForceY = 4f;

    public float lastYPos = 0;

    public float idleTime = 2f;
    public float currentIdleTime = 0;

    //patrol 
    public bool mustPatrol = true;
    private bool mustFlip = false;
    [SerializeField] private Transform groundCheckPos;

    public enum Animations
    {
        Idle = 0,
        Jumping = 1,
        Falling = 2
    };

    public Animations currentAnim;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        lastYPos = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (mustPatrol)
        {
            startPatrol();
        }*/
        if (mustFlip)
        {
            Flip();
        }
    }

    private void startPatrol()
    {
        if (mustFlip)
        {
            Flip();
        }
        //Jump();
    }

    private void Flip()
    {
        mustPatrol = false;
        facingRight = !facingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        //spriteRenderer.flipX = facingRight;
        mustPatrol = true;
    }

    private void FixedUpdate()
    {
        if (isIdle)
        {
            if (mustPatrol)
            {
                mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, whatIsGround);
                Debug.Log("MustFlip = " + mustFlip);
            }
            currentIdleTime += Time.deltaTime;
            if(currentIdleTime >= idleTime)
            {
                currentIdleTime = 0;
                //facingRight = !facingRight;
                //spriteRenderer.flipX = facingRight;  //FLIP
                Jump();
            }
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f), whatIsGround);

        if (isGrounded && isFalling)
        {
            isFalling = false;
            isJumping = false;
            isIdle = true;
            ChangeAnimation(Animations.Idle);
        }
        else if (transform.position.y > lastYPos && !isGrounded && !isIdle)
        {
            isJumping = true;
            isFalling = false;
            ChangeAnimation(Animations.Jumping);
        }
        else if (transform.position.y < lastYPos && !isGrounded && !isIdle)
        {
            isJumping = false;
            isFalling = true;
            ChangeAnimation(Animations.Falling);
        }
        lastYPos = transform.position.y;
         
    }

    void Jump()
    {
        isJumping = true;
        isIdle = false;
        int direction = 0;
        if (facingRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        rb.velocity = new Vector2(jumpForceX * direction, jumpForceY);
        Debug.Log("Jump!");
    }

    void ChangeAnimation(Animations newAnim)
    {
        if(currentAnim != newAnim)
        {
            currentAnim = newAnim;
            anim.SetInteger("state", (int)newAnim);
        }
    }
}
