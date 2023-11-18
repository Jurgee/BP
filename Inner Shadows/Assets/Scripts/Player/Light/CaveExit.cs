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
        // Assuming CaveEntry is attached to the same GameObject as CaveExit
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
        // Wait until the flashlight is turned off
        while (flashlight.enabled)
        {
            yield return null;
        }

        // Once the flashlight is off, start fading the light intensity
        StartCoroutine(FadeLightIntensity(0.5f, transitionDuration));
        entry.InCave = false;

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
