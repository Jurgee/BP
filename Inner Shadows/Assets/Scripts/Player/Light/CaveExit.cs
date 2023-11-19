using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class CaveExit : MonoBehaviour
{
    [SerializeField] private Light2D playerLight;
    [SerializeField] private Light2D flashlight;
    private float transitionDuration = 0.5f;
    private CaveEntry entry;

    private void Start()
    {
        entry = GameObject.FindObjectOfType<CaveEntry>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(WaitForFlashlightOffAndFade());
        }
    }

    private IEnumerator WaitForFlashlightOffAndFade()
    {
        // If the player is in the cave but the flashlight is still on, wait until it's off
        if (entry.InCave && flashlight.enabled)
        {
            while (flashlight.enabled)
            {
                yield return null;
            }
        }

        // Set InCave to false when leaving the cave
        entry.InCave = false;

        // If the flashlight is off, start fading the light intensity
        StartCoroutine(FadeLightIntensity(0.5f, transitionDuration));
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
    }
}
