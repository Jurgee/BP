/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for cave entry
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CaveEntry : MonoBehaviour
{
    [SerializeField] private Light2D playerLight; 
    private float transitionDuration = 0.5f; // Duration of the intensity transition
    private CaveEntry[] entries;
    public bool InCave = false;

    private void Start()
    {
        entries = GameObject.FindObjectsOfType<CaveEntry>(); // Find all objects
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeLightIntensity(0f, transitionDuration));
            foreach (var enter in entries)
            {
                enter.InCave = true;
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
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerLight.intensity = targetIntensity; // Ensure the target intensity is reached
        playerLight.enabled = false;

    }
}
