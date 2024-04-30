/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the enemy combat
 */
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask crystalLayer;
    private float cooldownTimer = Mathf.Infinity;

    public bool crystalHit = false;
    public EnemyMovement enemyMovement;
    public bool spiky;
    //References
    private Animator anim;
    private Health playerHealth;
    public Enemy enemyHealth;
    
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();

    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                if (enemyMovement != null) // Ensure the component is present
                {
                    enemyMovement.enabled = false; // Disable the movement script
                }
                
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                

            }
        }

    }
    // Enemy sees the player
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);

        RaycastHit2D crystal =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0, Vector2.left, 0, crystalLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        if (crystal.collider != null)
        {
            crystalHit = true;
        }
        if (crystal.collider == null)
        {
            crystalHit = false;
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // Damage player
    private void DamagePlayer()
    {
        if (PlayerInSight())
            if (spiky) // Fear of the spiked objects is on
            {
                playerHealth.TakeSpikyDamage(damage);
            }
            else
            {
                playerHealth.TakeDamage(damage);
            }
        if (enemyMovement != null) // Ensure the component is present
        {
            enemyMovement.enabled = true; // Enable the movement script
        }
    }

    // Damage boss only for Spike
    private void DamageBoss()
    {
        if (crystalHit)
        {
            enemyHealth.TakeEnemyDamage(25);
            crystalHit = false;
        }
    }
}
