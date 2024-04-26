using UnityEngine;

public class Gate : MonoBehaviour
{
    public int keyPicked; // Number of keys collected by the player
    public Sprite unlockedSprite; // The sprite to change to when gate is unlocked
    public BoxCollider2D boxCollider; // Reference to the BoxCollider
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    void Start()
    {
        // Ensure initial components are assigned
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        CheckKeyCount(); // Check the number of keys in each update frame
    }

    private void CheckKeyCount()
    {
        if (keyPicked >= 5) // If the player has 5 or more keys
        {
            if (spriteRenderer != null && unlockedSprite != null)
            {
                spriteRenderer.sprite = unlockedSprite; // Change the sprite
            }

            if (boxCollider != null)
            {
                boxCollider.enabled = false; // Disable the BoxCollider
            }
        }
    }
}

