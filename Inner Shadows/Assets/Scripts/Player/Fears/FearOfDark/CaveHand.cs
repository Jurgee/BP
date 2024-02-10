using UnityEngine;

public class CaveHand : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 0.5f; // Speed at which the hand moves towards the player

    private PlayerMovement direction;
    private CaveEntry entry;
    private Flashlight battery;
    public SpriteRenderer spriteRenderer;

    private Vector3 leftOffset; // Store the initial offset from the player to maintain distance
    private Vector3 rightOffset;

    private bool wasMovingRight = false; // Track previous right movement direction
    private bool wasMovingLeft = false;  // Track previous left movement direction

    void Start()
    {
        direction = GameObject.FindObjectOfType<PlayerMovement>();
        entry = GameObject.FindObjectOfType<CaveEntry>();
        battery = GameObject.FindObjectOfType<Flashlight>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        leftOffset = transform.position - player.position;
        rightOffset = player.position - transform.position;

        // Set the initial position of the cube
        transform.position = player.position + leftOffset;
    }

    void Update()
    {
        if (!entry.InCave)
        {
            spriteRenderer.enabled = false;
            return;
        }

        if (!battery.flashlight.enabled)
        {
            return;
        }
        spriteRenderer.enabled = true;
        
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

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // Move towards the player
    }

    void DecreaseBatteryFillAmount()
    {
        if (battery != null)
        {
            battery.battery.fillAmount -= 0.2f; // Take some battery after hand hit

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
