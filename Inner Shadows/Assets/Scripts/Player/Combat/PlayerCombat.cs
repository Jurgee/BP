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

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public bool swordAcquired = false;
    public int damage;
    void Start()
    {
        animator = GetComponent<Animator>();
        damage = 1;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) == true && canAttack && !PauseMenu.gameIsPaused && swordAcquired && !SkillTree.skillTreeActive) // left mouse clicked, can attack, got sword and the game is not paused
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack"); // Set trigger to attack
        FindObjectOfType<AudioManager>().Play("Slash");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
          enemy.GetComponent<Enemy>().TakeEnemyDamage(damage);
        }
        StartCoroutine(AttackCooldown()); // Start cooldown
    }

    IEnumerator AttackCooldown() // Wait to finish the animation
    {
        
        canAttack = false; // Disable attacking while the animation is playing
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for the animation to end
        canAttack = true; // Enable attacking again
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
