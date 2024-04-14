/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for damage with spikes
 */
using UnityEngine;

public class SpikyDamage : MonoBehaviour
{
    private float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // collided with player
        {
            collision.GetComponent<Health>().TakeSpikyDamage(damage);
        }
    }
}
