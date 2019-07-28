using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image controlPanel;

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
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
        Application.Quit();
    }
}
