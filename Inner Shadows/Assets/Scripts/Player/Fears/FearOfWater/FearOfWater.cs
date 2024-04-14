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
    public bool inWater;
    // Start is called before the first frame update
    void Start()
    {
        waterMeter.fillAmount = 0f;
        inWater = false;
    }

    void Update()
    {
        if (inWater)
        {
            waterMeter.fillAmount += 0.02f;
        }
        else
        {
            waterMeter.fillAmount -= 0.02f;
        }
        ColorChanger();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rain") || other.CompareTag("Waterfall") || other.CompareTag("Ocean"))
        {
            inWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rain") || other.CompareTag("Waterfall") || other.CompareTag("Ocean"))
        {
            inWater = false;
        }
    }
    private void ColorChanger()
    {
        Color fearColor = Color.Lerp(Color.blue, Color.red, waterMeter.fillAmount);
        waterMeter.color = fearColor;
    }
}
