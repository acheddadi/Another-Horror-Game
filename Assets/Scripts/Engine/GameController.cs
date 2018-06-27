using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PauseScreen pause;
    [SerializeField] private TextScrolling textDisplay;
    [SerializeField]private AudioSource enemyMusic;
    private InteractableController currentInteraction;
    public static bool gameIsPaused = false, leftClick = false, chasingPlayer = false;
    public static int health = 100;
    
	// Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	void Update()
	{
        if (gameIsPaused) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
        if (Input.GetKeyDown("escape") && !gameIsPaused) pause.gameObject.SetActive(true);
        if (chasingPlayer && enemyMusic.volume < 1.0f) enemyMusic.volume = Mathf.Clamp(enemyMusic.volume + Time.deltaTime, 0.0f, 1.0f);
        else if (!chasingPlayer && enemyMusic.volume > 0.0f) enemyMusic.volume = Mathf.Clamp(enemyMusic.volume - Time.deltaTime, 0.0f, 1.0f);
	}

    public void PassInteraction(InteractableController interaction)
    {
        currentInteraction = interaction;
        textDisplay.DisplayDialogue(currentInteraction.ReturnDialogue(), currentInteraction.IsEvent());
    }

    public void PlayEvent()
    {
        currentInteraction.StartEvent();
    }
}
