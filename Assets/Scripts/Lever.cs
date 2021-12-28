using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    [SerializeField] private GameObject otherLever;
    [SerializeField] private GameObject[] blocksToHide;
    private bool triggered;


    private void Update()
    {
        if (Input.GetKey("e"))
        {
            if (triggered == true)
            {
                for (int i = 0; i < blocksToHide.Length; i++)
                {
                    blocksToHide[i].SetActive(false);
                }
                gameObject.SetActive(false);
                otherLever.SetActive(true);
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
