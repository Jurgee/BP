/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the game over image
 */
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
        if (death.maxDeaths == death.deadCounter) // Limit is reached
        {
            menu.SetActive(true);
            
            Time.timeScale = 0f;
        }
    }
}
