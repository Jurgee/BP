/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for combat system for player
 */
using System.Collections;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    private bool canAttack = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) == true && canAttack && !PauseMenu.gameIsPaused) // left mouse clicked, can attack and the game is not paused
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack"); // Set trigger to attack
        FindObjectOfType<AudioManager>().Play("Slash");
        StartCoroutine(AttackCooldown()); // Start cooldown
    }

    IEnumerator AttackCooldown() // Wait to finish the animation
    {
        
        canAttack = false; // Disable attacking while the animation is playing
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for the animation to end
        canAttack = true; // Enable attacking again
    }
}
