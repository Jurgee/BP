/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for the story scene
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Story : MonoBehaviour
{
    public AudioSource audioSource; 
    public List<AudioClip> soundClips; 
    public float delayBetweenClips = 0.5f; // Delay between each sound in seconds
    public string sceneToLoad = "Game"; // Name of the scene to load

    public AudioSource backgroundMusicSource; 
    public AudioClip backgroundMusicClip; 
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component if not assigned
        }

        if (audioSource != null && soundClips.Count > 0)
        {
            StartCoroutine(PlaySoundSequence()); // Start the coroutine to play the sounds
        }

        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true; // Ensure background music loops
            backgroundMusicSource.Play();
        }
    }

    private IEnumerator PlaySoundSequence()
    {
        // Loop through each sound clip and play it
        foreach (var clip in soundClips)
        {
            if (clip != null) // Ensure the clip is valid
            {
                audioSource.clip = clip; // Set the current clip to play
                audioSource.Play(); // Play the clip

                // Wait for the clip to finish and then add the delay
                yield return new WaitForSeconds(clip.length + delayBetweenClips);
            }
        }

        // After the last clip has played, change the scene
        ChangeScene();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad); // Load the specified scene
    }
}