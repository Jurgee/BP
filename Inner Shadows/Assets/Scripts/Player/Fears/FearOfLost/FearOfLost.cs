using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FearOfLost : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerMovement player_movement;
    [SerializeField] private Image fear_meter;

    private float maxFear = 500.0f;
    private float fearPerXUnit = 1.0f; // Adjust this value as needed.
    private float fearPerYUnit = 1.0f; // Adjust this value as needed.
    private float smoothSpeed = 7.0f;
    private float fear_level;
    private Vector2 guidepostPosition;
    private bool isCounting;

    private void Start()
    {
        fear_meter.fillAmount = 0;
        fear_level = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Guidepost"))
        {
            // If we are not already counting (i.e., first guidepost hit), reset the fear and start counting.
            if (!isCounting)
            {
                fear_level = 0;
                fear_meter.fillAmount = 0;
                isCounting = true;
            }

            // Remember the position (x, y) of the guidepost when the player hits it.
            guidepostPosition = other.transform.position;
        }
    }

    private void Update()
    {
        
        // Check if we are allowed to start counting.
        if (isCounting)
        {
            if(player_movement.is_on_platform || player_movement.edge_1 || player_movement.grounded || player_movement.edge_2 || player_movement.edge_3)
            {
                float X_difference = Mathf.Abs(player.position.x - guidepostPosition.x);
                float Y_difference = Mathf.Abs((player.position.y - guidepostPosition.y) * 3);

                // Calculate the fear increase based on the difference in X and Y.
                float fearIncrease = X_difference * fearPerXUnit + Y_difference * fearPerYUnit;

                if (fearIncrease <= 34)
                {
                    // If the player is close enough, allow the fear to decrease.
                    fear_level -= 1.0f;
                    fear_level = Mathf.Clamp(fear_level, 0, maxFear);
                }
                else
                {
                    // If the player is not close enough, ensure the fear only increases or stays the same.
                    fear_level = Mathf.Max(fear_level, fearIncrease);
                }

                // Calculate the target fill amount and smoothly update it.
                float targetFillAmount = fear_level / maxFear;
                fear_meter.fillAmount = Mathf.Lerp(fear_meter.fillAmount, targetFillAmount, smoothSpeed * Time.deltaTime);
                ColorChanger();
            }
        }
    }

    void ColorChanger()
    {
        Color fear_color = Color.Lerp(Color.yellow, Color.red, fear_meter.fillAmount);
        fear_meter.color = fear_color;

    }
}
