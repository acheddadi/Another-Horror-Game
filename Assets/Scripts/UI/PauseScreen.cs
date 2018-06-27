using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseScreen : MonoBehaviour
{
    [SerializeField]private AudioClip pauseSFX, continueSFX;
    [SerializeField]private AudioSource guiSFX;
    [SerializeField]private AudioMixer mixer;
    private float maxVol, volRange;
    private const float MIN_VOL = -80.0f;

    private void Awake()
    {
        mixer.GetFloat("slaveVol", out maxVol);
        volRange = maxVol - MIN_VOL;
    }

    private void OnEnable()
    {
        mixer.SetFloat("slaveVol", MIN_VOL + volRange / 1.25f);
        guiSFX.clip = pauseSFX;
        guiSFX.Play();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameController.gameIsPaused = true;
    }

    private void OnDisable()
    {
        mixer.SetFloat("slaveVol", maxVol);
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
