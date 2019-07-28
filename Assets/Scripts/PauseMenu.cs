using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Image controlPanel;
    public bool isPaused;

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && isPaused == false)
        {
            Pause();
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            Resume();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale= 0f;
    }

    public void OpenControls()
    {
        controlPanel.gameObject.SetActive(true);
    }

    public void CloseControls()
    {
        controlPanel.gameObject.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
;