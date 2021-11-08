using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadDetect : MonoBehaviour
{

    GameObject Enemy;
    private Animator anim;
    private Frog frog;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject.transform.parent.gameObject;
        anim = Enemy.GetComponent<Animator>();
        frog = Enemy.GetComponent<Frog>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        frog.Die();
    }
}
