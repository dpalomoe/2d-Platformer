using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;

    private void Start()
    {
        scoreText.text = "Score : " + score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Gem gem = collision.gameObject.GetComponent<Gem>();
            if (!gem.picked)
            {
                score = score + 100;
                scoreText.text = "Score : " + score;
                gem.picked = true;
            }
        }
    }
}
