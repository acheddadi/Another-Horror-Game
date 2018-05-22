using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField]private AudioClip pauseSFX, continueSFX;
    [SerializeField]private AudioSource backgroundMusic, guiSFX;

    private void OnEnable()
    {
        backgroundMusic.volume = 0.25f;
        guiSFX.clip = pauseSFX;
        guiSFX.Play();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameController.gameIsPaused = true;
    }

    private void OnDisable()
    {
        backgroundMusic.volume = 1.0f;
        guiSFX.clip = continueSFX;
        guiSFX.Play();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameController.gameIsPaused = false;
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
