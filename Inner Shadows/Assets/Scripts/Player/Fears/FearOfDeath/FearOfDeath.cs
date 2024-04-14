using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class FearOfDeath : MonoBehaviour
{
    [SerializeField] public Image deathMeter;
    public int deadCounter;
    public int maxDeaths;
    private float smoothSpeed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        deadCounter = 0;
        maxDeaths = 10;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the fillAmount based on the deadCounter
        float targetFillAmount = (float)deadCounter / maxDeaths;
        // Smoothly adjust the fillAmount
        deathMeter.fillAmount = Mathf.Lerp(deathMeter.fillAmount, targetFillAmount, smoothSpeed * Time.deltaTime);
        ColorChanger();
    }
    private void ColorChanger()
    {
        Color fearColor = Color.Lerp(Color.white, Color.red, deathMeter.fillAmount);
        deathMeter.color = fearColor;
    }
}
