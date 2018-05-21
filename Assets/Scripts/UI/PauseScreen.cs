using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = true;
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
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
