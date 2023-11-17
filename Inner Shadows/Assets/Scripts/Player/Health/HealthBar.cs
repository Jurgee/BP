using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health player_health;
    [SerializeField] private Image current_healthbar;

    [SerializeField] private Sprite fullHealthHead;
    [SerializeField] private Sprite mediumHealthHead;
    [SerializeField] private Sprite lowHealthHead;

    [SerializeField] private Image playerHeadImage; 

    private void Start()
    {
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = player_health.current_health / 5f;

        // Change player head image based on health percentage
        if (healthPercentage >= 0.7f)
        {
            SetPlayerHeadImage(fullHealthHead);
        }
        else if (healthPercentage >= 0.3f)
        {
            SetPlayerHeadImage(mediumHealthHead);
        }
        else
        {
            SetPlayerHeadImage(lowHealthHead);
        }

        
        current_healthbar.fillAmount = healthPercentage;
    }

    private void SetPlayerHeadImage(Sprite newHeadSprite)
    {
        // Set the player's head image to the provided sprite
        if (playerHeadImage != null)
        {
            playerHeadImage.sprite = newHeadSprite;
        }
    }
}
