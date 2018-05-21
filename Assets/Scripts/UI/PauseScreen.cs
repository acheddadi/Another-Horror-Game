using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField]private AudioClip pauseSFX, continueSFX;
    [SerializeField]private AudioSource backgroundMusic, guiSFX;

    private void OnEnable()
    {
        backgroundMusic.Pause();
        guiSFX.clip = pauseSFX;
        guiSFX.Play();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        backgroundMusic.Play();
        guiSFX.clip = continueSFX;
        guiSFX.Play();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
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
