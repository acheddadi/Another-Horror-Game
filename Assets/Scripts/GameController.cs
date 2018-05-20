using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { NORMAL, PAUSE, DIALOGUE, CUTSCENE }
    private GameState currentState = GameState.NORMAL;
    private Queue<string> dialogue;
	// Use this for initialization
    void Start()
    {
        dialogue = new Queue<string>();
        Cursor.visible = false;
    }

	void Update()
	{
		if (Input.GetKey("escape"))Application.Quit();

        switch (currentState)
        {
            case GameState.NORMAL:
            break;
            case GameState.PAUSE:
            break;
            case GameState.DIALOGUE:
            break;
            case GameState.CUTSCENE:
            break;
        }
	}

    public void PassDialogue(Dialogue pass)
    {
        for (int i = 0; i < pass.dialogue.Length; i++)
        {
            dialogue.Enqueue(pass.dialogue[i]);
        }
        currentState = GameState.DIALOGUE;
    }

    public void NextDialogue()
    {
        string sentence = dialogue.Dequeue();
        Debug.Log(sentence);
        dialogue.Dequeue();
    }

    public GameState GetState()
    {
        return currentState;
    }
}
