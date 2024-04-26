using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillpointPickup : MonoBehaviour
{
    public SkillTree tree;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tree.AddSkillPoint(1f);
            FindObjectOfType<AudioManager>().Play("skillPick");

            gameObject.SetActive(false);
        }
    }
}
