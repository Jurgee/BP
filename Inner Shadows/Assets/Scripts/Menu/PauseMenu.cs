using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    public GameObject pauseMenu;
    // Update is called once per frame
    void Start()
    {
        pauseMenu.SetActive(false);
        gameIsPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        FindObjectOfType<AudioManager>().Play("buttonClick");

    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //freeze game
        gameIsPaused = true;
    }

    public void Menu()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
        gameIsPaused = false;
        

    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        Application.Quit();

    }
}
