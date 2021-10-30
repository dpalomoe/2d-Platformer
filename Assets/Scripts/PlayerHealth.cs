using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float startingHealth;
    //public variable but can only be modified in this class.
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    public GameOverScreen gameOverScreen;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damageReceived)
    {

        //update currentHealth. The value is going to be between 0 and startingHealth.
        currentHealth = Mathf.Clamp(currentHealth - damageReceived, 0, startingHealth);


        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
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
                anim.SetTrigger("die");
                GetComponent<PlayerController>().enabled = false;
                dead = true;
                //Wait and show GameOverScreen
                StartCoroutine("WaitGameOver");
            }
        }
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
        //TODO Implement points
        gameOverScreen.Setup(100);
    }
}
