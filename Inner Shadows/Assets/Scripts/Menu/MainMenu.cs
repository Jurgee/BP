using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        SceneManager.LoadScene("Story");
    }

    public void No()
    {
        yesNo.SetActive(false);
    }
}
