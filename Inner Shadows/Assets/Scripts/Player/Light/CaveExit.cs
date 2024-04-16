/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for cave exit, turn on players light
 */
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CaveExit : MonoBehaviour
{
    [SerializeField] private Light2D playerLight; 
    private float transitionDuration = 1f;
    private CaveEntry[] entries;
    private Health playerHealth;

    private void Start()
    {
        entries = GameObject.FindObjectsOfType<CaveEntry>(); // Find all objects

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeLightIntensity(0.5f, transitionDuration));
            // Set all objects to false
            foreach (var entry in entries)
            {
                entry.InCave = false;
                FindObjectOfType<AudioManager>().Stop("CaveTheme");
            }
        }
    }

    private IEnumerator FadeLightIntensity(float targetIntensity, float duration)
    {
        float startIntensity = playerLight.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / duration);
            elapsedTime += Time.fixedDeltaTime; 
            yield return null;
        }

        playerLight.intensity = targetIntensity; // Ensure the target intensity is reached
        playerLight.enabled = true;
    }
}
