/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the collectible hearts
 */
using UnityEngine;

public class HealthCollect : MonoBehaviour
{
    [SerializeField] private float healthValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            FindObjectOfType<AudioManager>().Play("heartPick");
            gameObject.SetActive(false);
        }
    }
}
