using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class CaveExit : MonoBehaviour
{
    [SerializeField] private Light2D playerLight;
    private float transitionDuration = 1f;
    private CaveEntry entry;

    private void Start()
    {
        entry = GameObject.FindObjectOfType<CaveEntry>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeLightIntensity(0.5f, transitionDuration));
            entry.InCave = false;
        }
    }

    private IEnumerator FadeLightIntensity(float targetIntensity, float duration)
    {
        float startIntensity = playerLight.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / duration);
            elapsedTime += Time.fixedDeltaTime; // Use Time.fixedDeltaTime for a smoother transition
            yield return null;
        }

        playerLight.intensity = targetIntensity; // Ensure the target intensity is reached
        playerLight.enabled = true;
    }
}
