using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCd;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cdTimer;

    private void Attack()
    {
        cdTimer = 0;

        arrows[FindFireball()].transform.position = firePoint.position;
        arrows[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile(); 
    }

    private int FindFireball()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        cdTimer += Time.deltaTime;

        if(cdTimer >= attackCd)
        {
            Attack();
        }
    }
}
