/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for cave hand funcs
 */
using UnityEngine;

public class CaveHand : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 0.5f; // Speed at which the hand moves towards the player

    private PlayerMovement direction;
    private CaveEntry[] entries;
    [SerializeField] Flashlight battery;
    public SpriteRenderer spriteRenderer;

    private Vector3 leftOffset; // Store the initial offset from the player to maintain distance
    private Vector3 rightOffset;

    private bool wasMovingRight = false; // Track previous right movement direction
    private bool wasMovingLeft = false;  // Track previous left movement direction

    void Start()
    {
        direction = GameObject.FindObjectOfType<PlayerMovement>();
        entries = GameObject.FindObjectsOfType<CaveEntry>(); //multiple
        spriteRenderer = GetComponent<SpriteRenderer>();

        leftOffset = transform.position - player.position;
        rightOffset = player.position - transform.position;

        // Set the initial position of the hand
        transform.position = player.position + leftOffset;
    }

    void Update()
    {
        bool inCaveWithFlashlight = false;

        foreach (var entry in entries)
        {
            if (entry.InCave && battery.flashlight.enabled)
            {
                inCaveWithFlashlight = true;
                break; // In cave and flashlight is on
            }
        }

        spriteRenderer.enabled = inCaveWithFlashlight;

        if (inCaveWithFlashlight)
        {

            if (direction.Dright) // Moving right
            {
                if (wasMovingLeft) // If there was a change from left to right movement
                {
                    transform.position = player.position + leftOffset; // Reset position to the left offset
                    wasMovingLeft = false; // Reset the flag
                }
                else
                {
                    MoveTowardsPlayer();
                }

                wasMovingRight = true; // Set the flag for right movement
            }

            if (direction.Aleft) // Moving left
            {
                if (wasMovingRight) // If there was a change from right to left movement
                {
                    transform.position = player.position + rightOffset; // Reset position to the right offset
                    wasMovingRight = false; // Reset the flag
                }
                else
                {
                    MoveTowardsPlayer();
                }

                wasMovingLeft = true; // Set the flag for left movement
            }

            // Check if the hand is at the player's position and decrease battery fill amount
            if (transform.position == player.position)
            {
                DecreaseBatteryFillAmount();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // Move towards the player
    }

    void DecreaseBatteryFillAmount()
    {
        if (battery != null)
        {
            battery.battery.fillAmount -= 0.3f; // Take some battery after hand hit

            // Reset cube position
            if (direction.Dright)
            {
                transform.position = player.position + leftOffset; // Reset position to the left offset
            }
            else if (direction.Aleft)
            {
                transform.position = player.position + rightOffset; // Reset position to the right offset
            }
        }
    }

}
