using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    [SerializeField] private Image height_fear_meter;
    [SerializeField] private AudioSource heart_beat_audio;
    [SerializeField] public AudioSource footsteps;
    
    [SerializeField] private Health player_health;
    [SerializeField] private Light2D player_light;
    private PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        height_fear_meter.fillAmount = 0;
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D))) && (movement.IsGrounded() || movement.IsOnPlatform()))
        {
            footsteps.enabled = true;
        }
        else
        {
            footsteps.enabled = false;
        }

        HeartBeatAudio();
        KillPlayer();
    }
    void HeartBeatAudio()
    {

        // Check if fillAmount is greater than 0.5 and play audio if true.
        if (height_fear_meter.fillAmount > 0.4f && !heart_beat_audio.isPlaying)
        {
            heart_beat_audio.Play();
        }
        else if (height_fear_meter.fillAmount <= 0.4f && heart_beat_audio.isPlaying)
        {
            // Stop the audio if the fillAmount is not greater than 0.5.
            heart_beat_audio.Stop();
        }

        // Check if fillAmount is greater than 0.8 and adjust pitch.
        if (height_fear_meter.fillAmount > 0.7f)
        {
            heart_beat_audio.pitch = 1.5f; // Set pitch to 1.5
        }
        else if (height_fear_meter.fillAmount <= 0.7f)
        {
            heart_beat_audio.pitch = 1.0f; // Reset pitch to default
        }
    }
    void KillPlayer()
    {
        if (height_fear_meter.fillAmount == 1)
        {
            heart_beat_audio.Stop();
        }
    }

}
