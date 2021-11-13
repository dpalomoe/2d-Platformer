using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private bool horizontal;

    private bool movingLeft;
    private bool movingUp;
    //The two edges of posible movements

    private float leftEdge;
    private float rightEdge;
    private float topEdge;
    private float botEdge;

    private bool inPlatform = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        topEdge = transform.position.y - movementDistance;
        botEdge = transform.position.y + movementDistance;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (horizontal)
        {
            //Movement of the Saw.
            if (movingLeft)
            {
                //Move left
                if (transform.position.x > leftEdge)
                {
                    //rb.velocity = new Vector2(speed * -1, rb.velocity.y);
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    //Next Update, move right
                    movingLeft = false;
                }
            }
            else
            {
                //Move right
                if (transform.position.x < rightEdge)
                {
                    //rb.velocity = new Vector2(speed * 1, rb.velocity.y);
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    //Next Update, move left
                    movingLeft = true;
                }
            }
        }
        else
        {
            if (movingUp)
            {
                //Move up
                if (transform.position.y > topEdge)
                {
                    //rb.velocity = new Vector2(rb.velocity.x, speed * -1);
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    //Next Update, move down
                    movingUp = false;
                }
            }
            else
            {
                //Move down
                if (transform.position.y < botEdge)
                {
                    //rb.velocity = new Vector2(rb.velocity.x, speed * 1);
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    //Next Update, move up
                    movingUp = true;
                }
            }
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("DENTRO");
            inPlatform = true;
            collision.transform.SetParent(transform);
        }
    }*/

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
