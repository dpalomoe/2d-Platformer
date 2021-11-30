using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private PlayerHealth dead;
    [SerializeField] private Boss end;
    public AudioSource fireClip;
    public bool paused = false;



    private Animator anim;
    private PlayerController playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerController>();
        dead = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        if (!playerMovement.isCrouching && !dead.dead && !paused && !end.isDead)
        {
            anim.SetTrigger("attack");
            fireClip.Play();
            cooldownTimer = 0;

            fireballs[FindFireball()].transform.position = firePoint.position;

            if (playerMovement.isFacingRight == true)
            {
                fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(1);
            }
            else
            {
                fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(-1);
            }
        }  
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
