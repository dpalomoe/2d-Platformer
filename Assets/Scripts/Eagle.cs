using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{

    [SerializeField] private float damage;
    private bool isDead = false;
    private int hits = 0;
    private SpriteRenderer spriteRenderer;
    public EagleEnemyHeadDetect jumpHead;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fireball"))
        {
            hits++;
            if (hits >= 3)
            {
                Die();
            }
            else
            {
                StartCoroutine("Hurt");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (jumpHead.gettingHit == false)
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }     
        }
    }
    public void Die()
    {
        isDead = true;
        spriteRenderer.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(gameObject.transform.parent.gameObject, 0.4f);
    }

    private IEnumerator Hurt()
    {
        spriteRenderer.color = new Color(255, 255, 255, 0.3f);
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(.1f);
    }
}
