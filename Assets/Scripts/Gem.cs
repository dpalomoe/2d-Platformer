using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gem : MonoBehaviour
{
    public bool picked;

    public AudioSource clip;

    private void Start()
    {
        picked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador toca Gema");
            //if collides with Player, disable gem, show animation and then destroy objects.
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log(picked);
            /*if (picked == false)
            {
                Debug.Log("Entro al play");
                clip.Play();
                picked = true;
            }*/
            Debug.Log(picked);
            Debug.Log("Destroy");
            Destroy(gameObject, 0.5f);
        }
    }

}
