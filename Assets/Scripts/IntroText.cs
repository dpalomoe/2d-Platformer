using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroText : MonoBehaviour
{
	public Text dialogueText;
	public string[] sentences;
	private Queue<string> sentencesQueue;
	private int numOfSentences;
	private int actualSentence = 0;
	private bool endDialogue = false;
	public GameObject tutorial;
	public GameObject canvas;
	public GameObject playerImage;
	public GameObject background;

	// Use this for initialization
	void Start()
	{
		sentencesQueue = new Queue<string>();
		StartDialogue();
	}
	public void StartDialogue()
	{
		numOfSentences = sentences.Length;
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (actualSentence >=numOfSentences)
		{
			EndDialogue();
			return;
		}
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentences[actualSentence]));
		actualSentence++;
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		endDialogue = true;
		canvas.SetActive(false);
		tutorial.SetActive(true);
		playerImage.SetActive(false);
		background.SetActive(false);
	}

    private void Update()
    {
		if (Input.GetKeyDown("space") && endDialogue == true)
        {
			SceneManager.LoadScene("MainScene");
		}
    }
}
