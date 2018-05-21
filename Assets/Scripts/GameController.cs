using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PauseScreen pause;
    private Queue<string> dialogue;
	// Use this for initialization
    void Start()
    {
        dialogue = new Queue<string>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	void Update()
	{
        if (Input.GetKeyDown("escape")) pause.gameObject.SetActive(true);
	}

    public void PassDialogue(Dialogue pass)
    {
        for (int i = 0; i < pass.dialogue.Length; i++)
        {
            dialogue.Enqueue(pass.dialogue[i]);
        }
        NextDialogue();
    }

    public void NextDialogue()
    {
        string sentence = dialogue.Dequeue();
        Debug.Log(sentence);
        dialogue.Dequeue();
    }
}
