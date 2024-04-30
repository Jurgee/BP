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
    public AudioSource audioSource; // The AudioSource component for playing the sound sequence
    public List<AudioClip> soundClips; // List of audio clips for the story
    public float delayBetweenClips = 0.5f; // Delay between each sound in seconds
    public string sceneToLoad = "Game"; // The scene to load after the story

    public AudioSource backgroundMusicSource; // The AudioSource component for background music
    public AudioClip backgroundMusicClip; // Background music AudioClip

    private Coroutine soundSequenceCoroutine; // To keep track of the coroutine

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component if not assigned
        }

        if (audioSource != null && soundClips.Count > 0)
        {
            soundSequenceCoroutine = StartCoroutine(PlaySoundSequence()); // Start the coroutine to play the sounds
        }

        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true; // Ensure background music loops
            backgroundMusicSource.Play();
        }
    }

    void Update()
    {
        // Check if the "Enter" key is pressed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) // Enter key or keypad Enter
        {
            if (soundSequenceCoroutine != null)
            {
                StopCoroutine(soundSequenceCoroutine); // Stop the sound sequence coroutine
            }
            ChangeScene(); // Skip to the specified scene
        }
    }

    private IEnumerator PlaySoundSequence()
    {
        foreach (var clip in soundClips)
        {
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();

                yield return new WaitForSeconds(clip.length + delayBetweenClips); // Wait for clip to finish
            }
        }

        ChangeScene(); // Change the scene after all clips have played
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad); // Load the specified scene
    }
}