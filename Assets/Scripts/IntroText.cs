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

	// Use this for initialization
	void Start()
	{
		sentencesQueue = new Queue<string>();
		StartDialogue();
	}
	public void StartDialogue()
	{
		Debug.Log("Entro en Start");

		numOfSentences = sentences.Length;
		Debug.Log("El numero de frases es "+ numOfSentences);

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
		Debug.Log("Entro aqui????");
		Debug.Log(actualSentence);
		actualSentence++;
		Debug.Log("Despues de sumar "+ actualSentence);
	}

	IEnumerator TypeSentence(string sentence)
	{
		Debug.Log("He apretado el boton");
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		SceneManager.LoadScene("MainScene");
	}
}
