using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleEnemyHeadDetect : MonoBehaviour
{

    GameObject Enemy;
    private Eagle eagle;
    public bool gettingHit = false;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject.transform.parent.gameObject;
        eagle = Enemy.GetComponent<Eagle>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            gettingHit = true;
            var magnitude = 2000;

            var force = transform.position - collision.transform.position;
            force.Normalize();
            collision.transform.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
            GetComponent<BoxCollider2D>().enabled = false;
            eagle.Die();
        }
    }
}
