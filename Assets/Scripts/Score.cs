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
            score = score + 300;
            scoreText.text = "Score : " + score;
        }
    }
}
