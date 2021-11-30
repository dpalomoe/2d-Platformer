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
            //if collides with Player, disable gem, show animation and then destroy objects.
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            clip.Play();
            Destroy(gameObject, 0.5f);
        }
    }

}
