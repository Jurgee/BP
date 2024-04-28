/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the key text
 */
using UnityEngine;
using TMPro;

public class KeysText : MonoBehaviour
{
    public TextMeshProUGUI keysText; // Reference to the TextMeshProUGUI component for the key count display
    public Gate gate; // Reference to the Gate object that tracks key pickups

    void Start()
    {
        // Hide the text initially
        if (keysText != null)
        {
            keysText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other) // When an object enters the trigger area
    {
        if (other.CompareTag("Player")) // Ensure it's the player triggering
        {
            if (keysText != null && gate != null) // Check if references are valid
            {
                // Display the keys found as a fraction (gate.keyPicked/5)
                keysText.text = $"Keys found: {gate.keyPicked}/5";
                keysText.gameObject.SetActive(true); // Show the text
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) // When an object leaves the trigger area
    {
        if (other.CompareTag("Player")) // Ensure it's the player triggering
        {
            if (keysText != null)
            {
                keysText.gameObject.SetActive(false); // Hide the text
            }
        }
    }
}