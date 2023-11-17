using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public Light2D flashlight; 
    [SerializeField] private Image battery;
    [SerializeField] private Light2D player_flash_spot;
    [SerializeField] private Light2D player_spotlight;

    private float batteryDrainRate = 0.4f; // Adjust this value to control how quickly the battery drains
    private float batteryRechargeRate = 0.2f; // Adjust this value to control how quickly the battery recharges
   
    private bool isRecharging = false;
    private bool canToggleFlashlight = true;
    

    private void Awake()
    {
        player_flash_spot.enabled = false;
        flashlight.enabled = false;
        SetBatteryVisibility(false);
    }

    void Update()
    {
        // Check if the "F" key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canToggleFlashlight)
            {
                ToggleFlashlight();
            }
        }

        // Update battery level
        UpdateBatteryLevel();
    }

    void ToggleFlashlight()
    {
        // Check if the flashlight is currently on
        if (flashlight.enabled)
        {
            // If on, turn it off
            flashlight.enabled = false;
            player_flash_spot.enabled = false;
            player_spotlight.enabled = true;
            isRecharging = true; // Start recharging when turning off the flashlight
        }
        else
        {
            // If off, turn it on
            flashlight.enabled = true;
            player_flash_spot.enabled = true;
            player_spotlight.enabled = false;
            SetBatteryVisibility(true);
            isRecharging = false; // Stop recharging when turning on the flashlight
        }
    }

    void UpdateBatteryLevel()
    {
        // Adjust battery level based on flashlight state
        if (flashlight.enabled)
        {
            // Drain battery
            battery.fillAmount -= batteryDrainRate * (Time.deltaTime / 10);

            // Decrease flashlight intensity as the battery level goes below 0.5
            flashlight.intensity = Mathf.Lerp(1f, 0f, 1f - (battery.fillAmount / 0.5f));
            player_flash_spot.intensity = Mathf.Lerp(1f, 0f, 1f - (battery.fillAmount / 0.5f));

            // If the battery is empty, turn off the flashlight and start recharging
            if (battery.fillAmount <= 0f)
            {
                flashlight.enabled = false;
                player_flash_spot.enabled = true;
                

                isRecharging = true; // Start recharging when the battery is empty
                canToggleFlashlight = false; // Disable flashlight toggle until battery is full
            }
           

        }
        else if (isRecharging)
        {
            // Recharge battery
            battery.fillAmount += batteryRechargeRate * (Time.deltaTime / 10);

            // Ensure battery level stays within the range [0, 1]
            battery.fillAmount = Mathf.Clamp01(battery.fillAmount);

            // Hide the battery meter when the battery is full
            if (battery.fillAmount >= 1f)
            {
                SetBatteryVisibility(false);
                isRecharging = false;
                canToggleFlashlight = true; // Enable flashlight toggle when battery is full
            }
        }
    }

    void SetBatteryVisibility(bool visible)
    {
        battery.gameObject.SetActive(visible);
    }
}
