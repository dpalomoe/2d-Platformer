using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadDetect : MonoBehaviour
{

    GameObject Enemy;
    private Frog frog;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject.transform.parent.gameObject;
        frog = Enemy.GetComponent<Frog>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var magnitude = 350;

        var force = transform.position - collision.transform.position;
        force.Normalize();
        collision.transform.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
        GetComponent<BoxCollider2D>().enabled = false;
        frog.Die();
    }
}
