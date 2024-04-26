/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for flashlight funcs
 */
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public Light2D flashlight;
    [SerializeField] public Image battery;
    [SerializeField] private Light2D playerFlashSpot;
    [SerializeField] private Light2D playerSpotlight;

    public float batteryDrainRate;
    private float batteryRechargeRate = 0.2f;
    

    private bool isRecharging = false;
    private bool canToggleFlashlight = true;
    public bool canUseFlashlight;
   

    private CaveEntry[] caveEntries; 
  


    private void Awake()
    {
        playerSpotlight.enabled = true;
        canToggleFlashlight = true;
        playerFlashSpot.enabled = false;
        flashlight.enabled = false;
        SetBatteryVisibility(false);

        // Get the CaveEntry script attached to the same GameObject
        caveEntries = GameObject.FindObjectsOfType<CaveEntry>(); // Find all caveEntries
       
        batteryDrainRate = 0.4f;
    }

    void Update()
    {
        if (!PauseMenu.gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.F) && canToggleFlashlight && canUseFlashlight)
            {
                ToggleFlashlight();
            }

            UpdateBatteryLevel();
        }

    }

    void ToggleFlashlight()
    {
        bool isInCave = false;
        foreach (var entry in caveEntries)
        {
            if (entry != null && entry.InCave)
            {
                isInCave = true;
                break; // Exit loop early if a valid entry is found
            }
        }


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
        foreach (var entry in caveEntries)
        {
            flashlight.enabled = true;
            playerFlashSpot.enabled = true;
            FindObjectOfType<AudioManager>().Play("FlashlightON");
            playerSpotlight.enabled = !entry.InCave;

            SetBatteryVisibility(true);
            isRecharging = false; // Stop recharging when turning on the flashlight
        }
    }


    void TurnOffFlashlight()
    {
        foreach (var entry in caveEntries)
        {
            flashlight.enabled = false;
            playerFlashSpot.enabled = false;
            FindObjectOfType<AudioManager>().Play("FlashlightOFF");
            playerSpotlight.enabled = !entry.InCave;

            isRecharging = true; // Start recharging when turning off the flashlight
            canToggleFlashlight = true; // Allow toggling the flashlight again
        }
    }


    void UpdateBatteryLevel()
    {
        foreach (var entry in caveEntries)
        {
            // Adjust battery level based on flashlight state
            if (flashlight.enabled)
            {
                // Drain battery
                battery.fillAmount -= batteryDrainRate * (Time.deltaTime / 15);

                // Decrease flashlight intensity as the battery level goes below 0.5
                flashlight.intensity = Mathf.Lerp(4f, 0f, 1f - (battery.fillAmount / 0.5f));
                playerFlashSpot.intensity = Mathf.Lerp(1f, 0f, 1f - (battery.fillAmount / 0.5f));
                playerSpotlight.enabled = entry.InCave ? false : true;

                // If the battery is empty, turn off the flashlight and start recharging
                if (battery.fillAmount <= 0f)
                {
                    TurnOffFlashlight();
                    canToggleFlashlight = false; // Disable flashlight toggle until the battery is full
                }

            }
            else if (isRecharging)
            {
                playerSpotlight.enabled = entry.InCave ? false : true;
                // Recharge battery
                battery.fillAmount += batteryRechargeRate * (Time.deltaTime / 15);

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
            else
            {
                playerSpotlight.enabled = entry.InCave ? false : true;
            }
        }
    }
    void SetBatteryVisibility(bool visible)
    {
        battery.gameObject.SetActive(visible);
    }

}
