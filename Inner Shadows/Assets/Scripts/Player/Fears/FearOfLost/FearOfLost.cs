/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for fear of lost phobia
 */

using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class FearOfLost : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] public Image fearMeter;

    private float maxFear = 500.0f;
    private float fearPerXUnit = 1.0f; 
    private float fearPerYUnit = 1.0f; 
    private float smoothSpeed = 15.0f;
    public float fearLevel;
    private Vector3 guidepostPosition;
    private bool isCounting;

    private void Start()
    {
        fearMeter.fillAmount = 0;
        fearLevel = 0;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Guidepost"))
        {
            // If we are not already counting (i.e., first guidepost hit), reset the fear and start counting.
            if (!isCounting)
            {
                fearLevel = 0;
                fearMeter.fillAmount = 0;
                isCounting = true;
            }

            // Remember the position (x, y) of the guidepost when the player hits it.
            guidepostPosition = other.transform.position;
            GetComponent<Health>().SetRespawnPosition(guidepostPosition); // set respawn point thanks to the guidepost
        }
    }

    private void Update()
    {
        // Check if we are allowed to start counting.
        if (isCounting)
        {
            if(playerMovement.is_on_platform || playerMovement.edge_1 || playerMovement.grounded || playerMovement.edge_2 || playerMovement.edge_3)
            {
                float X_difference = Mathf.Abs(player.position.x - guidepostPosition.x);
                float Y_difference = Mathf.Abs((player.position.y - guidepostPosition.y) * 3);

                // Calculate the fear increase based on the difference in X and Y.
                float fearIncrease = X_difference * fearPerXUnit + Y_difference * fearPerYUnit;

                if (fearIncrease <= 34)
                {
                    // If the player is close enough, allow the fear to decrease.
                    fearLevel -= 1.0f;
                    fearLevel = Mathf.Clamp(fearLevel, 0, maxFear);
                }
                else
                {
                    // If the player is not close enough, ensure the fear only increases or stays the same.
                    fearLevel = Mathf.Max(fearLevel, fearIncrease);
                }

                // Calculate the target fill amount and smoothly update it.
                float targetFillAmount = fearLevel / maxFear;
                fearMeter.fillAmount = Mathf.Lerp(fearMeter.fillAmount, targetFillAmount, smoothSpeed * Time.deltaTime);
                ColorChanger();

                if (fearMeter.fillAmount >= 1.0f)
                {
                    GetComponent<Health>().TakeDamage(100);
                    fearLevel = 0;
                }
            }
        }
    }
    private void ColorChanger()
    {
        Color fearColor = Color.Lerp(Color.green, Color.red, fearMeter.fillAmount);
        fearMeter.color = fearColor;
    }
}
