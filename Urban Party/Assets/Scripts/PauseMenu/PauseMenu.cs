using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public bool inSettings = false;
    public bool inConOrCred = false;
    public GameObject pausePanel;
    //public GameObject settingPanel;
    public GameObject creditsPanel;
    //public GameObject controlsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;

                if (inConOrCred)
                {
                    Pause();
                }

            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        //inSettings = false;
        inConOrCred = false;
        //settingPanel.SetActive(false);
        creditsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Restart()
    {
        Debug.Log("Restarting level...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void ReturnToTitle()
    {
        Debug.Log("Returning to title...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title 1");
    }

    //public void Settings()
    //{
    //    //settingPanel.SetActive(true);
    //    inSettings = true;
    //    inConOrCred = false;
    //    pausePanel.SetActive(false);
    //    creditsPanel.SetActive(false);
    //    //controlsPanel.SetActive(false);
    //    Cursor.lockState = CursorLockMode.None;
    //    Cursor.visible = true;
    //}

    public void Credits()
    {
        creditsPanel.SetActive(true);
        inConOrCred = true;
        //settingPanel.SetActive(false);
        //pausePanel.SetActive(false);
    }

    //public void Controls()
    //{
    //    //controlsPanel.SetActive(true);
    //    inConOrCred = true;
    //    //settingPanel.SetActive(false);
    //    pausePanel.SetActive(false);
    //}
}