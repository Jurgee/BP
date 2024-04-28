/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the flashlight pickup
 */
using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public Flashlight flashlight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            flashlight.canUseFlashlight = true;
            gameObject.SetActive(false);
        }
    }
}
