using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Image controlPanel;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
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