/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the pause menu
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    public GameObject pauseMenu;
    
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

    // Resume the game
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        FindObjectOfType<AudioManager>().Play("buttonClick");

    }

    // Pause the game
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //freeze game
        gameIsPaused = true;
    }

    // Back to menu
    public void Menu()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
        gameIsPaused = false;
        

    }
    // Quit the game
    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        Application.Quit();

    }
}
