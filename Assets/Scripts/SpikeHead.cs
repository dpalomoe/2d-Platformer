using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private bool horizontal;
    
    private bool movingLeft;
    private bool movingUp;
    //The edges of the movements
    private float leftEdge;
    private float rightEdge;
    private float topEdge;
    private float botEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        topEdge = transform.position.y - movementDistance;
        botEdge = transform.position.y + movementDistance;
    }

    // Update is called once per frame
    private void Update()
    {
        if (horizontal)
        {
            //Movement of the SpikeHead.
            if (movingLeft)
            {
                //Move left
                if (transform.position.x > leftEdge)
                {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If this object collides with Player, call function TakeDamage.
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
