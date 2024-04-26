
using System;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [SerializeField] private float idleDuration;
    private float idleTimer;

    [SerializeField] private Transform enemy;
    [SerializeField] private Animator animator;
    public bool reverse;
    private void Awake()
    {
        initScale = enemy.localScale;

    }
    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveeInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveeInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }
    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }
    private void DirectionChange()
    {
        animator.SetBool("moving", false);

        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }

    }
    private void MoveeInDirection(int _direction)
    {
        if (reverse)
        {

            idleTimer = 0;
            animator.SetBool("moving", true);
            enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
            _direction *= -1;
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        }
        else
        {
            idleTimer = 0;
            animator.SetBool("moving", true);
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
            enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
        }
    }
}

