/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for player health and fear of pointed objects
 */

using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private AudioSource player_death_sound;
    [SerializeField] private float starting_health;
    [SerializeField] private Light2D playerLight;
    [SerializeField] public Image spikyMeter;
    
    private CaveEntry[] entries;
    private FearOfHeights heights;
    private FearOfDark dark;
    private FearOfUnknown lost;
    private FearOfDeath death;

    private Vector3 respawnPosition;
    private float timeSinceLastDamage = 0f;
    private float resetTime = 20f; //20s
    public float current_health { get; private set; }
    public bool dead;

    
    private void Awake()
    {
        dead = false;
        current_health = starting_health;
        heights = GetComponent<FearOfHeights>();
        dark = GetComponent<FearOfDark>();
        lost = GetComponent<FearOfUnknown>();
        death = GetComponent<FearOfDeath>();

        spikyMeter.fillAmount = 0f;
        entries = GameObject.FindObjectsOfType<CaveEntry>(); // Find all objects
    }
    void Update()
    {
        if (!dead) // Check if the player is alive
        {
            // Increment the time since the last damage
            timeSinceLastDamage += Time.deltaTime;

            // If no damage has been taken for 20 seconds, reset spikyMeter to 0
            if (timeSinceLastDamage >= resetTime)
            {
                spikyMeter.fillAmount = 0f;
                timeSinceLastDamage = 0f; // Reset the timer
            }
        }
    }

    // Take damage to player
    public void TakeDamage(float damage)
    {
        current_health -= damage;
        current_health = Mathf.Clamp(current_health, 0, starting_health);

        if (current_health <= 0)
        {
            if (player_death_sound != null)
                player_death_sound.Play();
            dead = true;
            Respawn();
        }

        // Reset the time since last damage when the player takes damage
        timeSinceLastDamage = 0f;
    }

    // Function for pointed objects
    public void TakeSpikyDamage(float damage)
    {
        spikyMeter.fillAmount += 0.2f;

        if (spikyMeter.fillAmount <= 0.4f)
        {
            TakeDamage(damage);
        }
        else if (spikyMeter.fillAmount <= 0.8f)
        {
            TakeDamage(damage * 2);
        }
        else if (spikyMeter.fillAmount <= 1f)
        {
            TakeDamage(damage * 3);
        }
        ColorChanger();
    }
    // Add health
    public void AddHealth(float _value)
    {
        current_health = Mathf.Clamp(current_health + _value, 0, starting_health);
    }

    public void Respawn()
    {
        ResetFears();
        ResetCave();
        transform.position = respawnPosition;
        dead = false;
        playerLight.intensity = 0.53f;
        death.deadCounter += 1;
    }

    // Method to set the respawn position
    public void SetRespawnPosition(Vector3 position)
    {
        respawnPosition = position;
    }
    // Reset all fears to 0
    private void ResetFears()
    {
        
        current_health = starting_health;
        lost.fearLevel = 0f;
        dark.darkMeter.fillAmount = 0f;
        heights.fearLevel = 0f;
        spikyMeter.fillAmount = 0f;
    }

    // Reset cave bool values if player dies
    private void ResetCave()
    {
        foreach (var entry in entries)
        {
            if (entry != null) // Ensure entry is not null
            {
                entry.InCave = false;
            }
        }
    }

    private void ColorChanger()
    {
        Color fearColor = Color.Lerp(Color.magenta, Color.red, spikyMeter.fillAmount);
        spikyMeter.color = fearColor;
    }

    
}
