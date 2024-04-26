using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float chaseDistance;
    public float moveSpeed = 5f;
    [SerializeField] private Transform enemy;
    [SerializeField] private Animator animator;

    public float x;
    public float y;
    public float z;
    
    void Update()
    {
        // Calculate the distance between the boss and the player
        float distanceToPlayer = Vector3.Distance(enemy.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Move towards the player's position
            if (enemy.position.x > player.position.x)
            {
                enemy.position += Vector3.left * moveSpeed * Time.deltaTime;
                animator.SetBool("moving", true);
            }
            else if (enemy.position.x < player.position.x)
            {
                enemy.position += Vector3.right * moveSpeed * Time.deltaTime;
                animator.SetBool("moving", true);
            }
        }
        

        // Flip the boss sprite based on the player's position
        if (enemy.position.x > player.transform.position.x)
        {
            // Face left
            enemy.localScale = new Vector3(-x, y,z);

        }
        else
        {
            // Face right
            enemy.localScale = new Vector3(x, y, z);
        }
    }
}

