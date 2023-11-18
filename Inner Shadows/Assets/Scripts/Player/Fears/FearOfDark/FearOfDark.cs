using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class FearOfDark : MonoBehaviour
{

    
    [SerializeField] private Image dark_meter;
    public Light2D flashlight;
    public float fillRate = 1.0f; // Adjust this value to control how quickly the meter fills

    private bool isFillingDarkMeter = false;

    private void Start()
    {
        dark_meter.fillAmount = 0;
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
        dark_meter.fillAmount = 0; // Reset the dark meter
        // Add any additional logic to stop filling the dark meter if needed
    }

    private IEnumerator FillDarkMeter()
    {
        while (isFillingDarkMeter)
        {
            // Add to fill amount only when the flashlight is off
            if (flashlight != null && !flashlight.enabled)
            {
                dark_meter.fillAmount += fillRate * (Time.deltaTime / 20);
            }

            // Add any additional conditions to break out of the loop if needed
            yield return null;
        }
    }
}
