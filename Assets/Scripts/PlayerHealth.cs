using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float startingHealth;
    //public variable but only can be modified in this class.
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;
    public GameOverScreen gameOverScreen;
    public ContratulationsScreen contratulationsScreen;
    public AudioSource gameOverClip;
    public AudioSource congratulationsClip;

    [SerializeField] private float inmuneDuration;
    private SpriteRenderer spriteRend;
    private bool canTakeDamage;

    private float timer = Mathf.Infinity;
    [SerializeField] private Score score;
    private int aux;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float damageReceived)
    {

        //update currentHealth. The value is going to be between 0 and startingHealth.
        if (canTakeDamage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damageReceived, 0, startingHealth);

            if (currentHealth > 0)
            {
                canTakeDamage = false;
                anim.SetTrigger("hurt");
                StartCoroutine("Invencible");
                //TODO need to add cooldown to receive more damage.
                //TODO try to do a mini jump or something like that when receive damage
            }
            else
            {
                //avoid play die animation twice
                if (!dead)
                {
                    //If die, stop moving and play animation.
                    GetComponent<PlayerController>().Stop();
                    anim.SetBool("isDead", true);
                    AudioManager.instance.StopMusic();
                    gameOverClip.Play();
                    anim.SetTrigger("die");
                    GetComponent<PlayerController>().enabled = false;
                    dead = true;
                    //Wait and show GameOverScreen
                    StartCoroutine("WaitGameOver");
                }
            }
        } 
    }

    private IEnumerator Invencible()
    {
        //Ignore collisions between player and enemies or traps.
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < 2; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(inmuneDuration / 4);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(inmuneDuration / 4);
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    public void AddHealth(float healhToAdd)
    {
        //update currentHealth. The value is going to be between 0 and startingHealth.
        //TODO Pending to implement Collectionable
        //currentHealth = Mathf.Clamp(currentHealth + healhToAdd, 0, startingHealth);
    }

    //Function to wait 2 seconds before show GameOverScreen
    private IEnumerator WaitGameOver()
    {
        yield return new WaitForSeconds(2f);
        //TODO Implement points.
        gameOverScreen.Setup(score.score);
    }

    private void Update()
    {
        if (timer > inmuneDuration)
        {
            canTakeDamage = true;
        }
        timer += Time.deltaTime;
    }

    public void Congratulations()
    {
        AudioManager.instance.StopMusic();
        congratulationsClip.Play();
        StartCoroutine("WaitCongratulations");
    }

    private IEnumerator WaitCongratulations()
    {
        yield return new WaitForSeconds(2f);
        contratulationsScreen.Setup(score.score);
    }
}
