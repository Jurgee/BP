using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthPotions : MonoBehaviour
{
    public GameObject system;
    public int numberOfPotions;
    public TextMeshProUGUI textNumber;

    public bool canUseHealthPotions;
    [SerializeField] private Health health;
    void Start()
    {
        canUseHealthPotions = false; // Set initial state
        UpdatePotionCountDisplay(); // Display initial potion count
    }

    void Update()
    {
        if (canUseHealthPotions) // Check if potions can be used 
        {
            system.SetActive(true);

            if (numberOfPotions > 0)
            {
                if (Input.GetKeyDown(KeyCode.E)) // Key press to use a potion
                {
                    UseHealthPotion();
                }
            }
        }
        else
        {
            system.SetActive(false);
        }
    }

    void UseHealthPotion()
    {
        if (numberOfPotions > 0  && (health.current_health != health.starting_health)) // Ensure there's at least one potion to use
        {
            numberOfPotions--; // Decrease potion count
            UpdatePotionCountDisplay(); // Update UI display
            health.AddHealth(3f);
            FindObjectOfType<AudioManager>().Play("potion");

        }
    }

    void UpdatePotionCountDisplay()
    {
        if (textNumber != null) // Update text to show the number of potions
        {
            textNumber.text = $"{numberOfPotions}";
        }
    }

    public void AddHeatlhPotion()
    {
        numberOfPotions++;
        UpdatePotionCountDisplay();
    }
}
