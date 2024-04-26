using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject menu;
    public FearOfDeath death;
    void Start()
    {
        menu.SetActive(false);
    }

    
    void Update()
    {
        if (death.maxDeaths == death.deadCounter)
        {
            menu.SetActive(true);
            
            Time.timeScale = 0f;
        }
    }
}
