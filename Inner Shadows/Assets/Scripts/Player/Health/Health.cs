/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for player health and fear of pointed objects
 */

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // fear of pointed objects
    [SerializeField] public Image spikyMeter;
    public bool isFearedOfSpikes;

    [SerializeField] public float starting_health;
    [SerializeField] private Light2D playerLight;
    
    private CaveEntry[] entries;
    [Header("Fears")]
    private FearOfHeights heights;
    private FearOfDark dark;
    private FearOfUnknown lost;
    private FearOfDeath death;
    private FearOfWater water;

    [Header("Others")]
    private Vector3 respawnPosition;
    private float timeSinceLastDamage = 0f;
    private float resetTime = 20f; //20s
    public float current_health;
    public bool dead;

    [Header("iFrames")] 
    [SerializeField] private float iFramesDur;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        dead = false;
        current_health = starting_health;
        heights = GetComponent<FearOfHeights>();
        dark = GetComponent<FearOfDark>();
        lost = GetComponent<FearOfUnknown>();
        death = GetComponent<FearOfDeath>();
        water = GetComponent<FearOfWater>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spikyMeter.fillAmount = 0f;
        entries = GameObject.FindObjectsOfType<CaveEntry>(); // Find all objects
        isFearedOfSpikes = true;
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
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            dead = true;
            Respawn();
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("PlayerHit");
            StartCoroutine(Invunerability());
        }
        // Reset the time since last damage when the player takes damage
        timeSinceLastDamage = 0f;
    }

    // Function for pointed objects
    public void TakeSpikyDamage(float damage)
    {
        if (isFearedOfSpikes)
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
        water.waterMeter.fillAmount = 0f;
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

    private IEnumerator Invunerability()
    {
         Physics2D.IgnoreLayerCollision(6,7, true);
         for (int i = 0; i < numOfFlashes; i++)
         {
             spriteRenderer.color = new Color(0.5943396f, 0.3336152f, 0.3336152f, 0.5f);
             yield return new WaitForSeconds(iFramesDur / (numOfFlashes * 2));
             spriteRenderer.color = new Color(0.6320754f, 0.6320754f, 0.6320754f, 1f);
             yield return new WaitForSeconds(iFramesDur / (numOfFlashes * 2));

        }
         Physics2D.IgnoreLayerCollision(6, 7, false);
    }
    
}
