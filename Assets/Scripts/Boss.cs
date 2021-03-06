using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;


    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    public bool isDead = false;
    private int hits = 0;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerHealth playerCongratulations;

    //References
    private Animator anim;
    private PlayerHealth playerHealth;
    private BossPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<BossPatrol>();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }      
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerHealth>();
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fireball"))
        {
            hits++;
            if (hits >= 5)
            {
                Die();
            }
            else
            {
                StartCoroutine("Hurt");
            }
        }
    }

    private IEnumerator Hurt()
    {
        Debug.Log("Boss Hurt");
        spriteRenderer.color = new Color(255, 255, 255, 0.3f);
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(.1f);
    }

    public void Die()
    {
        isDead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(gameObject, 0.5f);
        playerCongratulations.Congratulations();
    }
}
