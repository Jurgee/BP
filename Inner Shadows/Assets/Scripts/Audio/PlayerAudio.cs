using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    [SerializeField] private Image height_fear_meter;
    [SerializeField] private AudioSource heart_beat_audio;
    
    [SerializeField] private Health player_health;
    [SerializeField] private Light2D player_light;


    // Start is called before the first frame update
    void Start()
    {
        height_fear_meter.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
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

            player_health.GetComponent<Health>().TakeDamage(10);
            player_light.intensity = 0;
            heart_beat_audio.Stop();


        }
    }

}
