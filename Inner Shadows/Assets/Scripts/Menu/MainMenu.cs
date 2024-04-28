/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the main menu
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject yesNo;

    void Start()
    {
        yesNo.SetActive(false);
    }
    public void PlayGame()
    {
        yesNo.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Yes()
    {
        SceneManager.LoadScene("Story"); // Load story scene
    }

    public void No()
    {
        yesNo.SetActive(false); // Go back to main menu
    }
}
