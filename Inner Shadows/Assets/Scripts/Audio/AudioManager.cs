using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource player_death;
    [SerializeField] private AudioSource player_hearth_beat;
    

    void Start()
    {
        // Set the priority of audio sources
        player_death.priority = 128; // Higher priority
        player_hearth_beat.priority = 64; // Lower priority
    }
}
