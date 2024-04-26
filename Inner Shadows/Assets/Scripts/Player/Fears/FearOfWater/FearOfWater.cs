/*
 * Inner shadows
 * Author: Jiøí Štípek
 * Description: Script for fear of dark phobia
 */

using UnityEngine;
using UnityEngine.UI;
public class FearOfWater : MonoBehaviour
{
    [SerializeField] public Image waterMeter;
    public Health health;
    public bool inRain;
    public bool inWaterfall;
    public bool inOcean;

    public bool level1;
    public bool level2;
    public bool level3;
    // Start is called before the first frame update
    void Start()
    {
        waterMeter.fillAmount = 0f;
        inRain = false;
        inWaterfall = false;
        inOcean = false;

        level1 = false;
        level2 = false;
        level3 = false;
    }

    void Update()
    {
        if ((inRain && !level1) || (inWaterfall && !level2) || (inOcean && !level3))
        {
            waterMeter.fillAmount += 0.8f * Time.deltaTime;
        }
        else
        {
            waterMeter.fillAmount -= 0.8f * Time.deltaTime;
        }
        ColorChanger();
        if (waterMeter.fillAmount >= 1.0f)
        {
            health.TakeDamage(100f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rain"))
        {
            inRain = true;
        }

        if (other.CompareTag("Waterfall"))
        {
            inWaterfall = true;
        }

        if (other.CompareTag("Ocean"))
        {
            inOcean = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rain"))
        {
            inRain = false;
        }

        if (other.CompareTag("Waterfall"))
        {
            inWaterfall = false;
        }

        if (other.CompareTag("Ocean"))
        {
            inOcean = false;
        }
    }
    private void ColorChanger()
    {
        Color fearColor = Color.Lerp(Color.blue, Color.red, waterMeter.fillAmount);
        waterMeter.color = fearColor;
    }


}
