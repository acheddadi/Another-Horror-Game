﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PauseScreen pause;
    [SerializeField]private TextScrolling textDisplay;
    public static bool gameIsPaused = false;
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
	}

    public void PassDialogue(Dialogue pass)
    {
        textDisplay.DisplayDialogue(pass);
    }
}