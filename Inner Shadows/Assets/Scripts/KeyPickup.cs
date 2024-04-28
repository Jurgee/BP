/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the key pick
 */
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Gate gate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gate.keyPicked++;
            FindObjectOfType<AudioManager>().Play("keyPick");
            gameObject.SetActive(false);
        }
    }
}
