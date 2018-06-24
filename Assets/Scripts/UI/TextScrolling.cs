using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScrolling : MonoBehaviour
{
	[SerializeField]private float scrollSpeed = 3.0f;
	[SerializeField]private Text textField;
	[SerializeField]private GameObject selection;
	private string fullSentence, scrollSentence;
	private float oldTime;
	private bool doneScrolling = false, displayingText = false, unPauseOnce = false, isEvent = false;
	private Queue<string> paragraph;

	// Use this for initialization
	void Start()
	{
		oldTime = Time.unscaledTime;
		paragraph = new Queue<string>();
		textField.text = scrollSentence = fullSentence = string.Empty;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		if (displayingText)
		{
			GameController.gameIsPaused = true;
			if (scrollSpeed <= 0) scrollSpeed = 1;
			if ((Time.unscaledTime > oldTime + 1 / scrollSpeed) && !doneScrolling)
			{
				if (scrollSentence != fullSentence)
				{
					scrollSentence = scrollSentence.Insert(scrollSentence.Length, fullSentence[scrollSentence.Length].ToString());
				}
				else doneScrolling = true;
				oldTime = Time.unscaledTime;
			}
			if (!isEvent && Input.GetMouseButtonDown(0)) NextSentence();
			else
			{
				if (paragraph.Count == 0)
				{
					if (scrollSentence == fullSentence) ShowSelection();
				}
				else if (Input.GetMouseButtonDown(0)) NextSentence();
			}
		}
		else if (unPauseOnce)
		{
			GameController.gameIsPaused = false;
			unPauseOnce = false;
		}
		textField.text = scrollSentence;
	}

	public void DisplayDialogue(Dialogue text, bool ev)
	{
		isEvent = ev;
		displayingText = true;
		foreach (string i in text.dialogue) paragraph.Enqueue(i);
		//NextSentence();
	}

	public void NextSentence()
	{
		if (paragraph.Count > 0)
		{
			scrollSentence = string.Empty;
			fullSentence = paragraph.Dequeue();
			Debug.Log(fullSentence);
			doneScrolling = false;
		}
		else
		{
			scrollSentence = fullSentence = string.Empty;
			displayingText = false;
			unPauseOnce = true;
			isEvent = false;
			HideSelection();
		}
	}

	private void ShowSelection()
	{
		selection.SetActive(true);
		Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
	}

	private void HideSelection()
	{
		selection.SetActive(false);
		Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
}
