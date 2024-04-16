using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MinimapControl minimapControl;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Game"); 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
