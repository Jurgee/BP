using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using System;

public class FearOfHeights : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerMovement player_movement;
    [SerializeField] private Light2D player_light;
    [SerializeField] private AudioSource heart_beat_audio;

    public Image fear_meter;
    private float groundY;
    private float previousY;

    public float fearLevel = 0;
    private float decreaseRate = 0.4f;
    private float smoothSpeed = 7.0f;
    private float onEdgeIncreaseRate = 0.0f;
    private float timeBetweenIncreases = 1.0f;
    private float timeSinceLastIncrease = 0.0f;

    private void Start()
    {
        fear_meter.fillAmount = 0;
        groundY = Mathf.Abs(player.position.y); // Store the absolute value of Y
        previousY = groundY;
    }

    private void Update()
    {
        float currentY = Mathf.Abs(player.position.y); // Get the absolute value of current Y

        if (player_movement.edge_1 || player_movement.edge_2 || player_movement.edge_3)
        {
            FearBarFiller();
            ColorChanger();

            if (CheckEdge() == 1) //edge 1
            {
                onEdgeIncreaseRate = 0.3f;
            }
            else if (CheckEdge() == 2) //edge 2
            {
                onEdgeIncreaseRate = 0.6f;
            }
            else if (CheckEdge() == 3) //edge 3
            {
                onEdgeIncreaseRate = 1.0f;
            }

            if (timeSinceLastIncrease >= timeBetweenIncreases)
            {
                fearLevel += onEdgeIncreaseRate;
                timeSinceLastIncrease = 0.0f;
            }

            // Increment the time since the last increase.
            timeSinceLastIncrease += (Time.deltaTime * smoothSpeed);
        }

        else if (player_movement.is_on_platform)
        {
            // Check if the player is moving upward
            if (currentY != previousY)
            {
                float deltaHeight = currentY - previousY;
                fearLevel += deltaHeight * 0.4f; // Adjust multiplier as needed
            }

            previousY = currentY;

            FearBarFiller();
            ColorChanger();
        }
        else if (player_movement.grounded)
        {
            groundY = Mathf.Abs(player.position.y);
            fearLevel = Mathf.Max(0, fearLevel - decreaseRate * (Time.deltaTime * smoothSpeed));
            FearBarFiller();
            ColorChanger();
        }
    }

    void FearBarFiller()
    {
        float targetFillAmount = Mathf.Abs(fearLevel / 30);
        fear_meter.fillAmount = Mathf.Lerp(fear_meter.fillAmount, targetFillAmount, smoothSpeed * Time.deltaTime);
        if (fear_meter.fillAmount >= 1.0f)
        {
            GetComponent<Health>().TakeDamage(100);
            fearLevel = 0;
        }
    }

    void ColorChanger()
    {
        Color fear_color = Color.Lerp(Color.blue, Color.red, fear_meter.fillAmount);
        fear_meter.color = fear_color;
    }

    int CheckEdge()
    {
        if (player_movement.edge_1)
        {
            return 1;
        }
        else if (player_movement.edge_2)
        {
            return 2;
        }
        else if (player_movement.edge_3)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }
}
