using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCollect : MonoBehaviour
{
    public HealthPotions potions;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            potions.AddHeatlhPotion();
            FindObjectOfType<AudioManager>().Play("heartPick");
            gameObject.SetActive(false);
        }
    }
}
