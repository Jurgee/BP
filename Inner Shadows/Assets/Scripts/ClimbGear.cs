/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the climbing gear
 */
using UnityEngine;

public class ClimbGear : MonoBehaviour
{
    public PlayerMovement movement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            movement.canClimb = true;
            gameObject.SetActive(false);
        }
    }
}
