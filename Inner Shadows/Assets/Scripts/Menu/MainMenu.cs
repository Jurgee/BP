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
        SceneManager.LoadSceneAsync("Story"); 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
