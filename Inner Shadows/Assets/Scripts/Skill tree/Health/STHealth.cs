/*
 * Inner shadows
 * Author: Ji�� �t�pek
 * Description: Script for the health skill tree
 */
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class STHealth : MonoBehaviour
{
    public Image[] levelImages; // An array of 3 UI Images
    public Health health;       // Assuming you have a 'Health' script/component
    public SkillTree skillTree; // Skill points
    public GameObject skillInfo;
    public TextMeshProUGUI skillInfoText; // Reference to TextMesh Pro component

    private bool F_click;
    private bool S_click;
    private bool T_click;

    void Start()
    {
        levelImages[0].color = Color.white; // First image to white
        levelImages[1].color = Color.black; // Second image to black
        levelImages[2].color = Color.black; // Third image to black
        
        F_click = false;
        S_click = false;
        T_click = false;

        skillInfo.SetActive(false);
    }

    // Helper method to change the color of a specific image
    public void ChangeImageColor(int index, Color color)
    {
        if (index >= 0 && index < levelImages.Length) // Check valid index
        {
            if (levelImages[index] != null)
            {
                levelImages[index].color = color; // Change the color
            }
        }
    }

    // Define the color from the hex code (06F51A)
    private Color GetLevelColor()
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString("#06F51A", out newColor))
        {
            return newColor; // Successfully converted hex to Unity color
        }
        return Color.white; // Fallback to white if conversion fails
    }

    // Update the health and change the color of the first image
    public void Level1()
    {
        if (!F_click && skillTree.skillPoints > 0)
        {
            health.starting_health = 8f;
            health.current_health = health.current_health + 3f;

            ChangeImageColor(0, GetLevelColor()); // Change the first image
            ChangeImageColor(1, Color.white); // change the second to white
            F_click = true;
            skillTree.RemoveSkillPoint();
        }

    }

    // Update the health and change the color of the second image
    public void Level2()
    {
        if (!S_click && F_click && skillTree.skillPoints > 0)
        {
            health.starting_health = 10f;
            health.current_health = health.current_health + 2f;

            ChangeImageColor(1, GetLevelColor()); // Change the second image
            ChangeImageColor(2, Color.white);
            S_click = true;
            skillTree.RemoveSkillPoint();
        }

        
        
    }

    // Update the health and change the color of the third image
    public void Level3()
    {
        if (!T_click && S_click && skillTree.skillPoints > 0)
        {
            health.starting_health = 15f;
            health.current_health = health.current_health + 5f;

            ChangeImageColor(2, GetLevelColor()); // Change the third image
            T_click = true;
            skillTree.RemoveSkillPoint();
        }
    }

    public void HoverOnLevel1()
    {
        // Activate the skillInfo GameObject
        skillInfo.SetActive(true);

        // Set the text
        SetSkillInfoText("Health level 1");
    }
    public void HoverOnLevel2()
    {
        // Activate the skillInfo GameObject
        skillInfo.SetActive(true);

        // Set the text
        SetSkillInfoText("Health level 2");
    }
    public void HoverOnLevel3()
    {
        // Activate the skillInfo GameObject
        skillInfo.SetActive(true);

        // Set the text
        SetSkillInfoText("Health level 3");
    }

    public void HoverOff()
    {
        // Deactivate the skillInfo GameObject when pointer exits
        skillInfo.SetActive(false);
    }

    private void SetSkillInfoText(string newText)
    {
        // Ensure the TextMeshPro component is assigned
        if (skillInfoText != null)
        {
            // Set the new text
            skillInfoText.text = newText;
        }
    }

}

