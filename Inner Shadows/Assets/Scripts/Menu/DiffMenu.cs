using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffMenu : MonoBehaviour
{
    public GameObject diffMenu;
    public FearOfDeath death;
    void Start()
    {
        diffMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Easy()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");

        death.maxDeaths = 20;
        diffMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Normal()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");

        death.maxDeaths = 10;
        diffMenu.SetActive(false);
        Time.timeScale = 1.0f;

    }

    public void Hard()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");

        death.maxDeaths = 2;
        diffMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
