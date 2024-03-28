using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private AudioSource player_death_sound;
    [SerializeField] private float starting_health;
    [SerializeField] private Light2D playerLight;

    private CaveEntry[] entries;

    private FearOfHeights heights;
    private FearOfDark dark;
    private FearOfLost lost;

    public float current_health { get; private set; }
    public bool dead;

    private Vector3 respawnPosition; 

    private void Awake()
    {
        dead = false;
        current_health = starting_health;
        heights = GetComponent<FearOfHeights>();
        dark = GetComponent<FearOfDark>();
        lost = GetComponent<FearOfLost>();

        entries = GameObject.FindObjectsOfType<CaveEntry>(); // Find all objects
    }

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
    }

    public void AddHealth(float _value)
    {
        current_health = Mathf.Clamp(current_health + _value, 0, starting_health);
    }

    public void Respawn()
    {
        transform.position = respawnPosition;
        ResetFears();
        ResetCave();
        dead = false;
        playerLight.intensity = 0.53f;

    }

    // Method to set the respawn position
    public void SetRespawnPosition(Vector3 position)
    {
        respawnPosition = position;
    }

    private void ResetFears()
    {
        current_health = starting_health;
        lost.fearLevel = 0;
        dark.darkMeter.fillAmount = 0f;
        heights.fearLevel = 0;

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
    
}
