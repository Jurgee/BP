using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
