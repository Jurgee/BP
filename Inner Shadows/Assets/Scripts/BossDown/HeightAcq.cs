/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for height acquired menu
 */
using UnityEngine;
using System.Collections;

public class HeightAcq : MonoBehaviour
{
    public Enemy enemy; // Reference to the enemy
    public GameObject menu; // The menu to be displayed
    private bool menuShown; // Flag to prevent showing the menu multiple times
    public float delayBeforeShowingMenu = 3f; // Delay before the menu is shown

    void Start()
    {
        menu.SetActive(false); // Ensure the menu is initially hidden
        menuShown = false; // Initialize the menuShown flag
    }

    void Update()
    {

        if (!menuShown && enemy.textH)
        {
            StartCoroutine(ShowMenuWithDelay()); // Start coroutine to delay the menu
            menuShown = true; // Set flag to prevent repeated coroutine starts
        }
    }

    private IEnumerator ShowMenuWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeShowingMenu); // Wait 3 seconds
        menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MenuOff()
    {
        menu.SetActive(false);
        enemy.textH = false;
        Time.timeScale = 1f;
        menuShown = false;
    }
}



