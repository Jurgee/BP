using System.Collections;
using System.Collections.Generic;
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
    float fear_level = 100;

    
    private float onEdgeIncreaseRate;
    private float timeBetweenIncreases = 1.0f;
    private float timeSinceLastIncrease = 0.0f;
    private float decreaseRate = 0.4f;
    private float smoothSpeed = 7.0f;


    private void Start()
    {
        fear_level = 0;
        fear_meter.fillAmount = 0;
        
    }

    private void Update()
    {
        
        if (player_movement.edge_1 || player_movement.edge_2 || player_movement.edge_3)
        {
            FearBarFiller();
            ColorChanger();

        }
        else if(player_movement.is_on_platform)
        {
            // Decrease fear level when on platform.
            fear_level = Mathf.Max(0, fear_level - decreaseRate * (Time.deltaTime * smoothSpeed));
            FearBarFiller();
            ColorChanger();
        }
        else if (player_movement.grounded)
        {
            // Decrease fear level when grounded.
            fear_level = Mathf.Max(0, fear_level - decreaseRate * (Time.deltaTime * smoothSpeed));
            FearBarFiller();
            ColorChanger();
        }
        else
        {
            // If not on edge or grounded, reset the increase timer.
            timeSinceLastIncrease = 0.0f;
        }

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
        else { }


        // If on edge and enough time has passed, increase fear level.
        if ((player_movement.edge_1 || player_movement.edge_2 || player_movement.edge_3) && timeSinceLastIncrease >= timeBetweenIncreases)
        {
            fear_level += onEdgeIncreaseRate;
            timeSinceLastIncrease = 0.0f;
        }

        // Increment the time since the last increase.
        timeSinceLastIncrease += (Time.deltaTime * smoothSpeed);
    }

    void FearBarFiller()
    {
        
        // Calculate the target fillAmount based on the fear level and player position.
        float targetFillAmount = Math.Abs((fear_level + player.position.y) / 30);

        // Use Mathf.Lerp to smoothly transition the fillAmount.
        fear_meter.fillAmount = Mathf.Lerp(fear_meter.fillAmount, targetFillAmount, smoothSpeed * Time.deltaTime);


        //----------------------LIGHT----------------------------------------------------------
        // Check if fillAmount is greater than 0.5f and adjust light intensity accordingly.
        if (fear_meter.fillAmount > 0.4f)
        {
            // Calculate the light intensity based on fillAmount, starting from 0.5f.
            float lightIntensity = Mathf.Lerp(1.0f, 0.0f, (fear_meter.fillAmount - 0.5f) * 2.0f);
            player_light.intensity = lightIntensity;
        }
        else
        {
            // If fillAmount is less than or equal to 0.5f, keep full light intensity.
            player_light.intensity = 1.0f;
        }
        //----------------------LIGHT----------------------------------------------------------
    }

    void ColorChanger()
    {
        Color fear_color = Color.Lerp(Color.blue, Color.red, fear_meter.fillAmount);
        fear_meter.color = fear_color;
        
    }

    int CheckEdge()
    {
        if(player_movement.edge_1 == true)
        {
            return 1;
        }
        else if(player_movement.edge_2 == true) 
        {
            return 2;
        }
        else if (player_movement.edge_3 == true)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }
}
