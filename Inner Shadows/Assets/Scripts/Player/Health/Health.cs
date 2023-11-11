using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private AudioSource player_death_sound;
    [SerializeField] private float starting_health;
    public float current_health { get; private set; }
    private bool dead;
    private void Awake()
    {
        current_health = starting_health;
    }

    public void TakeDamage(float damage)
    {
        current_health = Mathf.Clamp(current_health - damage, 0, starting_health);

        if (current_health < 0)
        {
            player_death_sound.Play();
            GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            //if (!dead)
            //{
            //    GetComponent<PlayerMovement>().enabled = false;
            //    dead = true;
            //}
        }
    }


    public void AddHealth(float _value)
    {
        current_health = Mathf.Clamp(current_health + _value, 0, starting_health);
    }

}
