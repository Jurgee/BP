/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the enemy
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Water")]
    [SerializeField] public STWater water;
    [SerializeField] HealthPotions health;
    [SerializeField] PlayerCombat player;
    public SkillTree skillTree;
    public bool healthPotions;
    public bool textP;

    [Header("Sharp")]
    public bool canDropSword;
    [SerializeField] Health playerHealth;
    public bool textS;

    [Header("Unknown")]
    [SerializeField] MinimapControl minimap;
    public bool minimapAcq;
    public FearOfUnknown unknown;
    public bool textU;

    [Header("Height")] 
    public FearOfHeights height;
    public bool fearOfHeights;
    public bool textH;

    [Header("Dark")] 
    public bool scared;
    public STBattery battery;
    public FearOfDark dark;
    public CaveHand hand;
    public bool textF;

    [Header("Final")] 
    public bool final;

    [Header("Others")]
    public Animator anim;
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] public GameObject fear;
    [SerializeField] public EnemyMovement enemyMovement;
    public bool boss;
    public float delayBeforeShowingEnd = 3f; // Delay before the end is shown

    public float wait;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyMovement = GetComponent<EnemyMovement>();
        textF = false;
        textS = false;
        textP = false;
        textU = false;
        textH = false;
    }

    public void TakeEnemyDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("hit");
        
        if (currentHealth <= 0)
        {
            Die(); 
        }
    }


    void Die()
    {
        anim.SetBool("isDead", true); // Play the die animation
        if (enemyMovement != null) // Ensure the component is present
        {
            enemyMovement.enabled = false; // Disable the movement script
        }
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length; // Get the length of the die animation
        StartCoroutine(DestroyAfterAnimation(animationLength + wait)); // Start coroutine to destroy the character after the animation
        if (boss)
        {
            fear.SetActive(false);
        }

        if (canDropSword) // Spike
        {
            player.swordAcquired = true;
            playerHealth.isFearedOfSpikes = false;
            water.spikeDead = true;
            textS = true;
        }

        if (healthPotions) // Nautilus
        {
            water.nautDead = true;
            health.canUseHealthPotions = true;
            skillTree.AddSkillPoint(2f);
            water.Level2();
            water.Level3();
            textP = true;
        }

        if (minimapAcq) // Voidwraith
        {
            minimap.minimapAcquired = true;
            unknown.isFeared = false;
            textU = true;
        }

        if (fearOfHeights) // Necromancer
        {
            height.isFeared = false;
            textH = true;
        }

        if (scared) // Shadowbane
        {
            battery.bubuDead = true;
            skillTree.AddSkillPoint(1f);
            battery.Level3();
            dark.isFeared = false;
            hand.scared = false;
            textF = true;
        }

        if (final)
        {
            StartCoroutine(ShowEndWithDelay());
            SceneManager.LoadScene("End"); // Play the end scene
        }
    }

    IEnumerator DestroyAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the animation to finish
        Destroy(gameObject); // Remove the character
    }

    private IEnumerator ShowEndWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeShowingEnd); // Wait 3 seconds
    }


}
