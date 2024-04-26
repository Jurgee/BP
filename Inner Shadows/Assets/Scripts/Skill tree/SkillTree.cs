using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for TextMesh Pro

public class SkillTree : MonoBehaviour
{
    public static bool skillTreeActive;

    public GameObject skillTree;
    public float skillPoints; // Number of skill points
    public TextMeshProUGUI skillPointsText; // Reference to TextMesh Pro component

    void Start()
    {
        skillTree.SetActive(false);
        skillTreeActive = false;
        skillPoints = 4;


        // Initialize the skill points text
        UpdateSkillPointsText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (skillTreeActive)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        skillTree.SetActive(false);
        Time.timeScale = 1f;
        skillTreeActive = false;
    }

    void Pause()
    {
        skillTree.SetActive(true);
        Time.timeScale = 0f; // Freeze game
        skillTreeActive = true;
    }

    public void AddSkillPoint(float skill)
    {
        skillPoints += skill; // Increase skill points
        UpdateSkillPointsText(); // Update the text
    }

    public void RemoveSkillPoint()
    {
        if (skillPoints > 0) // Ensure skill points don't go negative
        {
            skillPoints--;
            UpdateSkillPointsText(); // Update the text
        }
    }

    private void UpdateSkillPointsText()
    {
        if (skillPointsText != null) // Ensure the component is assigned
        {
            skillPointsText.text = $"{skillPoints}"; // Update text with current skill points
        }
    }
}