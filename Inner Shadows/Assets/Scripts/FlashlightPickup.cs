using System.Collections;
using System.Collections.Generic;
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
