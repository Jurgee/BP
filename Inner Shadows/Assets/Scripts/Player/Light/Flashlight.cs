using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public Light2D flashlight;
    [SerializeField] private Image battery;
    [SerializeField] private Light2D player_flash_spot;
    [SerializeField] private Light2D player_spotlight;

    private float batteryDrainRate = 0.4f;
    private float batteryRechargeRate = 0.2f;

    private bool isRecharging = false;
    private bool canToggleFlashlight = true;

    private CaveEntry caveEntry; // Assuming you have a CaveEntry script

    private void Awake()
    {
        canToggleFlashlight = true;
        player_flash_spot.enabled = false;
        flashlight.enabled = false;
        SetBatteryVisibility(false);

        // Get the CaveEntry script attached to the same GameObject
        caveEntry = GameObject.FindObjectOfType<CaveEntry>();
    }

    void Update()
    {
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
        bool isInCave = caveEntry != null && caveEntry.InCave;

        // If the flashlight is on and the player is out of the cave, turn it off
        if (flashlight.enabled && !isInCave)
        {
            TurnOffFlashlight();
        }
        // If the flashlight is off and the player is out of the cave, turn it on
        else if (!flashlight.enabled && !isInCave)
        {
            TurnOnFlashlight();
        }
        // If the flashlight is on and the player is in the cave, turn it off
        else if (flashlight.enabled && isInCave)
        {
            TurnOffFlashlight();
        }
        // If the flashlight is off and the player is in the cave, turn it on
        else if (!flashlight.enabled && isInCave)
        {
            TurnOnFlashlight();
        }
    }
    
    void TurnOnFlashlight()
    {
        flashlight.enabled = true;
        player_flash_spot.enabled = true;

        player_spotlight.enabled = !caveEntry.InCave;

        SetBatteryVisibility(true);
        isRecharging = false; // Stop recharging when turning on the flashlight
    }


    void TurnOffFlashlight()
    {
        flashlight.enabled = false;
        player_flash_spot.enabled = false;
        player_spotlight.enabled = true;
        isRecharging = true; // Start recharging when turning off the flashlight
        canToggleFlashlight = true; // Allow toggling the flashlight again
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
                player_flash_spot.enabled = false;
                player_spotlight.enabled = caveEntry.InCave ? false : true;

                isRecharging = true; // Start recharging when the battery is empty
                canToggleFlashlight = false; // Disable flashlight toggle until the battery is full
            }
        }
        else if (isRecharging)
        {

            if(caveEntry.InCave) 
            {
                player_spotlight.enabled = false;
            }
            else
            {
                player_spotlight.enabled = true;
            }
            // Recharge battery
            battery.fillAmount += batteryRechargeRate * (Time.deltaTime / 1);

            // Ensure battery level stays within the range [0, 1]
            battery.fillAmount = Mathf.Clamp01(battery.fillAmount);

            // Hide the battery meter when the battery is full
            if (battery.fillAmount >= 1f)
            {
                SetBatteryVisibility(false);
                isRecharging = false;
                canToggleFlashlight = true; // Enable flashlight toggle when the battery is full
            }
        }
    }


    void SetBatteryVisibility(bool visible)
    {
        battery.gameObject.SetActive(visible);
    }
}
