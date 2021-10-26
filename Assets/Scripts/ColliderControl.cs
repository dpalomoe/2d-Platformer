using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour
{

    public BoxCollider2D stand;
    public BoxCollider2D crouch;
    public CircleCollider2D circle;

    PlayerController playerC;

    // Start is called before the first frame update
    void Start()
    {
        playerC = GetComponent<PlayerController>();
        stand.enabled = true;
        crouch.enabled = false;
        circle.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //not grounded
        if (playerC.isGrounded == false)
        {
            stand.enabled = true;
            crouch.enabled = false;
            circle.enabled = true;
        }
        else
        {
            if(playerC.isCrouching == true)
            {
                stand.enabled = false;
                crouch.enabled = true;
                circle.enabled = true;
            }
            else
            {
                stand.enabled = true;
                crouch.enabled = false;
                circle.enabled = true;
            }
        }
    }
}
