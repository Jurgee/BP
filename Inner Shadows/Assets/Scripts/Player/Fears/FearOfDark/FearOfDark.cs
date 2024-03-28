/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for fear of dark phobia
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class FearOfDark : MonoBehaviour
{
    [SerializeField] public Image darkMeter;
    public Light2D flashlight;
    private float fillRate = 2.0f; // Adjust this value to control how quickly the meter fills

    private bool isFillingDarkMeter = false;

    private void Start()
    {
        darkMeter.fillAmount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cave entry"))
        {
            // Start adding to fillAmount when entering a cave with the flashlight off
            StartFillingDarkMeter();
        }
        else if(collision.CompareTag("Cave exit"))
        {
            StopFillingDarkMeter();
        }
    }

    private void StartFillingDarkMeter()
    {
        isFillingDarkMeter = true;
        StartCoroutine(FillDarkMeter());
    }

    private void StopFillingDarkMeter()
    {
        isFillingDarkMeter = false;
        darkMeter.fillAmount = 0; // Reset the dark meter
        
    }

    private IEnumerator FillDarkMeter()
    {
        while (isFillingDarkMeter)
        {
            // Add to fill amount only when the flashlight is off
            if (flashlight != null && !flashlight.enabled)
            {
                darkMeter.fillAmount += fillRate * (Time.deltaTime / 10);
                ColorChanger();
            }

            if (darkMeter.fillAmount >= 1.0f)
            {
                GetComponent<Health>().TakeDamage(100);
                darkMeter.fillAmount = 0f;
                isFillingDarkMeter = false;
            }
            // Add any additional conditions to break out of the loop if needed
            yield return null;
        }
    }

    private void ColorChanger()
    {
        Color fearColor = Color.Lerp(Color.gray, Color.red, darkMeter.fillAmount);
        darkMeter.color = fearColor;
    }
}
