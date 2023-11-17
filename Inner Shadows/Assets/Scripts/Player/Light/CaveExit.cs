using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CaveExit : MonoBehaviour
{
    [SerializeField] private Light2D playerLight; // Assign the Light2D component of the player in the Unity Editor
    private float transitionDuration = 0.5f; // Duration of the intensity transition

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeLightIntensity(0.5f, transitionDuration));
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
    }
}
